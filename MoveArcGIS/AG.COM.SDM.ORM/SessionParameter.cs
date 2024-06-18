namespace AG.COM.SDM.DAL
{
    /// <summary>
    /// 会话参数
    /// </summary>
    public class SessionParameter
    {
        /// <summary>
        /// 获取或设置服务器
        /// </summary>
        public string OleServer
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置OLE数据库
        /// </summary>
        public string OleDatabase
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置用户
        /// </summary>
        public string OleUser
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string OlePassword
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置端口
        /// </summary>
        public string OlePort
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oleServer">服务器</param>
        /// <param name="oleDatabase">数据库</param>
        /// <param name="oleport">端口</param>
        /// <param name="oleUser">用户</param>
        /// <param name="olePassword">密码</param>
        public SessionParameter(string oleServer, string oleDatabase, string oleport,string oleUser, string olePassword)
        {
            this.OleServer = oleServer;
            this.OleDatabase = oleDatabase;
            this.OlePort = oleport;
            this.OleUser = oleUser;
            this.OlePassword = olePassword;
        }
    }
}
