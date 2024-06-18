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
    /// ����դ�����ݴ�����
    /// </summary>
    public partial class FormImportRaster : Form
    {
        private IArray m_rasterArray = new ArrayClass();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormImportRaster()
        {
            InitializeComponent();
            SetToolTip();
        }

        private void SetToolTip()
        {
            ToolTip tbtnAddTip = new ToolTip();
            tbtnAddTip.SetToolTip(btnAdd, "���դ�������ļ�");
            ToolTip tbtnDelTip = new ToolTip();
            tbtnDelTip.SetToolTip(btnDel, "ɾ��դ�������ļ�");
            ToolTip tbtnOpen = new ToolTip();
            tbtnOpen.SetToolTip(btnOpen, "ѡ�����Ŀ��դ�����ݼ�");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //���դ�����ݼ���������IArray��
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "դ������(*.tif)|*.tif";
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
                    Utility.Common.MessageHandler.ShowInfoMsg("��ѡ��Ŀ��դ�����ݼ�", Text);
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
                        //����
                        Ras2GeoDatabase(Path.GetDirectoryName(strTmp), Path.GetFileName(strTmp), txtRasterName.Tag as IRasterDataset, iRasterLoader);
                        progressBar1.Value++;
                        Application.DoEvents();
                    }

                    this.Cursor = Cursors.Default;
                    lblProgress.Text = "�������";
                    Utility.Common.MessageHandler.ShowInfoMsg("����ɹ�", Text);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Utility.Common.MessageHandler.ShowInfoMsg("�������դ������", Text);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
        }

        private void frmAddRaster_Load(object sender, EventArgs e)
        {
            //��ʼ��
            foreach (string str in Enum.GetNames(typeof(ESRI.ArcGIS.DataSourcesRaster.rstMosaicColormapMode)))
            {
                cbxMCMode.Items.Add(str);
            }
            cbxMCMode.SelectedIndex = 1;
        }


        /// <summary>
        /// ����RasterLoader����
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
        /// դ�����ݺϳɵ�FGDBդ�����ݼ���
        /// </summary>
        /// <param name="inRasFilePath">�赼���դ�����ݼ�·��</param>
        /// <param name="inRasFileName">�赼���դ�����ݼ�����</param>
        /// <param name="outRasDataSet">Ŀ���դ�����ݼ�����</param>
        /// <param name="piRasterLoader">IRasterLoader�ӿ�</param>
        private void Ras2GeoDatabase(string inRasFilePath, string inRasFileName, IRasterDataset outRasDataSet, IRasterLoader piRasterLoader)
        {
            //��դ�����ݼ�
            IWorkspaceFactory inRasWorkspaceFac = new RasterWorkspaceFactoryClass();
            IWorkspace inWorkspace = inRasWorkspaceFac.OpenFromFile(inRasFilePath, 0);
            IRasterWorkspace inRasWorkspace = (IRasterWorkspace)inWorkspace;
            IRasterDataset inRasDataset = inRasWorkspace.OpenRasterDataset(inRasFileName);

            //��ȡIRaster
            IRasterLayer inRasLayer = new RasterLayerClass();
            inRasLayer.CreateFromDataset(inRasDataset);
            IRaster inRas = inRasLayer.Raster;

            IRasterBandCollection tRasterBandCollOut = outRasDataSet as IRasterBandCollection;
            IRasterBandCollection tRasterBandCollIn = inRas as IRasterBandCollection;

            if (tRasterBandCollOut.Count != tRasterBandCollIn.Count)
            {
                throw new Exception("����դ����Ŀ��դ�񲨶β�һ�£����ܵ���");
            }

            //����դ�����ݣ�������2�ַ�����һ��ʹ��Load����һ��ʹ��LoadRasters��
            if (piRasterLoader != null)
            {
                piRasterLoader.Load(outRasDataSet, inRas);
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //��դ�����ݼ�
            Catalog.IDataBrowser frm = new FormDataBrowser();
            frm.AddFilter(new Catalog.Filters.RasterDatasetFilter());
            frm.Text = "ѡ��դ�����ݼ�";
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