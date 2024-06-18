using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 加载中等待窗口
    /// </summary>
    public class LoadWaiting
    {
        private FormLoadWaiting m_Form = null;

        private Form m_FormParent = null;
        /// <summary>
        /// 父form，用于计算等待窗口大小
        /// </summary>
        public Form FormParent
        {
            set
            {
                m_FormParent = value;
            }
        }

        private Thread m_Thread = null;

        private bool m_Cancel = false;
        /// <summary>
        /// 获取是否在界面上点击停止
        /// </summary>
        public bool Cancel
        {
            get { return m_Cancel; }
        }

        private bool m_ShowStopButton = false;
        /// <summary>
        /// 显示停止按钮
        /// </summary>
        public bool ShowStopButton
        {
            get { return m_ShowStopButton; }
            set { m_ShowStopButton = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tFormParent">父form，用于计算等待窗口大小</param>
        public LoadWaiting(Form tFormParent)
        {
            m_FormParent = tFormParent;
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        public void Show()
        {
            m_Thread = new Thread(ThreadFuntion);
            m_Thread.Start();
            Thread.Sleep(500);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public void Close()
        {
            if (m_Thread != null)
            {
                m_Thread.Abort();
                m_Thread = null;
            }
        }

        private void ThreadFuntion()
        {
            m_Form = new FormLoadWaiting();
            m_Form.LoadWaitingStopEvent += new FormLoadWaiting.LoadWaitingStop(StopEvent);
            m_Form.ShowStopButton = m_ShowStopButton;
            if (m_FormParent != null)
            {
                //等待窗口的大小与父窗口一样
                m_Form.Location = new Point(m_FormParent.Location.X, m_FormParent.Location.Y);
                m_Form.Size = new Size(m_FormParent.Size.Width, m_FormParent.Size.Height);
            }
            else
            {
                m_Form.WindowState = FormWindowState.Maximized;
            }
            m_Form.ShowDialog();
        }

        /// <summary>
        /// loading窗点击停止后触发事件
        /// </summary>
        private void StopEvent()
        {
            m_Cancel = true;
        }
    }
}
