using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 应用工程信息
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
        /// 获取或设置应用工程编号
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
        /// 获取或设置应用工程名称
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
        /// 获取或设置应用工程数据
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
        /// 获取或设置地图文档数据浏览路径
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
        /// 获取或设置应用工程描述信息
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
        /// 获取或设置应用工程激活状态
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
        /// 获取或设置所属角色
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
        /// 重载ToString方法
        /// </summary>
        /// <returns>返回应用工程名称</returns>
        public override string ToString()
        {
            return this.m_AppName;
        }
    }
}
