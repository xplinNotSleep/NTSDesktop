using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 集成框架对外通信类
    /// </summary>
    public class HookHelper : IHookHelper
    {
        //private IMapService m_MapService;
        private IFramework m_Framework;

        #region IHookHelper 成员

        /// <summary>
        /// 获取或设置消息钩子对象
        /// </summary>
        public IFramework Framework
        {
            get
            {
                return m_Framework;
            }
            set
            {
                m_Framework = value as IFramework;
                if (m_Framework == null)
                    throw new ArgumentNullException();
                //else
                //    m_MapService = m_Framework.GetService(typeof(IMapService)) as IMapService;
            }
        }

        #endregion

        #region IHookHelper 成员
        /// <summary>
        /// 获取IWin32window接口
        /// </summary>
        public IWin32Window Win32Window
        {
            get
            {
                return this.m_Framework.MdiParentForm as IWin32Window;
            }
        }

        /// <summary>
        /// 获取地图获取对象
        /// </summary>
        //public IMapService MapService
        //{
        //    get
        //    {
        //        return m_Framework.GetService(typeof(IMapService)) as IMapService;
        //    }
        //}

        /// <summary>
        /// 获取ToolBar服务
        /// </summary>
        public IToolBarService ToolBarService
        {
            get { return m_Framework.GetService(typeof(IToolBarService)) as IToolBarService; }
        }

        /// <summary>
        /// 获取插件服务对象
        /// </summary>
        public IPluginsService PluginsService
        {
            get { return m_Framework.GetService(typeof(IPluginsService)) as IPluginsService; }
        }

        /// <summary>
        /// 获取菜单服务对象
        /// </summary>
        public IMenuService MenuService
        {
            get { return m_Framework.GetService(typeof(IMenuService)) as IMenuService; }
        }

        /// <summary>
        /// 获取文档视图服务对象
        /// </summary>
        public IDockDocumentService DockDocumentService
        {
            get { return m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService; }
        }
        #endregion
    }
}
