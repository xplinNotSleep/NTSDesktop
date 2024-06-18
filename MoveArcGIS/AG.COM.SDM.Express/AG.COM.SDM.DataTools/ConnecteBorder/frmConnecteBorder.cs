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
    /// ����
    /// 1���ָ��ͼ���ֳ�4�����ߣ�GetLineByPolygonGemetry
    /// 2�����߽��л�����ж��ǲ����Ѿ��ù�:ConnecteBorderOperator��ContainGeo
    /// 3,���Է��飬������ͬ�ķ���һ��:GetProperySameRecord,GetHasTable
    /// 4,�����Է���Ļ����ϣ��ڽ��пռ���飬���ڵ�ǰ�����ڻ������:GetspatialGroupe,SpatialGroupeByLine
    /// 5,�����ݲ�Կ��ܽӱߵ�Ҫ�ؽ��з���ͺϲ���GetSameFeatureGroup��GetSameFeature
    /// </summary>
    public partial class frmConnecteBorder : Form
    {
        #region ����

        private List<int> RecordList = null;//�ӱߵļ�¼
        ISet deleteFeatSet;//Ҫɾ����Ҫ�ؼ�
        List<string> GeoList = new List<string>();//��ͼ�����
        //��������ͬ�Ļ����ϣ�����ռ����
        Dictionary<List<IFeature>, ISelectionSet> featDict = new Dictionary<List<IFeature>, ISelectionSet>();
        IFeatureClass featClass = null;//Ҫ�ӱߵ�Ҫ����
        
        #endregion

        #region �Զ���˽�з���

        /// <summary>
        /// �����ֶ�
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
        /// ��ת�ɷֶε��ߣ��磬һ�����ͷֳ������ߣ�
        /// </summary>
        /// <param name="polygonGeo">��ļ���Ҫ��</param>
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
                    //�ϳ�һ��ֱ�߶�
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
        /// �ж��ǲ����Ѿ��ù����߶�
        /// </summary>
        /// <param name="geo">��ͼ�����</param>
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
            //���ߵ��е�
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
        /// �ӱߴ���
        /// </summary>
        /// <param name="list">���߼���</param>
        /// <param name="distance">�ݲ�</param>
        /// <param name="fieldIndexes">�ֶ�</param>
        /// <param name="JTBFeat">��ͼ��</param>
        private void ConnecteBorderOperator(List<IGeometry> list, double distance, List<int> fieldIndexes, IFeature JTBFeat)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    this.deleteFeatSet = new SetClass();//ɾ����Ҫ�ؼ�
                    IGeometry geo = list[i];
                    if (ContainGeo(geo, this.GeoList)) continue;
                    IFeatureCursor featCur = GetFeatureCursorByBuffer(distance, this.featClass, geo);
                    //IFeature feat = featCur.NextFeature();
                    //if (feat == null) continue;
                    //������ͬ�ļ�¼
                    ArrayList properyList = GetProperySameRecord(featCur, fieldIndexes, this.featClass);
                    if (properyList == null) continue;
                    //�ռ����
                    SpatialGroupeByLine(properyList, JTBFeat);
                    //�ϲ�
                    GetSameFeatureGroup(2 * distance);
                    //�ϲ���ɾ��
                    DeleteFeat();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// ����
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
        /// ���ý�ͼ����߻���õ���֮�ཻ��feature�α�
        /// </summary>
        /// <param name="distance">�������</param>
        /// <param name="featClass">�뻺��Ҫ�����ཻ��Ҫ����</param>
        /// <param name="geo">�������</param>
         /// <returns></returns>
        private IFeatureCursor GetFeatureCursorByBuffer(double distance, IFeatureClass featClass,IGeometry geo)
        {
            try
            {
                ////��ת��
                //IPolyline line = PublicMethod.GetLineFromPolygon(feat);
                //if (line == null) return null;
                //����
                IGeometry burrerGeo = GetGeometryByBuffer(geo, distance);
                if (burrerGeo == null) return null;
                //��ת��
                IGeometry polyGeo = PublicMethod.GetPolygonFromLine(burrerGeo);
                if (polyGeo == null) return null;
                //�ռ����
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
        /// ����Hashtable����ת�����õ�������ͬ�ļ�¼
        /// </summary>
        /// <param name="featCur">Ҫ�����Ҫ���α�</param>
        /// <param name="fieldIndexes">�ֶ���ż�</param>
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
                    //��һ��Ҫ�ص�����ת��һ���ַ���
                    string fieldValue = string.Empty;
                    for (int i = 0; i <= fieldIndexes.Count - 1; i++)
                    {
                        fieldValue += GetFieldValueString(feat.get_Value(fieldIndexes[i])) + ",";
                    }
                    //��¼��
                    int objectid = int.Parse(feat.get_Value(oidIndex).ToString());
                    //�Ƿ�����ͬ�ļ�¼
                    if (table.Contains(fieldValue) == false)
                        table.Add(fieldValue, objectid);
                    else
                    {
                        //int c = (int)table[fieldValue];
                        //�ݼ�������ͬ�ģɣĺ�
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
        /// �õ�������ͬ�ļ�¼
        /// </summary>
        /// <param name="featCur">����õ���Ҫ���α�</param>
        /// <param name="fieldIndexes">�ֶ����</param>
        /// <returns></returns>
        private ArrayList GetProperySameRecord(IFeatureCursor featCur, List<int> fieldIndexes,IFeatureClass featClass)
        {
            try
            {
                Hashtable table = GetHasTable(featCur, fieldIndexes);
                if (table == null||table.Count<1) return null;
                ArrayList List = new ArrayList();//������ͬ�ļ�¼
                IEnumerator enumrator = table.Values.GetEnumerator();
                while (enumrator.MoveNext())
                {
                    //�ж��ǲ���������ͬ�ļ�¼
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
        /// �ռ����
        /// </summary>
        /// <param name="list">������ͬ������</param>
        /// <param name="JTBFeat">��ͼ��Ҫ��</param>
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
        /// �ж��Ǽ�ά
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
        /// �ռ����
        /// </summary>
        /// <param name="featList">������ͬ�ļ�¼</param>
        /// <param name="JTBFeat">��ͼ��Ҫ��</param>
        private void GetspatialGroupe(List<IFeature> featList,IFeature JTBFeat)
        {
            try
            {
                //�ռ����
                List<IFeature> inList = new List<IFeature>();//�������Ҫ��
                //��ȡһ���յ�ISelectionSet
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
        /// �ӱߵ�Ҫ����
        /// </summary>
        /// <param name="distance"></param>
        private void GetSameFeatureGroup(double distance)
        {
            try
            {
                
                if (this.featDict.Count < 1) return;
                //������ͬ���ռ����������
                IEnumerator keyEnum = this.featDict.Keys.GetEnumerator();
                while (keyEnum.MoveNext())
                {
                    //�������Ҫ��
                    List<IFeature> inList = keyEnum.Current as List<IFeature>;
                    ISelectionSet outSet = this.featDict[inList];

                    ArrayList list = new ArrayList();//��ͬ��Ҫ�ط���һ��
                    for (int i = 0; i < inList.Count; i++)
                    {
                        IFeature feat = inList[i];
                        //����
                        IGeometry geo = GetGeometryByBuffer(feat.Shape, distance);
                        if (geo == null) return;
                        IFeatureCursor featCur = PublicMethod.CreateFeatCurBySpatialFilter(feat, outSet, esriSpatialRelEnum.esriSpatialRelIntersects);
                        //�����������ͬ��Ҫ�ط���һ��
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
                    //���·��飬�ϲ���ͬ��Ҫ��
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
        /// �ϲ���ͬ��Ҫ��
        /// </summary>
        /// <param name="list">������ͬ,�ռ�ӱߵ�Ҫ����</param>
        /// <param name="set">��ͼ���ⲿҪ��</param>
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
                    //�ϲ���ͬ���Ҫ��
                    if (indexList.Count > 1)
                    {
                        List<IFeature> tempList = list[0] as List<IFeature>;
                        for (int k = 1; k < indexList.Count; k++)
                        {
                            int index = indexList[k];
                            List<IFeature> addList = list[index] as List<IFeature>;
                            //�ϲ�
                            tempList.AddRange(addList);
                            //ɾ��
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
        /// �ֶ����
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
        /// ��OID�ҵ�FEATURE
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
        /// �ϲ���ͬ�ļ�¼
        /// </summary>
        /// <param name="list">��ͬ��Ҫ�ؼ�</param>
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
        /// ɾ��Ҫ��
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

        #region �ֶ�����
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
        
        //������
        private void tbOpen_Click(object sender, EventArgs e)
        {
            IFeatureClass featClass = PublicMethod.OpenFeatureClass();
            if (featClass == null) return;
            if (featClass.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                MessageBox.Show("���û�нӱߴ���", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (featClass.ShapeType != esriGeometryType.esriGeometryPolygon && featClass.ShapeType != esriGeometryType.esriGeometryPolyline)
            {
                MessageBox.Show("��ȷ���Ƿ����߻����", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this.GeoList = new List<string>();
            RecordList = new List<int>();
            txtLayer.Text = featClass.AliasName;
            txtLayer.Tag = featClass;
            this.featClass = featClass;
            LoadFields(featClass);
        }

        //�򿪽�ͼ��
        private void tbOpenJTB_Click(object sender, EventArgs e)
        {
            IFeatureClass featClass = PublicMethod.OpenFeatureClass();
            if (featClass == null) return;
            if (featClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                MessageBox.Show("������Ľ�ͼ��", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            txtJTB.Text = featClass.AliasName;
            txtJTB.Tag = featClass;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.txtLayer.Tag == null)
            {
                MessageBox.Show("��򿪽ӱ����ݲ�", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if(txtJTB.Tag==null)
            {
                MessageBox.Show("��򿪽�ͼ��", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if(numBuffer.Value<=0)
            {
                  MessageBox.Show("�ݲ�Ҫ����0", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (lbNeedFields.Items.Count<1)
            {
                MessageBox.Show("��ѡ����Ҫ������ֶ�", "��ܰ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                try
                {
                    double distance = Convert.ToDouble(numBuffer.Value);
                    IFeatureClass JTBFeatClass = txtJTB.Tag as IFeatureClass;//��ͼ��
                    IFeatureClass featClass = txtLayer.Tag as IFeatureClass;//Ҫ�ӱߵ�ͼ��
                    List<int> fieldIndexes = GetFieldIndexes(featClass);//�ֶ����

                    IFeatureCursor JTBCur = JTBFeatClass.Search(null, false);
                    IFeature JTBFeat = JTBCur.NextFeature();

                    //������
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
                        frm.DisplayInfo("�������ڽӱߴ���" + currentNum + "/" + totalCount);
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