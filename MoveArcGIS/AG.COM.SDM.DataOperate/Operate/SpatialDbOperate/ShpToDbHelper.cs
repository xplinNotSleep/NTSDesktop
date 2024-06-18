using AG.COM.SDM.Database;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;
using NetTopologySuite.Features;
using NetTopologySuite.Features.Fields;
using NetTopologySuite.IO.Esri;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AG.COM.SDM.Config;
using NetTopologySuite.Geometries;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

namespace AG.COM.SDM.DataOperate
{
    /// <summary>
    /// 矢量数据入库帮助类
    /// </summary>
    public static class ShpToDbHelper
    {
        /// <summary>
        /// 在空间数据库中根据源数据中的shp创建新的要素类数据
        /// </summary>
        /// <param name="shpPath"></param>
        /// <param name="IsOverload"></param>
        /// <param name="SRID"></param>
        public static void CreateToDbByShp(string shpPath, bool IsOverload, int SRID)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(shpPath);
                FileInfo[] filesInfo = dirInfo.GetFiles("*.shp", SearchOption.TopDirectoryOnly);
                if (filesInfo.Length == 0)
                {
                    AutoCloseMsgBox.Show("未查找到矢量数据文件!", "提示", 2000);
                    return;
                }
                #region
                //读取系统配置空间库(判断是否为sde)
                //if(SpatialDbHelper.IsSdeDb())
                //{
                //    SpatialDbHelper.GetSdeConfig();
                //}
                #endregion
                AdoDatabase adoDb = CommonVariables.SpatialdbConn.ConfigDatabase;
                foreach (FileInfo fileInfo in filesInfo)
                {
                    CreateLayer(fileInfo, shpPath, adoDb, IsOverload, SRID);
                }
            }
            catch (Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "提示", 4000);
                ExceptionLog.LogError(ex.Message, ex);
            }
        }

        public static void CreateLayer(FileInfo fileInfo, string shpPath,
            AdoDatabase adoDb, bool IsOverload, int SRID)
        {
            string fileName = fileInfo.FullName;
            string shpName = Path.GetFileNameWithoutExtension(fileName);
            string shpFile = shpPath + "\\" + shpName;
            //先查询数据库中是否存在指定名称的数据表
            bool IsExistLayer = SpatialDbHelper.ExistTable(adoDb, shpName);
            if (IsExistLayer)
            {
                if (!IsOverload) return;
                //如果覆盖的话那么直接删除表
                bool IsDelete = SpatialDbHelper.DeleteTable(adoDb, shpName);
                if (!IsDelete) return;
            }
            ShapefileUtils sourceShpUtil = new ShapefileUtils(shpFile);
            DbfField[] dbfFields = sourceShpUtil.GetShpFieldsOutGeo(shpFile);
            //从shp数据中获取几何字段信息并设置空间参考
            string strType = sourceShpUtil.GetShpGeoType(shpFile);
            GeoType geoType = getGeoType(strType);
            string geoInfo = $"GEOMETRY({strType}, {SRID})";
            bool IsCreateLayer = SpatialDbHelper.CreateTable(adoDb, shpName,
                "gid", "geom", geoInfo, dbfFields);
            if (!IsCreateLayer) return;
            int count = 0;
            Feature[] features = sourceShpUtil.ReadAllFeatures(shpFile);
            //遍历每个获取的要素
            foreach (Feature feature in features)
            {
                //将矢量图层中每个要素插入到数据库中
                bool IsInsert = IsInsertFeature(feature, sourceShpUtil, dbfFields,
                    geoType, adoDb, shpName, SRID);
                if (IsInsert)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                AutoCloseMsgBox.Show($"{shpName}导入失败!", "提示", 4000);
            }
            if (count == features.Length)
            {
                AutoCloseMsgBox.Show($"{shpName}导入成功!", "提示", 4000);
            }
        }

        public static bool IsInsertFeature(Feature feature, ShapefileUtils sourceShpUtil,
           DbfField[] dbfFields, GeoType geoType, AdoDatabase adoDb, string layerName,
           int SRID)
        {
            //获取该要素的几何信息
            Geometry geometry = feature.Geometry;
            Dictionary<DbfField, object> dicFieldInfos =
                new Dictionary<DbfField, object>();
            //遍历其中的每个字段，获取该要素所在字段对应的属性值
            dicFieldInfos = sourceShpUtil.getShpFieldInfo(dbfFields, feature);

            //进行数据导入
            bool IsInsert = SpatialDbHelper.IsInsertInTable(geoType, adoDb, layerName,
                dicFieldInfos, "geom", geometry, SRID);
            return IsInsert;
        }

        public static GeoType getGeoType(string strType)
        {
            GeoType geoType = GeoType.Point;
            switch (strType)
            {
                case "Point":
                    geoType = GeoType.Point;
                    break;
                case "MultiLineString":
                    geoType = GeoType.MultiLineString;
                    break;
                case "MultiPoint":
                    geoType = GeoType.MultiPoint;
                    break;
                case "Polygon":
                    geoType = GeoType.Polygon;
                    break;
                default:
                    break;

            }
            return geoType;

        }

        #region 
        //public static bool IsCreateTable(ShapefileUtils sourceShpUtil, string shpFile,
        //    int SRID, AdoDatabase adoDb, string shpName, DbfField[] dbfFields)
        //{
        //    //从shp数据中获取几何字段信息并设置空间参考
        //    string strType = sourceShpUtil.GetShpGeoType(shpFile);
        //    GeoType geoType = getGeoType(strType);
        //    string geoInfo = $"GEOMETRY({strType}, {SRID})";
        //    bool IsCreateLayer = SpatialDbHelper.CreateTable(adoDb, shpName,
        //        "gid", "geom", geoInfo, dbfFields);
        //    if (!IsCreateLayer) return false;
        //    return true;
        //}

        #endregion

    }
}
