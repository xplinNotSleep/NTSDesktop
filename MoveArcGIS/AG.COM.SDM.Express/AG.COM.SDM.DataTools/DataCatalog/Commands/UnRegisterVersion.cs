using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.Catalog;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 反注册版本 插件类
    /// </summary>
    internal class UnRegisterVersion : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UnRegisterVersion()
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
                    return (obj as ESRI.ArcGIS.Geodatabase.IVersionedObject).IsRegisteredAsVersioned;
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
                object obj = wrap.GeoObject;
                if (obj is ESRI.ArcGIS.Geodatabase.IVersionedObject)
                {
                    (obj as ESRI.ArcGIS.Geodatabase.IVersionedObject).RegisterAsVersioned(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
