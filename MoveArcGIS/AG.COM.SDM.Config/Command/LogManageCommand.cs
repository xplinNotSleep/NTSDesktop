using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 日志查看类
    /// </summary>
    public class LogManageCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_HookHelperEx = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LogManageCommand()
        {
            base.m_caption = "日志查看";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G5.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G5_32.ico"));        
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        public override void OnClick()
        {
            FormLogInfo tFrm = new FormLogInfo();
            tFrm.ShowInTaskbar = false;
            tFrm.Show(this.m_HookHelperEx.Win32Window);
        }

        /// <summary>
        /// 创建对象方法
        /// </summary>
        /// <param name="hook">对象</param>
        public override void OnCreate(object hook)
        {
            //this.//this.m_HookHelperEx.Hook = hook;   
        }
    }
}
