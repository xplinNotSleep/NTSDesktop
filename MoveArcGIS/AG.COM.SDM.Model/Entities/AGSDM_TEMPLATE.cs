using System;
using System.Collections.Generic;

namespace AG.COM.SDM.Model
{
	/// <summary>
    ///	目录展示模板实体类,对应"MicGIS"数据库中的"MIC_TEMPLATE"表
	/// </summary>
	[Serializable]
	public class AGSDM_TEMPLATE
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_TEMPLATE_NAME; 
		private string m_TEMPLATE_CLASS; 
		private string m_CREATOR; 
		private DateTime m_CREATE_TIME;
        private IList<AGSDM_TEMPLATE_ATTR> m_TemplateAttr;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_TEMPLATE()
		{
			m_ID = 0; 
			m_TEMPLATE_NAME = null; 
			m_TEMPLATE_CLASS = null; 
			m_CREATOR = null; 
			m_CREATE_TIME = DateTime.MinValue;
            m_TemplateAttr = new List<AGSDM_TEMPLATE_ATTR>();
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 获取或设置模板编号
		/// </summary>		
        public virtual decimal ID
		{
			get { return m_ID; }
			set { m_IsChanged |= (m_ID != value); m_ID = value; }
		}
			
		/// <summary>
		/// 获取或设置模板名称
		/// </summary>		
        public virtual string TEMPLATE_NAME
		{
			get { return m_TEMPLATE_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for TEMPLATE_NAME", value, value.ToString());
				
				m_IsChanged |= (m_TEMPLATE_NAME != value); m_TEMPLATE_NAME = value;
			}
		}
			
		/// <summary>
		/// 获取或设置模板分类
		/// </summary>		
        public virtual string TEMPLATE_CLASS
		{
			get { return m_TEMPLATE_CLASS; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for TEMPLATE_CLASS", value, value.ToString());
				
				m_IsChanged |= (m_TEMPLATE_CLASS != value); m_TEMPLATE_CLASS = value;
			}
		}
			
		/// <summary>
		/// 获取或设置创建人
		/// </summary>		
        public virtual string CREATOR
		{
			get { return m_CREATOR; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for CREATOR", value, value.ToString());
				
				m_IsChanged |= (m_CREATOR != value); m_CREATOR = value;
			}
		}
			
		/// <summary>
		/// 获取或设置创建时间
		/// </summary>		
        public virtual DateTime CREATE_TIME
		{
			get { return m_CREATE_TIME; }
			set { m_IsChanged |= (m_CREATE_TIME != value); m_CREATE_TIME = value; }
		}

        /// <summary>
        /// 获取或设置模板属性对象
        /// </summary>
        public virtual IList<AGSDM_TEMPLATE_ATTR> TemplateAttrs
        {
            get { return this.m_TemplateAttr; }
            set { this.m_TemplateAttr = value; }
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
			AGSDM_TEMPLATE castObj = (AGSDM_TEMPLATE)obj; 
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
