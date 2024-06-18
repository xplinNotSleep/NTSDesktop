using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace AG.COM.SDM.DAL
{
    /// <summary>
    /// �Ự�����������ࣩ���ṩ�����Ự
    /// </summary>
    public class SessionFactory
    {
        private static Dictionary<string, ISessionFactory> sessionsDict = new Dictionary<string, ISessionFactory>();
        private static Configuration cfg;
        static readonly object padlock = new object();

        /// <summary>
        /// �Ự����������
        /// </summary>
        private static SessionParameterManage m_SessionParaManager = new SessionParameterManage();

        /// <summary>
        /// ��ȡ�����ûỰ�������������
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
        /// ��ʼ���Ự������
        /// </summary>
        public SessionFactory()
        {

        }


        /// <summary>
        /// �򿪻Ự����
        /// </summary>
        /// <param name="AssemblyName">��������</param>
        /// <returns>���ػỰ����</returns>
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
        /// ��ָ�����򼯴����Ự����
        /// </summary>
        /// <param name="AssemblyName">��������</param>
        private static void BuildSessionFactory(string AssemblyName)
        {
            if (!sessionsDict.ContainsKey(AssemblyName))
            {
                //��ȡ���ݿ�������������
                //IDictionary<string, string> connProps = GetConnectProps(AssemblyName);
                //ʵ����������Ϣ��
                cfg = new Configuration();
                cfg.SetProperties(ConnProps);
                cfg.AddAssembly(AssemblyName);
                sessionsDict.Add(AssemblyName, cfg.BuildSessionFactory());
            }
        }

        /// <summary>
        /// ��ȡ���ݿ�������������
        /// </summary>
        private static IDictionary<string, string> GetConnectProps(string assemblyName)
        {
            IDictionary<string, string> connProps = new Dictionary<string, string>();
            if ((SessionParaManager == null) || (SessionParaManager[assemblyName] == null))
            {
                throw new Exception("ʵ���������Ϊ" + assemblyName + "�������ݿ�������ã�");
            }
            SessionParameter sessionParameter = SessionParaManager[assemblyName];
            return connProps;
        }

        /// <summary>
        /// ɾ��SessionFactory
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
        /// ���SessionFactory
        /// </summary>
        public static void ClearSessionFactory()
        {
            sessionsDict.Clear();

            SessionParaManager.Clear();
        }

        /// <summary>
        /// ˢ��SessionFactory�����޸�OLE���ݿ���������
        /// </summary>
        private void RefreshSessionFactory(SessionParameter frameSessionParameter)
        {
            //���SessionFactory
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
