using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 删除 插件类
    /// </summary>
    internal class DeleteObject : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DeleteObject()
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
            if (m_Tree.SelectedNode == null)
                return;
            try
            {
                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;

                if (DialogResult.OK == MessageBox.Show("确认要删除" + wrap.DataItem.Name + "?", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
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
                MessageBox.Show(ex.Message, "删除", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
