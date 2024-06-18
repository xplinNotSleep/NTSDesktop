using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// �޸ı�ṹ �����
    /// </summary>
    internal class ModifyTableStructure : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ModifyTableStructure()
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
                if (obj is ESRI.ArcGIS.Geodatabase.ITable)
                {
                    return true;
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
            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            if (wrap == null) return;

            IObjectClass obj = wrap.GeoObject as IObjectClass;
            AG.COM.SDM.GeoDataBase.FormSchemaEdit frm = new AG.COM.SDM.GeoDataBase.FormSchemaEdit(obj);
            frm.ShowDialog();
        }
    }
}
