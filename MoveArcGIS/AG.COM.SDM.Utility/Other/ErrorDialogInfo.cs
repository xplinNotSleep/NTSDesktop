using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Common
{
    /// <summary>
    /// ������Ϣ��ʾ����
    /// </summary>
    internal partial class ErrorDialogInfo : Form
    {
        private Exception m_exception;          //�쳣��Ϣ��

        /// <summary>
        /// ��ʼ��������Ϣ��ʾ�������
        /// </summary>
        /// <param name="ex">�쳣��Ϣ��</param>
        public ErrorDialogInfo(Exception ex)
        {
            InitializeComponent();
            this.m_exception = ex;
        }

        /// <summary>
        /// ��ʼ��������Ϣ��ʾ�������
        /// </summary>
        /// <param name="info">������Ϣ</param>
        /// <param name="ex">�쳣��Ϣ��<see cref="Exception"/></param>
        public ErrorDialogInfo(string info, Exception ex)
        {
            InitializeComponent();

            this.lblInfo.Text = info;
            this.m_exception = ex;
        }

        /// <summary>
        /// ��ʼ��������Ϣ��ʾ�������
        /// </summary>
        /// <param name="info">������Ϣ</param>
        /// <param name="title">������Ϣ</param>
        /// <param name="ex">�쳣��Ϣ��<see cref="Exception"/></param>
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