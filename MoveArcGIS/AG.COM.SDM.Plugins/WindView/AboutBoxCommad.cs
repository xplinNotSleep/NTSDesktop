using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins.WindView
{
    /// <summary>
    /// 关于对话框架插件类
    /// </summary>
    public class AboutBoxCommad : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AboutBoxCommad():base()
        {
            base.m_caption = "关于(&A)";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "about16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "about32.ico"));        
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
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            AboutBox tFromAboutBox = new AboutBox();
            tFromAboutBox.ShowDialog();
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
