using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Database
{
    /// <summary>
    /// Minio服务器配置
    /// </summary>
    [Serializable]
    public class MinioServer
    {
        private string m_ServerURL;
        private string m_AccessName;
        private string m_PassWord;
        private string m_BucketName;

        public string ServerURL
        {
            get
            {
                return m_ServerURL;
            }
            set
            {
                m_ServerURL = value;
            }
        }

        public string AccessName
        {
            get
            {
                return m_AccessName;
            }
            set
            {
                m_AccessName = value;
            }
        }

        public string PassWord
        {
            get
            {
                return m_PassWord;
            }
            set
            {
                m_PassWord = value;
            }
        }

        public string BucketName
        {
            get
            {
                return m_BucketName;
            }
            set
            {
                m_BucketName = value;
            }
        }
        
    }
}
