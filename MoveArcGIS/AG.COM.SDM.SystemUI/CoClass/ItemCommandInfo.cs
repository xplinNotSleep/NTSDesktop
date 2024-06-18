namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 存储控件功能绑定信息
    /// </summary>
    public class ItemCommandInfo
    {
        #region 字段
        private string m_Caption = "";
        private string m_ControlName = "";
        private string m_PlugAssembly = "";
        private string m_PlugType = "";
        #endregion

        #region 属性

        /// <summary>
        /// 显示文字
        /// </summary>
        public string Caption
        {
            get
            {
                return m_Caption;
            }
            set
            {
                m_Caption = value;
            }
        }

        /// <summary>
        /// 控件Name
        /// </summary>
        public string ControlName
        {
            get
            {
                return m_ControlName;
            }
            set
            {
                m_ControlName = value;
            }
        }

        /// <summary>
        /// dll文件名
        /// </summary>
        public string PlugAssembly
        {
            get
            {
                return m_PlugAssembly;
            }
            set
            {
                m_PlugAssembly = value;
            }
        }

        /// <summary>
        /// 功能全名
        /// </summary>
        public string PlugType
        {
            get
            {
                return m_PlugType;
            }
            set
            {
                m_PlugType = value;
            }
        }

        #endregion
    }
}
