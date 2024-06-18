using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// 退出操作插件类
    /// </summary>
    public class Exit : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Exit()
        {
            base.m_caption = "退出";
            base.m_toolTip = "退出";
            base.m_name = GetType().FullName;
            base.m_message = "退出";
            base.m_category = "文件";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "Close16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "Close32.ico"));         
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
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if(MessageBox.Show("是否关闭程序?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CommonVariables.IsClosed = true;

            //关闭应用程序
            Process.GetCurrentProcess().CloseMainWindow();
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }   
    }
}
