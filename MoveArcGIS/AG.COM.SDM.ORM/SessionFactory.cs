using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace AG.COM.SDM.DAL
{
    /// <summary>
    /// 会话工厂（单件类），提供创建会话
    /// </summary>
    public class SessionFactory
    {
        private static Dictionary<string, ISessionFactory> sessionsDict = new Dictionary<string, ISessionFactory>();
        private static Configuration cfg;
        static readonly object padlock = new object();

        /// <summary>
        /// 会话参数管理类
        /// </summary>
        private static SessionParameterManage m_SessionParaManager = new SessionParameterManage();

        /// <summary>
        /// 获取或设置会话参数管理类对象
        /// </summary>
        public static SessionParameterManage SessionParaManager
        {
            get { return m_SessionParaManager; }
            set { m_SessionParaManager = value; }
        }

        private static IDictionary<string, string> m_ConnProps = new Dictionary<string, string>();

        public static IDictionary<string, string> ConnProps
        {
            get { return m_ConnProps; }
            set { m_ConnProps = value; }
        }

        /// <summary>
        /// 初始化会话工厂类
        /// </summary>
        public SessionFactory()
        {

        }


        /// <summary>
        /// 打开会话连接
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns>返回会话连接</returns>
        public static ISession OpenSession(string AssemblyName)
        {
            if (!sessionsDict.ContainsKey(AssemblyName))
            {
                lock (padlock)
                {
                    BuildSessionFactory(AssemblyName);
                }
            }
            ISessionFactory sessions = sessionsDict[AssemblyName];
            return sessions.OpenSession();
        }

        public static void CheckOpenSession(string AssemblyName)
        {
            if (!sessionsDict.ContainsKey(AssemblyName))
            {
                lock (padlock)
                {
                    BuildSessionFactory(AssemblyName);
                }
            }
            ISessionFactory sessions = sessionsDict[AssemblyName];
            sessions.OpenSession();

            //sessions.Close();
        }

        public static bool IsConnect(string AssemblyName)
        {
            cfg = new Configuration();
            cfg.SetProperties(ConnProps);
            cfg.AddAssembly(AssemblyName);
            ISessionFactory sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory.OpenSession().IsConnected;
        }

        public static bool IsConnectSQLite(string AssemblyName, string dbPath)
        {
            IDictionary<string, string> connProps = new Dictionary<string, string>();
            string connStr = string.Format("Data Source={0}", dbPath);
            connProps.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            connProps.Add("connection.connection_string", connStr);
            connProps.Add("query.substitutions", "true 1, false 0, yes 'Y', no 'N'");
            connProps.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            cfg = new Configuration();
            cfg.SetProperties(connProps);
            cfg.AddAssembly(AssemblyName);
            ISessionFactory sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory.OpenSession().IsConnected;
        }

        /// <summary>
        /// 从指定程序集创建会话工厂
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        private static void BuildSessionFactory(string AssemblyName)
        {
            if (!sessionsDict.ContainsKey(AssemblyName))
            {
                //获取数据库连接属性设置
                //IDictionary<string, string> connProps = GetConnectProps(AssemblyName);
                //实例化配置信息类
                cfg = new Configuration();
                cfg.SetProperties(ConnProps);
                cfg.AddAssembly(AssemblyName);
                sessionsDict.Add(AssemblyName, cfg.BuildSessionFactory());
            }
        }

        /// <summary>
        /// 获取数据库连接属性设置
        /// </summary>
        private static IDictionary<string, string> GetConnectProps(string assemblyName)
        {
            IDictionary<string, string> connProps = new Dictionary<string, string>();
            if ((SessionParaManager == null) || (SessionParaManager[assemblyName] == null))
            {
                throw new Exception("实体程序集名称为" + assemblyName + "的无数据库参数配置！");
            }
            SessionParameter sessionParameter = SessionParaManager[assemblyName];
            return connProps;
        }

        /// <summary>
        /// 删除SessionFactory
        /// </summary>
        /// <param name="AssemblyName"></param>
        public static void RemoveSessionFactory(string AssemblyName)
        {
            if (string.IsNullOrEmpty(AssemblyName)) return;

            if (sessionsDict.ContainsKey(AssemblyName))
            {
                sessionsDict.Remove(AssemblyName);
            }

            SessionParaManager.Delete(AssemblyName);
        }

        /// <summary>
        /// 清除SessionFactory
        /// </summary>
        public static void ClearSessionFactory()
        {
            sessionsDict.Clear();

            SessionParaManager.Clear();
        }

        /// <summary>
        /// 刷新SessionFactory，在修改OLE数据库参数后调用
        /// </summary>
        private void RefreshSessionFactory(SessionParameter frameSessionParameter)
        {
            //清空SessionFactory
            ClearSessionFactory();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssemblyName"></param>
        public static void CreateTable(string AssemblyName)
        {
            ISession session = OpenSession(AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);
            schemaExport.Create(true, true);
            transaction.Commit();
            session.Close();

        }
    }
}
