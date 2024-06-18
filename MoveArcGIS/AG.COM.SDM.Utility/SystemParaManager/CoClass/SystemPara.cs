using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 打印通用参数对象
    /// </summary>
    [Serializable]
    public class SystemPara
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
