using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// ɾ�� �����
    /// </summary>
    internal class DeleteObject : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DeleteObject()
        {

        }

        /// <summary>
        /// ��ȡ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_Tree.SelectedNode == null)
                    return false;
                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
                object obj = wrap.GeoObject;
                if (obj is ESRI.ArcGIS.Geodatabase.IDataset)
                {
                    return (obj as ESRI.ArcGIS.Geodatabase.IDataset).CanDelete();
                }
                else
                    return false;

            }
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            m_Tree = (hook as DataManagerHook).TreeView;
            m_Images = (hook as DataManagerHook).Images;
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            if (m_Tree.SelectedNode == null)
                return;
            try
            {
                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;

                if (DialogResult.OK == MessageBox.Show("ȷ��Ҫɾ��" + wrap.DataItem.Name + "?", "ɾ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    object obj = wrap.GeoObject;
                    if (obj is ESRI.ArcGIS.Geodatabase.IDataset)
                    {
                        (obj as ESRI.ArcGIS.Geodatabase.IDataset).Delete();
                    }
                    m_Tree.SelectedNode.Remove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ɾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
