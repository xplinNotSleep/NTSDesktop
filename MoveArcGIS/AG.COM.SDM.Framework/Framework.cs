using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AG.COM.SDM.Framework;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Ribbon;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// Ӧ�ü��ɿ����
    /// </summary>
    public class Framework : IFramework, IFrameworkEvents
    {
        #region ˽�б���
        private DockManager m_DockManager;             //DockManager      
        private DocumentManager m_DocumentManager;
        private RibbonStatusBar m_StatusBar;            //״̬��
        private Form m_MdiParentForm;               //MDI����  
        private ServiceContainer m_ServiceContainer; //����������� 
        private IList<Type> m_ListServiceType;

        private bool m_EnabledMenuRefresh = true;
        /// <summary>
        /// �Ƿ����ò˵�ˢ��
        /// </summary>
        public bool EnabledMenuRefresh
        {
            get { return m_EnabledMenuRefresh; }
            set { m_EnabledMenuRefresh = value; }
        }

        #endregion

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Framework()
        {
            this.m_ListServiceType = new List<Type>();
            this.m_ServiceContainer = new ServiceContainer();
            this.m_ServiceContainer.AddService(typeof(IPluginsService), PluginsService.GetInstance(this));
        }

        #region IFramework ��Ա

        /// <summary>
        /// ���û��ȡ״̬��
        /// </summary>
        public RibbonStatusBar StatusBar
        {
            get
            {
                return this.m_StatusBar;
            }
            set
            {
                this.m_StatusBar = value;
            }
        }

        /// <summary>
        /// ���û��ȡDockPanel
        /// </summary>
        public DockManager DockManager
        {
            get
            {
                return m_DockManager;
            }
            set
            {
                m_DockManager = value;
            }
        }

        /// <summary>
        /// ���û��ȡDocumentManager
        /// </summary>
        public DocumentManager DocumentManager
        {
            get
            {
                return m_DocumentManager;
            }
            set
            {
                m_DocumentManager = value;

                if (value != null)
                {
                    //��DocumentManagerde Document�����¼�
                    value.DocumentActivate += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(value_DocumentActivate);
                }
            }
        }

        /// <summary>
        /// DocumentManagerde Document�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void value_DocumentActivate(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {

            DockPanel dockPanel = null;
            //Document�ڲ�ͬ������ں��ؼ��ĽṹҲ��һ��

            //����Ѿ�������һ��Document����ʱ��ͣ���ڶ������������
            if (e.Document.Control is DockPanel)
            {
                dockPanel = e.Document.Control as DockPanel;
            }
            //��һ����Documentͣ����Document.Control��FloatForm�����ǻ���ͣ��ǰ��״̬��
            else if (e.Document.Control is FloatForm)
            {
                dockPanel = e.Document.Control.Controls[0] as DockPanel;
            }
            else
            {
                throw new Exception("documentManager DocumentActivate�¼�����δ֪��Document�ں��ؼ�����");
            }

            //֪ͨ���еǼ�ע���ĵ���ͼ�����仯���¼�����
            object dockDocument = null;
            dockDocument = dockPanel.ControlContainer.Controls.Count > 0 ? dockPanel.ControlContainer.Controls[0] : null;

            this.OnMapDocumentChanged(dockDocument, e);

        }

        /// <summary>
        /// ��ȡ�����õ�ǰ�����Ӵ���
        /// </summary>
        public Form ActiveMdiChildForm
        {
            get
            {
                return this.m_MdiParentForm.ActiveMdiChild;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰMDI������
        /// </summary>
        public Form MdiParentForm
        {
            get
            {
                return m_MdiParentForm;
            }
            set
            {
                this.m_MdiParentForm = value;
            }
        }

        private List<string> m_HasLicPlugins = new List<string>();
        /// <summary>
        /// ��Ȩ�޵Ĳ��������
        /// </summary>
        public List<string> HasLicPlugins
        {
            get { return m_HasLicPlugins; }
            set
            {
                if (value == null)
                    m_HasLicPlugins = new List<string>();
                else
                    m_HasLicPlugins = value;
            }
        }

        private bool m_LicenseUnlimited = false;
        /// <summary>
        /// ���������
        /// </summary>
        public bool LicenseUnlimited
        {
            get { return m_LicenseUnlimited; }
            set { m_LicenseUnlimited = value; }
        }

        #endregion

        #region IServiceContainer ��Ա
        /// <summary>
        /// ��ָ��������ӵ����������У�����ѡ�񽫸÷����������κθ����������� 
        /// </summary>
        /// <param name="serviceType">Ҫ��ӵķ�������</param>
        /// <param name="callback">Ҫ��ӵķ������͵�ʵ��</param>
        /// <param name="promote">true���򽫴������������κθ���������;����Ϊ false</param>
        public void AddService(Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback, bool promote)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, callback, promote);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// ��ָ���ķ�����ӵ����������С� 
        /// </summary>
        /// <param name="serviceType">Ҫ��ӵķ�������</param>
        /// <param name="callback">���ڴ�������Ļص�����.</param>
        public void AddService(Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, callback);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// ��ָ���ķ�����ӵ����������С� 
        /// </summary>
        /// <param name="serviceType">Ҫ��ӵķ�������</param>
        /// <param name="serviceInstance">Ҫ��ӵķ������͵�ʵ��</param>
        /// <param name="promote">true,�򽫴������������κθ���������;����Ϊ false</param>
        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, serviceInstance, promote);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// ��ָ���ķ�����ӵ�����������.
        /// </summary>
        /// <param name="serviceType">Ҫ��ӵķ�������</param>
        /// <param name="serviceInstance">Ҫ��ӵķ������͵�ʵ��</param>
        public void AddService(Type serviceType, object serviceInstance)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, serviceInstance);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// �ӷ����������Ƴ�ָ���ķ������͡� 
        /// </summary>
        /// <param name="serviceType">Ҫ�Ƴ��ķ�������.</param>
        /// <param name="promote">true���򽫴������������κθ���������;����Ϊ false</param>
        public void RemoveService(Type serviceType, bool promote)
        {
            if (this.m_ListServiceType.Contains(serviceType) == true)
            {
                this.m_ServiceContainer.RemoveService(serviceType, promote);
                this.m_ListServiceType.Remove(serviceType);
            }
        }

        /// <summary>
        /// �ӷ����������Ƴ�ָ���ķ������͡� 
        /// </summary>
        /// <param name="serviceType">Ҫ�Ƴ��ķ�������</param>
        public void RemoveService(Type serviceType)
        {
            if (this.m_ListServiceType.Contains(serviceType) == true)
            {
                this.m_ServiceContainer.RemoveService(serviceType);
                this.m_ListServiceType.Remove(serviceType);
            }
        }

        #endregion

        #region IServiceProvider ��Ա
        /// <summary>
        /// ��ȡָ�����͵ķ������  
        /// </summary>
        /// <param name="serviceType">Ҫ��ȡ�ķ�������</param>
        /// <returns>serviceType ���͵ķ������ - �� - ���û�� serviceType ���͵ķ��������Ϊ������</returns>
        public object GetService(Type serviceType)
        {
            if (m_ServiceContainer != null)
                return m_ServiceContainer.GetService(serviceType);
            else
                return null;
        }

        #endregion

        #region IFrameworkEvents ��Ա

        /// <summary>
        /// ��ͼ�ĵ��ı��¼�
        /// </summary>
        public event MapDocumentChangedEventHandler MapDocumentChanged;

        /// <summary>
        /// ��ǰͼ�㷢���仯�¼�
        /// </summary>
        public event CurrentLayerChangedEventHandler CurrentLayerChanged;

        /// <summary>
        /// ӥ����ͼ�����ı��¼�
        /// </summary>
        public event EagleViewChangedEventHandler EagleViewChanged;

        /// <summary>
        /// ������󵥻������¼�
        /// </summary>
        public event PlugCommandClikedEventHandler PlugCommandCliked;

        /// <summary>
        /// ����֪ͨ��ͼ�ĵ������¼��ĵǼǶ���
        /// </summary>
        /// <param name="sender">�ĵ�����</param>
        /// <param name="e">�¼�����</param>
        public virtual void OnMapDocumentChanged(object sender, EventArgs e)
        {
            if (MapDocumentChanged != null)
            {
                MapDocumentChanged(sender, e);
            }
        }

        /// <summary>
        /// ����֪ͨ��ǰͼ�㷢���仯�ĵǼǶ���
        /// </summary>
        /// <param name="sender">ͼ�����</param>
        /// <param name="e">�¼�����</param>
        public virtual void OnCurrentLayerChanged(object sender, EventArgs e)
        {
            if (CurrentLayerChanged != null)
                CurrentLayerChanged(sender, e);
        }

        /// <summary>
        /// ����֪ͨӥ����ͼ�����仯�ĵǼǶ���
        /// </summary>
        /// <param name="newEnvelope">�µĿ��ӷ�Χ</param>
        /// <param name="e">�¼�����</param>
        public virtual void OnEagleViewChanged(object newEnvelope, EventArgs e)
        {
            if (EagleViewChanged != null)
                EagleViewChanged(newEnvelope, e);
        }

        /// <summary>
        /// ����֪ͨ������󵥻�ʱ�ĵǼǶ���
        /// </summary>
        /// <param name="sender">�������</param>
        /// <param name="e">�¼�����</param>
        public virtual void OnPlugCommandClicked(object sender, EventArgs e)
        {
            if (PlugCommandCliked != null)
                PlugCommandCliked(sender, e);
        }
        #endregion

        #region ˽�з���

        /// <summary>
        /// �������Ҽ��˵�ѡ��״̬�����仯�����¼�
        /// </summary>
        /// <param name="sender">�¼�����</param>
        /// <param name="e">�¼�����</param>
        private void MenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tMenuItem = sender as ToolStripMenuItem;
            ToolStrip tToolStrip = tMenuItem.Tag as ToolStrip;
            tToolStrip.Visible = tMenuItem.Checked;
        }
        #endregion
    }
}
