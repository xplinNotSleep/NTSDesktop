using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// 创建栅格数据窗体类
    /// </summary>
    public partial class FormCreateRasterDataSet : Form
    {
        private Dictionary<string, rstPixelType> dicRstPixelType;
        private Dictionary<string, esriRasterCompressionType> dicEsriRasterCompressionType;
        private IWorkspace m_iWorkSpace;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormCreateRasterDataSet()
        {
            InitializeComponent();
            SetToolTip();
        }

        private void SetToolTip()
        {
            ToolTip tbtSelectSpatialRef = new ToolTip();
            tbtSelectSpatialRef.SetToolTip(btSelectSpatialRef, "设置空间参考");
            ToolTip tbtnOpen = new ToolTip();
            tbtnOpen.SetToolTip(btnOpen, "选择数据库");
        }
        private void frmCRasterDataSet_Load(object sender, EventArgs e)
        {
            //初始化cbxPixel
            if (dicRstPixelType == null)
            {
                dicRstPixelType = new Dictionary<string, rstPixelType>();
            }
            dicRstPixelType.Add("1_BIT", rstPixelType.PT_U1);
            dicRstPixelType.Add("2_BIT", rstPixelType.PT_U2);
            dicRstPixelType.Add("4_BIT", rstPixelType.PT_U4);
            dicRstPixelType.Add("8_BIT_UNSIGNED", rstPixelType.PT_UCHAR);
            dicRstPixelType.Add("8_BIT_SIGNED", rstPixelType.PT_CHAR);
            dicRstPixelType.Add("16_BIT_UNSIGNED", rstPixelType.PT_USHORT);
            dicRstPixelType.Add("16_BIT_SIGNED", rstPixelType.PT_SHORT);
            dicRstPixelType.Add("32_BIT_UNSIGNED", rstPixelType.PT_ULONG);
            dicRstPixelType.Add("32_BIT_SIGNED", rstPixelType.PT_LONG);
            dicRstPixelType.Add("32_BIT_FLOAT", rstPixelType.PT_FLOAT);
            dicRstPixelType.Add("64_BIT", rstPixelType.PT_DOUBLE);

            cbxPixel.Items.Add("1_BIT");
            cbxPixel.Items.Add("2_BIT");
            cbxPixel.Items.Add("4_BIT");
            cbxPixel.Items.Add("8_BIT_UNSIGNED");
            cbxPixel.Items.Add("8_BIT_SIGNED");
            cbxPixel.Items.Add("16_BIT_UNSIGNED");
            cbxPixel.Items.Add("16_BIT_SIGNED");
            cbxPixel.Items.Add("32_BIT_UNSIGNED");
            cbxPixel.Items.Add("32_BIT_SIGNED");
            cbxPixel.Items.Add("32_BIT_FLOAT");
            cbxPixel.Items.Add("64_BIT");
            //默认值
            cbxPixel.SelectedIndex = 3;

            //初始化cbxCompression
            if (dicEsriRasterCompressionType == null)
            {
                dicEsriRasterCompressionType = new Dictionary<string, esriRasterCompressionType>();
            }
            dicEsriRasterCompressionType.Add("LZ77", esriRasterCompressionType.esriRasterCompressionLZ77);
            dicEsriRasterCompressionType.Add("JPEG", esriRasterCompressionType.esriRasterCompressionJPEG);
            dicEsriRasterCompressionType.Add("JPEG2000", esriRasterCompressionType.esriRasterCompressionJPEG2000);
            dicEsriRasterCompressionType.Add("不压缩", esriRasterCompressionType.esriRasterCompressionUncompressed);

            cbxCompression.Items.Add("LZ77");
            cbxCompression.Items.Add("JPEG");
            cbxCompression.Items.Add("JPEG2000");
            cbxCompression.Items.Add("不压缩");

            cbxCompression.SelectedIndex = 0;

            //初始化cbxPyramidResample
            foreach (string str in Enum.GetNames(typeof(rstResamplingTypes)))
            {
                cbxPyramidResample.Items.Add(str);
            }
            //默认值
            cbxPyramidResample.SelectedIndex = 0;

            ISpatialReference tSpatialReference = new UnknownCoordinateSystemClass();
            txtSpatialRef.Text = tSpatialReference.Name;
            txtSpatialRef.Tag = tSpatialReference;

            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.Update();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if(m_iWorkSpace==null)
                {
                    Utility.Common.MessageHandler.ShowErrorMsg("请选择栅格数据集的目录", Text);
                    return;
                }
                if (txtName.Text.Trim().Length == 0)
                {
                    Utility.Common.MessageHandler.ShowErrorMsg("请输入创建的栅格数据集名称", Text);
                    return;
                }
                string strName = txtName.Text;
                //像素枚举值
                rstPixelType PixelType = dicRstPixelType[cbxPixel.SelectedItem.ToString()];
                int iNumberOfBands = Convert.ToInt32(nudBands.Value);

                //创建RasterStorageDef用于创建栅格数据集
                IRasterStorageDef storageDef = new RasterStorageDefClass();
                if (txtCellSize.Text.Trim().Length != 0)
                {
                    IPnt iPnt = new PntClass();
                    iPnt.SetCoords(Convert.ToDouble(txtCellSize.Text.Trim()), Convert.ToDouble(txtCellSize.Text.Trim()));
                    storageDef.CellSize = iPnt;
                }
                //高级选项
                if (chkAdvanced.Checked)
                {
                    //压缩方式
                    storageDef.CompressionType = dicEsriRasterCompressionType[cbxCompression.SelectedItem.ToString()];
                    if (nudCompression.Enabled)
                    {
                        //压缩质量
                        storageDef.CompressionQuality = Convert.ToInt32(nudCompression.Value);
                    }
                    if (chkPyramid.Checked)
                    {
                        //金字塔
                        storageDef.PyramidLevel = Convert.ToInt32(nudPyramidLevel.Value);
                        storageDef.PyramidResampleType = (rstResamplingTypes)Enum.Parse(typeof(rstResamplingTypes), cbxPyramidResample.SelectedItem.ToString());
                    }
                    //不选择创建金字塔默认为不创建
                    else
                    {
                        storageDef.PyramidLevel = 0;
                    }
                    storageDef.TileHeight = Convert.ToInt32(txtHeight.Text);
                    storageDef.TileWidth = Convert.ToInt32(txtWidth.Text);
                    if (txtX.Text.Trim() != "" && txtY.Text.Trim() != "")
                    {
                        IPoint point = new PointClass();
                        point.PutCoords(Convert.ToDouble(txtX.Text), Convert.ToDouble(txtY.Text));
                        storageDef.Origin = point;
                    }
                }
                else
                {
                    storageDef.PyramidLevel = 0;
                }
                //空间参考
                if (txtSpatialRef.Tag != null)
                {
                    ISpatialReference iSpatialRef = txtSpatialRef.Tag as ISpatialReference;
                    IRasterDef iRasterDef = new RasterDefClass();
                    iRasterDef.SpatialReference = iSpatialRef;
                    //创建栅格数据
                    CreateRaster(m_iWorkSpace, strName, iNumberOfBands, PixelType, storageDef, "DEFAULTS", iRasterDef, null);
                }
                else
                {
                    CreateRaster(m_iWorkSpace, strName, iNumberOfBands, PixelType, storageDef, "DEFAULTS", null, null);
                }
                IntiListBox(m_iWorkSpace);
                Utility.Common.MessageHandler.ShowInfoMsg("创建栅格数据集成功", Text);
            }
            catch (Exception ex)
            {
                Utility.Common.MessageHandler.ShowErrorMsg("创建栅格数据集失败！" + ex.Message, Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void chkAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkAdvanced.Checked)
            {
                gboxAdvance.Enabled = true;
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.Update();
            }
            else
            {
                gboxAdvance.Enabled = false;
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.Update();
            }
        }

        private void cbxCompression_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCompression.SelectedIndex == 0 || cbxCompression.SelectedIndex == 3)
            {
                nudCompression.Enabled = false;
            }
            else
            {
                nudCompression.Enabled = true;
            }
        }

        private void chkPyramid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPyramid.Checked)
            {
                nudPyramidLevel.Enabled = true;
                cbxPyramidResample.Enabled = true;
            }
            else
            {
                nudPyramidLevel.Enabled = false;
                cbxPyramidResample.Enabled = false;
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Catalog.IDataBrowser frm = new Catalog.FormDataBrowser();
            frm.AddFilter(new Catalog.Filters.FileGeoDatabaseFilter());
            frm.AddFilter(new Catalog.Filters.SDEWorkspaceFilter());
            frm.AddFilter(new Catalog.Filters.PersonalGeoDatabaseFilter());
            frm.AddFilter(new Catalog.Filters.FeatureDatasetFilter());
            frm.MultiSelect = false;
            frm.Text = "选择工作空间";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<Catalog.DataItems.DataItem> items = frm.SelectedItems;
                if (items.Count == 0)
                    return;
                object obj = items[0].GetGeoObject();
                if (obj != null)
                {
                    if (obj is IWorkspace)
                    {
                        txtPath.Text = (obj as IWorkspace).PathName;
                        txtPath.Tag = obj;
                        try
                        {
                            m_iWorkSpace = txtPath.Tag as IWorkspace;
                            //初始化ListBox
                            IntiListBox(m_iWorkSpace);
                        }
                        catch (Exception ex)
                        {
                            Utility.Common.MessageHandler.ShowErrorMsg("打开文件数据库失败", ex);
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 创建栅格数据集
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="pName"></param>
        /// <param name="pnumBands"></param>
        /// <param name="pPixelType"></param>
        /// <param name="pStorageDef"></param>
        /// <param name="pKeyword"></param>
        /// <param name="pRasterDef"></param>
        /// <param name="pGeometryDef"></param>
        /// <returns></returns>
        private IRasterDataset CreateRaster(IWorkspace pWorkspace, string pName, int pnumBands, rstPixelType pPixelType, IRasterStorageDef pStorageDef, string pKeyword, IRasterDef pRasterDef, IGeometryDef pGeometryDef)
        {
            try
            {
                IWorkspaceFactory2 pWsFact = new FileGDBWorkspaceFactoryClass();
                IRasterWorkspaceEx pWs = pWorkspace as IRasterWorkspaceEx;
                IRasterDataset rasterDataset = pWs.CreateRasterDataset(pName, pnumBands, pPixelType, pStorageDef, pKeyword, pRasterDef, pGeometryDef);
                return rasterDataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 打开FGDB
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        private IWorkspace OpenFileGDB(string pPath)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace work = workspaceFactory.OpenFromFile(pPath, 0);
                return work;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 初始化ListBox
        /// </summary>
        /// <param name="piWorkspace"></param>
        private void IntiListBox(IWorkspace piWorkspace)
        {
            lstRaster.Items.Clear();
            try
            {
                List<string> listStr = ListRasterDataSet(piWorkspace);
                foreach (string str in listStr)
                {
                    lstRaster.Items.Add(str);
                }
            }
            catch (Exception ex)
            {
                Utility.Common.MessageHandler.ShowErrorMsg("初始化控件失败" + ex.Message, Text);
            }

        }
        /// <summary>
        /// 列出所有的栅格数据集
        /// </summary>
        /// <param name="piWorkspace"></param>
        /// <returns></returns>
        private List<string> ListRasterDataSet(IWorkspace piWorkspace)
        {
            try
            {
                List<string> listStr = new List<string>();
                //列出所有的栅格数据集
                IEnumDataset enumDataSet = piWorkspace.get_Datasets(esriDatasetType.esriDTRasterDataset);
                IDataset dataSet = enumDataSet.Next();
                while (dataSet != null)
                {
                    listStr.Add(dataSet.Name);
                    dataSet = enumDataSet.Next();
                }
                return listStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //删除栅格数据集
        private void DeleteRaster(IWorkspace pWorkspace, string pName)
        {
            try
            {
                IWorkspaceFactory2 pWsFact = new FileGDBWorkspaceFactoryClass();
                IRasterWorkspaceEx pWs = pWorkspace as IRasterWorkspaceEx;
                pWs.DeleteRasterDataset(pName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //鼠标右键操作
            if (e.Button == MouseButtons.Right)
            {
                int index = lstRaster.IndexFromPoint(e.X, e.Y);
                if (index >= 0)
                {
                    lstRaster.SelectedIndex = index;
                    this.contextMenuStrip1.Show(this.lstRaster, e.X, e.Y);
                }
            }
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNoCancel))
                {
                    DeleteRaster(m_iWorkSpace, lstRaster.Items[lstRaster.SelectedIndex].ToString());
                    IntiListBox(m_iWorkSpace);
                }
            }
            catch (Exception ex)
            {
                Utility.Common.MessageHandler.ShowInfoMsg("删除栅格数据集成功", Text);
            }
        }

        private void btSelectSpatialRef_Click(object sender, EventArgs e)
        {
            //设置空间参考
            GeoDataBase.SpatialReferenceDialog dlg = new GeoDataBase.SpatialReferenceDialog();
            dlg.SpatialReference = this.txtSpatialRef.Tag as ISpatialReference;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtSpatialRef.Tag = dlg.SpatialReference;
                txtSpatialRef.Text = dlg.SpatialReference.Name;
            }
        }
    }
}