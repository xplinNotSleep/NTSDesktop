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
	public class AGSDM_LOGGER
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_OBJECTID; 
		private string m_LOGUSER; 
		private string m_HOSTNAME; 
		private string m_USERNAME; 
		private DateTime m_LOGTIME; 
		private string m_LOGMSG; 
		private string m_LOGTYPE; 
		private string m_LOGLEVEL; 
		private string m_PRODUCTNAME; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_LOGGER()
		{
			m_OBJECTID = 0; 
			m_LOGUSER = null; 
			m_HOSTNAME = null; 
			m_USERNAME = null; 
			m_LOGTIME = DateTime.MinValue; 
			m_LOGMSG = null; 
			m_LOGTYPE = null; 
			m_LOGLEVEL = null; 
			m_PRODUCTNAME = null; 
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual  decimal OBJECTID
		{
			get { return m_OBJECTID; }
			set { m_IsChanged |= (m_OBJECTID != value); m_OBJECTID = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string LOGUSER
		{
			get { return m_LOGUSER; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LOGUSER", value, value.ToString());
				
				m_IsChanged |= (m_LOGUSER != value); m_LOGUSER = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string HOSTNAME
		{
			get { return m_HOSTNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for HOSTNAME", value, value.ToString());
				
				m_IsChanged |= (m_HOSTNAME != value); m_HOSTNAME = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string USERNAME
		{
			get { return m_USERNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for USERNAME", value, value.ToString());
				
				m_IsChanged |= (m_USERNAME != value); m_USERNAME = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual DateTime LOGTIME
		{
			get { return m_LOGTIME; }
			set { m_IsChanged |= (m_LOGTIME != value); m_LOGTIME = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string LOGMSG
		{
			get { return m_LOGMSG; }
			set	
			{
				if ( value != null)
					if( value.Length > 2147483647)
						throw new ArgumentOutOfRangeException("Invalid value for LOGMSG", value, value.ToString());
				
				m_IsChanged |= (m_LOGMSG != value); m_LOGMSG = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string LOGTYPE
		{
			get { return m_LOGTYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for LOGTYPE", value, value.ToString());
				
				m_IsChanged |= (m_LOGTYPE != value); m_LOGTYPE = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string LOGLEVEL
		{
			get { return m_LOGLEVEL; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LOGLEVEL", value, value.ToString());
				
				m_IsChanged |= (m_LOGLEVEL != value); m_LOGLEVEL = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string PRODUCTNAME
		{
			get { return m_PRODUCTNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for PRODUCTNAME", value, value.ToString());
				
				m_IsChanged |= (m_PRODUCTNAME != value); m_PRODUCTNAME = value;
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
			AGSDM_LOGGER castObj = (AGSDM_LOGGER)obj; 
			return castObj.GetHashCode() == this.GetHashCode();
		}
		
		/// <summary>
		/// 用唯一值实现GetHashCode
		/// </summary>
		public override int GetHashCode()
		{
			return this.GetType().FullName.GetHashCode();
				
		}
		#endregion
		
	}
}
