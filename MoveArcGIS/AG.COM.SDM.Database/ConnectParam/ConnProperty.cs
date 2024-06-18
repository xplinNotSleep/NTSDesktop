namespace AG.COM.SDM.Database
{
    /// <summary>
    /// 数据库连接属性
    /// </summary>
    public class ConnProperty
    {

        public DatabaseType DbType
        {
            get;set;
        }

        /// <summary>
        /// 获取或设置服务器IP
        /// </summary>
        public string Server {get;set;}

        /// <summary>
        /// 获取或设置服务器端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 获取或设置数据库名称
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 获取或设置实例名称
        /// </summary>
        public string Instance { get; set; }  
        
        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        public string Password { get; set; }  
        
        /// <summary>
        /// 获取或设置版本(sde)
        /// </summary>
        public string Version { get; set; }

    }

}
