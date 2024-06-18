using System;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// ɾ����ʷ�浵 �����
    /// </summary>
    internal class DeleteHisArchive : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DeleteHisArchive()
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
                if (obj is ESRI.ArcGIS.Geodatabase.IArchivableObject)
                {
                    return ((obj as ESRI.ArcGIS.Geodatabase.IArchivableObject).IsArchiving);
                }
                else
                    return false;

            }
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            if (m_Tree.SelectedNode == null) return;

            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            object obj = wrap.GeoObject;

            try
            {
                DialogResult tDlgResult = MessageBox.Show("�Ƿ�ɾ����˶����������������ʷ�浵?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                IVersionedObject3 tVersionObject = obj as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //��ȡ�汾ע����Ϣ
                    bool isRegistered, isMovingEditsToBase;
                    tVersionObject.GetVersionRegistrationInfo(out isRegistered, out isMovingEditsToBase);

                    if (isRegistered == true && isMovingEditsToBase == false)
                    {
                        IArchivableObject tArchiveObj = obj as IArchivableObject;

                        if (tDlgResult == DialogResult.Yes)
                        {
                            //ɾ��������ص���ʷ�浵
                            tArchiveObj.DisableArchiving(true, true);
                        }
                        else if (tDlgResult == DialogResult.No)
                        {
                            //��ɾ���䱾��
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
                MessageBox.Show(ex.Message, "ɾ����ʷ�浵", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
