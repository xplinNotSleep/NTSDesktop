using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 创建历史存档 插件类
    /// </summary>
    internal class CreateHisArchive : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CreateHisArchive()
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
                
                IVersionedObject3 tVersionObject = obj as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //获取版本注册信息
                    bool isRegistered, isMovingEditsToBase;
                    tVersionObject.GetVersionRegistrationInfo(out isRegistered, out isMovingEditsToBase);

                    if (isRegistered == true && isMovingEditsToBase == false)
                    {
                        IArchivableObject tArchiveObject = tVersionObject as IArchivableObject;
                        if (tArchiveObject.IsArchiving == false)
                            return true;
                        else
                            return false;
                    }
                }
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
            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            object obj = wrap.GeoObject;

            try
            {
                IVersionedObject3 tVersionObject = obj as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //获取版本注册信息
                    bool isRegistered, isMovingEditsToBase;
                    tVersionObject.GetVersionRegistrationInfo(out isRegistered, out isMovingEditsToBase);

                    if (isRegistered == true && isMovingEditsToBase == false)
                    {
                        IArchivableObject tArchiveObject = tVersionObject as IArchivableObject;
                        if (tArchiveObject.IsArchiving == false)
                            tArchiveObject.EnableArchiving(null, null, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
