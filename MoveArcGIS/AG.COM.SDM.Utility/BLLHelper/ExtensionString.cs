using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// String扩展函数
    /// </summary>
    public static class ExtensionString
    {
        /// <summary>
        /// string.IsNullOrEmpty另一种用法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 比较string是否相等（忽略大小写）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="compareString"></param>
        /// <returns></returns>
        public static bool EqualIgnoreCase(this string value, string compareString)
        {
            return string.Equals(value, compareString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
