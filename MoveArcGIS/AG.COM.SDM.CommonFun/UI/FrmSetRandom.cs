using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.CommonFun
{
    public partial class FrmSetRandom : DockDocument
    {
        public FrmSetRandom()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value <2)
            {
                AutoCloseMsgBox.Show("最大随机数不能小于2", "提示", 2000);
                return;
            }
            FrmTrackProgress trackThread = new FrmTrackProgress();
            trackThread.Text = $"测试随机计数器{DateTime.Now.ToString("yyMMddHHmmss")}";
            Action action = () =>
            {
                TestInvoke(trackThread);
            };
            trackThread.ProAction = action;
            trackThread.Show();
            trackThread.StartTimer1();

        }

        private void TestInvoke(FrmTrackProgress frmTrack)
        {
            Action action = () =>
            {
                TestAction(frmTrack);
            };
            this.Invoke(action);
        }

        private void TestAction(FrmTrackProgress frmTrack)
        {
            #region 测试代码
            Random ran = new Random();
            int maxValue = int.Parse(numericUpDown1.Value.ToString());
            int i = ran.Next(maxValue);
            frmTrack.SubMsg = "随机数为:" + i;
            #endregion
        }


    }
}
