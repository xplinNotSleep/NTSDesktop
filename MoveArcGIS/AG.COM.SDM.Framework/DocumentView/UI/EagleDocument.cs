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
    /// 鹰眼文档类
    /// </summary>
    public partial class EagleDocument : DockDocument, IEagleDocumentView
    {
        private IFramework m_Framework;                             //IFramework对象
        private IMapService m_MapService;                           //地图服务对象
        private IElement m_Element = null;  //矩形框元素对象
        private INewEnvelopeFeedback m_EnvelopeFeedback;            //绘制的矩形框
        private IPoint m_Point;                                     //鼠标移动点     
        private ILayer m_Layer = null;                              //鹰眼视图导航地图图层
        private Boolean m_IsAffirm = true;                         //是否指定导航图层
        private bool m_IsMouseDown = false;                         //鼠标左键是否按下
        private double m_DrawScale = 5000;                          //控制鹰眼红线框绘制比例

        public EagleDocument(IFramework pFramework)
            : this(pFramework, true)
        {

        }

        public EagleDocument(IFramework pFramework, bool isAffirm)
        {
            //由于本类改成继承UserCOntrol后，发现在执行构造函数时Load时间就会触发，造成不能在实例化传入参数，改为在构造函数传入参数
            this.m_Framework = pFramework;
            m_IsAffirm = isAffirm;

            //初始化界面元素
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
            //设置矩形框元素样式         
            CreateAndAddExtentElement(null);

            //获取地图服务对象
            m_MapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;
            //定制地图服务对象的OnExtentUpdate事件
            m_MapService.OnExtentUpdated += new IMapControlEvents2_OnExtentUpdatedEventHandler(MapService_OnExtentUpdated);
         
            if (this.m_IsAffirm == false)
            {
                //注册地图服务对象的OnMapReplaced事件
                m_MapService.OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(tMapService_OnMapReplaced);
            }
        }

        /// <summary>
        /// 创建并添加表示范围的element
        /// </summary>
        /// <param name="geometry"></param>
        private void CreateAndAddExtentElement(IGeometry geometry)
        {
            //由于发现重设element的geometry无法改变其图形，因此只有每次都重新new element并添加
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
        /// 获取或设置鹰眼红线框绘制比例，
        /// 如在大比例尺下（1：500），可以不绘制
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

        #region IEagleDocumentView 成员

        /// <summary>
        /// 获取鹰眼视图的IMap对象
        /// </summary>
        public ESRI.ArcGIS.Carto.IMap EagleMap
        {
            get { return this.MapControl.Map; }
        }

        /// <summary>
        /// 获取是否指定鹰眼视图的地图图层
        /// </summary>
        public Boolean IsAffirm
        {
            get
            {
                return this.m_IsAffirm;
            }
        }

        /// <summary>
        /// 设置鹰眼视图中地图文档对象的文件路径
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

                        //在10.1或以后版本，初始化显示范围和mapcontrol大小改变后显示范围会不正常，因为使用FullExtent重新设置显示范围
                        //如果想自定义显示范围，可以通过设置mxd的mapframe的自定义全图范围来实现
                        MapControl.Extent = MapControl.FullExtent;
                    }
                }
            }
        }
        #endregion

        #region IDocumentView 成员

        /// <summary>
        /// 文档标题
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
        /// 文档类型
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.EagleDocument; }
        }

        /// <summary>
        /// MapControl.Object对象
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
                        //实例化中心点对象
                        IPoint centerPt = new PointClass();
                        centerPt.X = e.mapX;
                        centerPt.Y = e.mapY;

                        //刷新鹰眼视图
                        RefreshEagleMap(tEnvelope, centerPt);
                    }
                }

                this.m_IsMouseDown = false;
                this.m_EnvelopeFeedback = null;
            }
        }

        /// <summary>
        /// 侦听地图服务中的OnExtentUpdate事件,并作出相应处理
        /// </summary>
        /// <param name="displayTransformation">显示转换对象</param>
        /// <param name="sizeChanged">size是否发生变化 如发生变化则为true,否则为false</param>
        /// <param name="newEnvelope">矩形区域</param>
        private void MapService_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            //未指定鹰眼导航层的情况
            if (this.IsAffirm == false)
            {
                if (m_MapService.FocusMap.LayerCount > 0)
                {
                    //获取主视图最底层的图层
                    ILayer tLayer = m_MapService.FocusMap.get_Layer(m_MapService.FocusMap.LayerCount - 1);
                    if (this.m_Layer != tLayer)
                    {
                        this.m_Layer = tLayer;
                        //清除所有图层
                        this.MapControl.ClearLayers();
                        //添加导航层
                        this.MapControl.AddLayer(this.m_Layer, 0);
                    }
                }
            }
            if (esriUnits.esriUnknownUnits != this.m_MapService.FocusMap.MapUnits)
            {
                //判断主视图显示范围是否发生变化
                if (newEnvelope != null && this.m_MapService.FocusMap.MapScale > this.m_DrawScale)
                {
                    //设置元素的几何对象
                    CreateAndAddExtentElement(newEnvelope as IGeometry);
                }
                else
                {
                    //获取主视图显示范围变化中的中心点
                    IEnvelope tEnvelope = newEnvelope as IEnvelope;
                    IPoint centerPt = new PointClass();
                    centerPt.X = tEnvelope.XMin + tEnvelope.Width / 2;
                    centerPt.Y = tEnvelope.YMin + tEnvelope.Height / 2;

                    //获取缩放比例，保证在大比例尺条件下鹰眼视图红线框的控制在设置值内
                    double zoomScale = this.m_DrawScale / this.m_MapService.FocusMap.MapScale;
                    tEnvelope.Expand(zoomScale, zoomScale, true);
                    tEnvelope.CenterAt(centerPt);

                    //设置元素的几何对象
                    CreateAndAddExtentElement(tEnvelope as IGeometry);
                }


                //刷新视图
                this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            }
            else
            {
                //判断主视图显示范围是否发生变化
                if (newEnvelope != null)
                {
                    //设置元素的几何对象
                    CreateAndAddExtentElement(newEnvelope as IGeometry);

                    //刷新视图
                    this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, this.m_Element, null);
                }
            }

        }

        /// <summary>
        /// 侦听地图服务中的OnMapReplaced事件,并作出相应处理
        /// </summary>
        /// <param name="newMap">map对象</param>
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
        /// 刷新鹰眼视图
        /// </summary>
        /// <param name="newEnvelope">地图显示范围</param>
        /// <param name="centerPt">显示中心点</param>
        private void RefreshEagleMap(IEnvelope newEnvelope, IPoint centerPt)
        {
            //元素重设置
            CreateAndAddExtentElement(newEnvelope as IGeometry);

            //刷新
            this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, this.m_Element, null);

            ////判断主视图地图比例尺是小于控制值，如果小于重置地图显示范围值
            //if (this.m_MapService.FocusMap.MapScale < this.m_DrawScale + 1)
            //{
            //    IEnvelope tEnvelope = new EnvelopeClass();
            //    tEnvelope = this.m_MapService.ActiveView.Extent;
            //    tEnvelope.CenterAt(centerPt);

            //    //触发OnEagleViewChanged事件
            //    this.m_Framework.OnEagleViewChanged(tEnvelope, null);
            //}
            //else
            //{
            //触发OnEagleViewChanged事件
            this.m_Framework.OnEagleViewChanged(newEnvelope, null);
            //}
        }

        /// <summary>
        /// 创建矩形填充框样式
        /// </summary>
        /// <returns>返回ISimpleFillSymbol样式</returns>
        private ISimpleFillSymbol CreateFillSymbol()
        {
            //颜色为红色
            IRgbColor tRgbColor = new RgbColorClass();
            tRgbColor.RGB = 255;

            //设置边线宽度
            ISimpleLineSymbol tOutLineSymbol = new SimpleLineSymbolClass();
            tOutLineSymbol.Width = 1.0;
            tOutLineSymbol.Color = tRgbColor;

            //设置填充样式
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
                //在10.1或以后版本，初始化显示范围和mapcontrol大小改变后显示范围会不正常，因为使用FullExtent重新设置显示范围
                //如果想自定义显示范围，可以通过设置mxd的mapframe的自定义全图范围来实现
                MapControl.Extent = MapControl.FullExtent;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.SystemUI.Utility.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.SystemUI.Utility.MessageHandler.ShowErrorMsg(ex.Message, "错误");
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
