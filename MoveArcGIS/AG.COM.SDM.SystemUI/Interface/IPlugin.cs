using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 插件基类接口
    /// </summary>
    public interface  IPlugin
    {        
        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get;}

        /// <summary>
        /// 插件显示名称
        /// </summary>
        string Caption { get;}

        /// <summary>
        /// 获取插件的可用状态
        /// </summary>
        bool Enabled { get;}

        /// <summary>
        /// 创建时初始化赋值
        /// </summary>
        /// <param name="hook">hook对象</param>
        void OnCreate(object framework);
    }
}
