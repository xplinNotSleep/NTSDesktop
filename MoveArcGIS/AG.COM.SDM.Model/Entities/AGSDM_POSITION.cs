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
    public class AGSDM_POSITION
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;
        private decimal m_ID;
        private decimal m_ORG_ID;
        private decimal m_OPU_ID;
        private string m_POS_NAME;
        private string m_POS_FUNCTION;
        private string m_DESCRIPTION;
        private string m_EMAIL;
        private string m_RANK;
        private string m_NO;
        private string m_NO_IN_ORG;
        private string m_NO_IN_CITY;
        private string m_IS_EDITOR_CREATE;
        private string m_EDITE_TYPE;
        private string m_IS_FIRST_LEADER;
        private string m_IS_PUB;
        private string m_CAN_MAN_DEPTS;
        private string m_CREATER;
        private DateTime m_CREATE_TIME;
        private string m_UPDATE;
        private DateTime m_UPDATE_TIME;
        private string m_DEL_FLAG;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_POSITION()
        {
            m_ID = 0;
            m_ORG_ID = -1;
            m_OPU_ID = -1;
            m_POS_NAME = null;
            m_POS_FUNCTION = null;
            m_DESCRIPTION = null;
            m_EMAIL = null;
            m_RANK = null;
            m_NO = null;
            m_NO_IN_ORG = null;
            m_NO_IN_CITY = null;
            m_IS_EDITOR_CREATE = null;
            m_EDITE_TYPE = null;
            m_IS_FIRST_LEADER = null;
            m_IS_PUB = null;
            m_CAN_MAN_DEPTS = null;
            m_CREATER = null;
            m_CREATE_TIME = DateTime.MinValue;
            m_UPDATE = null;
            m_UPDATE_TIME = DateTime.MinValue;
            m_DEL_FLAG = null;
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
        public virtual decimal ORG_ID
        {
            get { return m_ORG_ID; }
            set { m_IsChanged |= (m_ORG_ID != value); m_ORG_ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual decimal OPU_ID
        {
            get { return m_OPU_ID; }
            set { m_IsChanged |= (m_OPU_ID != value); m_OPU_ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string POS_NAME
        {
            get { return m_POS_NAME; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for POS_NAME", value, value.ToString());

                m_IsChanged |= (m_POS_NAME != value); m_POS_NAME = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string POS_FUNCTION
        {
            get { return m_POS_FUNCTION; }
            set
            {
                if (value != null)
                    if (value.Length > 200)
                        throw new ArgumentOutOfRangeException("Invalid value for POS_FUNCTION", value, value.ToString());

                m_IsChanged |= (m_POS_FUNCTION != value); m_POS_FUNCTION = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string DESCRIPTION
        {
            get { return m_DESCRIPTION; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for DESCRIPTION", value, value.ToString());

                m_IsChanged |= (m_DESCRIPTION != value); m_DESCRIPTION = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string EMAIL
        {
            get { return m_EMAIL; }
            set
            {
                if (value != null)
                    if (value.Length > 30)
                        throw new ArgumentOutOfRangeException("Invalid value for EMAIL", value, value.ToString());

                m_IsChanged |= (m_EMAIL != value); m_EMAIL = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string RANK
        {
            get { return m_RANK; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for RANK", value, value.ToString());

                m_IsChanged |= (m_RANK != value); m_RANK = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string NO
        {
            get { return m_NO; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for NO", value, value.ToString());

                m_IsChanged |= (m_NO != value); m_NO = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string NO_IN_ORG
        {
            get { return m_NO_IN_ORG; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for NO_IN_ORG", value, value.ToString());

                m_IsChanged |= (m_NO_IN_ORG != value); m_NO_IN_ORG = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string NO_IN_CITY
        {
            get { return m_NO_IN_CITY; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for NO_IN_CITY", value, value.ToString());

                m_IsChanged |= (m_NO_IN_CITY != value); m_NO_IN_CITY = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string IS_EDITOR_CREATE
        {
            get { return m_IS_EDITOR_CREATE; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for IS_EDITOR_CREATE", value, value.ToString());

                m_IsChanged |= (m_IS_EDITOR_CREATE != value); m_IS_EDITOR_CREATE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string EDITE_TYPE
        {
            get { return m_EDITE_TYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for EDITE_TYPE", value, value.ToString());

                m_IsChanged |= (m_EDITE_TYPE != value); m_EDITE_TYPE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string IS_FIRST_LEADER
        {
            get { return m_IS_FIRST_LEADER; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for IS_FIRST_LEADER", value, value.ToString());

                m_IsChanged |= (m_IS_FIRST_LEADER != value); m_IS_FIRST_LEADER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string IS_PUB
        {
            get { return m_IS_PUB; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for IS_PUB", value, value.ToString());

                m_IsChanged |= (m_IS_PUB != value); m_IS_PUB = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string CAN_MAN_DEPTS
        {
            get { return m_CAN_MAN_DEPTS; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for CAN_MAN_DEPTS", value, value.ToString());

                m_IsChanged |= (m_CAN_MAN_DEPTS != value); m_CAN_MAN_DEPTS = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string CREATER
        {
            get { return m_CREATER; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for CREATER", value, value.ToString());

                m_IsChanged |= (m_CREATER != value); m_CREATER = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime CREATE_TIME
        {
            get { return m_CREATE_TIME; }
            set { m_IsChanged |= (m_CREATE_TIME != value); m_CREATE_TIME = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string UPDATER
        {
            get { return m_UPDATE; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for UPDATE", value, value.ToString());

                m_IsChanged |= (m_UPDATE != value); m_UPDATE = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime UPDATE_TIME
        {
            get { return m_UPDATE_TIME; }
            set { m_IsChanged |= (m_UPDATE_TIME != value); m_UPDATE_TIME = value; }
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
            AGSDM_POSITION castObj = (AGSDM_POSITION)obj;
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
