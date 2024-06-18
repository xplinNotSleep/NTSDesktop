using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Config
{
    public class MinioServerConfig
    {
        //Minio服务器配置参数
        private string m_MinioServerURL;
        private string m_MinioAccessName;
        private string m_MinioPassWord;
        private string m_MinioServerBucket;
        private static MinioServerConfig m_MinioServerConfig;

        /// <summary>
        /// 获取OleDBConfig实例对象
        /// </summary>
        /// <returns>返回OleDBConfig单件实例</returns>
        public static MinioServerConfig GetInstance()
        {
            if (m_MinioServerConfig == null)
            {
                m_MinioServerConfig = new MinioServerConfig();
            }

            return m_MinioServerConfig;
        }

        public string MinioServerURL
        {
            get
            {
                return this.m_MinioServerURL;
            }
            set
            {
                m_MinioServerURL = value;
            }
        }

        public string MinioAccessName
        {
            get
            {
                return this.m_MinioAccessName;
            }
            set
            {
                m_MinioAccessName = value;
            }
        }

        public string MinioPassWord
        {
            get
            {
                return this.m_MinioPassWord;
            }
            set
            {
                m_MinioPassWord = value;
            }
        }

        public string MinioServerBucket
        {
            get
            {
                return this.m_MinioServerBucket;
            }
            set
            {
                m_MinioServerBucket = value;
            }
        }

    }
}
