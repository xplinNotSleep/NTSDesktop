using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ������������
    /// </summary>
    public class PluginsService:IPluginsService
    {
        private static IPluginsService m_PluginsService;        
        private IFramework m_Framework;
        private Dictionary<string, object> m_Plugins;

        /// <summary>
        /// ˽�й��캯��
        /// </summary>
        private PluginsService(IFramework mFramework)
        {
            this.m_Framework = mFramework;
            this.m_Plugins = new Dictionary<string, object>();
        }

        /// <summary>
        /// ��ȡ�������ʵ������
        /// </summary>
        /// <returns>���ز�����񵥼�ʵ��</returns>
        public static IPluginsService GetInstance(IFramework mFramework)
        {
            if (m_PluginsService == null)
            {
                m_PluginsService = new PluginsService(mFramework);
            }

            return m_PluginsService;
        }

        #region IPluginsService ��Ա
        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <returns>���ؼ�����</returns>
        public string[] GetAllPluginNames()
        {
            string[] strPluginNames = new string[m_Plugins.Count];
            m_Plugins.Keys.CopyTo(strPluginNames, 0);
            return strPluginNames;
        }

        /// <summary>
        /// �ж�PluginsService���Ƿ����ָ��������
        /// </summary>
        /// <param name="pluginName">�������</param>
        /// <returns>��������򷵻�true,���򷵻� false</returns>
        public bool ContainsPlugin(string pluginName)
        {
            return m_Plugins.ContainsKey(pluginName);
        }

        /// <summary>
        /// ���ݲ�����Ƶõ���ʵ������
        /// </summary>
        /// <param name="pluginName">�������</param>
        /// <returns>���ز��ʵ������</returns>
        public object GetPluginInstance(string pluginName)
        {
            if (this.ContainsPlugin(pluginName) == true)
                return m_Plugins[pluginName];
            else
                return null;
        }

        /// <summary>
        /// ��Ӳ��
        /// </summary>
        /// <param name="pluginName">�������</param>
        /// <param name="objInstance">ʵ������</param>
        public void AddPlugin(string pluginName, object objInstance)
        {
            if (ContainsPlugin(pluginName)==true) return;
            this.m_Plugins.Add(pluginName, objInstance);
        }

        /// <summary>
        /// �Ƴ�ָ�����ƵĲ��
        /// </summary>
        /// <param name="pluginName">ָ�����������</param>
        public void RemovePlugin(string pluginName)
        {
            if (this.ContainsPlugin(pluginName) == true)
                this.m_Plugins.Remove(pluginName);
        }

        /// <summary>
        /// ������в����
        /// </summary>
        public void Clear()
        {
            this.m_Plugins.Clear();
        }

        /// <summary>
        /// ����ʱ��ʼ����ֵ
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
