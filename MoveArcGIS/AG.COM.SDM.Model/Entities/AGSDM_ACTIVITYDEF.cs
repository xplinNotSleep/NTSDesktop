using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	活动定义
    /// </summary>
    [Serializable]
    public class AGSDM_ACTIVITYDEF : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_PROCESSDEFID;
        private string m_NAME;
        private string m_GUID;
        private decimal? m_SORT;
        private string m_FORMCLASS;
        private int? m_ACTUSERTYPE;
        private string m_SAMEUSERACTGUID;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_ACTIVITYDEF()
        {
            m_ID = 0;
            m_PROCESSDEFID = null;
            m_NAME = null;
            m_GUID = null;
            m_SORT = null;
            m_FORMCLASS = null;
            m_ACTUSERTYPE = null;
            m_SAMEUSERACTGUID = null;

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
        /// 名称
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
        /// 唯一ID
        /// </summary>	
        public virtual string GUID
        {
            get { return m_GUID; }
            set
            {
                if (value != null)
                    if (value.Length > 36)
                        throw new ArgumentOutOfRangeException("Invalid value for GUID", value, value.ToString());
                m_IsChanged |= (m_GUID != value); m_GUID = value;
            }
        }
        /// <summary>
        /// 序号
        /// </summary>	
        public virtual decimal? SORT
        {
            get { return m_SORT; }
            set { m_IsChanged |= (m_SORT != value); m_SORT = value; }
        }
        /// <summary>
        /// 窗体类名
        /// </summary>	
        public virtual string FORMCLASS
        {
            get { return m_FORMCLASS; }
            set
            {
                if (value != null)
                    if (value.Length > 200)
                        throw new ArgumentOutOfRangeException("Invalid value for FORMCLASS", value, value.ToString());
                m_IsChanged |= (m_FORMCLASS != value); m_FORMCLASS = value;
            }
        }
        /// <summary>
        /// 参与者类型
        /// </summary>	
        public virtual int? ACTUSERTYPE
        {
            get { return m_ACTUSERTYPE; }
            set { m_IsChanged |= (m_ACTUSERTYPE != value); m_ACTUSERTYPE = value; }
        }
        /// <summary>
        /// 相同参与者活动GUID
        /// </summary>	
        public virtual string SAMEUSERACTGUID
        {
            get { return m_SAMEUSERACTGUID; }
            set
            {
                if (value != null)
                    if (value.Length > 36)
                        throw new ArgumentOutOfRangeException("Invalid value for SAMEUSERACTGUID", value, value.ToString());
                m_IsChanged |= (m_SAMEUSERACTGUID != value); m_SAMEUSERACTGUID = value;
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
            AGSDM_ACTIVITYDEF castObj = (AGSDM_ACTIVITYDEF)obj;
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

