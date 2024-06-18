using AG.COM.SDM.Database;
using System;
using System.Threading;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 系统数据配置信息类
    /// </summary>
    public class OleDBConfig
    {
        #region 私有变量
        private static OleDBConfig m_OleDBConfig;
        private string m_OLEServer;                 //OLE服务器
        private string m_OLEDataBase;               //OLE数据库名
        private string m_OLEPort;                   //OLE端口
        private string m_OLEUser;                   //OLE用户名称
        private string m_OLEPassword;               //OLE用户密码
        private DatabaseType  m_OLEDBType=DatabaseType.Oracle;      //OLE数据库类型
        #endregion

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private OleDBConfig()
        {

        }

        /// <summary>
        /// 获取OleDBConfig实例对象
        /// </summary>
        /// <returns>返回OleDBConfig单件实例</returns>
        public static OleDBConfig GetInstance()
        {
            if (m_OleDBConfig == null)
            {
                m_OleDBConfig = new OleDBConfig();
            }     

            return m_OleDBConfig;            
        }

        /// <summary>
        /// 获取或设置OLE数据库类型
        /// </summary>
        public DatabaseType DatabaseType
        {
            get
            {
                return this.m_OLEDBType;
            }
            set
            {
                this.m_OLEDBType = value;
            }
        }

        /// <summary>
        /// 获取或设置OLE服务器名称或IP地址
        /// </summary>
        public string OLE_Server
        {
            get
            {
                return this.m_OLEServer;
            }
            set
            {
                this.m_OLEServer = value;
            }
        }

        /// <summary>
        /// 获取或设置业务数据库名称
        /// </summary>
        public string OLE_DataBase
        {
            get
            {
                return this.m_OLEDataBase;
            }
            set 
            { 
                m_OLEDataBase = value; 
            }
        }

        /// <summary>
        /// 获取或设置业务数据库端口
        /// </summary>
        public string OLE_Port
        {
            get
            {
                return this.m_OLEPort;
            }
            set
            {
                m_OLEPort = value;
            }
        }

        /// <summary>
        /// 获取业务数据库连接用户名称
        /// </summary>
        public string OLE_User
        {
            get
            {
                return this.m_OLEUser;
            }
            set { m_OLEUser = value; }
        }

        /// <summary>
        /// 获取业务数据库连接密码
        /// </summary>
        public string OLE_Password
        {
            get
            {
                return this.m_OLEPassword;
            }
            set
            { 
                m_OLEPassword = value;
            }
        }

        private AdoDatabase m_Database;
        /// <summary>
        /// 数据库操作连接
        /// </summary>
        public AdoDatabase ConfigDatabase
        {
            get
            {
                if(m_Database==null)
                {
                    RefreshDatabase();
                }
                return m_Database;
            }
        }

        public void RefreshDatabase()
        {
            m_Database = new AdoDatabase();
            try
            {
                int port = int.Parse(m_OLEPort);
                m_Database.InitConnParam(m_OLEDBType, m_OLEServer,
                    port, m_OLEDataBase, m_OLEUser,
                    m_OLEPassword);
                if (m_Database.OpenConnect(true) == false) m_Database = null;
            }
            catch
            {
                m_Database = null;
            }
        }

    }
    
}
