using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 插件服务对象类
    /// </summary>
    public class PluginsService:IPluginsService
    {
        private static IPluginsService m_PluginsService;        
        private IFramework m_Framework;
        private Dictionary<string, object> m_Plugins;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private PluginsService(IFramework mFramework)
        {
            this.m_Framework = mFramework;
            this.m_Plugins = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取插件服务实例对象
        /// </summary>
        /// <returns>返回插件服务单件实例</returns>
        public static IPluginsService GetInstance(IFramework mFramework)
        {
            if (m_PluginsService == null)
            {
                m_PluginsService = new PluginsService(mFramework);
            }

            return m_PluginsService;
        }

        #region IPluginsService 成员
        /// <summary>
        /// 获取插件键集合
        /// </summary>
        /// <returns>返回键集合</returns>
        public string[] GetAllPluginNames()
        {
            string[] strPluginNames = new string[m_Plugins.Count];
            m_Plugins.Keys.CopyTo(strPluginNames, 0);
            return strPluginNames;
        }

        /// <summary>
        /// 判断PluginsService中是否包括指定键对象
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns>如果包含则返回true,否则返回 false</returns>
        public bool ContainsPlugin(string pluginName)
        {
            return m_Plugins.ContainsKey(pluginName);
        }

        /// <summary>
        /// 根据插件名称得到其实例对象
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns>返回插件实例对象</returns>
        public object GetPluginInstance(string pluginName)
        {
            if (this.ContainsPlugin(pluginName) == true)
                return m_Plugins[pluginName];
            else
                return null;
        }

        /// <summary>
        /// 添加插件
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="objInstance">实例对象</param>
        public void AddPlugin(string pluginName, object objInstance)
        {
            if (ContainsPlugin(pluginName)==true) return;
            this.m_Plugins.Add(pluginName, objInstance);
        }

        /// <summary>
        /// 移除指定名称的插件
        /// </summary>
        /// <param name="pluginName">指定插件的名称</param>
        public void RemovePlugin(string pluginName)
        {
            if (this.ContainsPlugin(pluginName) == true)
                this.m_Plugins.Remove(pluginName);
        }

        /// <summary>
        /// 清除所有插件项
        /// </summary>
        public void Clear()
        {
            this.m_Plugins.Clear();
        }

        /// <summary>
        /// 创建时初始化赋值
        /// </summary>
        public void OnCreate()
        {
            foreach (KeyValuePair<string, object> pObj in m_Plugins)
            {
                ICommand pCommand = pObj.Value as ICommand;
                pCommand.OnCreate(this.m_Framework);
            }
        }
        #endregion
    }
}
