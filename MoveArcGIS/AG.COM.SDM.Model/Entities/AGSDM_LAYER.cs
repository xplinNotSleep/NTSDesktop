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
	public class AGSDM_LAYER
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID;
        private decimal? m_DATASET_ID;
        private decimal? m_DATASOURCEID; 
		private decimal? m_SEQUENCE; 
		private string m_LAYER_NAME; 
		private string m_LAYER_TABLE; 
		private string m_LAYER_PATH; 
		private string m_FEATURE_TYPE; 
		private string m_REMARK;
        private string m_STATE;
		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_LAYER()
		{
			m_ID = 0;
            m_DATASET_ID = null;
            m_DATASOURCEID = null; 
			m_SEQUENCE =  null; 
			m_LAYER_NAME = null; 
			m_LAYER_TABLE = null; 
			m_LAYER_PATH = null; 
			m_FEATURE_TYPE = null; 
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
		/// 数据集ID
		/// </summary>		
        public virtual decimal? DATASET_ID
		{
			get { return m_DATASET_ID; }
			set { m_IsChanged |= (m_DATASET_ID != value); m_DATASET_ID = value; }
		}

        /// <summary>
        /// 数据源ID
        /// </summary>		
        public virtual decimal? DATASOURCEID
        {
            get { return m_DATASOURCEID; }
            set { m_IsChanged |= (m_DATASOURCEID != value); m_DATASOURCEID = value; }
        }
			
		/// <summary>
		/// 图层顺序
		/// </summary>		
        public virtual decimal? SEQUENCE
		{
			get { return m_SEQUENCE; }
			set { m_IsChanged |= (m_SEQUENCE != value); m_SEQUENCE = value; }
		}
			
		/// <summary>
		/// 图层名称
		/// </summary>		
        public virtual string LAYER_NAME
		{
			get { return m_LAYER_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for LAYER_NAME", value, value.ToString());
				
				m_IsChanged |= (m_LAYER_NAME != value); m_LAYER_NAME = value;
			}
		}
			
		/// <summary>
		/// 图层表名
		/// </summary>		
        public virtual string LAYER_TABLE
		{
			get { return m_LAYER_TABLE; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for LAYER_TABLE", value, value.ToString());
				
				m_IsChanged |= (m_LAYER_TABLE != value); m_LAYER_TABLE = value;
			}
		}
			
		/// <summary>
		/// 图层路径
		/// </summary>		
        public virtual string LAYER_PATH
		{
			get { return m_LAYER_PATH; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for LAYER_PATH", value, value.ToString());
				
				m_IsChanged |= (m_LAYER_PATH != value); m_LAYER_PATH = value;
			}
		}
			
		/// <summary>
		/// 图层类型
		/// </summary>		
        public virtual string FEATURE_TYPE
		{
			get { return m_FEATURE_TYPE; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for FEATURE_TYPE", value, value.ToString());
				
				m_IsChanged |= (m_FEATURE_TYPE != value); m_FEATURE_TYPE = value;
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
					if( value.Length > 400)
						throw new ArgumentOutOfRangeException("Invalid value for REMARK", value, value.ToString());
				
				m_IsChanged |= (m_REMARK != value); m_REMARK = value;
			}
		}

        /// <summary>
        /// 是否注销 1表示正常使用， 0表示注销
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
            AGSDM_LAYER castObj = (AGSDM_LAYER)obj; 
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
