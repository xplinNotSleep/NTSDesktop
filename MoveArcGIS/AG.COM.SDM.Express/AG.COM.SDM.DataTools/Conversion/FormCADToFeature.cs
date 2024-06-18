using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace AG.COM.SDM.DataTools.Conversion
{
    public partial class FormCADToFeature : Form
    {
        #region 属性

        /// <summary>
        /// 通用的出错提示信息头部分
        /// </summary>
        private const string m_ErroMsgHead = "操作失败。原因是：";

        /// <summary>
        /// ArcGIS打开CAD文件CAD图层名称的字段名称
        /// </summary>
        private const string m_CADInArcGISLayerField = "Layer";

        /// <summary>
        /// 进度条
        /// </summary>
        ITrackProgress m_TrackProgress = null;

        /// <summary>
        /// CAD文件在ArcGIS的实际分层数组
        /// "Annotation", "MultiPatch", "Point", "Polygon", "Polyline" 
        /// </summary>
        private readonly string[] m_DivideLayerByGeometryTypes
            = new string[] { "Annotation", "MultiPatch", "Point", "Polygon", "Polyline" };

        /// <summary>
        /// 注记层文本长度
        /// </summary>
        private const int m_AnnoTextFieldLength = 255;

        /// <summary>
        /// 注记字段名称
        /// </summary>
        private const string m_AnnoTextFieldName = "TxtMemo";

        /// <summary>
        /// 用来获取CAD图层信息的featureclass
        /// </summary>
        private const string m_ExampleFeatureclass = "Polyline";

        #endregion

        public FormCADToFeature()
        {
            InitializeComponent();
        }      

        #region 控件事件

        /// <summary>
        /// 添加CAD文件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCADFile_Click(object sender, EventArgs e)
        {
            try
            {              
                OpenFileDialog tOpenFileDialog = new OpenFileDialog();
                tOpenFileDialog.Title = "请添加CAD文件";
                tOpenFileDialog.Filter = "CAD文件|*.dwg";
                tOpenFileDialog.Multiselect = true;
                if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in tOpenFileDialog.FileNames)
                    {
                        ///相同的项不添加
                        if (!lvwCADFileList.Items.Cast<ListViewItem>().Any(r => r.Text == fileName))
                        {
                            this.lvwCADFileList.Items.Add(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(m_ErroMsgHead + ex.Message);
            }
        }

        /// <summary>
        /// 删除CAD文件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCADFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwCADFileList.SelectedItems.Count > 0)
                {
                    this.lvwCADFileList.Items.Remove(this.lvwCADFileList.SelectedItems[0]);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(m_ErroMsgHead + ex.Message);
            }
        }

        /// <summary>
        /// 转换输出CAD文件位置单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog tSaveFileDialog = new SaveFileDialog();
                tSaveFileDialog.Title = "请选择输出GDB文件位置";
                tSaveFileDialog.Filter = "File GDB|*.*";
                if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = System.IO.Path.GetDirectoryName(tSaveFileDialog.FileName);
                    txtOutputName.Text = System.IO.Path.GetFileNameWithoutExtension(tSaveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(m_ErroMsgHead + ex.Message);
            }
        }

        /// <summary>
        /// 确定按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //检查录入信息
            if (CheckInputInfo() == false) return;

            try
            {
                EnableInputControl(false);

                m_TrackProgress = new TrackProgressDialog();
                //完成后不自动关闭
                m_TrackProgress.AutoFinishClose = false;
                m_TrackProgress.DisplayTotal = true;
                m_TrackProgress.TotalValue = 0;
                m_TrackProgress.TotalMessage = "正在建立GDB......";

                m_TrackProgress.SubMessage = "正在建立GDB......";
                m_TrackProgress.SubValue = 0;

                m_TrackProgress.Show();
                Application.DoEvents();

                //建库
                IFeatureWorkspace tFeatureWorkspaceOutput = CreateFileGDB();   

                //从CAD提取GDB中要建立Dataset和Featureclass的信息
                IList<CADToFeatureDatasetInfo> tDatasetInfos = GetDatasetInfoFromCAD(tFeatureWorkspaceOutput);
                IList<CADToFeatureLayerInfo> tLayerInfos = GetLayerInfoFromCAD(tDatasetInfos, tFeatureWorkspaceOutput);                          

                //建立Dataset和Featureclass
                CreateDatasetAndFeatureclass(tFeatureWorkspaceOutput, tDatasetInfos, tLayerInfos);

                HandleStop();

                //写入转化数据
                WriteConversionData(tFeatureWorkspaceOutput, tDatasetInfos, tLayerInfos);

                m_TrackProgress.TotalMessage = "转换完成";
                m_TrackProgress.SubMessage = "转换完成";
                m_TrackProgress.SetFinish();

                EnableInputControl(true);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(m_ErroMsgHead + ex.Message);

                if (m_TrackProgress != null)
                {
                    m_TrackProgress.SetFinish();
                }

                EnableInputControl(true);
            }
        }
        
        /// <summary>
        /// 关闭按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }  

        #endregion

        #region  转换主体

        /// <summary>
        /// 创建FileGDB库
        /// </summary>
        /// <returns></returns>
        private IFeatureWorkspace CreateFileGDB()
        {
            IWorkspaceFactory tWorkspaceFactory = new FileGDBWorkspaceFactoryClass();          
            IName pName = (IName)tWorkspaceFactory.Create(txtOutputPath.Text + "\\", txtOutputName.Text, null, 0);
            return (IFeatureWorkspace)pName.Open();
        }

        /// <summary>
        /// 建立Dataset和Featureclass
        /// </summary>
        /// <param name="tFeatureWorkspaceOutput"></param>
        /// <param name="tDatasetInfos"></param>
        /// <param name="tLayerInfos"></param>
        private void CreateDatasetAndFeatureclass(IFeatureWorkspace tFeatureWorkspaceOutput,
            IList<CADToFeatureDatasetInfo> tDatasetInfos, IList<CADToFeatureLayerInfo> tLayerInfos)
        {
            m_TrackProgress.TotalMax = tDatasetInfos.Count;
            m_TrackProgress.TotalValue = 0;          
            m_TrackProgress.TotalMessage = "";
            m_TrackProgress.SubMessage = "";
            Application.DoEvents();

            //建unknown的空间参考系
            ISpatialReference tSpatialReference = new UnknownCoordinateSystemClass();
            tSpatialReference.SetDomain(-450359962737.05, 450359962737.05, -450359962737.05, 450359962737.05);

            IFeatureDataset tFeatureDataset = null;
            IFieldsEdit tFieldsEdit = null;
            IFeatureClass tFeatureclass = null;

            //要建立的dataset和featureclass的信息已经获取并保存在tDatasetInfos和tLayerInfos中            
            foreach (CADToFeatureDatasetInfo tDatasetInfo in tDatasetInfos)
            {
                m_TrackProgress.TotalMessage = "正在建立第" + (m_TrackProgress.TotalValue + 1) + "个数据集（共" + m_TrackProgress.TotalMax
                        + "个）" + tDatasetInfo.GDBDatasetName;
                Application.DoEvents();

                tFeatureDataset = tFeatureWorkspaceOutput.CreateFeatureDataset(tDatasetInfo.GDBDatasetName, tSpatialReference);

                IList<CADToFeatureLayerInfo> tLayerInfosInDataset =
                    tLayerInfos.Where(r => r.GDBDatasetName == tDatasetInfo.GDBDatasetName).ToList();

                m_TrackProgress.SubValue = 0;
                m_TrackProgress.SubMax = tLayerInfosInDataset.Count;

                foreach (CADToFeatureLayerInfo tLayerInfoInDataset in tLayerInfosInDataset)
                {
                    HandleStop();

                    m_TrackProgress.SubMessage = "正在建立第" + (m_TrackProgress.SubValue + 1) + "个要素类（共" + m_TrackProgress.SubMax
                        + "个）" + tLayerInfoInDataset.GDBFeatureclassName;
                    Application.DoEvents();

                    tFieldsEdit = CreateFields(tSpatialReference, tLayerInfoInDataset);

                    tFeatureclass = tFeatureDataset.CreateFeatureClass(tLayerInfoInDataset.GDBFeatureclassName,
                        tFieldsEdit, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFieldsEdit);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureclass);

                    m_TrackProgress.SubValue++;
                }

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDataset);

                m_TrackProgress.TotalValue++;
            }
        }
     
        /// <summary>
        /// 创建Fields
        /// </summary>
        /// <param name="tSpatialReference"></param>
        /// <param name="tFieldsEdit"></param>
        /// <param name="tLayerInfoInDataset"></param>
        /// <returns></returns>
        private IFieldsEdit CreateFields(ISpatialReference tSpatialReference, CADToFeatureLayerInfo tLayerInfoInDataset)
        {
            IFieldsEdit tFieldsEdit = new FieldsClass();

            IFieldEdit tFieldEdit = new FieldClass();
            tFieldEdit.Name_2 = "OBJECTID";
            tFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            tFieldsEdit.AddField(tFieldEdit);

            tFieldEdit = new FieldClass();
            tFieldEdit.Name_2 = "SHAPE";
            tFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDefEdit tGeometryDefEdit = new GeometryDefClass();
            tGeometryDefEdit.GeometryType_2 = GetGeometryType(tLayerInfoInDataset.GDBGeometryType);
            tGeometryDefEdit.HasZ_2 = tLayerInfoInDataset.GDBFeatureclassHasZ;
            tGeometryDefEdit.SpatialReference_2 = tSpatialReference;
            tFieldEdit.GeometryDef_2 = tGeometryDefEdit;
            tFieldsEdit.AddField(tFieldEdit);

            //注记层还要添加一个字段放置注记的文本内容
            if (tLayerInfoInDataset.GDBGeometryType == "Annotation")
            {
                tFieldEdit = new FieldClass();
                tFieldEdit.Name_2 = m_AnnoTextFieldName;
                tFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                tFieldEdit.Length_2 = m_AnnoTextFieldLength;
                tFieldsEdit.AddField(tFieldEdit);
            }

            return tFieldsEdit;
        }

        /// <summary>
        /// 获取几何字段的GeometryType
        /// </summary>
        /// <param name="tGDBGeometryType"></param>
        /// <returns></returns>
        private static esriGeometryType GetGeometryType(string tGDBGeometryType)
        {
            esriGeometryType tEsriGeometryType;
            switch (tGDBGeometryType)
            {            
                    //标注层转为点层
                case "Annotation":
                    tEsriGeometryType = esriGeometryType.esriGeometryPoint;
                    break;
                case "MultiPatch":
                    tEsriGeometryType = esriGeometryType.esriGeometryMultiPatch;
                    break;
                case "Point":
                    tEsriGeometryType = esriGeometryType.esriGeometryPoint;
                    break;
                case "Polygon":
                    tEsriGeometryType = esriGeometryType.esriGeometryPolygon;
                    break;
                case "Polyline":
                    tEsriGeometryType = esriGeometryType.esriGeometryPolyline;
                    break;
                default:
                    throw new Exception("未知的几何图层类型");
            }
            return tEsriGeometryType;
        }

        /// <summary>
        /// 从CAD提取输出GDB的dataset信息
        /// </summary>
        /// <returns></returns>
        private IList<CADToFeatureDatasetInfo> GetDatasetInfoFromCAD(IFeatureWorkspace tFeatureWorkspace)
        {
            IList<CADToFeatureDatasetInfo> tDatasetInfos = new List<CADToFeatureDatasetInfo>();

            IFieldChecker fieldChecker = new FieldCheckerClass();           
            fieldChecker.ValidateWorkspace = tFeatureWorkspace as IWorkspace;          

            //获取dataset名称（一个CAD文件转换一个dataset）
            foreach (ListViewItem lvItem in lvwCADFileList.Items)
            {
                CADToFeatureDatasetInfo tDatasetInfo = new CADToFeatureDatasetInfo();
                tDatasetInfo.CADFullFileName = lvItem.Text;
                string tDatasetName = System.IO.Path.GetFileNameWithoutExtension(lvItem.Text);

                String validatedName = "";
                fieldChecker.ValidateTableName(tDatasetName, out validatedName);
                tDatasetName = validatedName;

                tDatasetInfo.GDBDatasetName = tDatasetName;
                int i = 0;
                //防止dataset名称重复
                while (tDatasetInfos.Any(r => r.GDBDatasetName == tDatasetInfo.GDBDatasetName))
                {
                    i++;
                    tDatasetInfo.GDBDatasetName = tDatasetName + "_" + i.ToString();
                }

                tDatasetInfos.Add(tDatasetInfo);
            }
            return tDatasetInfos;
        }

        /// <summary>
        /// 从CAD提取输出GDB的图层信息
        /// </summary>
        /// <param name="tDatasetInfos"></param>
        private IList<CADToFeatureLayerInfo> GetLayerInfoFromCAD(IList<CADToFeatureDatasetInfo> tDatasetInfos, IFeatureWorkspace tFeatureWorkspace)
        {
            IList<CADToFeatureLayerInfo> tLayerInfos = new List<CADToFeatureLayerInfo>();

            IWorkspaceFactory tWorkspaceFactoryCAD = new CadWorkspaceFactory();

            IFieldChecker fieldChecker = new FieldCheckerClass();
            fieldChecker.ValidateWorkspace = tFeatureWorkspace as IWorkspace;

            foreach (ListViewItem lvItem in lvwCADFileList.Items)
            {
                m_TrackProgress.SubMessage = "正在打开CAD文件，请稍后......";
                Application.DoEvents();

                IFeatureWorkspace tFeatureWorkspaceCAD =
                    tWorkspaceFactoryCAD.OpenFromFile(System.IO.Path.GetDirectoryName(lvItem.Text), 0) as IFeatureWorkspace;
                //因为要获取ICadDrawingLayers接口从而获得CAD的图层分层，而ICadDrawingLayers接口需要通过其中一个图层获得
                //这里用Polyline层
                IFeatureClass tFeatureClass =
                    tFeatureWorkspaceCAD.OpenFeatureClass(System.IO.Path.GetFileName(lvItem.Text) + ":" + m_ExampleFeatureclass);
                IFeatureLayer tFeatureLayer = new CadFeatureLayerClass();
                tFeatureLayer.FeatureClass = tFeatureClass;
                //关键接口ICadDrawingLayers，此接口能获取CAD文件中的分层信息
                ICadDrawingLayers tCadDrawingLayers = tFeatureLayer as ICadDrawingLayers;

                Dictionary<string, bool> divideLayerHasZ = GetFeatureClassHasZ(lvItem, tFeatureWorkspaceCAD, tFeatureClass);

                string tGDBDatasetName = tDatasetInfos.Where(r => r.CADFullFileName == lvItem.Text).First().GDBDatasetName;
                //分层原则，CAD层*按集合类型分层
                for (int i = 0; i < tCadDrawingLayers.DrawingLayerCount; i++)
                {
                    foreach (string tDivideLayerByGeometryType in m_DivideLayerByGeometryTypes)
                    {
                        CADToFeatureLayerInfo tLayerInfo = new CADToFeatureLayerInfo();
                        tLayerInfo.CADFullFileName = lvItem.Text;
                        tLayerInfo.GDBDatasetName = tGDBDatasetName;
                        tLayerInfo.CADLayerName = tCadDrawingLayers.get_DrawingLayerName(i);
                        tLayerInfo.GDBGeometryType = tDivideLayerByGeometryType;
                        tLayerInfo.GDBFeatureclassHasZ = divideLayerHasZ[tDivideLayerByGeometryType];                                            

                        string tGDBFeatureclassName =
                            System.IO.Path.GetFileNameWithoutExtension(lvItem.Text)
                            + "_" + tLayerInfo.CADLayerName + "_" + tDivideLayerByGeometryType;

                        String validatedName = "";
                        fieldChecker.ValidateTableName(tGDBFeatureclassName, out validatedName);
                        tGDBFeatureclassName = validatedName;

                        tLayerInfo.GDBFeatureclassName = tGDBFeatureclassName;
                        int p = 0;
                        //防止featureclass名称重复
                        while (tLayerInfos.Any(r => r.GDBFeatureclassName == tLayerInfo.GDBFeatureclassName))
                        {
                            p++;
                            tLayerInfo.GDBFeatureclassName = tGDBFeatureclassName + "_" + p.ToString();
                        }

                        tLayerInfos.Add(tLayerInfo);
                    }
                }

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureLayer);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClass);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureWorkspaceCAD);
            }

            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tWorkspaceFactoryCAD);

            return tLayerInfos;
        }

        /// <summary>
        /// 获取FeatureClass是否有Z值
        /// </summary>
        /// <param name="lvItem"></param>
        /// <param name="tFeatureWorkspaceCAD"></param>
        /// <param name="tFeatureClass"></param>
        /// <returns></returns>
        private Dictionary<string, bool> GetFeatureClassHasZ(ListViewItem lvItem, IFeatureWorkspace tFeatureWorkspaceCAD, IFeatureClass tFeatureClass)
        {
            Dictionary<string, bool> divideLayerHasZ = new Dictionary<string, bool>();
            foreach (string tDivideLayerByGeometryType in m_DivideLayerByGeometryTypes)
            {
                bool hasZ = false;
                IFeatureClass tFeatureClassGetHasZ = null;
                //因为其中一个用于获取CAD信息的featureclass已获得，因此要分开处理
                if (tDivideLayerByGeometryType != m_ExampleFeatureclass)
                {
                    tFeatureClassGetHasZ = tFeatureWorkspaceCAD.OpenFeatureClass
                        (System.IO.Path.GetFileName(lvItem.Text) + ":" + tDivideLayerByGeometryType);

                    hasZ = tFeatureClassGetHasZ.Fields.get_Field
                        (tFeatureClassGetHasZ.FindField(tFeatureClassGetHasZ.ShapeFieldName)).GeometryDef.HasZ;

                    divideLayerHasZ.Add(tDivideLayerByGeometryType, hasZ);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassGetHasZ);
                }
                else
                {
                    hasZ = tFeatureClass.Fields.get_Field
                       (tFeatureClass.FindField(tFeatureClass.ShapeFieldName)).GeometryDef.HasZ;

                    divideLayerHasZ.Add(tDivideLayerByGeometryType, hasZ);
                }
            }
            return divideLayerHasZ;
        }

        /// <summary>
        /// 写入转换数据
        /// </summary>
        /// <param name="tFeatureWorkspaceOutput"></param>
        /// <param name="tDatasetInfos"></param>
        /// <param name="tLayerInfos"></param>
        private void WriteConversionData(IFeatureWorkspace tFeatureWorkspaceOutput, IList<CADToFeatureDatasetInfo> tDatasetInfos, IList<CADToFeatureLayerInfo> tLayerInfos)
        {
            m_TrackProgress.TotalMax = tDatasetInfos.Count;
            m_TrackProgress.TotalValue = 0;
            m_TrackProgress.TotalMessage = "正在转换第1个文件（共" + m_TrackProgress.TotalMax.ToString() + "个）";
            m_TrackProgress.SubMessage = "";
            Application.DoEvents();

            IWorkspaceFactory tWorkspaceFactoryCAD = new CadWorkspaceFactory();
            IFeatureWorkspace tFeatureWorkspaceCAD = null;
            IFeatureClass tFeatureClassCAD = null, tFeatureClassOutput = null;
            IFeatureCursor tFeatureCursorCAD = null, tFeatureCursorInsert = null;
            IFeatureBuffer tFeatureBufferOutput = null;
            IFeature tFeatureCAD = null;

            IWorkspaceEdit2 tWorkspaceEdit2Output = tFeatureWorkspaceOutput as IWorkspaceEdit2;
            tWorkspaceEdit2Output.StartEditing(false);
            tWorkspaceEdit2Output.StartEditOperation();

            try
            {
                foreach (CADToFeatureDatasetInfo tDatasetInfo in tDatasetInfos)
                {
                    m_TrackProgress.TotalMessage = "正在转换第" + (m_TrackProgress.TotalValue + 1).ToString()
                        + "个文件（共" + m_TrackProgress.TotalMax.ToString() + "个）";
                    Application.DoEvents();

                    //CAD workspace                 
                    tFeatureWorkspaceCAD = tWorkspaceFactoryCAD.OpenFromFile
                        (System.IO.Path.GetDirectoryName(tDatasetInfo.CADFullFileName), 0) as IFeatureWorkspace;

                    foreach (string tDivideLayerByGeometryType in m_DivideLayerByGeometryTypes)
                    {
                        HandleStop();

                        m_TrackProgress.SubMessage = "正在打开CAD文件，请稍后......";
                        Application.DoEvents();

                        //CAD Featureclass
                        tFeatureClassCAD = tFeatureWorkspaceCAD.OpenFeatureClass
                            (System.IO.Path.GetFileName(tDatasetInfo.CADFullFileName) + ":" + tDivideLayerByGeometryType);

                        IList<CADToFeatureLayerInfo> tLayerInfosInDataset =
                            tLayerInfos.Where(r => r.GDBDatasetName == tDatasetInfo.GDBDatasetName
                                && r.GDBGeometryType == tDivideLayerByGeometryType).ToList();

                        foreach (CADToFeatureLayerInfo tLayerInfoInDataset in tLayerInfosInDataset)
                        {
                            //GDB Featurrclass
                            tFeatureClassOutput = tFeatureWorkspaceOutput.OpenFeatureClass(tLayerInfoInDataset.GDBFeatureclassName);

                            int textFieldIndexCAD = 0, textFieldIndexOutput = 0;
                            //获取CAD层和GDB层得注记字段的index
                            if (tDivideLayerByGeometryType == "Annotation")
                            {
                                textFieldIndexCAD = tFeatureClassCAD.FindField(m_AnnoTextFieldName);
                                textFieldIndexOutput = tFeatureClassOutput.FindField(m_AnnoTextFieldName);
                            }
                            
                            IQueryFilter tQueryFilter = new QueryFilterClass();
                            tQueryFilter.WhereClause = m_CADInArcGISLayerField + " = '" + tLayerInfoInDataset.CADLayerName + "'";
                            tFeatureCursorCAD = tFeatureClassCAD.Search(tQueryFilter, false);

                            tFeatureCursorInsert = tFeatureClassOutput.Insert(true);

                            m_TrackProgress.SubValue = 0;
                            m_TrackProgress.SubMax = tFeatureClassCAD.FeatureCount(tQueryFilter);

                            string strSubMsgHead = "正在转换" + tLayerInfoInDataset.CADLayerName
                                + "层（" + tDivideLayerByGeometryType + "）第";

                            tFeatureCAD = tFeatureCursorCAD.NextFeature();
                            while (tFeatureCAD != null)
                            {
                                m_TrackProgress.SubValue++;
                                m_TrackProgress.SubMessage = strSubMsgHead + m_TrackProgress.SubValue.ToString()
                                    + "条记录（共" + m_TrackProgress.SubMax + "条）";
                                Application.DoEvents();

                                tFeatureBufferOutput = tFeatureClassOutput.CreateFeatureBuffer();
                                WriteFeature(tFeatureBufferOutput, tFeatureCAD, tDivideLayerByGeometryType,
                                    textFieldIndexCAD, textFieldIndexOutput);
                                tFeatureCursorInsert.InsertFeature(tFeatureBufferOutput);

                                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureBufferOutput);
                                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCAD);

                                HandleStop();

                                tFeatureCAD = tFeatureCursorCAD.NextFeature();
                            }
                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassOutput);
                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorCAD);
                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorInsert);
                        }
                        ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassCAD);
                    }

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureWorkspaceCAD);

                    m_TrackProgress.TotalValue++;
                }

                m_TrackProgress.SubMessage = "正在保存......";
                Application.DoEvents();
                tWorkspaceEdit2Output.StopEditOperation();
                tWorkspaceEdit2Output.StopEditing(true);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);

                tWorkspaceEdit2Output.StopEditOperation();
                tWorkspaceEdit2Output.StopEditing(false);

                throw ex;
            }
            finally
            {
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureBufferOutput);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCAD);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassOutput);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorCAD);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorInsert);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassCAD);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureWorkspaceCAD);
            }
        }

        /// <summary>
        /// 写入feature的值
        /// </summary>
        /// <param name="tFeatureBufferOutput"></param>
        /// <param name="tFeatureCAD"></param>
        /// <param name="tDivideLayerByGeometryType"></param>
        /// <param name="textFieldIndexCAD"></param>
        /// <param name="textFieldIndexOutput"></param>
        private void WriteFeature(IFeatureBuffer tFeatureBufferOutput, IFeature tFeatureCAD, 
            string tDivideLayerByGeometryType, int textFieldIndexCAD, int textFieldIndexOutput)
        {
            tFeatureBufferOutput.Shape = tFeatureCAD.ShapeCopy;
            //注记层还要写入文本内容
            if (tDivideLayerByGeometryType == "Annotation")
            {
                string textMemo = tFeatureCAD.get_Value(textFieldIndexCAD) as string;
                //截掉超长字符串
                if (textMemo.Length > m_AnnoTextFieldLength)
                {
                    textMemo = textMemo.Substring(0, m_AnnoTextFieldLength);
                }
                tFeatureBufferOutput.set_Value(textFieldIndexOutput, textMemo);
            }
        }

        #endregion

        #region 其他

        /// <summary>
        /// 检查输入信息是否正确和完整
        /// </summary>
        /// <returns></returns>
        private bool CheckInputInfo()
        {
            if (lvwCADFileList.Items.Count == 0)
            {
                MessageBox.Show("请添加要转换的CA文件");
                return false;
            }

            if (string.IsNullOrEmpty(txtOutputPath.Text))
            {
                MessageBox.Show("请选择输出的GDB文件位置");
                return false;
            }

            if (string.IsNullOrEmpty(txtOutputName.Text))
            {
                MessageBox.Show("请输入输出的GDB文件名称");
                return false;
            }

            if (Directory.Exists(System.IO.Path.Combine(txtOutputPath.Text, txtOutputName.Text) + ".gdb"))
            {
                MessageBox.Show("输出位置已存在同名的GDB，请输入其他名称");
                return false;
            }

            if (lvwCADFileList.Items.Cast<ListViewItem>()
                .Any(r => char.IsNumber(System.IO.Path.GetFileNameWithoutExtension(r.Text).ToCharArray()[0])))
            {
                MessageBox.Show("CAD文件名首字不能是数字");
                return false;
            }

            if (lvwCADFileList.Items.Cast<ListViewItem>()
               .Any(r => System.IO.Path.GetFileNameWithoutExtension(r.Text).IndexOf(" ") > -1))
            {
                MessageBox.Show("CAD文件名不能有空格");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 进度条停止操作判断并处理
        /// </summary>
        private void HandleStop()
        {
            if (m_TrackProgress.IsContinue == false)
            {
                throw new Exception("停止操作");
            }
        }

        /// <summary>
        /// 设置输入类型控件的Enabled值
        /// </summary>
        /// <param name="tEnabled"></param>
        private void EnableInputControl(bool tEnabled)
        {         
            btnAddCADFile.Enabled = tEnabled;
            btnDeleteCADFile.Enabled = tEnabled;
            btnOutputPath.Enabled = tEnabled;
            txtOutputName.Enabled = tEnabled;
            btnOK.Enabled = tEnabled;
            btnCancle.Enabled = tEnabled;
        }

        #endregion       
    }
}
