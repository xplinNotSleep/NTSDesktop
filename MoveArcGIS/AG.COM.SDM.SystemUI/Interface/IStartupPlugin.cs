using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 系统启动插件接口
    /// </summary>
    public interface IStartupPlugin:IPlugin
    {
        /// <summary>
        /// 启动项描述信息
        /// </summary>
        string Description { get;}

        /// <summary>
        /// 系统启动处理
        /// </summary>
        void Startup();
    }
}
