using AG.COM.SDM.Database;
using System;
using System.Data;
using System.Threading;

namespace AG.COM.SDM.Config
{
    public delegate void SdeVersionChanged(string version);
    /// <summary>
    /// 系统数据配置信息类
    /// </summary>
    public class SpatialDBConfig
    {
        public event SdeVersionChanged OnSdeVersionChanged;
        #region 私有变量
        private static SpatialDBConfig m_SpatialDBConfig;
        private DatabaseType m_SpatialType = DatabaseType.Oracle;      //OLE数据库类型       
        private string m_SpatialServer;                    //SDE服务名称
        private string m_SpatialPort;
        private string m_SpatialDataBase;                  //SDE数据库名称
        private string m_SpatialInstance;                  //SDE实例名称
        private string m_SpatialUser;                      //SDE用户
        private string m_SpatialPassword;                  //SDE密码
        private string m_SpatialVersion;                   //SDE密码

        #region 新增BS配置参数变量
        //private string m_BSServer;                 //BS端服务器名称
        //private string m_BSDataBase;               //BS端数据库名
        //private string m_BSPort;                   //BS端服务器端口
        //private string m_BSUser;                   //BS端数据库用户名
        //private string m_BSPassword;               //BS端数据库密码
        //private DatabaseType m_BSDBType = DatabaseType.Oracle;      //OLE数据库类型
        #endregion

        private string m_Version;                   //SDE当前版本

        #endregion

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SpatialDBConfig()
        {

        }

        /// <summary>
        /// 获取SpatialDBConfig实例对象
        /// </summary>
        /// <returns>返回SpatialDBConfig单件实例</returns>
        public static SpatialDBConfig GetInstance()
        {
            if (m_SpatialDBConfig == null)
            {
                m_SpatialDBConfig = new SpatialDBConfig();
            }     

            return m_SpatialDBConfig;            
        }
        /// <summary>
        /// 获取或设置SDE数据库类型
        /// </summary>
        public DatabaseType Spatial_DBType
        {
            get
            {
                return this.m_SpatialType;
            }
            set
            {
                this.m_SpatialType = value;
            }
        }
        /// <summary>
        /// 获取或设置SDE数据库服务器名
        /// </summary>
        public string Spatial_Server
        {
            get 
            {
                return this.m_SpatialServer;
            }
            set
            {
                m_SpatialServer = value;
            }
        }
        /// <summary>
        /// 获取或设置SDE数据库端口号名
        /// </summary>
        public string Spatial_Port
        {
            get
            {
                return this.m_SpatialPort;
            }
            set
            {
                m_SpatialPort = value;
            }
        }
        /// <summary>
        /// 获取或设置SDE数据库名称
        /// </summary>
        public string Spatial_DataBase
        {
            get 
            {
                return this.m_SpatialDataBase;
            }
            set 
            { 
                m_SpatialDataBase = value; 
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库实例名称
        /// 例如：5151
        /// </summary>
        public string Spatial_Instance
        {
            get
            {
                return this.m_SpatialInstance;
            }
            set 
            { 
                m_SpatialInstance = value;
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库连接用户名
        /// </summary>
        public string Spatial_User
        {
            get
            {
                return this.m_SpatialUser;
            }
            set 
            { 
                m_SpatialUser = value;
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库连接密码
        /// </summary>
        public string Spatial_Password
        {
            get
            {
                return this.m_SpatialPassword;
            }
            set 
            { 
                m_SpatialPassword = value; 
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库连接版本
        /// </summary>
        public string Spatial_Version
        {
            get
            {
                return this.m_SpatialVersion;
            }
            set 
            { 
                m_SpatialVersion = value;
            }
        }

        public string PathName
        {
            get
            {
                return $"Database Connections\\{Spatial_Server}.sde";
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
                int port = int.Parse(m_SpatialPort);
                m_Database.InitConnParam(m_SpatialType, m_SpatialServer,
                    port, m_SpatialDataBase, m_SpatialUser,
                    m_SpatialPassword);
                if (m_Database.OpenConnect(true) == false) m_Database = null;
            }
            catch
            {
                m_Database = null;
            }
        }

        /// <summary>
        /// 空间数据库配置新增一个比较操作，如果是在系统中修改参数了的话
        /// 必须进行相应的更新
        /// </summary>
        public void CompareDatabase()
        {
            if(m_Database != null )
            {
                ConnProperty connParam = m_Database.ConnProperty;
                if(connParam.DbType != Spatial_DBType)
                {
                    m_Database.DbType = Spatial_DBType;
                    connParam.DbType = Spatial_DBType;
                }
            }

        }

        #region ESRI部分
        /// <summary>
        /// 获取SDE空间连接属性
        /// </summary>
        //public IPropertySet PropertySet
        //{
        //    get
        //    {
        //        if (this.m_PropertySet == null)
        //        {
        //            //实例化属性设置对象
        //            m_PropertySet = new PropertySetClass();
        //            m_PropertySet.SetProperty("Server", this.m_SpatialServer);
        //            m_PropertySet.SetProperty("Instance", this.m_SpatialInstance);
        //            m_PropertySet.SetProperty("DataBase", this.m_SpatialDataBase);
        //            m_PropertySet.SetProperty("User", this.m_SpatialUser);
        //            m_PropertySet.SetProperty("Password", this.m_SpatialPassword);
        //            m_PropertySet.SetProperty("Version", this.m_SpatialVersion);
        //        }

        //        return this.m_PropertySet;
        //    }
        //}
        #endregion

        #region BS参数
        ///// <summary>
        ///// 获取BS数据库连接服务器ip
        ///// </summary>
        //public string BS_Server
        //{
        //    get
        //    {
        //        return this.m_BSServer;
        //    }
        //    set
        //    {
        //        m_BSServer = value;
        //    }
        //}

        ///// <summary>
        ///// 获取BS数据库端口
        ///// </summary>
        //public string BS_Port
        //{
        //    get
        //    {
        //        return this.m_BSPort;
        //    }
        //    set
        //    {
        //        m_BSPort = value;
        //    }
        //}

        ///// <summary>
        ///// 获取BS数据库连接名称
        ///// </summary>
        //public string BS_DataBase
        //{
        //    get
        //    {
        //        return this.m_BSDataBase;
        //    }
        //    set
        //    {
        //        m_BSDataBase = value;
        //    }
        //}

        ///// <summary>
        ///// 获取BS数据库用户名称
        ///// </summary>
        //public string BS_User
        //{
        //    get
        //    {
        //        return this.m_BSUser;
        //    }
        //    set
        //    {
        //        m_BSUser = value;
        //    }
        //}

        ///// <summary>
        ///// 获取BS数据库用户密码
        ///// </summary>
        //public string BS_Password
        //{
        //    get
        //    {
        //        return this.m_BSPassword;
        //    }
        //    set
        //    {
        //        m_BSPassword = value;
        //    }
        //}

        ///// <summary>
        ///// 获取BS数据库类型
        ///// </summary>
        //public DatabaseType BS_DBType
        //{
        //    get
        //    {
        //        return this.m_BSDBType;
        //    }
        //    set
        //    {
        //        m_BSDBType = value;
        //    }
        //}

        //private BSDatabase m_BSDatabase;
        ///// <summary>
        ///// 数据库操作连接
        ///// </summary>
        //public BSDatabase ConfigBSDatabase
        //{
        //    get
        //    {
        //        if (m_BSDatabase == null)
        //        {
        //            m_BSDatabase = new BSDatabase();
        //        }
        //        return m_BSDatabase;
        //    }
        //}

        //public bool hasBSWorkspace
        //{
        //    get { return m_BSWorkspace == null ? false : true; }
        //}
        ///// <summary>
        ///// 获取BS空间库工作空间
        ///// </summary>
        //public IWorkspace BSWorkspace
        //{
        //    get
        //    {
        //        string server0 = this.BSPropertySet.GetProperty("Server").ToString();
        //        if (this.m_BSWorkspace == null)
        //        {
        //            ////由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
        //            string server = this.BSPropertySet.GetProperty("Server").ToString();
        //            if (NetHelper.Ping(server) == false) return null;

        //            //实例化SDEWorkspaceFactory对象
        //            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
        //            this.m_BSWorkspace = tWorkspaceFactory.Open(this.BSPropertySet, 0);
        //            CurrentVersion = this.BSWorkspace as IVersion;
        //        }

        //        //检查SDEWorkspace是否连接，失去连接则重新连接
        //        WorkspaceAGHelper.CheckSDEWorkspaceConnect(ref this.m_BSWorkspace);

        //        return this.m_BSWorkspace;
        //    }
        //}

        ///// <summary>
        ///// 获取SDE空间连接属性
        ///// </summary>
        //public IPropertySet BSPropertySet
        //{
        //    get
        //    {
        //        string m_BSInstance = $"sde:postgresql:{m_BSServer}";
        //        if (this.m_BSPropertySet == null)
        //        {
        //            //实例化属性设置对象
        //            m_BSPropertySet = new PropertySetClass();
        //            m_BSPropertySet.SetProperty("Server", this.m_BSServer);
        //            m_BSPropertySet.SetProperty("Instance", m_BSInstance);
        //            m_BSPropertySet.SetProperty("DataBase", this.m_BSDataBase);
        //            m_BSPropertySet.SetProperty("User", this.m_BSUser);
        //            m_BSPropertySet.SetProperty("Password", this.m_BSPassword);
        //            m_BSPropertySet.SetProperty("Version", this.m_SpatialVersion);
        //        }

        //        return this.m_BSPropertySet;
        //    }
        //}

        #endregion

    }




}
