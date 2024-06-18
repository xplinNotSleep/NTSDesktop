using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public class RasterSlopeClass
    {
        private FeatureLayer m_featureLayer = new FeatureLayer();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RasterSlopeClass()
        {
        }

        public IRasterLayer GetSelectedRasterLayer(IMap map, string layerName)
        {
            int layerCount = map.LayerCount;
            IRasterLayer selectedLayer = null;
            for (int i = 0; i != layerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is IRasterLayer)
                {
                    IRasterLayer layer2 = (IRasterLayer)layer;
                    if (layer2.Name == layerName)
                    {
                        selectedLayer = layer2;
                    }
                }
            }

            return selectedLayer;
        }

        public IFeatureLayer GetSelectedFeatureLayer(IMap map, string layerName)
        {
            int layerCount = map.LayerCount;
            IFeatureLayer selectedLayer = null;
            for (int i = 0; i != layerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = (IFeatureLayer)layer;
                    if (layer2.Name == layerName && layer2.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        selectedLayer = layer2;
                    }
                }
            }

            return selectedLayer;
        }

        //////////////////////////////////////////////////////////////////////////
        //IRasterLayer rlayer：需要生成坡度图的DEM图层
        //int cellSize：像元尺度大小（正整数；数值越大，精度越低；数值越小，精度越高，运算量越大）
        //////////////////////////////////////////////////////////////////////////
        public IRasterDataset CalcRasterSlope(IRasterLayer rlayer, int cellSize)
        {
            IRasterDataset ipInRastDataset = ((IRaster2)rlayer.Raster).RasterDataset;

            IGeoDataset ipGeoDataIn = ipInRastDataset as IGeoDataset;
            IGeoDataset ipGeoDataOut;
            object zFactor = new object();
            zFactor = System.Reflection.Missing.Value;

            IRasterAnalysisEnvironment ipRastAnalEnv = new RasterSurfaceOp();
            System.Object cSize = cellSize;
            ipRastAnalEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cSize);

            ISurfaceOp ipSurfOp = (ISurfaceOp)ipRastAnalEnv;
            ipGeoDataOut = ipSurfOp.Slope(ipGeoDataIn, ESRI.ArcGIS.GeoAnalyst.esriGeoAnalysisSlopeEnum.esriGeoAnalysisSlopeDegrees, ref zFactor);
            IRasterBandCollection bandCollection = (IRasterBandCollection)ipGeoDataOut;
            IRasterDataset ipOutRasterDataset = bandCollection.Item(0).RasterDataset;

            return ipOutRasterDataset;
        }

        /// <summary>
        /// 栅格坡度图重分类
        /// </summary>
        /// <param name="inRasterDataset">栅格坡度图</param>
        /// <param name="tablePath">分类规则表</param>
        /// <returns></returns>
        public IRasterDataset ReclassSlopeRaster(IRasterDataset inRasterDataset, string tablePath)
        {
            IGeoDataset ipGeoDataIn = inRasterDataset as IGeoDataset;

            IGPUtilities pGPUtilities = new GPUtilities();
            ITable pRemapTable = pGPUtilities.OpenTableFromString(tablePath);

            IReclassOp pReclassOp = new RasterReclassOp() as IReclassOp;
            IGeoDataset ipGeoDataOut = pReclassOp.Reclass(ipGeoDataIn, pRemapTable, "from_", "to", "Out", true);

            IRasterBandCollection bandCollection = (IRasterBandCollection)ipGeoDataOut;
            IRasterDataset ipOutRasterDataset = bandCollection.Item(0).RasterDataset;

            return ipOutRasterDataset;
        }

        public IFeatureClass SlopeRasterToPolygonShp(IRasterDataset inRasterDataset, string shpPath)
        {
            string fileFolderPath = shpPath.Substring(0, shpPath.LastIndexOf("\\") + 1);
            string fileName = shpPath.Substring(shpPath.LastIndexOf("\\") + 1, shpPath.Length - shpPath.LastIndexOf("\\") - 1);

            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pShapeFileWorkspace = pWorkspaceFactory.OpenFromFile(fileFolderPath, 0) as IFeatureWorkspace;

            if (pShapeFileWorkspace == null)
                Console.Write("文件目录不存在！");

            IConversionOp pConversionOp = new ESRI.ArcGIS.GeoAnalyst.RasterConversionOp() as IConversionOp;
            IGeoDataset pInputGeoDataset = inRasterDataset as IGeoDataset;
            string strNamePolygon = fileName;
            IFeatureClass pFeatureClass = pConversionOp.RasterDataToPolygonFeatureData(pInputGeoDataset, pShapeFileWorkspace as IWorkspace, strNamePolygon, true) as IFeatureClass;

            return pFeatureClass;
        }

        public void CalcFeatureClassSlopeLevel(IFeatureClass baseFeatureClass, IFeatureClass tagFeatureClass, string fieldName)
        {
            int index = 0;
            index = tagFeatureClass.FindField(fieldName);
            //判断字段是否已存在，不存在则创建
            if (index == -1)
            {
                IField field = this.CreateField(0, fieldName);
                tagFeatureClass.AddField(field);
                field = null;
                index = tagFeatureClass.FindField(fieldName);
            }
            else if (MessageBox.Show("字段已经存在，覆盖吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            IFeatureCursor cursor = tagFeatureClass.Update(null, true);
            for (IFeature feature = cursor.NextFeature(); feature != null; feature = cursor.NextFeature())
            {
                int featureSlopeLevel = CalcFeatureSlopeLevel(baseFeatureClass, feature);

                feature.set_Value(index, featureSlopeLevel.ToString());
                cursor.UpdateFeature(feature);
            }
        }

        public int CalcFeatureSlopeLevel(IFeatureClass baseFeatureClass, IFeature feature)
        {
            double[] ContainsAreaBySlope;
            ContainsAreaBySlope = new double[5];

            double[] IntersectAreaBySlope;
            IntersectAreaBySlope = new double[5];

            double[] AllAreaBySlope;
            AllAreaBySlope = new double[5];

            ISpatialFilter spatialFilter1 = new SpatialFilterClass();
            spatialFilter1.Geometry = feature.Shape as IGeometry;
            spatialFilter1.GeometryField = baseFeatureClass.ShapeFieldName;
            spatialFilter1.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

            IFeatureCursor cursor = baseFeatureClass.Search(spatialFilter1, true);

            for (IFeature baseFeature = cursor.NextFeature(); baseFeature != null; baseFeature = cursor.NextFeature())
            {
                string slopeLevel = baseFeature.get_Value(baseFeature.Table.FindField("GRIDCODE")).ToString();
                switch (slopeLevel)
                {
                    case "1":
                        ContainsAreaBySlope[0] = ContainsAreaBySlope[0] + getFeatureArea(baseFeature);
                        break;
                    case "2":
                        ContainsAreaBySlope[1] = ContainsAreaBySlope[1] + getFeatureArea(baseFeature);
                        break;
                    case "3":
                        ContainsAreaBySlope[2] = ContainsAreaBySlope[2] + getFeatureArea(baseFeature);
                        break;
                    case "4":
                        ContainsAreaBySlope[3] = ContainsAreaBySlope[3] + getFeatureArea(baseFeature);
                        break;
                    case "5":
                        ContainsAreaBySlope[4] = ContainsAreaBySlope[4] + getFeatureArea(baseFeature);
                        break;
                }
            }

            ISpatialFilter spatialFilter2 = new SpatialFilterClass();
            spatialFilter2.Geometry = feature.Shape as IGeometry;
            spatialFilter2.GeometryField = baseFeatureClass.ShapeFieldName;
            spatialFilter2.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            IFeatureCursor cursor2 = baseFeatureClass.Search(spatialFilter2, true);
            for (IFeature baseFeature = cursor2.NextFeature(); baseFeature != null; baseFeature = cursor2.NextFeature())
            {
                IGeometry baseGeometry = baseFeature.ShapeCopy as IGeometry;
                ITopologicalOperator pTopo = (ITopologicalOperator)feature.Shape;

                pTopo.Simplify();
                IGeometry clipGeo = pTopo.Intersect(baseGeometry, esriGeometryDimension.esriGeometry2Dimension);


                string slopeLevel = baseFeature.get_Value(baseFeature.Table.FindField("GRIDCODE")).ToString();
                switch (slopeLevel)
                {
                    case "1":
                        IntersectAreaBySlope[0] = IntersectAreaBySlope[0] + getGeometryArea(clipGeo);
                        break;
                    case "2":
                        IntersectAreaBySlope[1] = IntersectAreaBySlope[1] + getGeometryArea(clipGeo);
                        break;
                    case "3":
                        IntersectAreaBySlope[2] = IntersectAreaBySlope[2] + getGeometryArea(clipGeo);
                        break;
                    case "4":
                        IntersectAreaBySlope[3] = IntersectAreaBySlope[3] + getGeometryArea(clipGeo);
                        break;
                    case "5":
                        IntersectAreaBySlope[4] = IntersectAreaBySlope[4] + getGeometryArea(clipGeo);
                        break;
                }
            }

            AllAreaBySlope[0] = ContainsAreaBySlope[0] + IntersectAreaBySlope[0];
            AllAreaBySlope[1] = ContainsAreaBySlope[1] + IntersectAreaBySlope[1];
            AllAreaBySlope[2] = ContainsAreaBySlope[2] + IntersectAreaBySlope[2];
            AllAreaBySlope[3] = ContainsAreaBySlope[3] + IntersectAreaBySlope[3];
            AllAreaBySlope[4] = ContainsAreaBySlope[4] + IntersectAreaBySlope[4];

            int featureSlopeLevel = getMaxValueID(AllAreaBySlope) + 1;
            return featureSlopeLevel;
        }

        private double getFeatureArea(IFeature feature)
        {
            IPolygon shape = (IPolygon)feature.Shape;
            IArea area = (IArea)shape;

            return area.Area;
        }

        private double getGeometryArea(IGeometry geometry)
        {
            IPolygon shape = (IPolygon)geometry;
            IArea area = (IArea)shape;

            return area.Area;
        }

        private int getMaxValueID(double[] values)
        {
            int maxValueID = 0;
            double maxValue = values[0];

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                    maxValueID = i;
                }
            }

            return maxValueID;
        }

        private IField CreateField(int i, string strValue)
        {
            IField field = new FieldClass();
            IFieldEdit edit = (IFieldEdit)field;
            edit.Name_2 = strValue;
            if ((i == 0) || (i == 1))
            {
                edit.Type_2 = esriFieldType.esriFieldTypeDouble;
                return field;
            }
            if (i == 2)
            {
                edit.Type_2 = esriFieldType.esriFieldTypeString;
                edit.Length_2 = 50;
            }
            return field;
        }
    }
}
