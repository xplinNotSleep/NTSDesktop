using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 修改表结构 插件类
    /// </summary>
    internal class ModifyTableStructure : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ModifyTableStructure()
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
                if (obj is ESRI.ArcGIS.Geodatabase.ITable)
                {
                    return true;
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
            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            if (wrap == null) return;

            IObjectClass obj = wrap.GeoObject as IObjectClass;
            AG.COM.SDM.GeoDataBase.FormSchemaEdit frm = new AG.COM.SDM.GeoDataBase.FormSchemaEdit(obj);
            frm.ShowDialog();
        }
    }
}
