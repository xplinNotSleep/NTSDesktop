using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Utility
{
    public class UniqueValueStatHelper
    {
        /// <summary>
        /// 在Oracle数据库中统计唯一值，判断字段是否能统计
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool UniqueValueInOracleFieldNameFilter(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) return false;

            if (fieldName.Contains("."))
            {
                return false;
            }

            return true;
        }
    }
}
