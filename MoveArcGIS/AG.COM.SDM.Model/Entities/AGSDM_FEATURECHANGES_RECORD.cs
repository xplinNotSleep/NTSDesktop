using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	AGP_FEATURECHANGES_RECORD
    /// </summary>
    [Serializable]
    public class AGSDM_FEATURECHANGES_RECORD : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private decimal? m_USRID;
        private string m_USRNAME;
        private DateTime? m_MODI_TIME;
        private string m_MODI_TABLE;
        private decimal? m_FEATUREID;
        private string m_FEATUREUSID;
        private string m_CHANGESTYPE;
        private string m_CHANGESINFO;
        private string m_COMMENTS;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_FEATURECHANGES_RECORD()
        {
            m_ID = 0;
            m_USRID = null;
            m_USRNAME = null;
            m_MODI_TIME = null;
            m_MODI_TABLE = null;
            m_FEATUREID = null;
            m_FEATUREUSID = null;
            m_CHANGESTYPE = null;
            m_CHANGESINFO = null;
            m_COMMENTS = null;

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
        /// USRID
        /// </summary>	
        public virtual decimal? USRID
        {
            get { return m_USRID; }
            set { m_IsChanged |= (m_USRID != value); m_USRID = value; }
        }
        /// <summary>
        /// USRNAME
        /// </summary>	
        public virtual string USRNAME
        {
            get { return m_USRNAME; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for USRNAME", value, value.ToString());
                m_IsChanged |= (m_USRNAME != value); m_USRNAME = value;
            }
        }
        /// <summary>
        /// MODI_TIME
        /// </summary>	
        public virtual DateTime? MODI_TIME
        {
            get { return m_MODI_TIME; }
            set { m_IsChanged |= (m_MODI_TIME != value); m_MODI_TIME = value; }
        }
        /// <summary>
        /// MODI_TABLE
        /// </summary>	
        public virtual string MODI_TABLE
        {
            get { return m_MODI_TABLE; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for MODI_TABLE", value, value.ToString());
                m_IsChanged |= (m_MODI_TABLE != value); m_MODI_TABLE = value;
            }
        }
        /// <summary>
        /// FEATUREID
        /// </summary>	
        public virtual decimal? FEATUREID
        {
            get { return m_FEATUREID; }
            set { m_IsChanged |= (m_FEATUREID != value); m_FEATUREID = value; }
        }
        /// <summary>
        /// FEATUREUSID
        /// </summary>	
        public virtual string FEATUREUSID
        {
            get { return m_FEATUREUSID; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for FEATUREUSID", value, value.ToString());
                m_IsChanged |= (m_FEATUREUSID != value); m_FEATUREUSID = value;
            }
        }
        /// <summary>
        /// CHANGESTYPE
        /// </summary>	
        public virtual string CHANGESTYPE
        {
            get { return m_CHANGESTYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 30)
                        throw new ArgumentOutOfRangeException("Invalid value for CHANGESTYPE", value, value.ToString());
                m_IsChanged |= (m_CHANGESTYPE != value); m_CHANGESTYPE = value;
            }
        }
        /// <summary>
        /// CHANGESINFO
        /// </summary>	
        public virtual string CHANGESINFO
        {
            get { return m_CHANGESINFO; }
            set
            {
                if (value != null)
                    if (value.Length > 2000)
                        throw new ArgumentOutOfRangeException("Invalid value for CHANGESINFO", value, value.ToString());
                m_IsChanged |= (m_CHANGESINFO != value); m_CHANGESINFO = value;
            }
        }
        /// <summary>
        /// COMMENTS
        /// </summary>	
        public virtual string COMMENTS
        {
            get { return m_COMMENTS; }
            set
            {
                if (value != null)
                    if (value.Length > 500)
                        throw new ArgumentOutOfRangeException("Invalid value for COMMENTS", value, value.ToString());
                m_IsChanged |= (m_COMMENTS != value); m_COMMENTS = value;
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
            AGSDM_FEATURECHANGES_RECORD castObj = (AGSDM_FEATURECHANGES_RECORD)obj;
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