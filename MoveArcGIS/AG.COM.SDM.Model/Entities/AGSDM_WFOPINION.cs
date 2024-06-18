using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	工作流意见
    /// </summary>
    [Serializable]
    public class AGSDM_WFOPINION : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private decimal? m_ACTIVITYINSTID;
        private string m_ACTIVITYDEFGUID;
        private decimal? m_PROCESSINSTID;
        private decimal? m_USERID;
        private DateTime? m_UPDATETIME;
        private string m_OPINIONCONTENT;
        private string m_NAME;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_WFOPINION()
        {
            m_ID = 0;
            m_ACTIVITYINSTID = null;
            m_ACTIVITYDEFGUID = null;
            m_PROCESSINSTID = null;
            m_USERID = null;
            m_UPDATETIME = null;
            m_OPINIONCONTENT = null;
            m_NAME = null;
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
        /// 活动实例ID
        /// </summary>	
        public virtual decimal? ACTIVITYINSTID
        {
            get { return m_ACTIVITYINSTID; }
            set { m_IsChanged |= (m_ACTIVITYINSTID != value); m_ACTIVITYINSTID = value; }
        }
        /// <summary>
        /// 活动定义GUID
        /// </summary>	
        public virtual string ACTIVITYDEFGUID
        {
            get { return m_ACTIVITYDEFGUID; }
            set
            {
                if (value != null)
                    if (value.Length > 36)
                        throw new ArgumentOutOfRangeException("Invalid value for ACTIVITYDEFGUID", value, value.ToString());
                m_IsChanged |= (m_ACTIVITYDEFGUID != value); m_ACTIVITYDEFGUID = value;
            }
        }
        /// <summary>
        /// 流程实例ID
        /// </summary>	
        public virtual decimal? PROCESSINSTID
        {
            get { return m_PROCESSINSTID; }
            set { m_IsChanged |= (m_PROCESSINSTID != value); m_PROCESSINSTID = value; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>	
        public virtual decimal? USERID
        {
            get { return m_USERID; }
            set { m_IsChanged |= (m_USERID != value); m_USERID = value; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>	
        public virtual DateTime? UPDATETIME
        {
            get { return m_UPDATETIME; }
            set { m_IsChanged |= (m_UPDATETIME != value); m_UPDATETIME = value; }
        }
        /// <summary>
        /// 意见内容
        /// </summary>	
        public virtual string OPINIONCONTENT
        {
            get { return m_OPINIONCONTENT; }
            set
            {
                if (value != null)
                    if (value.Length > 2000)
                        throw new ArgumentOutOfRangeException("Invalid value for OPINIONCONTENT", value, value.ToString());
                m_IsChanged |= (m_OPINIONCONTENT != value); m_OPINIONCONTENT = value;
            }
        }

        /// <summary>
        /// 名字
        /// </summary>	
        public virtual string NAME
        {
            get { return m_NAME; }
            set
            {
                if (value != null)
                    if (value.Length > 36)
                        throw new ArgumentOutOfRangeException("Invalid value for NAME", value, value.ToString());
                m_IsChanged |= (m_NAME != value); m_NAME = value;
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
            AGSDM_WFOPINION castObj = (AGSDM_WFOPINION)obj;
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

