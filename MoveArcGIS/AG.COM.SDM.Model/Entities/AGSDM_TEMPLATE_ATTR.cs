using System;

namespace AG.COM.SDM.Model
{
	/// <summary>
    ///	目录展示模板属性实体类,对应MicGIS数据库中的"MIC_TEMPLATE_ATTR"表
	/// </summary>
	[Serializable]
	public class AGSDM_TEMPLATE_ATTR
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_SORT_NO; 
		private decimal? m_ROOT; 
		private decimal? m_FIRST_CATALOG; 
		private string m_SECOND_CATALOG; 
		private decimal m_TEMPLATE_ID;
        private AGSDM_TEMPLATE m_Template;
        private decimal m_ID;
		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_TEMPLATE_ATTR()
		{
			m_SORT_NO = 0; 
			m_ROOT =  null; 
			m_FIRST_CATALOG =  null; 
			m_SECOND_CATALOG = null; 
			m_TEMPLATE_ID = 0; 
		}
		#endregion
		
		#region 公有属性

        /// <summary>
        /// 编号
        /// </summary>
        public virtual decimal ID
        {
            get { return m_ID; }
            set { m_IsChanged |= (m_ID != value); m_ID = value; }
        }
        /// <summary>
        /// 获取或设置模板目录展示模板对象
        /// </summary>
        public virtual AGSDM_TEMPLATE Mic_Template
        {
            get { return this.m_Template; }
            set { this.m_Template = value; }
        }

		/// <summary>
		/// 获取或设置顺序号
		/// </summary>		
        public virtual decimal SORT_NO
		{
			get { return m_SORT_NO; }
			set { m_IsChanged |= (m_SORT_NO != value); m_SORT_NO = value; }
		}
			
		/// <summary>
		/// 获取或设置根目录条目编号
		/// </summary>		
        public virtual decimal? ROOT
		{
			get { return m_ROOT; }
			set { m_IsChanged |= (m_ROOT != value); m_ROOT = value; }
		}
			
		/// <summary>
		/// 获取或设置可扩展条目编号
		/// </summary>		
        public virtual decimal? FIRST_CATALOG
		{
			get { return m_FIRST_CATALOG; }
			set { m_IsChanged |= (m_FIRST_CATALOG != value); m_FIRST_CATALOG = value; }
		}
			
		/// <summary>
		/// 获取或设置不可扩展条目编号
		/// </summary>		
        public virtual string SECOND_CATALOG
		{
			get { return m_SECOND_CATALOG; }
			set	
			{
				if ( value != null)
					if( value.Length > 1000)
						throw new ArgumentOutOfRangeException("Invalid value for SENDCOND_CATALOG", value, value.ToString());
				
				m_IsChanged |= (m_SECOND_CATALOG != value); m_SECOND_CATALOG = value;
			}
		}
			
		/// <summary>
		/// 获取或设置模板编号
		/// </summary>		
        public virtual decimal TEMPLATE_ID
		{
			get { return m_TEMPLATE_ID; }
			set { m_IsChanged |= (m_TEMPLATE_ID != value); m_TEMPLATE_ID = value; }
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
			AGSDM_TEMPLATE_ATTR castObj = (AGSDM_TEMPLATE_ATTR)obj; 
			return ( castObj != null ) &&
				(m_SORT_NO == castObj.SORT_NO ) &&
				(m_TEMPLATE_ID == castObj.TEMPLATE_ID );
		}
		
		/// <summary>
		/// 用唯一值实现GetHashCode
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 * hash * m_SORT_NO.GetHashCode();
			hash = 27 * hash * m_TEMPLATE_ID.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
