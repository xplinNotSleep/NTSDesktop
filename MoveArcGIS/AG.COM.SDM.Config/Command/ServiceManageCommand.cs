using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 服务管理 插件类
    /// </summary>
    public sealed class ServiceManageCommand : BaseCommand
    {
        private IHookHelperEx m_HookHelperEx;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ServiceManageCommand()
        {
            base.m_caption = "服务管理";
            base.m_name = "ServiceManageCommand";
        }

        public override void OnClick()
        {
            FormServiceManage tFrm = new FormServiceManage();
            tFrm.ShowInTaskbar = false;
            tFrm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            tFrm.ShowDialog(m_HookHelperEx.Win32Window);
        }

        public override void OnCreate(object hook)
        {
            m_HookHelperEx = new HookHelperEx();
            ////this.m_HookHelperEx.Hook = hook;
        }
    }
}
