/************************************************************************
* Copyright (c) 2021 All Rights Reserved.
*命名空间：AG.COM.SDM.Framework.DocumentView.Interfaces
*文件名： ISceneDocumentView
*创建人： 雷振京
*创建时间：2021/4/19 16:53:35
*描述：
*=======================================================================
*修改标记
*修改时间：2021/4/19 16:53:35
*修改人：
*描述：
************************************************************************/
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Framework.DocumentView.Interfaces
{
    /// <summary>
    /// 三维文档对象接口
    /// </summary>
    public interface ISceneDocumentView : IDocumentView
    {
        /// <summary>
        /// 获取当前地图文档地图
        /// </summary>
        IScene Scene { get; }
        /// <summary>
        /// 相机
        /// </summary>
        ICamera Camera { get; }
        /// <summary>
        /// 获取当前地图文档视图
        /// </summary>
        IActiveView ActiveView { get; }

        /// <summary>
        /// 获取或设置地图文档默认右键菜单
        /// </summary>
        IContextMenu DefaultContextMenu { get; set; }
    }
}
