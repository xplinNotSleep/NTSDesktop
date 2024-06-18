using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 集成框架事件接口
    /// </summary>
    public interface IFrameworkEvents
    {
        /// <summary>
        /// 激活文档窗口发生变化事件
        /// </summary>
        event MapDocumentChangedEventHandler MapDocumentChanged;

        /// <summary>
        /// 当前图层发生变化事件
        /// </summary>
        event CurrentLayerChangedEventHandler CurrentLayerChanged;

        /// <summary>
        /// 鹰眼视图缩放范围发生变化事件
        /// </summary>
        event EagleViewChangedEventHandler EagleViewChanged;

        /// <summary>
        /// 插件对象单击事件
        /// </summary>
        event PlugCommandClikedEventHandler PlugCommandCliked;
    }
}
