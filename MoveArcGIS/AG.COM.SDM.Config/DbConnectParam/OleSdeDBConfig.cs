using System;
using System.Threading;

namespace AG.COM.SDM.Database
{
    public delegate void SdeVersionChanged(string version);
    /// <summary>
    /// 系统数据配置信息类
    /// </summary>
    public class OleSdeDBConfig
    {
        public event SdeVersionChanged OnSdeVersionChanged;
        #region 私有变量
        private static OleSdeDBConfig m_OleSdeDBConfig;
        private DatabaseType m_SDEBType = DatabaseType.Oracle;      //OLE数据库类型       
        private string m_SDEServer;                    //SDE服务名称
        private string m_SDEDataBase;                  //SDE数据库名称
        private string m_SDEInstance;                  //SDE实例名称
        private string m_SDEUser;                      //SDE用户
        private string m_SDEPassword;                  //SDE密码
        private string m_SDEVersion;                   //SDE密码

        private OleConnPropertyManager m_OleConnManager = new OleConnPropertyManager();     //OLE配置管理类
        private string m_OLEServer;                 //OLE服务器
        private string m_OLEDataBase;               //OLE数据库名
        private string m_OLEPort;                   //OLE端口
        private string m_OLEUser;                   //OLE用户名称
        private string m_OLEPassword;               //OLE用户密码
        private DatabaseType  m_OLEDBType=DatabaseType.Oracle;      //OLE数据库类型

        //新增BS配置参数变量
        private string m_BSServer;                 //BS端服务器名称
        private string m_BSDataBase;               //BS端数据库名
        private string m_BSPort;                   //BS端服务器端口
        private string m_BSUser;                   //BS端数据库用户名
        private string m_BSPassword;               //BS端数据库密码
        private DatabaseType m_BSDBType = DatabaseType.Oracle;      //OLE数据库类型

        private OracleDatabase m_OracleDatabase;  //Oracle数据库连接

        private string m_OracleSID;      
        private IPropertySet m_PropertySet;         //SDE连接属性
        private IPropertySet m_BSPropertySet;
        private IWorkspace m_Workspace;             //SDE工作空间
        private string m_Version;                   //SDE当前版本
        private IWorkspace m_BSWorkspace;           //BS库工作空间

        //Minio服务器配置参数
        private string m_MinioServerURL;
        private string m_MinioAccessName;
        private string m_MinioPassWord;
        private string m_MinioServerBucket;

        #endregion

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private OleSdeDBConfig()
        {

        }

        /// <summary>
        /// 获取OleSdeDBConfig实例对象
        /// </summary>
        /// <returns>返回OleSdeDBConfig单件实例</returns>
        public static OleSdeDBConfig GetInstance()
        {
            if (m_OleSdeDBConfig == null)
            {
                m_OleSdeDBConfig = new OleSdeDBConfig();
            }     

            return m_OleSdeDBConfig;            
        }
        /// <summary>
        /// 获取或设置SDE数据库类型
        /// </summary>
        public DatabaseType Spatial_DBType
        {
            get
            {
                return this.m_SDEBType;
            }
            set
            {
                this.m_SDEBType = value;
            }
        }
        /// <summary>
        /// 获取或设置SDE数据库服务器名
        /// </summary>
        public string Spatial_Server
        {
            get 
            {
                return this.m_SDEServer;
            }
            set
            {
                m_SDEServer = value;
            }
        } 

        /// <summary>
        /// 获取或设置SDE数据库名称
        /// </summary>
        public string Spatial_DataBase
        {
            get 
            {
                return this.m_SDEDataBase;
            }
            set 
            { 
                m_SDEDataBase = value; 
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
                return this.m_SDEInstance;
            }
            set 
            { 
                m_SDEInstance = value;
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库连接用户名
        /// </summary>
        public string Spatial_User
        {
            get
            {
                return this.m_SDEUser;
            }
            set 
            { 
                m_SDEUser = value;
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库连接密码
        /// </summary>
        public string Spatial_Password
        {
            get
            {
                return this.m_SDEPassword;
            }
            set 
            { 
                m_SDEPassword = value; 
            }
        }

        /// <summary>
        /// 获取或设置SDE数据库连接版本
        /// </summary>
        public string Spatial_Version
        {
            get
            {
                return this.m_SDEVersion;
            }
            set 
            { 
                m_SDEVersion = value;
            }
        }
        string pVersion = string.Empty;
        public string Version
        {
            get {
                if (m_Workspace == null) return "";
                IPropertySet property = m_Workspace.ConnectionProperties;
                pVersion=property.GetProperty("Version").ToString();
                if(pVersion.Contains("."))
                {
                    string[] pVersions = pVersion.Split('.');
                    pVersion = pVersions[pVersions.Length - 1];
                }
                return pVersion;
            }
            set
            {
                pVersion = value;
                IPropertySet property = m_Workspace.ConnectionProperties;
                property.SetProperty("Version", value);
               
            }
        }
        private IVersion m_CurrentVersion;//
        /// <summary>
        /// 当前版本
        /// </summary>
        public IVersion CurrentVersion
        {
            get
            {
                return m_CurrentVersion;
            }
            set
            {
                m_CurrentVersion = value;
                if(m_CurrentVersion==null)
                {
                    OnSdeVersionChanged?.Invoke("");
                    return;
                }
                pVersion = m_CurrentVersion.VersionName.ToUpper();
                if (pVersion.Contains("."))
                {
                    string[] pVersions = pVersion.Split('.');
                    pVersion = pVersions[pVersions.Length - 1];
                }
                if (pVersion == "default".ToUpper())
                {
                    OnSdeVersionChanged?.Invoke("");
                }
                else
                {
                    OnSdeVersionChanged?.Invoke(m_CurrentVersion.VersionName);
                }
                   
            }
        }
        private IHistoricalVersion m_CurrentHistoricalVersion;//
        public IHistoricalVersion HistoricalVersion
        {
            get
            {
                return m_CurrentHistoricalVersion;
            }
            set
            {
                m_CurrentHistoricalVersion = value;
                if(m_CurrentHistoricalVersion==null)
                {
                    OnSdeVersionChanged?.Invoke("");
                }
                else
                {
                    string timeStamp = m_CurrentHistoricalVersion.TimeStamp.ToString();
                    string date = DateTime.Parse(timeStamp).ToString("yyyy年MM月dd日 HH:mm:ss");
                    string versionname = (m_CurrentHistoricalVersion as IVersion).VersionName;
                    DateTime dt = DateTime.Now;
                    if(DateTime.TryParse(versionname,out dt))
                    {
                        OnSdeVersionChanged?.Invoke($"({date})");
                    }
                    else
                    {
                        OnSdeVersionChanged?.Invoke($"({(m_CurrentHistoricalVersion as IVersion).VersionName}_{date})");
                    }
                   
                }
               
            }
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
        /// 获取Ole数据库连接管理类
        /// </summary>
        public OleConnPropertyManager OLE_ConnManager
        {
            get
            {
                return this.m_OleConnManager;
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

        /// <summary>
        /// 获取BS数据库连接服务器ip
        /// </summary>
        public string BS_Server
        {
            get
            {
                return this.m_BSServer;
            }
            set
            {
                m_BSServer = value;
            }
        }

        /// <summary>
        /// 获取BS数据库端口
        /// </summary>
        public string BS_Port
        {
            get
            {
                return this.m_BSPort;
            }
            set
            {
                m_BSPort = value;
            }
        }

        /// <summary>
        /// 获取BS数据库连接名称
        /// </summary>
        public string BS_DataBase
        {
            get
            {
                return this.m_BSDataBase;
            }
            set
            {
                m_BSDataBase = value;
            }
        }

        /// <summary>
        /// 获取BS数据库用户名称
        /// </summary>
        public string BS_User
        {
            get
            {
                return this.m_BSUser;
            }
            set
            {
                m_BSUser = value;
            }
        }

        /// <summary>
        /// 获取BS数据库用户密码
        /// </summary>
        public string BS_Password
        {
            get
            {
                return this.m_BSPassword;
            }
            set
            {
                m_BSPassword = value;
            }
        }

        /// <summary>
        /// 获取BS数据库类型
        /// </summary>
        public DatabaseType BS_DBType
        {
            get
            {
                return this.m_BSDBType;
            }
            set
            {
                m_BSDBType = value;
            }
        }

        /// <summary>
        /// 获取设置数据库服务名称
        /// </summary>
        public string OracleSID
        {
            get
            {
                return m_OracleSID;
            }
            set
            {
                m_OracleSID = value;
            }
        }

        /// <summary>
        /// 获取SDE空间连接属性
        /// </summary>
        public IPropertySet PropertySet
        {
            get
            {
                if (this.m_PropertySet == null)
                {
                    //实例化属性设置对象
                    m_PropertySet = new PropertySetClass();
                    m_PropertySet.SetProperty("Server", this.m_SDEServer);
                    m_PropertySet.SetProperty("Instance", this.m_SDEInstance);
                    m_PropertySet.SetProperty("DataBase", this.m_SDEDataBase);
                    m_PropertySet.SetProperty("User", this.m_SDEUser);
                    m_PropertySet.SetProperty("Password", this.m_SDEPassword);
                    m_PropertySet.SetProperty("Version", this.m_SDEVersion);
                }

                return this.m_PropertySet;
            }
        }
        public string PathName
        {
            get
            {
                return $"Database Connections\\{OLE_Server}.sde";
            }
        }
        public bool hasWorkspace
        {
            get { return m_Workspace == null ? false : true; }
        }
        /// <summary>
        /// 获取SDE工作空间
        /// </summary>
        public IWorkspace Workspace
        {
            get
            {
                if (this.m_Workspace == null)
                {
                    ////由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
                    string server = this.PropertySet.GetProperty("Server").ToString();
                    if (NetHelper.Ping(server) == false) return null;

                    //实例化SDEWorkspaceFactory对象
                    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
                    this.m_Workspace = tWorkspaceFactory.Open(this.PropertySet, 0);
                    CurrentVersion = this.Workspace as IVersion;
                }

                //检查SDEWorkspace是否连接，失去连接则重新连接
                WorkspaceAGHelper.CheckSDEWorkspaceConnect(ref this.m_Workspace);

               return this.m_Workspace;                
            }
        }
        private Database m_Database;
        /// <summary>
        /// 数据库操作连接
        /// </summary>
        public Database ConfigDatabase
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
            m_Database = new Database();
            try
            {
                if (m_Database.Open() == false) m_Database = null;
            }
            catch
            {
                m_Database = null;
            }
        }
        /// <summary>
        /// Oracle数据库操作连接（已过时，新功能请勿使用此类）
        /// </summary>
        public OracleDatabase OracleDatabase
        {
            get
            {
                // 分析Oracle数据库操作对象是否存在
                if (m_OracleDatabase == null)
                {
                    RefreshOracleDatabase();
                }
                return m_OracleDatabase;
            }
        }

        /// <summary>
        /// 重新获取OracleDatabase对象
        /// </summary>
        public void RefreshOracleDatabase()
        {
            m_OracleDatabase = new OracleDatabase(m_SDEDataBase, m_SDEUser, m_SDEPassword, m_SDEServer);
            try
            {
                if (m_OracleDatabase.OpenConnection() == false) m_OracleDatabase = null;
            }
            catch
            {
                m_OracleDatabase = null;
            }
        }

        private BSDatabase m_BSDatabase;
        /// <summary>
        /// 数据库操作连接
        /// </summary>
        public BSDatabase ConfigBSDatabase
        {
            get
            {
                if (m_BSDatabase == null)
                {
                    m_BSDatabase = new BSDatabase();
                }
                return m_BSDatabase;
            }
        }

        public bool hasBSWorkspace
        {
            get { return m_BSWorkspace == null ? false : true; }
        }
        /// <summary>
        /// 获取BS空间库工作空间
        /// </summary>
        public IWorkspace BSWorkspace
        {
            get
            {
                string server0 = this.BSPropertySet.GetProperty("Server").ToString();
                if (this.m_BSWorkspace == null)
                {
                    ////由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
                    string server = this.BSPropertySet.GetProperty("Server").ToString();
                    if (NetHelper.Ping(server) == false) return null;

                    //实例化SDEWorkspaceFactory对象
                    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
                    this.m_BSWorkspace = tWorkspaceFactory.Open(this.BSPropertySet, 0);
                    CurrentVersion = this.BSWorkspace as IVersion;
                }

                //检查SDEWorkspace是否连接，失去连接则重新连接
                WorkspaceAGHelper.CheckSDEWorkspaceConnect(ref this.m_BSWorkspace);

                return this.m_BSWorkspace;
            }
        }

        /// <summary>
        /// 获取SDE空间连接属性
        /// </summary>
        public IPropertySet BSPropertySet
        {
            get
            {
                string m_BSInstance = $"sde:postgresql:{m_BSServer}";
                if (this.m_BSPropertySet == null)
                {
                    //实例化属性设置对象
                    m_BSPropertySet = new PropertySetClass();
                    m_BSPropertySet.SetProperty("Server", this.m_BSServer);
                    m_BSPropertySet.SetProperty("Instance", m_BSInstance);
                    m_BSPropertySet.SetProperty("DataBase", this.m_BSDataBase);
                    m_BSPropertySet.SetProperty("User", this.m_BSUser);
                    m_BSPropertySet.SetProperty("Password", this.m_BSPassword);
                    m_BSPropertySet.SetProperty("Version", this.m_SDEVersion);
                }

                return this.m_BSPropertySet;
            }
        }

        public string MinioServerURL
        {
            get
            {
                return this.m_MinioServerURL;
            }
            set
            {
                m_MinioServerURL = value;
            }
        }

        public string MinioAccessName
        {
            get
            {
                return this.m_MinioAccessName;
            }
            set
            {
                m_MinioAccessName = value;
            }
        }

        public string MinioPassWord
        {
            get
            {
                return this.m_MinioPassWord;
            }
            set
            {
                m_MinioPassWord = value;
            }
        }

        public string MinioServerBucket
        {
            get
            {
                return this.m_MinioServerBucket;
            }
            set
            {
                m_MinioServerBucket = value;
            }
        }

        /// <summary>
        /// 置空数据库连接，SDE连接
        /// </summary>
        public void NullConnection()
        {
            m_PropertySet = null;
            m_Workspace = null;
        }
    }


   
    /// <summary>
    /// 数据库类型
    /// </summary>
    
}
