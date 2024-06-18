using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 文档对象接口类
    /// </summary>
    public interface IDocumentView
    {    
        /// <summary>
        /// 获取或设置文档对象标题名称
        /// </summary>
        string DocumentTitle { get;set;}
        /// <summary>
        /// 获取文档对象类型
        /// </summary>
        EnumDocumentType DocumentType { get;}
        /// <summary>
        /// 获取当前文档关键对象
        /// </summary>
        Object Hook { get;}
    }
}
