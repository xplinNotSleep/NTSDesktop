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
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.DataOperate
{
    /// <summary>
    /// 矢量数据入库帮助类
    /// </summary>
    public static class ShpToDbAsyncHelper
    {
        /// <summary>
        /// 创建shp图层
        /// </summary>
        /// <param name="shpFile"></param>
        /// <param name="shpName"></param>
        /// <param name="adoDb"></param>
        /// <param name="IsOverload"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static async void CreateLayer(string shpFile, string shpName,
            AdoDatabase adoDb, bool IsOverload, int SRID, FrmTrackNoPause frmTrack)
        {
            //进程被终止
            if (frmTrack.IsBreak)
            {
                return;
            }
            frmTrack.TotalMsg = $"导入数据{shpName}";
            Application.DoEvents();
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
            int progressValue = 0;
            Feature[] features = sourceShpUtil.ReadAllFeatures(shpFile);
            //遍历每个获取的要素
            foreach (Feature feature in features)
            {
                if (frmTrack.IsBreak) break;
                await Task.Delay(100);
                progressValue++;
                //将矢量图层中每个要素插入到数据库中
                InsertFeature(feature, sourceShpUtil, dbfFields,
                    geoType, adoDb, shpName, SRID, progressValue,
                    features.Length, frmTrack);
                
            }
            
        }

        /// <summary>
        /// 创建shp图层(返回结果taskbool）
        /// </summary>
        /// <param name="shpFile"></param>
        /// <param name="shpName"></param>
        /// <param name="adoDb"></param>
        /// <param name="IsOverload"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static async Task<bool> IsCreateLayer(string shpFile, string shpName,
            AdoDatabase adoDb, bool IsOverload, int SRID, FrmTrackNoPause frmTrack)
        {
            //进程被终止
            if (frmTrack.IsBreak)
            {
                return false;
            }
            frmTrack.TotalMsg = $"导入数据{shpName}";
            Application.DoEvents();
            //先查询数据库中是否存在指定名称的数据表
            bool IsExistLayer = SpatialDbHelper.ExistTable(adoDb, shpName);
            if (IsExistLayer)
            {
                if (!IsOverload) return false;
                //如果覆盖的话那么直接删除表
                bool IsDelete = SpatialDbHelper.DeleteTable(adoDb, shpName);
                if (!IsDelete) return false;
            }
            ShapefileUtils sourceShpUtil = new ShapefileUtils(shpFile);
            DbfField[] dbfFields = sourceShpUtil.GetShpFieldsOutGeo(shpFile);
            //从shp数据中获取几何字段信息并设置空间参考
            string strType = sourceShpUtil.GetShpGeoType(shpFile);
            GeoType geoType = getGeoType(strType);
            string geoInfo = $"GEOMETRY({strType}, {SRID})";
            bool IsCreateLayer = SpatialDbHelper.CreateTable(adoDb, shpName,
                "gid", "geom", geoInfo, dbfFields);
            if (!IsCreateLayer) return false;
            int succount = 0;
            int progressValue = 0;
            Feature[] features = sourceShpUtil.ReadAllFeatures(shpFile);
            //遍历每个获取的要素
            foreach (Feature feature in features)
            {
                if (frmTrack.IsBreak) break;

                await Task.Delay(100);
                progressValue++;
                #region 是否成功将矢量图层中每个要素插入到数据库中
                bool IsInsert = IsInsertFeature(feature, sourceShpUtil, dbfFields,
                    geoType, adoDb, shpName, SRID, progressValue,
                    features.Length, frmTrack);
                if (IsInsert)
                {
                    succount++;
                }
                else
                {
                    //进程被终止
                    if (frmTrack.IsBreak)
                    {
                        break;
                    }
                }
                #endregion
            }
            if (succount == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否成功插入要素(绑定进度条)
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="sourceShpUtil"></param>
        /// <param name="dbfFields"></param>
        /// <param name="geoType"></param>
        /// <param name="adoDb"></param>
        /// <param name="layerName"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static void InsertFeature(Feature feature, ShapefileUtils sourceShpUtil,
           DbfField[] dbfFields, GeoType geoType, AdoDatabase adoDb, string layerName,
           int SRID, int progressValue, int featureCount, FrmTrackNoPause frmTrack)
        {
            //如果被点击终止，则不再执行下述操作
            if (frmTrack.IsBreak)
            {
                return;
            }
            frmTrack.ProValue++;
            frmTrack.SubMsg = "进度:" + progressValue + "/" + featureCount;
            Application.DoEvents();

            //获取该要素的几何信息
            Geometry geometry = feature.Geometry;
            Dictionary<DbfField, object> dicFieldInfos =
                new Dictionary<DbfField, object>();
            //遍历其中的每个字段，获取该要素所在字段对应的属性值
            dicFieldInfos = sourceShpUtil.getShpFieldInfo(dbfFields, feature);

            //进行数据导入
            SpatialDbHelper.InsertInTable(geoType, adoDb, layerName,
                dicFieldInfos, "geom", geometry, SRID);
        }

        /// <summary>
        /// 是否成功插入要素(绑定进度条)
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="sourceShpUtil"></param>
        /// <param name="dbfFields"></param>
        /// <param name="geoType"></param>
        /// <param name="adoDb"></param>
        /// <param name="layerName"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static bool IsInsertFeature(Feature feature, ShapefileUtils sourceShpUtil,
           DbfField[] dbfFields, GeoType geoType, AdoDatabase adoDb, string layerName,
           int SRID, int progressValue, int featureCount, FrmTrackNoPause frmTrack)
        {
            //如果被点击终止，则不再执行下述操作
            if(frmTrack.IsBreak)
            {
                return false;
            }
            frmTrack.ProValue++;
            frmTrack.SubMsg = "进度:" + progressValue + "/" + featureCount;
            Application.DoEvents();

            //获取该要素的几何信息
            Geometry geometry = feature.Geometry;
            Dictionary<DbfField, object> dicFieldInfos =
                new Dictionary<DbfField, object>();
            //遍历其中的每个字段，获取该要素所在字段对应的属性值
            dicFieldInfos = sourceShpUtil.getShpFieldInfo(dbfFields, feature);

            //进行数据导入
            //SpatialDbHelper.InsertInTable(geoType, adoDb, layerName,
            //    dicFieldInfos, "geom", geometry, SRID);
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


    }
}
