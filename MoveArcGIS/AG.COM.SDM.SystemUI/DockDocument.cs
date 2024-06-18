using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 可停靠窗体的基类
    /// </summary>
    public partial class DockDocument : UserControl
    {
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event EventHandler FormClosed;

        /// <summary>
        /// 关闭事件
        /// </summary>
        public event EventHandler FormClosing;

        /// <summary>
        /// 设置DockPanel触发的事件
        /// </summary>
        public event EventHandler SetDockPanelEvent;

        private string m_TabText = "";
        /// <summary>
        /// DockPanel的标题
        /// </summary>
        public string TabText
        {
            get { return m_TabText; }
            set
            {
                m_TabText = value;

                if (m_DockPanel != null)
                    m_DockPanel.Text = m_TabText;
            }
        }

        private DockPanel m_DockPanel = null;
        /// <summary>
        /// 停靠的DockPanel
        /// </summary>
        public DockPanel DockPanel
        {
            get { return m_DockPanel; }
            set
            {
                m_DockPanel = value;

                //触发SetDockPanelEvent事件
                OnSetDockPanelEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// 可视属性
        /// </summary>
        public DockVisibility DockVisibility
        {
            get
            {
                if (m_DockPanel != null)
                    return m_DockPanel.Visibility;
                else
                    return DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            set
            {
                if (m_DockPanel != null)
                    m_DockPanel.Visibility = value;
            }
        }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden
        {
            get
            {
                if (m_DockPanel != null)
                {
                    if (m_DockPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden || m_DockPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.AutoHide)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 设置或获取自动隐藏
        /// </summary>
        public bool AutoHide
        {
            get
            {
                if (m_DockPanel != null)
                {
                    if (m_DockPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.AutoHide)
                    {
                        return true;
                    }
                }

                return false;
            }
            set
            {
                if (m_DockPanel != null)
                {
                    if (value == true)
                        m_DockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                    else
                        m_DockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                }
            }
        }

        /// <summary>
        /// 默认构造函数 
        /// </summary>
        public DockDocument()
        {
            //初始化界面组件
            InitializeComponent();
        }

        /// <summary>
        /// 触发FormClosed事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnFormClosed(object sender, EventArgs e)
        {
            if (FormClosed != null)
            {
                FormClosed(sender, e);
            }
        }

        /// <summary>
        /// 触发FormClosing事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnFormClosing(object sender, EventArgs e)
        {
            if (FormClosing != null)
            {
                FormClosing(sender, e);
            }
        }

        /// <summary>
        /// 触发SetDockPanelEvent事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnSetDockPanelEvent(object sender, EventArgs e)
        {
            if (SetDockPanelEvent != null)
            {
                SetDockPanelEvent(sender, e);
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public void Close()
        {
            if (m_DockPanel != null)
            {
                //获取当前dockpanel的索引
                int idx = -1;
                DockManager dockManager = m_DockPanel.DockManager;
                for (int i = 0; i < dockManager.Panels.Count; i++)
                {
                    if (dockManager.Panels[i] == m_DockPanel)
                    {
                        idx = i;
                        break;
                    }
                }

                m_DockPanel.Close();

                //把ActivePanel设为当前dockpanel的前一个dockpanel
                //PS：因为dockpanel关闭后，某些情况下存在没有ActivePanel并不会刷新界面的情况，因此手动把前一个dockpanel设为ActivePanel
                idx = idx - 1;

                if (idx >= 0)
                {
                    dockManager.ActivePanel = dockManager.Panels[idx];
                }
            }
        }

        public new void Show()
        {
            if (m_DockPanel != null)
            {
                m_DockPanel.DockManager.ActivePanel = m_DockPanel;
            }
        }
    }
}