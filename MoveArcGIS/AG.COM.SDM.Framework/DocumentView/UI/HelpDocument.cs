using AG.COM.SDM.SystemUI;
using AG.COM.SDM.SystemUI.Utility;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 帮助文件文档类
    /// </summary>
    public partial class HelpDocument : DockDocument, IHelpDocumentView
    {
        /// <summary>
        /// 初始帮助文件文档对象实例
        /// </summary>
        public HelpDocument()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示指定路径的帮助文件
        /// </summary>
        /// <param name="helpFile">指定路径的帮助文件</param>
        public void DisplayHelpFile(string helpFile)
        {
            if (System.IO.File.Exists(helpFile))
            {
                this.webBrowser1.Navigate(helpFile);
            }
            //else
            //{
            //    string strHelpFile = string.Format("{0}\\blank.mht", CommonConstString.STR_HelpPath);
            //    this.webBrowser1.Navigate(strHelpFile);
            //}
        }
    }
}
