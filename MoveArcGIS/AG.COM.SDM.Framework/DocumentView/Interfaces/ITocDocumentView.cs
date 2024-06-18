using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 数据文档接口类
    /// </summary>
    public interface ITocDocumentView : IDocumentView
    {
        /// <summary>
        /// 默认上下文菜单（通用）
        /// </summary>
        IContextMenu DefaultContextMenu { get; set; }

        #region 数据源到文档对象
        /// <summary>
        /// 绑定数据文档到指定对象
        /// </summary>
        /// <param name="pTocBuddy">绑定对象</param>
        //void SetBuddyControl(object pTocBuddy);
        #endregion

        #region ESRI
        /// <summary>
        /// 获取或设置地图对象上下文菜单
        /// </summary>
        //IContextMenu MapContextMenu { get;set;}
        ///// <summary>
        ///// 获取或设置图层结点上下文菜单
        ///// </summary>
        //IContextMenu LayerContextMenu { get;set;}
        #endregion

    }
}
