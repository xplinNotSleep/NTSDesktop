using AG.COM.SDM.CADManager;
using AG.COM.SDM.CADManager.Convert;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public partial class FormXZQFFExportCAD : Form
    {
        #region 变量
      
        private IMapControlEvents2_OnAfterDrawEventHandler m_AfterDrawEvent2 = null;

        private IHookHelperEx m_hookHelper = null;

        /// <summary>
        /// 当前
        /// </summary>
        private SelectExtentExportCADInitTag m_InitTag = null;

        /// <summary>
        /// 在地图上画的查询范围的样式
        /// </summary>
        private ISymbol m_OuterExtentSymbol = null;

        /// <summary>
        /// 所有范围（所有图幅号、所有行政区）
        /// </summary>
        private List<string> m_AllExtent = new List<string>();

        /// <summary>
        /// 范围FeatureClass对象
        /// </summary>
        private IFeatureClass m_FeatureClassExtent = null;

        /// <summary>
        /// 图幅FeatureClass对象
        /// </summary>
        private IFeatureClass m_FeatureClassTF = null;

        /// <summary>
        /// 图幅名称字段索引
        /// </summary>
        private int m_TFFieldIdx = -1;

        /// <summary>
        /// 范围featureclass名（必须传入）
        /// </summary>
        public string ExtentFeatureClassName
        {
            get;
            set;
        }

        /// <summary>
        /// 范围字段名（必须传入）
        /// </summary>
        public string ExtentFieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 图幅featureclass名（必须传入）
        /// </summary>
        public string TFFeatureClassName
        {
            get;
            set;
        }

        /// <summary>
        /// 图幅字段名（必须传入）
        /// </summary>
        public string TFFieldName
        {
            get;
            set;
        }   

        /// <summary>
        /// 查询范围
        /// </summary>
        public IGeometry SearchExtent
        {
            get;
            set;
        }

        /// <summary>
        /// 保存所有查找值的拼音
        /// </summary>
        private Dictionary<string, List<List<string>>> m_PinYins = new Dictionary<string, List<List<string>>>();

        #endregion

        #region 初始化

        public FormXZQFFExportCAD(SelectExtentExportCADInitTag tInitTag, IHookHelperEx tHookHelper)
        {
            InitializeComponent();

            m_InitTag = tInitTag;
            m_hookHelper = tHookHelper;
        }

        private void FormSelectExtentPrint_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化CAD版本信息
                CADVersionHelper.InitVersionInfo();
                //检查CAD是否安装
                if (CADVersionHelper.CheckCurrentVersionIsInstall(true) == false)
                {
                    Close();
                    return;
                }

                //画出外部选择范围
                m_AfterDrawEvent2 = new IMapControlEvents2_OnAfterDrawEventHandler(OnAfterDraw2);
                (m_hookHelper.MapService.MapControl as IMapControlEvents2_Event).OnAfterDraw += m_AfterDrawEvent2;

                InitControl();

                ///初始化图层树
                this.ltvLayer.InitUI(this.m_hookHelper.FocusMap as IBasicMap);

                //添加CAD模板文件
                cmbCADTemplate.DataSource = ConvertManagerHelper.GetAllCADTemplateFile();
                if (cmbCADTemplate.Items.Count > 0)
                    cmbCADTemplate.SelectedIndex = 0;

                BindCADVersion();

                IFeatureWorkspace tFeatureWorkspace = CommonVariables.DatabaseConfig.Workspace as IFeatureWorkspace;
                IWorkspace2 tWorkspace2 = tFeatureWorkspace as IWorkspace2;
                if (tWorkspace2.get_NameExists(esriDatasetType.esriDTFeatureClass, ExtentFeatureClassName) == false)
                {
                    throw new Exception("SDE数据库中找不到图层 " + ExtentFeatureClassName);
                }
                m_FeatureClassExtent = tFeatureWorkspace.OpenFeatureClass(ExtentFeatureClassName);

                int extentFieldIdx = m_FeatureClassExtent.FindField(ExtentFieldName);
                if (extentFieldIdx < 0)
                {
                    throw new Exception("图层 " + ExtentFeatureClassName + " 没有 " + ExtentFieldName + " 字段");
                }

                if (tWorkspace2.get_NameExists(esriDatasetType.esriDTFeatureClass, TFFeatureClassName) == false)
                {
                    throw new Exception("SDE数据库中找不到图层 " + TFFeatureClassName);
                }
                m_FeatureClassTF = tFeatureWorkspace.OpenFeatureClass(TFFeatureClassName);

                m_TFFieldIdx = m_FeatureClassTF.FindField(TFFieldName);
                if (m_TFFieldIdx < 0)
                {
                    throw new Exception("图层 " + TFFeatureClassName + " 没有 " + TFFieldName + " 字段");
                }

                IFeatureCursor tFeatureCursor = m_FeatureClassExtent.Search(null, false);
                IFeature tFeature = tFeatureCursor.NextFeature();
                //查出范围字段的所有值
                while (tFeature != null)
                {
                    object objValue = tFeature.get_Value(extentFieldIdx);
                    string value = Convert.ToString(objValue).Trim();
                    if (!string.IsNullOrEmpty(value) && m_AllExtent.Contains(value) == false)
                    {
                        m_AllExtent.Add(value);
                    }

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeature);
                    tFeature = tFeatureCursor.NextFeature();
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursor);

                m_AllExtent.Sort();

                lstAllExtent.DataSource = m_AllExtent;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
                Close();
            }
        }

        /// <summary>
        /// 绑定导出CAD版本
        /// </summary>
        private void BindCADVersion()
        {
            List<string> tCADVersion = CADVersionHelper.GetCanSaveVersionText();
            cmbCADVersion.DataSource = tCADVersion;

            if (cmbCADVersion.Items.Count > 0)
            {
                cmbCADVersion.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 根据不同的打印类型设置控件
        /// </summary>
        private void InitControl()
        {
            string printTitle = m_InitTag.PrintTheme;

            this.Text = printTitle + "导出";
            grpSelectType.Text = printTitle + "选择方式";
            grpSelectExtent.Text = "导出" + printTitle + "：";        
        }

        #endregion           

        #region 其他

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    lstAllExtent.DataSource = m_AllExtent;
                }
                else
                {
                    List<string> result = ChineseSpellHelper.PinYinQuery(txtSearch.Text, m_AllExtent, ref m_PinYins);

                    lstAllExtent.DataSource = result;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //数据验证
                if (Valid() == false) return;

                List<ILayer> tLayers = GetSelectLayer(ltvLayer.Nodes);
                List<VectorClip2CADMultiFenFu> tFenFus = new List<VectorClip2CADMultiFenFu>();

                IGeometry tGeometryExtent = null;

                IFeatureCursor tFeatureCursor = null;
                IFeatureCursor tFeatureCursorTF = null;
                IQueryFilter tQueryFilter = null;
                IFeature tFeature = null;

                string value = lstAllExtent.SelectedItem.ToString();

                tQueryFilter = new QueryFilterClass();
                tQueryFilter.WhereClause = ExtentFieldName + " = '" + value + "'";
                tFeatureCursor = m_FeatureClassExtent.Search(tQueryFilter, false);
                tFeature = tFeatureCursor.NextFeature();
                if (tFeature != null)
                {
                    tGeometryExtent = tFeature.ShapeCopy;
                    //不分幅
                    if (chkFenFu.Checked == false)
                    {
                        tGeometryExtent = BufferGeometry(tGeometryExtent);

                        VectorClip2CADMultiFenFu tFenFu = new VectorClip2CADMultiFenFu();
                        tFenFu.GeometryFenFu = tGeometryExtent;
                        //文件名不带扩展名
                        tFenFu.FileName = txtOutput.Text + "\\" + value;

                        tFenFus.Add(tFenFu);
                    }
                    //分幅
                    else
                    {
                        //空间查询，查出范围内的图幅
                        ISpatialFilter tSpatialFilter = new SpatialFilterClass();
                        tSpatialFilter.Geometry = tGeometryExtent;
                        tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        tSpatialFilter.GeometryField = m_FeatureClassTF.ShapeFieldName;

                        tFeatureCursorTF = m_FeatureClassTF.Search(tSpatialFilter, false);

                        IFeature tFeatureTF = tFeatureCursorTF.NextFeature();
                        while (tFeatureTF != null)
                        {
                            IGeometry tGeometryTF = tFeatureTF.ShapeCopy;
                            ITopologicalOperator tTopo = tGeometryTF as ITopologicalOperator;
                            //范围与图幅裁剪出最终范围
                            IGeometry tGeometryUse = tTopo.Intersect(tGeometryExtent, esriGeometryDimension.esriGeometry2Dimension);

                            tGeometryUse = BufferGeometry(tGeometryUse);

                            VectorClip2CADMultiFenFu tFenFu = new VectorClip2CADMultiFenFu();
                            tFenFu.GeometryFenFu = tGeometryUse;
                            tFenFu.GeometryFenFuExtent = tFeatureTF.ShapeCopy;
                            //文件名不带扩展名
                            tFenFu.FileName = System.IO.Path.Combine(txtOutput.Text, value + "_" + Convert.ToString(tFeatureTF.get_Value(m_TFFieldIdx)));

                            tFenFus.Add(tFenFu);

                            tFeatureTF = tFeatureCursorTF.NextFeature();
                        }
                    }
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeature);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursor);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorTF);

                ControlHelper.EnabledAllControls(this.Controls, false);

                //导出CAD
                XZQFFVectorClip2CADMultiHelper tXZQFFVectorClip2CADMultiHelper = new XZQFFVectorClip2CADMultiHelper(tLayers,
                cmbCADTemplate.Text, m_hookHelper.ActiveView, chkExportElement.Checked, cmbCADVersion.Text, tFenFus, chkFenFu.Checked, m_FeatureClassTF
                , TFFieldName);
                if (tXZQFFVectorClip2CADMultiHelper.MainProcess() == true)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
            finally
            {
                ControlHelper.EnabledAllControls(this.Controls, true);
            }
        }

        /// <summary>
        /// 进行缓冲
        /// </summary>
        /// <param name="tGeometry"></param>
        /// <returns></returns>
        private IGeometry BufferGeometry(IGeometry tGeometry)
        {
            if (nudBufferSize.Value > 0)
            {
                ITopologicalOperator tTopo = tGeometry as ITopologicalOperator;
                tGeometry = tTopo.Buffer(Convert.ToDouble(nudBufferSize.Value));
            }

            return tGeometry;
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
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            if (lstAllExtent.SelectedItems.Count < 1)
            {
                MessageBox.Show("请选择导出" + m_InitTag.PrintTheme);
                return false;
            }

            if (string.IsNullOrEmpty(txtOutput.Text))
            {
                MessageBox.Show("请先选择导出CAD文件位置");
                return false;
            }

            List<ILayer> tLayers = GetSelectLayer(ltvLayer.Nodes);
            if (tLayers.Count < 1)
            {
                MessageBox.Show("请先选择图层");
                return false;
            }

            string CADTemplateFile = cmbCADTemplate.Text;
            if (string.IsNullOrEmpty(CADTemplateFile))
            {
                MessageBox.Show("请先选择CAD模板");
                return false;
            }

            string CADVersion = cmbCADVersion.Text;
            if (string.IsNullOrEmpty(CADVersion))
            {
                MessageBox.Show("请先选择导出CAD版本");
                return false;
            }

            return true;
        }

        private void rdoFromMap_Click(object sender, EventArgs e)
        {
            try
            {
                SearchExtent = null;

                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutput.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }                                  

        #endregion
   
        #region 外部选择范围，确定打印范围

        private void btnMouseSelectExtent_Click(object sender, EventArgs e)
        {
            try
            {
                SearchExtent = null;

                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }  

        /// <summary>
        /// 外部传入一个几何，通过这个几何对象选择图幅或行政区
        /// </summary>
        /// <param name="tGeometry"></param>
        public void OuterSelectExtent(IGeometry tGeometry)
        {
            try
            {
                if (tGeometry == null) return;
                SearchExtent = tGeometry;
                //使用空间查询，查出范围中的图幅或行政区
                ISpatialFilter tSpatialFilter = new SpatialFilterClass();
                tSpatialFilter.Geometry = SearchExtent;
                tSpatialFilter.GeometryField = m_FeatureClassExtent.ShapeFieldName;
                tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                IFeatureCursor tFeatureCursor = m_FeatureClassExtent.Search(tSpatialFilter, false);
                int extentFieldIdx = tFeatureCursor.FindField(ExtentFieldName);
                IFeature tFeature = tFeatureCursor.NextFeature();
                while (tFeature != null)
                {
                    object objValue = tFeature.get_Value(extentFieldIdx);
                    if (objValue != null)
                    {
                        string value = objValue.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (lstAllExtent.Items.Contains(value) == true)
                            {
                                lstAllExtent.SelectedItem = value;
                                break;
                            }
                        }
                    }

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeature);
                    tFeature = tFeatureCursor.NextFeature();
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursor);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 画出选择范围
        /// </summary>
        /// <param name="Display"></param>
        /// <param name="viewDrawPhase"></param>
        private void OnAfterDraw2(object Display, int viewDrawPhase)
        {
            try
            {            
                if (viewDrawPhase == (int)esriViewDrawPhase.esriViewForeground)
                {
                    if (SearchExtent == null) return;
                    //生成样式
                    if (m_OuterExtentSymbol == null)
                        m_OuterExtentSymbol = MakeSymbolHelper.MakeSymbolType1(SearchExtent.GeometryType, 255, 0, 0, 120, 1);
                    //画出图形
                    m_hookHelper.ActiveView.ScreenDisplay.SetSymbol(m_OuterExtentSymbol);
                    m_hookHelper.ActiveView.ScreenDisplay.DrawPolygon(SearchExtent);
                }       
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }     

        private void FormSelectExtentPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (null != m_AfterDrawEvent2)
                {
                    (m_hookHelper.MapService.MapControl as IMapControlEvents2_Event).OnAfterDraw -= m_AfterDrawEvent2;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }

            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
        }

        #endregion                                
    }
}
