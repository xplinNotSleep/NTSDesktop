using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Framework.DocumentView;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 集成框架对外通信接口
    /// </summary>
    public interface IHookHelper
    {
        /// <summary>
        /// 获取地图对象服务接口
        /// </summary>
        //IMapService MapService { get;}

        IFramework Framework { get; set; }

        /// <summary>
        /// 获取工具栏对象服务接口
        /// </summary>
        IToolBarService ToolBarService { get;}
        /// <summary>
        /// 获取插件对象服务接口
        /// </summary>
        IPluginsService PluginsService { get;}
        /// <summary>
        /// 获取菜单对象服务接口
        /// </summary>
        IMenuService MenuService { get;}        
        /// <summary>
        /// 获取文档对象服务接口
        /// </summary>
        IDockDocumentService DockDocumentService { get;}
        /// <summary>
        /// 获取IWin32Window接口
        /// </summary>
        IWin32Window Win32Window { get;}


        
    }

    
}
