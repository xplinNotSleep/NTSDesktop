using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.International.Converters.PinYinConverter;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 选择范围打印帮助类
    /// </summary>
    public class SelectExtentPrintHelper
    {
        /// <summary>
        /// 获得第一个汉字的首字母（大写）；
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        public static string GetFirstSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar.ToUpper();
        }

        /// <summary>
        /// 获取图幅四角的坐标位置的格式字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetTFLocationFormatString(double value)
        {
            string tmp1 = value.ToString("F2");

            int valueInt = (int)value;
            int gewei = Math.Abs(valueInt % 10);//取个位数
            int shiwei = Math.Abs((valueInt / 10) % 10);//取十位数

            return shiwei.ToString() + gewei.ToString() + tmp1.Substring(tmp1.IndexOf("."), 3);
        }

        /// <summary>
        /// 计算接图表的图幅号（返回的图幅号1-8按从左到右从上向下排）
        /// </summary>
        /// <param name="tFeatureClassTF"></param>
        /// <param name="TFFieldName"></param>
        /// <param name="tEnvelopExtent"></param>
        /// <param name="tTF1"></param>
        /// <param name="tTF2"></param>
        /// <param name="tTF3"></param>
        /// <param name="tTF4"></param>
        /// <param name="tTF5"></param>
        /// <param name="tTF6"></param>
        /// <param name="tTF7"></param>
        /// <param name="tTF8"></param>
        public static void CalculateJTB(IFeatureClass tFeatureClassTF, string TFFieldName, IEnvelope tEnvelopExtent, ref string tTF1, ref  string tTF2, ref  string tTF3
         , ref  string tTF4, ref  string tTF5, ref  string tTF6, ref  string tTF7, ref  string tTF8)
        {
            if (tFeatureClassTF == null || string.IsNullOrEmpty(TFFieldName)) return;

            //图幅名称字段索引
            int TFFieldIdx = tFeatureClassTF.Fields.FindField(TFFieldName);
            if (TFFieldIdx < 0) return;

            IEnvelope tEnvelopExpand = new EnvelopeClass();
            tEnvelopExpand.PutCoords(tEnvelopExtent.XMin, tEnvelopExtent.YMin, tEnvelopExtent.XMax, tEnvelopExtent.YMax);
            //原Envelop扩大10个单位，为了查出附近的图幅
            tEnvelopExpand.Expand(10, 10, false);

            //空间查询，查出附近的图幅
            ISpatialFilter tSpatialFilter = new SpatialFilterClass();
            tSpatialFilter.Geometry = tEnvelopExpand;
            tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            tSpatialFilter.GeometryField = tFeatureClassTF.ShapeFieldName;

            IFeatureCursor tFeatureCursorTF = tFeatureClassTF.Search(tSpatialFilter, false);

            IFeature tFeatureTF = tFeatureCursorTF.NextFeature();
            while (tFeatureTF != null)
            {
                //获取图幅号
                string tTFCurrent = Convert.ToString(tFeatureTF.get_Value(TFFieldIdx));

                IEnvelope tEnvelop = tFeatureTF.Shape != null ? tFeatureTF.Shape.Envelope : null;
                if (tEnvelop != null && !string.IsNullOrEmpty(tTFCurrent))
                {
                    //计算图幅的中点
                    double centerX = (tEnvelop.XMax + tEnvelop.XMin) / 2;
                    double centerY = (tEnvelop.YMax + tEnvelop.YMin) / 2;

                    if (centerX < tEnvelopExtent.XMin)
                    {
                        //左上
                        if (centerY > tEnvelopExtent.YMax)
                        {
                            tTF1 = tTFCurrent;
                        }
                        //左下
                        else if (centerY < tEnvelopExtent.YMin)
                        {
                            tTF6 = tTFCurrent;
                        }
                        //左中
                        else
                        {
                            tTF4 = tTFCurrent;
                        }
                    }
                    else if (centerX > tEnvelopExtent.XMax)
                    {
                        //右上
                        if (centerY > tEnvelopExtent.YMax)
                        {
                            tTF3 = tTFCurrent;
                        }
                        //右下
                        else if (centerY < tEnvelopExtent.YMin)
                        {
                            tTF8 = tTFCurrent;
                        }
                        //右中
                        else
                        {
                            tTF5 = tTFCurrent;
                        }
                    }
                    else
                    {
                        //中上
                        if (centerY > tEnvelopExtent.YMax)
                        {
                            tTF2 = tTFCurrent;
                        }
                        //中下
                        else if (centerY < tEnvelopExtent.YMin)
                        {
                            tTF7 = tTFCurrent;
                        }
                        //中中（不用图幅号）
                        else
                        {

                        }
                    }
                }

                tFeatureTF = tFeatureCursorTF.NextFeature();
            }

            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorTF);
        }

        /// <summary>
        /// 由于ArcGIS10以后不支持mxt，用此函数获取mxd或mxt二选一路径
        /// </summary>
        /// <param name="tNameWithOutExt"></param>
        /// <returns></returns>
        public static string GetPrintTemplateFile(string tNameWithOutExt)
        {
            string tPathMxd = tNameWithOutExt + ".mxd";
            if (File.Exists(tPathMxd) == true)
            {
                //如果mxd存在则使用mxd
                return tPathMxd;
            }
            else
            {
                string tPathMxt = tNameWithOutExt + ".mxt";
                if (File.Exists(tPathMxt) == true)
                {
                    //如果mxd不存在而mxt存在则使用mxt
                    return tPathMxt;
                }
                else
                {
                    //都不存在则使用mxd
                    return tPathMxd;
                }
            }
        }
    }
}
