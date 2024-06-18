using System.Drawing;
using System;
using System.Windows.Forms;
using AG.COM.SDM.Plugins.Common;
using AG.COM.SDM.Plugins.Demo;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// 数据视图图层上下文菜单 插件类
    /// </summary>
    public sealed class TocDefaultContextMenu:BaseContextMenu
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TocDefaultContextMenu()
            : base()
        {
            this.AddItem(new CmdTheme2010Blue(), -1, false, ToolStripItemDisplayStyle.ImageAndText);
            this.AddItem(new CmdTheme2013Gray(), -1, false, ToolStripItemDisplayStyle.ImageAndText);

        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            //this.m_hookHelper.Hook = hook;
        }


    }
}
