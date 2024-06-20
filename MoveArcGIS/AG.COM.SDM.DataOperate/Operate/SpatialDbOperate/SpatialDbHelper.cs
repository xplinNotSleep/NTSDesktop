using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Database;
using NetTopologySuite.Features.Fields;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.SpatialDb;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.DataOperate
{
    public static class SpatialDbHelper
    {
        /// <summary>
        /// 判断数据库中某个名称的表是否存在
        /// </summary>
        /// <param name="adoDb"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static bool ExistTable(AdoDatabase adoDb, string layerName)
        {
            return adoDb.ExistTable(layerName);
        }

        /// <summary>
        /// 删除数据库中某表
        /// </summary>
        /// <param name="adoDb"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static bool DeleteTable(AdoDatabase adoDb, string layerName)
        {
            return adoDb.DeleteTable(layerName);
        }

        /// <summary>
        /// 在配置的空间数据库中创建具有空间属性的表
        /// </summary>
        /// <param name="adoDb"></param>
        /// <param name=""></param>
        public static bool CreateTable(AdoDatabase adoDb, string layerName,
            string idField, string geoField, string geoType, DbfField[] FieldInfos)
        {
            //先创建一个数据表并添加可以自增的id字段
            bool IsCreateTable = adoDb.CreateTable(layerName, idField);
            if (!IsCreateTable)
            {
                return false;
            }
            //添加postgis支持的几何字段
            bool addGeoColumn = adoDb.AddColumn(layerName, geoField, geoType);
            if (!addGeoColumn)
            {
                return false;
            }
            //添加其他字段
            bool addOtherFields = AddFields(adoDb, layerName, FieldInfos);
            if (!addOtherFields)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将若干配置好的字段添加到新的数据表中
        /// </summary>
        /// <param name="adoDb"></param>
        /// <param name="layerName"></param>
        /// <param name="dicFieldInfos"></param>
        /// <returns></returns>
        public static bool AddFields(AdoDatabase adoDb, string layerName,
            DbfField[] FieldInfos)
        {
            int count = 0;
            foreach (DbfField dbfField in FieldInfos)
            {
                string fieldName = dbfField.Name;
                //string fieldType = ConvertToType(dbfField.FieldType);
                string fieldType = InitColumnType(dbfField);
                bool IsAdd = adoDb.AddColumn(layerName, fieldName, fieldType);
                if (!IsAdd) continue;
                count++;
            }
            if (count == 0) return false;
            return true;
        }

        public static string InitColumnType(DbfField dbfField)
        {
            string fieldType = "";
            DbfType dbfType = dbfField.FieldType;
            switch (dbfType)
            {
                case DbfType.Character:
                    fieldType = "varchar" + $"({dbfField.Length})";
                    break;
                case DbfType.Date:
                    fieldType = "date";
                    break;
                case DbfType.Numeric:
                    fieldType = "numeric" + $"({dbfField.Length})";
                    //fieldType = "numeric" + $"({dbfField.Length},{dbfField.NumericScale})";
                    break;
                case DbfType.Float:
                    fieldType = "float" + $"({dbfField.Length})";
                    break;
                case DbfType.Logical:
                    fieldType = "bool";
                    break;
                case DbfType.Shape:
                    fieldType = "geometry";
                    break;
                default:
                    fieldType = "varchar" + $"({dbfField.Length})";
                    break;
            }
            return fieldType;

        }

        /// <summary>
        /// 将源数据中的属性信息和几何坐标插入目标数据表中
        /// </summary>
        /// <param name="geoType"></param>
        /// <param name="adoDb"></param>
        /// <param name="LayerName"></param>
        /// <param name="dicFieldInfos"></param>
        /// <param name="fieldGeo"></param>
        /// <param name="geometry"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static void InsertInTable(GeoType geoType, AdoDatabase adoDb, string LayerName,
            Dictionary<DbfField, object> dicFieldInfos,
            string fieldGeo, Geometry geometry, int SRID)
        {
            string strGeoInsert = GetGeoQuery(geoType, geometry, SRID);
            if (strGeoInsert == "")
            {
                AutoCloseMsgBox.Show($"暂不支持导入{geoType}格式的数据!", "警告", 1000);
                return;
            }

            string columnsStr = $"{fieldGeo}";
            string columnsParams = $"{strGeoInsert}";
            foreach (DbfField dbfField in dicFieldInfos.Keys)
            {
                string pickFieldName = dbfField.Name;
                columnsStr += $",{pickFieldName}";
                if (dicFieldInfos[dbfField] == null)
                {
                    if (dbfField.FieldType == DbfType.Character)
                    {
                        columnsParams += $",''";
                    }
                    else
                    {
                        columnsParams += $",null";
                    }

                }
                string fieldValue = dicFieldInfos[dbfField].ToString();
                //如果对应字段的属性值不为空，则sql里面添加要插入的字段值
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    if (dbfField.FieldType == DbfType.Date)
                    {
                        columnsParams += $",to_date('{fieldValue}','YYYY-MM-DD')";
                    }
                    else
                    {
                        columnsParams += $",'{fieldValue}'";
                    }
                }
                else
                {
                    if (dbfField.FieldType == DbfType.Character)
                    {
                        columnsParams += $",''";
                    }
                    else
                    {
                        columnsParams += $",null";
                    }
                }
            }
            string sqlInsert = $"insert into {LayerName} ({columnsStr}) values ({columnsParams})";
            bool IsInsert = adoDb.Execute(sqlInsert);

            return;
        }

        /// <summary>
        /// 将源数据中的属性信息和几何坐标插入目标数据表中
        /// </summary>
        /// <param name="geoType"></param>
        /// <param name="adoDb"></param>
        /// <param name="LayerName"></param>
        /// <param name="dicFieldInfos"></param>
        /// <param name="fieldGeo"></param>
        /// <param name="geometry"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static bool IsInsertInTable(GeoType geoType, AdoDatabase adoDb, string LayerName,
            Dictionary<DbfField, object> dicFieldInfos,
            string fieldGeo, Geometry geometry, int SRID)
        {
            try
            {
                string strGeoInsert = GetGeoQuery(geoType, geometry, SRID);
                if (strGeoInsert == "")
                {
                    AutoCloseMsgBox.Show($"暂不支持导入{geoType}格式的数据!", "警告", 3000);
                    return false;
                }

                string columnsStr = $"{fieldGeo}";
                string columnsParams = $"{strGeoInsert}";
                foreach (DbfField dbfField in dicFieldInfos.Keys)
                {
                    string pickFieldName = dbfField.Name;
                    columnsStr += $",{pickFieldName}";
                    if (dicFieldInfos[dbfField] == null)
                    {
                        if (dbfField.FieldType == DbfType.Character)
                        {
                            columnsParams += $",''";
                        }
                        else
                        {
                            columnsParams += $",null";
                        }

                    }
                    string fieldValue = dicFieldInfos[dbfField].ToString();
                    //如果对应字段的属性值不为空，则sql里面添加要插入的字段值
                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        if (dbfField.FieldType == DbfType.Date)
                        {
                            columnsParams += $",to_date('{fieldValue}','YYYY-MM-DD')";
                        }
                        else
                        {
                            columnsParams += $",'{fieldValue}'";
                        }
                    }
                    else
                    {
                        if (dbfField.FieldType == DbfType.Character)
                        {
                            columnsParams += $",''";
                        }
                        else
                        {
                            columnsParams += $",null";
                        }
                    }
                }
                string sqlInsert = $"insert into {LayerName} ({columnsStr}) values ({columnsParams})";
                bool IsInsert = adoDb.Execute(sqlInsert);

                return IsInsert;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据源数据中获取出的几何初始化几何插入sql段
        /// </summary>
        /// <param name="geoType"></param>
        /// <param name="geometry"></param>
        /// <param name="SRID"></param>
        /// <returns></returns>
        public static string GetGeoQuery(GeoType geoType, Geometry geometry, int SRID)
        {
            string strQuery = "";
            switch (geoType)
            {
                case GeoType.Point:
                    Point point = geometry as Point;
                    double px = point.X;
                    double py = point.Y;
                    strQuery = $"st_geometryfromtext('{geoType.ToString()}({px} {py})',{SRID})";
                    break;
                case GeoType.MultiLineString:
                    MultiLineString polyline = geometry as MultiLineString;
                    LineString line = polyline[0] as LineString;
                    double lx0 = line.StartPoint.X;
                    double ly0 = line.StartPoint.Y;
                    double lx1 = line.EndPoint.X;
                    double ly1 = line.EndPoint.Y;
                    strQuery = $"st_geometryfromtext('{geoType.ToString()}(({lx0} {ly0}, {lx1} {ly1}))',{SRID})";
                    break;
                case GeoType.Polygon:
                    Polygon polygon = geometry as Polygon;
                    break;
                default:
                    break;
            }
            return strQuery;
        }

        public static bool IsSdeDb()
        {
            AdoDatabase adoDb = CommonVariables.SpatialdbConn.ConfigDatabase;
            bool IsSDE = adoDb.IsSdeDatabase();
            return IsSDE;
        }

        /// <summary>
        /// 从全局空间数据库配置中初始化sde服务对象
        /// </summary>
        public static void GetSdeConfig()
        {
            //用Pure.Data获取sde数据库时需要检测运行路径下是否存在bin文件夹，不存在则直接报错，
            //故还需要在运行路径下创建一个bin文件夹防止报错
            string binPath = Application.StartupPath + "\\" + "bin";
            if (!Directory.Exists(binPath))
            {
                Directory.CreateDirectory(binPath);
            }
            SdeConnectstringParam sdeParam = InitSdeParam();
            SdeLayerUtils sdeConfig = new SdeLayerUtils(sdeParam);
            Console.Write("gg");
        }

        public static SdeConnectstringParam InitSdeParam()
        {
            SpatialDBConfig sysSdeConfig = CommonVariables.SpatialdbConn;
            SdeConnectstringParam sdeParam = new SdeConnectstringParam()
            {
                Host = sysSdeConfig.Spatial_Server,
                Port = sysSdeConfig.Spatial_Port,
                Database = sysSdeConfig.Spatial_DataBase,
                Username = sysSdeConfig.Spatial_User,
                Password = sysSdeConfig.Spatial_Password
            };
            return sdeParam;
        }


        #region 
        /// <summary>
        /// 将DbfField的格式转换为导入数据库的字段格式
        /// </summary>
        /// <param name="dbfType"></param>
        /// <returns></returns>
        //public static string ConvertToType(DbfType dbfType)
        //{
        //    string fieldType = "";
        //    switch (dbfType)
        //    {
        //        case DbfType.Character:
        //            fieldType = "varchar";
        //            break;
        //        case DbfType.Date:
        //            fieldType = "date";
        //            break;
        //        case DbfType.Numeric:
        //            fieldType = "numeric";
        //            break;
        //        case DbfType.Float:
        //            fieldType = "float";
        //            break;
        //        case DbfType.Logical:
        //            fieldType = "bool";
        //            break;
        //        case DbfType.Shape:
        //            fieldType = "geometry";
        //            break;
        //        default:
        //            fieldType = "varchar";
        //            break;
        //    }
        //    return fieldType;
        //}

        /// <summary>
        /// 将shp源数据中的要素通过sql语句插入到目标图层中(主键id需要赋值）
        /// </summary>
        /// <param name="exportGeoType"></param>
        /// <param name="adoDb"></param>
        /// <param name="LayerName"></param>
        /// <param name="IsLine"></param>
        /// <param name="featureImport"></param>
        /// <param name="pickFields"></param>
        /// <param name="fieldId"></param>
        /// <param name="fieldGeo"></param>
        /// <returns></returns>
        //public static bool IsInsertInTableWithId(GeoType geoType, AdoDatabase adoDb, string LayerName,
        //    Dictionary<DbfField, object> dicFieldInfos,
        //    string fieldId, string fieldGeo, Geometry geometry)
        //{
        //    try
        //    {
        //        string strGeoInsert = GetGeoQuery(geoType, geometry);
        //        if (strGeoInsert == "")
        //        {
        //            AutoCloseMsgBox.Show($"暂不支持导入{geoType}格式的数据!", "警告", 3000);
        //            return false;
        //        }

        //        string columnsStr = $"{fieldId},{fieldGeo}";
        //        //string columnsParams = $"{maxId},{strGeoInsert}";
        //        for (int j = 0; j < pickFields.Count; j++)
        //        {
        //            string pickFieldName = pickFields[j];
        //            //读取数据源中的字段值
        //            int index = featureImport.Fields.FindField(pickFields[j]);
        //            if (index < 0) continue;
        //            IField field = featureImport.Fields.Field[index];
        //            string fieldValue = featureImport.get_Value(index).ToString();
        //            //如果对应字段的属性值不为空，则sql里面添加要插入的字段值
        //            if (!string.IsNullOrEmpty(fieldValue))
        //            {
        //                columnsStr += $",{pickFields[j]}";
        //                if (field.Type == esriFieldType.esriFieldTypeDate)
        //                {
        //                    columnsParams += $",to_date('{fieldValue}','YYYY-MM-DD')";
        //                }
        //                else
        //                {
        //                    columnsParams += $",'{fieldValue}'";
        //                }
        //            }
        //        }
        //        string sqlInsert = $"insert into {LayerName} ({columnsStr}) values ({columnsParams})";
        //        bool IsInsert = adoDb.Execute(sqlInsert);

        //        return IsInsert;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        #endregion

    }
}
