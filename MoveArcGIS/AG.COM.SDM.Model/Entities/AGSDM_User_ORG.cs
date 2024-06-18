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
	public class AGSDM_USER_ORG
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID;
        private string m_USER_ID;
        private string m_ORG_ID; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_USER_ORG()
		{
			m_ID = 0; 
			m_USER_ID =  null; 
			m_ORG_ID =  null; 
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
		/// 用户ID
		/// </summary>		
        public virtual string USER_ID
		{
			get { return m_USER_ID; }
			set { m_IsChanged |= (m_USER_ID != value); m_USER_ID = value; }
		}
			
		/// <summary>
		/// 部门ID
		/// </summary>		
        public virtual string ORG_ID
		{
			get { return m_ORG_ID; }
			set { m_IsChanged |= (m_ORG_ID != value); m_ORG_ID = value; }
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
			AGSDM_USER_ORG castObj = (AGSDM_USER_ORG)obj; 
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
