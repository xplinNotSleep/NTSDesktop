using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility.Wrapers
{
    /// <summary>
    /// 字段类型包装类
    /// </summary>
    public class FieldTyperWrapper
    {
        private esriFieldType m_FieldType = esriFieldType.esriFieldTypeString;
       
        public FieldTyperWrapper(esriFieldType fldType)
        {
            m_FieldType = fldType;
        }

        public esriFieldType FieldType
        {
            get { return m_FieldType; }
        }

        public override string ToString()
        {
            switch (m_FieldType)
            {
                case esriFieldType.esriFieldTypeBlob:
                    return "二进制";
                case esriFieldType.esriFieldTypeDate:
                    return "日期";
                case esriFieldType.esriFieldTypeDouble:
                    return "双精度";
                case esriFieldType.esriFieldTypeGeometry:
                    return "几何";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "GlobalID";
                case esriFieldType.esriFieldTypeGUID:
                    return "GUID";
                case esriFieldType.esriFieldTypeInteger:
                    return "整型";
                case esriFieldType.esriFieldTypeOID:
                    return "ObjectID";
                case esriFieldType.esriFieldTypeRaster:
                    return "栅格";
                case esriFieldType.esriFieldTypeSingle:
                    return "单精度";
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "小整数";
                case esriFieldType.esriFieldTypeString:
                    return "字符串";
                case esriFieldType.esriFieldTypeXML:
                    return "XML";
                default:
                    return "";
            }

        }
    }
}
