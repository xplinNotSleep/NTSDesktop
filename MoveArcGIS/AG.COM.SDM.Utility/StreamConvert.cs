using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 流转换类
    /// </summary>
    public class StreamConvert
    {
        /// <summary>
        /// 将AE类型的内存流转换为字节流
        /// </summary>
        /// <param name="mbs">AE类型的内存流</param>
        /// <returns>返回字节流</returns>
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
        /// 将字节流转换为AE可用的字节流
        /// </summary>
        /// <param name="stream">字节流</param>
        /// <returns>返回二进制内存流</returns>
        public static IMemoryBlobStream ConvertStream2AEStream(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            IMemoryBlobStreamVariant mbs = new MemoryBlobStreamClass();
            ((IMemoryBlobStreamVariant)mbs).ImportFromVariant(bytes);       

            return mbs as IMemoryBlobStream;
        }

        /// <summary>
        /// 将8位无符号整数组转换为十六进制的字符串
        /// </summary>
        /// <param name="bts">字节数组</param>
        /// <returns>返回字符串</returns>
        public static string ConvertBytes2HexText(byte[] bts)
        {
            //实例化可变字符串类
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
        /// 转换文本对象为字节数组对象
        /// </summary>
        /// <param name="text">文本对象</param>
        /// <returns>返回字节数组对象</returns>
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
        /// 将指定的字符串转换为AE类型的内存流
        /// </summary>
        /// <param name="text">指定的字符串</param>
        /// <returns>内存流</returns>
        public static IMemoryBlobStream ConvertString2AEStream(string text)
        {
            //将十六进制的文本对象转换为字节数组
            byte[] bts = ConvertHexText2Bytes(text);

            using (Stream stream = new MemoryStream(bts))
            {
                //将字节点转换为AE可用的内存流
                return ConvertStream2AEStream(stream);
            }
        }

        /// <summary>
        /// 转换二进制内存流为字符串
        /// </summary>
        /// <param name="mstream">二进制内存流对象</param>
        /// <returns>返回字符串</returns>
        public static string ConvertAEStream2String(IMemoryBlobStream mstream)
        {
            using (Stream stream = ConvertAEStream2Stream(mstream))
            {
                byte[] bts = new byte[stream.Length];
                stream.Read(bts, 0, bts.Length);

                //将字节流转换为十六进制的文本对象
                return ConvertBytes2HexText(bts);
            }
        }

        /// <summary>
        /// 转换数据集对象为字符串
        /// </summary>
        /// <param name="pDataset">数据集对象</param>
        /// <returns>字符串</returns>
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
                //转换二进制内存流为字符串
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
        /// 转换数据集字符串为数据集
        /// </summary>
        /// <param name="strDataset">数据集字符串</param>
        /// <param name="DataType">数据类型</param>
        /// <returns>返回数据集</returns>
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
                        return null; //table表不能位于featuredataset中
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
        /// 转换流对象为字符串
        /// </summary>
        /// <param name="pStream">流对象</param>
        /// <returns>字符串</returns>
        public static string ConvertPersistStream2String(IPersistStream pStream)
        {
            if (pStream == null) return "";
            string strStream = "";

            IObjectStream objStream = new ObjectStreamClass();
            try
            {
                objStream.Stream = new MemoryBlobStreamClass();

                //实例化属性对象类
                IPropertySet pPropSet = new PropertySetClass();
                pPropSet.SetProperty("key", pStream);
                (pPropSet as IPersistStream).Save(objStream, 0);

                //实例化二进制内存流对象
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
        /// 将字符串转换为流对象
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="keyName">键值</param>
        /// <returns>返回流对象</returns>
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
        /// 将字符串转换为IPropertySet对象
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="keyName">键值</param>
        /// <returns>返回IPropertySet</returns>
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
        /// 深度克隆对象
        /// </summary>
        /// <param name="stream">持久对象</param>
        /// <param name="keyName">键值</param>
        /// <returns>返回克隆对象</returns>
        public static IPersistStream DeepClone(IPersistStream stream,string keyName)
        {
            if (stream == null) return null;

            string strStream = ConvertPersistStream2String(stream);
            return ConvertString2PersistStream(strStream,keyName);
        }

        /// <summary>
        /// 保存对象到指定的文件
        /// </summary>
        /// <param name="fileName">指定的文件路径</param>
        /// <param name="obj">object对象</param>
        public static void SaveObjectToFile(string fileName, object obj)
        {
            IPropertySet pPropSet = new PropertySetClass();
            pPropSet.SetProperty("key", obj);

            //实例化二进制内存流
            IMemoryBlobStream mstream = new MemoryBlobStreamClass();
            try
            {
                (pPropSet as IPersistStream).Save(mstream, 0);
                //保存对象到文件
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
        /// 从指定的文件中获取对象
        /// </summary>
        /// <param name="fileName">指定的文件路径</param>
        /// <returns>返回Object对象</returns>
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
        /// 判断字段是否为数值、日期、字符串三种类型中的一种
        /// </summary>
        /// <param name="field"></param>
        /// <returns>如果是则返回 true,否则返回 false</returns>
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
        /// 获取注记图层的参考比例
        /// </summary>
        /// <param name="pLayer">注记层</param>
        /// <returns>返回参考比例</returns>
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
        /// 在指定的图层创建要素
        /// </summary>
        /// <param name="pLayer">图层</param>
        /// <param name="pGeometry">几何对象</param>
        /// <returns>返回要素</returns>
        public static IFeature CreateFeature(IFeatureLayer pLayer, IGeometry pGeometry)
        {
            if (pLayer == null) return null;

            IWorkspaceEdit wse = (pLayer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
            if (wse.IsBeingEdited() == false) return null;

            //开始编辑
            wse.StartEditOperation();

            //创建新的要素
            IFeature pFeature = pLayer.FeatureClass.CreateFeature();

            if (pLayer is IAnnotationLayer)
            {
                ITextElement pTextElement = CreateTextElement("新建文本", new System.Drawing.Font("宋体", 10), System.Drawing.Color.Black, pGeometry, GetAnnoLayerRefScale(pLayer as IAnnotationLayer), pLayer.FeatureClass);
                (pFeature as IAnnotationFeature).Annotation = pTextElement as IElement;
            }
            else
            {               
                IGeometryDef tGeometryDef = pFeature.Fields.get_Field(pFeature.Fields.FindField(pLayer.FeatureClass.ShapeFieldName)).GeometryDef;
                AG.COM.SDM.Utility.Editor.LibEditor.ResetGeometryMZ(pGeometry, tGeometryDef);
                //创建新图形，并保存                
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

            //保存
            pFeature.Store();

            //停止编辑
            wse.StopEditOperation();

            return pFeature;
        }

        /// <summary>
        /// 创建文本元素对象
        /// </summary>
        /// <param name="text">显示文字</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>
        /// <param name="pGeometry">几何对象</param>
        /// <param name="referenceScale">参考比例</param>
        /// <param name="pFeatClassAnno">注记要素类</param>
        /// <returns>返回文本元素对象</returns>
        public static ITextElement CreateTextElement(string text, System.Drawing.Font font,System.Drawing.Color color,IGeometry pGeometry, double referenceScale, IFeatureClass pFeatClassAnno)
        {
            ITextSymbol pTextSymbol;
            //IGroupElement pGroupElement;
            stdole.IFontDisp pFontDisp;
            ITextElement pTextElement;
            
            //定义字体
            pFontDisp = new stdole.StdFontClass() as stdole.IFontDisp;
            pFontDisp.Name = font.Name;
            pFontDisp.Bold = font.Bold;
            pFontDisp.Underline = font.Underline;
            pFontDisp.Size = Convert.ToDecimal(font.Size);
            pFontDisp.Italic = font.Italic;

            //定义textsymbol
            pTextSymbol = new TextSymbolClass();
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            pTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
            pTextSymbol.Font = pFontDisp;
            pTextSymbol.Color = ESRI.ArcGIS.ADF.Converter.ToRGBColor(color) as IColor;

            //生成textelement
            pTextElement = new TextElementClass();
            pTextElement.Symbol = pTextSymbol;

            //注记位置                 
            (pTextElement as IElement).Geometry = pGeometry; 
            pTextElement.Text = text;

            return pTextElement;
        }

        /// <summary>
        /// 缩放到选择的几何对象范围
        /// </summary>
        /// <param name="pActiveView">地图对象</param>
        /// <param name="pGeometry">几何对象</param>
        public static void Zoom2SelectGeometry(IActiveView  pActiveView,IGeometry pGeometry)
        {
            IEnvelope tCurEnvelope = pActiveView.Extent;      //获取当前视图范围
            IEnvelope tGeoEnvelope = pGeometry.Envelope;      //获取定位几何的视图范围

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

            //定义定位视图中心点
            IPoint tCenterPoint = new PointClass();
            tCenterPoint.PutCoords((tGeoEnvelope.XMax + tGeoEnvelope.XMin) / 2, (tGeoEnvelope.YMax + tGeoEnvelope.YMin) / 2);
            tCurEnvelope.CenterAt(tCenterPoint);

            pActiveView.Extent = tCurEnvelope;
            //pActiveView.Refresh();
            //Application.DoEvents();
        }

        /// <summary>
        /// 缩放到选择的几何对象范围(旧代码)
        /// </summary>
        /// <param name="pActiveView">地图对象</param>
        /// <param name="pGeometry">几何对象</param>
        public static void Zoom2SelectGeometry0(IActiveView pActiveView, IGeometry pGeometry)
        {
            IEnvelope tFullEnvelope = pActiveView.FullExtent; //获取视图全图范围 
            IEnvelope tCurEnvelope = pActiveView.Extent;      //获取当前视图范围

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

            //定义中心点
            IPoint tCenterPoint = new PointClass();
            tCenterPoint.PutCoords((tGeoEnvelope.XMax + tGeoEnvelope.XMin) / 2, (tGeoEnvelope.YMax + tGeoEnvelope.YMin) / 2);
            tCurEnvelope.CenterAt(tCenterPoint);

            pActiveView.Extent = tCurEnvelope;
            pActiveView.Refresh();
            Application.DoEvents();
        }

        /// <summary>
        /// 缩放到选择要素
        /// </summary>
        /// <param name="pMap">地图对象</param>
        /// <param name="pFeatureLayer">要素图层</param>
        public static void Zoom2SelectedFeatures(IMap pMap,IFeatureLayer pFeatureLayer)
        {
            //查询IFeatureSelection引用
            IFeatureSelection tFeatureSelection = pFeatureLayer as IFeatureSelection;
            if (tFeatureSelection.SelectionSet.Count == 0) return;

            //获取选择集对象
            ISelectionSet tSelectionSet = tFeatureSelection.SelectionSet;

            //获取查询游标
            ICursor tOutCursor;
            tSelectionSet.Search(null, false, out tOutCursor);

            IFeatureCursor tFeatureCursor = tOutCursor as IFeatureCursor;

            IFeature tFeature1 = tFeatureCursor.NextFeature();
            if (tFeature1 != null)
            {
                IGeometry tGeometry = tFeature1.ShapeCopy;

                //查询接口引用
                ITopologicalOperator tTopologicalOperator = tGeometry as ITopologicalOperator;

                for (IFeature tFeature2 = tFeatureCursor.NextFeature(); tFeature2 != null; tFeature2 = tFeatureCursor.NextFeature())
                {
                    tTopologicalOperator.Union(tFeature2.ShapeCopy);    
                }

                //查询包络范围
                IEnvelope tEnvelope=tGeometry.Envelope;
                
                //中心点
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
            //刷新对象
            tActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null,null); 

            //释放相关资源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
        }

        /// <summary>
        /// 获取图层的几何类型
        /// </summary>
        /// <param name="pLayer">要素图层</param>
        /// <returns>返回几何类型</returns>
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
