namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 输出GDB的分层信息
    /// </summary>
    public class CADToFeatureLayerInfo
    {
        /// <summary>
        /// CAD文件全路径名称
        /// </summary>
        public string CADFullFileName { get; set; }

        /// <summary>
        /// 在GDB中Dataset的名称
        /// </summary>
        public string GDBDatasetName { get; set; }

        /// <summary>
        /// CAD图层的名称
        /// </summary>
        public string CADLayerName { get; set; }

        /// <summary>
        /// 在GDB中Featureclass的名称
        /// </summary>
        public string GDBFeatureclassName { get; set; }

        /// <summary>
        /// 在GDB中Featureclass的Geometry类型
        /// </summary>
        public string GDBGeometryType { get; set; }

        /// <summary>
        /// 在GDB中Featureclass是否有Z值
        /// </summary>        
        public bool GDBFeatureclassHasZ { get; set; }
    }
}
