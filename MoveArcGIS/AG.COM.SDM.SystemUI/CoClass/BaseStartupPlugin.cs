namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 系统启动项 抽象基础类
    /// </summary>
    public abstract class BaseStartupPlugin:IStartupPlugin
    {
        #region 保护变量
        protected string m_name="";
        protected string m_caption="";
        protected bool m_enabled = true;
        protected string m_description = "";
        #endregion 

        #region IStartupPlugin 成员
        /// <summary>
        /// 获取对象描述信息
        /// </summary>
        public string Description
        {
            get
            {
                return this.m_description;
            }
        }

        /// <summary>
        /// 启动处理方法
        /// </summary>
        public abstract void Startup();      

        #endregion

        #region IPlugin 成员

        /// <summary>
        /// 获取当前对象名称
        /// </summary>
        public string Name
        {
            get { return this.m_name; }
        }

        /// <summary>
        /// 获取当前对象显示文本
        /// </summary>
        public string Caption
        {
            get { return this.m_caption; }
        }

        /// <summary>
        /// 获取当前对象可用状态
        /// </summary>
        public bool Enabled
        {
            get { return this.m_enabled; }
        }

        /// <summary>
        /// 对象创建时初始化处理
        /// </summary>
        /// <param name="hook">hook对象</param>
        public abstract void OnCreate(object hook);
       
        #endregion
    }
}
