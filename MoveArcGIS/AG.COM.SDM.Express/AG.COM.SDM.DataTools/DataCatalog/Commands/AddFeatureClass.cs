using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 新建要素类
    /// </summary>
    internal class AddFeatureClass : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AddFeatureClass()
        {
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_Tree.SelectedNode == null)
                    return true;
                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
                if (wrap.DataItem.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFeatureDataset)
                    return true;
                if (wrap.DataItem.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtSdeConnection)
                    return true;
                if (wrap.DataItem.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtAccess)
                    return true;
                if (wrap.DataItem.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFileGdb)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// 创建时处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_Tree = (hook as DataManagerHook).TreeView;
            m_Images = (hook as DataManagerHook).Images;
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            object obj = m_Tree.SelectedNode.Tag;
            if (obj is DataItemWrap)   obj = (obj as DataItemWrap).GeoObject;

            Manager.FormCreateFeatureClass frm = new Manager.FormCreateFeatureClass();
            frm.Init(obj, false);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.FeatureClassName != null)
                {
                    TreeNode node = new TreeNode();
                    DataItem item = new FeatureClassItem(frm.FeatureClassName);
                    node.Tag = new DataItemWrap(item, false);
                    node.Text = item.Name;
                    node.ImageIndex = m_Images.GetImageIndex(item.Type);
                    node.SelectedImageIndex = node.ImageIndex;
                    m_Tree.SelectedNode.Nodes.Add(node);
                }
            }
        }
    }
}
