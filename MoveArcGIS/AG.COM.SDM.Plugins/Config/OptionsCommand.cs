using System.Drawing;
using AG.COM.SDM.Config.DbConnUI;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;


namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// 系统选项类
    /// </summary>
    public sealed class OptionsCommand : BaseCommand, IUseIcon
    {
        private IHookHelper m_hookHelper;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public OptionsCommand()
        {             
            m_hookHelper = new HookHelper();
            this.m_caption = "系统选项";
            base.m_name = GetType().FullName;
            this.m_message = "系统选项";
            this.m_toolTip = "系统选项";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G6.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G6_32.ico"));      
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
        /// 获取当前可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 创建时的初始赋值
        /// </summary>
        /// <param name="hook">传递对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;    
        }

        /// <summary>
        /// 单击事件
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
