using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ��ɫ��Ϣ��
    /// </summary>
    public class RoleInfo
    {
        private string m_RoleID;
        private string m_RoleName;
        private string m_Description;
        private IList<string> m_ListUser;

        /// <summary>
        /// ��ȡ�����ý�ɫ���
        /// </summary>
        public string RoleID
        {
            get
            {
                return this.m_RoleID;
            }
            set
            {
                this.m_RoleID = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ��ɫ����
        /// </summary>
        public string RoleName
        {
            get
            {
                return this.m_RoleName;
            }
            set
            {
                this.m_RoleName = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ��ɫ��������Ϣ
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
        /// ��ȡ�����õ�ǰ��ɫ�������ĳ�Ա�б�
        /// </summary>
        public IList<string> ListUser
        {
            get
            {
                return this.m_ListUser;
            }
            set
            {
                this.m_ListUser = value;
            }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns>���ؽ�ɫ����</returns>
        public override string ToString()
        {
            return this.m_RoleName;
        }
    }
}
