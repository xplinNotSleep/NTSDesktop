using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Database
{
    /// <summary>
    /// 错误日志记录类
    /// </summary>
    public class DBErrorLog
    {
        private static string m_logFullName;
        private static bool m_isStartLog = false;

        public static bool IsStartLog
        {
            get 
            { 
                return m_isStartLog; 
            }
            set 
            {
                m_isStartLog = value;
                //创建日志文件
                m_logFullName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".log";
            }
        }
        public static string LogFullName
        {
            get { return m_logFullName; }
        }

        public static void WriteLog(Exception ex)
        {
            if (ex == null) return;

            System.IO.StreamWriter sw = null;
            using (sw = new System.IO.StreamWriter(m_logFullName, true))
            {
                sw.WriteLine(ex.ToString());
            }
        }

        public static void WriteLog(List<Exception> exList)
        {
            if (exList == null) return;

            System.IO.StreamWriter sw = null;
            using (sw = new System.IO.StreamWriter(m_logFullName, true))
            {
                foreach (Exception ex in exList)
                    sw.WriteLine(ex.ToString());
            }
        }

        public static void WriteLog(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            System.IO.StreamWriter sw = null;
            using (sw = new System.IO.StreamWriter(m_logFullName, true))
            {
                sw.WriteLine(message);
            }
        }

        public static void WriteLog(List<string> messageList)
        {
            if (messageList == null) return;

            System.IO.StreamWriter sw = null;
            using (sw = new System.IO.StreamWriter(m_logFullName, true))
            {
                foreach (string message in messageList)
                    sw.WriteLine(message);
            }
        }
    }
}
