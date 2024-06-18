using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 文档对象服务接口
    /// </summary>
    public interface IDockDocumentService
    {

        /// <summary>
        /// 所有DockDocments集合
        /// </summary>
        Dictionary<string, DockDocument> DockDocuments{get;}
        /// <summary>
        /// 获取当前服务中是否包含此窗体
        /// </summary>
        /// <param name="dockDocumentName">窗体名称</param>
        /// <returns>如果包含则返回 true,否则返回 </returns>
        Boolean ContainsDocument(string dockDocumentName);

        /// <summary>
        /// 获取指定名称的文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        /// <returns>返回指定名称的文档对象</returns>
        DockDocument GetDockDocument(string dockDocumentName);

        /// <summary>
        /// 移除所有项
        /// </summary>
        void Clear();
        
        /// <summary>
        /// 添加文档对象,默认停靠状态为靠左停靠
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        /// <param name="dockDocument">DockDocument对象</param>
        void AddDockDocument(string dockDocumentName, DockDocument dockDocument);

        /// <summary>
        /// 添加文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        /// <param name="dockDocument">DockDocument对象</param>
        /// <param name="dockState">停靠状态</param>
        void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockState dockState);

        /// <summary>
        /// 添加文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档名称</param>
        /// <param name="dockDocument">DockDocument对象</param>
        /// <param name="parentDocument">停靠在的DockDocument对象</param>
        /// <param name="dockState">停靠状态</param>
        void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockDocument parentDocument, DockState dockState);

        /// <summary>
        /// 添加文档（以Document方式添加，并与当前Document左右分屏）
        /// </summary>
        /// <param name="dockDocumentName"></param>
        /// <param name="dockDocument"></param>
        void AddDockDocumentSplit(string dockDocumentName, DockDocument dockDocument);

        /// <summary>
        /// 把Document以tab形式停靠到另一个Document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="documentTarget"></param>
        void DockDocumentToAnother(DockDocument document, DockDocument documentTarget);

        /// <summary>
        /// 把Document设为分屏显示
        /// </summary>
        /// <param name="document"></param>
        void DockDocumentSplit(DockDocument document);

        /// <summary>
        /// 移除并关闭指定名称的文档对象
        /// </summary>
        /// <param name="dockDocumentName">容器名称</param>
        void RemoveDockDocument(string dockDocumentName);

        /// <summary>
        /// 移除指定名称的文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        void RemoveKey(string dockDocumentName);        
    }
}
