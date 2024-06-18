using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 打印通用参数帮助类
    /// </summary>
    public class SystemParaHelper
    {
        /// <summary>
        /// 系统参数保存文件
        /// </summary>
        public static string SystemParaFileString = ProjectFeatureHelper.Path + "\\AGSDM_SystemParaFile.resx";

        /// <summary>
        /// 系统参数资源文件的Key
        /// </summary>
        public static string SystemParaConfigKey = "AGSDM_SystemParaFile";

        /// <summary>
        /// 从xml读取
        /// </summary>
        /// <returns></returns>
        public static List<SystemPara> ReadFromXml()
        {           
            if (File.Exists(SystemParaFileString) == true)
            {
                ResourceHelper helper = new ResourceHelper(SystemParaFileString);

                List<SystemPara> result = helper.GetObject(SystemParaConfigKey) as List<SystemPara>;
                if (result != null)
                {
                    return result;
                }
            }

            return new List<SystemPara>();
        }

        /// <summary>
        /// 输入系统参数的key获取值
        /// </summary>
        /// <param name="tParaKey"></param>
        /// <returns></returns>
        public static string GetParaValue(string tParaKey)
        {
            if (!string.IsNullOrEmpty(tParaKey))
            {
                List<SystemPara> tSystemParas = ReadFromXml();
                SystemPara tSystemPara = tSystemParas.FirstOrDefault(t => t.Name == tParaKey);
                if (tSystemPara != null)
                {
                    return tSystemPara.Value;
                }
            }

            return "";
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="tSystemParas"></param>
        public static void SaveToFile(List<SystemPara> tSystemParas)
        {
            ResourceHelper helper = new ResourceHelper(SystemParaFileString);

            helper.SetObject(SystemParaConfigKey, tSystemParas);

            helper.Save();
        }
    }
}
