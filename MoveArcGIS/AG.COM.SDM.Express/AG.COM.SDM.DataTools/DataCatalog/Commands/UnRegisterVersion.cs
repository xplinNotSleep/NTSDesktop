using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.Catalog;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// ��ע��汾 �����
    /// </summary>
    internal class UnRegisterVersion : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public UnRegisterVersion()
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
                if (obj is ESRI.ArcGIS.Geodatabase.IVersionedObject)
                {
                    return (obj as ESRI.ArcGIS.Geodatabase.IVersionedObject).IsRegisteredAsVersioned;
                }
                else
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
                MessageBox.Show(ex.Message, "ɾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
