using AG.COM.SDM.SystemUI;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 地图文档类
    /// </summary>
    public partial class MainDocumentCopy : DockDocument, IMainDocumentView 
    {
        private IFramework m_Framework;             //系统集成框架对象
        private IContextMenu m_DefaultContextMenu;         //默认的右键菜单
        private MainMessageFilter m_MessageFilter;   //鼠标滚轮向前状态量
        private int m_MouseWheelForward = 0;        //鼠标滚轮向前状态量
        private int m_MouseWheelBack = 0;           //鼠标滚轮向后状态量
        private bool m_IsMiddleButton;              //是否为鼠标中键

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MainDocumentCopy(IFramework pFramework)
        {
            InitializeComponent();

            this.m_Framework = pFramework;

            //初始地图消息筛选器并注册鼠标滚动事件
            this.m_MessageFilter = new MainMessageFilter(this.groupBox1.Handle);
            this.m_MessageFilter.OnWheelBackward += new MouseEventHandler(MapControl_OnWheelBackward);
            this.m_MessageFilter.OnWheelForward += new MouseEventHandler(MapControl_OnWheelForward);
            Application.AddMessageFilter(this.m_MessageFilter);

            this.SetDockPanelEvent += new EventHandler(MainDocument_SetDockPanelEvent);
            groupBox1.MouseDown += new MouseEventHandler(groupBox1_OnMouseDown);
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
                this.TabText=value;
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
            get { return this.groupBox1.Handle; }
        }

        #endregion

        private void groupBox1_OnMouseDown(object sender, 
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
                this.m_DefaultContextMenu.PopupMenu(e.X, e.Y, groupBox1.Handle);
            }
        }

        /// <summary>
        /// 鼠标滚轮向前滚动时的处理操作
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">鼠标事件参数</param>
        private void MapControl_OnWheelForward(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// 鼠标滚轮向后滚动时的处理操作
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">鼠标事件参数</param>
        private void MapControl_OnWheelBackward(object sender, MouseEventArgs e)
        {
        }

        private void groupBox1_OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void groupBox1_OnMouseUp(object sender, MouseEventArgs e)
        {
            
        }

        public void RemoveMapMsgFilter()
        {
            Application.RemoveMessageFilter(this.m_MessageFilter);
        }

    }   
}