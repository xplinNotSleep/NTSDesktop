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
    /// ʸ�����ݲü�������
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
        /// ��ȡ�û�����ѡ��ؼ�
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
                tbtnPath.SetToolTip(btnPath, "����ʸ���������Ŀ¼");

                ///��ʼ��ͼ����
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
                MessageHandler.ShowInfoMsg("����ѡ��ü���Χ","��ʾ");
                return;
            }

            if (txtPath.Text.Length == 0)
            {
                MessageHandler.ShowInfoMsg("����ѡ�񵼳�·��", "��ʾ");
                return;
            }

            //��ȡѡ�񵼳���ͼ��
            List<ILayer> tLayers = GetSelectLayer(ltvLayer.Nodes);
            if (tLayers.Count < 1)
            {
                MessageHandler.ShowInfoMsg("����ѡ��ͼ��", "��ʾ");
                return;
            }
            IsComplated = false;
            TrackProgressDialog tProgress = new TrackProgressDialog();
            tProgress.SubMessage = "��������Ŀ¼����";
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
                    //������� ���ȴ�ŵ���ʱ�ļ��Ӽ�
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
                    tProgress.SubMessage = string.Format("���ڵ�������{0}({1}/{2})", tLayer.Name,i+1,tLayerCount);
                    Application.DoEvents();
                    string LayerName = GetOraginLayerNameByILayer(tLayer);
                    //����shp�ļ�
                    IWorkspaceFactory pShpWF = new ShapefileWorkspaceFactoryClass();

                    IWorkspace tWorkspace = pShpWF.OpenFromFile(tempDirectory, 0);
                    IFeatureClass featureClass = CreateShpFile(tWorkspace as IFeatureWorkspace, LayerName, tFeatureLayer.FeatureClass);

                    //Ҫ���ർ���½���ʸ��ͼ��
                    FeactureClass2ShpFile(featureClass, tFeatureLayer.FeatureClass, this.controlSelArea1.RegionGeometry);
                    //�ر���Դ����  
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

                    //ZipHelper.ZipFiles(tempDirectory, strZipFile, 1, textBox1.Text, "�¸�Ƽ�");
                    ZipFiles(tempDirectory, strZipFile, 1, textBox1.Text, "�¸�Ƽ�");

                    Directory.Delete(txtPath.Text, true);
                    Directory.Delete(tempDirectory, true);
                }


                tProgress.SubValue = i;
                tProgress.SubMessage = string.Format("�������");
                MessageHandler.ShowInfoMsg("�����ɹ�", Text);
                IsComplated = true;
                Close();
            }
            catch(Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
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
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
           
        }
        /// <summary>
        /// ɾ���ļ����Լ��ļ�
        /// </summary>
        /// <param name="directoryPath"> �ļ���·�� </param>
        /// <param name="fileName"> �ļ����� </param>
        public static void DeleteDirectory(string directoryPath, string fileName)
        {

            //ɾ���ļ�
            for (int i = 0; i < Directory.GetFiles(directoryPath).Length; i++)
            {
                if (Directory.GetFiles(directoryPath)[i] == fileName)
                {
                    File.Delete(fileName);
                }
            }

            //ɾ���ļ���
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
        /// ����Hook����
        /// </summary>
        /// <param name="pHook"></param>
        public void SetHook(IHookHelperEx pHook)
        {
            m_hookHelperEx = pHook;
            this.controlSelArea1.SetHook2(pHook);
        }

        /// <summary>
        /// ��ȡFeatureLayer�ļ���
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
        /// ��ȡѡ���ͼ��
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
        /// ��ѡ���ͼ����ӵ�һ��list
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
        /// ����ʸ��ͼ��
        /// </summary>
        /// <param name="pFeatureWorkspace"></param>
        /// <param name="pShpName"></param>
        /// <param name="pInputFc"></param>
        /// <returns></returns>
        private IFeatureClass CreateShpFile(IFeatureWorkspace pFeatureWorkspace, string pShpName, IFeatureClass pInputFc)
        {
            //��֤�ֶΣ��粻���ϵ��ֶ�����
            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.ValidateWorkspace = pFeatureWorkspace as IWorkspace;
            IFields tFieldsValid = null;
            IEnumFieldError tEnumFieldError = null;
            tFieldChecker.Validate(pInputFc.Fields, out tEnumFieldError, out tFieldsValid);

            //���˵�Shapefile��֧�ֵ��ֶ�����
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
        /// ����ShapeFile�ļ�
        /// </summary>
        /// <param name="pShpName"></param>
        /// <param name="pDirectory"></param>
        /// <returns></returns>
        private IFeatureClass CreateShpFile(string pShpName, string pDirectory, IFeatureClass pInputFc)
        {
            //����shp�ļ�
            IWorkspaceFactory pShpWF = new ShapefileWorkspaceFactoryClass();

            IWorkspace tWorkspace = pShpWF.OpenFromFile(pDirectory, 0);
            try
            {
               
                IFeatureWorkspace pFeatureWorkspace = tWorkspace as IFeatureWorkspace;

                //��֤�ֶΣ��粻���ϵ��ֶ�����
                IFieldChecker tFieldChecker = new FieldCheckerClass();
                tFieldChecker.ValidateWorkspace = pFeatureWorkspace as IWorkspace;
                IFields tFieldsValid = null;
                IEnumFieldError tEnumFieldError = null;
                tFieldChecker.Validate(pInputFc.Fields, out tEnumFieldError, out tFieldsValid);

                //���˵�Shapefile��֧�ֵ��ֶ�����
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
                //�ر���Դ����  
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
        /// ��Ҫ���ർ���½���ʸ��ͼ����
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

            //���ÿռ��������
            ISpatialFilter tSpatialFilter = new SpatialFilterClass();
            tSpatialFilter.Geometry = tGeometryIntersect;
            tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            tSpatialFilter.GeometryField = tFeatureClass.ShapeFieldName;
            //�����ϲ�ѯ�����ĿΪ0���򷵻�
            if (tFeatureClass.FeatureCount(tSpatialFilter) == 0)
                return;
            //����Ƿ���Zֵ
            bool tHasZ = false;
            IsHasZValue(tFeatureClass, out tHasZ);

            //��������������
            IFeatureCursor tFeatureCursor = tFeatureClass.Search(tSpatialFilter, false);

            IFeature tFeature = tFeatureCursor.NextFeature();
            //~~~~~~~~~~~~~~
            IFeatureCursor tOutputCursor = tOutputFc.Insert(true);
            
            IGeometry tGeometry = null;
            while (tFeature != null)
            {
                #region ԭ�������Ǹ���ѡ����Χ���뷶Χ�߽��ཻ�Ĺ��߽��вü�
                //�����˷���
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

                //��ѡ�������ཻ�Ĺ����Ա�������
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
                    //����Ҫ��������ͼ����ֶ���������ݲ����䵼���½���ͼ����
                    for (int i = 0; i < tFeature.Fields.FieldCount; i++)
                    {
                        int tPos;
                        if (tFeature.Fields.get_Field(i).Name.Length > 10)
                            tPos = tFeatureBuffer.Fields.FindField(tFeature.Fields.get_Field(i).Name.PadLeft(10));
                        else
                            tPos = tFeatureBuffer.Fields.FindField(tFeature.Fields.get_Field(i).Name);
                        //�Ҳ����ֶ�����
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
        /// Ҫ����תΪshp�ļ�
        /// </summary>
        /// <param name="pShpName">shp����</param>
        /// <param name="pDirectory">shp�ļ����·��</param>
        /// <param name="pFc">Ҫ����</param>
        /// <param name="tGeometryIntersect"></param>
        private void FeactureClass2ShpFile(string pShpName, string pDirectory, IFeatureClass tFeatureClass, IGeometry tGeometryIntersect)
        {
            ITopologicalOperator2 tTopo = tGeometryIntersect as ITopologicalOperator2;
            if (tTopo != null)
            {
                tTopo.IsKnownSimple_2 = false;
                tTopo.Simplify();
            }

            //���ÿռ��������
            ISpatialFilter tSpatialFilter = new SpatialFilterClass();
            tSpatialFilter.Geometry = tGeometryIntersect;
            tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            tSpatialFilter.GeometryField = tFeatureClass.ShapeFieldName;        
            //�����ϲ�ѯ�����ĿΪ0���򷵻�
            if (tFeatureClass.FeatureCount(tSpatialFilter) == 0)
                return;          

            IFeatureClass tOutputFc = CreateShpFile(pShpName, pDirectory, tFeatureClass);
            DelFeatureClass.Add(tOutputFc);
            //ͼ�������ظ�
            if (tOutputFc == null)
                return;
            //����Ƿ���Zֵ
            bool tHasZ = false;
            IsHasZValue(tFeatureClass, out tHasZ);

            //��������������
            IFeatureCursor tFeatureCursor = tFeatureClass.Search(tSpatialFilter, false);

            IFeature tFeature = tFeatureCursor.NextFeature();
            //~~~~~~~~~~~~~~
            IFeatureCursor tOutputCursor = tOutputFc.Insert(true);
            IGeometry tGeometry = null;
            while (tFeature != null)
            {
                //�����˷���
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
                        //�Ҳ����ֶ�����
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
        /// ��ȡFeatureclass�Ƿ���Zֵ
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
        /// ��ȡͼ��ԭʼ����
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
            if (e.KeyChar != '\b')//�������������˸�� 
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//������������0-9���� 
                {
                    e.Handled = true;
                }
            }
        }


        /// <summary>
        /// ����ѹ����������ļ�ѹ����һ��ѹ������֧�ּ��ܡ�ע�ͣ�,��ԭ��·���µ��ļ�ɾ��
        /// </summary>
        /// <param name="topDirectoryName">ѹ���ļ�Ŀ¼</param>
        /// <param name="zipedFileName">ѹ�����ļ���</param>
        /// <param name="compresssionLevel">ѹ������ 1-9</param>
        /// <param name="password">����</param>
        /// <param name="comment">ע��</param>
        public static void ZipFiles(string topDirectoryName, string zipedFileName, int compresssionLevel, string password, string comment)
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (compresssionLevel != 0)
                {
                    zos.SetLevel(compresssionLevel);//����ѹ������
                }

                if (!string.IsNullOrEmpty(password))
                {
                    zos.Password = password;//����zip����������
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    zos.SetComment(comment);//����zip����ע��
                }

                //ѭ������Ŀ¼�����е�*.jpg�ļ���֧����Ŀ¼������
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