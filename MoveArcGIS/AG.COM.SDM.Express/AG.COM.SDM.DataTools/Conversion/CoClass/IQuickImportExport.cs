namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// ���ٵ��뵼��
    /// </summary>
    public interface IQuickImportExport
    {
        object InputFeatures { get;set;}

        object OutputFile { get;set;}

        QuickType QType { get;set;}

        void Execute();
    }
}
