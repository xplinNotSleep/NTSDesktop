using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using System;
using AG.COM.SDM.Config;
using AG.COM.SDM.Database;
using System.Collections;
using System.Resources;
using System.Globalization;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 公共变量类
    /// </summary>
    public class CommonVariables
    {
        #region 私有字段
        //系统变量是否已初始化
        private static bool m_HasInitialized = false;
        #endregion 

        #region 静态公有属性
        /// <summary>
        /// 保存系统全局变量配置的文件
        /// </summary>
        public static readonly string ConfigFile = CommonConstString.STR_ConfigPath + "\\appconfig.resx";

        /// <summary>
        /// 系统数据库配置信息类
        /// </summary>
        public static SpatialDBConfig SpatialdbConn = SpatialDBConfig.GetInstance();

        /// <summary>
        /// 系统数据库配置信息类
        /// </summary>
        public static OleDBConfig OledbConn = OleDBConfig.GetInstance();

        /// <summary>
        /// 系统数据库配置信息类
        /// </summary>
        public static MinioServerConfig MinioConn = MinioServerConfig.GetInstance();

        /// <summary>
        /// 通用的出错提示信息头部分
        /// </summary>
        public static readonly string ErroMsgHead = "操作失败。原因是：";

        /// <summary>
        /// UIDesign加载来源
        /// </summary>
        public static UIDesignFrom UIDesignLoadFrom = UIDesignFrom.XmlFile;

        /// <summary>
        /// 当前皮肤名称
        /// </summary>
        public static string CurrentSkinName = "";

        public static bool IsClosed = false;

        /// <summary>
        /// 符号样式文件
        /// </summary>
        public static string StyleFiles;

        #endregion

        /// <summary>
        /// 系统变量初始化
        /// </summary>
        public static void Init()
        {
            Init(false);
        }

        /// <summary>
        /// 系统变量初始化
        /// </summary>
        /// <param name="ForceInit">是否强制更新</param>
        public static void Init(bool ForceInit)
        {
            //若是强制更新则把已更新标识设为false
            if (ForceInit == true) m_HasInitialized = false;

            //如果已经初始化,则返回
            if (m_HasInitialized) return;

            //实例化资源文件帮助类
            ResourceHelper helper = new ResourceHelper(ConfigFile,
                CommonConstString.STR_TempPath);

            Hashtable newHash = helper.KeyValues;
            //SetHashByCommon(newHash);

            //Spatial连接设置
            SpatialdbConn.Spatial_DBType = TryToParse(helper, CommonVariablesKeys.SpatialDbType);
            SpatialdbConn.Spatial_Server = helper.GetString(CommonVariablesKeys.SpatialServer);
            SpatialdbConn.Spatial_Instance = helper.GetString(CommonVariablesKeys.SpatialInstance);
            SpatialdbConn.Spatial_DataBase = helper.GetString(CommonVariablesKeys.SpatialDatabase);
            SpatialdbConn.Spatial_User = helper.GetString(CommonVariablesKeys.SpatialUser);
            SpatialdbConn.Spatial_Password = helper.GetString(CommonVariablesKeys.SpatialPassword);
            SpatialdbConn.Spatial_Port = helper.GetString(CommonVariablesKeys.SpatialPort);

            //OLE连接设置
            OledbConn.DatabaseType = TryToParse(helper, CommonVariablesKeys.OLEDbType);
            OledbConn.OLE_Server = helper.GetString(CommonVariablesKeys.OLEServer);
            OledbConn.OLE_Port = helper.GetString(CommonVariablesKeys.OLEPort);
            OledbConn.OLE_DataBase = helper.GetString(CommonVariablesKeys.OLEDataBase);
            OledbConn.OLE_User = helper.GetString(CommonVariablesKeys.OLEUser);
            OledbConn.OLE_Password = helper.GetString(CommonVariablesKeys.OLEPassword);

            #region AGSDM BS数据库连接设置
            //OleConnProperty sysBSOleConn = SpatialDbConn.OLE_ConnManager.GetOleConn(CommonConstString.STR_AGSDMBSOleName);
            //if (sysBSOleConn != null)
            //{
            //    SpatialDbConn.BS_DBType = sysBSOleConn.DataBaseType;
            //    SpatialDbConn.BS_Server = sysBSOleConn.OLE_Server;
            //    SpatialDbConn.BS_DataBase = sysBSOleConn.OLE_DataBase;
            //    SpatialDbConn.BS_Password = sysBSOleConn.OLE_Password;
            //    SpatialDbConn.BS_Port = sysBSOleConn.OLE_Port;
            //    SpatialDbConn.BS_User = sysBSOleConn.OLE_User;
            //}
            #endregion

            //Minio服务器设置
            MinioConn.MinioServerURL = helper.GetString(CommonVariablesKeys.MinioServerURL);
            MinioConn.MinioAccessName = helper.GetString(CommonVariablesKeys.MinioServerAccess);
            MinioConn.MinioPassWord = helper.GetString(CommonVariablesKeys.MinioServerPassWord);
            //增加提交的桶名称
            MinioConn.MinioServerBucket = helper.GetString(CommonVariablesKeys.MinioServerBucket);

            //UIDesign加载来源
            object objUIDesignLoadFrom = helper.GetObject(CommonVariablesKeys.UIDesignLoadFrom);
            if (objUIDesignLoadFrom is UIDesignFrom)
                UIDesignLoadFrom = (UIDesignFrom)objUIDesignLoadFrom;

            //默认皮肤名称
            CurrentSkinName = helper.GetString(CommonConstString.SkinKeyInConfig);

            #region 设置样式符号文件名
            //StyleFiles = helper.GetString(CommonVariablesKeys.StyleFiles);
            //if (StyleFiles.Length == 0)
            //{
            //    //符号样式文件   
            //    DirectoryInfo dirInfo = new DirectoryInfo(CommonConstString.STR_StylePath);
            //    //--刘飞 2020/12/31 --修改，当路径不存在时直接获取文件会报错
            //    if (dirInfo.Exists)
            //    {
            //        FileInfo[] fileInfos = dirInfo.GetFiles();
            //        StringBuilder sb = new StringBuilder();
            //        foreach (FileInfo fileInfo in fileInfos)
            //        {
            //            sb.Append(fileInfo.FullName + "|");
            //        }

            //        StyleFiles = sb.ToString();
            //    }
            //}
            #endregion

            m_HasInitialized = true;
        }

        private static DatabaseType TryToParse(ResourceHelper helper, string DbType)
        {
            DatabaseType type = DatabaseType.Oracle;
            string strSpatialDbType = helper.GetString(DbType); ;
            if (!Enum.TryParse(strSpatialDbType, out DatabaseType dbType))
            {
                type = DatabaseType.Oracle;
            }
            else
            {
                type = (DatabaseType)Enum.Parse(typeof(DatabaseType),
                helper.GetString(DbType), false);
            }
            return type;
        }

        /// <summary>
        /// 设置resourceHelper里面的哈希表
        /// </summary>
        public static void SetHashByCommon(Hashtable hashtable)
        {
            hashtable.Clear();
            ResourceSet resourceSet = CommonVariablesKeys.ResourceManager.
                GetResourceSet(CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                object resourceName = entry.Key;
                object resourceValue = entry.Value;
                hashtable.Add(resourceName, resourceValue);
            }

        }

        #region Arcgis相关

        /// <summary>
        /// 缓冲区图形
        /// </summary>
        //public static IGeometry BufferGeometry = null;

        #region    咬合样式设置
        //private static ILineSymbol m_SketchSymbol = null;
        ///// <summary>
        ///// 获取素描线样式
        ///// </summary>
        //public static ILineSymbol SketchSymbol
        //{
        //    get
        //    {
        //        if (m_SketchSymbol == null)
        //        {
        //            m_SketchSymbol = new SimpleLineSymbolClass();
        //        }

        //        return m_SketchSymbol;
        //    }
        //    set
        //    {
        //        m_SketchSymbol = value;
        //    }
        //}

        //private static IMarkerSymbol m_SnapSymbol_Vertex = null;
        ///// <summary>
        ///// 获取咬合节点样式
        ///// </summary>
        //public static IMarkerSymbol SnapSymbol_Vertex
        //{
        //    get
        //    {
        //        if (m_SnapSymbol_Vertex == null)
        //        {
        //            SnapSymbol_Vertex = GetDefaultSnapSymbol();
        //        }

        //        return m_SnapSymbol_Vertex;
        //    }
        //    set
        //    {
        //        m_SnapSymbol_Vertex = value;
        //    }
        //}

        //private static IMarkerSymbol m_SnapSymbol_Edge = null;
        ///// <summary>
        ///// 获取咬合的边样式
        ///// </summary>
        //public static IMarkerSymbol SnapSymbol_Edge
        //{
        //    get
        //    {
        //        if (m_SnapSymbol_Edge == null)
        //            m_SnapSymbol_Edge = GetDefaultSnapSymbol();
        //        return m_SnapSymbol_Edge;
        //    }
        //    set { m_SnapSymbol_Edge = value; }
        //}

        //private static IMarkerSymbol m_SnapSymbol_Endpoint = null;
        ///// <summary>
        ///// 获取咬合的端点样式
        ///// </summary>
        //public static IMarkerSymbol SnapSymbol_Endpoint
        //{
        //    get
        //    {
        //        if (m_SnapSymbol_Endpoint == null)
        //            m_SnapSymbol_Endpoint = GetDefaultSnapSymbol();
        //        return m_SnapSymbol_Endpoint;
        //    }
        //    set { m_SnapSymbol_Endpoint = value; }
        //}

        //private static IMarkerSymbol m_SnapSymbol_Centroid = null;
        ///// <summary>
        ///// 获取咬合的中心点样式
        ///// </summary>
        //public static IMarkerSymbol SnapSymbol_Centroid
        //{
        //    get
        //    {
        //        if (m_SnapSymbol_Centroid == null)
        //            m_SnapSymbol_Centroid = GetDefaultSnapSymbol();
        //        return m_SnapSymbol_Centroid;
        //    }
        //    set { m_SnapSymbol_Centroid = value; }
        //}

        //private static IMarkerSymbol m_SnapSymbol_PerpendicularFoot = null;
        ///// <summary>
        ///// 获取咬合的垂足点样式
        ///// </summary>
        //public static IMarkerSymbol SnapSymbol_PerpendicularFoot
        //{
        //    get
        //    {
        //        if (m_SnapSymbol_PerpendicularFoot == null)
        //            m_SnapSymbol_PerpendicularFoot = GetDefaultSnapSymbol();
        //        return m_SnapSymbol_PerpendicularFoot;
        //    }
        //    set { m_SnapSymbol_PerpendicularFoot = value; }
        //}

        #endregion

        #endregion


    }


}
