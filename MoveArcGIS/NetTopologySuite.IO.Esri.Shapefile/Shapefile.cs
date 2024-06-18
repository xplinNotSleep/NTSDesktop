﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NetTopologySuite.Features;
using NetTopologySuite.Features.Fields;
using NetTopologySuite.Features.Helpers;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Esri.Shapefiles.Readers;
using NetTopologySuite.IO.Esri.Shapefiles.Writers;
using NetTopologySuite.IO.Esri.Shp;

namespace NetTopologySuite.IO.Esri
{


    /// <summary>
    /// Base shapefile class.
    /// </summary>
    public abstract class Shapefile : ManagedDisposable
    {
        internal const int FileCode = 9994; // 0x0000270A; 
        internal const int Version = 1000;

        internal const int FileHeaderSize = 100;
        internal const int RecordHeaderSize = 2 * sizeof(int);


        /// <summary>
        /// Minimal Measure value considered as not "no-data".
        /// </summary>
        /// <remarks>
        /// Any floating point number smaller than –10E38 is considered by a shapefile reader
        /// to represent a "no data" value. This rule is used only for measures (M values).
        /// <br />
        /// http://www.esri.com/library/whitepapers/pdfs/shapefile.pdf (page 2, bottom)
        /// </remarks>
        internal const double MeasureMinValue = -10e38;

        /// <summary>
        /// Shape type.
        /// </summary>
        public abstract ShapeType ShapeType { get; }


        #region Static methods

        /// <summary>
        /// Reads shape type information from SHP stream.
        /// </summary>
        /// <param name="shpStream">SHP file stream.</param>
        /// <returns>Shape type.</returns>
        internal static ShapeType GetShapeType(Stream shpStream)
        {
            if (shpStream == null)
            {
                throw new ArgumentNullException("Uninitialized SHP stream.", nameof(shpStream));
            }

            shpStream.Position = 0;
            var fileCode = shpStream.ReadInt32BigEndian();
            if (fileCode != Shapefile.FileCode)
                throw new ShapefileException("Invalid shapefile format.");

            shpStream.Advance(28);
            return shpStream.ReadShapeType();
        }


        /// <summary>
        /// Gets default <see cref="ShapeType"/> for specified geometry.
        /// </summary>
        /// <param name="geometry">A Geometry object.</param>
        /// <returns>Shape type.</returns>
        public static ShapeType GetShapeType(Geometry geometry)
        {
            return geometry.GetShapeType();
        }

        /// <summary>
        /// Reads shape type information from SHP file.
        /// </summary>
        /// <param name="shpPath">Path to SHP file.</param>
        /// <returns>Shape type.</returns>
        public static ShapeType GetShapeType(string shpPath)
        {
            shpPath = Path.ChangeExtension(shpPath, ".shp");
            using (var shpStream = new FileStream(shpPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return GetShapeType(shpStream);
            }
        }

        /// <summary>
        /// Opens shapefile reader.
        /// </summary>
        /// <param name="shpPath">Path to shapefile.</param>
        /// <param name="options">Reader options.</param>
        /// <returns>Shapefile reader.</returns>
        public static ShapefileReader OpenRead(string shpPath, ShapefileReaderOptions options = null)
        {
            var shapeType = GetShapeType(shpPath);

            if (shapeType.IsPoint())
            {
                return new ShapefilePointReader(shpPath, options);
            }
            else if (shapeType.IsMultiPoint())
            {
                return new ShapefileMultiPointReader(shpPath, options);
            }
            else if (shapeType.IsPolyLine())
            {
                return new ShapefilePolyLineReader(shpPath, options);
            }
            else if (shapeType.IsPolygon())
            {
                return new ShapefilePolygonReader(shpPath, options);
            }
            else
            {
                throw new ShapefileException("Unsupported shapefile type: " + shapeType, shpPath);
            }
        }

        public static ShapefileReader OpenRead(string shpPath, string backFieldNames, string whereClause, Encoding Encoding)
        {
            var shapeType = GetShapeType(shpPath);

            if (shapeType.IsPoint())
            {
                return new ShapefilePointReader(shpPath, backFieldNames, whereClause, Encoding);
            }
            else if (shapeType.IsMultiPoint())
            {
                return new ShapefileMultiPointReader(shpPath, backFieldNames, whereClause, Encoding);
            }
            else if (shapeType.IsPolyLine())
            {
                return new ShapefilePolyLineReader(shpPath, backFieldNames, whereClause, Encoding);
            }
            else if (shapeType.IsPolygon())
            {
                return new ShapefilePolygonReader(shpPath, backFieldNames, whereClause, Encoding);
            }
            else
            {
                throw new ShapefileException("Unsupported shapefile type: " + shapeType, shpPath);
            }
        }

        /// <summary>
        /// 获取图层中的所有字段
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static DbfField[] GetShpDbfFields(string layerName)
        {
            DbfField[] dbfFields = null;
            using (var dbfReader = new DbfReader(layerName))
            {
                DbfFieldCollection dbfColFields = dbfReader.Fields;
                dbfFields = new DbfField[dbfColFields.Count];
                for (int i = 0; i < dbfColFields.Count; i++)
                {
                    var dbfColField = dbfColFields[i];
                    var type = dbfColField.FieldType;
                    var name = dbfColField.Name;
                    dbfFields[i] = DbfField.Create(name, type);
                }
            }
            return dbfFields;
        }

        /// <summary>
        /// 获取图层中的除几何外的所有字段
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static DbfField[] GetShpDbfFieldsOutGeo(string layerName)
        {
            DbfField[] dbfFields = null;
            using (var dbfReader = new DbfReader(layerName))
            {
                DbfFieldCollection dbfColFields = dbfReader.Fields;
                dbfFields = new DbfField[dbfColFields.Count];
                for (int i = 0; i < dbfColFields.Count; i++)
                {
                    var dbfColField = dbfColFields[i];
                    var type = dbfColField.FieldType;
                    var name = dbfColField.Name;
                    if(type != DbfType.Shape)
                    {
                        dbfFields[i] = DbfField.Create(name, type);
                    }
                }
            }
            return dbfFields;
        }

        /// <summary>
        /// 获取shp图层中的几何字段并设置空间参考信息，用于入库
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static string GetShpGeoType(string layerName)
        {
            //获取shp图层的几何类型
            ShapeType shapeType = GetShapeType(layerName);
            string geoType = ConvertGeoType(shapeType);
            return geoType;
            
        }

        public static string ConvertGeoType(ShapeType shapeType)
        {
            string geoType = "";
            if(shapeType.IsPoint())
            {
                geoType = "Point";
            }
            if (shapeType.IsPolyLine())
            {
                geoType = "MultiLineString";
            }
            if (shapeType.IsMultiPoint())
            {
                geoType = "MultiPoint";
            }
            if (shapeType.IsPolygon())
            {
                geoType = "Polygon";
            }
            #region
            //switch(shapeType)
            //{
            //    case ShapeType.Point:
            //        break;
            //    case ShapeType.:
            //        geoType = "Point";
            //        break;
            //    case ShapeType.Point:
            //        geoType = "Point";
            //        break;
            //    case ShapeType.Point:
            //        geoType = "Point";
            //        break;
            //    case ShapeType.Point:
            //        geoType = "Point";
            //        break;
            //    case ShapeType.Point:
            //        geoType = "Point";
            //        break;
            //    default:
            //        break;
            //}
            #endregion
            return geoType;
        }

        /// <summary>
        /// Reads all features from shapefile.
        /// </summary>
        /// <param name="shpPath">Path to shapefile.</param>
        /// <param name="options">Reader options.</param>
        /// <returns>Shapefile features.</returns>
        public static Feature[] ReadAllFeatures(string shpPath, ShapefileReaderOptions options = null)
        {
            using (var shp = OpenRead(shpPath, options))
            {
                return shp.ToArray();
            }
        }

        public static Feature[] ReadAllFeatures(string shpPath, string backFieldNames = null, string whereClause=null, Encoding Encoding=null)
        {
            using (var shp = OpenRead(shpPath, backFieldNames,whereClause,Encoding))
            {
                return shp.ToArray();
            }
        }

        public static IList<IDictionary<string, object>> GetAttritubes(string layName, string backFieldNames = null, string whereClause = null)
        {
            using (var dbReader = new DbfReader(Path.ChangeExtension(layName, ".dbf")))
            {
                var fields = dbReader.Fields;
                List<string> listBackFieldNames = null;
                if (!string.IsNullOrEmpty(backFieldNames))
                {
                    listBackFieldNames = backFieldNames.ToLower().Split(new char[] { ',' }).ToList();
                }
                IList<IDictionary<string, object>> attrs = new List<IDictionary<string, object>>();
                foreach (var attr in dbReader)
                {
                    IDictionary<string, object> fieldValues = new Dictionary<string, object>();
                    if (listBackFieldNames != null)
                    {
                        foreach (var fieldName in listBackFieldNames)
                        {
                            var fieldName2 = fieldName.Trim();
                            fieldValues.Add(fieldName2, attr[fieldName2]);
                        }
                    }
                    else
                    {
                        foreach (var field in fields)
                        {
                            fieldValues.Add(field.Name, attr[field.Name]);
                        }
                    }

                    attrs.Add(fieldValues);
                }
                return attrs;
            }
        }

        /// <summary>
        /// Reads all geometries from SHP file.
        /// </summary>
        /// <param name="shpPath">Path to SHP file.</param>
        /// <param name="options">Reader options.</param>
        /// <returns>Shapefile geometries.</returns>
        public static Geometry[] ReadAllGeometries(string shpPath, ShapefileReaderOptions options = null)
        {
            shpPath = Path.ChangeExtension(shpPath, ".shp");
            using (var shpStream = File.OpenRead(shpPath))
            {
                var shp = Shp.Shp.OpenRead(shpStream, options);
                return shp.ToArray();
            }
        }

        public static Dictionary<DbfField, object> getShpFieldInfo(DbfField[] fields, Feature feature)
        {
            Dictionary<DbfField, object> dicShpFieldInfo =
                new Dictionary<DbfField, object>();
            IAttributesTable attr = feature.Attributes;
            foreach (DbfField dbfField in fields)
            {
                string fieldName = dbfField.Name;
                object value = attr.GetOptionalValue(fieldName);
                if(value == null)
                {
                    value = "";
                }
                else if (string.IsNullOrEmpty(value.ToString()))
                {
                    value = "";
                }
                dicShpFieldInfo.Add(dbfField, value);
            }
            return dicShpFieldInfo;
        }

        /// <summary>
        /// Opens shapefile writer.
        /// </summary>
        /// <param name="shpPath">Path to shapefile.</param>
        /// <param name="options">Writer options.</param>
        /// <returns>Shapefile writer.</returns>
        public static ShapefileWriter OpenWrite(string shpPath, ShapefileWriterOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));
            if (options.ShapeType.IsPoint())
            {
                return new ShapefilePointWriter(shpPath, options);
            }
            else if (options.ShapeType.IsMultiPoint())
            {
                return new ShapefileMultiPointWriter(shpPath, options);
            }
            else if (options.ShapeType.IsPolyLine())
            {
                return new ShapefilePolyLineWriter(shpPath, options);
            }
            else if (options.ShapeType.IsPolygon())
            {
                return new ShapefilePolygonWriter(shpPath, options);
            }
            else
            {
                throw new ShapefileException("Unsupported shapefile type: " + options.ShapeType, shpPath);
            }
        }

        public static void WriteAllFeatures(IEnumerable<IFeature> features,
            DbfField[] dbfFields, string shpPath, string projection = null, Encoding encoding = null)
        {
            if (features == null)
                throw new ArgumentNullException(nameof(features));

            var firstFeature = features.FirstOrDefault();
            if (firstFeature == null)
                throw new ArgumentException(nameof(ShapefileWriter) + " requires at least one feature to be written.");

            //var fields = firstFeature.Attributes.GetDbfFields();
            var fields = dbfFields;
            var shapeType = features.FindNonEmptyGeometry().GetShapeType();
            var options = new ShapefileWriterOptions(shapeType, fields)
            {
                Projection = projection,
                Encoding = encoding
            };

            using (var shpWriter = OpenWrite(shpPath, options))
            {
                shpWriter.Write(features);
            }
        }


        /// <summary>
        /// Writes features to the shapefile.
        /// </summary>
        /// <param name="features">Features to be written.</param>
        /// <param name="shpPath">Path to shapefile.</param>
        /// <param name="projection">Projection metadata for the shapefile (content of the PRJ file).</param>
        /// <param name="encoding">DBF file encoding (if not set UTF8 is used).</param>
        public static void WriteAllFeatures(IEnumerable<IFeature> features, string shpPath, string projection = null, Encoding encoding = null)
        {
            if (features == null)
                throw new ArgumentNullException(nameof(features));

            var firstFeature = features.FirstOrDefault();
            if (firstFeature == null)
                throw new ArgumentException(nameof(ShapefileWriter) + " requires at least one feature to be written.");

            var fields = firstFeature.Attributes.GetDbfFields();
            var shapeType = features.FindNonEmptyGeometry().GetShapeType();
            var options = new ShapefileWriterOptions(shapeType, fields)
            {
                Projection = projection,
                Encoding = encoding
            };

            using (var shpWriter = OpenWrite(shpPath, options))
            {
                shpWriter.Write(features);
            }
        }

        #endregion

    }

}
