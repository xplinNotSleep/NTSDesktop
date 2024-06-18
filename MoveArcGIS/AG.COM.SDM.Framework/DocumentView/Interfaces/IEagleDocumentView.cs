using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;
using System;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 鹰眼视图接口类
    /// </summary>
    public interface IEagleDocumentView : IDocumentView
    {
        /// <summary>
        /// 鹰眼视图地图对象
        /// </summary>
        IMap EagleMap { get; }
        /// <summary>
        /// 获取或设置是否指定了鹰眼视图图层对象
        /// </summary>
        Boolean IsAffirm { get; }
        /// <summary>
        /// 设置鹰眼视图导航地图文档路径
        /// </summary>
        string MapDocument { set; }
    }
}
