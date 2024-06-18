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
	public class AGSDM_LAYER_STRUCTURE
	{
		#region 私有成员

        private decimal? m_OWNERID;
        private string m_OBJECTTYPE;
        private decimal? m_CLASSID;
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_NAME; 
		private decimal? m_PRANTID; 
		private decimal? m_LAYERID;
        private decimal? M_PROJECTID;
        private decimal? m_Project_Layer_ID;
        private string m_FEATURE_TYPE; 
		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_LAYER_STRUCTURE()
		{
			m_ID = 0;
            m_OWNERID = null;
            m_OBJECTTYPE = null;
            m_CLASSID = null;
			m_NAME = null; 
			m_PRANTID =  null; 
			m_LAYERID =  null;
            M_PROJECTID = null;
            m_FEATURE_TYPE = null;
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
		/// 节点名称
		/// </summary>		
        public virtual string NAME
		{
			get { return m_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 100)
						throw new ArgumentOutOfRangeException("Invalid value for NAME", value, value.ToString());
				
				m_IsChanged |= (m_NAME != value); m_NAME = value;
			}
		}
			
		/// <summary>
		/// 父级节点名称
		/// </summary>		
        public virtual decimal? PARENTID
		{
			get { return m_PRANTID; }
			set { m_IsChanged |= (m_PRANTID != value); m_PRANTID = value; }
		}
			
		/// <summary>
		/// 图层ID
		/// </summary>		
        public virtual decimal? LAYERID
		{
			get { return m_LAYERID; }
			set { m_IsChanged |= (m_LAYERID != value); m_LAYERID = value; }
		}

        /// <summary>
        /// 图层ID
        /// </summary>		
        public virtual decimal? PROJECTID
        {
            get { return M_PROJECTID; }
            set { m_IsChanged |= (M_PROJECTID != value); M_PROJECTID = value; }
        }

        /// <summary>
        /// 工程图层编号
        /// </summary>
        public virtual decimal? PROJECT_LAYER_ID
        {
            get { return m_Project_Layer_ID; }
            set { m_IsChanged |= (m_Project_Layer_ID != value); m_Project_Layer_ID = value; }
        }

        public virtual string FEATURE_TYPE
        {
            get { return m_FEATURE_TYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for FEATURE_TYPE", value, value.ToString());

                m_IsChanged |= (m_FEATURE_TYPE != value); m_FEATURE_TYPE = value;
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
			AGSDM_LAYER_STRUCTURE castObj = (AGSDM_LAYER_STRUCTURE)obj; 
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
