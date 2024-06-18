using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 地图文档对象接口
    /// </summary>
    public interface IMapDocumentView:IDocumentView 
    {

        /// <summary>
        /// 获取当前地图文档地图
        /// </summary>
        IMap Map { get;}
        /// <summary>
        /// 获取当前地图文档视图
        /// </summary>
        IActiveView ActiveView { get;}

        /// <summary>
        /// 获取或设置地图文档默认右键菜单
        /// </summary>
        IContextMenu DefaultContextMenu { get;set;}
    }
}
