using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    public class FormMessageFilter : IMessageFilter
    {
        static Timer timer;
        static int timeCount = 0;

        public FormMessageFilter(Timer pTimer)
        {
            timer = pTimer;
        }

        public bool PreFilterMessage(ref Message m)
        {
            //如果检测到有鼠标或则键盘的消息，则使计数为0.....
            if (m.Msg == 0x0200 || m.Msg == 0x0201 || m.Msg == 0x0204 || m.Msg == 0x0207)
            {
                timer.Stop();
                timeCount = 0;
            }
            else
            {
                timer.Interval = 1000;
                timer.Start();
            }
            return false;
        }

    }
}
