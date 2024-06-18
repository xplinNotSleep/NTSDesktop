using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// 导入栅格数据窗体类
    /// </summary>
    public partial class FormImportRaster : Form
    {
        private IArray m_rasterArray = new ArrayClass();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormImportRaster()
        {
            InitializeComponent();
            SetToolTip();
        }

        private void SetToolTip()
        {
            ToolTip tbtnAddTip = new ToolTip();
            tbtnAddTip.SetToolTip(btnAdd, "添加栅格数据文件");
            ToolTip tbtnDelTip = new ToolTip();
            tbtnDelTip.SetToolTip(btnDel, "删除栅格数据文件");
            ToolTip tbtnOpen = new ToolTip();
            tbtnOpen.SetToolTip(btnOpen, "选择导入的目标栅格数据集");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //添加栅格数据集并保存在IArray中
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "栅格数据(*.tif)|*.tif";
            ofd.Multiselect = true;
            ofd.RestoreDirectory = true;
            string[] fileArray = null;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileArray = ofd.FileNames;
            }
            if (fileArray != null && fileArray.Length > 0)
            {
                foreach (string file in fileArray)
                {
                    lstRaster.Items.Add(file);
                    m_rasterArray.Add(file);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lstRaster.SelectedIndex >= 0)
            {
                m_rasterArray.Remove(lstRaster.SelectedIndex);
                lstRaster.Items.RemoveAt(lstRaster.SelectedIndex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(txtRasterName.Tag is IRasterDataset))
                {
                    Utility.Common.MessageHandler.ShowInfoMsg("请选择目标栅格数据集", Text);
                    return;
                }

                if (m_rasterArray.Count > 0)
                {
                    rstMosaicColormapMode rstMColormapMode = (rstMosaicColormapMode)Enum.Parse(typeof(ESRI.ArcGIS.DataSourcesRaster.rstMosaicColormapMode), cbxMCMode.SelectedItem.ToString());
                    double pixelTolerance = Convert.ToDouble(nudPATolerance.Value);
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    progressBar1.Maximum = m_rasterArray.Count;

                    IRasterLoader iRasterLoader = CreateIRasterLoader(rstMColormapMode, pixelTolerance, Convert.ToInt32(nudBackGround.Value), chkOne2Eight.Checked);
                    for (int i = 0; i < m_rasterArray.Count; i++)
                    {
                        string strTmp = (string)m_rasterArray.get_Element(i);
                        lblProgress.Text = Path.GetFileName(strTmp);
                        Application.DoEvents();
                        //导入
                        Ras2GeoDatabase(Path.GetDirectoryName(strTmp), Path.GetFileName(strTmp), txtRasterName.Tag as IRasterDataset, iRasterLoader);
                        progressBar1.Value++;
                        Application.DoEvents();
                    }

                    this.Cursor = Cursors.Default;
                    lblProgress.Text = "导入完成";
                    Utility.Common.MessageHandler.ShowInfoMsg("导入成功", Text);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Utility.Common.MessageHandler.ShowInfoMsg("请先添加栅格数据", Text);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void frmAddRaster_Load(object sender, EventArgs e)
        {
            //初始化
            foreach (string str in Enum.GetNames(typeof(ESRI.ArcGIS.DataSourcesRaster.rstMosaicColormapMode)))
            {
                cbxMCMode.Items.Add(str);
            }
            cbxMCMode.SelectedIndex = 1;
        }


        /// <summary>
        /// 创建RasterLoader对象
        /// </summary>
        /// <param name="prstMosaicColormapMode"></param>
        /// <param name="pPixelAlignmentTolerance"></param>
        /// <param name="iBackground"></param>
        /// <param name="bForeground"></param>
        /// <returns></returns>
        private IRasterLoader CreateIRasterLoader(rstMosaicColormapMode prstMosaicColormapMode, double pPixelAlignmentTolerance, int iBackground,bool bForeground)
        {
            IRasterLoader iRasterLoader = new RasterLoaderClass();
            if (iBackground != -1)
            {
                iRasterLoader.Background = iBackground;
            }
            else
            {

            }
            if (bForeground)
            {
                iRasterLoader.Foreground = 255;
            }

            iRasterLoader.MosaicColormapMode = prstMosaicColormapMode;
            iRasterLoader.PixelAlignmentTolerance = pPixelAlignmentTolerance;
            return iRasterLoader;
        }

        /// <summary>
        /// 栅格数据合成到FGDB栅格数据集中
        /// </summary>
        /// <param name="inRasFilePath">需导入的栅格数据集路径</param>
        /// <param name="inRasFileName">需导入的栅格数据集名称</param>
        /// <param name="outRasDataSet">目标的栅格数据集名称</param>
        /// <param name="piRasterLoader">IRasterLoader接口</param>
        private void Ras2GeoDatabase(string inRasFilePath, string inRasFileName, IRasterDataset outRasDataSet, IRasterLoader piRasterLoader)
        {
            //打开栅格数据集
            IWorkspaceFactory inRasWorkspaceFac = new RasterWorkspaceFactoryClass();
            IWorkspace inWorkspace = inRasWorkspaceFac.OpenFromFile(inRasFilePath, 0);
            IRasterWorkspace inRasWorkspace = (IRasterWorkspace)inWorkspace;
            IRasterDataset inRasDataset = inRasWorkspace.OpenRasterDataset(inRasFileName);

            //获取IRaster
            IRasterLayer inRasLayer = new RasterLayerClass();
            inRasLayer.CreateFromDataset(inRasDataset);
            IRaster inRas = inRasLayer.Raster;

            IRasterBandCollection tRasterBandCollOut = outRasDataSet as IRasterBandCollection;
            IRasterBandCollection tRasterBandCollIn = inRas as IRasterBandCollection;

            if (tRasterBandCollOut.Count != tRasterBandCollIn.Count)
            {
                throw new Exception("导入栅格与目标栅格波段不一致，不能导入");
            }

            //导入栅格数据（这里有2种方法，一种使用Load，另一种使用LoadRasters）
            if (piRasterLoader != null)
            {
                piRasterLoader.Load(outRasDataSet, inRas);
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //打开栅格数据集
            Catalog.IDataBrowser frm = new FormDataBrowser();
            frm.AddFilter(new Catalog.Filters.RasterDatasetFilter());
            frm.Text = "选择栅格数据集";
            frm.MultiSelect = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<Catalog.DataItems.DataItem> items = frm.SelectedItems;
                if (items.Count == 0)
                    return;
                object obj = items[0].GetGeoObject();
                if (obj != null)
                {
                    if (obj is IRasterDataset)
                    {
                        txtRasterName.Text = (obj as IDataset).FullName.NameString;
                        txtRasterName.Tag = obj;
                    }
                }
            }
        }
    }
}