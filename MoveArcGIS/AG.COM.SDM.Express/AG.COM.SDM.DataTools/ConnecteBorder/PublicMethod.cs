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
        /// ����Ҫ����
        /// </summary>
        /// <param name="pObject">IWorkspace����IFeatureDataset����</param>
        /// <param name="pName">Ҫ��������</param>
        /// <param name="pFeatureType">Ҫ������</param>
        /// <param name="pFields">�ֶμ�</param>
        /// <param name="pUidClsId">CLSIDֵ</param>
        /// <param name="pUidClsExt">EXTCLSIDֵ</param>
        /// <param name="pConfigWord">������Ϣ�ؼ���</param>
        /// <returns>����IFeatureClass</returns>
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType,
            IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
        {
            try
            {
                #region ������
                if (pObject == null)
                {
                    throw (new Exception("[pObject] ����Ϊ��!"));
                }
                if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
                {
                    throw (new Exception("[pObject] ����ΪIFeatureWorkspace ���� IFeatureDataset"));
                }
                if (pName.Length == 0)
                {
                    throw (new Exception("[pName] ����Ϊ��!"));
                }
                if (pFields == null || pFields.FieldCount == 0)
                {
                    throw (new Exception("[pFields] ����Ϊ��!"));
                }

                //���ζ����ֶ�����
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
                    throw (new Exception("�ֶμ����Ҳ������ζ�����"));
                }

                #endregion

                #region pUidClsID�ֶ�Ϊ��ʱ
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

                #region pUidClsExt�ֶ�Ϊ��ʱ
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

                #region �ֶμ���Ϊ��ʱ
                /*
            if (pFields == null)
            {
                //ʵ�����ֶμ��϶���
                pFields = new FieldsClass();
                IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

                //�������ζ����ֶζ���
                IGeometryDef tGeometryDef = new GeometryDefClass();
                IGeometryDefEdit tGeometryDefEdit = tGeometryDef as IGeometryDefEdit;

                //ָ�����ζ����ֶ�����ֵ
                tGeometryDefEdit.GeometryType_2 = pGeometryType;
                tGeometryDefEdit.GridCount_2 = 1;
                tGeometryDefEdit.set_GridSize(0, 1000);
                if (pObject is IWorkspace)
                {
                    tGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                }

                //����OID�ֶ�
                IField fieldOID = new FieldClass();
                IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
                fieldEditOID.Name_2 = "OBJECTID";
                fieldEditOID.AliasName_2 = "OBJECTID";
                fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
                tFieldsEdit.AddField(fieldOID);

                //���������ֶ�
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
                    //����������FeatureClass
                    IWorkspace tWorkspace = pObject as IWorkspace;
                    //��ѯ���ýӿ�
                    IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                    tFeatureClass = tFeatureWorkspace.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
                }
                else if (pObject is IFeatureDataset)
                {
                    //��Ҫ�ؼ��д���FeatureClass
                    IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                    tFeatureClass = tFeatureDataset.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
                }

                //��ѯ�ӿ�����
                ISchemaLock tSchemaLock = tFeatureClass as ISchemaLock;
                if (tSchemaLock != null)
                {
                    //���ı������״̬
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
        /// ����Դ���ݵ�Ŀ��Ҫ����
        /// </summary>
        /// <param name="pFromFeatureClass">ԴҪ����</param>
        /// <param name="pToFeatureClass">Ŀ��Ҫ����</param>
        /// <param name="pDictFields">ƥ�����(keyΪĿ��Ҫ�����ֶ�����ֵ valueΪԴҪ�����ֶ�����ֵ)</param>
        /// <param name="pQueryFilter">ԴҪ�����������</param>
        public static void ExportDataToFeatureClass(IFeatureClass pFromFeatureClass, IFeatureClass pToFeatureClass, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter)
        {
            try
            {
                //�����α�״̬Ϊ����
                IFeatureCursor tFeatureCursor1 = pToFeatureClass.Insert(true);
                //����Ҫ�ػ���
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();

                //��ȡԴ�ļ�Ҫ����ĵļ�¼���α�
                IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(pQueryFilter, false);

                //����ƥ�����ö����
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();

                for (IFeature tFeature = tFeatureCursor2.NextFeature(); tFeature != null; tFeature = tFeatureCursor2.NextFeature())
                {

                    //���ó�ʼλ��
                    tDictEnumerator.Reset();

                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //����ֵ
                        tFeatureBuffer.set_Value((int)tDictEnumerator.Key, tFeature.get_Value((int)tDictEnumerator.Value));
                    }

                    //���ü��ζ���
                    tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    //�����¼
                    tFeatureCursor1.InsertFeature(tFeatureBuffer);
                }

                //һ����д��
                tFeatureCursor1.Flush();

                //�ͷŷ��й���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //��ѯ�ӿ�����
                ISchemaLock tSchemaLock = pToFeatureClass as ISchemaLock;
                if (tSchemaLock != null)
                {
                    //���ı������״̬
                    tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
            }
        }

        /// <summary>
        /// ��ȡԴ��Ŀ��Ҫ����
        /// </summary>
        /// <returns>����Ҫ����</returns>
        public static IFeatureClass OpenFeatureClass()
        {
            //ʵ���������������ʵ��
            AG.COM.SDM.Catalog.IDataBrowser tIDataBrowser = new FormDataBrowser();
            //���ɶ�ѡ
            tIDataBrowser.MultiSelect = false;
            //��ӹ�����
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
        /// ����Դ���ݵ�Ŀ��Ҫ����
        /// </summary>
        /// <param name="pFromFeatureClass">ԴҪ����</param>
        /// <param name="pToFeatureClass">Ŀ��Ҫ����</param>
        /// <param name="pDictFields">ƥ�����(keyΪĿ��Ҫ�����ֶ�����ֵ valueΪԴҪ�����ֶ�����ֵ)</param>
        /// <param name="pQueryFilter">ԴҪ�����������</param>
        public static void ImportDataToFeatureClass(IFeatureClass pFromFeatureClass, IFeatureClass pToFeatureClass, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter)
        {
            try
            {
                //�����α�״̬Ϊ����
                IFeatureCursor tFeatureCursor1 = pToFeatureClass.Insert(true);
                //����Ҫ�ػ���
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();

                //��ȡԴ�ļ�Ҫ����ĵļ�¼���α�
                IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(pQueryFilter, false);

                //����ƥ�����ö����
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();

                for (IFeature tFeature = tFeatureCursor2.NextFeature(); tFeature != null; tFeature = tFeatureCursor2.NextFeature())
                {

                    BeforeImport(tFeature, pToFeatureClass);

                    //���ó�ʼλ��
                    tDictEnumerator.Reset();

                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //����ֵ
                        tFeatureBuffer.set_Value((int)tDictEnumerator.Key, tFeature.get_Value((int)tDictEnumerator.Value));
                    }
                    //���ü��ζ���
                    tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    //�����¼
                    tFeatureCursor1.InsertFeature(tFeatureBuffer);
                }

                //һ����д��
                tFeatureCursor1.Flush();

                //�ͷŷ��й���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //��ѯ�ӿ�����
                ISchemaLock tSchemaLock = pToFeatureClass as ISchemaLock;
                if (tSchemaLock != null)
                {
                    //���ı������״̬
                    tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
            }
        }

        /// <summary>
        /// ���ǰ��Ԥ����
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
        /// ���ڿմ���
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
                        //��ȡ�༯
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
        /// ���ÿռ���˵õ�FEATURE�α�
        /// </summary>
        /// <param name="feat">���˵�ƥ��Ҫ��</param>
        /// <param name="featClass">Ҫ���˵�Ҫ����</param>
        /// <param name="type">��������</param>
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
        /// ���ÿռ���˵õ�FEATURE�α�
        /// </summary>
        /// <param name="feat">���˵�ƥ��Ҫ��</param>
        /// <param name="featClass">Ҫ���˵�ѡ��</param>
        /// <param name="type">��������</param>
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
        /// ���ÿռ���˵õ�FEATURE�α�
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
        /// �жϴ򿪵������ǲ�����
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
        /// �ָ���
        /// </summary>
        /// <param name="p_SplitFeat">�ָ��Ҫ��</param>
        /// <param name="featClass">���ָ��Ҫ����</param>
        public static void SplitRegion(IFeature p_SplitFeat, IFeatureClass featClass)
        {
            try
            {
                //�õ�p_SplitFeat��featClass�ص���Ҫ��
                IFeatureCursor cur = CreateFeatCurBySpatialFilter(p_SplitFeat, featClass, esriSpatialRelEnum.esriSpatialRelOverlaps);
                if (cur != null)
                {
                    IPolyline polyline = null;

                    if (p_SplitFeat.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        //��ת��
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
                        //���и�
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
        /// ��ת��
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
        /// ��ת��
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
                // ���
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
