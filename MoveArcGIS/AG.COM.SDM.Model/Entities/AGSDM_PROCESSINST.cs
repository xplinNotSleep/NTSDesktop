using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	流程实例
    /// </summary>
    [Serializable]
    public class AGSDM_PROCESSINST : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_PROCESSDEFID;
        private string m_NAME;
        private string m_TITLE;
        private string m_ACTIVITYTEXT;
        private int? m_STATE;
        private decimal? m_CREATEUSERID;
        private DateTime? m_CREATETIME;
        private DateTime? m_UPDATETIME;
        private string m_BSID;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_PROCESSINST()
        {
            m_ID = 0;
            m_PROCESSDEFID = null;
            m_NAME = null;
            m_TITLE = null;
            m_ACTIVITYTEXT = null;
            m_STATE = null;
            m_CREATEUSERID = null;
            m_CREATETIME = null;
            m_UPDATETIME = null;
            m_BSID = null;
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
        /// BSID
        /// </summary>	
        public virtual string BSID
        {
            get { return m_BSID; }
            set { m_IsChanged |= (m_BSID != value); m_BSID = value; }
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
        /// 流程实例名称
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
        /// 流程主题
        /// </summary>	
        public virtual string TITLE
        {
            get { return m_TITLE; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for TITLE", value, value.ToString());
                m_IsChanged |= (m_TITLE != value); m_TITLE = value;
            }
        }
        /// <summary>
        /// 当前活动文本
        /// </summary>	
        public virtual string ACTIVITYTEXT
        {
            get { return m_ACTIVITYTEXT; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for ACTIVITYTEXT", value, value.ToString());
                m_IsChanged |= (m_ACTIVITYTEXT != value); m_ACTIVITYTEXT = value;
            }
        }
        /// <summary>
        /// 当前状态
        /// </summary>	
        public virtual int? STATE
        {
            get { return m_STATE; }
            set { m_IsChanged |= (m_STATE != value); m_STATE = value; }
        }
        /// <summary>
        /// 创建用户ID
        /// </summary>	
        public virtual decimal? CREATEUSERID
        {
            get { return m_CREATEUSERID; }
            set { m_IsChanged |= (m_CREATEUSERID != value); m_CREATEUSERID = value; }
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
        /// 修改时间
        /// </summary>	
        public virtual DateTime? UPDATETIME
        {
            get { return m_UPDATETIME; }
            set { m_IsChanged |= (m_UPDATETIME != value); m_UPDATETIME = value; }
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
            AGSDM_PROCESSINST castObj = (AGSDM_PROCESSINST)obj;
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

