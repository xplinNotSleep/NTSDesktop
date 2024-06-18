using AG.COM.SDM.Database;
using System;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// OLE数据库连接属性
    /// </summary>
    [Serializable]
    public class OleConnProperty
    {
        private string m_OLEServer;                 //OLE服务器
        private string m_OLEDataBase;               //OLE数据库名
        private string m_OLEPort;                   //OLE端口
        private string m_OLEUser;                   //OLE用户名称
        private string m_OLEPassword;               //OLE用户密码
        private string m_OLEName;                   //OLE标识
        private DatabaseType m_OLEDBType = DatabaseType.Oracle;      //OLE数据库类型

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
            set { m_OLEDataBase = value; }
        }

        /// <summary>
        /// 获取或设置业务数据库连接端口
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
        /// 获取或设置业务数据库连接用户名称
        /// </summary>
        public string OLE_User
        {
            get
            {
                return this.m_OLEUser;
            }
            set
            {
                m_OLEUser = value;
            }
        }

        /// <summary>
        /// 获取或设置业务数据库连接密码
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

        /// <summary>
        /// 获取或设置OLE标识
        /// </summary>
        public string OLE_Name
        {
            get
            {
                return this.m_OLEName;
            }
            set
            {
                m_OLEName = value;
            }
        }
    }
}