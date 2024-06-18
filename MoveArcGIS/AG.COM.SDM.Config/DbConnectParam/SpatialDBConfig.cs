using AG.COM.SDM.Database;
using System;
using System.Data;
using System.Threading;

namespace AG.COM.SDM.Config
{
    public delegate void SdeVersionChanged(string version);
    /// <summary>
    /// ϵͳ����������Ϣ��
    /// </summary>
    public class SpatialDBConfig
    {
        public event SdeVersionChanged OnSdeVersionChanged;
        #region ˽�б���
        private static SpatialDBConfig m_SpatialDBConfig;
        private DatabaseType m_SpatialType = DatabaseType.Oracle;      //OLE���ݿ�����       
        private string m_SpatialServer;                    //SDE��������
        private string m_SpatialPort;
        private string m_SpatialDataBase;                  //SDE���ݿ�����
        private string m_SpatialInstance;                  //SDEʵ������
        private string m_SpatialUser;                      //SDE�û�
        private string m_SpatialPassword;                  //SDE����
        private string m_SpatialVersion;                   //SDE����

        #region ����BS���ò�������
        //private string m_BSServer;                 //BS�˷���������
        //private string m_BSDataBase;               //BS�����ݿ���
        //private string m_BSPort;                   //BS�˷������˿�
        //private string m_BSUser;                   //BS�����ݿ��û���
        //private string m_BSPassword;               //BS�����ݿ�����
        //private DatabaseType m_BSDBType = DatabaseType.Oracle;      //OLE���ݿ�����
        #endregion

        private string m_Version;                   //SDE��ǰ�汾

        #endregion

        /// <summary>
        /// ˽�й��캯��
        /// </summary>
        private SpatialDBConfig()
        {

        }

        /// <summary>
        /// ��ȡSpatialDBConfigʵ������
        /// </summary>
        /// <returns>����SpatialDBConfig����ʵ��</returns>
        public static SpatialDBConfig GetInstance()
        {
            if (m_SpatialDBConfig == null)
            {
                m_SpatialDBConfig = new SpatialDBConfig();
            }     

            return m_SpatialDBConfig;            
        }
        /// <summary>
        /// ��ȡ������SDE���ݿ�����
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
        /// ��ȡ������SDE���ݿ��������
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
        /// ��ȡ������SDE���ݿ�˿ں���
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
        /// ��ȡ������SDE���ݿ�����
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
        /// ��ȡ������SDE���ݿ�ʵ������
        /// ���磺5151
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
        /// ��ȡ������SDE���ݿ������û���
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
        /// ��ȡ������SDE���ݿ���������
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
        /// ��ȡ������SDE���ݿ����Ӱ汾
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
        /// ���ݿ��������
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
        /// �ռ����ݿ���������һ���Ƚϲ������������ϵͳ���޸Ĳ����˵Ļ�
        /// ���������Ӧ�ĸ���
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

        #region ESRI����
        /// <summary>
        /// ��ȡSDE�ռ���������
        /// </summary>
        //public IPropertySet PropertySet
        //{
        //    get
        //    {
        //        if (this.m_PropertySet == null)
        //        {
        //            //ʵ�����������ö���
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

        #region BS����
        ///// <summary>
        ///// ��ȡBS���ݿ����ӷ�����ip
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
        ///// ��ȡBS���ݿ�˿�
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
        ///// ��ȡBS���ݿ���������
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
        ///// ��ȡBS���ݿ��û�����
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
        ///// ��ȡBS���ݿ��û�����
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
        ///// ��ȡBS���ݿ�����
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
        ///// ���ݿ��������
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
        ///// ��ȡBS�ռ�⹤���ռ�
        ///// </summary>
        //public IWorkspace BSWorkspace
        //{
        //    get
        //    {
        //        string server0 = this.BSPropertySet.GetProperty("Server").ToString();
        //        if (this.m_BSWorkspace == null)
        //        {
        //            ////���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
        //            string server = this.BSPropertySet.GetProperty("Server").ToString();
        //            if (NetHelper.Ping(server) == false) return null;

        //            //ʵ����SDEWorkspaceFactory����
        //            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
        //            this.m_BSWorkspace = tWorkspaceFactory.Open(this.BSPropertySet, 0);
        //            CurrentVersion = this.BSWorkspace as IVersion;
        //        }

        //        //���SDEWorkspace�Ƿ����ӣ�ʧȥ��������������
        //        WorkspaceAGHelper.CheckSDEWorkspaceConnect(ref this.m_BSWorkspace);

        //        return this.m_BSWorkspace;
        //    }
        //}

        ///// <summary>
        ///// ��ȡSDE�ռ���������
        ///// </summary>
        //public IPropertySet BSPropertySet
        //{
        //    get
        //    {
        //        string m_BSInstance = $"sde:postgresql:{m_BSServer}";
        //        if (this.m_BSPropertySet == null)
        //        {
        //            //ʵ�����������ö���
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
