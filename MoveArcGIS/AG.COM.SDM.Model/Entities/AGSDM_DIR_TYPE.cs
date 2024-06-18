using System;

namespace AG.COM.SDM.Model
{
	/// <summary>
	///	
	/// </summary>
	[Serializable]
	public class AGSDM_DIR_TYPE
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
        private bool m_IsAdd;
		private decimal m_ID; 
		private string m_ITEM_NAME; 
		private decimal m_ITEM_TYPE; 
		private byte[] m_ITEM_ICON; 
		private decimal m_DEPEND_ID; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
        public AGSDM_DIR_TYPE()
		{
			m_ID = 0; 
			m_ITEM_NAME = null; 
			m_ITEM_TYPE =  0;  
			m_ITEM_ICON = new byte[]{}; 
			m_DEPEND_ID = 0;
            this.m_IsAdd = false;
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 条目编码
		/// </summary>		
		public virtual decimal ID
		{
			get { return m_ID; }
			set { m_IsChanged |= (m_ID != value); m_ID = value; }
		}
			
		/// <summary>
		/// 条目名称
		/// </summary>		
		public virtual string ITEM_NAME
		{
			get { return m_ITEM_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for ITEM_NAME", value, value.ToString());
				
				m_IsChanged |= (m_ITEM_NAME != value); m_ITEM_NAME = value;
			}
		}
			
		/// <summary>
		/// 条目类型
		/// </summary>		
        public virtual decimal ITEM_TYPE
		{
			get { return m_ITEM_TYPE; }
			set { m_IsChanged |= (m_ITEM_TYPE != value); m_ITEM_TYPE = value; }
		}
			
		/// <summary>
		/// 条目图标
		/// </summary>		
        public virtual byte[] ITEM_ICON
		{
			get { return m_ITEM_ICON; }
			set { m_IsChanged |= (m_ITEM_ICON != value); m_ITEM_ICON = value; }
		}
			
		/// <summary>
		/// 隶属条目ID
		/// </summary>		
        public virtual decimal DEPEND_ID
		{
			get { return m_DEPEND_ID; }
			set { m_IsChanged |= (m_DEPEND_ID != value); m_DEPEND_ID = value; }
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

        /// <summary>
        /// 对象是否为新增状态
        /// </summary>
        public virtual bool IsAdd
        {
            get { return m_IsAdd; }
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

        /// <summary>
        /// 标识对象为新增状态
        /// </summary>
        public virtual void MarkAsAdd()
        {
            this.m_IsAdd = true;
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
            AGSDM_DIR_TYPE castObj = (AGSDM_DIR_TYPE)obj; 
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
