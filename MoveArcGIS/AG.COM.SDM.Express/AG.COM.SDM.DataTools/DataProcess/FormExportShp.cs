using AG.COM.SDM.DataTools.DataProcess.CoClass;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public partial class FormExportShp : Form
    {
        private IHookHelperEx m_hookHelper = null;

        public FormExportShp(IHookHelperEx hookHelper)
        {
            m_hookHelper = hookHelper;

            InitializeComponent();
        }

        private void FormExportShp_Load(object sender, EventArgs e)
        {
            try
            {
                ltvLayer.InitUI(m_hookHelper.FocusMap as IBasicMap);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = fbd.SelectedPath;
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
                if (string.IsNullOrEmpty(txtOutputPath.Text))
                {
                    MessageBox.Show("请先选择导出位置");
                    return;
                }

                List<IFeatureLayer> featureLayers = ltvLayer.GetSelectFeatureLayer().OfType<IFeatureLayer>().ToList();
                if (featureLayers.Count < 1)
                {
                    MessageBox.Show("请先选择图层");
                    return;
                }

                ExportShpHelper exportShpHelper = new ExportShpHelper();
                if (exportShpHelper.Export(featureLayers, txtOutputPath.Text) == true)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
