namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 部门信息
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
        /// 获取或设置单位名称
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
        /// 获取或设置单位代码
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
        /// 获取或设置当前部分联系人信息
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
        /// 获取或设置当前部门联系电话
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
        /// 获取或设置当前部门所在地址
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
        /// 获取或设置当前部门描述信息
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
        /// 节点ID号
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
        /// 父节点ID号
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
        /// 重载ToString()方法
        /// </summary>
        /// <returns>返回单位名称</returns>
        public override string ToString()
        {
            return this.m_DeptName;
        }
    }
}
