using System.Drawing;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;


namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ���Ź��� �����
    /// </summary>
    public sealed class DeptManageCommand : BaseCommand, IUseIcon
    {
        private IHookHelper m_hookHelper;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DeptManageCommand()
        {
            m_hookHelper = new HookHelper();
            base.m_caption = "���Ź���";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G3+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G3+_32.ico"));        
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
            tFrm.ShowInTaskbar = false;
            tFrm.PrivilegeType = EnumPrivilegeType.Dept;
            tFrm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;
        }
    }
}
