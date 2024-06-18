using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ӥ���ĵ���
    /// </summary>
    public partial class EagleDocument : DockDocument, IEagleDocumentView
    {
        private IFramework m_Framework;                             //IFramework����
        private IMapService m_MapService;                           //��ͼ�������
        private IElement m_Element = null;  //���ο�Ԫ�ض���
        private INewEnvelopeFeedback m_EnvelopeFeedback;            //���Ƶľ��ο�
        private IPoint m_Point;                                     //����ƶ���     
        private ILayer m_Layer = null;                              //ӥ����ͼ������ͼͼ��
        private Boolean m_IsAffirm = true;                         //�Ƿ�ָ������ͼ��
        private bool m_IsMouseDown = false;                         //�������Ƿ���
        private double m_DrawScale = 5000;                          //����ӥ�ۺ��߿���Ʊ���

        public EagleDocument(IFramework pFramework)
            : this(pFramework, true)
        {

        }

        public EagleDocument(IFramework pFramework, bool isAffirm)
        {
            //���ڱ���ĳɼ̳�UserCOntrol�󣬷�����ִ�й��캯��ʱLoadʱ��ͻᴥ������ɲ�����ʵ���������������Ϊ�ڹ��캯���������
            this.m_Framework = pFramework;
            m_IsAffirm = isAffirm;

            //��ʼ������Ԫ��
            InitializeComponent();

            this.SetDockPanelEvent += new EventHandler(EagleDocument_SetDockPanelEvent);
        }

        void EagleDocument_SetDockPanelEvent(object sender, EventArgs e)
        {
            if (DockPanel != null)
            {          
                DockPanel.Options.ShowCloseButton = false;              
            }
        }

        private void EagleDocument_Load(object sender, EventArgs e)
        {
            //���þ��ο�Ԫ����ʽ         
            CreateAndAddExtentElement(null);

            //��ȡ��ͼ�������
            m_MapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;
            //���Ƶ�ͼ��������OnExtentUpdate�¼�
            m_MapService.OnExtentUpdated += new IMapControlEvents2_OnExtentUpdatedEventHandler(MapService_OnExtentUpdated);
         
            if (this.m_IsAffirm == false)
            {
                //ע���ͼ��������OnMapReplaced�¼�
                m_MapService.OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(tMapService_OnMapReplaced);
            }
        }

        /// <summary>
        /// ��������ӱ�ʾ��Χ��element
        /// </summary>
        /// <param name="geometry"></param>
        private void CreateAndAddExtentElement(IGeometry geometry)
        {
            //���ڷ�������element��geometry�޷��ı���ͼ�Σ����ֻ��ÿ�ζ�����new element�����
            m_Element = new RectangleElement();

            if (geometry != null)
            {
                m_Element.Geometry = geometry;
            }

            (this.m_Element as IFillShapeElement).Symbol = CreateFillSymbol();

            this.MapControl.ActiveView.GraphicsContainer.DeleteAllElements();
            this.MapControl.ActiveView.GraphicsContainer.AddElement(this.m_Element, 0);
        }

        /// <summary>
        /// ��ȡ������ӥ�ۺ��߿���Ʊ�����
        /// ���ڴ�������£�1��500�������Բ�����
        /// </summary>
        public double DrawScale
        {
            get { return this.m_DrawScale; }
            set
            {
                if (value < 500)
                {
                    this.m_DrawScale = 5000;
                }
                else
                {
                    this.m_DrawScale = value;
                }
            }
        }

        #region IEagleDocumentView ��Ա

        /// <summary>
        /// ��ȡӥ����ͼ��IMap����
        /// </summary>
        public ESRI.ArcGIS.Carto.IMap EagleMap
        {
            get { return this.MapControl.Map; }
        }

        /// <summary>
        /// ��ȡ�Ƿ�ָ��ӥ����ͼ�ĵ�ͼͼ��
        /// </summary>
        public Boolean IsAffirm
        {
            get
            {
                return this.m_IsAffirm;
            }
        }

        /// <summary>
        /// ����ӥ����ͼ�е�ͼ�ĵ�������ļ�·��
        /// </summary>
        public string MapDocument
        {
            set
            {
                string strMapFile = value;
                if (strMapFile.Trim().Length == 0)
                {
                    IMap tMap = new MapClass();
                    this.MapControl.Map = tMap;
                    this.m_IsAffirm = false;
                }
                else
                {
                    if (System.IO.File.Exists(strMapFile) == true)
                    {
                        IMapDocument tMapDocument = new MapDocumentClass();
                        tMapDocument.Open(strMapFile, "");

                        this.MapControl.Map = tMapDocument.get_Map(0);
                        this.m_IsAffirm = true;

                        //��10.1���Ժ�汾����ʼ����ʾ��Χ��mapcontrol��С�ı����ʾ��Χ�᲻��������Ϊʹ��FullExtent����������ʾ��Χ
                        //������Զ�����ʾ��Χ������ͨ������mxd��mapframe���Զ���ȫͼ��Χ��ʵ��
                        MapControl.Extent = MapControl.FullExtent;
                    }
                }
            }
        }
        #endregion

        #region IDocumentView ��Ա

        /// <summary>
        /// �ĵ�����
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return this.TabText;
            }
            set
            {
                this.TabText = value;
            }
        }

        /// <summary>
        /// �ĵ�����
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.EagleDocument; }
        }

        /// <summary>
        /// MapControl.Object����
        /// </summary>
        public object Hook
        {
            get { return this.MapControl.Object; }
        }

        #endregion

        private void MapControl_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1)
            {
                this.m_IsMouseDown = true;
                this.m_Point = this.MapControl.ToMapPoint(e.x, e.y);

                if (this.m_EnvelopeFeedback == null)
                {
                    this.m_EnvelopeFeedback = new NewEnvelopeFeedbackClass();
                    this.m_EnvelopeFeedback.Display = this.MapControl.ActiveView.ScreenDisplay;
                    this.m_EnvelopeFeedback.Start(this.m_Point);
                }
            }
        }

        private void MapControl_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (this.m_IsMouseDown == true)
            {               
                this.m_Point = this.MapControl.ToMapPoint(e.x, e.y);
                this.m_EnvelopeFeedback.MoveTo(this.m_Point);
            }
        }

        private void MapControl_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent e)
        {
            if (this.m_IsMouseDown == true)
            {
                IEnvelope tEnvelope;
                if (this.m_EnvelopeFeedback == null)
                {
                    IEnvelope tempEnv = this.m_Element.Geometry.Envelope;
                    tEnvelope = new EnvelopeClass();
                    if (tempEnv != null && tempEnv.IsEmpty == false)
                    {
                        tEnvelope.XMin = e.mapX - tempEnv.Width / 2;
                        tEnvelope.YMin = e.mapY - tempEnv.Height / 2;
                        tEnvelope.XMax = e.mapX + tempEnv.Width / 2;
                        tEnvelope.YMax = e.mapY + tempEnv.Height / 2;
                    }
                }
                else
                {
                    tEnvelope = this.m_EnvelopeFeedback.Stop();
                }

                if (tEnvelope.IsEmpty == false && tEnvelope.Width > 0 && tEnvelope.Height > 0)
                {
                    if (!this.MapControl.ActiveView.FullExtent.IsEmpty)
                    {
                        //ʵ�������ĵ����
                        IPoint centerPt = new PointClass();
                        centerPt.X = e.mapX;
                        centerPt.Y = e.mapY;

                        //ˢ��ӥ����ͼ
                        RefreshEagleMap(tEnvelope, centerPt);
                    }
                }

                this.m_IsMouseDown = false;
                this.m_EnvelopeFeedback = null;
            }
        }

        /// <summary>
        /// ������ͼ�����е�OnExtentUpdate�¼�,��������Ӧ����
        /// </summary>
        /// <param name="displayTransformation">��ʾת������</param>
        /// <param name="sizeChanged">size�Ƿ����仯 �緢���仯��Ϊtrue,����Ϊfalse</param>
        /// <param name="newEnvelope">��������</param>
        private void MapService_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            //δָ��ӥ�۵���������
            if (this.IsAffirm == false)
            {
                if (m_MapService.FocusMap.LayerCount > 0)
                {
                    //��ȡ����ͼ��ײ��ͼ��
                    ILayer tLayer = m_MapService.FocusMap.get_Layer(m_MapService.FocusMap.LayerCount - 1);
                    if (this.m_Layer != tLayer)
                    {
                        this.m_Layer = tLayer;
                        //�������ͼ��
                        this.MapControl.ClearLayers();
                        //��ӵ�����
                        this.MapControl.AddLayer(this.m_Layer, 0);
                    }
                }
            }
            if (esriUnits.esriUnknownUnits != this.m_MapService.FocusMap.MapUnits)
            {
                //�ж�����ͼ��ʾ��Χ�Ƿ����仯
                if (newEnvelope != null && this.m_MapService.FocusMap.MapScale > this.m_DrawScale)
                {
                    //����Ԫ�صļ��ζ���
                    CreateAndAddExtentElement(newEnvelope as IGeometry);
                }
                else
                {
                    //��ȡ����ͼ��ʾ��Χ�仯�е����ĵ�
                    IEnvelope tEnvelope = newEnvelope as IEnvelope;
                    IPoint centerPt = new PointClass();
                    centerPt.X = tEnvelope.XMin + tEnvelope.Width / 2;
                    centerPt.Y = tEnvelope.YMin + tEnvelope.Height / 2;

                    //��ȡ���ű�������֤�ڴ������������ӥ����ͼ���߿�Ŀ���������ֵ��
                    double zoomScale = this.m_DrawScale / this.m_MapService.FocusMap.MapScale;
                    tEnvelope.Expand(zoomScale, zoomScale, true);
                    tEnvelope.CenterAt(centerPt);

                    //����Ԫ�صļ��ζ���
                    CreateAndAddExtentElement(tEnvelope as IGeometry);
                }


                //ˢ����ͼ
                this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            }
            else
            {
                //�ж�����ͼ��ʾ��Χ�Ƿ����仯
                if (newEnvelope != null)
                {
                    //����Ԫ�صļ��ζ���
                    CreateAndAddExtentElement(newEnvelope as IGeometry);

                    //ˢ����ͼ
                    this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, this.m_Element, null);
                }
            }

        }

        /// <summary>
        /// ������ͼ�����е�OnMapReplaced�¼�,��������Ӧ����
        /// </summary>
        /// <param name="newMap">map����</param>
        private void tMapService_OnMapReplaced(object newMap)
        {
            if (newMap != null)
            {
                IMap tMap = newMap as IMap;
                if (tMap.LayerCount == 0) return;
                this.m_Layer = tMap.get_Layer(tMap.LayerCount - 1);
                this.MapControl.ClearLayers();
                this.MapControl.AddLayer(this.m_Layer, 0);

                this.MapService_OnExtentUpdated(null, true, (tMap as IActiveView).Extent);
                //RefreshEagleMap((tMap as IActiveView).Extent);
            }
        }

        /// <summary>
        /// ˢ��ӥ����ͼ
        /// </summary>
        /// <param name="newEnvelope">��ͼ��ʾ��Χ</param>
        /// <param name="centerPt">��ʾ���ĵ�</param>
        private void RefreshEagleMap(IEnvelope newEnvelope, IPoint centerPt)
        {
            //Ԫ��������
            CreateAndAddExtentElement(newEnvelope as IGeometry);

            //ˢ��
            this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, this.m_Element, null);

            ////�ж�����ͼ��ͼ��������С�ڿ���ֵ�����С�����õ�ͼ��ʾ��Χֵ
            //if (this.m_MapService.FocusMap.MapScale < this.m_DrawScale + 1)
            //{
            //    IEnvelope tEnvelope = new EnvelopeClass();
            //    tEnvelope = this.m_MapService.ActiveView.Extent;
            //    tEnvelope.CenterAt(centerPt);

            //    //����OnEagleViewChanged�¼�
            //    this.m_Framework.OnEagleViewChanged(tEnvelope, null);
            //}
            //else
            //{
            //����OnEagleViewChanged�¼�
            this.m_Framework.OnEagleViewChanged(newEnvelope, null);
            //}
        }

        /// <summary>
        /// ��������������ʽ
        /// </summary>
        /// <returns>����ISimpleFillSymbol��ʽ</returns>
        private ISimpleFillSymbol CreateFillSymbol()
        {
            //��ɫΪ��ɫ
            IRgbColor tRgbColor = new RgbColorClass();
            tRgbColor.RGB = 255;

            //���ñ��߿��
            ISimpleLineSymbol tOutLineSymbol = new SimpleLineSymbolClass();
            tOutLineSymbol.Width = 1.0;
            tOutLineSymbol.Color = tRgbColor;

            //���������ʽ
            ISimpleFillSymbol tFillSymbol = new SimpleFillSymbolClass();
            tFillSymbol.Outline = tOutLineSymbol;
            tFillSymbol.Color = tRgbColor;
            tFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;

            return tFillSymbol;
        }

        private void MapControl_Resize(object sender, EventArgs e)
        {
            try
            {
                //��10.1���Ժ�汾����ʼ����ʾ��Χ��mapcontrol��С�ı����ʾ��Χ�᲻��������Ϊʹ��FullExtent����������ʾ��Χ
                //������Զ�����ʾ��Χ������ͨ������mxd��mapframe���Զ���ȫͼ��Χ��ʵ��
                MapControl.Extent = MapControl.FullExtent;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.SystemUI.Utility.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.SystemUI.Utility.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
        }

        public void HideEx()
        {
            if (this.DockPanel != null)
            {
                this.DockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                this.DockPanel.HideImmediately();
            }
        }
    }
}
