

using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ���Ź��̹�������
    /// </summary>
    public sealed class DeptProManageCommand : BaseCommand
    {
        private IHookHelper m_hookHelper;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DeptProManageCommand()
        {
            m_hookHelper = new HookHelper();
            this.m_caption = "���Ź��̹���";
            this.m_name = "DeptProManageCommand";
        }

        public override void OnClick()
        {
            FormPrivilegeManage tFrm = new FormPrivilegeManage();
            tFrm.ShowInTaskbar = false;
            tFrm.PrivilegeType = EnumPrivilegeType.DeptPro;
            tFrm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;
        }
    }
}
