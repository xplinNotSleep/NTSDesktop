using AG.COM.SDM.SystemUI;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 地图文档类
    /// </summary>
    public partial class MapDocument : DockDocument, IMapDocumentView 
    {
        private IFramework m_Framework;             //系统集成框架对象
        private IContextMenu m_DefaultMenu;         //默认的右键菜单
        private MapMessageFilter m_MessageFilter;   //鼠标滚轮向前状态量
        private int m_MouseWheelForward = 0;        //鼠标滚轮向前状态量
        private int m_MouseWheelBack = 0;           //鼠标滚轮向后状态量
        private bool m_IsMiddleButton;              //是否为鼠标中键

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MapDocument(IFramework pFramework)
        {
            InitializeComponent();

            this.m_Framework = pFramework;

            //初始地图消息筛选器并注册鼠标滚动事件
            this.m_MessageFilter = new MapMessageFilter(this.axMapControl1.Handle);
            this.m_MessageFilter.OnWheelBackward += new MouseEventHandler(MapControl_OnWheelBackward);
            this.m_MessageFilter.OnWheelForward += new MouseEventHandler(MapControl_OnWheelForward);
            Application.AddMessageFilter(this.m_MessageFilter);

            this.SetDockPanelEvent += new EventHandler(MapDocument_SetDockPanelEvent);
            this.axMapControl1.AutoMouseWheel = true;
            axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(axMapControl1_OnMouseDown);
        }

        void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //map的DockPanel丢失焦点之后，再进行平移等操作，DockPanel也不能再次获得焦点，导致鼠标滚轮事件不触发，因此手动设置焦点
            if (DockPanel != null)
            {
                DockPanel.Show();
            }
        }

        void MapDocument_SetDockPanelEvent(object sender, EventArgs e)
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

        #region IMapDocumentView 成员

        /// <summary>
        /// 激活的地图视图
        /// </summary>
        public IActiveView ActiveView
        {
            get { return this.axMapControl1.ActiveView; }
        }

        /// <summary>
        /// 获取地图对象
        /// </summary>
        public IMap Map
        {
            get
            {
                return this.axMapControl1.Map;
            }
        }

        /// <summary>
        /// 获取或设置地图默认右键菜单
        /// </summary>
        public IContextMenu DefaultContextMenu
        {
            get { return this.m_DefaultMenu; }
            set { this.m_DefaultMenu = value; }
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
            get { return EnumDocumentType.MapDocument; }
        }

        /// <summary>
        /// 获取AxMapControl对象
        /// </summary>
        public object Hook
        {
            get { return this.axMapControl1.Object; }
        }

        #endregion

        private void AxMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //弹出右键菜单 
            if (e.button == 2)
            {
                IToolContextMenu toolMenu = axMapControl1.CurrentTool as IToolContextMenu;
                if (toolMenu != null && toolMenu.ContextMenuStrip != null)
                {
                    //先设置每个菜单项的可用性
                    foreach (ToolStripItem item in toolMenu.ContextMenuStrip.Items)
                    {
                        if (item.Tag is ESRI.ArcGIS.SystemUI.ICommand)
                        {
                            item.Enabled = (item.Tag as ESRI.ArcGIS.SystemUI.ICommand).Enabled;
                        }
                    }

                    //再弹出菜单 
                    toolMenu.ContextMenuStrip.Show(axMapControl1, e.x, e.y);
                }
                else
                {
                    //如果默认菜单不为空，则弹出
                    if (this.m_DefaultMenu != null)
                    {
                        this.m_DefaultMenu.OnCreate(this.m_Framework);
                        this.m_DefaultMenu.PopupMenu(e.x, e.y, axMapControl1.hWnd);
                    }
                }
            }

            if (e.button == 4)
            {
                this.m_IsMiddleButton = true;
                
                //移动起始位置
                IPoint tPt = new PointClass();
                tPt.X = e.mapX;
                tPt.Y = e.mapY;

                //this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHand;
                IScreenDisplay tScreenDisplay = this.axMapControl1.ActiveView.ScreenDisplay;                
                tScreenDisplay.PanStart(tPt);
            }
        }

        /// <summary>
        /// 鼠标滚轮向前滚动时的处理操作
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">鼠标事件参数</param>
        private void MapControl_OnWheelForward(object sender, MouseEventArgs e)
        {
            m_MouseWheelForward = m_MouseWheelForward + 1;
            if (m_MouseWheelForward == 2)
            {
                System.Drawing.Point pt = this.axMapControl1.PointToClient(new System.Drawing.Point(e.X, e.Y));
                if (pt.X > 0 && pt.Y > 0)
                {
                    IEnvelope tEnv = this.axMapControl1.Extent;

                    if (tEnv.IsEmpty == false)
                    {
                        tEnv.Expand(0.6, 0.6, true);
                        this.axMapControl1.Extent = tEnv;
                    }
                }

                m_MouseWheelForward = 0;
            }           
        }

        /// <summary>
        /// 鼠标滚轮向后滚动时的处理操作
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">鼠标事件参数</param>
        private void MapControl_OnWheelBackward(object sender, MouseEventArgs e)
        {
            m_MouseWheelBack = m_MouseWheelBack + 1;
            if (m_MouseWheelBack == 2)
            {
                System.Drawing.Point pt = this.axMapControl1.PointToClient(new System.Drawing.Point(e.X, e.Y));

                if (pt.X > 0 && pt.Y > 0)
                {
                    IEnvelope tEnv = this.axMapControl1.Extent;

                    if (tEnv.IsEmpty == false)
                    {
                        tEnv.Expand(1.4, 1.4, true);
                        this.axMapControl1.Extent = tEnv;
                    }
                }
                m_MouseWheelBack = 0;
            }            
        }

        private void AxMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            IPoint tPt = new PointClass();
            tPt.X = e.mapX;
            tPt.Y = e.mapY;

            IScreenDisplay tScreenDisplay = this.axMapControl1.ActiveView.ScreenDisplay;            
            tScreenDisplay.PanMoveTo(tPt);
        }

        private void AxMapControl1_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent e)
        {
            this.m_IsMiddleButton = false;           

            IScreenDisplay tScreenDisplay = this.axMapControl1.ActiveView.ScreenDisplay;
            tScreenDisplay.PanStop();
            //this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            
        }

        public void RemoveMapMsgFilter()
        {
            Application.RemoveMessageFilter(this.m_MessageFilter);
        }

        public void UnRegisterPanEvents()
        {
            this.axMapControl1.OnMouseDown -= new IMapControlEvents2_Ax_OnMouseDownEventHandler(this.AxMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove -= new IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.AxMapControl1_OnMouseMove);
            this.axMapControl1.OnMouseUp -= new IMapControlEvents2_Ax_OnMouseUpEventHandler(this.AxMapControl1_OnMouseUp);
        }
    }   
}