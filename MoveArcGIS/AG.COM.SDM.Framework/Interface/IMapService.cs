using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework.DocumentView;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 地图服务接口
    /// </summary>
    public interface IMapService
    {
        /// <summary>
        /// 获取或设置地图捕捉容差
        /// </summary>
        double Tolerance { get;set;}
        /// <summary>
        /// 获取当前激活视图对象
        /// </summary>
        IActiveView ActiveView { get;}
        /// <summary>
        /// 获取当前激活地图对象
        /// </summary>
        IMap FocusMap { get;}
        /// <summary>
        /// 获取或设置地图文档对象
        /// </summary>
        IMapDocument MapDocument { get;set;}
        /// <summary>
        /// 获取当前操作地图控件
        /// </summary>
        IMapControl2 MapControl { get;}
        /// <summary>
        /// 获取当前地图操作工具
        /// </summary>
        ITool CurrentTool { get;set;}
        /// <summary>
        /// 获取当前操作地图图层
        /// </summary>
        ILayer CurrentLayer { get;}
        /// <summary>
        /// 获取当前操作对象栈
        /// </summary>
        IOperationStack OperationStack { get;}
        /// <summary>
        /// 获取版面视图对象
        /// </summary>
        IPageLayout PageLayout { get;}
        /// <summary>
        /// 获取版面视图控件
        /// </summary>
        IPageLayoutControl2 PageLayoutControl { get;}
        /// <summary>
        /// 获取数据视图控件
        /// </summary>
        ITOCControl TOCControl { get;}
        /// <summary>
        /// 获取鹰眼地图控件
        /// </summary>
        IMapControl2 EagleMapControl { get;}
        /// <summary>
        /// 获取或设置钩子对象
        /// </summary>
        object Hook { get;set;}
        /// <summary>
        /// 获取地图编辑对象
        /// </summary>
        MapEditor Editing { get;}
        /// <summary>
        /// 地图信息提示
        /// </summary>
        ToolTip InfoTip { get;}
        /// <summary>
        /// 视图刷新事件
        /// </summary>
        event IMapControlEvents2_OnViewRefreshedEventHandler OnViewRefreshed;
        /// <summary>
        /// 地图当前范围更新事件
        /// </summary>
        event IMapControlEvents2_OnExtentUpdatedEventHandler OnExtentUpdated;
        /// <summary>
        /// 地图对象更新事件
        /// </summary>
        event IMapControlEvents2_OnMapReplacedEventHandler OnMapReplaced;
    }
}
