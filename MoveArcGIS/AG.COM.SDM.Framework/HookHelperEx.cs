using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 集成框架对外通信类
    /// </summary>
    public class HookHelperEx : IHookHelperEx
    {
        //private IMapService m_MapService;
        private IFramework m_Framework;

        #region IHookHelper 成员

        #region ESRI
        ///// <summary>
        ///// 获取当前地图的ActiveView(如果当前窗口为视图窗口时，此ActiveView为IPageLayout的ActiveView，
        ///// 如果要操作IPageLayout中的地图，则需写成 ActiveView.FocusMap as IActiveView)
        ///// </summary>
        //public ESRI.ArcGIS.Carto.IActiveView ActiveView
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.ActiveView;
        //    }
        //}

        ///// <summary>
        ///// 获取当前地图服务的激活地图
        ///// </summary>
        //public ESRI.ArcGIS.Carto.IMap FocusMap
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.FocusMap;
        //    }
        //}

        ///// <summary>
        ///// 获取当前操作栈
        ///// </summary>
        //public ESRI.ArcGIS.SystemUI.IOperationStack OperationStack
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.OperationStack;
        //    }
        //}

        ///// <summary>
        ///// 获取版面视图
        ///// </summary>
        //public ESRI.ArcGIS.Carto.IPageLayout PageLayout
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.PageLayout;
        //    }
        //}

        #endregion

        /// <summary>
        /// 获取或设置消息钩子对象
        /// </summary>
        public object Hook
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

        #region IHookHelperEx 成员
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
