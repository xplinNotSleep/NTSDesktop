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
	public class AGSDM_STARTITEM
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_START_NAME; 
		private string m_FILE_NAME; 
		private string m_ASSEMBLY_NAME; 
		private string m_TYPE_NAME; 
		private string m_DESCRIPTION; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_STARTITEM()
		{
			m_ID = 0; 
			m_START_NAME = null; 
			m_FILE_NAME = null; 
			m_ASSEMBLY_NAME = null; 
			m_TYPE_NAME = null; 
			m_DESCRIPTION = null; 
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
		/// 启动项名称
		/// </summary>		
        public virtual string START_NAME
		{
			get { return m_START_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for START_NAME", value, value.ToString());
				
				m_IsChanged |= (m_START_NAME != value); m_START_NAME = value;
			}
		}
			
		/// <summary>
		/// 文件名称
		/// </summary>		
        public virtual string FILE_NAME
		{
			get { return m_FILE_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for FILE_NAME", value, value.ToString());
				
				m_IsChanged |= (m_FILE_NAME != value); m_FILE_NAME = value;
			}
		}
			
		/// <summary>
		/// 程序集名称
		/// </summary>		
        public virtual string ASSEMBLY_NAME
		{
			get { return m_ASSEMBLY_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for ASSEMBLY_NAME", value, value.ToString());
				
				m_IsChanged |= (m_ASSEMBLY_NAME != value); m_ASSEMBLY_NAME = value;
			}
		}
			
		/// <summary>
		/// 类型名称
		/// </summary>		
        public virtual string TYPE_NAME
		{
			get { return m_TYPE_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 200)
						throw new ArgumentOutOfRangeException("Invalid value for TYPE_NAME", value, value.ToString());
				
				m_IsChanged |= (m_TYPE_NAME != value); m_TYPE_NAME = value;
			}
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
            AGSDM_STARTITEM castObj = (AGSDM_STARTITEM)obj; 
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
