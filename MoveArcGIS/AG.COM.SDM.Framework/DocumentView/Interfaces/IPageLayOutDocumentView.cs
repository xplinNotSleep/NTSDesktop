using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 页面布局文档对象接口
    /// </summary>
    public interface IPageLayOutDocumentView : IDocumentView
    {
        /// <summary>
        /// 获取视图内容
        /// </summary>
        IActiveView ActiveView { get;}
        /// <summary>
        /// 获取页面布局对象
        /// </summary>
        IPageLayout PageLayout { get;}
    }
}
