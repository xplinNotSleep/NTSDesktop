using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 控件绑定功能对象
    /// 此对能能获取控件，控件Name，绑定功能（插件）等
    /// </summary>
    public class UIDesignControl
    {
        private string m_Name = "";
        /// <summary>
        /// 控件名称
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// 控件
        /// </summary>
        public object Control
        {
            get;
            set;
        }

        /// <summary>
        /// 插件（ICommand对象）
        /// </summary>
        public object Plugin
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定功能
        /// </summary>
        public ItemCommandInfo BindFun
        {
            get;
            set;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public Image Image
        {
            get;
            set;
        }

        private bool m_HasInit = false;
        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool HasInit
        {
            get { return m_HasInit; }
            set { m_HasInit = value; }
        }
    }
}
