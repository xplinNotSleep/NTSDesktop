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
	public class AGSDM_DATASET
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_DATASET_NAME; 
		private string m_DATASET_NAME_CN; 
		private string m_COORDINATE; 
		private string m_PROJECT_TYPE; 
		private decimal? m_SEQUENCE; 
		private string m_SCALE; 
		private string m_YEAR; 
		private decimal? m_DATASOURCE_ID; 
		private string m_DATASET_TYPE; 
		private string m_REMARK;
        private string m_STATE;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
        public AGSDM_DATASET()
		{
			m_ID = 0; 
			m_DATASET_NAME = null; 
			m_DATASET_NAME_CN = null; 
			m_COORDINATE = null; 
			m_PROJECT_TYPE = null; 
			m_SEQUENCE =  null; 
			m_SCALE = null; 
			m_YEAR = null; 
			m_DATASOURCE_ID =  null; 
			m_DATASET_TYPE = null; 
			m_REMARK = null;
            m_STATE = "1";
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
		/// 数据集名称
		/// </summary>		
        public virtual string DATASET_NAME
		{
			get { return m_DATASET_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for DATASET_NAME", value, value.ToString());
				
				m_IsChanged |= (m_DATASET_NAME != value); m_DATASET_NAME = value;
			}
		}
			
		/// <summary>
		/// 数据集中文名
		/// </summary>		
        public virtual string DATASET_NAME_CN
		{
			get { return m_DATASET_NAME_CN; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for DATASET_NAME_CN", value, value.ToString());
				
				m_IsChanged |= (m_DATASET_NAME_CN != value); m_DATASET_NAME_CN = value;
			}
		}
			
		/// <summary>
		/// 数据集坐标名称
		/// </summary>		
        public virtual string COORDINATE
		{
			get { return m_COORDINATE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for COORDINATE", value, value.ToString());
				
				m_IsChanged |= (m_COORDINATE != value); m_COORDINATE = value;
			}
		}
			
		/// <summary>
		/// 坐标系类型
		/// </summary>		
        public virtual string PROJECT_TYPE
		{
			get { return m_PROJECT_TYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for PROJECT_TYPE", value, value.ToString());
				
				m_IsChanged |= (m_PROJECT_TYPE != value); m_PROJECT_TYPE = value;
			}
		}
			
		/// <summary>
		/// 数据集顺序
		/// </summary>		
		public virtual decimal? SEQUENCE
		{
			get { return m_SEQUENCE; }
			set { m_IsChanged |= (m_SEQUENCE != value); m_SEQUENCE = value; }
		}
			
		/// <summary>
		/// 数据集比例尺
		/// </summary>		
        public virtual string SCALE
		{
			get { return m_SCALE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SCALE", value, value.ToString());
				
				m_IsChanged |= (m_SCALE != value); m_SCALE = value;
			}
		}

        /// <summary>
        /// 是否注销
        /// </summary>
        public virtual string STATE
        {
            get { return m_STATE; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 2)
                        throw new ArgumentOutOfRangeException("Invalid value for STATE", value, value.ToString());

                    m_IsChanged |= (m_STATE != value); m_STATE = value;
                }
            }
        }

		/// <summary>
		/// 数据年份
		/// </summary>		
        public virtual string YEAR
		{
			get { return m_YEAR; }
			set	
			{
				if ( value != null)
					if( value.Length > 10)
						throw new ArgumentOutOfRangeException("Invalid value for YEAR", value, value.ToString());
				
				m_IsChanged |= (m_YEAR != value); m_YEAR = value;
			}
		}
			
		/// <summary>
		/// 数据源ID
		/// </summary>		
        public virtual decimal? DATASOURCE_ID
		{
			get { return m_DATASOURCE_ID; }
			set { m_IsChanged |= (m_DATASOURCE_ID != value); m_DATASOURCE_ID = value; }
		}
			
		/// <summary>
		/// 数据集类型
		/// </summary>		
		public virtual string DATASET_TYPE
		{
			get { return m_DATASET_TYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for DATASET_TYPE", value, value.ToString());
				
				m_IsChanged |= (m_DATASET_TYPE != value); m_DATASET_TYPE = value;
			}
		}
			
		/// <summary>
		/// 备注
		/// </summary>		
        public virtual string REMARK
		{
			get { return m_REMARK; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for REMARK", value, value.ToString());
				
				m_IsChanged |= (m_REMARK != value); m_REMARK = value;
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
            AGSDM_DATASET castObj = (AGSDM_DATASET)obj; 
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
