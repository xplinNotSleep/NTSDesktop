using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// 操作帮助插件类
    /// </summary>
    public class HelpCommand : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();

        private readonly string m_HelpFileName = "空间数据管理系统用户操作手册V1.0.chm";

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public HelpCommand()
        {
            base.m_caption = "操作说明";
            base.m_toolTip = "操作说明";
            base.m_name = GetType().FullName;
            base.m_message = "操作说明";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "helpBook16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "helpBook32.ico"));      
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
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            try
            {
                string helpFilePath = Application.StartupPath + "\\" + m_HelpFileName;
                ///检查文件是否存在，文件在系统的根目录，文件名在本类的变量m_HelpFileName记录
                if (!File.Exists(helpFilePath))
                {
                    MessageBox.Show("操作说明文件" + helpFilePath + "不存在。");
                    return;
                }
                System.Diagnostics.Process.Start(helpFilePath);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show("操作失败。原因是：" + ex.Message);
            }
        }
    }
}
