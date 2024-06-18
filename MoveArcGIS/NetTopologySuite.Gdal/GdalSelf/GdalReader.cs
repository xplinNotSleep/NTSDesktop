using NetTopologySuite.GdalEx.Extension;
using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Text.Unicode;

namespace NetTopologySuite.GdalEx
{
    public enum GdalFileType
    {
        Shp = 0,
        Gdb = 1,
        Sde = 2,
        UnKnown = 9
    }

    public class GdalReader : IReader
    {
        static ConcurrentDictionary<string, IReader> Readers { get; set; } = new ConcurrentDictionary<string, IReader>();
        bool IsUTF8 { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="path">
        /// sde的路径写法：PG:dbname=testgdal host=172.18.2.168 port=5432 user=sde password=sde
        /// </param>
        /// <returns></returns>
        public static IReader GetReader(GdalFileType fileType, string path)
        {
            if (Readers.ContainsKey(path))
            {
                return Readers[path];
            }
            IReader reader = null;
            switch (fileType)
            {
                case GdalFileType.Shp:
                    reader = new GdalReader("ESRI Shapefile", path, false);
                    break;
                case GdalFileType.Gdb:
                    reader = new GdalReader("OpenFileGDB", path, false);
                    break;
                case GdalFileType.Sde:
                    reader = new GdalReader("PostgreSQL", path, true);
                    break;
                default:
                    reader = null;
                    break;
            }
            Readers[path] = reader;
            return reader;
        }

        DataSource dataSource = null;
        public GdalReader(string typeName, string path, bool isUTF8 = false)
        {
            var driver = Ogr.GetDriverByName(typeName);
            //这里新增创建新的gdb文件
            if(File.Exists(path)||Directory.Exists(path))
            {
                dataSource = driver.Open(path, 1);
            }
            else
            {
                dataSource = driver.CreateDataSource(path, null);
            }
            IsUTF8 = isUTF8;
        }

        public DataSource GetDataSource() { return dataSource; }

        public List<string> GetLayerNames()
        {
            var layers = new List<string>();
            for (int i = 0; i < dataSource.GetLayerCount(); i++)
            {
                var layer = dataSource.GetLayerByIndex(i);
                layers.Add(IsUTF8 ? layer.GetNameEx() : layer.GetName());
            }
            return layers;
        }

        public Layer GetLayerByName(string layerName)
        {
            return IsUTF8 ? dataSource.GetLayerByNameEx(layerName) : dataSource.GetLayerByName(layerName);
        }

        public List<Feature> GetFeatures(string layerName)
        {
            var layer = GetLayerByName(layerName);
            return GetFeatures(layer);
        }

        public List<Feature> GetFeatures(Layer layer)
        {
            List<Feature> features = new List<Feature>();

            long feaCount = layer.GetFeatureCount(1);
            for (int i = 0; i < feaCount; i++)
            {
                var featrue = layer.GetFeature(i);
                if (featrue == null) continue;
                features.Add(featrue);
            }
            return features;
        }

        public List<FieldAttribute> GetFields(string layerName)
        {
            var layer = GetLayerByName(layerName);
            return GetFields(layer);
        }

        public List<FieldAttribute> GetFields(Layer layer,string backFieldNames=null)
        {
            List<FieldAttribute> attrs = new List<FieldAttribute>();
            var featrueDefn = layer.GetLayerDefn();
            List<string> listBackFieldNames = null;
            if(!string.IsNullOrEmpty( backFieldNames ))
            {
                listBackFieldNames=backFieldNames.ToLower().Split(new char[] { ',' }).ToList();
            }
            for (int j = 0; j < featrueDefn.GetFieldCount(); j++)
            {
                var field = featrueDefn.GetFieldDefn(j);
                var name= (IsUTF8 ? field.GetNameEx() : field.GetName()).ToLower();
                if (listBackFieldNames != null && !listBackFieldNames.Contains(name))
                    continue;
                FieldAttribute fieldAttr = new FieldAttribute();
                fieldAttr.Name = name;
                fieldAttr.Type = field.GetFieldType();
                fieldAttr.Width = field.GetWidth();
                fieldAttr.Precision = field.GetPrecision();
                attrs.Add(fieldAttr);
            }
            return attrs;
        }
        
        public object GetFeatureVal(Feature feature, FieldType type, string fieldName)
        {
            object val = null;
            switch (type)
            {
                case FieldType.OFTString:
                    val = IsUTF8 ? feature.GetFieldAsStringEx(fieldName) : feature.GetFieldAsString(fieldName);
                    break;
                case FieldType.OFTInteger:
                    val = IsUTF8 ? feature.GetFieldAsIntegerEx(fieldName) : feature.GetFieldAsInteger(fieldName);
                    break;
                case FieldType.OFTInteger64:
                    val = IsUTF8 ? feature.GetFieldAsInteger64Ex(fieldName) : feature.GetFieldAsInteger64(fieldName);
                    break;
                case FieldType.OFTDate:
                case FieldType.OFTDateTime:
                    val = IsUTF8 ? feature.GetFieldAsISO8601DateTimeEx(fieldName, null) : feature.GetFieldAsISO8601DateTime(fieldName, null);
                    break;
                case FieldType.OFTReal:
                    val = IsUTF8 ? feature.GetFieldAsDoubleEx(fieldName) : feature.GetFieldAsDouble(fieldName);
                    break;
                default:
                    val = IsUTF8 ? feature.GetFieldAsStringEx(fieldName) : feature.GetFieldAsString(fieldName);
                    break;
            }
            return val;

        }
        
        public IList<IDictionary<string, object>> GetAttributes(string layerName)
        {
            var layer = GetLayerByName(layerName);
            var features = GetFeatures(layer);
            var fields = GetFields(layer);
            return GetAttributes(features, fields);
        }

        public IList<IDictionary<string, object>> GetAttributes(List<Feature> features,List<FieldAttribute> fields)
        {
            IList<IDictionary<string, object>> attrs = new List<IDictionary<string, object>>();
            foreach (var feature in features)
            {
                attrs.Add(GetAttrbute(feature,fields));
            }
            return attrs;
        }

        public IDictionary<string, object> GetAttrbute(Feature feature, List<FieldAttribute> fields)
        {
            IDictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (var field in fields)
            {
                object val = GetFeatureVal(feature, field.Type, field.Name);
                keyValuePairs.Add(field.Name, val);
            }
            return keyValuePairs;
        }

        /// <summary>
        /// 获取几何要素类字段
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public List<FieldDefn> GetFieldsByLayer(string layerName)
        {
            List<FieldDefn> fieldDefns = new List<FieldDefn>();
            var layer = GetLayerByName(layerName);
            var featrueDefn = layer.GetLayerDefn();
            int count = featrueDefn.GetFieldCount();
            for (int j = 0; j < featrueDefn.GetFieldCount(); j++)
            {
                var fieldDefn = featrueDefn.GetFieldDefn(j);
                fieldDefns.Add(fieldDefn);
            }
            return fieldDefns;

        }

        public List<FeatureDefn> GetFeatureDefns(string layerName)
        {
            var layer = GetLayerByName(layerName);
            return GetFeatureDefns(layer);
        }

        public List<FeatureDefn> GetFeatureDefns(Layer layer)
        {
            List<FeatureDefn> featureDefns = new List<FeatureDefn>();
            for (int i = 0; i < layer.GetFeatureCount(1); i++)
            {
                featureDefns.Add(layer.GetFeature(i).GetDefnRef());
            }
            return featureDefns;
        }

        public List<Geometry> GetGeometries(string layerName)
        {
            var layer = GetLayerByName(layerName);
            return GetGeometries(layer);
        }

        public List<Geometry> GetGeometries(Layer layer)
        {
            List<Geometry> geos = new List<Geometry>();
            for (long i = 0; i < layer.GetFeatureCount(1); i++)
            {
                var oFeature = layer.GetFeature(i);
                var oGeo = oFeature?.GetGeometryRef();
                if (oGeo != null)
                {
                    geos.Add(oGeo);
                }
            }
            return geos;
        }

        #region create部分(创建新的要素类)
        /// <summary>
        /// 从源gdb中获取特定名称图层的几何信息
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public IFeatureLayer GetInfoByLayer(string layerName)
        {
            Layer sourceLayer = GetLayerByName(layerName);
            SpatialReference sr = sourceLayer.GetSpatialRef();
            string srname = sr.GetName();
            wkbGeometryType geoType = sourceLayer.GetGeomType();
            List<FieldDefn> fieldDefns = GetFieldsByLayer(layerName);

            IFeatureLayer featureLayer = new FeatureLayer()
            {
                Name = layerName,
                SR = sr,
                Layer = sourceLayer,
                GeometryType = geoType,
                FieldDefns = fieldDefns
            };

            return featureLayer;
        }

        /// <summary>
        /// 从源gdb中拷贝图层到新建的图层中
        /// </summary>
        /// <param name="featureLayer"></param>
        public void CreateLayerBySourceLayer(IFeatureLayer featureLayer)
        {
            SpatialReference sr = featureLayer.SR;
            string srName = sr.GetName();
            string layerName = featureLayer.Name;
            wkbGeometryType geoType = featureLayer.GeometryType;
            Layer newLayer = IsUTF8 ? dataSource.CreateLayerEx(layerName, sr, geoType, null) :
                dataSource.CreateLayer(layerName, sr, geoType, null);
            List<FieldDefn> fieldDefns = featureLayer.FieldDefns;
            foreach(FieldDefn fieldDefn in fieldDefns)
            {
                newLayer.CreateField(fieldDefn, 1);
            }
            Layer sourceLayer = featureLayer.Layer;
            List<Feature> sourceFeatures = GetFeatures(sourceLayer);
            for(int i = 0; i < sourceFeatures.Count; i++)
            {
                Feature sourceFeature = sourceFeatures[i];
                FeatureDefn featureDefn = sourceFeature.GetDefnRef();
                Feature newFeature = new Feature(featureDefn);
                foreach (FieldDefn fieldDefn in fieldDefns)
                {
                    SetFieldValue(sourceFeature, newFeature, fieldDefn);
                }
                Geometry geometry = sourceFeature.GetGeometryRef();
                newFeature.SetGeometry(geometry);
                newLayer.CreateFeature(newFeature);
            }

        }

        /// <summary>
        /// 根据字段类型设置不同类型字段值
        /// </summary>
        /// <param name="feature"></param>
        /// <param name=""></param>
        public void SetFieldValue(Feature feature, Feature newFeature,FieldDefn fieldDefn)
        {
            FieldType fieldType = fieldDefn.GetFieldType();
            string fieldName = fieldDefn.GetName();
            switch (fieldType)
            {
                case FieldType.OFTString:
                    string strVal = IsUTF8 ? feature.GetFieldAsStringEx(fieldName) : feature.GetFieldAsString(fieldName);
                    newFeature.SetField(fieldName, strVal);
                    break;
                case FieldType.OFTInteger:
                    int intVal = IsUTF8 ? feature.GetFieldAsIntegerEx(fieldName) : feature.GetFieldAsInteger(fieldName);
                    newFeature.SetField(fieldName, intVal);
                    break;
                case FieldType.OFTInteger64:
                    int int64Val = IsUTF8 ? feature.GetFieldAsIntegerEx(fieldName) : feature.GetFieldAsInteger(fieldName);
                    newFeature.SetField(fieldName, int64Val);
                    break;
                case FieldType.OFTDate:
                case FieldType.OFTDateTime:
                    string dateVal = IsUTF8 ? feature.GetFieldAsISO8601DateTimeEx(fieldName, null) : feature.GetFieldAsISO8601DateTime(fieldName, null);
                    newFeature.SetField(fieldName, dateVal);
                    break;
                case FieldType.OFTReal:
                    double doubleVal = IsUTF8 ? feature.GetFieldAsDoubleEx(fieldName) : feature.GetFieldAsDouble(fieldName);
                    newFeature.SetField(fieldName, doubleVal);
                    break;
                default:
                    string val = IsUTF8 ? feature.GetFieldAsStringEx(fieldName) : feature.GetFieldAsString(fieldName);
                    newFeature.SetField(fieldName, val);
                    break;
            }

        }

        /// <summary>
        /// 可直接拷贝要素类图层
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public Layer CopyLayer(Layer layer, string newName)
        {
            string[] options = new string[] { "FEATURE_TYPE=feature" };
            return IsUTF8 ? dataSource.CopyLayerEx(layer, newName, options) : 
                dataSource.CopyLayer(layer, newName, options);
        }

        #endregion

        #region 功能测试模块
        public void CreateShpTest()
        {
            // 创建图层，创建一个多边形图层，这里没有指定空间参考，如果需要的话，需要在这里进行指定
            Layer oLayer =
            IsUTF8? dataSource.CreateLayerEx("TestPolygon", null, wkbGeometryType.wkbPolygon, null) :
                dataSource.CreateLayer("TestPolygon", null, wkbGeometryType.wkbPolygon, null);
            if (oLayer == null)
            {
                Console.WriteLine("图层创建失败！\n");
                return;
            }

            // 下面创建属性表
            // 先创建一个叫FieldID的整型属性
            FieldDefn oFieldID = new FieldDefn("FieldID", FieldType.OFTInteger);
            oLayer.CreateField(oFieldID, 1);

            // 再创建一个叫FeatureName的字符型属性，字符长度为50
            FieldDefn oFieldName = new FieldDefn("FieldName", FieldType.OFTString);
            oFieldName.SetWidth(100);
            oLayer.CreateField(oFieldName, 1);

            FeatureDefn oDefn = oLayer.GetLayerDefn();

            // 创建三角形要素
            Feature oFeatureTriangle = new Feature(oDefn);
            oFeatureTriangle.SetField(0, 0);
            oFeatureTriangle.SetField(1, "三角形");
            Geometry geomTriangle = Geometry.CreateFromWkt("POLYGON ((0 0,20 0,10 15,0 0))");
            oFeatureTriangle.SetGeometry(geomTriangle);
            oLayer.CreateFeature(oFeatureTriangle);

            // 创建矩形要素
            Feature oFeatureRectangle = new Feature(oDefn);
            oFeatureRectangle.SetField(0, 1);
            oFeatureRectangle.SetField(1, "矩形");
            Geometry geomRectangle = Geometry.CreateFromWkt("POLYGON ((30 0,60 0,60 30,30 30,30 0))");
            oFeatureRectangle.SetGeometry(geomRectangle);

            oLayer.CreateFeature(oFeatureRectangle);

            // 创建五角形要素
            Feature oFeaturePentagon = new Feature(oDefn);
            oFeaturePentagon.SetField(0, 2);
            oFeaturePentagon.SetField(1, "五角形");
            Geometry geomPentagon = Geometry.CreateFromWkt("POLYGON ((70 0,85 0,90 15,80 30,65 15,70 0))");
            oFeaturePentagon.SetGeometry(geomPentagon);
            oLayer.CreateFeature(oFeaturePentagon);

            Console.WriteLine("\n数据集创建完成！\n");

        }

        #endregion

    }
}
