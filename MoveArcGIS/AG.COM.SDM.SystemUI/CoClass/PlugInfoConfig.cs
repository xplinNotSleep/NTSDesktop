using System;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 插件配置信息类
    /// </summary>
    [Serializable]
    public class PlugInfoConfig
    {
        private string m_Caption;
        private string m_PlugAssembly;
        private string m_PlugType;      
        private string m_ParentID;          
        private int m_Level;
        private int m_SubType;     
        private object m_Tag;

        /// <summary>
        /// 实例化PlugInfoConfig新对象
        /// </summary>
        public PlugInfoConfig()
        {
            m_Caption = "";
            m_PlugAssembly = "";
            m_PlugType = "";                     
            m_Level = 0;
            m_SubType = -1;          
        }

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
        /// 插件所在组件
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
        /// 执行方法名称
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

        /// <summary>
        /// 返回父节点编号
        /// </summary>
        public string ParentID
        {
            get
            {
                return m_ParentID;
            }
            set
            {
                m_ParentID = value;
            }
        }

        /// <summary>
        /// 所处级别
        /// </summary>
        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                m_Level = value;
            }
        }

        /// <summary>
        /// 子类型
        /// </summary>
        public int SubType
        {
            get
            {
                return this.m_SubType;
            }
            set
            {
                this.m_SubType = value;
            }
        } 

        /// <summary>
        /// 获取或设置有关插件信息的数据
        /// </summary>
        public object Tag
        {
            get
            {
                return this.m_Tag;
            }
            set
            {
                this.m_Tag = value;
            }
        }

        /// <summary>
        /// 重载ToString()方法
        /// </summary>
        /// <returns>返回显示文本信息</returns>
        public override string ToString()
        {
            return this.m_Caption;
        }
    }
}
