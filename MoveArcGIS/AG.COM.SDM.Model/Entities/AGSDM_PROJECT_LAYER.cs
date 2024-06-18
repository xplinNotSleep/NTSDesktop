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
    public class AGSDM_PROJECT_LAYER
    {
        #region 私有成员

        private decimal? m_OWNERID;
        private string m_OBJECTTYPE;
        private decimal? m_CLASSID;
        private bool m_IsChanged;
        private bool m_IsDeleted;
        private decimal m_ID;
        private decimal? m_PROJECT_ID;
        private decimal? m_LAYER_ID;
        private string m_ALIAS;
        private decimal? m_SEQUENCE;
        private decimal? m_MIN_SCALE;
        private decimal? m_MAX_SCALE;
        private decimal? m_MIN_LABEL_SCALE;
        private decimal? m_MAX_LABEL_SCALE;
        private string m_OPERATION;
        private string m_SELECTEABLE;
        private string m_DEFAULT_VISIBLE;
        private string m_SHOW_LABEL;
        private string m_HIGH_LABEL;
        private string m_ANNO_FIELD;
        private string m_ISEXPAND;
        private string m_LABEL_STYLE;
        private string m_LAYER_STYLE;
        private string m_REMARK;
        private string m_GROUPFIELD;
        private string m_DISPLAYFIELD;
        private string m_TABLENAME;
        private string m_LAYERTYPE;
        private string m_DEFINITIONQUERY;
        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AGSDM_PROJECT_LAYER()
        {
            m_ID = 0;
            m_OWNERID = null;
            m_OBJECTTYPE = null;
            m_CLASSID = null;
            m_PROJECT_ID = null;
            m_LAYER_ID = null;
            m_ALIAS = null;
            m_SEQUENCE = null;
            m_MIN_SCALE = 0;
            m_MAX_SCALE = 0;
            m_MIN_LABEL_SCALE = 0;
            m_MAX_LABEL_SCALE = 0;
            m_OPERATION = "1";
            m_SELECTEABLE = "1";
            m_DEFAULT_VISIBLE = "1";
            m_SHOW_LABEL = "1";
            m_HIGH_LABEL = "1";
            m_ISEXPAND = "1";
            m_ANNO_FIELD = null;
            m_REMARK = null;
            m_GROUPFIELD = null;
            m_DISPLAYFIELD = null;
            m_TABLENAME = null;
            m_LAYERTYPE = null;
            m_DEFINITIONQUERY = null;
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
        /// 地图工程ID
        /// </summary>		
        public virtual decimal? PROJECT_ID
        {
            get { return m_PROJECT_ID; }
            set { m_IsChanged |= (m_PROJECT_ID != value); m_PROJECT_ID = value; }
        }

        /// <summary>
        /// 图层ID
        /// </summary>		
        public virtual decimal? LAYER_ID
        {
            get { return m_LAYER_ID; }
            set { m_IsChanged |= (m_LAYER_ID != value); m_LAYER_ID = value; }
        }

        /// <summary>
        /// 别名
        /// </summary>		
        public virtual string ALIAS
        {
            get { return m_ALIAS; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for ALIAS", value, value.ToString());

                m_IsChanged |= (m_ALIAS != value); m_ALIAS = value;
            }
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
        /// 最大显示比例尺
        /// </summary>		
        public virtual decimal? MIN_SCALE
        {
            get { return m_MIN_SCALE; }
            set { m_IsChanged |= (m_MIN_SCALE != value); m_MIN_SCALE = value; }
        }

        /// <summary>
        /// 最小显示比例尺
        /// </summary>		
        public virtual decimal? MAX_SCALE
        {
            get { return m_MAX_SCALE; }
            set { m_IsChanged |= (m_MAX_SCALE != value); m_MAX_SCALE = value; }
        }

        /// <summary>
        /// 最大标注比例尺
        /// </summary>		
        public virtual decimal? MIN_LABEL_SCALE
        {
            get { return m_MIN_LABEL_SCALE; }
            set { m_IsChanged |= (m_MIN_LABEL_SCALE != value); m_MIN_LABEL_SCALE = value; }
        }

        /// <summary>
        /// 最小标注比例尺
        /// </summary>		
        public virtual decimal? MAX_LABEL_SCALE
        {
            get { return m_MAX_LABEL_SCALE; }
            set { m_IsChanged |= (m_MAX_LABEL_SCALE != value); m_MAX_LABEL_SCALE = value; }
        }

        /// <summary>
        /// 是否可编辑
        /// </summary>		
        public virtual string OPERATION
        {
            get { return m_OPERATION; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for OPERATION", value, value.ToString());

                m_IsChanged |= (m_OPERATION != value); m_OPERATION = value;
            }
        }

        /// <summary>
        /// 是否可选
        /// </summary>		
        public virtual string SELECTEABLE
        {
            get { return m_SELECTEABLE; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for SELECTEABLE", value, value.ToString());

                m_IsChanged |= (m_SELECTEABLE != value); m_SELECTEABLE = value;
            }
        }

        /// <summary>
        /// 是否默认显示
        /// </summary>		
        public virtual string DEFAULT_VISIBLE
        {
            get { return m_DEFAULT_VISIBLE; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for DEFAULT_VISIBLE", value, value.ToString());

                m_IsChanged |= (m_DEFAULT_VISIBLE != value); m_DEFAULT_VISIBLE = value;
            }
        }

        /// <summary>
        /// 是否显示标注
        /// </summary>		
        public virtual string SHOW_LABEL
        {
            get { return m_SHOW_LABEL; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for SHOW_LABEL", value, value.ToString());

                m_IsChanged |= (m_SHOW_LABEL != value); m_SHOW_LABEL = value;
            }
        }

        /// <summary>
        /// 是否高级显示注记
        /// </summary>		
        public virtual string HIGH_LABEL
        {
            get { return m_HIGH_LABEL; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for HIGH_LABEL", value, value.ToString());

                m_IsChanged |= (m_HIGH_LABEL != value); m_HIGH_LABEL = value;
            }
        }

        /// <summary>
        /// 是否展开
        /// </summary>
        public virtual string ISEXPAND
        {
            get { return m_ISEXPAND; }
            set
            {
                if (value != null)
                    if (value.Length > 1)
                        throw new ArgumentOutOfRangeException("Invalid value for ISEXPAND", value, value.ToString());

                m_IsChanged |= (m_ISEXPAND != value); m_ISEXPAND = value;
            }
        }

        /// <summary>
        /// 标注字段
        /// </summary>		
        public virtual string ANNO_FIELD
        {
            get { return m_ANNO_FIELD; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for ANNO_FIELD", value, value.ToString());

                m_IsChanged |= (m_ANNO_FIELD != value); m_ANNO_FIELD = value;
            }
        }

        /// <summary>
        /// 标注样式
        /// </summary>		
        public virtual string LABEL_STYLE
        {
            get { return m_LABEL_STYLE; }
            set { m_IsChanged |= (m_LABEL_STYLE != value); m_LABEL_STYLE = value; }
        }

        /// <summary>
        /// 图层样式
        /// </summary>		
        public virtual string LAYER_STYLE
        {
            get { return m_LAYER_STYLE; }
            set { m_IsChanged |= (m_LAYER_STYLE != value); m_LAYER_STYLE = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>		
        public virtual string REMARK
        {
            get { return m_REMARK; }
            set
            {
                if (value != null)
                    if (value.Length > 400)
                        throw new ArgumentOutOfRangeException("Invalid value for REMARK", value, value.ToString());

                m_IsChanged |= (m_REMARK != value); m_REMARK = value;
            }
        }

        public virtual string GROUPFIELD
        {
            get { return m_GROUPFIELD; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for GROUPFIELD", value, value.ToString());
                m_IsChanged |= (m_GROUPFIELD != value); m_GROUPFIELD = value;
            }
        }

        /// <summary>
        /// 主要显示字段
        /// </summary>		
        public virtual string DISPLAYFIELD
        {
            get { return m_DISPLAYFIELD; }
            set
            {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for DISPLAYFIELD", value, value.ToString());

                m_IsChanged |= (m_DISPLAYFIELD != value); m_DISPLAYFIELD = value;
            }
        }

        /// <summary>
        /// 表名
        /// </summary>		
        public virtual string TABLENAME
        {
            get { return m_TABLENAME; }
            set
            {
                if (value != null)
                    if (value.Length > 100)
                        throw new ArgumentOutOfRangeException("Invalid value for TABLENAME", value, value.ToString());

                m_IsChanged |= (m_TABLENAME != value); m_TABLENAME = value;
            }
        }

        /// <summary>
        /// 图层类型 0=featureclass，1=RasterDataset，2=RasterCatalog，3=MapService  
        /// </summary>		
        public virtual string LAYERTYPE
        {
            get { return m_LAYERTYPE; }
            set
            {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for LAYERTYPE", value, value.ToString());

                m_IsChanged |= (m_LAYERTYPE != value); m_LAYERTYPE = value;
            }
        }

        /// <summary>
        /// 属性过滤条件
        /// </summary>
        public virtual string DEFINITIONQUERY
        {
            get { return m_DEFINITIONQUERY; }
            set
            {
                m_IsChanged |= (m_DEFINITIONQUERY != value); m_DEFINITIONQUERY = value;
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
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            AGSDM_PROJECT_LAYER castObj = (AGSDM_PROJECT_LAYER)obj;
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

    }
}
