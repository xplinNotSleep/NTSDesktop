

using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// 部门工程管理插件类
    /// </summary>
    public sealed class DeptProManageCommand : BaseCommand
    {
        private IHookHelper m_hookHelper;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DeptProManageCommand()
        {
            m_hookHelper = new HookHelper();
            this.m_caption = "部门工程管理";
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
