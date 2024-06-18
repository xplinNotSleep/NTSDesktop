using AG.COM.SDM.Database;
using System;
using System.Threading;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ϵͳ����������Ϣ��
    /// </summary>
    public class OleDBConfig
    {
        #region ˽�б���
        private static OleDBConfig m_OleDBConfig;
        private string m_OLEServer;                 //OLE������
        private string m_OLEDataBase;               //OLE���ݿ���
        private string m_OLEPort;                   //OLE�˿�
        private string m_OLEUser;                   //OLE�û�����
        private string m_OLEPassword;               //OLE�û�����
        private DatabaseType  m_OLEDBType=DatabaseType.Oracle;      //OLE���ݿ�����
        #endregion

        /// <summary>
        /// ˽�й��캯��
        /// </summary>
        private OleDBConfig()
        {

        }

        /// <summary>
        /// ��ȡOleDBConfigʵ������
        /// </summary>
        /// <returns>����OleDBConfig����ʵ��</returns>
        public static OleDBConfig GetInstance()
        {
            if (m_OleDBConfig == null)
            {
                m_OleDBConfig = new OleDBConfig();
            }     

            return m_OleDBConfig;            
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
