using System;

namespace AG.COM.SDM.Utility.Logger
{
    /// <summary>
    /// 写日志接口
    /// </summary>
    public interface ILog
    {
        void WriteMessage(string message, string logType);
        void WriteMessage(string message, string logType, LogLevel loglevel);
        void WriteError(Exception ex, string logType);          
    }
}
