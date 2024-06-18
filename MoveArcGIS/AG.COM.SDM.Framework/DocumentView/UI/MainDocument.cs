using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AG.COM.SDM.Framework.DocumentView
{
    public partial class MainDocument : DockDocument, IMainDocumentView 
    {
        private IFramework m_Framework;             //系统集成框架对象
        private IContextMenu m_DefaultContextMenu;         //默认的右键菜单
        private MainMessageFilter m_MessageFilter;

        public MainDocument(IFramework pFramework)
        {
            InitializeComponent();

            m_Framework = pFramework;
            //初始地图消息筛选器并注册鼠标滚动事件
            this.m_MessageFilter = new MainMessageFilter(this.MainPanel.Handle);
            Application.AddMessageFilter(this.m_MessageFilter);

            this.SetDockPanelEvent += new EventHandler(MainDocument_SetDockPanelEvent);

        }

        void MainDocument_SetDockPanelEvent(object sender, EventArgs e)
        {
            if (DockPanel != null)
            {
                DockPanel.Options.AllowDockBottom = false;
                DockPanel.Options.AllowDockFill = false;
                DockPanel.Options.AllowDockLeft = false;
                DockPanel.Options.AllowDockRight = false;
                DockPanel.Options.AllowDockTop = false;
                DockPanel.Options.FloatOnDblClick = false;
                DockPanel.Options.ShowAutoHideButton = false;
                DockPanel.Options.ShowCloseButton = false;
                DockPanel.Options.ShowMaximizeButton = false;

            }
        }

        #region IMainDocumentView 成员

        /// <summary>
        /// 获取或设置地图默认右键菜单
        /// </summary>
        public IContextMenu DefaultContextMenu
        {
            get { return this.m_DefaultContextMenu; }
            set { this.m_DefaultContextMenu = value; }
        }

        #endregion

        #region IDocumentView 成员

        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return this.TabText;
            }
            set
            {
                this.TabText = value;
            }
        }

        /// <summary>
        /// 文档类型
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.MainDocument; }
        }

        /// <summary>
        /// 获取AxMapControl对象
        /// </summary>
        public object Hook
        {
            get { return this.MainPanel.Handle; }
        }

        #endregion

        private void MainPanel_OnMouseDown(object sender,
            MouseEventArgs e)
        {
            //map的DockPanel丢失焦点之后，再进行平移等操作，DockPanel也不能再次获得焦点，导致鼠标滚轮事件不触发，因此手动设置焦点
            if (DockPanel != null)
            {
                DockPanel.Show();
            }
            //弹出右键菜单 
            if (e.Button == MouseButtons.Right)
            {
                this.m_DefaultContextMenu.OnCreate(this.m_Framework);
                this.m_DefaultContextMenu.PopupMenu(e.X, e.Y, MainPanel.Handle);
            }
        }

        public void RemoveMapMsgFilter()
        {
            Application.RemoveMessageFilter(this.m_MessageFilter);
        }

    }
}
