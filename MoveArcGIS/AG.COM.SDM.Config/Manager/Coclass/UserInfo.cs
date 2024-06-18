using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// �û���Ϣ
    /// </summary>
    public class UserInfo
    {
        private string m_Name;
        private string m_Password;
        private string m_FullName;
        private string m_Description;
        private bool m_IsActive;
        private string m_DeptName;
        private IList<RoleInfo> m_ListRoleInfo;

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        /// <summary>
        /// ��ȡ������ȫ��
        /// </summary>
        public string FullName
        {
            get
            {
                return this.m_FullName;
            }
            set
            {
                this.m_FullName = value;
            }
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public string Password
        {
            get
            {
                return this.m_Password;
            }
            set
            {
                this.m_Password = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ�û�����״̬
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
        /// ��ȡ�����õ�ǰ�û���������Ϣ
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
        /// ��ȡ�����õ�ǰ�û���������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.m_DeptName;
            }
            set
            {
                this.m_DeptName = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ�û�������ɫ
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
        /// ����ToString()����
        /// </summary>
        /// <returns>�����û�����</returns>
        public override string ToString()
        {
            return this.m_Name;
        }
    }
}
