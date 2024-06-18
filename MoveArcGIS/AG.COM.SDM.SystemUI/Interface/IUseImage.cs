using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 存在大小菜单之分，图标大小分别是32和16  
    /// 新框架（Dev）使用
    /// </summary>
    public interface IUseImage
    {
        /// <summary>
        /// 插件Image(16*16)
        /// </summary>
        Image Image16 { get; }

        /// <summary>
        /// 插件Image(32*32)
        /// </summary>
        Image Image32 { get; }
    }
}
