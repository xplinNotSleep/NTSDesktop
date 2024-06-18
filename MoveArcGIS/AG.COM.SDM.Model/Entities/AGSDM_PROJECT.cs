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
	public class AGSDM_PROJECT
	{
		#region 私有成员

        private string m_OBJECTTYPE;
        private decimal? m_CLASSID; 
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private string m_PROJECT_NAME; 
		private string m_STATE; 
		private string m_CREATE_TIME; 
		private string m_CREATOR; 
		private decimal? m_OWNERID; 
        private decimal m_Rotation;
        private decimal m_ReferenceScale;
        private string m_MapUnit;
        private string m_DisplayUnit;
		private string m_REMARK;
        private string m_FullExtend;
        private string m_LabelEngine;
        private string m_CLIPGEOMETRY;

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_PROJECT()
		{
			m_ID = 0;
            m_OBJECTTYPE = null;
            m_CLASSID = null; 
			m_PROJECT_NAME = null; 
			m_STATE = null; 
			m_CREATE_TIME = null; 
			m_CREATOR = null; 
			m_OWNERID =  null; 
			m_REMARK = null;
            m_FullExtend = null;
            m_LabelEngine = null;
            m_CLIPGEOMETRY = null;
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
		/// 工程名称
		/// </summary>		
        public virtual string PROJECT_NAME
		{
			get { return m_PROJECT_NAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for PROJECT_NAME", value, value.ToString());
				
				m_IsChanged |= (m_PROJECT_NAME != value); m_PROJECT_NAME = value;
			}
		}
			
		/// <summary>
		/// 状态
		/// </summary>		
        public virtual string STATE
		{
			get { return m_STATE; }
			set	
			{
				if ( value != null)
					if( value.Length > 10)
						throw new ArgumentOutOfRangeException("Invalid value for STATE", value, value.ToString());
				
				m_IsChanged |= (m_STATE != value); m_STATE = value;
			}
		}
			
		/// <summary>
		/// 创建时间
		/// </summary>		
        public virtual string CREATE_TIME
		{
			get { return m_CREATE_TIME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for CREATE_TIME", value, value.ToString());
				
				m_IsChanged |= (m_CREATE_TIME != value); m_CREATE_TIME = value;
			}
		}
			
		/// <summary>
		/// 创建人
		/// </summary>		
        public virtual string CREATOR
		{
			get { return m_CREATOR; }
			set	
			{
				if ( value != null)
					if( value.Length > 20)
						throw new ArgumentOutOfRangeException("Invalid value for CREATOR", value, value.ToString());
				
				m_IsChanged |= (m_CREATOR != value); m_CREATOR = value;
			}
		}
			
		/// <summary>
		/// 所属部门
		/// </summary>		
        public virtual decimal? OWNERID
		{
			get { return m_OWNERID; }
			set { m_IsChanged |= (m_OWNERID != value); m_OWNERID = value; }
		}

        /// <summary>
        /// 相关比例尺
        /// </summary>		
        public virtual decimal REFERENCESCALE
        {
            get { return m_ReferenceScale; }
            set { m_IsChanged |= (m_ReferenceScale != value); m_ReferenceScale = value; }
        }

        /// <summary>
        /// 旋转角度
        /// </summary>		
        public virtual decimal ROTATION
        {
            get { return m_Rotation; }
            set { m_IsChanged |= (m_Rotation != value); m_Rotation = value; }
        }

        /// <summary>
        /// 显示单位
        /// </summary>
        public virtual string DISPLAYUNIT
        {
            get { return m_DisplayUnit; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for CREATOR", value, value.ToString());

                m_IsChanged |= (m_DisplayUnit != value); m_DisplayUnit = value;
            }
        }

        /// <summary>
        /// 显示单位
        /// </summary>
        public virtual string MAPUNIT
        {
            get { return m_MapUnit; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for CREATOR", value, value.ToString());

                m_IsChanged |= (m_MapUnit != value); m_MapUnit = value;
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
					if( value.Length > 40)
						throw new ArgumentOutOfRangeException("Invalid value for REMARK", value, value.ToString());
				
				m_IsChanged |= (m_REMARK != value); m_REMARK = value;
			}
		}

        /// <summary>
        /// 全屏范围
        /// </summary>
        public virtual string FULLEXTEND
        {
            get { return m_FullExtend; }
            set 
            {
                m_IsChanged |= (m_FullExtend != value); m_FullExtend = value;
            }
        }

        /// <summary>
        /// 标注引擎
        /// </summary>		
        public virtual string LABELENGINE
        {
            get { return m_LabelEngine; }
            set
            {
                if (value != null)
                    if (value.Length > 20)
                        throw new ArgumentOutOfRangeException("Invalid value for LABELENGINE", value, value.ToString());

                m_IsChanged |= (m_LabelEngine != value); m_LabelEngine = value;
            }
        }

        /// <summary>
        /// 地图显示范围
        /// </summary>
        public virtual string CLIPGEOMETRY
        {
            get { return m_CLIPGEOMETRY; }
            set
            {
                m_IsChanged |= (m_CLIPGEOMETRY != value); m_CLIPGEOMETRY = value;
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
            AGSDM_PROJECT castObj = (AGSDM_PROJECT)obj; 
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
