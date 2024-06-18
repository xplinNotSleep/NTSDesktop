using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// ��ת����
    /// </summary>
    public class StreamConvert
    {
        /// <summary>
        /// ��AE���͵��ڴ���ת��Ϊ�ֽ���
        /// </summary>
        /// <param name="mbs">AE���͵��ڴ���</param>
        /// <returns>�����ֽ���</returns>
        public static Stream ConvertAEStream2Stream(IMemoryBlobStream mbs)
        {
            object obj; 
            ((IMemoryBlobStreamVariant)mbs).ExportToVariant(out obj);

            byte[] bytes = (byte[])obj;

            System.IO.Stream strm = new System.IO.MemoryStream();
            strm.Write(bytes, 0, bytes.Length);
            strm.Position = 0;
            strm.Flush();

            return strm; 
        }

        /// <summary>
        /// ���ֽ���ת��ΪAE���õ��ֽ���
        /// </summary>
        /// <param name="stream">�ֽ���</param>
        /// <returns>���ض������ڴ���</returns>
        public static IMemoryBlobStream ConvertStream2AEStream(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            IMemoryBlobStreamVariant mbs = new MemoryBlobStreamClass();
            ((IMemoryBlobStreamVariant)mbs).ImportFromVariant(bytes);       

            return mbs as IMemoryBlobStream;
        }

        /// <summary>
        /// ��8λ�޷���������ת��Ϊʮ�����Ƶ��ַ���
        /// </summary>
        /// <param name="bts">�ֽ�����</param>
        /// <returns>�����ַ���</returns>
        public static string ConvertBytes2HexText(byte[] bts)
        {
            //ʵ�����ɱ��ַ�����
            StringBuilder str = new StringBuilder();
            
            for (int i = 0; i <= bts.Length - 1; i++)
            {
                string s = bts[i].ToString("X");

                if (s.Length == 0)
                    str.Append("00");
                else if (s.Length == 1)
                    str.Append("0");

                str.Append(s);
            }

            return str.ToString();
        }

        /// <summary>
        /// ת���ı�����Ϊ�ֽ��������
        /// </summary>
        /// <param name="text">�ı�����</param>
        /// <returns>�����ֽ��������</returns>
        public static byte[] ConvertHexText2Bytes(string text)
        {
            byte[] bts = new byte[text.Length / 2];

            string str;

            for (int i = 0; i <= text.Length - 1; i = i + 2)
            {
                str = text[i].ToString() + text[i + 1].ToString();
                bts[i / 2] = byte.Parse(str, System.Globalization.NumberStyles.HexNumber);
            }
            return bts;
        }

        /// <summary>
        /// ��ָ�����ַ���ת��ΪAE���͵��ڴ���
        /// </summary>
        /// <param name="text">ָ�����ַ���</param>
        /// <returns>�ڴ���</returns>
        public static IMemoryBlobStream ConvertString2AEStream(string text)
        {
            //��ʮ�����Ƶ��ı�����ת��Ϊ�ֽ�����
            byte[] bts = ConvertHexText2Bytes(text);

            using (Stream stream = new MemoryStream(bts))
            {
                //���ֽڵ�ת��ΪAE���õ��ڴ���
                return ConvertStream2AEStream(stream);
            }
        }

        /// <summary>
        /// ת���������ڴ���Ϊ�ַ���
        /// </summary>
        /// <param name="mstream">�������ڴ�������</param>
        /// <returns>�����ַ���</returns>
        public static string ConvertAEStream2String(IMemoryBlobStream mstream)
        {
            using (Stream stream = ConvertAEStream2Stream(mstream))
            {
                byte[] bts = new byte[stream.Length];
                stream.Read(bts, 0, bts.Length);

                //���ֽ���ת��Ϊʮ�����Ƶ��ı�����
                return ConvertBytes2HexText(bts);
            }
        }

        /// <summary>
        /// ת�����ݼ�����Ϊ�ַ���
        /// </summary>
        /// <param name="pDataset">���ݼ�����</param>
        /// <returns>�ַ���</returns>
        public static string ConvertDataset2String(IDataset pDataset)
        {
            if (pDataset == null)  return "";

            int wsType = -1;
            int dataType = -1;

            if (pDataset is IFeatureClass)
                dataType = 0;
            else if (pDataset is ITable)
                dataType = 1;
            else if (pDataset is ITopology)
                dataType = 2;

            IWorkspace ws = pDataset.Workspace;
            if (ws.Type == esriWorkspaceType.esriFileSystemWorkspace)
                wsType = 0;
            else if (ws.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
                if (System.IO.Path.GetExtension(ws.PathName).Trim().ToLower() == ".mdb")
                    wsType = 1;
                else if (System.IO.Path.GetExtension(ws.PathName).Trim().ToLower() == ".gdb")
                    wsType = 2;
            }
            else
                wsType = 3;

            string dsName = "";
            if (pDataset is IFeatureClass)
            {
                if ((pDataset as IFeatureClass).FeatureDataset != null)
                {
                    dsName = (pDataset as IFeatureClass).FeatureDataset.Name;
                }
            }
            else if (pDataset is ITopology)
            {
                if ((pDataset as ITopology).FeatureDataset != null)
                {
                    dsName = (pDataset as ITopology).FeatureDataset.Name;
                }
            }

            if (dsName.Length == 0) dsName = "0";
            string dataName = pDataset.Name;
            string wsConProp = "";

            IPersistStream pstream = ws.ConnectionProperties as IPersistStream;
            IObjectStream objStream = new ObjectStreamClass();

            try
            {
                objStream.Stream = new MemoryBlobStreamClass();
                pstream.Save(objStream, 0);
                IMemoryBlobStream mstream = objStream.Stream as IMemoryBlobStream;
                //ת���������ڴ���Ϊ�ַ���
                wsConProp = ConvertAEStream2String(mstream);
            }
            catch
            {
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objStream);
            }

            string splitstr = "@@@@@@@@@@";
            return wsType.ToString() + splitstr + dataType.ToString() + splitstr + wsConProp + splitstr + dsName + splitstr + dataName;
        }

        /// <summary>
        /// ת�����ݼ��ַ���Ϊ���ݼ�
        /// </summary>
        /// <param name="strDataset">���ݼ��ַ���</param>
        /// <param name="DataType">��������</param>
        /// <returns>�������ݼ�</returns>
        public static IDataset ConvertString2Dataset(string strDataset, ref int DataType)
        {
            int wsType = -1;
            int dataType = -1;
            string wsName = "";
            string dsName = "";
            string dataName = "";
            string splitstr = "@@@@@@@@@@";
            string[] s = strDataset.Split(splitstr.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (s.Length != 5) return null;
            if (int.TryParse(s[0], out wsType) == false) return null;
            if (int.TryParse(s[1], out dataType) == false) return null;

            wsType = int.Parse(s[0]);
            dataType = int.Parse(s[1]);
            wsName = s[2];
            dsName = s[3].Trim();
            dataName = s[4];

            DataType = dataType;

            IMemoryBlobStream mstream = ConvertString2AEStream(wsName);
            if (mstream == null) return null;

            IObjectStream objstream = new ObjectStreamClass();
            objstream.Stream = mstream;

            IPropertySet propset = new PropertySetClass();
            (propset as IPersistStream).Load(objstream);
            if (propset == null) return null;

            IWorkspaceFactory wsf = null;
            if (wsType == 0)
                wsf = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
            else if (wsType == 1)
                wsf = new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass();
            else if (wsType == 2)
                wsf = new ESRI.ArcGIS.DataSourcesGDB.FileGDBWorkspaceFactoryClass();
            else if (wsType == 3)
                wsf = new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass();
            else
                return null;

            try
            {
                IWorkspace ws = wsf.Open(propset, 0);
                if (ws == null) return null;

                object obj = null;
                if ((dsName.Length == 0) || (dsName == "0"))
                {
                    if (dataType == 0)
                        obj = (ws as IFeatureWorkspace).OpenFeatureClass(dataName);
                    else if (dataType == 1)
                        obj = (ws as IFeatureWorkspace).OpenTable(dataName);
                    else if (dataType == 2)
                        obj = (ws as ITopologyWorkspace).OpenTopology(dataName);
                }
                else
                {
                    IFeatureDataset fds = (ws as IFeatureWorkspace).OpenFeatureDataset(dsName);
                    if (fds == null) return null;
                    if (dataType == 0)
                        obj = (fds as IFeatureClassContainer).get_ClassByName(dataName);
                    else if (dataType == 1)
                        return null; //table����λ��featuredataset��
                    else if (dataType == 2)
                        obj = (fds as ITopologyContainer).get_TopologyByName(dataName);
                }
                return obj as IDataset;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mstream);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objstream);
            }
        }

        /// <summary>
        /// ת��������Ϊ�ַ���
        /// </summary>
        /// <param name="pStream">������</param>
        /// <returns>�ַ���</returns>
        public static string ConvertPersistStream2String(IPersistStream pStream)
        {
            if (pStream == null) return "";
            string strStream = "";

            IObjectStream objStream = new ObjectStreamClass();
            try
            {
                objStream.Stream = new MemoryBlobStreamClass();

                //ʵ�������Զ�����
                IPropertySet pPropSet = new PropertySetClass();
                pPropSet.SetProperty("key", pStream);
                (pPropSet as IPersistStream).Save(objStream, 0);

                //ʵ�����������ڴ�������
                IMemoryBlobStream mstream = objStream.Stream as IMemoryBlobStream;
                strStream = ConvertAEStream2String(mstream);
            }
            catch
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objStream);
            }

            return strStream;
        }

        /// <summary>
        /// ���ַ���ת��Ϊ������
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <param name="keyName">��ֵ</param>
        /// <returns>����������</returns>
        public static IPersistStream ConvertString2PersistStream(string text,string keyName)
        {
            if (text == null || text.Length == 0) return null;

            IMemoryBlobStream mstream = ConvertString2AEStream(text);
            if (mstream == null)  return null;

            IPersistStream pCloneStream=null;
            IObjectStream objstream = new ObjectStreamClass();

            try
            {
                objstream.Stream = mstream;

                IPropertySet pPropSet = new PropertySetClass();
                (pPropSet as IPersistStream).Load(objstream);

                if (pPropSet.Count == 0) return null;
                pCloneStream = pPropSet.GetProperty(keyName) as IPersistStream;
            }
            catch
            {
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objstream);
            }

            return pCloneStream;
        }

        /// <summary>
        /// ���ַ���ת��ΪIPropertySet����
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <param name="keyName">��ֵ</param>
        /// <returns>����IPropertySet</returns>
        public static IPropertySet ConvertString2PropertySet(string text)
        {
            if (text == null || text.Length == 0) return null;

            IMemoryBlobStream mstream = ConvertString2AEStream(text);
            if (mstream == null) return null;

            IObjectStream objstream = new ObjectStreamClass();

            try
            {
                objstream.Stream = mstream;

                IPropertySet pPropSet = new PropertySetClass();
                (pPropSet as IPersistStream).Load(objstream);

                return pPropSet;
            }
            catch
            {

            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objstream);
            }
            return null;
        }

        /// <summary>
        /// ��ȿ�¡����
        /// </summary>
        /// <param name="stream">�־ö���</param>
        /// <param name="keyName">��ֵ</param>
        /// <returns>���ؿ�¡����</returns>
        public static IPersistStream DeepClone(IPersistStream stream,string keyName)
        {
            if (stream == null) return null;

            string strStream = ConvertPersistStream2String(stream);
            return ConvertString2PersistStream(strStream,keyName);
        }

        /// <summary>
        /// �������ָ�����ļ�
        /// </summary>
        /// <param name="fileName">ָ�����ļ�·��</param>
        /// <param name="obj">object����</param>
        public static void SaveObjectToFile(string fileName, object obj)
        {
            IPropertySet pPropSet = new PropertySetClass();
            pPropSet.SetProperty("key", obj);

            //ʵ�����������ڴ���
            IMemoryBlobStream mstream = new MemoryBlobStreamClass();
            try
            {
                (pPropSet as IPersistStream).Save(mstream, 0);
                //��������ļ�
                mstream.SaveToFile(fileName);
            }
            catch
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mstream);
            }            
        }

        /// <summary>
        /// ��ָ�����ļ��л�ȡ����
        /// </summary>
        /// <param name="fileName">ָ�����ļ�·��</param>
        /// <returns>����Object����</returns>
        public static object LoadObjectFromFile(string fileName)
        {
            IMemoryBlobStream mstream = new MemoryBlobStreamClass();  
            mstream.LoadFromFile(fileName);

            IPropertySet pPropSet = new PropertySetClass();
            (pPropSet as IPersistStream).Load(mstream);

            if (pPropSet.Count > 0)
                return pPropSet.GetProperty("key");
            else
                return null; 
        }

        /// <summary>
        /// �ж��ֶ��Ƿ�Ϊ��ֵ�����ڡ��ַ������������е�һ��
        /// </summary>
        /// <param name="field"></param>
        /// <returns>������򷵻� true,���򷵻� false</returns>
        public static bool IsNormalField(IField field)
        {
            if ((field.Type != esriFieldType.esriFieldTypeBlob) &&
                    (field.Type != esriFieldType.esriFieldTypeGeometry) &&
                    (field.Type != esriFieldType.esriFieldTypeGlobalID) &&
                    (field.Type != esriFieldType.esriFieldTypeGUID) &&
                    (field.Type != esriFieldType.esriFieldTypeOID) &&
                    (field.Type != esriFieldType.esriFieldTypeRaster) &&
                    (field.Type != esriFieldType.esriFieldTypeXML))
                return true;
            else
                return false;
        }

        /// <summary>
        /// ��ȡע��ͼ��Ĳο�����
        /// </summary>
        /// <param name="pLayer">ע�ǲ�</param>
        /// <returns>���زο�����</returns>
        public static double GetAnnoLayerRefScale(IAnnotationLayer pLayer)
        {
            if ((pLayer as IFeatureLayer).FeatureClass.Extension is IAnnoClass)
            {
                return ((pLayer as IFeatureLayer).FeatureClass.Extension as IAnnoClass).ReferenceScale;
            }            
            else
                return 20000;
        }

        /// <summary>
        /// ��ָ����ͼ�㴴��Ҫ��
        /// </summary>
        /// <param name="pLayer">ͼ��</param>
        /// <param name="pGeometry">���ζ���</param>
        /// <returns>����Ҫ��</returns>
        public static IFeature CreateFeature(IFeatureLayer pLayer, IGeometry pGeometry)
        {
            if (pLayer == null) return null;

            IWorkspaceEdit wse = (pLayer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
            if (wse.IsBeingEdited() == false) return null;

            //��ʼ�༭
            wse.StartEditOperation();

            //�����µ�Ҫ��
            IFeature pFeature = pLayer.FeatureClass.CreateFeature();

            if (pLayer is IAnnotationLayer)
            {
                ITextElement pTextElement = CreateTextElement("�½��ı�", new System.Drawing.Font("����", 10), System.Drawing.Color.Black, pGeometry, GetAnnoLayerRefScale(pLayer as IAnnotationLayer), pLayer.FeatureClass);
                (pFeature as IAnnotationFeature).Annotation = pTextElement as IElement;
            }
            else
            {               
                IGeometryDef tGeometryDef = pFeature.Fields.get_Field(pFeature.Fields.FindField(pLayer.FeatureClass.ShapeFieldName)).GeometryDef;
                AG.COM.SDM.Utility.Editor.LibEditor.ResetGeometryMZ(pGeometry, tGeometryDef);
                //������ͼ�Σ�������                
                pFeature.Shape = pGeometry;
            }
            try
            {
                ISubtypes subTypes = pLayer.FeatureClass as ISubtypes;
                if (subTypes != null)
                {
                    if (subTypes.HasSubtype)
                    {
                        IRowSubtypes prowSubTypes = (IRowSubtypes)pFeature;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            //����
            pFeature.Store();

            //ֹͣ�༭
            wse.StopEditOperation();

            return pFeature;
        }

        /// <summary>
        /// �����ı�Ԫ�ض���
        /// </summary>
        /// <param name="text">��ʾ����</param>
        /// <param name="font">����</param>
        /// <param name="color">��ɫ</param>
        /// <param name="pGeometry">���ζ���</param>
        /// <param name="referenceScale">�ο�����</param>
        /// <param name="pFeatClassAnno">ע��Ҫ����</param>
        /// <returns>�����ı�Ԫ�ض���</returns>
        public static ITextElement CreateTextElement(string text, System.Drawing.Font font,System.Drawing.Color color,IGeometry pGeometry, double referenceScale, IFeatureClass pFeatClassAnno)
        {
            ITextSymbol pTextSymbol;
            //IGroupElement pGroupElement;
            stdole.IFontDisp pFontDisp;
            ITextElement pTextElement;
            
            //��������
            pFontDisp = new stdole.StdFontClass() as stdole.IFontDisp;
            pFontDisp.Name = font.Name;
            pFontDisp.Bold = font.Bold;
            pFontDisp.Underline = font.Underline;
            pFontDisp.Size = Convert.ToDecimal(font.Size);
            pFontDisp.Italic = font.Italic;

            //����textsymbol
            pTextSymbol = new TextSymbolClass();
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            pTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
            pTextSymbol.Font = pFontDisp;
            pTextSymbol.Color = ESRI.ArcGIS.ADF.Converter.ToRGBColor(color) as IColor;

            //����textelement
            pTextElement = new TextElementClass();
            pTextElement.Symbol = pTextSymbol;

            //ע��λ��                 
            (pTextElement as IElement).Geometry = pGeometry; 
            pTextElement.Text = text;

            return pTextElement;
        }

        /// <summary>
        /// ���ŵ�ѡ��ļ��ζ���Χ
        /// </summary>
        /// <param name="pActiveView">��ͼ����</param>
        /// <param name="pGeometry">���ζ���</param>
        public static void Zoom2SelectGeometry(IActiveView  pActiveView,IGeometry pGeometry)
        {
            IEnvelope tCurEnvelope = pActiveView.Extent;      //��ȡ��ǰ��ͼ��Χ
            IEnvelope tGeoEnvelope = pGeometry.Envelope;      //��ȡ��λ���ε���ͼ��Χ

            //if(pGeometry.GeometryType==esriGeometryType.esriGeometryPoint)
            //{
            //    tCurEnvelope.Width = tGeoEnvelope.Width + 500;
            //    tCurEnvelope.Height = tGeoEnvelope.Height + 500;
            //}
            //if(pGeometry.GeometryType==esriGeometryType.esriGeometryPolyline
            //    ||pGeometry.GeometryType==esriGeometryType.esriGeometryLine)
            //{
            //    tCurEnvelope.Width = tGeoEnvelope.Width + 200;
            //    tCurEnvelope.Height = tGeoEnvelope.Height + 200;
            //}
            //if(pGeometry.GeometryType==esriGeometryType.esriGeometryPolygon)
            //{
            //    tCurEnvelope.Width = tGeoEnvelope.Width * 1.2;
            //    tCurEnvelope.Height = tGeoEnvelope.Height * 1.2;
            //}

            tCurEnvelope.Width = tGeoEnvelope.Width + 200;
            tCurEnvelope.Height = tGeoEnvelope.Height + 200;

            //���嶨λ��ͼ���ĵ�
            IPoint tCenterPoint = new PointClass();
            tCenterPoint.PutCoords((tGeoEnvelope.XMax + tGeoEnvelope.XMin) / 2, (tGeoEnvelope.YMax + tGeoEnvelope.YMin) / 2);
            tCurEnvelope.CenterAt(tCenterPoint);

            pActiveView.Extent = tCurEnvelope;
            //pActiveView.Refresh();
            //Application.DoEvents();
        }

        /// <summary>
        /// ���ŵ�ѡ��ļ��ζ���Χ(�ɴ���)
        /// </summary>
        /// <param name="pActiveView">��ͼ����</param>
        /// <param name="pGeometry">���ζ���</param>
        public static void Zoom2SelectGeometry0(IActiveView pActiveView, IGeometry pGeometry)
        {
            IEnvelope tFullEnvelope = pActiveView.FullExtent; //��ȡ��ͼȫͼ��Χ 
            IEnvelope tCurEnvelope = pActiveView.Extent;      //��ȡ��ǰ��ͼ��Χ

            if (tCurEnvelope.Width > tFullEnvelope.Width / 20)
            {
                tCurEnvelope.Width = tFullEnvelope.Width / 20;
            }

            if (tCurEnvelope.Height > tFullEnvelope.Height / 20)
            {
                tCurEnvelope.Height = tFullEnvelope.Height / 20;
            }

            IEnvelope tGeoEnvelope = pGeometry.Envelope;
            if (tCurEnvelope.Width < tGeoEnvelope.Width)
            {
                tCurEnvelope.Width = tGeoEnvelope.Width;
            }

            if (tCurEnvelope.Height < tGeoEnvelope.Height)
            {
                tCurEnvelope.Height = tGeoEnvelope.Height;
            }

            //�������ĵ�
            IPoint tCenterPoint = new PointClass();
            tCenterPoint.PutCoords((tGeoEnvelope.XMax + tGeoEnvelope.XMin) / 2, (tGeoEnvelope.YMax + tGeoEnvelope.YMin) / 2);
            tCurEnvelope.CenterAt(tCenterPoint);

            pActiveView.Extent = tCurEnvelope;
            pActiveView.Refresh();
            Application.DoEvents();
        }

        /// <summary>
        /// ���ŵ�ѡ��Ҫ��
        /// </summary>
        /// <param name="pMap">��ͼ����</param>
        /// <param name="pFeatureLayer">Ҫ��ͼ��</param>
        public static void Zoom2SelectedFeatures(IMap pMap,IFeatureLayer pFeatureLayer)
        {
            //��ѯIFeatureSelection����
            IFeatureSelection tFeatureSelection = pFeatureLayer as IFeatureSelection;
            if (tFeatureSelection.SelectionSet.Count == 0) return;

            //��ȡѡ�񼯶���
            ISelectionSet tSelectionSet = tFeatureSelection.SelectionSet;

            //��ȡ��ѯ�α�
            ICursor tOutCursor;
            tSelectionSet.Search(null, false, out tOutCursor);

            IFeatureCursor tFeatureCursor = tOutCursor as IFeatureCursor;

            IFeature tFeature1 = tFeatureCursor.NextFeature();
            if (tFeature1 != null)
            {
                IGeometry tGeometry = tFeature1.ShapeCopy;

                //��ѯ�ӿ�����
                ITopologicalOperator tTopologicalOperator = tGeometry as ITopologicalOperator;

                for (IFeature tFeature2 = tFeatureCursor.NextFeature(); tFeature2 != null; tFeature2 = tFeatureCursor.NextFeature())
                {
                    tTopologicalOperator.Union(tFeature2.ShapeCopy);    
                }

                //��ѯ���緶Χ
                IEnvelope tEnvelope=tGeometry.Envelope;
                
                //���ĵ�
                IPoint tCenterPoint=new PointClass();
                tCenterPoint.PutCoords((tEnvelope.XMax+tEnvelope.XMin)/2,(tEnvelope.YMax+tEnvelope.YMin)/2);

                if (tEnvelope.Width > 0)
                {
                    tEnvelope.Expand(1.2, 1.2, true);
                    tEnvelope.CenterAt(tCenterPoint);
                }

                if (pFeatureLayer.MaximumScale == 0)
                {
                    if (pMap.MapUnits == esriUnits.esriUnknownUnits)
                    {
                        pMap.MapUnits = esriUnits.esriMeters;
                        pMap.MapScale = 500;
                        (pMap as IActiveView).Extent.CenterAt(tCenterPoint);
                    }
                }
                else
                {
                    pMap.MapScale = pFeatureLayer.MinimumScale;
                    IEnvelope tEnv = (pMap as IActiveView).Extent;
                    tEnv.CenterAt(tCenterPoint);
                    (pMap as IActiveView).ScreenDisplay.DisplayTransformation.VisibleBounds = tEnv;
                }
            }

            IActiveView tActiveView = pMap as IActiveView;
            //ˢ�¶���
            tActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null,null); 

            //�ͷ������Դ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
        }

        /// <summary>
        /// ��ȡͼ��ļ�������
        /// </summary>
        /// <param name="pLayer">Ҫ��ͼ��</param>
        /// <returns>���ؼ�������</returns>
        private static int GetLayerShapeType(IFeatureLayer pLayer)
        {
            if (pLayer is IAnnotationLayer)
                return 0;
            else if (pLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                return 1;
            else if (pLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultipoint)
                return 2;
            else if ((pLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryLine)
                || (pLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline))
                return 3;
            else
                return 4;
        } 
    }
}
