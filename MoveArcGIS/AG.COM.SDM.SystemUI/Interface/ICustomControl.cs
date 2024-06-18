using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 自定义控件接口类
    /// </summary>
    public interface ICustomControl : IPlugin
    {
        /// <summary>
        /// 获取自定义控件
        /// </summary>
        Control CustomControl { get; }
    }
}
