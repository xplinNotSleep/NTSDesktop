using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 地图鼠标滚轮消息筛选器
    /// </summary>
    internal class MainMessageFilter : IMessageFilter
    {
        public event MouseEventHandler OnWheelForward;
        public event MouseEventHandler OnWheelBackward;
        private IntPtr m_FilterWindows;

        /// <summary>
        /// 初始化消息筛选器实例对象
        /// </summary>
        /// <param name="hwnd">窗口或特定对象的句柄</param>
        public MainMessageFilter(IntPtr hwnd)
        {
            this.m_FilterWindows = hwnd;
        }

        #region IMessageFilter 成员

        /// <summary>
        /// 在调度消息之前将其筛选出来。
        /// </summary>
        /// <param name="m">要调度的消息。无法修改此消息。</param>
        /// <returns>如果筛选消息并禁止消息被调度，则为 true；如果允许消息继续到达下一个筛选器或控件，则为 false</returns>
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            //如果当前消息对象句柄为需要筛选的对象句柄，同时消息值为鼠标滚轮值时
            if (this.m_FilterWindows.Equals(m.HWnd) && m.Msg == 0x020A)
            {
                //获取鼠标滚轮值
                Byte[] byts = System.BitConverter.GetBytes(m.WParam.ToInt32());
                int zDelta = BitConverter.ToInt16(byts, 2);

                //鼠标相对于显示屏的位置
                uint lparm = (uint)m.LParam;

                //实例化鼠标事件参数
                MouseEventArgs e = new MouseEventArgs(MouseButtons.Middle, 1, (int)((UInt16)lparm), (int)(lparm >> 16), zDelta);

                //触发鼠标滚轮事件
                if (zDelta > 0)
                {
                    if (OnWheelForward != null)
                        OnWheelForward(m.HWnd, e);
                }
                else
                {
                    if (OnWheelBackward != null)
                        OnWheelBackward(m.HWnd, e);
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}
