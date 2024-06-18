using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Ӧ�ù�����Ϣ
    /// </summary>
    public class MapDocInfo
    {
        private string m_AppID;
        private string m_AppName;
        private object m_AppData;
        private string m_DataBrowserName;
        private string m_Description;
        private bool m_IsActive;
        private IList<RoleInfo> m_ListRoleInfo;

        /// <summary>
        /// ��ȡ������Ӧ�ù��̱��
        /// </summary>
        public string AppID
        {
            get
            {
                return this.m_AppID;
            }
            set
            {
                this.m_AppID = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ӧ�ù�������
        /// </summary>
        public string AppName
        {
            get
            {
                return this.m_AppName;
            }
            set
            {
                this.m_AppName = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ӧ�ù�������
        /// </summary>
        public object AppData
        {
            get
            {
                return this.m_AppData;
            }
            set
            {
                this.m_AppData = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ͼ�ĵ��������·��
        /// </summary>
        public string DataBrowserName
        {
            get
            {
                return this.m_DataBrowserName;
            }
            set
            {
                this.m_DataBrowserName = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ӧ�ù���������Ϣ
        /// </summary>
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ӧ�ù��̼���״̬
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.m_IsActive;
            }
            set
            {
                this.m_IsActive = value;
            }
        }

        /// <summary>
        /// ��ȡ������������ɫ
        /// </summary>
        public IList<RoleInfo> ListRoleInfo
        {
            get
            {
                return this.m_ListRoleInfo;
            }
            set
            {
                this.m_ListRoleInfo = value;
            }
        }

        /// <summary>
        /// ����ToString����
        /// </summary>
        /// <returns>����Ӧ�ù�������</returns>
        public override string ToString()
        {
            return this.m_AppName;
        }
    }
}
