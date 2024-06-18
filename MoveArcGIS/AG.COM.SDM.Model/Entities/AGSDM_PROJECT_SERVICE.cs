/*
/*NHibernate映射代码模板
/*作者：DDL
/*版本更新和支持：http://renrenqq.cnblogs.com/
/*日期：2006年8月24日 
*/
using AG.COM.SDM.DAL;
using System;

namespace AG.COM.SDM.Model
{
    /// <summary>
    ///	
    /// </summary>
    [Serializable]
    public class AGSDM_PROJECT_SERVICE:IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;
        private decimal m_ID;
        private decimal m_ProjectID;
        private decimal m_SERVICE_ID;
     

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_PROJECT_SERVICE()
        {
            m_ID = 0;
            m_ProjectID = -1;
            m_SERVICE_ID = -1;
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
        /// 工程编号
        /// </summary>		
        public virtual decimal PROJECT_ID
        {
            get { return m_ProjectID; }
            set { m_IsChanged |= (m_ProjectID != value); m_ProjectID = value; }
        }

        /// <summary>
        /// 服务编号
        /// </summary>		
        public virtual decimal SERVICE_ID
        {
            get { return m_SERVICE_ID; }
            set { m_IsChanged |= (m_SERVICE_ID != value); m_SERVICE_ID = value; }
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
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            AGSDM_PROJECT_SERVICE castObj = (AGSDM_PROJECT_SERVICE)obj;
            return (castObj != null) &&
                (m_ID == castObj.ID);
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


        #region IChange 成员

        public void MakeAsDefault()
        {
            m_IsChanged = false;
        }

        #endregion
    }
}
