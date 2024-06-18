using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// Json序列化帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 序列化为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            if (obj == null) return "";

            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tSerializeString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string tSerializeString)
        {
            return JsonConvert.DeserializeObject<T>(tSerializeString);
        }
    }
}
