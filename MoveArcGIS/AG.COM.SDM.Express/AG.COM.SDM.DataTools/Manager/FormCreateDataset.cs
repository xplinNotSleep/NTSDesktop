using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// ����Ҫ�ؼ� ������
    /// </summary>
    public partial class FormCreateDataset : Form
    {
        private IFeatureDatasetName m_FeatureDatasetName = null;

        /// <summary>
        /// ��ȡIFeatureDatasetName
        /// <see cref="IFeatureDatasetName"/>
        /// </summary>
        public IFeatureDatasetName DatasetName
        {
            get { return m_FeatureDatasetName; }
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormCreateDataset()
        {
            InitializeComponent();
        }

        private void btSelectSpatialRef_Click(object sender, EventArgs e)
        {
            try
            {
                AG.COM.SDM.GeoDataBase.SpatialReferenceDialog dlg = new AG.COM.SDM.GeoDataBase.SpatialReferenceDialog();
                dlg.SpatialReference = this.txtSpatialRef.Tag as ISpatialReference;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtSpatialRef.Tag = dlg.SpatialReference;
                    txtSpatialRef.Text = dlg.SpatialReference.Name;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
        }

        private void btOpenWorkspace_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser frm = new AG.COM.SDM.Catalog.FormDataBrowser();
            frm.AddFilter(new AG.COM.SDM.Catalog.Filters.SDEWorkspaceFilter());
            frm.AddFilter(new AG.COM.SDM.Catalog.Filters.PersonalGeoDatabaseFilter());
            frm.AddFilter(new AG.COM.SDM.Catalog.Filters.FileGeoDatabaseFilter());
            frm.Text = "ѡ�����ռ�";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<AG.COM.SDM.Catalog.DataItems.DataItem> items = frm.SelectedItems;
                if (items.Count == 0) return;

                object obj = items[0].GetGeoObject();
                if (obj != null)
                {
                    if (obj is IWorkspace)
                    {
                        txtWorkspace.Text = (obj as IWorkspace).PathName;

                        txtWorkspace.Tag = obj;
                    }

                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (txtWorkspace.Tag == null)
            {
                MessageHandler.ShowErrorMsg("��ѡ��Ҫ����Ҫ�ؼ���λ�ã�", this.Text);
                return;
            }

            if (txtName.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("Ҫ�������Ʋ���Ϊ�գ�", this.Text);
                return;
            }

            if (txtSpatialRef.Tag == null)
            {
                MessageHandler.ShowErrorMsg("�ռ�ο�����Ϊ�գ�", this.Text);
                return;
            }

            IWorkspace ws = this.txtWorkspace.Tag as IWorkspace;
            string dsname = txtName.Text.Trim();
            ISpatialReference spatialRef = txtSpatialRef.Tag as ISpatialReference;

            try
            {
                IFeatureDataset ds = (ws as IFeatureWorkspace).CreateFeatureDataset(dsname, spatialRef);
                m_FeatureDatasetName = ds.FullName as IFeatureDatasetName;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                m_FeatureDatasetName = null;
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }

        }

        /// <summary>
        /// λ�ó�ʼ��
        /// </summary>
        /// <param name="ws">�����ռ�</param>
        /// <param name="canSelect">�Ƿ������ѡ��</param>
        public void Init(IWorkspace ws, bool canSelect)
        {
            txtWorkspace.Text = ws.PathName;
            txtWorkspace.Tag = ws;
            btOpenWorkspace.Visible = canSelect;
        }
    }
}