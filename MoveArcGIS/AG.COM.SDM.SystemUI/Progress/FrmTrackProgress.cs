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
    public partial class FrmTrackProgress : Form
    {
        public FrmTrackProgress()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;//是否检查线程间操作
        }

        private Action Action;
        private ManualResetEvent ResetEvent;
        private CancellationTokenSource TokenSource;

        private bool IsUseTimer = false;

        /// <summary>
        /// 外部传入的方法
        /// </summary>
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

        /// <summary>
        /// 设置总进度条最大值
        /// </summary>
        public int MaxValue
        {
            get
            {
                return this.progressBar1.Maximum;
            }
            set
            {
                this.progressBar1.Maximum = value;
            }
        }

        /// <summary>
        /// 是否暂停
        /// </summary>
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!IsContinue)
            {
                if(IsUseTimer)
                {
                    timer1.Stop();
                }
                else
                {
                    ResetEvent.Reset();
                }
                IsContinue = true;
                btnStop.Text = "继续操作";
            }
            else
            {
                IsContinue = false;
                btnStop.Text = "暂停操作";
                if (IsUseTimer)
                {
                    timer1.Start();
                }
                else
                {
                    ResetEvent.Set();
                }
                
            }

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (!IsContinue)
            {
                if (IsUseTimer)
                {
                    timer1.Stop();
                }
                else
                {
                    ResetEvent.Reset();
                }
                IsContinue = true;
                btnStop.Text = "继续操作";

            }
            DialogResult result = MessageBox.Show("是否终止操作?", "提示",
                MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                IsBreak = true;
                if (IsUseTimer)
                {
                    timer1.Stop();
                }
                else
                {
                    ResetEvent.Set();
                    TokenSource.Cancel();
                }
                //timer1.Enabled = false;
                this.Close();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Task.Run(ProAction);
        }

        private void FrmTrackProgress_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 开始计时器1
        /// </summary>
        public void StartTimer1()
        {
            IsUseTimer = true;
            timer1.Start();
        }

        public void StartAction(Action action, ManualResetEvent resetEvent,
            CancellationTokenSource tokenSource)
        {
            ResetEvent = resetEvent;
            Action = action;
            TokenSource = tokenSource;
            //cancellationToken = new CancellationTokenSource();
            Task.Factory.StartNew(Action, tokenSource.Token);

        }

        public void StartAction(Action action, ManualResetEvent resetEvent)
        {
            ResetEvent = resetEvent;
            Action = action;
            //cancellationToken = new CancellationTokenSource();
            Task.Factory.StartNew(Action);

        }

        /// <summary>
        /// 定义一个可暂停线程的方法
        /// </summary>
        /// <returns></returns>
        public static ManualResetEvent CanStopTrack(FrmTrackProgress frmTrackProgress,
            Action action)
        {
            var resetEvent = new ManualResetEvent(false);
            var count = 0;
            Task.Run(action);

            return resetEvent;

        }
    }
}
