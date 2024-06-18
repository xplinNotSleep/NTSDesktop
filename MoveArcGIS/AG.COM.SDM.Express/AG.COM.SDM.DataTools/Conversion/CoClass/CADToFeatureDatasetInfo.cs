namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 输出GDB的dataset信息
    /// </summary>
    public class CADToFeatureDatasetInfo
    {
        /// <summary>
        /// CAD文件全路径名称
        /// </summary>
        public string CADFullFileName { get; set; }

        /// <summary>
        /// 在GDB中Dataset的名称
        /// </summary>
        public string GDBDatasetName { get; set; }
    }
}
