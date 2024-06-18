using System;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    /// <summary>
    /// ������������
    /// </summary>
    public partial class frmProgeress : Form
    {
        private bool m_Abort = false;

        public frmProgeress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �������ֵ
        /// </summary>
        /// <param name="maxNum">���ֵ</param>
        public void SetMaxValue(int maxNum)
        {
            this.progressBar1.Maximum = maxNum;
        }

        /// <summary>
        /// ���õ�ǰֵ
        /// </summary>
        /// <param name="CurrentNum">��ǰֵ</param>
        public void SetCurrentVulue(int CurrentNum)
        {
            this.progressBar1.Value = CurrentNum;
        }

        /// <summary>
        /// ��ʾ��ǰ��Ϣ
        /// </summary>
        /// <param name="info">��ǰ��Ϣ</param>
        public void DisplayInfo(string info)
        {
            this.labDisplay.Text = info;
        }

        /// <summary>
        /// ��ȡ��������ֹ״̬
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