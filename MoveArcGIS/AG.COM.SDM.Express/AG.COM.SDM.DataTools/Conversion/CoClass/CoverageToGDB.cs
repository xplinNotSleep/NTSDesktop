using ESRI.ArcGIS.DataInteroperabilityTools;
using ESRI.ArcGIS.Geoprocessor;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// Coverage×ªGDBÊý¾Ý
    /// </summary>
    public class CoverageToGDB:ICoverageToGDB
    {
        private object m_inputFeatures;
        private object m_outputfile;

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

        public void Execute()
        {
            try
            {
                QuickImport tQi = new QuickImport("ARCINFO," + m_inputFeatures, m_outputfile);
                Geoprocessor tGp = new Geoprocessor();
                tGp.Execute(tQi, null);
            }
            catch
            {
                throw;
            }            
        }
    }
}
