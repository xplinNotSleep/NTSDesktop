using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 地图文档对象接口
    /// </summary>
    public interface IMainDocumentView:IDocumentView 
    {

        /// <summary>
        /// 获取或设置地图文档默认右键菜单
        /// </summary>
        IContextMenu DefaultContextMenu { get;set;}
    }
}
