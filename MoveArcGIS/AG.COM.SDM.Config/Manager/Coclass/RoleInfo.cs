using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 角色信息表
    /// </summary>
    public class RoleInfo
    {
        private string m_RoleID;
        private string m_RoleName;
        private string m_Description;
        private IList<string> m_ListUser;

        /// <summary>
        /// 获取或设置角色编号
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
        /// 获取或设置当前角色名称
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
        /// 获取或设置当前角色的描述信息
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
        /// 获取或设置当前角色所包含的成员列表
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
        /// 重载ToString()方法
        /// </summary>
        /// <returns>返回角色名称</returns>
        public override string ToString()
        {
            return this.m_RoleName;
        }
    }
}
