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
	public sealed class AGSDM_METADATA_QUALITYINFO
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private decimal? m_DATASETID; 
		private string m_SUBJECTNAME; 
		private string m_SJZLMS; 
		private string m_SJZ; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_METADATA_QUALITYINFO()
		{
			m_ID = 0; 
			m_DATASETID =  null; 
			m_SUBJECTNAME = null; 
			m_SJZLMS = null; 
			m_SJZ = null; 
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 
		/// </summary>		
		public  decimal ID
		{
			get { return m_ID; }
			set { m_IsChanged |= (m_ID != value); m_ID = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  decimal? DATASETID
		{
			get { return m_DATASETID; }
			set { m_IsChanged |= (m_DATASETID != value); m_DATASETID = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string SUBJECTNAME
		{
			get { return m_SUBJECTNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SUBJECTNAME", value, value.ToString());
				
				m_IsChanged |= (m_SUBJECTNAME != value); m_SUBJECTNAME = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string SJZLMS
		{
			get { return m_SJZLMS; }
			set	
			{
				if ( value != null)
					if( value.Length > 2000)
						throw new ArgumentOutOfRangeException("Invalid value for SJZLMS", value, value.ToString());
				
				m_IsChanged |= (m_SJZLMS != value); m_SJZLMS = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string SJZ
		{
			get { return m_SJZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 2000)
						throw new ArgumentOutOfRangeException("Invalid value for SJZ", value, value.ToString());
				
				m_IsChanged |= (m_SJZ != value); m_SJZ = value;
			}
		}
			
		/// <summary>
		/// 对象的值是否被改变
		/// </summary>
		public bool IsChanged
		{
			get { return m_IsChanged; }
		}
		
		/// <summary>
		/// 对象是否已经被删除
		/// </summary>
		public bool IsDeleted
		{
			get { return m_IsDeleted; }
		}
		
		#endregion 
		
		#region 公有函数
		
		/// <summary>
		/// 标记对象已删除
		/// </summary>
		public void MarkAsDeleted()
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
			AGSDM_METADATA_QUALITYINFO castObj = (AGSDM_METADATA_QUALITYINFO)obj; 
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
