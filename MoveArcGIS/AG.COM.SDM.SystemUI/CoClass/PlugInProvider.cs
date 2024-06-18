using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 插件信息提供类
    /// </summary>
    public class PlugInProvider
    {
        /// <summary>
        /// 获取插件信息对象（PlugInfoConfig）
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
        /// 根据指定的插件信息生成PlugInfoConfig
        /// </summary>
        /// <param name="strPlugInfo">指定的插件信息</param>
        /// <returns>返回插件信息</returns>
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
                throw (new Exception(string.Format("[{0}] 格式不符合要求。\r\n 请按程序集、类型、节点类型依次填写，以‘|’或‘：’作为分隔符。", strPlugInfo)));
            }
        }

        /// <summary>
        /// 获取指定插件配置信息类的插件实例对象
        /// </summary>
        /// <param name="pluginfo"><see cref="PlugInfoConfig"/>pluginfo</param>
        /// <returns>返回实例对象</returns>
        public static object GetInstance(PlugInfoConfig pluginfo)
        {
            if (pluginfo == null) return null;
            if (string.IsNullOrEmpty(pluginfo.PlugAssembly)) return null;
            if (string.IsNullOrEmpty(pluginfo.PlugType)) return null;

            //指定程序集返回指定类型的实例对象
            return GetInstance(pluginfo.PlugAssembly, pluginfo.PlugType, pluginfo.SubType);
        }

        /// <summary>
        /// 根据指定对象返回节点类型
        /// </summary>
        /// <param name="obj">指定对象</param>
        /// <returns>返回EnumNodeType</returns>
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
                throw (new Exception(string.Format("[{0}]对象不能转换为整型", obj)));
            }
        }

        /// <summary>
        /// 从指定程序集返回指定类型的实例对象
        /// </summary>
        /// <param name="assembly">程序集名称</param>
        /// <param name="typename">类型名称</param>
        /// <returns>正常情况下返回实例对象,否则返回null</returns>
        public static object GetInstance(string assembly, string typename)
        {
            return GetInstance(assembly, typename, -1);
        }

        /// <summary>
        /// 从指定程序集返回指定类型的实例对象
        /// </summary>
        /// <param name="assembly">程序集名称</param>
        /// <param name="typename">类型名称</param>
        /// <param name="subtype">子类型</param>
        /// <returns>正常情况下返回实例对象,否则返回null</returns>
        public static object GetInstance(string assembly, string typename, int subtype)
        {
            object objInstance = null;

            //从指定文件路径实例程序集对象
            Assembly pAssembly = Assembly.LoadFrom(GetAssemblyPath(assembly));
            //创建指定类型的对象
            objInstance = pAssembly.CreateInstance(typename);
            if (subtype != -1)
            {
                //查询接口
                ICommand pCommand = objInstance as ICommand;
                if (pCommand != null)
                {
                    (pCommand as ICommandSubType).SetSubType(subtype);
                }
            }

            return objInstance;
        }

        /// <summary>
        /// 返回程序集文件的绝对路径
        /// </summary>
        /// <param name="assembly">程序集名称</param>
        /// <returns>返回程序集路径</returns>
        private static string GetAssemblyPath(string assembly)
        {
            return string.Format(@"{0}\{1}", Application.StartupPath, assembly);
        }
    }
}
