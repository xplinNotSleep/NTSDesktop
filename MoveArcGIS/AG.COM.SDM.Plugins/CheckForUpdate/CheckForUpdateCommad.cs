using System.Drawing;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins.CheckForUpdate
{
    /// <summary>
    /// ���ڶԻ���ܲ����
    /// </summary>
    public class CheckForUpdate : BaseCommand, IUseIcon
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CheckForUpdate():base()
        {
            base.m_caption = "����°汾";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "about16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "about32.ico"));        
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
        /// ��ȡ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            CheckForUpdateForm form = new CheckForUpdateForm();
            form.ShowDialog();
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
        }
    }
}
