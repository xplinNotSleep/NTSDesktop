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
	public class AGSDM_METADATA
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ITEMID; 
		private string m_ITEMNAME; 
		private string m_ITEMALIASNAME; 
		private string m_NODETYPE; 
		private decimal? m_PARENTID; 
		private string m_FEATURETYPE; 
		private string m_GEOMETRYTYPE; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_METADATA()
		{
			m_ITEMID = 0; 
			m_ITEMNAME = null; 
			m_ITEMALIASNAME = null; 
			m_NODETYPE = null; 
			m_PARENTID =  null; 
			m_FEATURETYPE = null; 
			m_GEOMETRYTYPE = null; 
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal ITEMID
		{
			get { return m_ITEMID; }
			set { m_IsChanged |= (m_ITEMID != value); m_ITEMID = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string ITEMNAME
		{
			get { return m_ITEMNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for ITEMNAME", value, value.ToString());
				
				m_IsChanged |= (m_ITEMNAME != value); m_ITEMNAME = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string ITEMALIASNAME
		{
			get { return m_ITEMALIASNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for ITEMALIASNAME", value, value.ToString());
				
				m_IsChanged |= (m_ITEMALIASNAME != value); m_ITEMALIASNAME = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string NODETYPE
		{
			get { return m_NODETYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for NODETYPE", value, value.ToString());
				
				m_IsChanged |= (m_NODETYPE != value); m_NODETYPE = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal? PARENTID
		{
			get { return m_PARENTID; }
			set { m_IsChanged |= (m_PARENTID != value); m_PARENTID = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string FEATURETYPE
		{
			get { return m_FEATURETYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for FEATURETYPE", value, value.ToString());
				
				m_IsChanged |= (m_FEATURETYPE != value); m_FEATURETYPE = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual string GEOMETRYTYPE
		{
			get { return m_GEOMETRYTYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for GEOMETRYTYPE", value, value.ToString());
				
				m_IsChanged |= (m_GEOMETRYTYPE != value); m_GEOMETRYTYPE = value;
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
			AGSDM_METADATA castObj = (AGSDM_METADATA)obj; 
			return ( castObj != null ) &&
				(m_ITEMID == castObj.ITEMID );
		}
		
		/// <summary>
		/// 用唯一值实现GetHashCode
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * m_ITEMID.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
