using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �������Ի���
    /// </summary>
    public partial class TrackProgressDialog : Form, ITrackProgress
    {
        private bool m_IsDisplayTotal = false;
        private bool m_IsContinue = true;
        private bool m_FinishClose = true;
        private bool m_btnShow = true;
        private bool m_IsMoveUp = false;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// ��ȡ�ӽ�����
        /// </summary>
        public ProgressBar SubTarckProgressBar
        {
            get { return progbarSub; }
        }

        /// <summary>
        /// ��ȡ�ӽ���˵������
        /// </summary>
        public Label SubTarckLabel
        {
            get { return lblSubtip; }
        }

        /// <summary>
        /// ���ư�ť������
        /// </summary>
        /// <returns></returns>
        public bool btnShow
        {
            set
            {
                this.m_btnShow = value;
            }
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public TrackProgressDialog()
        {
            //��ʼ���������
            InitializeComponent();
        }

        private void TrackProgressDialog_Load(object sender, EventArgs e)
        {                   
            if (this.m_btnShow == false)
            {
                //this.panel2.Dock = DockStyle.None;
                this.panel2.Visible = false;
            }
            if (this.m_IsDisplayTotal == false)
            {
                this.panelTotal.Visible = false;
                this.Height = this.Height - this.panelTotal.Height + 10;
            }
        }

        #region  ITrackProgress�ӿ�
        /// <summary>
        /// �Ƿ��������,������򷵻� true,���򷵻� false
        /// </summary>
        public bool IsContinue
        {
            get
            {
                return this.m_IsContinue;
            }
        }

        /// <summary>
        /// �����Ƿ���ʾ�ܽ����� 
        /// true ��ʾ false ����ʾ
        /// </summary>
        public bool DisplayTotal
        {
            set
            {
                this.m_IsDisplayTotal = value;
            }
        }

        /// <summary>
        /// ��ʶ����ɴ������
        /// </summary>
        public void SetFinish()
        {
            if (this.m_FinishClose == true)
            {
                this.Close();
            }
            else
            {
                this.btnCancel.Enabled = false;
                //this.btnClose.Enabled = true;
            }
        }

        /// <summary>
        /// ��ȡ��������ɺ��Ƿ��Զ��رմ���
        /// </summary>
        public bool AutoFinishClose
        {
            get
            {
                return this.m_FinishClose;
            }
            set
            {
                this.m_FinishClose = value;
            }
        }

        /// <summary>
        /// ���ñ�������Ϣ
        /// </summary>
        public string Title
        {
            set
            {
                this.Title = value;
            }
        }

        /// <summary>
        /// �����ܽ�������ʾ��Χ����Сֵ
        /// </summary>
        public int TotalMin
        {
            get
            {
                return this.progBarTotal.Minimum;
            }
            set
            {
                this.progBarTotal.Minimum = value;
            }
        }

        /// <summary>
        /// �����ܽ�������ʾ��Χ�����ֵ
        /// </summary>
        public int TotalMax
        {
            get
            {
                return this.progBarTotal.Maximum;
            }
            set
            {
                this.progBarTotal.Maximum = value;
            }
        }

        /// <summary>
        /// �����ܽ�������ǰ��ʾֵ
        /// </summary>
        public int TotalValue
        {
            get
            {
                return this.progBarTotal.Value;
            }
            set
            {
                if (value <= progBarTotal.Maximum && value >= progBarTotal.Minimum)
                {
                    this.progBarTotal.Value = value;
                }
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�ܽ�����ʾ�ٷֱ�
        /// </summary>
        public int TotalPercent
        {
            get
            {
                double percent = this.progBarTotal.Value * 100.0 / this.progBarTotal.Maximum;
                return Convert.ToInt32(Math.Floor(percent));
            }
        }

        /// <summary>
        /// �����ܽ�����ʾ��Ϣ
        /// </summary>
        public string TotalMessage
        {
            set
            {
                this.lblTotaltip.Text = value;
            }
        }

        /// <summary>
        /// �����ӽ�������ʾ��Χ����Сֵ
        /// </summary>
        public int SubMin
        {
            get
            {
                return this.progbarSub.Minimum;
            }
            set
            {
                this.progbarSub.Minimum = value;
            }
        }

        /// <summary>
        /// �����ӽ�������ʾ��Χ�����ֵ
        /// </summary>
        public int SubMax
        {
            get
            {
                return this.progbarSub.Maximum;
            }
            set
            {
                this.progbarSub.Maximum = value;
            }
        }

        /// <summary>
        /// �����ӽ�������ǰ��ʾֵ
        /// </summary>
        public int SubValue
        {
            get
            {
                return this.progbarSub.Value;
            }
            set
            {
                if (value <= progbarSub.Maximum && value >= progbarSub.Minimum)
                {
                    this.progbarSub.Value = value;
                }
            }
        }

        /// <summary>
        /// ��ȡ�ӽ�����ʾ�ٷֱ�
        /// </summary>
        public int SubPercent
        {
            get
            {
                double percent = (this.progbarSub.Value * 1.0 / this.progbarSub.Maximum) * 100;
                return Convert.ToInt32(Math.Floor(percent));
            }
        }

        /// <summary>
        /// �����ӽ�����ʾ��Ϣ
        /// </summary>
        public string SubMessage
        {
            set
            {
                this.lblSubtip.Text = value;
            }
        }
        #endregion

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.m_IsContinue = false;
            //this.btnClose.Enabled = true;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.m_IsContinue = false;
            this.Close();
        }

        public void Show()
        {
            base.Show();

            //ֻ�е�һ��show��Ҫ�ƶ�
            if (m_IsMoveUp == false)
            {
                //Ϊ��ֹ������������Messagebox��ס����˰ѽ��������130����
                this.Location = new Point(this.Location.X, this.Location.Y - 130);

                m_IsMoveUp = true;
            }
        }
    }
}