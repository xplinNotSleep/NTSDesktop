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
	public class AGSDM_ROLE
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ROLE_ID; 
		private string m_ROLE_CODE; 
		private string m_ROLE_NAME; 
		private string m_ISVALID; 
		private string m_ISAUTHORIZATION; 
		private decimal? m_PARENT_ROLE_ID; 
		private string m_DESCRIPTION;
        private string m_VISIBLEEXTENT;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_ROLE()
		{
			m_ROLE_ID = 0; 
			m_ROLE_CODE = null; 
			m_ROLE_NAME = null; 
			m_ISVALID = null; 
			m_ISAUTHORIZATION = null; 
			m_PARENT_ROLE_ID =  null; 
			m_DESCRIPTION = null;
            m_VISIBLEEXTENT = null;
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 主键ID
		/// </summary>		
        public virtual decimal ROLE_ID
		{
			get { return m_ROLE_ID; }
			set { m_IsChanged |= (m_ROLE_ID != value); m_ROLE_ID = value; }
		}
			
		/// <summary>
		/// 角色编码
		/// </summary>		
        public virtual string ROLE_CODE
		{
			get { return m_ROLE_CODE; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for ROLE_CODE", value, value.ToString());
				
				m_IsChanged |= (m_ROLE_CODE != value); m_ROLE_CODE = value;
			}
		}
			
		/// <summary>
		/// 角色名称
		/// </summary>		
        public virtual string ROLE_NAME
		{
			get { return m_ROLE_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for ROLE_NAME", value, value.ToString());
				
				m_IsChanged |= (m_ROLE_NAME != value); m_ROLE_NAME = value;
			}
		}
			
		/// <summary>
		/// 是否激活
		/// </summary>		
        public virtual string ISVALID
		{
			get { return m_ISVALID; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for ISVALID", value, value.ToString());
				
				m_IsChanged |= (m_ISVALID != value); m_ISVALID = value;
			}
		}
			
		/// <summary>
		/// 是否可分级授权
		/// </summary>		
        public virtual string ISAUTHORIZATION
		{
			get { return m_ISAUTHORIZATION; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for ISAUTHORIZATION", value, value.ToString());
				
				m_IsChanged |= (m_ISAUTHORIZATION != value); m_ISAUTHORIZATION = value;
			}
		}
			
		/// <summary>
		/// 上一级角色编码
		/// </summary>		
        public virtual decimal? PARENT_ROLE_ID
		{
			get { return m_PARENT_ROLE_ID; }
			set { m_IsChanged |= (m_PARENT_ROLE_ID != value); m_PARENT_ROLE_ID = value; }
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
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for DESCRIPTION", value, value.ToString());
				
				m_IsChanged |= (m_DESCRIPTION != value); m_DESCRIPTION = value;
			}
		}

        /// <summary>
        /// 地图显示范围
        /// </summary>
        public virtual string VISIBLEEXTENT
        {
            get { return m_VISIBLEEXTENT; }
            set
            {
                m_IsChanged |= (m_VISIBLEEXTENT != value); m_VISIBLEEXTENT = value;
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
            AGSDM_ROLE castObj = (AGSDM_ROLE)obj; 
			return ( castObj != null ) &&
				(m_ROLE_ID == castObj.ROLE_ID );
		}
		
		/// <summary>
		/// 用唯一值实现GetHashCode
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * m_ROLE_ID.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
