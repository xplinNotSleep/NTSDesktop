using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 定义当前图层发生改变时的委托类型
    /// </summary>
    /// <param name="sender">对象</param>
    /// <param name="e">事件参数</param>
    public delegate void CurrentLayerChangedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// 定义鹰眼视图范围发生变化时的委托类型
    /// </summary>
    /// <param name="newEnvelope">新的视图范围</param>
    /// <param name="e">事件参数</param>
    public delegate void EagleViewChangedEventHandler(object newEnvelope,EventArgs e);

    /// <summary>
    /// 当前激活文档发生变化时的委托类型
    /// </summary>
    /// <param name="sender">文档对象</param>
    /// <param name="e">事件参数</param>
    public delegate void MapDocumentChangedEventHandler(object sender, EventArgs e);

    //public delegate void MapDocumentOpenEventHandler(object sender, EventArgs e);

    //public delegate void MapDocumentSaveEventHandler(object sender, EventArgs e);

    //public delegate void MapDocumentNewEventHandler(object sender,

    /// <summary>
    /// 插件对象单击处理事件的委托类型
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PlugCommandClikedEventHandler(object sender,EventArgs e);

    /// <summary>
    /// 图层视图控制鼠标单击事件的委托类型
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LayerControlMouseDownEventHandler(object sender, EventArgs e);

}
