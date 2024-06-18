using System;
using System.Collections.Generic;
using System.Text;
//using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 插件服务接口
    /// </summary>
    public interface IPluginsService
    {
        /// <summary>
        /// 获取插件键集合
        /// </summary>
        /// <returns>返回键集合</returns>
        String[] GetAllPluginNames();

        /// <summary>
        /// 判断PluginsService中是否包括指定键对象
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns>如果包含则返回true,否则返回 false</returns>
        Boolean ContainsPlugin(string pluginName);

        /// <summary>
        /// 根据插件名称得到其实例对象
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns>返回插件实例对象</returns>
        Object GetPluginInstance(string pluginName);

        /// <summary>
        /// 添加插件
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="objInstance">实例对象</param>
        void AddPlugin(string pluginName, Object objInstance);

        /// <summary>
        /// 移除指定名称的插件
        /// </summary>
        /// <param name="pluginName">指定的插件名称</param>
        void RemovePlugin(string pluginName);

        /// <summary>
        /// 清除所有插件项
        /// </summary>
        void Clear();

        /// <summary>
        /// 创建时初始化赋值
        /// </summary>
        void OnCreate();        
    }
}
