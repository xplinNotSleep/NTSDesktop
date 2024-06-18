/*
/*NHibernate映射代码模板
/*作者：DDL
/*版本更新和支持：http://renrenqq.cnblogs.com/
/*日期：2006年8月24日 
*/
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	
    /// </summary>
    [Serializable]
    public class AGSDM_SYSTEMCONFIG
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;
        private decimal m_ID;
        private string m_NAME;
        private string m_VALUE;
        private string m_DESCRIPTION;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_SYSTEMCONFIG()
        {
            m_ID = 0;
            m_NAME = null;
            m_VALUE = null;
            m_DESCRIPTION = null;
        }
        #endregion

        #region 公有属性

        /// <summary>
        /// 
        /// </summary>		
        public decimal ID
        {
            get { return m_ID; }
            set { m_IsChanged |= (m_ID != value); m_ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string NAME
        {
            get { return m_NAME; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for NAME", value, value.ToString());

                m_IsChanged |= (m_NAME != value); m_NAME = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string VALUE
        {
            get { return m_VALUE; }
            set
            {
                if (value != null)
                    if (value.Length > 2147483647)
                        throw new ArgumentOutOfRangeException("Invalid value for VALUE", value, value.ToString());

                m_IsChanged |= (m_VALUE != value); m_VALUE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string DESCRIPTION
        {
            get { return m_DESCRIPTION; }
            set
            {
                if (value != null)
                    if (value.Length > 200)
                        throw new ArgumentOutOfRangeException("Invalid value for DESCRIPTION", value, value.ToString());

                m_IsChanged |= (m_DESCRIPTION != value); m_DESCRIPTION = value;
            }
        }

        /// <summary>
        /// 对象的值是否被改变
        /// </summary>
        public bool IsChanged
        {
            get { return m_IsChanged; }
        }

        /// <summary>
        /// 对象是否已经被删除
        /// </summary>
        public bool IsDeleted
        {
            get { return m_IsDeleted; }
        }

        #endregion

        #region 公有函数

        /// <summary>
        /// 标记对象已删除
        /// </summary>
        public void MarkAsDeleted()
        {
            m_IsDeleted = true;
            m_IsChanged = true;
        }


        #endregion

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            AGSDM_SYSTEMCONFIG castObj = (AGSDM_SYSTEMCONFIG)obj;
            return (castObj != null) &&
                (m_ID == castObj.ID);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * m_ID.GetHashCode();
            return hash;
        }
        #endregion

    }
}
