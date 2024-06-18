using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 注册版本 插件类
    /// </summary>
    internal class RegisterVersion : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RegisterVersion()
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
                if (obj is ESRI.ArcGIS.Geodatabase.IVersionedObject)
                {
                    return ((obj as ESRI.ArcGIS.Geodatabase.IVersionedObject).IsRegisteredAsVersioned == false);
                }
                else
                    return false;

            }
        }

        /// <summary>
        /// 创建时处理方法
        /// </summary>
        /// <param name="hook"></param>
        public override void OnCreate(object hook)
        {
            m_Tree = (hook as DataManagerHook).TreeView;
            m_Images = (hook as DataManagerHook).Images;
        }

        /// <summary>
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            if (m_Tree.SelectedNode == null)
                return;
            try
            {
                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
                object obj = wrap.GeoObject;
                if (obj is ESRI.ArcGIS.Geodatabase.IVersionedObject)
                {
                    (obj as ESRI.ArcGIS.Geodatabase.IVersionedObject).RegisterAsVersioned(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
