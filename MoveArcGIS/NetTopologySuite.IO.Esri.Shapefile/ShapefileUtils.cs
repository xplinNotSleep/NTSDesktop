using NetTopologySuite.Features;
using NetTopologySuite.Features.Fields;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetTopologySuite.IO.Esri
{
    public class ShapefileUtils : ILayerUtils
    {
        string layerPath { get; set; }
        public ShapefileUtils() { }
        public ShapefileUtils(string layerPath)
        {
            this.layerPath = layerPath;
        }

        public ShapeType GetShapeType(string layName)
        {
            layName = GetLayerName(layName);
            return Shapefile.GetShapeType(layName);
        }

        public string GetProjection(string layName)
        {
            layName = GetLayerName(layName);
            var projFile = Path.ChangeExtension(layName, ".prj");
            if (File.Exists(projFile))
            {
                return File.ReadAllText(projFile);
            }
            return null;
        }

        /// <summary>
        /// 获取shp图层中的几何字段并设置空间参考信息，用于入库
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public string GetShpGeoType(string layName)
        {
            layName = GetLayerName(layName);
            string GeoType = Shapefile.GetShpGeoType(layName);
            return GeoType;
        }

        public Feature[] ReadAllFeatures(string layName, string backFieldNames = null, string whereClause = null)
        {
            layName = GetLayerName(layName);
            return Shapefile.ReadAllFeatures(layName, backFieldNames, whereClause);
        }

        public Geometry[] ReadAllGeometries(string layName, string whereClause = null)
        {
            layName = GetLayerName(layName);
            return Shapefile.ReadAllGeometries(layName);
        }

        public void WriteAllFeatures(IEnumerable<IFeature> features, string layName,
            DbfField[] dbfFields, string projection = null, Encoding encoding = null)
        {
            layName = GetLayerName(layName);
            Shapefile.WriteAllFeatures(features, dbfFields, layName, projection, encoding);
        }

        public void WriteAllFeatures(IEnumerable<IFeature> features, string layName, string projection = null, Encoding encoding = null)
        {
            layName = GetLayerName(layName);
            Shapefile.WriteAllFeatures(features, layName, projection, encoding);
        }

        public DbfFieldCollection GetDbfFields(string layName)
        {
            using (var dbfReader = new DbfReader(layName))
            {
                return dbfReader.Fields;
            }
        }

        /// <summary>
        /// 获取某个路径shp图层中的所有字段
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public DbfField[] GetShpDbfFields(string layerName)
        {
            layerName = GetLayerName(layerName);
            DbfField[] dbfFields = Shapefile.GetShpDbfFields(layerName);
            return dbfFields;
        }

        /// <summary>
        /// 获取除了几何字段之外的其他普通字段
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public DbfField[] GetShpFieldsOutGeo(string layerName)
        {
            layerName = GetLayerName(layerName);
            DbfField[] dbfFields = Shapefile.GetShpDbfFieldsOutGeo(layerName);
            return dbfFields;
        }

        public Dictionary<DbfField, object> getShpFieldInfo(DbfField[] fields, Feature feature)
        {
            //string shplayerName = GetLayerName(layerName);
            return Shapefile.getShpFieldInfo(fields, feature);
        }

        public IList<IDictionary<string, object>> GetAttritubes(string layName, string backFieldNames = null, string whereClause = null)
        {
            return Shapefile.GetAttritubes(layName, backFieldNames, whereClause);
        }

        private string GetLayerName(string layName)
        {
            if (!File.Exists(layName) && this.layerPath != null)
            {
                if (layName.ToLower().EndsWith(".shp"))
                {
                    layName = Path.Combine(this.layerPath, layName);
                }
                else
                {
                    layName = Path.Combine(this.layerPath, layName + ".shp");
                }
            }
            return layName;
        }
    }
}
