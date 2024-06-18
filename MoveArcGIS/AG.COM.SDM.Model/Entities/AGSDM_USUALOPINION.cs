using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	常用意见
    /// </summary>
    [Serializable]
    public class AGSDM_USUALOPINION : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private decimal? m_USERID;
        private string m_OPINIONCONTENT;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_USUALOPINION()
        {
            m_ID = 0;
            m_USERID = null;
            m_OPINIONCONTENT = null;

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
        /// 用户ID
        /// </summary>	
        public virtual decimal? USERID
        {
            get { return m_USERID; }
            set { m_IsChanged |= (m_USERID != value); m_USERID = value; }
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
                    if (value.Length > 500)
                        throw new ArgumentOutOfRangeException("Invalid value for OPINIONCONTENT", value, value.ToString());
                m_IsChanged |= (m_OPINIONCONTENT != value); m_OPINIONCONTENT = value;
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
            AGSDM_USUALOPINION castObj = (AGSDM_USUALOPINION)obj;
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

