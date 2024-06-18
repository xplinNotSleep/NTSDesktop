using ESRI.ArcGIS.DataInteroperabilityTools;
using ESRI.ArcGIS.Geoprocessor;

namespace AG.COM.SDM.DataTools.Conversion
{
    public class MyQuickExport : IQuickImportExport
    {
        private object m_inputFeatures;
        private object m_outputfile;
        private QuickType m_type;

        public object InputFeatures
        {
            get { return m_inputFeatures; }
            set { m_inputFeatures = value; }
        }

        public object OutputFile
        {
            get { return m_outputfile; }
            set { m_outputfile = value; }
        }

        public QuickType QType
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public void Execute()
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

            if (tStr == "") return;

            QuickExport tQe = new QuickExport(m_inputFeatures, tStr + m_outputfile);
            Geoprocessor tGp = new Geoprocessor();
            tGp.OverwriteOutput = true;
            tGp.Execute(tQe, null);
        }
    }
}