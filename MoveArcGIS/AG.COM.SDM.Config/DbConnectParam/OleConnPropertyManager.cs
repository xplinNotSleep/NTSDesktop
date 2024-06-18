using System.Collections.Generic;

namespace AG.COM.SDM.Database
{
    /// <summary>
    /// OLE数据库连接属性管理类
    /// </summary>
    public class OleConnPropertyManager
    {
        private List<OleConnProperty> m_OleConnColl;

        /// <summary>
        /// 获取或设置Ole数据库连接列表
        /// </summary>
        public List<OleConnProperty> OleConnColl
        {
            get
            {
                if (m_OleConnColl == null)
                {
                    m_OleConnColl = new List<OleConnProperty>();
                }
                return m_OleConnColl;
            }
            set
            {
                m_OleConnColl = value;
            }
        }

        /// <summary>
        /// 通过Ole标识获取Ole数据库连接属性
        /// </summary>
        /// <param name="strOleConnName">Ole标识</param>
        /// <returns>Ole数据库连接属性</returns>
        public OleConnProperty GetOleConn(string strOleConnName)
        {
            if (m_OleConnColl == null)
            {
                return null;
            }
            foreach (OleConnProperty pOleConn in m_OleConnColl)
            {
                if (pOleConn.OLE_Name == strOleConnName)
                {
                    return pOleConn;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加Ole数据库连接
        /// </summary>
        /// <param name="oleConn">Ole数据库连接</param>
        public void Add(OleConnProperty oleConn)
        {
            if (Contained(oleConn.OLE_Name))
                return;
            else 
                m_OleConnColl.Add(oleConn);
        }

        /// <summary>
        /// 删除Ole数据库连接
        /// </summary>
        /// <param name="oleConn">Ole数据库连接</param>
        public void Remove(OleConnProperty oleConn)
        {
            if (Contained(oleConn.OLE_Name))
                m_OleConnColl.Remove(oleConn);
        }

        //判断是否包括指定的Ole数据库连接
        public bool Contained(string strOleConnName)
        {
            foreach (OleConnProperty pOleConn in m_OleConnColl)
            {
                if (pOleConn.OLE_Name == strOleConnName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
