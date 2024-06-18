using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 地图操作工具右键快捷菜单接口
    /// </summary>
    public interface IToolContextMenu : IPlugin
    {
        /// <summary>
        /// 获取右键快捷菜单
        /// </summary>
        ContextMenuStrip ContextMenuStrip { get;}
    }
}
