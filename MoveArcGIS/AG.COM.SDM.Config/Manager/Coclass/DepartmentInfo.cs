namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public class DepartmentInfo
    {
        private string m_DeptName;
        private string m_DeptCode;
        private string m_RelPerson;
        private string m_RelTelephone;
        private string m_Address;
        private string m_Description;
        private string m_ItemID;
        private string m_ParentID;

        /// <summary>
        /// ��ȡ�����õ�λ����
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
        /// ��ȡ�����õ�λ����
        /// </summary>
        public string DeptCode
        {
            get
            {
                return this.m_DeptCode;
            }
            set
            {
                this.m_DeptCode = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ������ϵ����Ϣ
        /// </summary>
        public string RelPerson
        {
            get
            {
                return this.m_RelPerson;
            }
            set
            {
                this.m_RelPerson = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ������ϵ�绰
        /// </summary>
        public string RelTelephone
        {
            get
            {
                return this.m_RelTelephone;
            }
            set
            {
                this.m_RelTelephone = value;
            }

        } 

        /// <summary>
        /// ��ȡ�����õ�ǰ�������ڵ�ַ
        /// </summary>
        public string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ����������Ϣ
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
        /// �ڵ�ID��
        /// </summary>
        public string ItemID
        {
            get
            {
                return this.m_ItemID;
            }
            set
            {
                this.m_ItemID = value;
            }
        }

        /// <summary>
        /// ���ڵ�ID��
        /// </summary>
        public string ParentID
        {
            get
            {
                return this.m_ParentID;
            }
            set
            {
                this.m_ParentID = value;
            }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns>���ص�λ����</returns>
        public override string ToString()
        {
            return this.m_DeptName;
        }
    }
}
