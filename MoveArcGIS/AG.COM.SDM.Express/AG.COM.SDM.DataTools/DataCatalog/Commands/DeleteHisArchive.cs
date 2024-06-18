using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 删除历史存档 插件类
    /// </summary>
    internal class DeleteHisArchive : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DeleteHisArchive()
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
                if (obj is ESRI.ArcGIS.Geodatabase.IArchivableObject)
                {
                    return ((obj as ESRI.ArcGIS.Geodatabase.IArchivableObject).IsArchiving);
                }
                else
                    return false;

            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (m_Tree.SelectedNode == null) return;

            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            object obj = wrap.GeoObject;

            try
            {
                DialogResult tDlgResult = MessageBox.Show("是否删除与此对象相关联的所有历史存档?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                IVersionedObject3 tVersionObject = obj as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //获取版本注册信息
                    bool isRegistered, isMovingEditsToBase;
                    tVersionObject.GetVersionRegistrationInfo(out isRegistered, out isMovingEditsToBase);

                    if (isRegistered == true && isMovingEditsToBase == false)
                    {
                        IArchivableObject tArchiveObj = obj as IArchivableObject;

                        if (tDlgResult == DialogResult.Yes)
                        {
                            //删除所有相关的历史存档
                            tArchiveObj.DisableArchiving(true, true);
                        }
                        else if (tDlgResult == DialogResult.No)
                        {
                            //仅删除其本身
                            tArchiveObj.DisableArchiving(true, false);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除历史存档", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
