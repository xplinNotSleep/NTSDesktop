using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Utility
{
    public static class ExtILayer
    {
        #region ILayer扩展属性的实现

        /// <summary>
        /// 保存图层扩展属性的数组，key为ILayer对象
        /// </summary>
        private static Dictionary<ILayer, Dictionary<string, object>> m_LayerTags = new Dictionary<ILayer, Dictionary<string, object>>();

        /// <summary>
        /// 获取ILayer对象的某个扩展属性
        /// </summary>
        /// <param name="tLayer"></param>
        /// <param name="TagKey"></param>
        /// <returns></returns>
        public static object GetTag(this ILayer tLayer, string TagKey)
        {
            object result = null;

            if (tLayer != null && !string.IsNullOrEmpty(TagKey))
            {
                Dictionary<string, object> tTag = null;
                if (m_LayerTags.TryGetValue(tLayer, out tTag))
                {
                    if (tTag.TryGetValue(TagKey, out result))
                    {
                        //TryGetValue已经赋值result，不用再另外赋值
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 设置ILayer对象的某个扩展属性
        /// </summary>
        /// <param name="tLayer"></param>
        /// <param name="TagKey"></param>
        /// <param name="TagValue"></param>
        public static void SetTag(this ILayer tLayer, string TagKey, object TagValue)
        {
            if (tLayer != null && !string.IsNullOrEmpty(TagKey))
            {
                if (!m_LayerTags.ContainsKey(tLayer))
                {
                    m_LayerTags.Add(tLayer, new Dictionary<string, object>());
                }

                Dictionary<string, object> tTag = m_LayerTags[tLayer];
                if (!tTag.ContainsKey(TagKey))
                {
                    tTag.Add(TagKey, TagValue);
                }
                else
                {
                    tTag[TagKey] = TagValue;
                }
            }
        }

        #endregion
    }
}
