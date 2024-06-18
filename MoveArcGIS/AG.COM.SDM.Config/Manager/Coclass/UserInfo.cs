using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 用户信息
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
        /// 获取或设置名称
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
        /// 获取或设置全名
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
        /// 获取或设置密码
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
        /// 获取或设置当前用户激活状态
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
        /// 获取或设置当前用户的描述信息
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
        /// 获取或设置当前用户隶属部门
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
        /// 获取或设置当前用户隶属角色
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
        /// 重载ToString()方法
        /// </summary>
        /// <returns>返回用户名称</returns>
        public override string ToString()
        {
            return this.m_Name;
        }
    }
}
