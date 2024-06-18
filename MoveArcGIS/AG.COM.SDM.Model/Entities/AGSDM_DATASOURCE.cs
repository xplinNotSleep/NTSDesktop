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
	public class AGSDM_DATASOURCE
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_SOURCE_NAME; 
		private string m_IP; 
		private string m_USER_NAME; 
		private string m_PASSWORD; 
		private string m_SERVICE_NAME;
		private string m_DATABASE_NAME;
		private string m_REMARK;
        private string m_STATE;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
        public AGSDM_DATASOURCE()
		{
			m_ID = 0; 
			m_SOURCE_NAME = null; 
			m_IP = null; 
			m_USER_NAME = null; 
			m_PASSWORD = null; 
			m_SERVICE_NAME = null; 
			m_REMARK = null;
            m_STATE = "1";
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
		/// 数据源名称
		/// </summary>		
        public virtual string SOURCE_NAME
		{
			get { return m_SOURCE_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SOURCE_NAME", value, value.ToString());
				
				m_IsChanged |= (m_SOURCE_NAME != value); m_SOURCE_NAME = value;
			}
		}
			
		/// <summary>
		/// IP地址
		/// </summary>		
        public virtual string IP
		{
			get { return m_IP; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for IP", value, value.ToString());
				
				m_IsChanged |= (m_IP != value); m_IP = value;
			}
		}
			
		/// <summary>
		/// 用户名
		/// </summary>		
        public virtual string USER_NAME
		{
			get { return m_USER_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for USER_NAME", value, value.ToString());
				
				m_IsChanged |= (m_USER_NAME != value); m_USER_NAME = value;
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
		/// 服务名
		/// </summary>		
        public virtual string SERVICE_NAME
		{
			get { return m_SERVICE_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SERVICE_NAME", value, value.ToString());
				
				m_IsChanged |= (m_SERVICE_NAME != value); m_SERVICE_NAME = value;
			}
		}
		/// <summary>
		/// 数据库名
		/// </summary>		
		public virtual string DATABASE_NAME
		{
			get { return m_DATABASE_NAME; }
			set
			{
				if (value != null)
					if (value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for DATABASE_NAME", value, value.ToString());

				m_IsChanged |= (m_DATABASE_NAME != value); m_DATABASE_NAME = value;
			}
		}
		/// <summary>
		/// 备注
		/// </summary>		
		public virtual string REMARK
		{
			get { return m_REMARK; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for REMARK", value, value.ToString());
				
				m_IsChanged |= (m_REMARK != value); m_REMARK = value;
			}
		}

        /// <summary>
        /// 是否注销
        /// </summary>
        public virtual string STATE
        {
            get { return m_STATE; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 2)
                        throw new ArgumentOutOfRangeException("Invalid value for STATE", value, value.ToString());

                    m_IsChanged |= (m_STATE != value); m_STATE = value;
                }
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
            AGSDM_DATASOURCE castObj = (AGSDM_DATASOURCE)obj; 
			return ( castObj != null ) &&
				(m_ID == castObj.ID );
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
		
	}
}
