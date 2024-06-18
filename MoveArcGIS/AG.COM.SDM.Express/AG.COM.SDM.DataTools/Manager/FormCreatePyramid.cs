using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.Manager
{
    ///<summary>
    /// 创建金字塔
    ///</summary>
    public partial class FormCreatePyramid : Form
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormCreatePyramid()
        {
            InitializeComponent();
            SetToolTip();
        }

        private void SetToolTip()
        {
            ToolTip tbtnOpenTip = new ToolTip();
            tbtnOpenTip.SetToolTip(btnOpen, "选择栅格数据集");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //打开栅格数据集
            Catalog.IDataBrowser frm = new Catalog.FormDataBrowser();
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
                        //读取选择的栅格数据集信息
                        IRasterPyramid2 tRasterPyramid = obj as IRasterPyramid2;
                        nudPyramidLevel.Value = tRasterPyramid.PyramidLevel;
                        cbxPyramidResample.Text = tRasterPyramid.PyramidResamplingMethod.ToString();
                        IPnt tPnt = tRasterPyramid.MinimumSize;
                        txtX.Text = tPnt.X.ToString();
                        txtY.Text = tPnt.Y.ToString();
                    }
                }
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            try
            {
                IRasterPyramid2 tRasterPyramid = txtRasterName.Tag as IRasterPyramid2;
                if (tRasterPyramid != null)
                {
                    //创建或修改金字塔
                    IPnt tPnt = new PntClass();
                    tPnt.X = Convert.ToInt64(txtX.Text);
                    tPnt.Y = Convert.ToInt64(txtY.Text);
                    tRasterPyramid.MinimumSize = tPnt;
                    tRasterPyramid.BuildPyramid(Convert.ToInt32(nudPyramidLevel.Value), (rstResamplingTypes) Enum.Parse(typeof (rstResamplingTypes), cbxPyramidResample.SelectedItem.ToString()));
                    Utility.Common.MessageHandler.ShowInfoMsg("成功创建金字塔", Text);
                }
            }
            catch(Exception ex)
            {
                Utility.Common.MessageHandler.ShowErrorMsg(ex);
            }
        }
        /// <summary>
        /// 检查输入数据
        /// </summary>
        private void CheckData()
        {
            if (cbxPyramidResample.SelectedItem.ToString() == "")
                Utility.Common.MessageHandler.ShowInfoMsg("请选择重采样模式", Text);
        }

        private void cbxPyramidResample_DropDown(object sender, EventArgs e)
        {            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormCreatePyramid_Load(object sender, EventArgs e)
        {
            //初始化下拉框
            foreach (string str in Enum.GetNames(typeof(rstResamplingTypes)))
            {
                cbxPyramidResample.Items.Add(str);
            }
        }
    }
}