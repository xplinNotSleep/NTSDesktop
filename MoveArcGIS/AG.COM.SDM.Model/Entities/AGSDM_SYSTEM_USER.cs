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
	public class AGSDM_SYSTEM_USER
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_USER_ID; 
		private string m_USER_CODE; 
		private string m_NAME_CN; 
		private string m_NAME_EN; 
		private string m_PASSWORD;
		private decimal m_PASSWORDTYPE;
		private string m_SEX; 
		private string m_TEL; 
		private string m_MOBILE; 
		private string m_FAX; 
		private string m_EMAIL; 
		private string m_POSITION; 
		private DateTime m_CTEATE_TIME; 
		private string m_DESCRIPTION; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_SYSTEM_USER()
		{
			m_USER_ID = 0; 
			m_USER_CODE = null; 
			m_NAME_CN = null; 
			m_NAME_EN = null; 
			m_PASSWORD = null;
			m_PASSWORDTYPE = 0;
			m_SEX = null; 
			m_TEL = null; 
			m_MOBILE = null; 
			m_FAX = null; 
			m_EMAIL = null; 
			m_POSITION = null; 
			m_CTEATE_TIME = DateTime.MinValue; 
			m_DESCRIPTION = null; 
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 主建ID
		/// </summary>		
        public virtual decimal USER_ID
		{
			get { return m_USER_ID; }
			set { m_IsChanged |= (m_USER_ID != value); m_USER_ID = value; }
		}
			
		/// <summary>
		/// 用户编号
		/// </summary>		
        public virtual string USER_CODE
		{
			get { return m_USER_CODE; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for USER_CODE", value, value.ToString());
				
				m_IsChanged |= (m_USER_CODE != value); m_USER_CODE = value;
			}
		}
			
		/// <summary>
		/// 中文名
		/// </summary>		
        public virtual string NAME_CN
		{
			get { return m_NAME_CN; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NAME_CN", value, value.ToString());
				
				m_IsChanged |= (m_NAME_CN != value); m_NAME_CN = value;
			}
		}
			
		/// <summary>
		/// 英文名
		/// </summary>		
        public virtual string NAME_EN
		{
			get { return m_NAME_EN; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NAME_EN", value, value.ToString());
				
				m_IsChanged |= (m_NAME_EN != value); m_NAME_EN = value;
			}
		}
			
		/// <summary>
		/// 密码
		/// </summary>		
        public virtual string PASSWORD
		{
			get { return m_PASSWORD; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for PASSWORD", value, value.ToString());
				
				m_IsChanged |= (m_PASSWORD != value); m_PASSWORD = value;
			}
		}
		/// <summary>
		/// 密码类型
		/// </summary>
		public virtual decimal PASSWORDTYPE
        {
			get { return m_PASSWORDTYPE; }
			set { m_IsChanged |= (m_PASSWORDTYPE != value); m_PASSWORDTYPE = value; }
		}
		/// <summary>
		/// 性别
		/// </summary>		
		public virtual string SEX
		{
			get { return m_SEX; }
			set	
			{
				if ( value != null)
					if( value.Length > 10)
						throw new ArgumentOutOfRangeException("Invalid value for SEX", value, value.ToString());
				
				m_IsChanged |= (m_SEX != value); m_SEX = value;
			}
		}
			
		/// <summary>
		/// 办公电话
		/// </summary>		
        public virtual string TEL
		{
			get { return m_TEL; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for TEL", value, value.ToString());
				
				m_IsChanged |= (m_TEL != value); m_TEL = value;
			}
		}
			
		/// <summary>
		/// 移动电话
		/// </summary>		
        public virtual string MOBILE
		{
			get { return m_MOBILE; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for MOBILE", value, value.ToString());
				
				m_IsChanged |= (m_MOBILE != value); m_MOBILE = value;
			}
		}
			
		/// <summary>
		/// 传真
		/// </summary>		
        public virtual string FAX
		{
			get { return m_FAX; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for FAX", value, value.ToString());
				
				m_IsChanged |= (m_FAX != value); m_FAX = value;
			}
		}
			
		/// <summary>
		/// 邮箱
		/// </summary>		
        public virtual string EMAIL
		{
			get { return m_EMAIL; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for EMAIL", value, value.ToString());
				
				m_IsChanged |= (m_EMAIL != value); m_EMAIL = value;
			}
		}
			
		/// <summary>
		/// 职务
		/// </summary>		
        public virtual string POSITION
		{
			get { return m_POSITION; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for POSITION", value, value.ToString());
				
				m_IsChanged |= (m_POSITION != value); m_POSITION = value;
			}
		}
			
		/// <summary>
		/// 创建时间
		/// </summary>		
        public virtual DateTime CTEATE_TIME
		{
			get { return m_CTEATE_TIME; }
			set { m_IsChanged |= (m_CTEATE_TIME != value); m_CTEATE_TIME = value; }
		}
			
		/// <summary>
		/// 描述信息
		/// </summary>		
        public virtual string DESCRIPTION
		{
			get { return m_DESCRIPTION; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for DESCRIPTION", value, value.ToString());
				
				m_IsChanged |= (m_DESCRIPTION != value); m_DESCRIPTION = value;
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
		public override bool Equals( object obj )
		{
			if( this == obj ) return true;
			if( ( obj == null ) || ( obj.GetType() != GetType() ) ) return false;
            AGSDM_SYSTEM_USER castObj = (AGSDM_SYSTEM_USER)obj; 
			return ( castObj != null ) &&
				(m_USER_ID == castObj.USER_ID );
		}
		
		/// <summary>
		/// 用唯一值实现GetHashCode
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * m_USER_ID.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
