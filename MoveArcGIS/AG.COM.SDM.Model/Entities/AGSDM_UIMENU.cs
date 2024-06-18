using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	UI菜单
    /// </summary>
    [Serializable]
    public class AGSDM_UIMENU : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;

        private decimal m_ID;
        private string m_CODE;
        private string m_GUID;
        private string m_PARENTCODE;
        private string m_CAPTION;
        private string m_DLLFILE;
        private string m_CLASSNAME;
        private string m_MENUTYPE;
        private string m_EXT1;
        private string m_EXT2;
        private string m_EXT3;
        private string m_EXT4;
        private string m_EXT5;
        private string m_EXT6;
        private string m_EXT7;
        private string m_EXT8;
        private string m_EXT9;
        private string m_EXT10;
        private decimal? m_SORT;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_UIMENU()
        {
            m_ID = 0;
            m_CODE = null;
            m_GUID = null;
            m_PARENTCODE = null;
            m_CAPTION = null;
            m_DLLFILE = null;
            m_CLASSNAME = null;
            m_MENUTYPE = null;
            m_EXT1 = null;
            m_EXT2 = null;
            m_EXT3 = null;
            m_EXT4 = null;
            m_EXT5 = null;
            m_EXT6 = null;
            m_EXT7 = null;
            m_EXT8 = null;
            m_EXT9 = null;
            m_EXT10 = null;
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
        /// 编码
        /// </summary>	
        public virtual string CODE
        {
            get { return m_CODE; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for CODE", value, value.ToString());
                m_IsChanged |= (m_CODE != value); m_CODE = value;
            }
        }
        /// <summary>
        /// 唯一编码
        /// </summary>	
        public virtual string GUID
        {
            get { return m_GUID; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for GUID", value, value.ToString());
                m_IsChanged |= (m_GUID != value); m_GUID = value;
            }
        }
        /// <summary>
        /// 父编码
        /// </summary>	
        public virtual string PARENTCODE
        {
            get { return m_PARENTCODE; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for PARENTCODE", value, value.ToString());
                m_IsChanged |= (m_PARENTCODE != value); m_PARENTCODE = value;
            }
        }
        /// <summary>
        /// 标题
        /// </summary>	
        public virtual string CAPTION
        {
            get { return m_CAPTION; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for CAPTION", value, value.ToString());
                m_IsChanged |= (m_CAPTION != value); m_CAPTION = value;
            }
        }
        /// <summary>
        /// dll文件
        /// </summary>	
        public virtual string DLLFILE
        {
            get { return m_DLLFILE; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for DLLFILE", value, value.ToString());
                m_IsChanged |= (m_DLLFILE != value); m_DLLFILE = value;
            }
        }
        /// <summary>
        /// 类名
        /// </summary>	
        public virtual string CLASSNAME
        {
            get { return m_CLASSNAME; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for CLASSNAME", value, value.ToString());
                m_IsChanged |= (m_CLASSNAME != value); m_CLASSNAME = value;
            }
        }
        /// <summary>
        /// 菜单类型
        /// </summary>	
        public virtual string MENUTYPE
        {
            get { return m_MENUTYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for MENUTYPE", value, value.ToString());
                m_IsChanged |= (m_MENUTYPE != value); m_MENUTYPE = value;
            }
        }
        /// <summary>
        /// 扩展属性1
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
        /// 扩展属性2
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
        /// 扩展属性3
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
        /// 扩展属性4
        /// </summary>	
        public virtual string EXT4
        {
            get { return m_EXT4; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT4", value, value.ToString());
                m_IsChanged |= (m_EXT4 != value); m_EXT4 = value;
            }
        }
        /// <summary>
        /// 扩展属性5
        /// </summary>	
        public virtual string EXT5
        {
            get { return m_EXT5; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT5", value, value.ToString());
                m_IsChanged |= (m_EXT5 != value); m_EXT5 = value;
            }
        }
        /// <summary>
        /// 扩展属性6
        /// </summary>	
        public virtual string EXT6
        {
            get { return m_EXT6; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT6", value, value.ToString());
                m_IsChanged |= (m_EXT6 != value); m_EXT6 = value;
            }
        }
        /// <summary>
        /// 扩展属性7
        /// </summary>	
        public virtual string EXT7
        {
            get { return m_EXT7; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT7", value, value.ToString());
                m_IsChanged |= (m_EXT7 != value); m_EXT7 = value;
            }
        }
        /// <summary>
        /// 扩展属性8
        /// </summary>	
        public virtual string EXT8
        {
            get { return m_EXT8; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT8", value, value.ToString());
                m_IsChanged |= (m_EXT8 != value); m_EXT8 = value;
            }
        }
        /// <summary>
        /// 扩展属性9
        /// </summary>	
        public virtual string EXT9
        {
            get { return m_EXT9; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT9", value, value.ToString());
                m_IsChanged |= (m_EXT9 != value); m_EXT9 = value;
            }
        }
        /// <summary>
        /// 扩展属性10
        /// </summary>	
        public virtual string EXT10
        {
            get { return m_EXT10; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for EXT10", value, value.ToString());
                m_IsChanged |= (m_EXT10 != value); m_EXT10 = value;
            }
        }
        /// <summary>
        /// 排序
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
            AGSDM_UIMENU castObj = (AGSDM_UIMENU)obj;
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

