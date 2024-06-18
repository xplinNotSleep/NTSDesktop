using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ProjectFeature帮助类
    /// 用于不同地方项目在同一份代码时，快速切换配置文件
    /// </summary>
    public class ProjectFeatureHelper
    {
        private static string m_Path = CommonConstString.STR_PreAppPath + "\\ProjectFeature";

        /// <summary>
        /// 获取ProjectFeature路径
        /// </summary>
        public static string Path
        {
            get
            {               
                return m_Path;
            }
            set
            {
                m_Path = value;
            }
        }

        /// <summary>
        /// 通过地方变量重新组织ProjectFeature路径
        /// </summary>
        /// <param name="tProjectFeature"></param>
        public static void CombineProjectFeaturePath(string tProjectFeature)
        {
            if (!string.IsNullOrEmpty(tProjectFeature))
            {
                m_Path = CommonConstString.STR_PreAppPath + "\\ProjectFeature" + tProjectFeature;
            }
        }
    }
}
