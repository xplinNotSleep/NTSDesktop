using System;
using System.Reflection;
using System.Xml;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 系统表管理创建工厂抽象类
    /// </summary>
    public abstract class AbstractFactory
    {
        /// <summary>
        /// 获取系统表管理工厂实例
        /// </summary>
        /// <returns>工厂类</returns>
        public static AbstractFactory GetInstance()
        {
            string strConfigPath = CommonConstString.STR_ConfigPath + "\\StartConfig.xml";
            AbstractFactory factory = null;
            if (!System.IO.File.Exists(strConfigPath))
            {
                factory = new FrameFactory();
                return factory;
            }
            else
            {
                try
                {
                    XmlDocument tDataBaseInfoXml = new XmlDocument();
                    tDataBaseInfoXml.Load(strConfigPath);
                    //数据源参数设置写入XML文件
                    string dllName = tDataBaseInfoXml.DocumentElement.SelectSingleNode("DllInfo").InnerText;
                    string methInfo = tDataBaseInfoXml.DocumentElement.SelectSingleNode("MethInfo").InnerText;
                    object instance = CreateObject(dllName, methInfo);
                    factory = instance as AbstractFactory;
                }
                catch (Exception err)
                {
                    throw err;
                }
                return factory;
            }
        }

        /// <summary>
        /// 创建岗位管理
        /// </summary>
        /// <returns>岗位管理接口</returns>
        public abstract IPost CreatePost();

        /// <summary>
        /// 创建用户管理
        /// </summary>
        /// <returns>用户管理接口</returns>
        public abstract IUser CreateUser();

        /// <summary>
        /// 创建部门管理
        /// </summary>
        /// <returns>部门管理接口</returns>
        public abstract IDepartment CreateDepartment();

        /// <summary>
        /// 常见菜单管理
        /// </summary>
        /// <returns>菜单管理接口</returns>
        public abstract IMenu CreateMenu();

        /// <summary>
        /// 创建角色管理
        /// </summary>
        /// <returns>角色管理接口</returns>
        public abstract IRole CreateRole();

        /// <summary>
        /// 反射创建对象
        /// </summary>
        /// <param name="AssemblyPath">程序集名称</param>
        /// <param name="ClassNamespace">类型名称</param>
        /// <returns>OBJECT对象</returns>
        private static object CreateObject(string AssemblyPath, string ClassNamespace)
        {
            object objType = null;
            if (objType == null)
            {
                try
                {
                    Assembly tAssembly = Assembly.Load(AssemblyPath);
                    objType = tAssembly.CreateInstance(ClassNamespace);//反射创建
                }
                catch
                {
                    throw;
                }
            }
            return objType;
        }
    }
}

