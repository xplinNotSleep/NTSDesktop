using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	地图文档
    /// </summary>
    [Serializable]
    public class AGSDM_MAPDOC : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_NAME;
        private string m_MXDFILE;
        private string m_DESCRIPTION;
        private DateTime? m_UPDATETIME;
        private DateTime? m_CREATETIME;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_MAPDOC()
        {
            m_ID = 0;
            m_NAME = null;
            m_MXDFILE = null;
            m_DESCRIPTION = null;
            m_UPDATETIME = null;
            m_CREATETIME = null;

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
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for NAME", value, value.ToString());
                m_IsChanged |= (m_NAME != value); m_NAME = value;
            }
        }
        /// <summary>
        /// Mxd文件名
        /// </summary>	
        public virtual string MXDFILE
        {
            get { return m_MXDFILE; }
            set
            {
                if (value != null)
                    if (value.Length > 200)
                        throw new ArgumentOutOfRangeException("Invalid value for MXDFILE", value, value.ToString());
                m_IsChanged |= (m_MXDFILE != value); m_MXDFILE = value;
            }
        }
        /// <summary>
        /// 描述
        /// </summary>	
        public virtual string DESCRIPTION
        {
            get { return m_DESCRIPTION; }
            set
            {
                if (value != null)
                    if (value.Length > 500)
                        throw new ArgumentOutOfRangeException("Invalid value for DESCRIPTION", value, value.ToString());
                m_IsChanged |= (m_DESCRIPTION != value); m_DESCRIPTION = value;
            }
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
        /// 创建时间
        /// </summary>	
        public virtual DateTime? CREATETIME
        {
            get { return m_CREATETIME; }
            set { m_IsChanged |= (m_CREATETIME != value); m_CREATETIME = value; }
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
            AGSDM_MAPDOC castObj = (AGSDM_MAPDOC)obj;
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

