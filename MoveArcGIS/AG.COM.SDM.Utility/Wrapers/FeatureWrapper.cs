using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility.Wrapers
{
    /// <summary>
    /// 要素包装类
    /// </summary>
    public class FeatureWrapper
    {
        private IFeature m_Feature = null;
        
        public FeatureWrapper(IFeature feature)
        {
            m_Feature = feature;
        }

        public IFeature Feature
        {
            get { return m_Feature; }
        }

        public override string ToString()
        {
            string str = m_Feature.Class.AliasName + "-" + m_Feature.OID.ToString();
            return str;
        }

    }
}
