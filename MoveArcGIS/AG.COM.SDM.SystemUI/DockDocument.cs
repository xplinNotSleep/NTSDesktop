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
    /// ��ͣ������Ļ���
    /// </summary>
    public partial class DockDocument : UserControl
    {
        /// <summary>
        /// �ر��¼�
        /// </summary>
        public event EventHandler FormClosed;

        /// <summary>
        /// �ر��¼�
        /// </summary>
        public event EventHandler FormClosing;

        /// <summary>
        /// ����DockPanel�������¼�
        /// </summary>
        public event EventHandler SetDockPanelEvent;

        private string m_TabText = "";
        /// <summary>
        /// DockPanel�ı���
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
        /// ͣ����DockPanel
        /// </summary>
        public DockPanel DockPanel
        {
            get { return m_DockPanel; }
            set
            {
                m_DockPanel = value;

                //����SetDockPanelEvent�¼�
                OnSetDockPanelEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// ��������
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
        /// �Ƿ�����
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
        /// ���û��ȡ�Զ�����
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
        /// Ĭ�Ϲ��캯�� 
        /// </summary>
        public DockDocument()
        {
            //��ʼ���������
            InitializeComponent();
        }

        /// <summary>
        /// ����FormClosed�¼�
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
        /// ����FormClosing�¼�
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
        /// ����SetDockPanelEvent�¼�
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
        /// �رմ���
        /// </summary>
        public void Close()
        {
            if (m_DockPanel != null)
            {
                //��ȡ��ǰdockpanel������
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

                //��ActivePanel��Ϊ��ǰdockpanel��ǰһ��dockpanel
                //PS����Ϊdockpanel�رպ�ĳЩ����´���û��ActivePanel������ˢ�½�������������ֶ���ǰһ��dockpanel��ΪActivePanel
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