using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    /// <summary>
    /// 流程
    /// 1，分割接图表，分成4条边线：GetLineByPolygonGemetry
    /// 2，对线进行缓冲和判断是不是已经用过:ConnecteBorderOperator，ContainGeo
    /// 3,属性分组，属性相同的放在一组:GetProperySameRecord,GetHasTable
    /// 4,在属性分组的基础上，在进行空间分组，即在当前巨型内或外分组:GetspatialGroupe,SpatialGroupeByLine
    /// 5,利用容差对可能接边的要素进行分组和合并：GetSameFeatureGroup，GetSameFeature
    /// </summary>
    public partial class frmConnecteBorder : Form
    {
        #region 变量

        private List<int> RecordList = null;//接边的记录
        ISet deleteFeatSet;//要删除的要素集
        List<string> GeoList = new List<string>();//接图表边线
        //在属性相同的基础上，内外空间分组
        Dictionary<List<IFeature>, ISelectionSet> featDict = new Dictionary<List<IFeature>, ISelectionSet>();
        IFeatureClass featClass = null;//要接边的要素类
        
        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 加载字段
        /// </summary>
        /// <param name="featClass"></param>
        private void LoadFields(IFeatureClass featClass)
        {
            lbNeedFields.Items.Clear();
            lbNotNeedFields.Items.Clear();
            for (int i = 0; i < featClass.Fields.FieldCount; i++)
            {
                IField field = featClass.Fields.get_Field(i);
                if ((field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry) ||
                (field.Name.ToLower() == "shape_length" ||field.Name.ToLower() == "shape_len"|| field.Name.ToLower() == "shape_area"))
                {
                    continue;
                }
                else
                {
                    lbNotNeedFields.Items.Add(field.Name);
                }
            }
        }

        /// <summary>
        /// 面转成分段的线（如，一个巨型分成四条线）
        /// </summary>
        /// <param name="polygonGeo">面的几何要素</param>
        /// <returns></returns>
        private List<IGeometry> GetLineByPolygonGemetry(IGeometry polygonGeo)
        {
            try
            {
                if (polygonGeo.GeometryType != esriGeometryType.esriGeometryPolygon) return null;
                IPointCollection pointColl = polygonGeo as IPointCollection;
                if (pointColl == null) return null;
                IPointCollection pointCollLine = new PolylineClass();
                int num = 0;
                List<IGeometry> lineList = new List<IGeometry>();
                for (int i = 0; i < pointColl.PointCount; i++)
                {
                    IPoint p = pointColl.get_Point(i);
                    object missing = System.Reflection.Missing.Value;
                    pointCollLine.AddPoint(p, ref missing, ref missing);
                    //合成一条直线段
                    if (pointCollLine.PointCount == 2)
                    {
                        lineList.Add(pointCollLine as IGeometry);
                        pointCollLine = new PolylineClass();
                        pointCollLine.AddPoint(p, ref missing, ref missing);
                    }
                }

                return lineList;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

        /// <summary>
        /// 判断是不是已经用过该线段
        /// </summary>
        /// <param name="geo">接图表边线</param>
        /// <param name="GeoList"></param>
        /// <returns></returns>
        private bool ContainGeo(IGeometry geo, List<string> GeoList)
        {
            PointInfo pointInfo = new PointInfo();
            IPointCollection pointColl = geo as IPointCollection;
            if (pointColl == null) return true;
            IPointCollection newPointColl = new PolylineClass();

            IPoint p1 = pointColl.get_Point(0);
            IPoint p2 = pointColl.get_Point(1);
            //边线的中点
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            string str = centerPoint.X + "," + centerPoint.Y;
            if (GeoList.Contains(str))
            {
                return true;
            }
            else
            {
                GeoList.Add(str);
                return false;
            }
        }

        /// <summary>
        /// 接边处理
        /// </summary>
        /// <param name="list">边线集合</param>
        /// <param name="distance">容差</param>
        /// <param name="fieldIndexes">字段</param>
        /// <param name="JTBFeat">接图表</param>
        private void ConnecteBorderOperator(List<IGeometry> list, double distance, List<int> fieldIndexes, IFeature JTBFeat)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    this.deleteFeatSet = new SetClass();//删除的要素集
                    IGeometry geo = list[i];
                    if (ContainGeo(geo, this.GeoList)) continue;
                    IFeatureCursor featCur = GetFeatureCursorByBuffer(distance, this.featClass, geo);
                    //IFeature feat = featCur.NextFeature();
                    //if (feat == null) continue;
                    //属性相同的记录
                    ArrayList properyList = GetProperySameRecord(featCur, fieldIndexes, this.featClass);
                    if (properyList == null) continue;
                    //空间分组
                    SpatialGroupeByLine(properyList, JTBFeat);
                    //合并
                    GetSameFeatureGroup(2 * distance);
                    //合并后删除
                    DeleteFeat();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 缓冲
        /// </summary>
        /// <param name="feat"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        private IGeometry GetGeometryByBuffer(IGeometry geo,double distance)
        {
            ITopologicalOperator topoOpertator = geo as ITopologicalOperator;
            if (topoOpertator == null) return null;
            return topoOpertator.Buffer(distance);
        }

        /// <summary>
        /// 利用接图表边线缓冲得到与之相交的feature游标
        /// </summary>
        /// <param name="distance">缓冲距离</param>
        /// <param name="featClass">与缓冲要素做相交的要素类</param>
        /// <param name="geo">缓冲的线</param>
         /// <returns></returns>
        private IFeatureCursor GetFeatureCursorByBuffer(double distance, IFeatureClass featClass,IGeometry geo)
        {
            try
            {
                ////面转线
                //IPolyline line = PublicMethod.GetLineFromPolygon(feat);
                //if (line == null) return null;
                //缓冲
                IGeometry burrerGeo = GetGeometryByBuffer(geo, distance);
                if (burrerGeo == null) return null;
                //线转面
                IGeometry polyGeo = PublicMethod.GetPolygonFromLine(burrerGeo);
                if (polyGeo == null) return null;
                //空间过滤
                IFeatureCursor featCur = PublicMethod.CreateFeatCurBySpatialFilter(polyGeo, featClass, esriSpatialRelEnum.esriSpatialRelIntersects);
                return featCur;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

        /// <summary>
        /// 利用Hashtable做中转，来得到属性相同的记录
        /// </summary>
        /// <param name="featCur">要处理的要素游标</param>
        /// <param name="fieldIndexes">字段序号集</param>
        /// <returns></returns>
        private System.Collections.Hashtable GetHasTable(IFeatureCursor featCur, List<int> fieldIndexes)
        {
            try
            {
                System.Collections.Hashtable table = new System.Collections.Hashtable();
                if (featCur == null) return null;
                IFeature feat = featCur.NextFeature();
                int oidIndex = -1;
                if (feat != null) 
                oidIndex = feat.Fields.FindField(feat.Class.OIDFieldName);
                while (feat != null)
                {
                    //把一个要素的属性转成一条字符串
                    string fieldValue = string.Empty;
                    for (int i = 0; i <= fieldIndexes.Count - 1; i++)
                    {
                        fieldValue += GetFieldValueString(feat.get_Value(fieldIndexes[i])) + ",";
                    }
                    //记录号
                    int objectid = int.Parse(feat.get_Value(oidIndex).ToString());
                    //是否有相同的记录
                    if (table.Contains(fieldValue) == false)
                        table.Add(fieldValue, objectid);
                    else
                    {
                        //int c = (int)table[fieldValue];
                        //垒加属性相同的ＩＤ号
                        object value = table[fieldValue];
                        table[fieldValue] = value + "," + objectid;
                    }

                    feat = featCur.NextFeature();
                }
                return table;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
                return null;
            }
        }

        private string GetFieldValueString(object fieldValue)
        {
            if (fieldValue.ToString().Trim() == "")
                return "";
            else if (fieldValue is System.DBNull)
                return "";
            else
                return fieldValue.ToString();
        }

        /// <summary>
        /// 得到属性相同的记录
        /// </summary>
        /// <param name="featCur">缓冲得到的要素游标</param>
        /// <param name="fieldIndexes">字段序号</param>
        /// <returns></returns>
        private ArrayList GetProperySameRecord(IFeatureCursor featCur, List<int> fieldIndexes,IFeatureClass featClass)
        {
            try
            {
                Hashtable table = GetHasTable(featCur, fieldIndexes);
                if (table == null||table.Count<1) return null;
                ArrayList List = new ArrayList();//属性相同的记录
                IEnumerator enumrator = table.Values.GetEnumerator();
                while (enumrator.MoveNext())
                {
                    //判断是不是属性相同的记录
                    object obj = enumrator.Current;
                    if (obj.ToString().Contains(","))
                    {
                        string[] strList = obj.ToString().Split(',');
                        List<IFeature> featList = GetFeatListByOID(strList, featClass);
                        List.Add(featList);
                    }
                  
                }
                return List;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
                return null;
            }
        }
       
        /// <summary>
        /// 空间分组
        /// </summary>
        /// <param name="list">属性相同的数组</param>
        /// <param name="JTBFeat">接图表要素</param>
        private void SpatialGroupeByLine(ArrayList propertySmaelist,IFeature JTBFeat)
        {
            try
            {
                this.featDict = new Dictionary<List<IFeature>, ISelectionSet>();
                for (int i = 0; i < propertySmaelist.Count; i++)
                {
                    List<IFeature> featList = propertySmaelist[i] as List<IFeature>;
                    GetspatialGroupe(featList, JTBFeat);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
       
        /// <summary>
        /// 判断是几维
        /// </summary>
        /// <returns></returns>
        private esriGeometryDimension GetGeometryDimension()
        {
            if (this.featClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                return esriGeometryDimension.esriGeometry1Dimension;
            }
            else if (this.featClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                return esriGeometryDimension.esriGeometry2Dimension;
            }
            else
            {
                return esriGeometryDimension.esriGeometry1Dimension;
            }
        }

        /// <summary>
        /// 空间分组
        /// </summary>
        /// <param name="featList">属性相同的记录</param>
        /// <param name="JTBFeat">接图表要素</param>
        private void GetspatialGroupe(List<IFeature> featList,IFeature JTBFeat)
        {
            try
            {
                //空间分组
                List<IFeature> inList = new List<IFeature>();//线上面的要素
                //获取一个空的ISelectionSet
                QueryFilter query = new QueryFilter();
                query.WhereClause = this.featClass.OIDFieldName + "=-1";
                ISelectionSet outSet = this.featClass.Select(query, esriSelectionType.esriSelectionTypeHybrid, esriSelectionOption.esriSelectionOptionNormal, (this.featClass as IDataset).Workspace);
                ITopologicalOperator Opertor = JTBFeat.Shape as ITopologicalOperator;
                for (int i = 0; i < featList.Count; i++)
                {
                    IFeature feat = featList[i];
                    if (Opertor != null)
                    {
                        if(!Opertor.Intersect(feat.Shape,GetGeometryDimension()).IsEmpty)
                        {
                            inList.Add(feat);
                        }
                        else
                        {
                            outSet.Add(feat.OID);
                        }
                    }
                }
                this.featDict.Add(inList, outSet);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 接边的要素组
        /// </summary>
        /// <param name="distance"></param>
        private void GetSameFeatureGroup(double distance)
        {
            try
            {
                
                if (this.featDict.Count < 1) return;
                //属性相同，空间分组后的数组
                IEnumerator keyEnum = this.featDict.Keys.GetEnumerator();
                while (keyEnum.MoveNext())
                {
                    //面里面的要素
                    List<IFeature> inList = keyEnum.Current as List<IFeature>;
                    ISelectionSet outSet = this.featDict[inList];

                    ArrayList list = new ArrayList();//相同的要素放在一起
                    for (int i = 0; i < inList.Count; i++)
                    {
                        IFeature feat = inList[i];
                        //缓冲
                        IGeometry geo = GetGeometryByBuffer(feat.Shape, distance);
                        if (geo == null) return;
                        IFeatureCursor featCur = PublicMethod.CreateFeatCurBySpatialFilter(feat, outSet, esriSpatialRelEnum.esriSpatialRelIntersects);
                        //经过缓冲把相同的要素放在一起
                        List<IFeature> featList = new List<IFeature>();
                        featList.Add(feat);
                        IFeature relationFeat = featCur.NextFeature();
                        while (relationFeat != null)
                        {
                            featList.Add(relationFeat);
                            relationFeat = featCur.NextFeature();
                        }
                        list.Add(featList);
                    }
                    //重新分组，合并相同的要素
                   ArrayList newList= GetSameFeature(list,outSet);
                   Union(newList);
                }
               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
       
        /// <summary>
        /// 合并相同的要素
        /// </summary>
        /// <param name="list">属性相同,空间接边的要素组</param>
        /// <param name="set">接图表外部要素</param>
        /// <returns></returns>
        private ArrayList GetSameFeature(ArrayList list, ISelectionSet set)
        {
            try
            {
                IEnumIDs enumID = set.IDs;
                enumID.Reset();
                int ID = enumID.Next();
                while (ID != -1)
                {
                    IFeature feat = this.featClass.GetFeature(ID);
                    List<int> indexList = new List<int>();
                    for (int j = 0; j < list.Count; j++)
                    {
                        List<IFeature> featList = list[j] as List<IFeature>;
                        if (featList.Contains(feat))
                        {
                            indexList.Add(j);
                        }
                    }
                    //合并相同组的要素
                    if (indexList.Count > 1)
                    {
                        List<IFeature> tempList = list[0] as List<IFeature>;
                        for (int k = 1; k < indexList.Count; k++)
                        {
                            int index = indexList[k];
                            List<IFeature> addList = list[index] as List<IFeature>;
                            //合并
                            tempList.AddRange(addList);
                            //删除
                            list.RemoveAt(index);
                        }
                    }

                    ID = enumID.Next();
                }
                return list;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

        /// <summary>
        /// 字段序号
        /// </summary>
        /// <param name="featClass"></param>
        /// <returns></returns>
        private List<int> GetFieldIndexes(IFeatureClass featClass)
        {
            List<int> fieldIndexes = new List<int>();
            for (int i = 0; i < lbNeedFields.Items.Count; i++)
            {
                string fieldName = lbNeedFields.Items[i] as string;
                int index = featClass.FindField(fieldName);
                fieldIndexes.Add(index);
            }
            return fieldIndexes;
        }

        /// <summary>
        /// 用OID找到FEATURE
        /// </summary>
        /// <param name="strList"></param>
        /// <param name="featClass"></param>
        /// <returns></returns>
        private List<IFeature> GetFeatListByOID(string[] strList,IFeatureClass featClass)
        {
            List<IFeature> list = new List<IFeature>();
            for (int i = 0; i < strList.Length;i++ )
            {
                int oid =Convert.ToInt16(strList[i]);
                IFeature feat = featClass.GetFeature(oid);
                list.Add(feat);
            }
            return list;
        }

        /// <summary>
        /// 合并相同的记录
        /// </summary>
        /// <param name="list">相同的要素集</param>
        private void Union(ArrayList list)
        {
            try
            {
                if (list == null) return;
                for (int i = 0; i < list.Count; i++)
                {
                    List<IFeature> featList = list[i] as List<IFeature>;
                    ITopologicalOperator topoOperator = null;
                    IFeature firstFeat = null;
                    for (int j = 0; j < featList.Count; j++)
                    {
                        IFeature feat = featList[j];
                        if (j == 0)
                        {
                            topoOperator = feat.Shape as ITopologicalOperator;
                            firstFeat = feat;
                            continue;
                        }
                        topoOperator = topoOperator.Union(feat.Shape) as ITopologicalOperator;
                        this.deleteFeatSet.Add(feat);
                    }

                    firstFeat.Shape = topoOperator as IGeometry;
                    firstFeat.Store();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
       
        /// <summary>
        /// 删除要素
        /// </summary>
        /// <param name="list"></param>
        private void DeleteFeat()
        {
            try
            {
                IFeatureEdit featEdit;
                this.deleteFeatSet.Reset();
                featEdit =this.deleteFeatSet.Next() as IFeatureEdit;
                if (featEdit != null)
                    featEdit.DeleteSet(deleteFeatSet);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        public frmConnecteBorder()
        {
            InitializeComponent();
        }

        #region 字段设置
        private void btAddAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <lbNotNeedFields .Items.Count; i++)
            {
                object item = lbNotNeedFields.Items[i];
                lbNeedFields.Items.Add(item);
            }
            lbNotNeedFields.Items.Clear();
        }

        private void btAddOne_Click(object sender, EventArgs e)
        {
            object item = lbNotNeedFields.SelectedItem;
            if (item != null)
                lbNeedFields.Items.Add(item);
            lbNotNeedFields.Items.Remove(item);
        }

        private void btRomoveOne_Click(object sender, EventArgs e)
        {
            object item = lbNeedFields.SelectedItem;
            if (item != null)
                lbNotNeedFields.Items.Add(item);
            lbNeedFields.Items.Remove(item);
        }

        private void btRomoveAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbNeedFields.Items.Count; i++)
            {
                object item = lbNeedFields.Items[i];
                lbNotNeedFields.Items.Add(item);
            }
            lbNeedFields.Items.Clear();
        }
        private void lbNeedFields_DoubleClick(object sender, EventArgs e)
        {
            btRomoveOne_Click(null, null);
        }

        private void lbNotNeedFields_DoubleClick(object sender, EventArgs e)
        {
            btAddOne_Click(null, null);
        }

        #endregion ..........................................
        
        //打开数据
        private void tbOpen_Click(object sender, EventArgs e)
        {
            IFeatureClass featClass = PublicMethod.OpenFeatureClass();
            if (featClass == null) return;
            if (featClass.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                MessageBox.Show("点层没有接边处理", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (featClass.ShapeType != esriGeometryType.esriGeometryPolygon && featClass.ShapeType != esriGeometryType.esriGeometryPolyline)
            {
                MessageBox.Show("请确认是否是线或面层", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this.GeoList = new List<string>();
            RecordList = new List<int>();
            txtLayer.Text = featClass.AliasName;
            txtLayer.Tag = featClass;
            this.featClass = featClass;
            LoadFields(featClass);
        }

        //打开接图表
        private void tbOpenJTB_Click(object sender, EventArgs e)
        {
            IFeatureClass featClass = PublicMethod.OpenFeatureClass();
            if (featClass == null) return;
            if (featClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                MessageBox.Show("请打开面层的接图表", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            txtJTB.Text = featClass.AliasName;
            txtJTB.Tag = featClass;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.txtLayer.Tag == null)
            {
                MessageBox.Show("请打开接边数据层", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if(txtJTB.Tag==null)
            {
                MessageBox.Show("请打开接图表", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if(numBuffer.Value<=0)
            {
                  MessageBox.Show("容差要大于0", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (lbNeedFields.Items.Count<1)
            {
                MessageBox.Show("请选择需要参与的字段", "温馨提示！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                try
                {
                    double distance = Convert.ToDouble(numBuffer.Value);
                    IFeatureClass JTBFeatClass = txtJTB.Tag as IFeatureClass;//接图表
                    IFeatureClass featClass = txtLayer.Tag as IFeatureClass;//要接边的图层
                    List<int> fieldIndexes = GetFieldIndexes(featClass);//字段序号

                    IFeatureCursor JTBCur = JTBFeatClass.Search(null, false);
                    IFeature JTBFeat = JTBCur.NextFeature();

                    //进度条
                    frmProgeress frm = new frmProgeress();
                    frm.Show();
                    frm.Owner = this;

                    int totalCount = JTBFeatClass.FeatureCount(null);
                    frm.SetMaxValue(totalCount);
                    int currentNum = 0;
                    Application.DoEvents();
                    while (JTBFeat != null)
                    {
                        if (frm.Abort)
                        {
                            frm.Abort = false;
                            frm.Dispose();
                            return;
                        }

                        List<IGeometry> geoList=GetLineByPolygonGemetry(JTBFeat.Shape);
                        ConnecteBorderOperator(geoList, distance / 2, fieldIndexes, JTBFeat);
                        currentNum++;
                        frm.SetCurrentVulue(currentNum);
                        frm.DisplayInfo("数据正在接边处理" + currentNum + "/" + totalCount);
                        Application.DoEvents();
                        JTBFeat = JTBCur.NextFeature();
                    }
                    frm.Dispose();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }
        }

    }
}