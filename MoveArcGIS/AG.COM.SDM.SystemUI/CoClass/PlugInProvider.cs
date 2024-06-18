using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �����Ϣ�ṩ��
    /// </summary>
    public class PlugInProvider
    {
        /// <summary>
        /// ��ȡ�����Ϣ����PlugInfoConfig��
        /// </summary>
        /// <param name="tItemCommandInfo"></param>
        /// <returns></returns>
        public static PlugInfoConfig GetPlugInfoConfig(ItemCommandInfo tItemCommandInfo)
        {
            PlugInfoConfig pluginfo = new PlugInfoConfig();
            pluginfo.Caption = tItemCommandInfo.Caption;
            pluginfo.PlugAssembly = tItemCommandInfo.PlugAssembly;
            pluginfo.PlugType = tItemCommandInfo.PlugType;

            return pluginfo;
        }

        /// <summary>
        /// ����ָ���Ĳ����Ϣ����PlugInfoConfig
        /// </summary>
        /// <param name="strPlugInfo">ָ���Ĳ����Ϣ</param>
        /// <returns>���ز����Ϣ</returns>
        public static PlugInfoConfig GetPlugInfoConfig(string strPlugInfo)
        {
            string[] strAttributes = strPlugInfo.Split('|', ':');
            if (strAttributes.Length == 3)
            {
                PlugInfoConfig pluginfo = new PlugInfoConfig();
                pluginfo.PlugAssembly = strAttributes[0];
                pluginfo.PlugType = strAttributes[1];
                return pluginfo;
            }
            else
            {
                throw (new Exception(string.Format("[{0}] ��ʽ������Ҫ��\r\n �밴���򼯡����͡��ڵ�����������д���ԡ�|���򡮣�����Ϊ�ָ�����", strPlugInfo)));
            }
        }

        /// <summary>
        /// ��ȡָ�����������Ϣ��Ĳ��ʵ������
        /// </summary>
        /// <param name="pluginfo"><see cref="PlugInfoConfig"/>pluginfo</param>
        /// <returns>����ʵ������</returns>
        public static object GetInstance(PlugInfoConfig pluginfo)
        {
            if (pluginfo == null) return null;
            if (string.IsNullOrEmpty(pluginfo.PlugAssembly)) return null;
            if (string.IsNullOrEmpty(pluginfo.PlugType)) return null;

            //ָ�����򼯷���ָ�����͵�ʵ������
            return GetInstance(pluginfo.PlugAssembly, pluginfo.PlugType, pluginfo.SubType);
        }

        /// <summary>
        /// ����ָ�����󷵻ؽڵ�����
        /// </summary>
        /// <param name="obj">ָ������</param>
        /// <returns>����EnumNodeType</returns>
        public static EnumNodeType GetNodeType(object obj)
        {
            if (obj == null) return EnumNodeType.None;

            try
            {
                int i = Convert.ToInt16(obj);
                return (EnumNodeType)i;
            }
            catch
            {
                throw (new Exception(string.Format("[{0}]������ת��Ϊ����", obj)));
            }
        }

        /// <summary>
        /// ��ָ�����򼯷���ָ�����͵�ʵ������
        /// </summary>
        /// <param name="assembly">��������</param>
        /// <param name="typename">��������</param>
        /// <returns>��������·���ʵ������,���򷵻�null</returns>
        public static object GetInstance(string assembly, string typename)
        {
            return GetInstance(assembly, typename, -1);
        }

        /// <summary>
        /// ��ָ�����򼯷���ָ�����͵�ʵ������
        /// </summary>
        /// <param name="assembly">��������</param>
        /// <param name="typename">��������</param>
        /// <param name="subtype">������</param>
        /// <returns>��������·���ʵ������,���򷵻�null</returns>
        public static object GetInstance(string assembly, string typename, int subtype)
        {
            object objInstance = null;

            //��ָ���ļ�·��ʵ�����򼯶���
            Assembly pAssembly = Assembly.LoadFrom(GetAssemblyPath(assembly));
            //����ָ�����͵Ķ���
            objInstance = pAssembly.CreateInstance(typename);
            if (subtype != -1)
            {
                //��ѯ�ӿ�
                ICommand pCommand = objInstance as ICommand;
                if (pCommand != null)
                {
                    (pCommand as ICommandSubType).SetSubType(subtype);
                }
            }

            return objInstance;
        }

        /// <summary>
        /// ���س����ļ��ľ���·��
        /// </summary>
        /// <param name="assembly">��������</param>
        /// <returns>���س���·��</returns>
        private static string GetAssemblyPath(string assembly)
        {
            return string.Format(@"{0}\{1}", Application.StartupPath, assembly);
        }
    }
}
