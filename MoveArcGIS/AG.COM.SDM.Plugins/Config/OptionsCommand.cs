using System.Drawing;
using AG.COM.SDM.Config.DbConnUI;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;


namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ϵͳѡ����
    /// </summary>
    public sealed class OptionsCommand : BaseCommand, IUseIcon
    {
        private IHookHelper m_hookHelper;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public OptionsCommand()
        {             
            m_hookHelper = new HookHelper();
            this.m_caption = "ϵͳѡ��";
            base.m_name = GetType().FullName;
            this.m_message = "ϵͳѡ��";
            this.m_toolTip = "ϵͳѡ��";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G6.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G6_32.ico"));      
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
        /// ��ȡ��ǰ����״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// ����ʱ�ĳ�ʼ��ֵ
        /// </summary>
        /// <param name="hook">���ݶ���</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;    
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        public override void OnClick()
        {
            FrmDbOptions frm = new FrmDbOptions(true);
            frm.ShowInTaskbar = false;
            frm.Show();
            //frm.ShowDialog();
        }
    }
}
