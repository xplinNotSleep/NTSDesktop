namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 快速导入导出
    /// </summary>
    public interface IQuickImportExport
    {
        object InputFeatures { get;set;}

        object OutputFile { get;set;}

        QuickType QType { get;set;}

        void Execute();
    }
}
