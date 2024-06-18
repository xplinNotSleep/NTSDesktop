using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ICSharpCode.SharpZipLib.Zip;
using FileStream = System.IO.FileStream;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 矢量数据裁剪窗体类
    /// </summary>
    public partial class FormVectorClip : Form, ISelAreaForm
    {
        private IHookHelperEx m_hookHelperEx;
        public FormVectorClip()
        {
            InitializeComponent();
            this.controlSelArea1.MainForm = this;
            this.FormClosed += FormVectorClip_FormClosed;
        }

 

        /// <summary>
        /// 获取用户区域选择控件
        /// </summary>
        public ControlSelArea SelArea
        {
            get { return this.controlSelArea1; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormVectorClip_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip tbtnPath = new ToolTip();
                tbtnPath.SetToolTip(btnPath, "设置矢量数据输出目录");

                ///初始化图层树
                this.ltvLayer.InitUI(this.m_hookHelperEx.FocusMap as IBasicMap);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }
       
        private void FormVectorClip_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.controlSelArea1.ExitSelect();
        }
        bool IsComplated = false;
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.controlSelArea1.RegionGeometry == null)
            {
                MessageHandler.ShowInfoMsg("请先选择裁剪范围","提示");
                return;
            }

            if (txtPath.Text.Length == 0)
            {
                MessageHandler.ShowInfoMsg("请先选择导出路径", "提示");
                return;
            }

            //获取选择导出的图层
            List<ILayer> tLayers = GetSelectLayer(ltvLayer.Nodes);
            if (tLayers.Count < 1)
            {
                MessageHandler.ShowInfoMsg("请先选择图层", "提示");
                return;
            }
            IsComplated = false;
            TrackProgressDialog tProgress = new TrackProgressDialog();
            tProgress.SubMessage = "正在生成目录……";
            tProgress.DisplayTotal = false;

            if (!System.IO.Directory.Exists(txtPath.Text))
            {
                System.IO.Directory.CreateDirectory(txtPath.Text);
            }

            int tLayerCount = tLayers.Count;

            tProgress.SubMax = tLayerCount;
            tProgress.TopMost = true;
            tProgress.Show();

            string tempDirectory = string.Empty;
            try
            {
                int i = 0;
                DelFeatureClass = new List<IFeatureClass>();
                
                string[] tempfiles = txtPath.Text.Split('\\');
                string filepatch = String.Join("\\", tempfiles, 0, tempfiles.Length - 1);
               
               if(cbEncryption.Checked)
                {
                    //如果加密 则先存放到临时文件加夹
                    tempDirectory = CommonConstString.STR_TempPath + "\\" + tempfiles[tempfiles.Length - 1];
                    if (!System.IO.Directory.Exists(tempDirectory))
                    {
                        System.IO.Directory.CreateDirectory(tempDirectory);
                    }
                    //tempDirectory = txtPath.Text;
                }
                else
                {
                    tempDirectory = txtPath.Text;
                }

                foreach (ILayer tLayer in tLayers)
                {
                    if (!tProgress.IsContinue)
                    {
                        this.Close();
                        return;
                    }

                    IFeatureLayer tFeatureLayer = tLayer as IFeatureLayer;
                    tProgress.SubValue = i+1;
                    tProgress.SubMessage = string.Format("正在导出……{0}({1}/{2})", tLayer.Name,i+1,tLayerCount);
                    Application.DoEvents();
                    string LayerName = GetOraginLayerNameByILayer(tLayer);
                    //创建shp文件
                    IWorkspaceFactory pShpWF = new ShapefileWorkspaceFactoryClass();

                    IWorkspace tWorkspace = pShpWF.OpenFromFile(tempDirectory, 0);
                    IFeatureClass featureClass = CreateShpFile(tWorkspace as IFeatureWorkspace, LayerName, tFeatureLayer.FeatureClass);

                    //要素类导入新建的矢量图层
                    FeactureClass2ShpFile(featureClass, tFeatureLayer.FeatureClass, this.controlSelArea1.RegionGeometry);
                    //关闭资源锁定  
                    IWorkspaceFactoryLockControl ipWsFactoryLock = (IWorkspaceFactoryLockControl)pShpWF;
                    if (ipWsFactoryLock.SchemaLockingEnabled)
                    {
                        ipWsFactoryLock.DisableSchemaLocking();
                    }
                    i++;
                }


                /////////////////////////////
                if (cbEncryption.Checked)
                {
                    string zipedFileName = tempfiles[tempfiles.Length - 1] + ".zip";
                    string strZipFile = System.IO.Path.Combine(filepatch, zipedFileName);

                    //ZipHelper.ZipFiles(tempDirectory, strZipFile, 1, textBox1.Text, "奥格科技");
                    ZipFiles(tempDirectory, strZipFile, 1, textBox1.Text, "奥格科技");

                    Directory.Delete(txtPath.Text, true);
                    Directory.Delete(tempDirectory, true);
                }


                tProgress.SubValue = i;
                tProgress.SubMessage = string.Format("导出完成");
                MessageHandler.ShowInfoMsg("导出成功", Text);
                IsComplated = true;
                Close();
            }
            catch(Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
            finally
            {
                tProgress.AutoFinishClose = true;
                tProgress.SetFinish();

            }
        }
        private void FormVectorClip_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if(IsComplated)
                {
                   
                }
              
              
            }
            catch (Exception ex)
            {

                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
           
        }
        /// <summary>
        /// 删除文件夹以及文件
        /// </summary>
        /// <param name="directoryPath"> 文件夹路径 </param>
        /// <param name="fileName"> 文件名称 </param>
        public static void DeleteDirectory(string directoryPath, string fileName)
        {

            //删除文件
            for (int i = 0; i < Directory.GetFiles(directoryPath).Length; i++)
            {
                if (Directory.GetFiles(directoryPath)[i] == fileName)
                {
                    File.Delete(fileName);
                }
            }

            //删除文件夹
            for (int i = 0; i < Directory.GetDirectories(directoryPath).Length; i++)
            {
                if (Directory.GetDirectories(directoryPath)[i] == fileName)
                {
                    Directory.Delete(fileName, true);
                }
            }
        }
        private void btnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog tFbd = new FolderBrowserDialog();
            if (tFbd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = System.IO.Path.Combine(tFbd.SelectedPath, DateTime.Now.ToString("yyyMMddhhmmss"));
            }

        }

        /// <summary>
        /// 设置Hook对象
        /// </summary>
        /// <param name="pHook"></param>
        public void SetHook(IHookHelperEx pHook)
        {
            m_hookHelperEx = pHook;
            this.controlSelArea1.SetHook2(pHook);
        }

        /// <summary>
        /// 获取FeatureLayer的集合
        /// </summary>
        /// <returns></returns>
        private IEnumLayer GetFeatureLayers()
        {
            IMap tMap = m_hookHelperEx.ActiveView.FocusMap;
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";//FeatureLayer
            IEnumLayer layers = tMap.get_Layers(uid, true);
            return layers;
        }

        /// <summary>
        /// 获取选择的图层
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <returns></returns>
        public List<ILayer> GetSelectLayer(TreeNodeCollection tNodeColl)
        {
            List<ILayer> tLayers = new List<ILayer>();

            AddSelectLayerToList(tNodeColl, tLayers);

            return tLayers;
        }

        /// <summary>
        /// 把选择的图层添加到一个list
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <param name="tLayers"></param>
        private void AddSelectLayerToList(TreeNodeCollection tNodeColl, List<ILayer> tLayers)
        {
            foreach (TreeNode tNode in tNodeColl)
            {
                if (tNode.Tag is IFeatureLayer && tNode.Checked == true)
                {
                    tLayers.Add(tNode.Tag as IFeatureLayer);
                }

                AddSelectLayerToList(tNode.Nodes, tLayers);
            }
        }
        /// <summary>
        /// 创建矢量图层
        /// </summary>
        /// <param name="pFeatureWorkspace"></param>
        /// <param name="pShpName"></param>
        /// <param name="pInputFc"></param>
        /// <returns></returns>
        private IFeatureClass CreateShpFile(IFeatureWorkspace pFeatureWorkspace, string pShpName, IFeatureClass pInputFc)
        {
            //验证字段，如不符合的字段名称
            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.ValidateWorkspace = pFeatureWorkspace as IWorkspace;
            IFields tFieldsValid = null;
            IEnumFieldError tEnumFieldError = null;
            tFieldChecker.Validate(pInputFc.Fields, out tEnumFieldError, out tFieldsValid);

            //过滤掉Shapefile不支持的字段类型
            IFieldsEdit tFieldsEditNew = new FieldsClass();
            for (int i = 0; i < tFieldsValid.FieldCount; i++)
            {
                IField tFieldValid = tFieldsValid.get_Field(i);
                if (tFieldValid.Type != esriFieldType.esriFieldTypeBlob && tFieldValid.Type != esriFieldType.esriFieldTypeRaster &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeXML)
                {
                    tFieldsEditNew.AddField(tFieldValid);
                }
            }

            return pFeatureWorkspace.CreateFeatureClass(pShpName, tFieldsEditNew, null, null, esriFeatureType.esriFTSimple, "SHAPE", null);

        }
        /// <summary>
        /// 创建ShapeFile文件
        /// </summary>
        /// <param name="pShpName"></param>
        /// <param name="pDirectory"></param>
        /// <returns></returns>
        private IFeatureClass CreateShpFile(string pShpName, string pDirectory, IFeatureClass pInputFc)
        {
            //创建shp文件
            IWorkspaceFactory pShpWF = new ShapefileWorkspaceFactoryClass();

            IWorkspace tWorkspace = pShpWF.OpenFromFile(pDirectory, 0);
            try
            {
               
                IFeatureWorkspace pFeatureWorkspace = tWorkspace as IFeatureWorkspace;

                //验证字段，如不符合的字段名称
                IFieldChecker tFieldChecker = new FieldCheckerClass();
                tFieldChecker.ValidateWorkspace = pFeatureWorkspace as IWorkspace;
                IFields tFieldsValid = null;
                IEnumFieldError tEnumFieldError = null;
                tFieldChecker.Validate(pInputFc.Fields, out tEnumFieldError, out tFieldsValid);

                //过滤掉Shapefile不支持的字段类型
                IFieldsEdit tFieldsEditNew = new FieldsClass();
                for (int i = 0; i < tFieldsValid.FieldCount; i++)
                {
                    IField tFieldValid = tFieldsValid.get_Field(i);
                    if (tFieldValid.Type != esriFieldType.esriFieldTypeBlob && tFieldValid.Type != esriFieldType.esriFieldTypeRaster &&
                        tFieldValid.Type != esriFieldType.esriFieldTypeXML)
                    {
                        tFieldsEditNew.AddField(tFieldValid);
                    }
                }

                string tFullPath = System.IO.Path.Combine(pDirectory, pShpName + ".shp");
                if (System.IO.File.Exists(tFullPath))
                    return null;
                else
                    return pFeatureWorkspace.CreateFeatureClass(pShpName, tFieldsEditNew, null, null, esriFeatureType.esriFTSimple, "SHAPE", null);
            }
            catch (Exception ex)
            {
                return null;

            }
            finally
            {
                //关闭资源锁定  
                IWorkspaceFactoryLockControl ipWsFactoryLock = (IWorkspaceFactoryLockControl)pShpWF;
                if (ipWsFactoryLock.SchemaLockingEnabled)
                {
                    ipWsFactoryLock.DisableSchemaLocking();
                }
                Marshal.ReleaseComObject(tWorkspace);
            }
          
        }
        List<IFeatureClass> DelFeatureClass = new List<IFeatureClass>();
        /// <summary>
        /// 将要素类导入新建的矢量图层中
        /// </summary>
        /// <param name="tOutputFc"></param>
        /// <param name="tFeatureClass"></param>
        /// <param name="tGeometryIntersect"></param>
        private void FeactureClass2ShpFile(IFeatureClass tOutputFc, IFeatureClass tFeatureClass, IGeometry tGeometryIntersect)
        {
            ITopologicalOperator2 tTopo = tGeometryIntersect as ITopologicalOperator2;
            if (tTopo != null)
            {
                tTopo.IsKnownSimple_2 = false;
                tTopo.Simplify();
            }

            //设置空间过滤条件
            ISpatialFilter tSpatialFilter = new SpatialFilterClass();
            tSpatialFilter.Geometry = tGeometryIntersect;
            tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            tSpatialFilter.GeometryField = tFeatureClass.ShapeFieldName;
            //当符合查询结果数目为0，则返回
            if (tFeatureClass.FeatureCount(tSpatialFilter) == 0)
                return;
            //检查是否有Z值
            bool tHasZ = false;
            IsHasZValue(tFeatureClass, out tHasZ);

            //按过滤条件搜索
            IFeatureCursor tFeatureCursor = tFeatureClass.Search(tSpatialFilter, false);

            IFeature tFeature = tFeatureCursor.NextFeature();
            //~~~~~~~~~~~~~~
            IFeatureCursor tOutputCursor = tOutputFc.Insert(true);
            
            IGeometry tGeometry = null;
            while (tFeature != null)
            {
                #region 原来这里是根据选定范围将与范围边界相交的管线进行裁剪
                //做拓扑分析
                //ITopologicalOperator tTopologicalOperator = tGeometryIntersect as ITopologicalOperator;
                //tTopologicalOperator.Simplify();
                //switch (tFeatureClass.ShapeType)
                //{
                //    case esriGeometryType.esriGeometryPolygon:
                //        tGeometry = tTopologicalOperator.Intersect(tFeature.ShapeCopy, esriGeometryDimension.esriGeometry2Dimension);
                //        break;
                //    case esriGeometryType.esriGeometryPolyline:
                //        tGeometry = tTopologicalOperator.Intersect(tFeature.ShapeCopy, esriGeometryDimension.esriGeometry1Dimension);
                //        break;
                //    case esriGeometryType.esriGeometryPoint:
                //        tGeometry = tFeature.ShapeCopy;
                //        break;
                //}
                #endregion

                //与选定区域相交的管线仍保持完整
                tGeometry = tFeature.ShapeCopy;

                if (tGeometry != null && tGeometry.IsEmpty == false)
                {
                    if (tHasZ == true)
                    {
                        GeometryHelper.SetZValue(tGeometry);
                    }

                    //~~~~~~~~~~~~~~
                    IFeatureBuffer tFeatureBuffer = tOutputFc.CreateFeatureBuffer();
                    //~~~~~~~~~~~~~~
                    tFeatureBuffer.Shape = tGeometry;
                    //遍历要导出管网图层的字段里面的数据并将其导入新建的图层中
                    for (int i = 0; i < tFeature.Fields.FieldCount; i++)
                    {
                        int tPos;
                        if (tFeature.Fields.get_Field(i).Name.Length > 10)
                            tPos = tFeatureBuffer.Fields.FindField(tFeature.Fields.get_Field(i).Name.PadLeft(10));
                        else
                            tPos = tFeatureBuffer.Fields.FindField(tFeature.Fields.get_Field(i).Name);
                        //找不到字段跳过
                        if (tPos == -1)
                            continue;
                        IField tField = tFeatureBuffer.Fields.get_Field(tPos);
                        if (tField.Editable && tField.Type != esriFieldType.esriFieldTypeGeometry && tField.Type != esriFieldType.esriFieldTypeOID && tFeature.get_Value(i) != null && tFeature.get_Value(i).ToString() != "")
                        {
                            //~~~~~~~~~~~~~~
                            tFeatureBuffer.set_Value(tPos, tFeature.get_Value(i));
                        }
                    }
                    //~~~~~~~~~~~~~~
                    tOutputCursor.InsertFeature(tFeatureBuffer);
                }
                tFeature = tFeatureCursor.NextFeature();
            }
            tOutputCursor.Flush();
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tOutputCursor);
            GC.Collect();

        }
        /// <summary>
        /// 要素类转为shp文件
        /// </summary>
        /// <param name="pShpName">shp名称</param>
        /// <param name="pDirectory">shp文件存放路径</param>
        /// <param name="pFc">要素类</param>
        /// <param name="tGeometryIntersect"></param>
        private void FeactureClass2ShpFile(string pShpName, string pDirectory, IFeatureClass tFeatureClass, IGeometry tGeometryIntersect)
        {
            ITopologicalOperator2 tTopo = tGeometryIntersect as ITopologicalOperator2;
            if (tTopo != null)
            {
                tTopo.IsKnownSimple_2 = false;
                tTopo.Simplify();
            }

            //设置空间过滤条件
            ISpatialFilter tSpatialFilter = new SpatialFilterClass();
            tSpatialFilter.Geometry = tGeometryIntersect;
            tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            tSpatialFilter.GeometryField = tFeatureClass.ShapeFieldName;        
            //当符合查询结果数目为0，则返回
            if (tFeatureClass.FeatureCount(tSpatialFilter) == 0)
                return;          

            IFeatureClass tOutputFc = CreateShpFile(pShpName, pDirectory, tFeatureClass);
            DelFeatureClass.Add(tOutputFc);
            //图层命名重复
            if (tOutputFc == null)
                return;
            //检查是否有Z值
            bool tHasZ = false;
            IsHasZValue(tFeatureClass, out tHasZ);

            //按过滤条件搜索
            IFeatureCursor tFeatureCursor = tFeatureClass.Search(tSpatialFilter, false);

            IFeature tFeature = tFeatureCursor.NextFeature();
            //~~~~~~~~~~~~~~
            IFeatureCursor tOutputCursor = tOutputFc.Insert(true);
            IGeometry tGeometry = null;
            while (tFeature != null)
            {
                //做拓扑分析
                ITopologicalOperator tTopologicalOperator = tGeometryIntersect as ITopologicalOperator;
                tTopologicalOperator.Simplify();
                switch (tFeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPolygon:
                        tGeometry = tTopologicalOperator.Intersect(tFeature.ShapeCopy, esriGeometryDimension.esriGeometry2Dimension);
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        tGeometry = tTopologicalOperator.Intersect(tFeature.ShapeCopy, esriGeometryDimension.esriGeometry1Dimension);
                        break;
                    case esriGeometryType.esriGeometryPoint:
                        tGeometry = tFeature.ShapeCopy;
                        break;
                }
             
                if (tGeometry != null && tGeometry.IsEmpty == false)
                {
                    if (tHasZ == true)
                    {
                        GeometryHelper.SetZValue(tGeometry);
                    }

                    //~~~~~~~~~~~~~~
                    IFeatureBuffer tFeatureBuffer = tOutputFc.CreateFeatureBuffer();
                    //~~~~~~~~~~~~~~
                    tFeatureBuffer.Shape = tGeometry;
                    for (int i = 0; i < tFeature.Fields.FieldCount; i++)
                    {
                        int tPos;
                        if (tFeature.Fields.get_Field(i).Name.Length > 10)
                            tPos = tFeatureBuffer.Fields.FindField(tFeature.Fields.get_Field(i).Name.PadLeft(10));
                        else
                            tPos = tFeatureBuffer.Fields.FindField(tFeature.Fields.get_Field(i).Name);
                        //找不到字段跳过
                        if (tPos == -1)
                            continue;
                        IField tField = tFeatureBuffer.Fields.get_Field(tPos);
                        if (tField.Editable && tField.Type != esriFieldType.esriFieldTypeGeometry && tField.Type != esriFieldType.esriFieldTypeOID && tFeature.get_Value(i) != null && tFeature.get_Value(i).ToString() != "")
                        {
                            //~~~~~~~~~~~~~~
                            tFeatureBuffer.set_Value(tPos, tFeature.get_Value(i));
                        }
                    }
                    //~~~~~~~~~~~~~~
                    tOutputCursor.InsertFeature(tFeatureBuffer);
                }
                tFeature = tFeatureCursor.NextFeature();
            }
            tOutputCursor.Flush();
            //IWorkspace s = (tOutputFc as IDataset).Workspace;
           // ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(s);
            //ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tOutputCursor);
            //ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tOutputFc);
            //ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursor);
            //var mm= Marshal.ReleaseComObject(tOutputFc);
            GC.Collect();
        }

        /// <summary>
        /// 获取Featureclass是否有Z值
        /// </summary>
        /// <param name="tFeatureClass"></param>
        /// <param name="isHasZ"></param>
        private void IsHasZValue(IFeatureClass tFeatureClass, out bool isHasZ)
        {
            isHasZ = false;

            IFields fields = tFeatureClass.Fields;
            int indexGeometry = fields.FindField(tFeatureClass.ShapeFieldName);
            IField field = fields.get_Field(indexGeometry);
            IGeometryDef geometryDef = field.GeometryDef;

            isHasZ = geometryDef.HasZ;          
        }      

        /// <summary>
        /// 获取图层原始名称
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        private string GetOraginLayerNameByILayer(ILayer pLayer)
        {
            IDataLayer tLayer = pLayer as IDataLayer;
            IDatasetName tDName = (tLayer.DataSourceName) as IDatasetName;
            string tName = tDName.Name;
            string layerName = string.Empty;
            if (tName.Contains("."))
            {
                string[] temps = tName.Split('.');
                layerName = temps[temps.Length - 1];
            }
            else
            {
                layerName = tName;
            }
            //string layerName = tName.Contains(".") ? tName.Split('.')[1] : tName;
            return layerName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ltvLayer.Nodes.Count; i++)
            {
                ltvLayer.Nodes[i].Checked = checkBox1.Checked;
                for (int j = 0; j < ltvLayer.Nodes[i].Nodes.Count; j++)
                {
                    ltvLayer.Nodes[i].Nodes[j].Checked = checkBox1.Checked;
                }
            }
        }

        private void cbEncryption_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cbEncryption_Click(object sender, EventArgs e)
        {
            if(cbEncryption.Checked)
            {
                textBox1.ReadOnly = false;
            }
            else
            {
                textBox1.ReadOnly = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键 
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字 
                {
                    e.Handled = true;
                }
            }
        }


        /// <summary>
        /// 制作压缩包（多个文件压缩到一个压缩包，支持加密、注释）,将原来路径下的文件删除
        /// </summary>
        /// <param name="topDirectoryName">压缩文件目录</param>
        /// <param name="zipedFileName">压缩包文件名</param>
        /// <param name="compresssionLevel">压缩级别 1-9</param>
        /// <param name="password">密码</param>
        /// <param name="comment">注释</param>
        public static void ZipFiles(string topDirectoryName, string zipedFileName, int compresssionLevel, string password, string comment)
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (compresssionLevel != 0)
                {
                    zos.SetLevel(compresssionLevel);//设置压缩级别
                }

                if (!string.IsNullOrEmpty(password))
                {
                    zos.Password = password;//设置zip包加密密码
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    zos.SetComment(comment);//设置zip包的注释
                }

                //循环设置目录下所有的*.jpg文件（支持子目录搜索）
                foreach (string file in Directory.GetFiles(topDirectoryName, "*.*", SearchOption.AllDirectories))
                {
                    if (File.Exists(file))
                    {
                        FileInfo item = new FileInfo(file);
                        FileStream fs = File.OpenRead(item.FullName);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);

                        ZipEntry entry = new ZipEntry(item.Name);
                        zos.PutNextEntry(entry);
                        zos.Write(buffer, 0, buffer.Length);

                        zos.CloseEntry();
                        fs.Close();

                        DeleteDirectory(topDirectoryName, file);
                    }
                }

            }
        }
    }
}