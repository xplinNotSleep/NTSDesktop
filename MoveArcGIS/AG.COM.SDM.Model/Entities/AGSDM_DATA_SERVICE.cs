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
    public class AGSDM_DATA_SERVICE : IChange
    {
        #region 私有成员

        private bool m_IsChanged;
        private bool m_IsDeleted;
        private decimal m_ID;
        private string m_SERVICENAME;
        private string m_URL;
        private string m_UserName;
        private string m_Password;
        private string m_Description;
        private decimal m_ServerType;
        private string m_MapName;

        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_DATA_SERVICE()
        {
            m_ID = 0;
            m_ServerType = 0;
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
        /// 服务名称
        /// </summary>		
        public virtual string SERVICENAME
        {
            get { return m_SERVICENAME; }
            set { m_IsChanged |= (m_SERVICENAME != value); m_SERVICENAME = value; }
        }

        /// <summary>
        /// 服务地址
        /// </summary>		
        public virtual string URL
        {
            get { return m_URL; }
            set { m_IsChanged |= (m_URL != value); m_URL = value; }
        }

        /// <summary>
        /// 服务类型 1 Local 0 Internet
        /// </summary>
        public virtual decimal SERVERTYPE
        {
            get { return m_ServerType; }
            set { m_IsChanged |= (m_ServerType != value); m_ServerType = value; }
        }

        /// <summary>
        /// 地图名称
        /// </summary>
        public virtual string MAPNAME
        {
            get { return m_MapName; }
            set
            {
                m_IsChanged |= (m_MapName != value); m_MapName = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>		
        public virtual string USERNAME
        {
            get { return m_UserName; }
            set { m_IsChanged |= (m_UserName != value); m_UserName = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>		
        public virtual string PASSWORD
        {
            get { return m_Password; }
            set { m_IsChanged |= (m_Password != value); m_Password = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>		
        public virtual string DESCRIPTION
        {
            get { return m_Description; }
            set { m_IsChanged |= (m_Description != value); m_Description = value; }
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
            AGSDM_DATA_SERVICE castObj = (AGSDM_DATA_SERVICE)obj;
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
