using NetTopologySuite.Features;
using NetTopologySuite.Features.Helpers;
using Pure.Data;
using Pure.Data.Migration.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NetTopologySuite.IO.SpatialDb
{
    internal class SdeContontHelper
    {
        IDatabase Database { get; set; }
        ITransformationProvider TransformationProvider { get; set; }
        public SdeContontHelper(SdeConnectstringParam param)
        {
            //此处内部代码还需要检测运行路径下是否存在bin文件夹，不存在则直接报错，
            //故还需要在运行路径下创建一个bin文件夹防止报错
            Database = new Database($"Host={param.Host};Port={param.Port};Database={param.Database};Username={param.Username};Password={param.Password}", DatabaseType.PostgreSQL);
            TransformationProvider = Database.CreateTransformationProvider();
            TransformationProvider.SetSchema(param.Username);
        }

        internal DataTable GetLayer(string layerName, string backFieldNames = null, string whereClause = null)
        {
            if (!TransformationProvider.TableExists(layerName))
            {
                throw new Exception("表名有误,请验证");
            }
            if (!TransformationProvider.ColumnExists(layerName, "shape"))
            {
                throw new Exception("当前表非空间表");
            }
            string columnsStr = "";
            if(string.IsNullOrEmpty(backFieldNames))
            {
                //var columns = TransformationProvider.GetColumnInfoByTableName(layerName);
                var columns = TransformationProvider.GetColumns(layerName);
                foreach (var column in columns.Where(p => p.Name != "shape"))
                {
                    columnsStr += column.Name + ",";
                }
                columnsStr=columnsStr.TrimEnd(',');
            }
            else
            {
                columnsStr = backFieldNames;
            }
            var sql = $"select {columnsStr},st_asbinary(shape) as shape from {layerName}";
            if (!string.IsNullOrEmpty(whereClause))
            {
                sql += " where " + whereClause;
            }
            return Database.ExecuteDataTable(sql);
        }

        internal IList<IDictionary<string, object>> GetAttritubes(string layerName, string backFieldNames = null, string whereClause = null)
        {
            if (!TransformationProvider.TableExists(layerName))
            {
                throw new Exception("表名有误,请验证");
            }
            string columnsStr = "";
            if (string.IsNullOrEmpty(backFieldNames))
            {
                var columns = TransformationProvider.GetColumns(layerName);
                foreach (var column in columns.Where(p => p.Name != "shape"))
                {
                    columnsStr += column.Name + ",";
                }
                columnsStr = columnsStr.TrimEnd(',');
            }
            else
            {
                columnsStr = backFieldNames;
            }
            var sql = $"select {columnsStr} from {layerName}";
            if (!string.IsNullOrEmpty(whereClause))
            {
                sql += " where " + whereClause;
            }
            return Database.ExecuteList<dynamic>(sql).Select(p => p as IDictionary<string, object>).ToList();
        }

        //internal IEnumerable<ColumnInfo> GetColumns(string layerName)
        //{
        //    return TransformationProvider.GetColumns(layerName);
        //}

        internal Column[] GetColumns(string layerName)
        {
            return TransformationProvider.GetColumns(layerName);
        }

        internal void CreateGeometryTable(string layerName, Column[] columns, bool IsOverwise = true)
        {
            if (TransformationProvider.TableExists(layerName))
            {
                if (IsOverwise)
                {
                    TransformationProvider.RemoveTable(layerName);
                }
                else
                {
                    return;
                }
            }
            var shapeColumn = columns.FirstOrDefault(p => p.Name.ToLower() == "shape");
            if (shapeColumn != null)
            {
                shapeColumn.TypeString = "st_geometry";
            }
            TransformationProvider.AddTable(layerName, columns);
        }

        internal DataTable GetGeometry(string layerName, string whereClause = null)
        {
            if (!TransformationProvider.TableExists(layerName))
            {
                throw new Exception("表名有误,请验证");
            }
            if (!TransformationProvider.ColumnExists(layerName, "shape"))
            {
                throw new Exception("当前表非空间表");
            }
            var sql = $"select st_asbinary(shape) as shape from {layerName}";
            if (!string.IsNullOrEmpty(whereClause))
            {
                sql += " where " + whereClause;
            }
            return Database.ExecuteDataTable(sql);
        }

        internal ShapeType GetShapeType(string layerName)
        {
            if (!TransformationProvider.TableExists(layerName))
            {
                throw new Exception("表名有误,请验证");
            }
            if (!TransformationProvider.ColumnExists(layerName, "shape"))
            {
                throw new Exception("当前表非空间表");
            }
            var sql = $"select st_asbinary(shape) as shape from {layerName} limit 1";
            var bytes = (byte[])Database.ExecuteScalar(sql);
            WKBReader wKBReader = new WKBReader();
            var geometry = wKBReader.Read(bytes);
            return geometry.GetShapeType();
        }

        internal string GetProjection(string layName)
        {
            var sql = $"select b.srtext from sde_layers a left join public.sde_spatial_references b on a.srid =b.srid where a.table_name ='{layName}'";
            var ret = Database.ExecuteScalar(sql);
            if (ret != null)
            {
                return ret.ToString();
            }
            return null;
        }

        internal void InsertLayer(string layerName, IEnumerable<IFeature> features)
        {
            if (!TransformationProvider.ColumnExists(layerName, "shape"))
            {
                throw new Exception("当前表非空间表");
            }
            var sqlMaxObjectid = $"select max(objectid) from {layerName}";
            var maxObjectId = Database.ExecuteScalar(sqlMaxObjectid);
            int objectId = 0;
            if (maxObjectId != null)
            {
                objectId = (int)maxObjectId;
            }
            foreach (var feature in features)
            {
                Dictionary<string, object> value = new Dictionary<string, object>();
                string columnsStr = "";
                string columnsParams = "";
                if (feature.Attributes != null)
                {
                    foreach (var attr in feature.Attributes.GetNames())
                    {
                        if (feature.Attributes[attr] is DBNull) continue;
                        if (attr == "objectid")
                        {
                            value.Add(attr, ++objectId);
                        }
                        else
                        {
                            value.Add(attr, feature.Attributes[attr]);
                        }
                        columnsStr += attr + ",";
                        columnsParams += "@" + attr + ",";
                    }
                }
                columnsStr += "shape";
                columnsParams += "st_geomfromwkb(@shape)";
                var sql = $"insert into {layerName} ({columnsStr}) values ({columnsParams})";
                value.Add("shape", feature.Geometry.AsBinary());
                Database.Execute(sql, value);
            }
        }
    }
}
