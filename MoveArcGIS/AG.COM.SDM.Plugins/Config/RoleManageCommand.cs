using System.Drawing;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;


namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ��ɫ��������
    /// </summary>
    public sealed class RoleManageCommand : BaseCommand, IUseIcon
    {
        private IHookHelper m_hookHelper = new HookHelper();

        /// <summary>
        /// Ĭ�Ϲ��캯�� 
        /// </summary>
        public RoleManageCommand()
        {
            m_hookHelper = new HookHelper();
            base.m_caption = "��ɫ����";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G2.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G2_32.ico"));       
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

        public override void OnClick()
        {
            FormPrivilegeManage tFrm = new FormPrivilegeManage();
            tFrm.HookHelper = m_hookHelper;
            tFrm.ShowInTaskbar = false;
            tFrm.PrivilegeType = EnumPrivilegeType.Role;
            tFrm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;
        }
    }
}
