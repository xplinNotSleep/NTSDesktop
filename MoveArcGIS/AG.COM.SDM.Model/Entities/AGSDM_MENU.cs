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
	public class AGSDM_MENU
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_MENU_ID; 
		private string m_PARENT_MENU_ID; 
		private string m_MENU_CODE; 
		private string m_MENU_NAME; 
		private string m_ASSEMBLY_NAME; 
		private string m_TYPE_NAME; 
		private string m_SHORTCUT; 
		private string m_ISBEGINGROUP; 
		private decimal? m_MENU_TYPE; 
		private decimal? m_MENU_LEVEL;
        private decimal m_SORTID;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_MENU()
		{
			m_ID = 0; 
			m_MENU_ID = null; 
			m_PARENT_MENU_ID = null; 
			m_MENU_CODE = null; 
			m_MENU_NAME = null; 
			m_ASSEMBLY_NAME = null; 
			m_TYPE_NAME = null; 
			m_SHORTCUT = null; 
			m_ISBEGINGROUP = null; 
			m_MENU_TYPE =  null; 
			m_MENU_LEVEL =  null;
            m_SORTID = 0;
		}

		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 主键ID
		/// </summary>		
        public virtual decimal ID
		{
			get { return m_ID; }
			set { m_IsChanged |= (m_ID != value); m_ID = value; }
		}
			
		/// <summary>
		/// 菜单ID
		/// </summary>		
        public virtual string MENU_ID
		{
			get { return m_MENU_ID; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for MENU_ID", value, value.ToString());
				
				m_IsChanged |= (m_MENU_ID != value); m_MENU_ID = value;
			}
		}
			
		/// <summary>
		/// 上级菜单ID
		/// </summary>		
        public virtual string PARENT_MENU_ID
		{
			get { return m_PARENT_MENU_ID; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for PARENT_MENU_ID", value, value.ToString());
				
				m_IsChanged |= (m_PARENT_MENU_ID != value); m_PARENT_MENU_ID = value;
			}
		}
			
		/// <summary>
		/// 菜单代码
		/// </summary>		
		public virtual string MENU_CODE
		{
			get { return m_MENU_CODE; }
			set	
			{
				if ( value != null)
					if( value.Length > 30)
						throw new ArgumentOutOfRangeException("Invalid value for MENU_CODE", value, value.ToString());
				
				m_IsChanged |= (m_MENU_CODE != value); m_MENU_CODE = value;
			}
		}
			
		/// <summary>
		/// 菜单名称
		/// </summary>		
        public virtual string MENU_NAME
		{
			get { return m_MENU_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for MENU_NAME", value, value.ToString());
				
				m_IsChanged |= (m_MENU_NAME != value); m_MENU_NAME = value;
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
		/// 快捷键
		/// </summary>		
        public virtual string SHORTCUT
		{
			get { return m_SHORTCUT; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SHORTCUT", value, value.ToString());
				
				m_IsChanged |= (m_SHORTCUT != value); m_SHORTCUT = value;
			}
		}
			
		/// <summary>
		/// 是否开始分组
		/// </summary>		
        public virtual string ISBEGINGROUP
		{
			get { return m_ISBEGINGROUP; }
			set	
			{
				if ( value != null)
					if( value.Length > 5)
						throw new ArgumentOutOfRangeException("Invalid value for ISBEGINGROUP", value, value.ToString());
				
				m_IsChanged |= (m_ISBEGINGROUP != value); m_ISBEGINGROUP = value;
			}
		}
			
		/// <summary>
		/// 菜单类型
		/// </summary>		
        public virtual decimal? MENU_TYPE
		{
			get { return m_MENU_TYPE; }
			set { m_IsChanged |= (m_MENU_TYPE != value); m_MENU_TYPE = value; }
		}
			
		/// <summary>
		/// 菜单级别
		/// </summary>		
        public virtual decimal? MENU_LEVEL
		{
			get { return m_MENU_LEVEL; }
			set { m_IsChanged |= (m_MENU_LEVEL != value); m_MENU_LEVEL = value; }
		}

        /// <summary>
        /// 排序
        /// </summary>
        public virtual decimal SORTID
        {
            get { return m_SORTID; }
            set { m_IsChanged |= (m_SORTID != value); m_SORTID = value; }
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
            AGSDM_MENU castObj = (AGSDM_MENU)obj; 
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
