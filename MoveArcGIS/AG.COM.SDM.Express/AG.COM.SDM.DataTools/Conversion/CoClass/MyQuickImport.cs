using ESRI.ArcGIS.DataInteroperabilityTools;
using ESRI.ArcGIS.Geoprocessor;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 快速导入
    /// </summary>
    public class MyQuickImport:IQuickImportExport
    {
        private object m_inputFeatures;
        private object m_outputfile;
        private QuickType m_type;

        /// <summary>
        /// 导入要素
        /// </summary>
        public object InputFeatures
        {
            get { return m_inputFeatures; }
            set { m_inputFeatures = value; }
        }

        /// <summary>
        /// 导出文件路径
        /// </summary>
        public object OutputFile
        {
            get { return m_outputfile; }
            set { m_outputfile = value; }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public QuickType QType
        {
            get { return m_type; }
            set { m_type = value; }
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        public void Execute()
        {
            try
            {
                string tStr = "";
                switch (m_type)
                {
                    case QuickType.Arcinfo:
                        tStr = "ARCINFO,";
                        break;
                    case QuickType.E00:
                        tStr = "E00,";
                        break;
                    case QuickType.Mif:
                        tStr = "MIF,";
                        break;
                    case QuickType.Cad:
                        tStr = "ACAD,";
                        break;
                    case QuickType.Gml:
                        tStr = "GML2,";
                        break;
                    case QuickType.Vct:
                        tStr = "IDRISI,";
                        break;
                    default:
                        break;
                }

                if (tStr == "")  return;

                QuickImport tQi = new QuickImport(tStr + m_inputFeatures, m_outputfile);
                Geoprocessor tGp = new Geoprocessor();
                tGp.OverwriteOutput = true;
                tGp.Execute(tQi, null);
            }
            catch
            {
            }
        }
    }
}
