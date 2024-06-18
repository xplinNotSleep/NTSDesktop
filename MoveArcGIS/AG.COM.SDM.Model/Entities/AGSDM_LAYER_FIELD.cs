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
	public class AGSDM_LAYER_FIELD
	{
		#region 私有成员

        private decimal? m_OWNERID;
        private string m_OBJECTTYPE;
        private decimal? m_CLASSID; 
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private decimal? m_PROJECT_LAYER_ID; 
		private string m_FIELD_NAME; 
		private string m_FIELD_NAME_CN; 
		private string m_FIELD_TYPE; 
		private string m_FIELD_SIZE; 
		private string m_VIEW_IN_RESULT; 
		private string m_VIEW_IN_TITLE; 
		private string m_EDITABLE; 
		private string m_IS_GROUP_FIELD; 
		private string m_VIEW_IN_BLURQUERY; 
		private decimal? m_SEQUENCE; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_LAYER_FIELD()
		{
			m_ID = 0;
            m_OWNERID = null;
            m_OBJECTTYPE = null;
            m_CLASSID = null; 
			m_PROJECT_LAYER_ID =  null; 
			m_FIELD_NAME = null; 
			m_FIELD_NAME_CN = null; 
			m_FIELD_TYPE = null; 
			m_FIELD_SIZE = null; 
			m_VIEW_IN_RESULT = null; 
			m_VIEW_IN_TITLE = null; 
			m_EDITABLE = null; 
			m_IS_GROUP_FIELD = null; 
			m_VIEW_IN_BLURQUERY = null; 
			m_SEQUENCE =  null; 
		}
		#endregion
		
		#region 公有属性
			
		/// <summary>
		/// 
		/// </summary>		
        public virtual decimal ID
		{
			get { return m_ID; }
			set { m_IsChanged |= (m_ID != value); m_ID = value; }
		}

        public virtual decimal? OWNERID
        {
            get { return m_OWNERID; }
            set { m_IsChanged |= (m_OWNERID != value); m_OWNERID = value; }
        }

        /// <summary>
        /// 对象类型
        /// </summary>
        public virtual string OBJECTTYPE
        {
            get { return m_OBJECTTYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for OBJECTTYPE", value, value.ToString());

                m_IsChanged |= (m_OBJECTTYPE != value); m_OBJECTTYPE = value;
            }
        }

        public virtual decimal? CLASSID
        {
            get { return m_CLASSID; }
            set { m_IsChanged |= (m_CLASSID != value); m_CLASSID = value; }
        }
			
		/// <summary>
		/// 工程图层ID
		/// </summary>		
        public virtual decimal? PROJECT_LAYER_ID
		{
			get { return m_PROJECT_LAYER_ID; }
			set { m_IsChanged |= (m_PROJECT_LAYER_ID != value); m_PROJECT_LAYER_ID = value; }
		}
			
		/// <summary>
		/// 字段名
		/// </summary>		
        public virtual string FIELD_NAME
		{
			get { return m_FIELD_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for FIELD_NAME", value, value.ToString());
				
				m_IsChanged |= (m_FIELD_NAME != value); m_FIELD_NAME = value;
			}
		}
			
		/// <summary>
		/// 字段中文名
		/// </summary>		
        public virtual string FIELD_NAME_CN
		{
			get { return m_FIELD_NAME_CN; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for FIELD_NAME_CN", value, value.ToString());
				
				m_IsChanged |= (m_FIELD_NAME_CN != value); m_FIELD_NAME_CN = value;
			}
		}
			
		/// <summary>
		/// 字段类型
		/// </summary>		
		public virtual string FIELD_TYPE
		{
			get { return m_FIELD_TYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for FIELD_TYPE", value, value.ToString());
				
				m_IsChanged |= (m_FIELD_TYPE != value); m_FIELD_TYPE = value;
			}
		}
			
		/// <summary>
		/// 字段大小
		/// </summary>		
        public virtual string FIELD_SIZE
		{
			get { return m_FIELD_SIZE; }
			set	
			{
				if ( value != null)
					if( value.Length > 10)
						throw new ArgumentOutOfRangeException("Invalid value for FIELD_SIZE", value, value.ToString());
				
				m_IsChanged |= (m_FIELD_SIZE != value); m_FIELD_SIZE = value;
			}
		}
			
		/// <summary>
		/// 查询结果中是否显示
		/// </summary>		
        public virtual string VIEW_IN_RESULT
		{
			get { return m_VIEW_IN_RESULT; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for VIEW_IN_RESULT", value, value.ToString());
				
				m_IsChanged |= (m_VIEW_IN_RESULT != value); m_VIEW_IN_RESULT = value;
			}
		}
			
		/// <summary>
		/// 快速查看中是否显示
		/// </summary>		
        public virtual string VIEW_IN_TITLE
		{
			get { return m_VIEW_IN_TITLE; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for VIEW_IN_TITLE", value, value.ToString());
				
				m_IsChanged |= (m_VIEW_IN_TITLE != value); m_VIEW_IN_TITLE = value;
			}
		}
			
		/// <summary>
		/// 是否可编辑
		/// </summary>		
        public virtual string EDITABLE
		{
			get { return m_EDITABLE; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for EDITABLE", value, value.ToString());
				
				m_IsChanged |= (m_EDITABLE != value); m_EDITABLE = value;
			}
		}
			
		/// <summary>
		/// 是否分组字段
		/// </summary>		
        public virtual string IS_GROUP_FIELD
		{
			get { return m_IS_GROUP_FIELD; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for IS_GROUP_FIELD", value, value.ToString());
				
				m_IsChanged |= (m_IS_GROUP_FIELD != value); m_IS_GROUP_FIELD = value;
			}
		}
			
		/// <summary>
		/// 模糊查询时是否显示
		/// </summary>		
        public virtual string VIEW_IN_BLURQUERY
		{
			get { return m_VIEW_IN_BLURQUERY; }
			set	
			{
				if ( value != null)
					if( value.Length > 1)
						throw new ArgumentOutOfRangeException("Invalid value for VIEW_IN_BLURQUERY", value, value.ToString());
				
				m_IsChanged |= (m_VIEW_IN_BLURQUERY != value); m_VIEW_IN_BLURQUERY = value;
			}
		}
			
		/// <summary>
		/// 字段的显示顺序
		/// </summary>		
        public virtual decimal? SEQUENCE
		{
			get { return m_SEQUENCE; }
			set { m_IsChanged |= (m_SEQUENCE != value); m_SEQUENCE = value; }
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
            AGSDM_LAYER_FIELD castObj = (AGSDM_LAYER_FIELD)obj; 
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
