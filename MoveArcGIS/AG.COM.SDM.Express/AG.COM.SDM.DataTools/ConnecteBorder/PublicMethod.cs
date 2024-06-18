using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    public class PointInfo
    {
        public double x;
        public double y;
    }


    public partial class PublicMethod
    {
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="pObject">IWorkspace或者IFeatureDataset对象</param>
        /// <param name="pName">要素类名称</param>
        /// <param name="pFeatureType">要素类型</param>
        /// <param name="pFields">字段集</param>
        /// <param name="pUidClsId">CLSID值</param>
        /// <param name="pUidClsExt">EXTCLSID值</param>
        /// <param name="pConfigWord">配置信息关键词</param>
        /// <returns>返回IFeatureClass</returns>
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType,
            IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
        {
            try
            {
                #region 错误检测
                if (pObject == null)
                {
                    throw (new Exception("[pObject] 不能为空!"));
                }
                if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
                {
                    throw (new Exception("[pObject] 必须为IFeatureWorkspace 或者 IFeatureDataset"));
                }
                if (pName.Length == 0)
                {
                    throw (new Exception("[pName] 不能为空!"));
                }
                if (pFields == null || pFields.FieldCount == 0)
                {
                    throw (new Exception("[pFields] 不能为空!"));
                }

                //几何对象字段名称
                string strShapeFieldName = "";
                for (int i = 0; i < pFields.FieldCount; i++)
                {
                    if (pFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        strShapeFieldName = pFields.get_Field(i).Name;
                        break;
                    }
                }

                if (strShapeFieldName.Length == 0)
                {
                    throw (new Exception("字段集中找不到几何对象定义"));
                }

                #endregion

                #region pUidClsID字段为空时
                if (pUidClsId == null)
                {
                    pUidClsId = new UIDClass();
                    switch (pFeatureType)
                    {
                        case (esriFeatureType.esriFTSimple):
                            pUidClsId.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                            break;
                        case (esriFeatureType.esriFTSimpleJunction):
                            pUidClsId.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                            break;
                        case (esriFeatureType.esriFTComplexJunction):
                            pUidClsId.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                            break;
                        case (esriFeatureType.esriFTSimpleEdge):
                            pUidClsId.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                            break;
                        case (esriFeatureType.esriFTComplexEdge):
                            pUidClsId.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                            break;
                        case (esriFeatureType.esriFTAnnotation):
                            pUidClsId.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                            break;
                        case (esriFeatureType.esriFTDimension):
                            pUidClsId.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                            break;
                    }
                }
                #endregion

                #region pUidClsExt字段为空时
                if (pUidClsExt == null)
                {
                    switch (pFeatureType)
                    {
                        case esriFeatureType.esriFTAnnotation:
                            pUidClsExt = new UIDClass();
                            pUidClsExt.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                            break;
                        case esriFeatureType.esriFTDimension:
                            pUidClsExt = new UIDClass();
                            pUidClsExt.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                            break;
                    }
                }

                #endregion

                #region 字段集合为空时
                /*
            if (pFields == null)
            {
                //实倒化字段集合对象
                pFields = new FieldsClass();
                IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

                //创建几何对象字段定义
                IGeometryDef tGeometryDef = new GeometryDefClass();
                IGeometryDefEdit tGeometryDefEdit = tGeometryDef as IGeometryDefEdit;

                //指定几何对象字段属性值
                tGeometryDefEdit.GeometryType_2 = pGeometryType;
                tGeometryDefEdit.GridCount_2 = 1;
                tGeometryDefEdit.set_GridSize(0, 1000);
                if (pObject is IWorkspace)
                {
                    tGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                }

                //创建OID字段
                IField fieldOID = new FieldClass();
                IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
                fieldEditOID.Name_2 = "OBJECTID";
                fieldEditOID.AliasName_2 = "OBJECTID";
                fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
                tFieldsEdit.AddField(fieldOID);

                //创建几何字段
                IField fieldShape = new FieldClass();
                IFieldEdit fieldEditShape = fieldShape as IFieldEdit;
                fieldEditShape.Name_2 = "SHAPE";
                fieldEditShape.AliasName_2 = "SHAPE";
                fieldEditShape.Type_2 = esriFieldType.esriFieldTypeGeometry;
                fieldEditShape.GeometryDef_2 = tGeometryDef;
                tFieldsEdit.AddField(fieldShape);
            } */
                #endregion

                IFeatureClass tFeatureClass = null;
                if (pObject is IWorkspace)
                {
                    //创建独立的FeatureClass
                    IWorkspace tWorkspace = pObject as IWorkspace;
                    //查询引用接口
                    IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                    tFeatureClass = tFeatureWorkspace.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
                }
                else if (pObject is IFeatureDataset)
                {
                    //在要素集中创建FeatureClass
                    IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                    tFeatureClass = tFeatureDataset.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
                }

                //查询接口引用
                ISchemaLock tSchemaLock = tFeatureClass as ISchemaLock;
                if (tSchemaLock != null)
                {
                    //更改表的锁定状态
                    tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
                return tFeatureClass;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }

        }

        /// <summary>
        /// 导出源数据到目标要素类
        /// </summary>
        /// <param name="pFromFeatureClass">源要素类</param>
        /// <param name="pToFeatureClass">目标要素类</param>
        /// <param name="pDictFields">匹配规则(key为目标要素类字段索引值 value为源要素类字段索引值)</param>
        /// <param name="pQueryFilter">源要素类过滤条件</param>
        public static void ExportDataToFeatureClass(IFeatureClass pFromFeatureClass, IFeatureClass pToFeatureClass, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter)
        {
            try
            {
                //设置游标状态为插入
                IFeatureCursor tFeatureCursor1 = pToFeatureClass.Insert(true);
                //设置要素缓存
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();

                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(pQueryFilter, false);

                //返回匹配规则枚举数
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();

                for (IFeature tFeature = tFeatureCursor2.NextFeature(); tFeature != null; tFeature = tFeatureCursor2.NextFeature())
                {

                    //设置初始位置
                    tDictEnumerator.Reset();

                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //设置值
                        tFeatureBuffer.set_Value((int)tDictEnumerator.Key, tFeature.get_Value((int)tDictEnumerator.Value));
                    }

                    //设置几何对象
                    tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    //插入记录
                    tFeatureCursor1.InsertFeature(tFeatureBuffer);
                }

                //一次性写入
                tFeatureCursor1.Flush();

                //释放非托管资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //查询接口引用
                ISchemaLock tSchemaLock = pToFeatureClass as ISchemaLock;
                if (tSchemaLock != null)
                {
                    //更改表的锁定状态
                    tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
            }
        }

        /// <summary>
        /// 获取源或目标要素类
        /// </summary>
        /// <returns>返回要素类</returns>
        public static IFeatureClass OpenFeatureClass()
        {
            //实例化数据浏览窗体实例
            AG.COM.SDM.Catalog.IDataBrowser tIDataBrowser = new FormDataBrowser();
            //不可多选
            tIDataBrowser.MultiSelect = false;
            //添加过滤器
            tIDataBrowser.AddFilter(new FeatureClassFilter());

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                if (items.Count > 0)
                {
                    IFeatureClass tFeatureClass = items[0].GetGeoObject() as IFeatureClass;
                    return tFeatureClass;
                }
            }

            return null;
        }

        /// <summary>
        /// 导入源数据到目标要素类
        /// </summary>
        /// <param name="pFromFeatureClass">源要素类</param>
        /// <param name="pToFeatureClass">目标要素类</param>
        /// <param name="pDictFields">匹配规则(key为目标要素类字段索引值 value为源要素类字段索引值)</param>
        /// <param name="pQueryFilter">源要素类过滤条件</param>
        public static void ImportDataToFeatureClass(IFeatureClass pFromFeatureClass, IFeatureClass pToFeatureClass, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter)
        {
            try
            {
                //设置游标状态为插入
                IFeatureCursor tFeatureCursor1 = pToFeatureClass.Insert(true);
                //设置要素缓存
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();

                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(pQueryFilter, false);

                //返回匹配规则枚举数
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();

                for (IFeature tFeature = tFeatureCursor2.NextFeature(); tFeature != null; tFeature = tFeatureCursor2.NextFeature())
                {

                    BeforeImport(tFeature, pToFeatureClass);

                    //设置初始位置
                    tDictEnumerator.Reset();

                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //设置值
                        tFeatureBuffer.set_Value((int)tDictEnumerator.Key, tFeature.get_Value((int)tDictEnumerator.Value));
                    }
                    //设置几何对象
                    tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    //插入记录
                    tFeatureCursor1.InsertFeature(tFeatureBuffer);
                }

                //一次性写入
                tFeatureCursor1.Flush();

                //释放非托管资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //查询接口引用
                ISchemaLock tSchemaLock = pToFeatureClass as ISchemaLock;
                if (tSchemaLock != null)
                {
                    //更改表的锁定状态
                    tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
            }
        }

        /// <summary>
        /// 入库前的预处理
        /// </summary>
        /// <param name="fromFeat"></param>
        /// <param name="toFeatClass"></param>
        public static void BeforeImport(IFeature fromFeat, IFeatureClass toFeatClass)
        {
            if (toFeatClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                ImportRegion(fromFeat, toFeatClass);
            }

        }

        /// <summary>
        /// 面挖空处理
        /// </summary>
        /// <param name="fromFeat"></param>
        /// <param name="toFeatClass"></param>
        public static void ImportRegion(IFeature fromFeat, IFeatureClass toFeatClass)
        {
            try
            {
                IFeatureCursor cur = CreateFeatCurBySpatialFilter(fromFeat, toFeatClass, esriSpatialRelEnum.esriSpatialRelIntersects);
                IGeometry pFromGeo = fromFeat.ShapeCopy;
                if ((pFromGeo as ITopologicalOperator).IsSimple == false)
                    (pFromGeo as ITopologicalOperator).Simplify();
                if (cur != null)
                {
                    IFeature tempFeat = cur.NextFeature();
                    while (tempFeat != null)
                    {
                        ITopologicalOperator topoOperation = tempFeat.Shape as ITopologicalOperator;
                        if (topoOperation.IsSimple == false)
                            topoOperation.Simplify();
                        //获取余集
                        IGeometry geo = topoOperation.Difference(pFromGeo);

                        if (geo != null && !geo.IsEmpty)
                        {
                            tempFeat.Shape = geo;
                            tempFeat.Store();
                        }
                        else
                        {
                            tempFeat.Delete();
                        }
                        tempFeat = cur.NextFeature();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
            }
        }

        /// <summary>
        /// 利用空间过滤得到FEATURE游标
        /// </summary>
        /// <param name="feat">过滤的匹配要素</param>
        /// <param name="featClass">要过滤的要素类</param>
        /// <param name="type">过滤类型</param>
        /// <returns></returns>
        public static IFeatureCursor CreateFeatCurBySpatialFilter(IFeature feat, IFeatureClass featClass, esriSpatialRelEnum type)
        {
            try
            {
                ISpatialFilter sFilter = new SpatialFilterClass();
                sFilter.Geometry = (IGeometry)feat.Shape;
                sFilter.GeometryField = "Shape";
                esriSpatialRelEnum spatialType = type;
                sFilter.SpatialRel = spatialType;
                IFeatureCursor featCur = featClass.Search(sFilter, false);
                return featCur;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 利用空间过滤得到FEATURE游标
        /// </summary>
        /// <param name="feat">过滤的匹配要素</param>
        /// <param name="featClass">要过滤的选择集</param>
        /// <param name="type">过滤类型</param>
        /// <returns></returns>
        public static IFeatureCursor CreateFeatCurBySpatialFilter(IFeature feat, ISelectionSet selectSet, esriSpatialRelEnum type)
        {
            try
            {
                ISpatialFilter sFilter = new SpatialFilterClass();
                sFilter.Geometry = (IGeometry)feat.Shape;
                sFilter.GeometryField = "Shape";
                esriSpatialRelEnum spatialType = type;
                sFilter.SpatialRel = spatialType;
                ICursor Cur = null;
                selectSet.Search(sFilter, false, out Cur);
                return Cur as IFeatureCursor;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 利用空间过滤得到FEATURE游标
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="featClass"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IFeatureCursor CreateFeatCurBySpatialFilter(IGeometry geo, IFeatureClass featClass, esriSpatialRelEnum type)
        {
            try
            {
                ISpatialFilter sFilter = new SpatialFilterClass();
                sFilter.Geometry = geo;
                sFilter.GeometryField = "Shape";
                esriSpatialRelEnum spatialType = type;
                sFilter.SpatialRel = spatialType;
                IFeatureCursor featCur = featClass.Search(sFilter, false);
                return featCur;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 判断打开的数据是不是面
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsRegionClass(object obj)
        {
            if (obj is IFeatureClass)
            {
                IFeatureClass featClass = obj as IFeatureClass;
                if (featClass.ShapeType == esriGeometryType.esriGeometryPolygon) return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 分割面
        /// </summary>
        /// <param name="p_SplitFeat">分割的要素</param>
        /// <param name="featClass">被分割的要素类</param>
        public static void SplitRegion(IFeature p_SplitFeat, IFeatureClass featClass)
        {
            try
            {
                //得到p_SplitFeat与featClass重叠的要素
                IFeatureCursor cur = CreateFeatCurBySpatialFilter(p_SplitFeat, featClass, esriSpatialRelEnum.esriSpatialRelOverlaps);
                if (cur != null)
                {
                    IPolyline polyline = null;

                    if (p_SplitFeat.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        //面转线
                        polyline = GetLineFromPolygon(p_SplitFeat);
                    }
                    else if (p_SplitFeat.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        polyline = p_SplitFeat.Shape as IPolyline;
                    }
                    if (polyline == null) return;

                    IFeature tempFeat = cur.NextFeature();
                    while (tempFeat != null)
                    {
                        //面切割
                        IFeatureEdit featEdit = tempFeat as IFeatureEdit;
                        featEdit.Split(polyline as IGeometry);

                        tempFeat = cur.NextFeature();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.StackTrace);
            }
        }

        /// <summary>
        /// 面转线
        /// </summary>
        /// <param name="feat"></param>
        /// <returns></returns>
        public static IPolyline GetLineFromPolygon(IFeature feat)
        {
            try
            {
                if (feat.Shape.GeometryType != esriGeometryType.esriGeometryPolygon) return null;
                IGeometry geo = feat.Shape;
                IPolygon polygon = geo as IPolygon;
                if (polygon == null) return null;
                IPointCollection pointColl = polygon as IPointCollection;
                IPointCollection pointCollLine = new PolylineClass();
                for (int i = 0; i < pointColl.PointCount; i++)
                {
                    IPoint p = pointColl.get_Point(i);
                    object missing = System.Reflection.Missing.Value;
                    pointCollLine.AddPoint(p, ref missing, ref missing);
                }
                IPolyline line = pointCollLine as IPolyline;
                return line;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

        /// <summary>
        /// 线转面
        /// </summary>
        /// <param name="feat"></param>
        /// <returns></returns>
        public static IGeometry GetPolygonFromLine(IGeometry geo)
        {
            try
            {
                IPointCollection pointColl = geo as IPointCollection;
                IPointCollection pointCollPolygon = new PolygonClass();
                object missing = System.Reflection.Missing.Value;
                for (int i = 0; i < pointColl.PointCount; i++)
                {
                    IPoint p = pointColl.get_Point(i);
                    pointCollPolygon.AddPoint(p, ref missing, ref missing);
                }
                // 封闭
                if (pointColl.PointCount > 0)
                    pointCollPolygon.AddPoint(pointColl.get_Point(0), ref missing, ref missing);
                return pointCollPolygon as IGeometry;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

    }
}
