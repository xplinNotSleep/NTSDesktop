using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins.WindView
{
    /// <summary>
    /// ������ͼ���Ʋ���� 
    /// </summary>
    public sealed class TocView : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public TocView()
            : base()
        {
            base.m_caption = "������ͼ";
            base.m_toolTip = "������ͼ";
            base.m_name = GetType().FullName;
            base.m_category = "��ͼ����";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "CatalogTreeWindowShow16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "CatalogTreeWindowShow32.ico"));             
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// icoͼ�����16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// icoͼ�����32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// ��ȡ����ѡ��״̬
        /// </summary>
        public override bool Checked
        {
            get
            {
                bool bIsHidden = this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()).IsHidden;
                return !bIsHidden;
            }
        }

        /// <summary>
        /// ��ȡ�������״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return this.m_HookHelper.DockDocumentService.ContainsDocument(EnumDocumentType.TocDocument.ToString());
            }
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            if (this.m_checked == true)
            {
                this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()).AutoHide = true;
                m_checked = false;
                MessageBox.Show("����Ϊ�Զ�����");
            }
            else
            {
                this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()).AutoHide = false;
                m_checked = true;
                MessageBox.Show("ȡ���Զ�����");
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
