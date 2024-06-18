using System.Windows.Forms;
using AG.COM.SDM.Catalog.DataItems;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// ˢ�� �����
    /// </summary>
    internal class RefreshObject : BaseCommand
    {
        private TreeView m_Tree = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public RefreshObject()
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
                    return true;
                else if (obj is ESRI.ArcGIS.Geodatabase.IWorkspace)
                    return true;
                else
                    return false;

            }
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="hook"></param>
        public override void OnCreate(object hook)
        {
            m_Tree = (hook as DataManagerHook).TreeView;
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            DataItem dataItem = new DatabaseWorkspaceItem(Utility.CommonVariables.DatabaseConfig.Workspace);
            
        }

    }
}
