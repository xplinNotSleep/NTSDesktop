using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	活动参与者定义
    /// </summary>
    [Serializable]
    public class AGSDM_ACTUSERDEF : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_PROCESSDEFID;
        private string m_ACTIVITYDEFID;
        private decimal? m_WFUSERID;
        private int? m_WFUSERTYPE;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_ACTUSERDEF()
        {
            m_ID = 0;
            m_PROCESSDEFID = null;
            m_ACTIVITYDEFID = null;
            m_WFUSERID = null;
            m_WFUSERTYPE = null;

        }
        #endregion

        #region 公有属性

        /// <summary>
        /// ID
        /// </summary>	
        public virtual decimal ID
        {
            get { return m_ID; }
            set { m_IsChanged |= (m_ID != value); m_ID = value; }
        }
        /// <summary>
        /// 流程定义ID
        /// </summary>	
        public virtual string PROCESSDEFID
        {
            get { return m_PROCESSDEFID; }
            set
            {
                if (value != null)
                    if (value.Length > 36)
                        throw new ArgumentOutOfRangeException("Invalid value for PROCESSDEFID", value, value.ToString());
                m_IsChanged |= (m_PROCESSDEFID != value); m_PROCESSDEFID = value;
            }
        }
        /// <summary>
        /// 活动定义ID
        /// </summary>	
        public virtual string ACTIVITYDEFID
        {
            get { return m_ACTIVITYDEFID; }
            set
            {
                if (value != null)
                    if (value.Length > 36)
                        throw new ArgumentOutOfRangeException("Invalid value for ACTIVITYDEFID", value, value.ToString());
                m_IsChanged |= (m_ACTIVITYDEFID != value); m_ACTIVITYDEFID = value;
            }
        }
        /// <summary>
        /// WF用户ID
        /// </summary>	
        public virtual decimal? WFUSERID
        {
            get { return m_WFUSERID; }
            set { m_IsChanged |= (m_WFUSERID != value); m_WFUSERID = value; }
        }
        /// <summary>
        /// WF用户类型
        /// </summary>	
        public virtual int? WFUSERTYPE
        {
            get { return m_WFUSERTYPE; }
            set { m_IsChanged |= (m_WFUSERTYPE != value); m_WFUSERTYPE = value; }
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
            AGSDM_ACTUSERDEF castObj = (AGSDM_ACTUSERDEF)obj;
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

        #region IChange 成员

        public void MakeAsDefault()
        {
            m_IsChanged = false;
        }

        #endregion
    }
}

