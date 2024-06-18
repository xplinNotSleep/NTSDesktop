using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	活动实例
    /// </summary>
    [Serializable]
    public class AGSDM_ACTIVITYINST : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_ACTIVITYDEFID;
        private decimal? m_PROCESSINSTID;
        private string m_NAME;
        private decimal? m_WFUSERID;
        private int? m_WFUSERTYPE;
        private int? m_STATE;
        private int? m_ISDOING;
        private DateTime? m_CREATETIME;
        private decimal? m_SUBMITUSERID;
        private DateTime? m_SUBMITTIME;
        private string m_EXT1;
        private string m_EXT2;
        private string m_EXT3;
        private decimal? m_HANDLEUSERID;
        private decimal? m_RECEIVEUSERID;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_ACTIVITYINST()
        {
            m_ID = 0;
            m_ACTIVITYDEFID = null;
            m_PROCESSINSTID = null;
            m_NAME = null;
            m_WFUSERID = null;
            m_WFUSERTYPE = null;
            m_STATE = null;
            m_ISDOING = null;
            m_CREATETIME = null;
            m_SUBMITUSERID = null;
            m_SUBMITTIME = null;
            m_EXT1 = null;
            m_EXT2 = null;
            m_EXT3 = null;
            m_HANDLEUSERID = null;
            m_RECEIVEUSERID = null;

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
        /// 流程实例ID
        /// </summary>	
        public virtual decimal? PROCESSINSTID
        {
            get { return m_PROCESSINSTID; }
            set { m_IsChanged |= (m_PROCESSINSTID != value); m_PROCESSINSTID = value; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>	
        public virtual string NAME
        {
            get { return m_NAME; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for NAME", value, value.ToString());
                m_IsChanged |= (m_NAME != value); m_NAME = value;
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
        /// 活动状态
        /// </summary>	
        public virtual int? STATE
        {
            get { return m_STATE; }
            set { m_IsChanged |= (m_STATE != value); m_STATE = value; }
        }
        /// <summary>
        /// 是否在办
        /// </summary>	
        public virtual int? ISDOING
        {
            get { return m_ISDOING; }
            set { m_IsChanged |= (m_ISDOING != value); m_ISDOING = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>	
        public virtual DateTime? CREATETIME
        {
            get { return m_CREATETIME; }
            set { m_IsChanged |= (m_CREATETIME != value); m_CREATETIME = value; }
        }
        /// <summary>
        /// 提交用户ID
        /// </summary>	
        public virtual decimal? SUBMITUSERID
        {
            get { return m_SUBMITUSERID; }
            set { m_IsChanged |= (m_SUBMITUSERID != value); m_SUBMITUSERID = value; }
        }
        /// <summary>
        /// 提交时间
        /// </summary>	
        public virtual DateTime? SUBMITTIME
        {
            get { return m_SUBMITTIME; }
            set { m_IsChanged |= (m_SUBMITTIME != value); m_SUBMITTIME = value; }
        }
        /// <summary>
        /// 扩展字段1
        /// </summary>	
        public virtual string EXT1
        {
            get { return m_EXT1; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT1", value, value.ToString());
                m_IsChanged |= (m_EXT1 != value); m_EXT1 = value;
            }
        }
        /// <summary>
        /// 扩展字段2
        /// </summary>	
        public virtual string EXT2
        {
            get { return m_EXT2; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT2", value, value.ToString());
                m_IsChanged |= (m_EXT2 != value); m_EXT2 = value;
            }
        }
        /// <summary>
        /// 扩展字段3
        /// </summary>	
        public virtual string EXT3
        {
            get { return m_EXT3; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT3", value, value.ToString());
                m_IsChanged |= (m_EXT3 != value); m_EXT3 = value;
            }
        }
        /// <summary>
        /// 指定办理用户ID
        /// </summary>	
        public virtual decimal? HANDLEUSERID
        {
            get { return m_HANDLEUSERID; }
            set { m_IsChanged |= (m_HANDLEUSERID != value); m_HANDLEUSERID = value; }
        }
        /// <summary>
        /// 接收用户ID
        /// </summary>	
        public virtual decimal? RECEIVEUSERID
        {
            get { return m_RECEIVEUSERID; }
            set { m_IsChanged |= (m_RECEIVEUSERID != value); m_RECEIVEUSERID = value; }
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
            AGSDM_ACTIVITYINST castObj = (AGSDM_ACTIVITYINST)obj;
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

