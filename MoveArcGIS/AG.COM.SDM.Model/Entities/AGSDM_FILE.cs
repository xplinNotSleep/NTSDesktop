using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	附件
    /// </summary>
    [Serializable]
    public class AGSDM_FILE : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private decimal? m_FKID;
        private string m_FKTYPE;
        private string m_FILENAME;
        private string m_FILEPATH;
        private string m_FILEEXT;
        private DateTime? m_CREATETIME;
        private decimal? m_CREATEUSERID;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_FILE()
        {
            m_ID = 0;
            m_FKID = null;
            m_FKTYPE = null;
            m_FILENAME = null;
            m_FILEPATH = null;
            m_FILEEXT = null;
            m_CREATETIME = null;
            m_CREATEUSERID = null;

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
        /// 外键ID
        /// </summary>	
        public virtual decimal? FKID
        {
            get { return m_FKID; }
            set { m_IsChanged |= (m_FKID != value); m_FKID = value; }
        }
        /// <summary>
        /// 外键类型
        /// </summary>	
        public virtual string FKTYPE
        {
            get { return m_FKTYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for FKTYPE", value, value.ToString());
                m_IsChanged |= (m_FKTYPE != value); m_FKTYPE = value;
            }
        }
        /// <summary>
        /// 文件名
        /// </summary>	
        public virtual string FILENAME
        {
            get { return m_FILENAME; }
            set
            {
                if (value != null)
                    if (value.Length > 500)
                        throw new ArgumentOutOfRangeException("Invalid value for FILENAME", value, value.ToString());
                m_IsChanged |= (m_FILENAME != value); m_FILENAME = value;
            }
        }
        /// <summary>
        /// 文件全路径
        /// </summary>	
        public virtual string FILEPATH
        {
            get { return m_FILEPATH; }
            set
            {
                if (value != null)
                    if (value.Length > 500)
                        throw new ArgumentOutOfRangeException("Invalid value for FILEPATH", value, value.ToString());
                m_IsChanged |= (m_FILEPATH != value); m_FILEPATH = value;
            }
        }
        /// <summary>
        /// 扩展名
        /// </summary>	
        public virtual string FILEEXT
        {
            get { return m_FILEEXT; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for FILEEXT", value, value.ToString());
                m_IsChanged |= (m_FILEEXT != value); m_FILEEXT = value;
            }
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
        /// 创建用户ID
        /// </summary>	
        public virtual decimal? CREATEUSERID
        {
            get { return m_CREATEUSERID; }
            set { m_IsChanged |= (m_CREATEUSERID != value); m_CREATEUSERID = value; }
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
            AGSDM_FILE castObj = (AGSDM_FILE)obj;
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

