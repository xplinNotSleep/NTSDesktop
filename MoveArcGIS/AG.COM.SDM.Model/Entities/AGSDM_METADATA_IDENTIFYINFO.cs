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
	public sealed class AGSDM_METADATA_IDENTIFYINFO
	{
		#region 私有成员
			
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private decimal m_ID; 
		private decimal? m_DATASETID; 
		private string m_SUBJECTNAME; 
		private string m_MC; 
		private DateTime m_RQ; 
		private string m_BB; 
		private string m_YZ; 
		private string m_ZY; 
		private string m_XZ; 
		private string m_FL; 
		private decimal? m_XBJD; 
		private decimal? m_DBJD; 
		private decimal? m_NBWD; 
		private decimal? m_BBWD; 
		private string m_DLBSF; 
		private DateTime m_ZZSJ; 
		private string m_BSFS; 
		private string m_KJFBL; 
		private string m_LB; 
		private string m_FZDWMC; 
		private string m_LXR; 
		private string m_DH; 
		private string m_CZ; 
		private string m_TXDZ; 
		private string m_YZBM; 
		private string m_DZXXDZ; 
		private string m_WZ; 
		private string m_WJMC; 
		private string m_SYXZDM; 
		private string m_AQDJDM; 
		private string m_GSMC; 
		private string m_GSBB; 
		private string m_GLRJMC; 		

		#endregion
		
		#region 默认( 空 ) 构造函数
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public AGSDM_METADATA_IDENTIFYINFO()
		{
			m_ID = 0; 
			m_DATASETID =  null; 
			m_SUBJECTNAME = null; 
			m_MC = null; 
			m_RQ = DateTime.MinValue; 
			m_BB = null; 
			m_YZ = null; 
			m_ZY = null; 
			m_XZ = null; 
			m_FL = null; 
			m_XBJD =  null; 
			m_DBJD =  null; 
			m_NBWD =  null; 
			m_BBWD =  null; 
			m_DLBSF = null; 
			m_ZZSJ = DateTime.MinValue; 
			m_BSFS = null; 
			m_KJFBL = null; 
			m_LB = null; 
			m_FZDWMC = null; 
			m_LXR = null; 
			m_DH = null; 
			m_CZ = null; 
			m_TXDZ = null; 
			m_YZBM = null; 
			m_DZXXDZ = null; 
			m_WZ = null; 
			m_WJMC = null; 
			m_SYXZDM = null; 
			m_AQDJDM = null; 
			m_GSMC = null; 
			m_GSBB = null; 
			m_GLRJMC = null; 
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
		public  decimal? DATASETID
		{
			get { return m_DATASETID; }
			set { m_IsChanged |= (m_DATASETID != value); m_DATASETID = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string SUBJECTNAME
		{
			get { return m_SUBJECTNAME; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SUBJECTNAME", value, value.ToString());
				
				m_IsChanged |= (m_SUBJECTNAME != value); m_SUBJECTNAME = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string MC
		{
			get { return m_MC; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for MC", value, value.ToString());
				
				m_IsChanged |= (m_MC != value); m_MC = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  DateTime RQ
		{
			get { return m_RQ; }
			set { m_IsChanged |= (m_RQ != value); m_RQ = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string BB
		{
			get { return m_BB; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for BB", value, value.ToString());
				
				m_IsChanged |= (m_BB != value); m_BB = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string YZ
		{
			get { return m_YZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for YZ", value, value.ToString());
				
				m_IsChanged |= (m_YZ != value); m_YZ = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string ZY
		{
			get { return m_ZY; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for ZY", value, value.ToString());
				
				m_IsChanged |= (m_ZY != value); m_ZY = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string XZ
		{
			get { return m_XZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for XZ", value, value.ToString());
				
				m_IsChanged |= (m_XZ != value); m_XZ = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string FL
		{
			get { return m_FL; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for FL", value, value.ToString());
				
				m_IsChanged |= (m_FL != value); m_FL = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  decimal? XBJD
		{
			get { return m_XBJD; }
			set { m_IsChanged |= (m_XBJD != value); m_XBJD = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  decimal? DBJD
		{
			get { return m_DBJD; }
			set { m_IsChanged |= (m_DBJD != value); m_DBJD = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  decimal? NBWD
		{
			get { return m_NBWD; }
			set { m_IsChanged |= (m_NBWD != value); m_NBWD = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  decimal? BBWD
		{
			get { return m_BBWD; }
			set { m_IsChanged |= (m_BBWD != value); m_BBWD = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string DLBSF
		{
			get { return m_DLBSF; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for DLBSF", value, value.ToString());
				
				m_IsChanged |= (m_DLBSF != value); m_DLBSF = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  DateTime ZZSJ
		{
			get { return m_ZZSJ; }
			set { m_IsChanged |= (m_ZZSJ != value); m_ZZSJ = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string BSFS
		{
			get { return m_BSFS; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for BSFS", value, value.ToString());
				
				m_IsChanged |= (m_BSFS != value); m_BSFS = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string KJFBL
		{
			get { return m_KJFBL; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for KJFBL", value, value.ToString());
				
				m_IsChanged |= (m_KJFBL != value); m_KJFBL = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string LB
		{
			get { return m_LB; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LB", value, value.ToString());
				
				m_IsChanged |= (m_LB != value); m_LB = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string FZDWMC
		{
			get { return m_FZDWMC; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for FZDWMC", value, value.ToString());
				
				m_IsChanged |= (m_FZDWMC != value); m_FZDWMC = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string LXR
		{
			get { return m_LXR; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for LXR", value, value.ToString());
				
				m_IsChanged |= (m_LXR != value); m_LXR = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string DH
		{
			get { return m_DH; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for DH", value, value.ToString());
				
				m_IsChanged |= (m_DH != value); m_DH = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string CZ
		{
			get { return m_CZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for CZ", value, value.ToString());
				
				m_IsChanged |= (m_CZ != value); m_CZ = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string TXDZ
		{
			get { return m_TXDZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for TXDZ", value, value.ToString());
				
				m_IsChanged |= (m_TXDZ != value); m_TXDZ = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string YZBM
		{
			get { return m_YZBM; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for YZBM", value, value.ToString());
				
				m_IsChanged |= (m_YZBM != value); m_YZBM = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string DZXXDZ
		{
			get { return m_DZXXDZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for DZXXDZ", value, value.ToString());
				
				m_IsChanged |= (m_DZXXDZ != value); m_DZXXDZ = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string WZ
		{
			get { return m_WZ; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for WZ", value, value.ToString());
				
				m_IsChanged |= (m_WZ != value); m_WZ = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string WJMC
		{
			get { return m_WJMC; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for WJMC", value, value.ToString());
				
				m_IsChanged |= (m_WJMC != value); m_WJMC = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string SYXZDM
		{
			get { return m_SYXZDM; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for SYXZDM", value, value.ToString());
				
				m_IsChanged |= (m_SYXZDM != value); m_SYXZDM = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string AQDJDM
		{
			get { return m_AQDJDM; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for AQDJDM", value, value.ToString());
				
				m_IsChanged |= (m_AQDJDM != value); m_AQDJDM = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string GSMC
		{
			get { return m_GSMC; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for GSMC", value, value.ToString());
				
				m_IsChanged |= (m_GSMC != value); m_GSMC = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string GSBB
		{
			get { return m_GSBB; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for GSBB", value, value.ToString());
				
				m_IsChanged |= (m_GSBB != value); m_GSBB = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public  string GLRJMC
		{
			get { return m_GLRJMC; }
			set	
			{
				if ( value != null)
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for GLRJMC", value, value.ToString());
				
				m_IsChanged |= (m_GLRJMC != value); m_GLRJMC = value;
			}
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
			AGSDM_METADATA_IDENTIFYINFO castObj = (AGSDM_METADATA_IDENTIFYINFO)obj; 
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
