using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SpatialAnalyst;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 栅格数据裁剪窗体类
    /// </summary>
    public partial class FormRasterClip : Form, ISelAreaForm
    {
        private IHookHelperEx m_hookHelperEx;
        private Dictionary<string, ILayer> m_dictLayers;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormRasterClip()
        {
            InitializeComponent();
            this.controlSelArea1.MainForm = this;
        }

        private void FormRasterClip_Load(object sender, EventArgs e)
        {
            //初始化字典类
            m_dictLayers = new Dictionary<string, ILayer>();
            //读取已加载的栅格图层
            IEnumLayer tEnumLayer = GetRasterLayers();
            ILayer tLayer = tEnumLayer.Next();
            while (tLayer != null)
            {
                m_dictLayers.Add(tLayer.Name, tLayer);
                //图层名
                cboxSource.Items.Add(tLayer.Name);
                tLayer = tEnumLayer.Next();
            }

            ToolTip btnPathTip = new ToolTip();
            btnPathTip.SetToolTip(btnOpen, "设置栅格数据输出目录");
        }

        /// <summary>
        /// 获取用户区域选择控件
        /// </summary>
        public ControlSelArea SelArea
        {
            get { return this.controlSelArea1; }
        }

        /// <summary>
        /// 遍历所有加载的栅格图层
        /// </summary>
        /// <returns></returns>
        private IEnumLayer GetRasterLayers()
        {
            IMap tMap = m_hookHelperEx.ActiveView.FocusMap;
            UID uid = new UIDClass();
            uid.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";   //RasterLayer
            IEnumLayer layers = tMap.get_Layers(uid, true);
            return layers;
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
        /// Raster裁剪
        /// </summary>
        /// <param name="pGeoDataset">输入栅格数据源</param>
        /// <param name="pGeometry">裁剪图形</param>
        /// <param name="pSelectType">图形类型</param>
        /// <returns></returns>
        private IGeoDataset RasterClip(IGeoDataset pGeoDataset, IGeometry pGeometry, AreaSelectType pSelectType)
        {
            //因为 tExtractionOp.Polygon函数处理圆时有bug，因此要处理为普通多边形
            if (pSelectType == AreaSelectType.TYPE_CIRCLE)
            {
                IPolycurve tPolycurve = pGeometry as IPolycurve;
                tPolycurve.Densify(0.1, 0.01);
            }

            IExtractionOp tExtractionOp = new RasterExtractionOpClass();
            return tExtractionOp.Polygon(pGeoDataset, pGeometry as IPolygon, true);
        }

        /// <summary>
        /// 颜色处理
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pOutput"></param>
        /// <returns></returns>
        private IRaster AddColormap(IRasterLayer pInput, IGeoDataset pOutput)
        {
            IRaster tOutRaster = null;
            if (pOutput is IRaster2)
            {
                tOutRaster = pOutput as IRaster;
            }
            else if(pOutput is IRasterLayer)
            {
                IRasterLayer tRasterLayer = pOutput as IRasterLayer;
                tOutRaster = tRasterLayer.Raster;
            }
            else if(pOutput is IRasterDataset)
            {
                IRasterDataset rasterDataset = pOutput as IRasterDataset;
                tOutRaster = rasterDataset.CreateDefaultRaster();
            }
            //更新颜色模块
            IRasterLayer tOutRasterLayer = new RasterLayerClass();
            tOutRasterLayer.CreateFromRaster(tOutRaster);
            tOutRasterLayer.Renderer = pInput.Renderer;
            tOutRasterLayer.Renderer.Update();

            return tOutRaster;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            //设置输出路径
            SaveFileDialog tSfd = new SaveFileDialog();
            tSfd.Title = @"输出文件";
            tSfd.Filter = @"TIFF文件(*.tif)|*.tif";
            if (tSfd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = tSfd.FileName;
            }
        }

        private void btnClip_Click(object sender, EventArgs e)
        {
            if (this.cboxSource.Text == string.Empty)
            {
                MessageBox.Show("请选择栅格数据集", "栅格数据裁剪", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.txtPath.Text == string.Empty)
            {
                MessageBox.Show("请选择保存路径", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TrackProgressDialog tProgress = new TrackProgressDialog();
            tProgress.DisplayTotal = false;
            tProgress.SubMax = 3;
            tProgress.TopMost = true;
            tProgress.Show();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //获取输入数据源
                ILayer tLayer = m_dictLayers[cboxSource.SelectedItem.ToString()];
                IRasterLayer tRasterLayer = tLayer as IRasterLayer;
                IRaster tRaster = tRasterLayer.Raster;
                IGeoDataset tInputGeo = tRaster as IGeoDataset;

                tProgress.SubValue = 1;
                tProgress.SubMessage = "分析栅格数据……";
                Application.DoEvents();

                //裁剪数据
                IGeoDataset tOutputGeo = RasterClip(tInputGeo, controlSelArea1.RegionGeometry, controlSelArea1.SelectType);
                if (tOutputGeo == null)
                {
                    MessageHandler.ShowInfoMsg("裁剪失败", Text);
                }
                
                //保存裁剪结果
                if (!tProgress.IsContinue)
                {
                    this.Close();
                    return;
                }
                    
                tProgress.SubValue = 2;
                tProgress.SubMessage = "修正栅格数据波段颜色……";
                Application.DoEvents();

                IRaster clipRaster = AddColormap(tRasterLayer, tOutputGeo);
                IWorkspaceFactory tWsf = new RasterWorkspaceFactoryClass();

                IWorkspace tWorkspace = tWsf.OpenFromFile(System.IO.Path.GetDirectoryName(txtPath.Text), 0);
                ISaveAs2 tSaveAs = clipRaster as ISaveAs2;
                tSaveAs.SaveAs(System.IO.Path.GetFileName(txtPath.Text), tWorkspace, "TIFF");

                tProgress.SubValue = 3;
                tProgress.SubMessage = "裁剪成功……";
                Application.DoEvents();

                if (MessageBox.Show("裁剪成功，是否继续裁剪", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageHandler.ShowErrorMsg(ex);
            }
            finally
            {
                tProgress.AutoFinishClose = true;
                tProgress.SetFinish();
                this.Cursor = Cursors.Default;
            }
        }

        private void FormRasterClip_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.controlSelArea1.ExitSelect();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}