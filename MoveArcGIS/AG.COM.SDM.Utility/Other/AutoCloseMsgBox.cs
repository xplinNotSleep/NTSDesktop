using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    public class AutoCloseMsgBox
    {
        static string _caption;

        public static void Show(string text, string caption, int timeout)
        {
            _caption = caption;
            StartTimer(timeout);
            MessageBox.Show(text, _caption);
        }

        private static void StartTimer(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Enabled = true;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = FindWindow(null, _caption);
            if(ptr != IntPtr.Zero)
            {
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            ((Timer)sender).Enabled = false;
            
        }
        

        const int WM_CLOSE = 0x0010;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);


    }
}
