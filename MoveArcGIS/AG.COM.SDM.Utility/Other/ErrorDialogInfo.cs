using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Common
{
    /// <summary>
    /// 错误信息显示窗体
    /// </summary>
    internal partial class ErrorDialogInfo : Form
    {
        private Exception m_exception;          //异常信息类

        /// <summary>
        /// 初始化错误信息显示窗体对象
        /// </summary>
        /// <param name="ex">异常信息类</param>
        public ErrorDialogInfo(Exception ex)
        {
            InitializeComponent();
            this.m_exception = ex;
        }

        /// <summary>
        /// 初始化错误信息显示窗体对象
        /// </summary>
        /// <param name="info">错误信息</param>
        /// <param name="ex">异常信息类<see cref="Exception"/></param>
        public ErrorDialogInfo(string info, Exception ex)
        {
            InitializeComponent();

            this.lblInfo.Text = info;
            this.m_exception = ex;
        }

        /// <summary>
        /// 初始化错误信息显示窗体对象
        /// </summary>
        /// <param name="info">错误信息</param>
        /// <param name="title">标题信息</param>
        /// <param name="ex">异常信息类<see cref="Exception"/></param>
        public ErrorDialogInfo(string info, string title, Exception ex)
        {
            InitializeComponent();

            this.lblInfo.Text = info;
            this.Text = title;
            this.m_exception = ex;
        }

        private void ErrorDialogInfo_Load(object sender, EventArgs e)
        {
            this.txtMessage.Text = this.m_exception.Message;
            this.txtStackTrace.Text = this.m_exception.StackTrace;
            this.txtSource.Text = this.m_exception.Source;
        }
    }
}