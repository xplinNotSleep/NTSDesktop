using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// �������ñ�ʶ�� ������
    /// </summary>
    public partial class FormResetBSM : Form
    {
        private IMap m_Map = null;

        /// <summary>
        /// ���õ�ͼ����
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
        /// Ĭ�Ϲ��캯��
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
                MessageHandler.ShowErrorMsg("��ѡ��ͼ�㣡", this.Text);
                return;
            }
            if (cboFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("��ѡ���ʶ���ֶΣ�", this.Text);
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
                MessageHandler.ShowInfoMsg("�ر��ʶ����ɣ�", this.Text);
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