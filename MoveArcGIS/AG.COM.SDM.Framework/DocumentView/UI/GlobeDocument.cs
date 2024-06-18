using AG.COM.SDM.Framework.DocumentView.Interfaces;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    public partial class GlobeDocument : DockDocument, IGlobeDocumentView
    {
        private IMap m_pMap;
        private IActiveView m_pMapActiveView = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_pMapActiveViewEvents = null;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;

        private IContextMenu m_DefaultMenu;         //默认的右键菜单
        private IHookHelperEx m_HookHelperEx;             //系统集成框架对象
        private bool rd3Dto2D = false;
        private bool rd2Dto3D = false;
        public GlobeDocument(IHookHelperEx pHookHelperEx)
        {
            this.m_HookHelperEx = pHookHelperEx;
            InitializeComponent();
            this.MouseWheel += GlobeDocument_MouseWheel;
            this.FormClosing += GlobeDocument_FormClosing;
            //ICamera camera = axGlobeControl1.GlobeCamera as ICamera;
            //camera.Azimuth = 333.516;
            //camera.Inclination = -1.65;
            //axGlobeControl1.Globe.GlobeDisplay.RefreshViewers();
            Init();
        }
        #region 方法
        public IActiveView ActiveView
        {
            get { return this.axGlobeControl1.GlobeViewer as IActiveView; }
        }

        public IContextMenu DefaultContextMenu
        {
            get { return this.m_DefaultMenu; }
            set { this.m_DefaultMenu = value; }
        }
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

        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.GlobeDocument; }
        }

        public object Hook
        {
            get { return this.axGlobeControl1.Object; }
        }
        public ESRI.ArcGIS.Controls.AxGlobeControl GlobeControl
        {
            get { return this.axGlobeControl1; }
        }
        public void LoadSxFile(string filename)
        {
            this.axGlobeControl1.Load3dFile(filename);
        }
        public void ShowNorthArrowEnabled()
        {
            axGlobeControl1.GlobeViewer.NorthArrowEnabled = true;
            ICamera camera = axGlobeControl1.GlobeCamera as ICamera;
            camera.Azimuth = 334.516;
            camera.Inclination = -16.5;
            axGlobeControl1.Globe.GlobeDisplay.RefreshViewers();
        }
        public void Set2Dto3D()
        {
            rd3Dto2D = false;
            rd2Dto3D = true;
        }
        public void Set3Dto2D()
        {
            rd3Dto2D = true;
            rd2Dto3D = false;
        }
        public void Startt2Dto3D()
        {
            tmr2Dto3D.Enabled = true;
            Set2Dto3D();
        }
        #endregion

        private void GlobeDocument_FormClosing(object sender, EventArgs e)
        {
            
        }

        private void GlobeDocument_MouseWheel(object sender, MouseEventArgs e)
        {
            if (rd3Dto2D == true)
            {
                //将axGlobeControl1相对于软件的坐标，变换成屏幕坐标
                System.Drawing.Point pSceLoc = axGlobeControl1.PointToScreen(axGlobeControl1.Location);
                //将鼠标所在位置坐标变换成屏幕坐标
                System.Drawing.Point Pt = this.PointToScreen(e.Location);
                //判断鼠标是否在屏幕外，如果是返回，无操作
                if (Pt.X < pSceLoc.X || Pt.X > pSceLoc.X + axGlobeControl1.Width || Pt.Y < pSceLoc.Y || Pt.Y > pSceLoc.Y + axGlobeControl1.Height)
                {
                    return;
                }
                double scale = 0.2;
                if (e.Delta > 0) scale = -scale;
                IGlobeCamera pGlobeCamera = axGlobeControl1.GlobeCamera;
                ICamera pCamera = pGlobeCamera as ICamera;
                IGlobeDisplay pGlobeDisplay = axGlobeControl1.GlobeDisplay;

             
                if (pGlobeCamera.OrientationMode == esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal)
                {
                    double xo, yo, zo;
                    pGlobeCamera.GetObserverLatLonAlt(out xo, out yo, out zo);
                    zo = zo * (1 + scale);
                    pGlobeCamera.SetObserverLatLonAlt(xo, yo, zo);
                  
                }
                else
                {
                    pCamera.ViewingDistance += pCamera.ViewingDistance * scale;
                }
                axGlobeControl1.GlobeDisplay.RefreshViewers();
               

            }
        }
        private void Init()
        {
            DockDocument mapDocument = m_HookHelperEx.DockDocumentService.GetDockDocument(Convert.ToString(EnumDocumentType.MapDocument));
            axMapControl1 = mapDocument.Controls[0] as ESRI.ArcGIS.Controls.AxMapControl;
            m_pMap = axMapControl1.Map;
            m_pMapActiveView = m_pMap as IActiveView;
            m_pMapActiveViewEvents = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event)m_pMap;
            m_pMapActiveViewEvents.AfterDraw += M_pActiveViewEvents_AfterDraw;

            Framework framework = m_HookHelperEx.Hook as Framework;
            if (framework != null)
            {
                //framework.DocumentManager.DocumentActivate += DocumentManager_DocumentActivate;
                framework.MapDocumentChanged += Framework_MapDocumentChanged;


            }
        }
        private void M_pActiveViewEvents_AfterDraw(ESRI.ArcGIS.Display.IDisplay Display, esriViewDrawPhase phase)
        {
            if (rd2Dto3D)
            {
                tmr2Dto3D.Interval = 500;
                tmr2Dto3D.Enabled = true;
            }

        }
        private void Framework_MapDocumentChanged(object sender, EventArgs e)
        {
            var doc = sender;
            if (sender is GlobeDocument)
            {
                Set3Dto2D();
            }
            else if (sender is MapDocument)
            {
                Set2Dto3D();
            }
            else
            {
                rd2Dto3D = false;
                rd3Dto2D = false;
            }
        }
        public IGeometry ProjectGeometryGeo(IGeometry pGeo)
        {
            //如果是地理坐标系，则投影到投影坐标系
            if (pGeo.SpatialReference is IProjectedCoordinateSystem)
            {
                ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironment();
                IGeographicCoordinateSystem pcs = srFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);     //投影到 WGS1984 坐标系
                pGeo.Project(pcs);
            }
            return pGeo;
        }
        /// <summary>
        /// XY坐标转经纬度
        /// </summary>
        /// <param name="pGeo"></param>
        /// <returns></returns>
        public IGeometry ProjectGeometryGeo(ESRI.ArcGIS.Controls.AxMapControl axMapControl, IPoint pt)
        {
            //如果是地理坐标系，则投影到投影坐标系
            IMap pMap = axMapControl.Map;// pActiveView.FocusMap;
            ISpatialReferenceFactory pfactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference flatref = pMap.SpatialReference;
            IGeometry pGeo = (IGeometry)pt;
            pGeo.SpatialReference = flatref;
            ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironment();
            IGeographicCoordinateSystem pcs = srFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);     //投影到 WGS1984 坐标系
            pGeo.Project(pcs);
            return pGeo;
        }
        /// <summary>
        /// 经纬度转XY坐标
        /// </summary>
        /// <param name="axGlobeControl"></param>
        /// <param name="pGeo"></param>
        /// <returns></returns>
        public IGeometry ProjectGeometry(ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl, IGeometry pGeo)
        {
            try
            {
                IMap pMap = axGlobeControl.Globe as IMap;
                ISpatialReference earthref = pMap.SpatialReference;
                pGeo.SpatialReference = earthref;
                ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironment();
                IProjectedCoordinateSystem pcs = srFactory.CreateProjectedCoordinateSystem(4527);      //投影到 Mercator 坐标系
                pGeo.Project(pcs);
                return pGeo;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        //图层更新联动3d-2d   
        private void Synchronization_2DTo3D()
        {
            ISceneViewer sceneViewer = axGlobeControl1.Globe.GlobeDisplay.ActiveViewer;
            IGlobeCamera pGlobeCamera = sceneViewer.Camera as IGlobeCamera;
            IGlobeViewUtil pGlobeViewUtil = pGlobeCamera as IGlobeViewUtil;
            ICamera pCamera = sceneViewer.Camera;
            #region 二维视图同步到三维

            IEnvelope pEnv = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds;
            IPoint point = new PointClass();        //将此区域的中心点保存起来
            point.SpatialReference = pEnv.SpatialReference;
            point.X = (pEnv.XMax + pEnv.XMin) / 2;  //取得视角中心点X坐标
            point.Y = (pEnv.YMax + pEnv.YMin) / 2;  //取得视角中心点Y坐标

            IGeometry geometry = ProjectGeometryGeo(axMapControl1, point);
            if (pGlobeCamera.OrientationMode == esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal)
            {
                IPoint ptTaget = new PointClass();     //创建一个目标点
                ptTaget.X = point.X;       //视觉区域中心点作为目标点
                ptTaget.Y = point.Y;
                ptTaget.Z = 0.000999;        //设置目标点高度，这里设为1 0米 单位1千米

                IPoint ptObserver = new PointClass();   //创建观察点 的X，Y，Z
                ptObserver.X = point.X;    //设置观察点坐标的X坐标
                ptObserver.Y = point.Y;
                ptObserver.Z = 0.299091134;

                pGlobeCamera.SetObserverLatLonAlt(ptObserver.Y, ptObserver.X, ptObserver.Z);

                pGlobeCamera.SetTargetLatLonAlt(ptTaget.Y, ptTaget.X, ptTaget.Z);

                pCamera.Azimuth = 333.516;
                pCamera.Inclination = -16;
                axGlobeControl1.GlobeDisplay.RefreshViewers();
            }
            else
            {
                IGeometry geometry1 = ProjectGeometryGeo(pEnv as IGeometry);
                pGlobeCamera.SetToZoomToExtents(pEnv, axGlobeControl1.Globe, sceneViewer);
                axGlobeControl1.GlobeDisplay.RefreshViewers();
            }


            #endregion
        }

        private void Synchronization_3DTo2D()
        {
            ISceneViewer sceneViewer = axGlobeControl1.Globe.GlobeDisplay.ActiveViewer;
            IGlobeCamera pGlobeCamera = sceneViewer.Camera as IGlobeCamera;
            IGlobeViewUtil pGlobeViewUtil = pGlobeCamera as IGlobeViewUtil;
            #region 三维维视图同步到二维

            //获取屏幕中心点坐标
            int x = axGlobeControl1.Width / 2;
            int y = axGlobeControl1.Height / 2;
            IPoint center = GlobeToDD(axGlobeControl1.GlobeDisplay, x, y, true);
            IGeometry geometry1 = ProjectGeometry(axGlobeControl1, center);
            Zoom2SelectGeometry(axMapControl1, center);
            #endregion
        }
        /// <summary>
        /// 缩放到选择的几何对象范围
        /// </summary>
        /// <param name="pActiveView">地图对象</param>
        /// <param name="pGeometry">几何对象</param>
        private void Zoom2SelectGeometry(ESRI.ArcGIS.Controls.AxMapControl axMapControl, IGeometry pGeometry)
        {
            IActiveView pActiveView = axMapControl.ActiveView;
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
            //Application.DoEvents();
        }

        /// <summary>
        /// 首先是通过IGlobeDisplay.Locate方法获得屏幕坐标的X,Y对应的地理坐标X,Y，第二步是通过地理坐标X,Y求解到地形影响下对应的高程值。
        /// </summary>
        /// <param name="globeDisplay"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="maxResolution"></param>
        /// <returns></returns>
        private IPoint GlobeToDD(IGlobeDisplay globeDisplay, int X, int Y, bool maxResolution)
        {
            IPoint point;
            System.Object objectOwner;
            System.Object objectObject;


            globeDisplay.Locate(globeDisplay.ActiveViewer, X, Y, false, true, out point, out objectOwner, out objectObject);  //获得屏幕坐标的X,Y对应的地理坐标点point
            if (point == null)
            {
                return null;
            }
            else
            {
                if (point.IsEmpty == true)
                {
                    return null;
                }
                else
                {
                    point.Z = GetGlobeElevation(globeDisplay, point.X, point.Y, maxResolution); //结合地形求解该点对应的Z值
                    return point;
                }
            }
        }
        private double GetGlobeElevation(IGlobeDisplay globeDisplay, double longitude, double latitude, bool maxResolution)
        {
            IUnitConverter unitConverter = new UnitConverterClass();


            double doubleZ = 0;
            globeDisplay.GetSurfaceElevation(longitude, latitude, maxResolution, out doubleZ);
            return unitConverter.ConvertUnits(doubleZ, esriUnits.esriMeters, globeDisplay.Globe.GlobeUnits); //高程值单位转换
        }

        private void tmr2Dto3D_Tick(object sender, EventArgs e)
        {
            tmr2Dto3D.Enabled = false;
            Synchronization_2DTo3D();
        }

        private void tmr3Dto2D_Tick(object sender, EventArgs e)
        {
            tmr3Dto2D.Enabled = false;
            Synchronization_3DTo2D();
        }

        private void axGlobeControl1_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseUpEvent e)
        {
            if (!rd3Dto2D) return;  //判断
            tmr3Dto2D.Interval = 500;
            tmr3Dto2D.Enabled = true;
        }
    }
}
