/*
/*NHibernate映射代码模板
/*作者：DDL
/*版本更新和支持：http://renrenqq.cnblogs.com/
/*日期：2006年8月24日 
*/
using System;

namespace AG.COM.SDM.Model
{
	/// <summary>
	///	
	/// </summary>
	[Serializable]
	public class AGSDM_ORG
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ORG_ID; 
		private decimal? m_ID; 
		private string m_ORG_CODE; 
		private string m_ORG_NAME; 
		private string m_ORG_TYPE; 
		private decimal? m_ORG_LEVEL; 
		private string m_ORG_GRADE;
        private decimal m_PARENT_ORG_ID; 
		private string m_ORG_SEQ; 
		private string m_ORG_ADDR; 
		private string m_ZIP_CODE; 
		private string m_LINK_MAN; 
		private string m_LINK_TEL; 
		private string m_EMAIL; 
		private string m_WEB_URL; 
		private string m_STATUS; 
		private string m_AREA; 
		private string m_DESCRIPTION;
        private decimal m_PRINCIPALOR;
        private decimal m_OPERATOR;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
        public AGSDM_ORG()
		{
			m_ORG_ID = 0; 
			m_ID =  null; 
			m_ORG_CODE = null; 
			m_ORG_NAME = null; 
			m_ORG_TYPE = null; 
			m_ORG_LEVEL =  null; 
			m_ORG_GRADE = null; 
			m_PARENT_ORG_ID = -1; 
			m_ORG_SEQ = null; 
			m_ORG_ADDR = null; 
			m_ZIP_CODE = null; 
			m_LINK_MAN = null; 
			m_LINK_TEL = null; 
			m_EMAIL = null; 
			m_WEB_URL = null; 
			m_STATUS = null; 
			m_AREA = null; 
			m_DESCRIPTION = null;
            m_PRINCIPALOR = -1;
            m_OPERATOR = -1;
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// ORG_ID
		/// </summary>		
        public virtual decimal ORG_ID
		{
			get { return m_ORG_ID; }
			set { m_IsChanged |= (m_ORG_ID != value); m_ORG_ID = value; }
		}
			
		/// <summary>
		/// ID
		/// </summary>		
        public virtual decimal? ID
		{
			get { return m_ID; }
			set { m_IsChanged |= (m_ID != value); m_ID = value; }
		}
			
		/// <summary>
		/// 机构代码
		/// </summary>		
        public virtual string ORG_CODE
		{
			get { return m_ORG_CODE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for ORG_CODE", value, value.ToString());
				
				m_IsChanged |= (m_ORG_CODE != value); m_ORG_CODE = value;
			}
		}
			
		/// <summary>
		/// 机构名称
		/// </summary>		
        public virtual string ORG_NAME
		{
			get { return m_ORG_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for ORG_NAME", value, value.ToString());
				
				m_IsChanged |= (m_ORG_NAME != value); m_ORG_NAME = value;
			}
		}
			
		/// <summary>
		/// 机构类型
		/// </summary>		
        public virtual string ORG_TYPE
		{
			get { return m_ORG_TYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for ORG_TYPE", value, value.ToString());
				
				m_IsChanged |= (m_ORG_TYPE != value); m_ORG_TYPE = value;
			}
		}
			
		/// <summary>
		/// 机构级别
		/// </summary>		
        public virtual decimal? ORG_LEVEL
		{
			get { return m_ORG_LEVEL; }
			set { m_IsChanged |= (m_ORG_LEVEL != value); m_ORG_LEVEL = value; }
		}
			
		/// <summary>
		/// ORG_GRADE
		/// </summary>		
        public virtual string ORG_GRADE
		{
			get { return m_ORG_GRADE; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for ORG_GRADE", value, value.ToString());
				
				m_IsChanged |= (m_ORG_GRADE != value); m_ORG_GRADE = value;
			}
		}			

        public virtual decimal PARENT_ORG_ID
        {
            get { return m_PARENT_ORG_ID; }
            set { m_IsChanged |= (m_ID != value); m_PARENT_ORG_ID = value; }
        }
			
		/// <summary>
		/// 机构顺序
		/// </summary>		
        public virtual string ORG_SEQ
		{
			get { return m_ORG_SEQ; }
			set	
			{
				if ( value != null)
					if( value.Length > 500)
						throw new ArgumentOutOfRangeException("Invalid value for ORG_SEQ", value, value.ToString());
				
				m_IsChanged |= (m_ORG_SEQ != value); m_ORG_SEQ = value;
			}
		}
			
		/// <summary>
		/// 机构地址
		/// </summary>		
        public virtual string ORG_ADDR
		{
			get { return m_ORG_ADDR; }
			set	
			{
				if ( value != null)
					if( value.Length > 250)
						throw new ArgumentOutOfRangeException("Invalid value for ORG_ADDR", value, value.ToString());
				
				m_IsChanged |= (m_ORG_ADDR != value); m_ORG_ADDR = value;
			}
		}
			
		/// <summary>
		/// 邮政编码
		/// </summary>		
        public virtual string ZIP_CODE
		{
			get { return m_ZIP_CODE; }
			set	
			{
				if ( value != null)
					if( value.Length > 10)
						throw new ArgumentOutOfRangeException("Invalid value for ZIP_CODE", value, value.ToString());
				
				m_IsChanged |= (m_ZIP_CODE != value); m_ZIP_CODE = value;
			}
		}
			
		/// <summary>
		/// 联系人名称
		/// </summary>		
        public virtual string LINK_MAN
		{
			get { return m_LINK_MAN; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LINK_MAN", value, value.ToString());
				
				m_IsChanged |= (m_LINK_MAN != value); m_LINK_MAN = value;
			}
		}
			
		/// <summary>
		/// 联系人电话
		/// </summary>		
        public virtual string LINK_TEL
		{
			get { return m_LINK_TEL; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LINK_TEL", value, value.ToString());
				
				m_IsChanged |= (m_LINK_TEL != value); m_LINK_TEL = value;
			}
		}
			
		/// <summary>
		/// 联系人邮箱
		/// </summary>		
        public virtual string EMAIL
		{
			get { return m_EMAIL; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for EMAIL", value, value.ToString());
				
				m_IsChanged |= (m_EMAIL != value); m_EMAIL = value;
			}
		}
			
		/// <summary>
		/// 机构网址
		/// </summary>		
        public virtual string WEB_URL
		{
			get { return m_WEB_URL; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for WEB_URL", value, value.ToString());
				
				m_IsChanged |= (m_WEB_URL != value); m_WEB_URL = value;
			}
		}
			
		/// <summary>
		/// 激活状态
		/// </summary>		
        public virtual string STATUS
		{
			get { return m_STATUS; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for STATUS", value, value.ToString());
				
				m_IsChanged |= (m_STATUS != value); m_STATUS = value;
			}
		}
			
		/// <summary>
		/// AREA
		/// </summary>		
        public virtual string AREA
		{
			get { return m_AREA; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for AREA", value, value.ToString());
				
				m_IsChanged |= (m_AREA != value); m_AREA = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string DESCRIPTION
		{
			get { return m_DESCRIPTION; }
			set	
			{
				if ( value != null)
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for DESCRIPTION", value, value.ToString());
				
				m_IsChanged |= (m_DESCRIPTION != value); m_DESCRIPTION = value;
			}
		}

        /// <summary>
        /// 部门负责人岗位编号
        /// </summary>
        public virtual decimal PRINCIPALOR
        {
            get { return m_PRINCIPALOR; }
            set { m_PRINCIPALOR = value; }
        }

        /// <summary>
        /// 部门权限操作人编号
        /// </summary>
        public virtual decimal OPERATOR
        {
            get { return m_OPERATOR; }
            set { m_OPERATOR = value; }
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
		public override bool Equals( object obj )
		{
			if( this == obj ) return true;
			if( ( obj == null ) || ( obj.GetType() != GetType() ) ) return false;
            AGSDM_ORG castObj = (AGSDM_ORG)obj; 
			return ( castObj != null ) &&
				(m_ORG_ID == castObj.ORG_ID );
		}
		
		/// <summary>
		/// 用唯一值实现GetHashCode
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * m_ORG_ID.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
