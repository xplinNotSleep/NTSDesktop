using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    public partial class FrmTrackNoPause : Form
    {
        public FrmTrackNoPause()
        {
            InitializeComponent();
        }

        public Action ProAction
        {
            get;set;
        }

        /// <summary>
        /// 显示具体进度情况
        /// </summary>
        public string SubMsg
        {
            get
            {
                return label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        /// <summary>
        /// 显示总进度情况
        /// </summary>
        public string TotalMsg
        {
            get
            {
                return label2.Text;
            }
            set
            {
                this.label2.Text = value;
            }
        }

        /// <summary>
        /// 设置总进度条当前显示值
        /// </summary>
        public int ProValue
        {
            get
            {
                return this.progressBar1.Value;
            }
            set
            {
                if (value <= progressBar1.Maximum && value >= progressBar1.Minimum)
                {
                    this.progressBar1.Value = value;
                }
            }
        }

        public bool IsContinue
        {
            get;set;
        }

        public bool IsBreak
        {
            get;set;
        }

        public int STACount = 0;
        public int MaxCount = 0;

        private void btnEnd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否终止操作?", "提示",
                MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                IsBreak = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Task.Run(ProAction);
        }

        private void FrmTrackNoPause_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 定义一个可暂停线程的方法
        /// </summary>
        /// <returns></returns>
        public static ManualResetEvent CanStopTrack(FrmTrackNoPause FrmTrackNoPause,
            Action action)
        {
            var resetEvent = new ManualResetEvent(false);
            var count = 0;
            Task.Run(action);

            return resetEvent;

        }
    }
}
