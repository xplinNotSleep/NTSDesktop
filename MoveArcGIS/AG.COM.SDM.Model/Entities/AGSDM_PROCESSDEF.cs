using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	流程定义
    /// </summary>
    [Serializable]
    public class AGSDM_PROCESSDEF : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_NAME;
        private string m_GUID;
        private string m_VIEWFORMCLASS;
        private string m_USABLE;
        private decimal? m_SORT;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_PROCESSDEF()
        {
            m_ID = 0;
            m_NAME = null;
            m_GUID = null;
            m_VIEWFORMCLASS = null;
            m_USABLE = null;
            m_SORT = null;

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
        /// 查看窗体类名
        /// </summary>	
        public virtual string VIEWFORMCLASS
        {
            get { return m_VIEWFORMCLASS; }
            set
            {
                if (value != null)
                    if (value.Length > 200)
                        throw new ArgumentOutOfRangeException("Invalid value for VIEWFORMCLASS", value, value.ToString());
                m_IsChanged |= (m_VIEWFORMCLASS != value); m_VIEWFORMCLASS = value;
            }
        }
        /// <summary>
        /// 是否可用(0=否,1=是)
        /// </summary>	
        public virtual string USABLE
        {
            get { return m_USABLE; }
            set
            {
                if (value != null)
                    if (value.Length > 5)
                        throw new ArgumentOutOfRangeException("Invalid value for USABLE", value, value.ToString());
                m_IsChanged |= (m_USABLE != value); m_USABLE = value;
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
            AGSDM_PROCESSDEF castObj = (AGSDM_PROCESSDEF)obj;
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

