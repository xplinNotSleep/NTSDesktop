using AG.COM.SDM.SystemUI;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ��ͼ�ĵ���
    /// </summary>
    public partial class MainDocumentCopy : DockDocument, IMainDocumentView 
    {
        private IFramework m_Framework;             //ϵͳ���ɿ�ܶ���
        private IContextMenu m_DefaultContextMenu;         //Ĭ�ϵ��Ҽ��˵�
        private MainMessageFilter m_MessageFilter;   //��������ǰ״̬��
        private int m_MouseWheelForward = 0;        //��������ǰ״̬��
        private int m_MouseWheelBack = 0;           //���������״̬��
        private bool m_IsMiddleButton;              //�Ƿ�Ϊ����м�

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MainDocumentCopy(IFramework pFramework)
        {
            InitializeComponent();

            this.m_Framework = pFramework;

            //��ʼ��ͼ��Ϣɸѡ����ע���������¼�
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

        #region IMainDocumentView ��Ա

        /// <summary>
        /// ��ȡ�����õ�ͼĬ���Ҽ��˵�
        /// </summary>
        public IContextMenu DefaultContextMenu
        {
            get { return this.m_DefaultContextMenu; }
            set { this.m_DefaultContextMenu = value; }
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
            get { return EnumDocumentType.MainDocument; }
        }

        /// <summary>
        /// ��ȡAxMapControl����
        /// </summary>
        public object Hook
        {
            get { return this.groupBox1.Handle; }
        }

        #endregion

        private void groupBox1_OnMouseDown(object sender, 
            MouseEventArgs e)
        {
            //map��DockPanel��ʧ����֮���ٽ���ƽ�ƵȲ�����DockPanelҲ�����ٴλ�ý��㣬�����������¼�������������ֶ����ý���
            if (DockPanel != null)
            {
                DockPanel.Show();
            }
            //�����Ҽ��˵� 
            if (e.Button == MouseButtons.Right)
            {
                this.m_DefaultContextMenu.OnCreate(this.m_Framework);
                this.m_DefaultContextMenu.PopupMenu(e.X, e.Y, groupBox1.Handle);
            }
        }

        /// <summary>
        /// ��������ǰ����ʱ�Ĵ������
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">����¼�����</param>
        private void MapControl_OnWheelForward(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// ������������ʱ�Ĵ������
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">����¼�����</param>
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