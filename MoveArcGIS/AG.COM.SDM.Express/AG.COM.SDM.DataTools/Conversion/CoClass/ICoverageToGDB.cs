namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// ConverageToGBD½Ó¿Ú
    /// </summary>
    public interface ICoverageToGDB
    {
        object InputFeatures { get;set;}
        object OutputFile { get;set;}
        void Execute();
    }
}
