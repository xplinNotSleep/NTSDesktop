using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    public partial class AdvancedMessageBox : Form
    {
        /// <summary>
        /// 当前窗口的返回结果
        /// </summary>
        private AdvancedDialogResult m_Result = AdvancedDialogResult.No;

        /// <summary>
        /// 获取返回结果
        /// </summary>
        public AdvancedDialogResult DialogResult
        {
            get { return m_Result; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AdvancedMessageBox()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            m_Result = AdvancedDialogResult.Yes;
            Close();
        }

        private void btnYesAll_Click(object sender, EventArgs e)
        {
            m_Result = AdvancedDialogResult.YesAll;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            m_Result = AdvancedDialogResult.No;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_Result = AdvancedDialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 弹出提示信息窗口
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static AdvancedDialogResult Show(string Message)
        {
            return Show(Message, "");
        }

        /// <summary>
        /// 弹出提示信息窗口
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public static AdvancedDialogResult Show(string Message, string Title)
        {
            AdvancedMessageBox tAdvancedMessageBox = new AdvancedMessageBox();
            tAdvancedMessageBox.lblMessage.Text = Message;
            tAdvancedMessageBox.Text = Title;

            tAdvancedMessageBox.ShowDialog();

            return tAdvancedMessageBox.DialogResult;
        }

    }

    /// <summary>
    /// AdvancedMessageBox信息框的返回结果
    /// </summary>
    public enum AdvancedDialogResult
    {
        Yes,
        No,
        Cancel,
        YesAll
    }
}
