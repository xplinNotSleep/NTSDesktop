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
    /// 应用集成框架类
    /// </summary>
    public class Framework : IFramework, IFrameworkEvents
    {
        #region 私有变量
        private DockManager m_DockManager;             //DockManager      
        private DocumentManager m_DocumentManager;
        private RibbonStatusBar m_StatusBar;            //状态栏
        private Form m_MdiParentForm;               //MDI窗体  
        private ServiceContainer m_ServiceContainer; //服务对象容器 
        private IList<Type> m_ListServiceType;

        private bool m_EnabledMenuRefresh = true;
        /// <summary>
        /// 是否启用菜单刷新
        /// </summary>
        public bool EnabledMenuRefresh
        {
            get { return m_EnabledMenuRefresh; }
            set { m_EnabledMenuRefresh = value; }
        }

        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Framework()
        {
            this.m_ListServiceType = new List<Type>();
            this.m_ServiceContainer = new ServiceContainer();
            this.m_ServiceContainer.AddService(typeof(IPluginsService), PluginsService.GetInstance(this));
        }

        #region IFramework 成员

        /// <summary>
        /// 设置或获取状态栏
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
        /// 设置或获取DockPanel
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
        /// 设置或获取DocumentManager
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
                    //绑定DocumentManagerde Document激活事件
                    value.DocumentActivate += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(value_DocumentActivate);
                }
            }
        }

        /// <summary>
        /// DocumentManagerde Document激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void value_DocumentActivate(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {

            DockPanel dockPanel = null;
            //Document在不同情况下内含控件的结构也不一样

            //如果已经有至少一个Document，此时再停靠第二个或多个的情况
            if (e.Document.Control is DockPanel)
            {
                dockPanel = e.Document.Control as DockPanel;
            }
            //第一次有Document停靠，Document.Control是FloatForm（就是还是停靠前的状态）
            else if (e.Document.Control is FloatForm)
            {
                dockPanel = e.Document.Control.Controls[0] as DockPanel;
            }
            else
            {
                throw new Exception("documentManager DocumentActivate事件出错，未知的Document内含控件类型");
            }

            //通知所有登记注册文档视图发生变化的事件对象
            object dockDocument = null;
            dockDocument = dockPanel.ControlContainer.Controls.Count > 0 ? dockPanel.ControlContainer.Controls[0] : null;

            this.OnMapDocumentChanged(dockDocument, e);

        }

        /// <summary>
        /// 获取或设置当前激活子窗体
        /// </summary>
        public Form ActiveMdiChildForm
        {
            get
            {
                return this.m_MdiParentForm.ActiveMdiChild;
            }
        }

        /// <summary>
        /// 获取或设置当前MDI父窗体
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
        /// 有权限的插件类名称
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
        /// 无限制许可
        /// </summary>
        public bool LicenseUnlimited
        {
            get { return m_LicenseUnlimited; }
            set { m_LicenseUnlimited = value; }
        }

        #endregion

        #region IServiceContainer 成员
        /// <summary>
        /// 将指定服务添加到服务容器中，并可选择将该服务提升到任何父服务容器。 
        /// </summary>
        /// <param name="serviceType">要添加的服务类型</param>
        /// <param name="callback">要添加的服务类型的实例</param>
        /// <param name="promote">true，则将此请求提升到任何父服务容器;否则为 false</param>
        public void AddService(Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback, bool promote)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, callback, promote);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// 将指定的服务添加到服务容器中。 
        /// </summary>
        /// <param name="serviceType">要添加的服务类型</param>
        /// <param name="callback">用于创建服务的回调对象.</param>
        public void AddService(Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, callback);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// 将指定的服务添加到服务容器中。 
        /// </summary>
        /// <param name="serviceType">要添加的服务类型</param>
        /// <param name="serviceInstance">要添加的服务类型的实例</param>
        /// <param name="promote">true,则将此请求提升到任何父服务容器;否则为 false</param>
        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, serviceInstance, promote);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// 将指定的服务添加到服务容器中.
        /// </summary>
        /// <param name="serviceType">要添加的服务类型</param>
        /// <param name="serviceInstance">要添加的服务类型的实例</param>
        public void AddService(Type serviceType, object serviceInstance)
        {
            if (this.m_ListServiceType.Contains(serviceType) == false)
            {
                this.m_ServiceContainer.AddService(serviceType, serviceInstance);
                this.m_ListServiceType.Add(serviceType);
            }
        }

        /// <summary>
        /// 从服务容器中移除指定的服务类型。 
        /// </summary>
        /// <param name="serviceType">要移除的服务类型.</param>
        /// <param name="promote">true，则将此请求提升到任何父服务容器;否则为 false</param>
        public void RemoveService(Type serviceType, bool promote)
        {
            if (this.m_ListServiceType.Contains(serviceType) == true)
            {
                this.m_ServiceContainer.RemoveService(serviceType, promote);
                this.m_ListServiceType.Remove(serviceType);
            }
        }

        /// <summary>
        /// 从服务容器中移除指定的服务类型。 
        /// </summary>
        /// <param name="serviceType">要移除的服务类型</param>
        public void RemoveService(Type serviceType)
        {
            if (this.m_ListServiceType.Contains(serviceType) == true)
            {
                this.m_ServiceContainer.RemoveService(serviceType);
                this.m_ListServiceType.Remove(serviceType);
            }
        }

        #endregion

        #region IServiceProvider 成员
        /// <summary>
        /// 获取指定类型的服务对象。  
        /// </summary>
        /// <param name="serviceType">要获取的服务类型</param>
        /// <returns>serviceType 类型的服务对象。 - 或 - 如果没有 serviceType 类型的服务对象，则为空引用</returns>
        public object GetService(Type serviceType)
        {
            if (m_ServiceContainer != null)
                return m_ServiceContainer.GetService(serviceType);
            else
                return null;
        }

        #endregion

        #region IFrameworkEvents 成员

        /// <summary>
        /// 地图文档改变事件
        /// </summary>
        public event MapDocumentChangedEventHandler MapDocumentChanged;

        /// <summary>
        /// 当前图层发生变化事件
        /// </summary>
        public event CurrentLayerChangedEventHandler CurrentLayerChanged;

        /// <summary>
        /// 鹰眼视图发生改变事件
        /// </summary>
        public event EagleViewChangedEventHandler EagleViewChanged;

        /// <summary>
        /// 插件对象单击处理事件
        /// </summary>
        public event PlugCommandClikedEventHandler PlugCommandCliked;

        /// <summary>
        /// 负责通知地图文档对象事件的登记对象
        /// </summary>
        /// <param name="sender">文档对象</param>
        /// <param name="e">事件参数</param>
        public virtual void OnMapDocumentChanged(object sender, EventArgs e)
        {
            if (MapDocumentChanged != null)
            {
                MapDocumentChanged(sender, e);
            }
        }

        /// <summary>
        /// 负责通知当前图层发生变化的登记对象
        /// </summary>
        /// <param name="sender">图层对象</param>
        /// <param name="e">事件参数</param>
        public virtual void OnCurrentLayerChanged(object sender, EventArgs e)
        {
            if (CurrentLayerChanged != null)
                CurrentLayerChanged(sender, e);
        }

        /// <summary>
        /// 负责通知鹰眼视图发生变化的登记对象
        /// </summary>
        /// <param name="newEnvelope">新的可视范围</param>
        /// <param name="e">事件参数</param>
        public virtual void OnEagleViewChanged(object newEnvelope, EventArgs e)
        {
            if (EagleViewChanged != null)
                EagleViewChanged(newEnvelope, e);
        }

        /// <summary>
        /// 负责通知插件对象单击时的登记对象
        /// </summary>
        /// <param name="sender">插件对象</param>
        /// <param name="e">事件参数</param>
        public virtual void OnPlugCommandClicked(object sender, EventArgs e)
        {
            if (PlugCommandCliked != null)
                PlugCommandCliked(sender, e);
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 工具栏右键菜单选中状态发生变化处理事件
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        private void MenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tMenuItem = sender as ToolStripMenuItem;
            ToolStrip tToolStrip = tMenuItem.Tag as ToolStrip;
            tToolStrip.Visible = tMenuItem.Checked;
        }
        #endregion
    }
}
