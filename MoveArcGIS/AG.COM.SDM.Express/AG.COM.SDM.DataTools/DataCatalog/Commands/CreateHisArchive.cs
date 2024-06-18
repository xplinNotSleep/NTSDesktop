using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// ������ʷ�浵 �����
    /// </summary>
    internal class CreateHisArchive : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CreateHisArchive()
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
                
                IVersionedObject3 tVersionObject = obj as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //��ȡ�汾ע����Ϣ
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
            if (m_Tree.SelectedNode == null)
                return;
            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            object obj = wrap.GeoObject;

            try
            {
                IVersionedObject3 tVersionObject = obj as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //��ȡ�汾ע����Ϣ
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
                MessageBox.Show(ex.Message, "ɾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
