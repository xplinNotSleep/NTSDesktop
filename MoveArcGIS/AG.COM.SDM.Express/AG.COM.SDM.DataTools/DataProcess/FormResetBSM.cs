using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 重新设置标识码 窗体类
    /// </summary>
    public partial class FormResetBSM : Form
    {
        private IMap m_Map = null;

        /// <summary>
        /// 设置地图对象
        /// </summary>
        public IMap Map
        {
            set
            {
                featureLayerSelector1.Map = value;
                m_Map = value;
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormResetBSM()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            IFeatureClass fcls = featureLayerSelector1.FeatureClass;
            if (fcls == null)
            {
                MessageHandler.ShowErrorMsg("请选择图层！", this.Text);
                return;
            }
            if (cboFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("请选择标识码字段！", this.Text);
                return;
            }
            string fldName = cboFields.Text;
            int startValue = (int)nudStartValue.Value;

            ITrackProgress pdlg = new TrackProgressDialog();
            pdlg.DisplayTotal = false;
            (pdlg as Form).Owner = this;
            pdlg.Show();

            IWorkspaceEdit wse = Utility.Editor.LibEditor.GetNewEditableWorkspace((fcls as IDataset).Workspace);
            wse.StartEditing(false);
            wse.StartEditOperation();
            try
            {
                pdlg.SubMax = fcls.FeatureCount(null);
                pdlg.SubValue = 0;
                Application.DoEvents();

                IFeatureCursor pCursor = fcls.Search(null, false);
                IFeature pFeature = pCursor.NextFeature();
                int fldIndex = fcls.Fields.FindField(fldName);
                while (pFeature != null)
                {
                    pFeature.set_Value(fldIndex, startValue);
                    startValue++;
                    pFeature.Store();

                    pdlg.SubValue++;
                    Application.DoEvents();

                    pFeature = pCursor.NextFeature();
                }
                wse.StopEditOperation();
                wse.StopEditing(true);
                pdlg.Close();
                MessageHandler.ShowInfoMsg("重编标识码完成！", this.Text);
            }
            catch (Exception ex)
            {
                wse.AbortEditOperation();
                wse.StopEditing(false);
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }
            finally
            {
                pdlg.Close();
            }
        }

        private void featureLayerSelector1_LayerChanged()
        {
            IFeatureClass fcls = featureLayerSelector1.FeatureClass;
            if (fcls != null)
            {
                cboFields.Items.Clear();
                IField fld;
                string fldName;
                for (int i = 0; i <= fcls.Fields.FieldCount - 1; i++)
                {
                    fld = fcls.Fields.get_Field(i);
                    if ((fld.Type == esriFieldType.esriFieldTypeInteger) ||
                        (fld.Type == esriFieldType.esriFieldTypeSingle) ||
                        (fld.Type == esriFieldType.esriFieldTypeDouble) ||
                        (fld.Type == esriFieldType.esriFieldTypeString))
                    {
                        cboFields.Items.Add(fld.Name);
                    }
                }
                for (int i = 0; i <= cboFields.Items.Count - 1; i++)
                {
                    fldName = cboFields.Items[i].ToString().ToLower();
                    if ((fldName == "bsm") || (fldName == "mbbsm"))
                    {
                        cboFields.SelectedIndex = i;
                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}