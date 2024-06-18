using AG.COM.SDM.Database;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 空间数据库连接属性
    /// </summary>
    public class SpatialDbConnProperty
    {
        /// <summary>
        /// 获取或设置SDE数据库平台类型
        /// </summary>
        public DatabaseType DbType { get; set; }

        /// <summary>
        /// 获取或设置SDE服务名称
        /// </summary>
        public string Server {get;set;}

        /// <summary>
        /// 获取或设置SDE接口号名称
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 获取或设置SDE数据库名称
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 获取或设置SDE实例名称
        /// </summary>
        public string Instance { get; set; }  
        
        /// <summary>
        /// 获取或设置SDE用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 获取或设置SDE密码
        /// </summary>
        public string Password { get; set; }  
        
        /// <summary>
        /// 获取或设置SDE版本
        /// </summary>
        public string Version { get; set; }

        public SpatialDbConnProperty()
        {
            Version = "SDE.DEFAULT";
        }

        #region ArcEngine 部分

        /// <summary>
        /// 工作空间
        /// </summary>
        //public IWorkspace Workspace
        //{
        //    get
        //    {
        //        try
        //        {
        //            ////由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
        //            //string server = this.PropertySet.GetProperty("Server").ToString();
        //            //if (NetHelper.Ping(server) == false) return null;    

        //            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
        //            IWorkspace tWorkspace = tWorkspaceFactory.Open(this.PropertySet, 0);
        //            return tWorkspace;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        /// <summary>
        /// 获取SDE空间连接属性
        /// </summary>
        //public IPropertySet PropertySet
        //{
        //    get
        //    {
        //        //实例化属性设置对象
        //        IPropertySet tPropertySet = new PropertySetClass();
        //        tPropertySet.SetProperty("Server", Server);
        //        tPropertySet.SetProperty("Instance",Instance);
        //        tPropertySet.SetProperty("DataBase",DataBase);
        //        tPropertySet.SetProperty("User",User);
        //        tPropertySet.SetProperty("Password",Password);
        //        tPropertySet.SetProperty("Version", Version);
        //        return tPropertySet;
        //    }
        //}

        #endregion


    }

}
