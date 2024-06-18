using System;
using System.Threading;

namespace AG.COM.SDM.Database
{
    public delegate void SdeVersionChanged(string version);
    /// <summary>
    /// ϵͳ����������Ϣ��
    /// </summary>
    public class OleSdeDBConfig
    {
        public event SdeVersionChanged OnSdeVersionChanged;
        #region ˽�б���
        private static OleSdeDBConfig m_OleSdeDBConfig;
        private DatabaseType m_SDEBType = DatabaseType.Oracle;      //OLE���ݿ�����       
        private string m_SDEServer;                    //SDE��������
        private string m_SDEDataBase;                  //SDE���ݿ�����
        private string m_SDEInstance;                  //SDEʵ������
        private string m_SDEUser;                      //SDE�û�
        private string m_SDEPassword;                  //SDE����
        private string m_SDEVersion;                   //SDE����

        private OleConnPropertyManager m_OleConnManager = new OleConnPropertyManager();     //OLE���ù�����
        private string m_OLEServer;                 //OLE������
        private string m_OLEDataBase;               //OLE���ݿ���
        private string m_OLEPort;                   //OLE�˿�
        private string m_OLEUser;                   //OLE�û�����
        private string m_OLEPassword;               //OLE�û�����
        private DatabaseType  m_OLEDBType=DatabaseType.Oracle;      //OLE���ݿ�����

        //����BS���ò�������
        private string m_BSServer;                 //BS�˷���������
        private string m_BSDataBase;               //BS�����ݿ���
        private string m_BSPort;                   //BS�˷������˿�
        private string m_BSUser;                   //BS�����ݿ��û���
        private string m_BSPassword;               //BS�����ݿ�����
        private DatabaseType m_BSDBType = DatabaseType.Oracle;      //OLE���ݿ�����

        private OracleDatabase m_OracleDatabase;  //Oracle���ݿ�����

        private string m_OracleSID;      
        private IPropertySet m_PropertySet;         //SDE��������
        private IPropertySet m_BSPropertySet;
        private IWorkspace m_Workspace;             //SDE�����ռ�
        private string m_Version;                   //SDE��ǰ�汾
        private IWorkspace m_BSWorkspace;           //BS�⹤���ռ�

        //Minio���������ò���
        private string m_MinioServerURL;
        private string m_MinioAccessName;
        private string m_MinioPassWord;
        private string m_MinioServerBucket;

        #endregion

        /// <summary>
        /// ˽�й��캯��
        /// </summary>
        private OleSdeDBConfig()
        {

        }

        /// <summary>
        /// ��ȡOleSdeDBConfigʵ������
        /// </summary>
        /// <returns>����OleSdeDBConfig����ʵ��</returns>
        public static OleSdeDBConfig GetInstance()
        {
            if (m_OleSdeDBConfig == null)
            {
                m_OleSdeDBConfig = new OleSdeDBConfig();
            }     

            return m_OleSdeDBConfig;            
        }
        /// <summary>
        /// ��ȡ������SDE���ݿ�����
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
        /// ��ȡ������SDE���ݿ��������
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
        /// ��ȡ������SDE���ݿ�����
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
        /// ��ȡ������SDE���ݿ�ʵ������
        /// ���磺5151
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
        /// ��ȡ������SDE���ݿ������û���
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
        /// ��ȡ������SDE���ݿ���������
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
        /// ��ȡ������SDE���ݿ����Ӱ汾
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
        /// ��ǰ�汾
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
                    string date = DateTime.Parse(timeStamp).ToString("yyyy��MM��dd�� HH:mm:ss");
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
        /// ��ȡ������OLE���ݿ�����
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
        /// ��ȡ������OLE���������ƻ�IP��ַ
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
        /// ��ȡOle���ݿ����ӹ�����
        /// </summary>
        public OleConnPropertyManager OLE_ConnManager
        {
            get
            {
                return this.m_OleConnManager;
            }
        }

        /// <summary>
        /// ��ȡ������ҵ�����ݿ�����
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
        /// ��ȡ������ҵ�����ݿ�˿�
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
        /// ��ȡҵ�����ݿ������û�����
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
        /// ��ȡҵ�����ݿ���������
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
        /// ��ȡBS���ݿ����ӷ�����ip
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
        /// ��ȡBS���ݿ�˿�
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
        /// ��ȡBS���ݿ���������
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
        /// ��ȡBS���ݿ��û�����
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
        /// ��ȡBS���ݿ��û�����
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
        /// ��ȡBS���ݿ�����
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
        /// ��ȡ�������ݿ��������
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
        /// ��ȡSDE�ռ���������
        /// </summary>
        public IPropertySet PropertySet
        {
            get
            {
                if (this.m_PropertySet == null)
                {
                    //ʵ�����������ö���
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
        /// ��ȡSDE�����ռ�
        /// </summary>
        public IWorkspace Workspace
        {
            get
            {
                if (this.m_Workspace == null)
                {
                    ////���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
                    string server = this.PropertySet.GetProperty("Server").ToString();
                    if (NetHelper.Ping(server) == false) return null;

                    //ʵ����SDEWorkspaceFactory����
                    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
                    this.m_Workspace = tWorkspaceFactory.Open(this.PropertySet, 0);
                    CurrentVersion = this.Workspace as IVersion;
                }

                //���SDEWorkspace�Ƿ����ӣ�ʧȥ��������������
                WorkspaceAGHelper.CheckSDEWorkspaceConnect(ref this.m_Workspace);

               return this.m_Workspace;                
            }
        }
        private Database m_Database;
        /// <summary>
        /// ���ݿ��������
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
        /// Oracle���ݿ�������ӣ��ѹ�ʱ���¹�������ʹ�ô��ࣩ
        /// </summary>
        public OracleDatabase OracleDatabase
        {
            get
            {
                // ����Oracle���ݿ���������Ƿ����
                if (m_OracleDatabase == null)
                {
                    RefreshOracleDatabase();
                }
                return m_OracleDatabase;
            }
        }

        /// <summary>
        /// ���»�ȡOracleDatabase����
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
        /// ���ݿ��������
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
        /// ��ȡBS�ռ�⹤���ռ�
        /// </summary>
        public IWorkspace BSWorkspace
        {
            get
            {
                string server0 = this.BSPropertySet.GetProperty("Server").ToString();
                if (this.m_BSWorkspace == null)
                {
                    ////���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
                    string server = this.BSPropertySet.GetProperty("Server").ToString();
                    if (NetHelper.Ping(server) == false) return null;

                    //ʵ����SDEWorkspaceFactory����
                    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
                    this.m_BSWorkspace = tWorkspaceFactory.Open(this.BSPropertySet, 0);
                    CurrentVersion = this.BSWorkspace as IVersion;
                }

                //���SDEWorkspace�Ƿ����ӣ�ʧȥ��������������
                WorkspaceAGHelper.CheckSDEWorkspaceConnect(ref this.m_BSWorkspace);

                return this.m_BSWorkspace;
            }
        }

        /// <summary>
        /// ��ȡSDE�ռ���������
        /// </summary>
        public IPropertySet BSPropertySet
        {
            get
            {
                string m_BSInstance = $"sde:postgresql:{m_BSServer}";
                if (this.m_BSPropertySet == null)
                {
                    //ʵ�����������ö���
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
        /// �ÿ����ݿ����ӣ�SDE����
        /// </summary>
        public void NullConnection()
        {
            m_PropertySet = null;
            m_Workspace = null;
        }
    }


   
    /// <summary>
    /// ���ݿ�����
    /// </summary>
    
}
