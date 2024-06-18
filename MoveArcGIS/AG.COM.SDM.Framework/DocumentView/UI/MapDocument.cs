using AG.COM.SDM.SystemUI;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ��ͼ�ĵ���
    /// </summary>
    public partial class MapDocument : DockDocument, IMapDocumentView 
    {
        private IFramework m_Framework;             //ϵͳ���ɿ�ܶ���
        private IContextMenu m_DefaultMenu;         //Ĭ�ϵ��Ҽ��˵�
        private MapMessageFilter m_MessageFilter;   //��������ǰ״̬��
        private int m_MouseWheelForward = 0;        //��������ǰ״̬��
        private int m_MouseWheelBack = 0;           //���������״̬��
        private bool m_IsMiddleButton;              //�Ƿ�Ϊ����м�

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MapDocument(IFramework pFramework)
        {
            InitializeComponent();

            this.m_Framework = pFramework;

            //��ʼ��ͼ��Ϣɸѡ����ע���������¼�
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
            //map��DockPanel��ʧ����֮���ٽ���ƽ�ƵȲ�����DockPanelҲ�����ٴλ�ý��㣬�����������¼�������������ֶ����ý���
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

        #region IMapDocumentView ��Ա

        /// <summary>
        /// ����ĵ�ͼ��ͼ
        /// </summary>
        public IActiveView ActiveView
        {
            get { return this.axMapControl1.ActiveView; }
        }

        /// <summary>
        /// ��ȡ��ͼ����
        /// </summary>
        public IMap Map
        {
            get
            {
                return this.axMapControl1.Map;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ͼĬ���Ҽ��˵�
        /// </summary>
        public IContextMenu DefaultContextMenu
        {
            get { return this.m_DefaultMenu; }
            set { this.m_DefaultMenu = value; }
        }

        #endregion

        #region IDocumentView ��Ա

        /// <summary>
        /// �ĵ�����
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
        /// �ĵ�����
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.MapDocument; }
        }

        /// <summary>
        /// ��ȡAxMapControl����
        /// </summary>
        public object Hook
        {
            get { return this.axMapControl1.Object; }
        }

        #endregion

        private void AxMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //�����Ҽ��˵� 
            if (e.button == 2)
            {
                IToolContextMenu toolMenu = axMapControl1.CurrentTool as IToolContextMenu;
                if (toolMenu != null && toolMenu.ContextMenuStrip != null)
                {
                    //������ÿ���˵���Ŀ�����
                    foreach (ToolStripItem item in toolMenu.ContextMenuStrip.Items)
                    {
                        if (item.Tag is ESRI.ArcGIS.SystemUI.ICommand)
                        {
                            item.Enabled = (item.Tag as ESRI.ArcGIS.SystemUI.ICommand).Enabled;
                        }
                    }

                    //�ٵ����˵� 
                    toolMenu.ContextMenuStrip.Show(axMapControl1, e.x, e.y);
                }
                else
                {
                    //���Ĭ�ϲ˵���Ϊ�գ��򵯳�
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
                
                //�ƶ���ʼλ��
                IPoint tPt = new PointClass();
                tPt.X = e.mapX;
                tPt.Y = e.mapY;

                //this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHand;
                IScreenDisplay tScreenDisplay = this.axMapControl1.ActiveView.ScreenDisplay;                
                tScreenDisplay.PanStart(tPt);
            }
        }

        /// <summary>
        /// ��������ǰ����ʱ�Ĵ������
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">����¼�����</param>
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
        /// ������������ʱ�Ĵ������
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">����¼�����</param>
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