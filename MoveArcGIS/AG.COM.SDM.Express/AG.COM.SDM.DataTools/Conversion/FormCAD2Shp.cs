using System;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.Conversion
{
    public partial class FormCAD2Shp : Form
    {
        long m_iGuage;
        Boolean m_bIsEndTrans;

        public FormCAD2Shp()
        {
            InitializeComponent();
        }

        //分析文件路径名
        private void DisassemblePathName(String sPathName, String sDir, String sFilename)
        {
            string[] pM;
            int i;
            sDir="";
            sFilename="";
            pM=sPathName.Split('\\');
            for(i=0;i<pM.Length;i++)
            {
                if (sDir=="")
                    sDir=pM[i];
                else
                    sDir=sDir + '\\' + pM[i];
            }
            sFilename=pM[pM.Length];
        }

        //创建CAD的要素实体工作空间
        private IFeatureWorkspace CreateCADWorkSpace(string sDir)
        {
            IWorkspaceFactory pCADWorkSpace;
            IPropertySet pPropertySet;
            IFeatureWorkspace pFeatureWorkspace;
            pCADWorkSpace =new CadWorkspaceFactoryClass();
            pPropertySet =new PropertySetClass();
            pPropertySet.SetProperty("DATABASE", sDir);
            pFeatureWorkspace = pCADWorkSpace.Open(pPropertySet, 0) as IFeatureWorkspace;
            return pFeatureWorkspace;
        }

        //获取并设置进度条最大值
        private void GetMaxProBar(ListBox pListBox)
        {
            try
            {
                int i;
                int j;
                string PathName;
                IFeatureWorkspace pFeatureWorkspace;
                IFeatureClassContainer pFeatureClassCollection;
                IFeatureClass pFeatureClass;
                String pDir;
                String pFileName;
                long pRecordsNum;
                pRecordsNum = 0;
                for (i = 0; i < pListBox.Items.Count; i++)
                {
                    PathName = pListBox.Items[i].ToString();
                    string[] pM;
                    pDir = "";
                    pFileName = "";
                    pM = PathName.Split(new char[] { '\\' });
                    for (j = 0; j < pM.Length - 1; j++)
                    {
                        if (pDir == "")
                            pDir = pM[j];
                        else
                            pDir = pDir + "\\" + pM[j];
                    }
                    pFileName = pM[pM.Length - 1];
                    pFeatureWorkspace = CreateCADWorkSpace(pDir);
                    pFeatureClassCollection = pFeatureWorkspace.OpenFeatureDataset(pFileName) as IFeatureClassContainer;
                    for (j = 0; j < pFeatureClassCollection.ClassCount; j++)
                    {
                        pFeatureClass = pFeatureClassCollection.get_Class(j);
                        pRecordsNum = pRecordsNum + pFeatureClass.FeatureCount(null);
                    }
                }
                this.pbrStat.Maximum = System.Convert.ToInt32(pRecordsNum);
            }
            catch
            {

            }
        }

        //数据转化主函数
        private void TransformDataFile(string PathName, string DataType)
        {
            try
            {
                IFeatureWorkspace pFeatureWorkspace;
                IFeatureClassContainer pFeatureClassCollection;
                IFeatureClass pFeatureClass;
                String pDir;
                String pFileName;
                int i;
                string[] pM;
                pDir="";
                pFileName="";
                pM=PathName.Split( '\\');
                for(i=0;i<pM.Length-1;i++)
                {
                    if (pDir=="")
                        pDir=pM[i];
                    else
                        pDir=pDir + "\\" + pM[i];
                }
                pFileName=pM[pM.Length-1];
                pFeatureWorkspace = CreateCADWorkSpace(pDir);
                if (DataType == "")
                    DataType = "CAD";
                if (DataType == "CAD")
                {
                    pFeatureClassCollection = pFeatureWorkspace.OpenFeatureDataset(pFileName) as IFeatureClassContainer;

                    WriteInfo("读取CAD文件" + pFileName + "成功！", 0);
                    pFileName = this.PickUpFileName(pFileName);
                    for (i = 0; i < pFeatureClassCollection.ClassCount; i++)
                    {
                        pFeatureClass = pFeatureClassCollection.get_Class(i);
                        if (this.CreateShapFileText(pDir, pFileName, pFeatureClass, i))
                            this.WriteInfo(pFileName + "文件:" + "转换" + pFeatureClass.AliasName + "图层成功！", 0);
                        else
                            this.WriteInfo(pFileName + "文件:" + "转换" + pFeatureClass.AliasName + "图层出错！请验查数据。", 0);
                    }
                    this.WriteInfo("转换CAD文件" + pFileName + "成功", 0);
                }
                if (this.m_bIsEndTrans == true)
                {
                    this.m_iGuage = 0;
                    this.WriteInfo("转换CAD文件完成", 0);
                    this.listBox1.ClearSelected();
                    this.listBox2.ClearSelected();
                    MessageBox.Show("转换CAD文件完成！", "提示!");
                    this.pbrStat.Value = 0;
                    this.m_bIsEndTrans = false;
                }
            }
            catch
            {
                MessageBox.Show("转换CAD文件出错！", "提示!");
                this.pbrStat.Value = 0;
                this.lstTransformInfo.Items.Clear();
                this.WriteInfo("转换CAD文件出错！",0);
            }
        }

        //创建ShapeFile文件
        private Boolean CreateShapFileText(String sDir, String sFilename, IFeatureClass pFeatureClass, int LayerID)
        {
            try
            {
                String sPathName;
                IFeatureWorkspace pFeatureWorkspace;
                IFeatureClass pNewFeatureClass;      
                long pObjID;
                String sType;
                sPathName = "";
                pObjID = pFeatureClass.ObjectClassID;
                switch (pFeatureClass.ShapeType) 
                {
                    case esriGeometryType.esriGeometryPoint:                     //'点类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Point.shp";
                        sType = "点";
                        break;
                    case esriGeometryType.esriGeometryMultipoint:                //多点类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Multipoint.shp";
                        sType = "多点";
                        break;
                    case esriGeometryType.esriGeometryLine:                      //线段类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Line.shp";
                        sType = "线段";
                        break;
                    case esriGeometryType.esriGeometryCircularArc:               //圆弧段类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_CirCularArc.shp";
                        sType = "圆弧段";
                        break;
                    case esriGeometryType.esriGeometryEllipticArc:               //椭圆弧段类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_EllipticArc.shp";
                        sType = "椭圆弧段";
                        break;
                    case esriGeometryType.esriGeometryPolyline:                  //线类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Polyline.shp";
                        sType = "线";
                        break;
                    case esriGeometryType.esriGeometryRing:                      //环类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Ring.shp";
                        sType = "环";
                        break;
                    case esriGeometryType.esriGeometryPolygon:                   //面类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Polygon.shp";
                        sType = "面";
                        break;
                    case esriGeometryType.esriGeometryEnvelope:                  //矩形类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Envelope.shp";
                        sType = "矩形";
                        break;
                    case esriGeometryType.esriGeometryAny :                       //任意类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Envelope.shp";
                        sType = "任意";
                        break;
                    case esriGeometryType.esriGeometryMultiPatch:                //多路径类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_MultiPatch.shp";
                        sType = "多路径";
                        break;
                    case esriGeometryType.esriGeometrySphere:                    //球类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Sphere.shp";
                        sType = "球";
                        break;
                    case esriGeometryType.esriGeometryTriangles:                 //三角形类型的实体
                        sPathName = sFilename + System.Convert.ToString(LayerID) + "_Triangles.shp";
                        sType = "三角形";
                        break;
                    default:
                        sType = "";
                        break;
                }
               
                UID pCLSID = new UIDClass();
                UID pEXTCLSID = new UIDClass();
                pCLSID.Value = '{' + Guid.NewGuid().ToString() + '}';
                pEXTCLSID.Value = '{' + Guid.NewGuid().ToString() + '}';
                if (this.txtLayOut.Text.Trim() != "")
                    sDir = this.txtLayOut.Text.Trim();
                pFeatureWorkspace = CreateShapeFileWorkSpace(sDir);
                if (System.IO.File.Exists(sDir + '\\' + sPathName)) 
                {
                    System.IO.File.Delete(sDir + '\\' + sPathName);
                    if (System.IO.File.Exists(sDir + '\\' + PickUpFileName(sPathName) + ".dbf"))
                        System.IO.File.Delete(sDir + '\\' + PickUpFileName(sPathName) + ".dbf");
                    if (System.IO.File.Exists(sDir + '\\' + PickUpFileName(sPathName) + ".sbn"))
                        System.IO.File.Delete(sDir + '\\' + PickUpFileName(sPathName) + ".sbn");
                    if (System.IO.File.Exists(sDir + '\\' + PickUpFileName(sPathName) + ".sbx"))
                        System.IO.File.Delete(sDir + '\\' + PickUpFileName(sPathName) + ".sbx");
                    if (System.IO.File.Exists(sDir + '\\' + PickUpFileName(sPathName) + ".shx"))
                        System.IO.File.Delete(sDir + '\\' + PickUpFileName(sPathName) + ".shx");
                }
                pNewFeatureClass = pFeatureWorkspace.CreateFeatureClass(sPathName, pFeatureClass.Fields, pCLSID, pEXTCLSID, pFeatureClass.FeatureType, "Shape", "");
                if (AddFeatureToClass(pFeatureClass, pNewFeatureClass, sType, sFilename))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        //把旧的实体类数据写入新的实体类数据
        private Boolean AddFeatureToClass(IFeatureClass pOldFeatureClass, IFeatureClass pNewFeatureClass, String sType, string sFilename)
        {
            try
            {
                IDataset pDataset;
                IWorkspace pWorkspace;
                IWorkspaceEdit pWorkspaceEdit;
                IFeatureCursor pFeatureCursor;
                IFeature pFeature;
                IFeature pOldFeature;
                IFields pFields;
                long i;
                long J;
                long lnNum;
                lnNum = 0;
                J = 0;
                pFeatureCursor = pOldFeatureClass.Search(null, false);
                pDataset = pNewFeatureClass as IDataset;
                pWorkspace=pDataset.Workspace;
                pWorkspaceEdit = pWorkspace as IWorkspaceEdit;
                //开始编辑
                pWorkspaceEdit.StartEditing(true);
                pWorkspaceEdit.StartEditOperation();
                this.WriteInfo(sFilename + "文件:" + "正在转换" + sType + "类型图层...", 0);
                pOldFeature = pFeatureCursor.NextFeature();
                while (pOldFeature != null)
                {
                    pFeature = pNewFeatureClass.CreateFeature();
                    pFeature.Shape = pOldFeature.Shape;
                    pFields = pFeature.Fields;
                    for (i = 0; i < pFields.FieldCount; i++)
                    {
                        if (pFeature.Fields.get_Field(System.Convert.ToInt32(i)).Editable)
                            pFeature.set_Value(System.Convert.ToInt32(i),pOldFeature.get_Value(System.Convert.ToInt32(i)));
                    }
                    pOldFeature = pFeatureCursor.NextFeature();
                    lnNum = lnNum + 1;
                    J = J + 1;
                    if (J % 1000 == 1)
                        this.WriteInfo(sFilename + "文件:" + "转换" + sType + "类型数据" + lnNum + "条记录", 2);
                    this.m_iGuage = this.m_iGuage + 1;
                    this.pbrStat.Value = System.Convert.ToInt32(this.m_iGuage);
                    this.pbrStat.Refresh();
                    pFeature.Store();
                }
                this.WriteInfo(sFilename + "文件:" + "转换" + sType + "类型数据" + lnNum + "条记录", 2);
                this.WriteInfo(sFilename + "文件:" + "正在进行保存" + sType + "类型图层...", 2);
                //停止编辑
                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
                this.WriteInfo(sFilename + "文件:" + "保存" + sType + "类型图层成功", 3);
                pWorkspaceEdit = null;
                pWorkspace = null;
                pDataset = null;
                pFeatureCursor = null;
                pFeature = null;
                pOldFeature = null;
                pFields = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //创建ShapeFile的要素实体工作空间
        private IFeatureWorkspace CreateShapeFileWorkSpace(String sDir)
        {
            IWorkspaceFactory pShapWorkSpace;
            IPropertySet pPropertySet;
            pShapWorkSpace =new ShapefileWorkspaceFactoryClass(); 
            pPropertySet =new PropertySetClass();
            pPropertySet.SetProperty("DATABASE", sDir);
            return pShapWorkSpace.Open(pPropertySet, 0) as IFeatureWorkspace;
        }

        //提取文件名
        private string PickUpFileName(String sFilename)
        {
            string[] pM;
            pM=sFilename.Split('.');
            if (pM.Length == 0)
                return sFilename;
            else
                return pM[0];
        }

        //向用户提示转化的操作信息
        private void WriteInfo(String Info, int InfoType)
        {
            if (InfoType == 0)
                InfoType = 1;
            if (InfoType == 1)
            {
                WriteToList(Info);
                WriteToState(Info);
            }
            else if (InfoType == 2)
                WriteToList(Info);
            else if (InfoType == 3)
                WriteToState(Info);
        }

        //写入状态栏的信息
        private void WriteToState(String Info)
        {
            this.labState.Text = Info;
            this.labState.Refresh();
        }

        //写入列表框的信息
        private void WriteToList(String Info)
        {
            this.lstTransformInfo.Items.Add(Info);
            this.lstTransformInfo.Refresh();
            this.lstTransformInfo.TopIndex = this.lstTransformInfo.Items.Count - 1;
        }

        //转换文件
        private void cmdTransform_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                int intLength;
                intLength = 0;
                if (this.txtFile.Text == "")
                {
                    MessageBox.Show("请打开CAD文件(.DWG .DXF)！", "提示!");
                    return;
                }
                this.lstTransformInfo.Items.Clear();
                intLength = this.listBox2.Items.Count;
                GetMaxProBar(this.listBox2);
                this.m_iGuage = 0;
                for (i = 0; i < intLength; i++)
                {
                    if (i == (intLength - 1))
                    {
                        this.m_bIsEndTrans = true;
                    }
                    this.listBox1.SelectedIndex = i;
                    this.listBox2.SelectedIndex = i;
                    this.txtFile.Text = this.listBox2.Items[i].ToString();          //listbox2用于存储完整路径
                    this.TransformDataFile(this.txtFile.Text, "");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "提示!");
                this.txtFile.Text = "";
                this.lstTransformInfo.Items.Clear();
            }
        }

        //打开文件
        private void cmdOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                int j;
                string[] strFileNames;
                OpenFileDialog dlgOpenFile=new OpenFileDialog();
                dlgOpenFile.Title = "打开CAD文件";
                dlgOpenFile.DefaultExt = "dwg";
                dlgOpenFile.Filter = "CAD文件(DWG)|*.dwg|CAD文件(DXF)|*.dxf";
                dlgOpenFile.FileName = "";
                dlgOpenFile.Multiselect = true;
                dlgOpenFile.ShowDialog();
                this.m_bIsEndTrans = false;
                strFileNames = dlgOpenFile.FileNames;
                if (strFileNames[0].Trim() != "")
                {
                    for (i = 0; i < strFileNames.Length; i++)
                    {
                        for (j = 0; j < listBox2.Items.Count; j++)      //排除已经添加的项
                        {
                            if (strFileNames[i] == listBox2.Items[j].ToString().Trim()) 
                                break;
                        }
                        if (j >= listBox2.Items.Count)
                        {
                            this.listBox1.Items.Add(this.GetFileNameFromPath(strFileNames[i]));
                            this.listBox2.Items.Add(strFileNames[i]);
                        }
                    }
                }
                else
                    return;
                this.txtFile.Text = strFileNames[0];
            }
            catch
            {
                this.txtFile.Text = "";
            }
        }

        /// <summary>
        /// 根据文件完全路径获取文件名称
        /// </summary>
        /// <param name="strPath"></param>文件完全路径
        /// <returns></returns>文件名称
        private string GetFileNameFromPath(string strPath)
        {
            int intLastIndex;
            string strFileName;
            string[] PM;
            intLastIndex = strPath.LastIndexOf('\\');
            strFileName = strPath.Substring(intLastIndex + 1);
            PM=strFileName.Split('.');
            strFileName=PM[0];
            return strFileName;
        }

        private void cmdSpread_Click(object sender, EventArgs e)
        {
            if (this.cmdSpread.Text=="展开")
            {
                this.cmdSpread.Text = "收缩";
                this.groupBox2.Visible=true;
                this.Height =523;
            }
            else
            {
                this.cmdSpread.Text="展开";
                this.groupBox2.Visible=false;
                this.Height=327;
            }
        }

        private void frmCADTransform_Load(object sender, EventArgs e)
        {
            this.Height=327;
            this.Width=432;
            this.cmdSpread.Text="展开";
            this.txtFile.ReadOnly = true;
            this.txtFile.ReadOnly = true;
            this.txtLayOut.ReadOnly = true;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            this.txtFile.Text = "";
            this.txtLayOut.Text = "";
            this.m_bIsEndTrans = false;
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            int i;
            if (this.listBox1.SelectedItem != null)
            {
                i=this.listBox1.SelectedIndex;
                this.listBox1.Items.RemoveAt(i);
                this.listBox2.Items.RemoveAt(i);
            }
        }

        private void butLayOut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pBrowserDialog;
            pBrowserDialog=new FolderBrowserDialog();
            pBrowserDialog.Description="指定输出Shape文件的路径";
            pBrowserDialog.ShowDialog();
            this.txtLayOut.Text = pBrowserDialog.SelectedPath.Trim();
        }
    }
}