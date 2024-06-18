using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility.Wrapers
{
    /// <summary>
    /// �ֶ����Ͱ�װ��
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
                    return "������";
                case esriFieldType.esriFieldTypeDate:
                    return "����";
                case esriFieldType.esriFieldTypeDouble:
                    return "˫����";
                case esriFieldType.esriFieldTypeGeometry:
                    return "����";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "GlobalID";
                case esriFieldType.esriFieldTypeGUID:
                    return "GUID";
                case esriFieldType.esriFieldTypeInteger:
                    return "����";
                case esriFieldType.esriFieldTypeOID:
                    return "ObjectID";
                case esriFieldType.esriFieldTypeRaster:
                    return "դ��";
                case esriFieldType.esriFieldTypeSingle:
                    return "������";
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "С����";
                case esriFieldType.esriFieldTypeString:
                    return "�ַ���";
                case esriFieldType.esriFieldTypeXML:
                    return "XML";
                default:
                    return "";
            }

        }
    }
}
