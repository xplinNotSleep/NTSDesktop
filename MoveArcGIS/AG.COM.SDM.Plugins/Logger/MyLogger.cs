using AG.COM.SDM.Config;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Plugins.Logger
{
    /// <summary>
    ///Depiction: 重新写的日志记录类（NHibernate）
    /// </summary>
    /// Writer:徐斌
    /// Create Date:2010-9-9
    public class MyLogger : ILog
    {
        private static MyLogger m_DBLogger;

        //获取日志实例对象
        public static MyLogger GetInstance()
        {
            if (m_DBLogger == null)
            {
                m_DBLogger = new MyLogger();
            }

            return m_DBLogger;
        }

        /// <summary>
        /// 私有实例化构造函数
        /// </summary>
        private MyLogger()
        {

        }

        /// <summary>
        /// 写入日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logType">日志类别</param>
        public void WriteMessage(string message, string logType)
        {
            this.WriteMessage(message, "正常操作", LogLevel.Info);
        }

        /// <summary>
        /// 写入日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logType">日志类别</param>
        /// <param name="loglevel">日志级别</param>
        public void WriteMessage(string message, string logType, LogLevel loglevel)
        {
            try
            {
                AGSDM_LOGGER tLoger = new AGSDM_LOGGER();
                tLoger.USERNAME = System.Environment.UserName;
                tLoger.HOSTNAME = System.Environment.MachineName;
                tLoger.LOGTIME = DateTime.Now;
                tLoger.LOGUSER = SystemInfo.UserName;
                tLoger.LOGMSG = message;
                tLoger.LOGTYPE = logType;
                tLoger.LOGLEVEL = loglevel.ToString();
                tLoger.PRODUCTNAME = SystemInfo.ProductName;
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                //保存数据源
                tEntityHandler.AddEntity(tLoger);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
                return;
            }
        }

        /// <summary>
        /// 写入日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logType">日志类别</param>
        /// <param name="loglevel">日志级别</param>
        public void WriteMessage(string message, string logType, OperateLevel loglevel)
        {
            try
            {
                AGSDM_LOGGER tLoger = new AGSDM_LOGGER();
                tLoger.USERNAME = System.Environment.UserName;
                tLoger.HOSTNAME = System.Environment.MachineName;
                tLoger.LOGTIME = DateTime.Now;
                tLoger.LOGUSER = SystemInfo.UserName;
                tLoger.LOGMSG = message;
                tLoger.LOGTYPE = logType;
                tLoger.LOGLEVEL = loglevel.ToString();
                tLoger.PRODUCTNAME = SystemInfo.ProductName;
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                //保存数据源
                tEntityHandler.AddEntity(tLoger);
            }
            catch (Exception ex)
            {             
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");    
                return;
            }
        }

        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="ex">错误消息</param>
        /// <param name="logType">日志级别</param>
        public void WriteError(Exception ex, string logType)
        {
            string message = ex.Message;
            this.WriteMessage(message, logType, LogLevel.Error);
        }

    }
}
