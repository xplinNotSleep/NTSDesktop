using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    public partial class FormLoadWaiting : Form
    {
        public delegate void LoadWaitingStop();

        /// <summary>
        /// 是否显示停止按钮
        /// </summary>
        public bool ShowStopButton
        {
            get { return btnStop.Visible; }
            set { btnStop.Visible = value; }
        }

        /// <summary>
        /// 点击停止事件
        /// </summary>
        public LoadWaitingStop LoadWaitingStopEvent;

        public FormLoadWaiting()
        {
            InitializeComponent();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (LoadWaitingStopEvent != null)
            {
                LoadWaitingStopEvent();
            }
        }
    }
}
