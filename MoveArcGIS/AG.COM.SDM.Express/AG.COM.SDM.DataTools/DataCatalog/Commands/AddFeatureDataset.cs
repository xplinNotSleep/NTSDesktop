using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 新建要素集
    /// </summary>
    internal class AddFeatureDataset : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AddFeatureDataset()
        { 
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_Tree.SelectedNode == null) return false;
                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
                if (wrap.DataItem.Type == DataType.dtAccess)
                    return true;
                if (wrap.DataItem.Type == DataType.dtUnknown)
                    return true;
                if (wrap.DataItem.Type == DataType.dtConverage)
                    return true;
                if (wrap.DataItem.Type == DataType.dtFileGdb)
                    return true;
                if (wrap.DataItem.Type == DataType.dtSdeConnection)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            object obj = m_Tree.SelectedNode.Tag;
             DataItemWrap wrap = obj as DataItemWrap;
            IWorkspace ws = wrap.GeoObject as IWorkspace;
            if (ws == null) return;

            Manager.FormCreateDataset frm = new Manager.FormCreateDataset();
            frm.Init(ws, false);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.DatasetName != null)
                {
                    TreeNode node = new TreeNode();
                    DataItem item = new FeatureDatasetItem(frm.DatasetName);
                    node.Tag = new DataItemWrap(item, false);
                    node.Text = item.Name;
                    node.ImageIndex = m_Images.GetImageIndex(item.Type);
                    node.SelectedImageIndex = node.ImageIndex;
                    m_Tree.SelectedNode.Nodes.Insert(0, node);
                }
            }
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_Tree = (hook as DataManagerHook).TreeView;
            m_Images = (hook as DataManagerHook).Images;
        } 
    }
}
