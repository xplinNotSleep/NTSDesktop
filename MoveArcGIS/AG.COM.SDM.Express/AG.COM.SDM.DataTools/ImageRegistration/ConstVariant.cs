using System.Collections.Generic;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    public class ConstVariant
    {
        public const string ConstImages = "AG.COM.SDM.DataTools.ImageRegistration.Resourses.Bitmap.";
        public const string ConstCursors = "AG.COM.SDM.DataTools.ImageRegistration.Resourses.Cursor.";

        public static bool GeoReferState = false;
        public static bool GeoEditPoint = false;

        public static List<IElement> ElementImage = new List<IElement>();
        public static List<IElement> ElementMap = new List<IElement>();
        
        /// 判断是否是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^(-)?(([1-9]\d*)|0)([.]\d*)?$");
        }
    }
}
