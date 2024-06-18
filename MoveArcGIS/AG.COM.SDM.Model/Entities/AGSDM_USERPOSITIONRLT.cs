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
    public class AGSDM_USERPOSITIONRLT
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;
        private decimal m_ID;
        private decimal m_USER_ID;
        private decimal m_POS_ID;
        private string m_IS_MAIN;
        private string m_DEL_FLAG;
        private DateTime m_ASSIGN_TIME;
        private string m_ASSIGN_USER;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_USERPOSITIONRLT()
        {
            m_ID = 0;
            m_USER_ID = -1;
            m_POS_ID = -1;
            m_IS_MAIN = null;
            m_DEL_FLAG = null;
            m_ASSIGN_TIME = DateTime.MinValue;
            m_ASSIGN_USER = null;
        }
        #endregion

        #region 公有属性

        /// <summary>
        /// 
        /// </summary>		
        public virtual decimal ID
        {
            get { return m_ID; }
            set { m_IsChanged |= (m_ID != value); m_ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual decimal USER_ID
        {
            get { return m_USER_ID; }
            set { m_IsChanged |= (m_USER_ID != value); m_USER_ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual decimal POS_ID
        {
            get { return m_POS_ID; }
            set { m_IsChanged |= (m_POS_ID != value); m_POS_ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string IS_MAIN
        {
            get { return m_IS_MAIN; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for IS_MAIN", value, value.ToString());

                m_IsChanged |= (m_IS_MAIN != value); m_IS_MAIN = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string DEL_FLAG
        {
            get { return m_DEL_FLAG; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for DEL_FLAG", value, value.ToString());

                m_IsChanged |= (m_DEL_FLAG != value); m_DEL_FLAG = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime ASSIGN_TIME
        {
            get { return m_ASSIGN_TIME; }
            set { m_IsChanged |= (m_ASSIGN_TIME != value); m_ASSIGN_TIME = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string ASSIGN_USER
        {
            get { return m_ASSIGN_USER; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for ASSIGN_USER", value, value.ToString());

                m_IsChanged |= (m_ASSIGN_USER != value); m_ASSIGN_USER = value;
            }
        }

        /// <summary>
        /// 对象的值是否被改变
        /// </summary>
        public virtual bool IsChanged
        {
            get { return m_IsChanged; }
        }

        /// <summary>
        /// 对象是否已经被删除
        /// </summary>
        public virtual bool IsDeleted
        {
            get { return m_IsDeleted; }
        }

        #endregion

        #region 公有函数

        /// <summary>
        /// 标记对象已删除
        /// </summary>
        public virtual void MarkAsDeleted()
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
            AGSDM_USERPOSITIONRLT castObj = (AGSDM_USERPOSITIONRLT)obj;
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
