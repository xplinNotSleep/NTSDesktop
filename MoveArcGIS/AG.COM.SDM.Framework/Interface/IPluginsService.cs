using System;
using System.Collections.Generic;
using System.Text;
//using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// �������ӿ�
    /// </summary>
    public interface IPluginsService
    {
        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <returns>���ؼ�����</returns>
        String[] GetAllPluginNames();

        /// <summary>
        /// �ж�PluginsService���Ƿ����ָ��������
        /// </summary>
        /// <param name="pluginName">�������</param>
        /// <returns>��������򷵻�true,���򷵻� false</returns>
        Boolean ContainsPlugin(string pluginName);

        /// <summary>
        /// ���ݲ�����Ƶõ���ʵ������
        /// </summary>
        /// <param name="pluginName">�������</param>
        /// <returns>���ز��ʵ������</returns>
        Object GetPluginInstance(string pluginName);

        /// <summary>
        /// ��Ӳ��
        /// </summary>
        /// <param name="pluginName">�������</param>
        /// <param name="objInstance">ʵ������</param>
        void AddPlugin(string pluginName, Object objInstance);

        /// <summary>
        /// �Ƴ�ָ�����ƵĲ��
        /// </summary>
        /// <param name="pluginName">ָ���Ĳ������</param>
        void RemovePlugin(string pluginName);

        /// <summary>
        /// ������в����
        /// </summary>
        void Clear();

        /// <summary>
        /// ����ʱ��ʼ����ֵ
        /// </summary>
        void OnCreate();        
    }
}
