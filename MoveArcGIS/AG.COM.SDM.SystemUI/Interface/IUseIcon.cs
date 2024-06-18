using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// QIOS库的控件的图标都是ico格式，实现此接口从而保证图标质量
    /// 由于Ribbon界面存在大小菜单之分，因此图标也分别需要32和16   
    /// </summary>
    public interface IUseIcon
    {
        /// <summary>
        /// ico图标对象(16*16)
        /// </summary>
        Icon Icon16 { get; }

        /// <summary>
        /// ico图标对象(32*32)
        /// </summary>
        Icon Icon32 { get; }
    }
}
