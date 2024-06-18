using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// 系统注销插件类
    /// </summary>
    public sealed class ReLogin : BaseCommand, IUseIcon 
    {
        IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ReLogin()
        {
            base.m_caption = "注销";
            base.m_toolTip = "注销";
            base.m_name = GetType().FullName;
            base.m_message = "注销";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "ReLogin.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "ReLogin_32.ico")); 
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
            if(MessageBox.Show("是否重新启动?","提示",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
            {
                return;
            }

            string strUserFile = CommonConstString.STR_ConfigPath + "\\Restart.txt";

            using (StreamWriter sw = File.CreateText(strUserFile))
            {
                sw.WriteLine("1");
            }

            CommonVariables.IsClosed = true;

            Application.Restart();         
        }

        /// <summary>
        /// 创建对象方法
        /// </summary>
        /// <param name="hook">对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
