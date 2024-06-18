using System;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    /// <summary>
    /// 进度条窗体类
    /// </summary>
    public partial class frmProgeress : Form
    {
        private bool m_Abort = false;

        public frmProgeress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <param name="maxNum">最大值</param>
        public void SetMaxValue(int maxNum)
        {
            this.progressBar1.Maximum = maxNum;
        }

        /// <summary>
        /// 设置当前值
        /// </summary>
        /// <param name="CurrentNum">当前值</param>
        public void SetCurrentVulue(int CurrentNum)
        {
            this.progressBar1.Value = CurrentNum;
        }

        /// <summary>
        /// 显示当前信息
        /// </summary>
        /// <param name="info">当前信息</param>
        public void DisplayInfo(string info)
        {
            this.labDisplay.Text = info;
        }

        /// <summary>
        /// 获取或设置终止状态
        /// </summary>
        public bool Abort
        {
            get
            {
                return m_Abort;
            }
            set
            {
                m_Abort = value;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            m_Abort = true;
        }

    }
}