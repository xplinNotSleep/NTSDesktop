using System;

namespace AG.COM.SDM.Model
{
	/// <summary>
	///	
	/// </summary>
	[Serializable]
	public sealed class AGSDM_ROLELAYERRLT
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private decimal? m_ROLEID; 
		private decimal? m_LAYERID; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
        public AGSDM_ROLELAYERRLT()
		{
			m_ID = 0; 
			m_ROLEID =  null; 
			m_LAYERID =  null; 
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
		public  decimal? ROLEID
		{
			get { return m_ROLEID; }
			set { m_IsChanged |= (m_ROLEID != value); m_ROLEID = value; }
		}
			
		/// <summary>
		/// AM_PROJECTLAYER表的ID
		/// </summary>		
		public  decimal? LAYERID
		{
			get { return m_LAYERID; }
			set { m_IsChanged |= (m_LAYERID != value); m_LAYERID = value; }
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
            AGSDM_ROLELAYERRLT castObj = (AGSDM_ROLELAYERRLT)obj; 
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
