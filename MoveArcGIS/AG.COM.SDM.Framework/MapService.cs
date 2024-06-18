using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework.DocumentView;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility.Display;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 地图服务类
    /// </summary>
    public class MapService : IMapService
    {
        #region 属性
        private IFramework m_Framework;
        private IMapDocument m_MapDocument = null;
        private ILayer m_Layer = null;
        private object m_Hook = null;
        private double m_Tolerance = 0.000001;
        private IOperationStack m_OperationStack = new ControlsOperationStackClass();
        private ToolTip m_InfoTip;
        private static string m_MapDocName = "";
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pFramework">IFramework对象</param>
        public MapService(IFramework pFramework)
        {
            this.m_Framework = pFramework;
            (this.m_Framework as IFrameworkEvents).CurrentLayerChanged += new CurrentLayerChangedEventHandler(MapService_OnCurrentLayerChanged);
            (this.m_Framework as IFrameworkEvents).EagleViewChanged += new EagleViewChangedEventHandler(MapService_EagleViewChanged);

            //实例化工具提示
            this.m_InfoTip = new ToolTip();
            this.m_InfoTip.ToolTipIcon = ToolTipIcon.Info;
            this.m_InfoTip.IsBalloon = true;
            this.m_InfoTip.AutoPopDelay = 3000;
        }

        #region IMapService事件
        /// <summary>
        /// 当前工作地图视图刷新事件
        /// </summary>
        public event IMapControlEvents2_OnViewRefreshedEventHandler OnViewRefreshed;

        /// <summary>
        /// 当前工作地图可视范围发生变化事件
        /// </summary>
        public event IMapControlEvents2_OnExtentUpdatedEventHandler OnExtentUpdated;

        /// <summary>
        /// 地图对象发生变化事件
        /// </summary>
        public event IMapControlEvents2_OnMapReplacedEventHandler OnMapReplaced;
        #endregion

        #region IMapService 成员

        /// <summary>
        /// 容差
        /// </summary>
        public double Tolerance
        {
            get
            {
                return m_Tolerance;
            }
            set
            {
                m_Tolerance = value;
            }
        }

        /// <summary>
        /// 获取激活的视图对象
        /// </summary>
        public IActiveView ActiveView
        {
            get
            {
                if (m_Hook is IMapControl2)
                {
                    return ((IMapControl2)m_Hook).ActiveView;
                }
                else if (m_Hook is IPageLayoutControl2)
                {
                    IPageLayoutControl2 pageLayoutControl = m_Hook as IPageLayoutControl2;
                    return pageLayoutControl.ActiveView;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取或设置当前激活的地图对象
        /// </summary>
        public IMap FocusMap
        {
            get
            {
                return ActiveView.FocusMap;
            }
        }

        /// <summary>
        /// 获取或设置地图文档对象
        /// </summary>
        public IMapDocument MapDocument
        {
            get
            {
                return m_MapDocument;
            }
            set
            {
                m_MapDocument = value;
            }
        }

        
        public static string MapDocName
        {
            get
            {
                return m_MapDocName;
            }
            set
            {
                m_MapDocName = value;
            }
        }


        /// <summary>
        /// 获取MapControl控件
        /// </summary>
        public IMapControl2 MapControl
        {
            get
            {
                if ((this.m_Hook as IMapControl2) != null)
                    return (this.m_Hook as IMapControl2);
                else
                {
                    IDockDocumentService dockDocService = this.m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService;
                    if (dockDocService == null)
                        return null;
                    else
                    {
                        IMapDocumentView mapDocView = dockDocService.GetDockDocument(Convert.ToString(EnumDocumentType.MapDocument)) as IMapDocumentView;
                        if (mapDocView == null)
                            return null;
                        else
                            return mapDocView.Hook as IMapControl2;
                    }
                }

            }
        }


        /// <summary>
        /// 页面视图版式
        /// </summary>
        public IPageLayout PageLayout
        {
            get
            {
                IPageLayoutControl2 pageLayControl = m_Hook as IPageLayoutControl2;
                if (pageLayControl != null)
                    return pageLayControl.PageLayout;
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取IPageLayoutControl对象
        /// </summary>
        public IPageLayoutControl2 PageLayoutControl
        {
            get
            {
                if ((this.m_Hook as IPageLayoutControl2) != null)
                    return (this.m_Hook as IPageLayoutControl2);
                else
                {
                    IDockDocumentService dockDocService = this.m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService;
                    if (dockDocService == null)
                        return null;
                    else
                    {
                        IPageLayOutDocumentView PageDocView = dockDocService.GetDockDocument(Convert.ToString(EnumDocumentType.PageLayoutDocument)) as IPageLayOutDocumentView;
                        if (PageDocView == null)
                            return null;
                        else
                            return PageDocView.Hook as IPageLayoutControl2;
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置当前激活对象的操作工具
        /// </summary>
        public ESRI.ArcGIS.SystemUI.ITool CurrentTool
        {
            get
            {
                if (this.m_Hook != null)
                {
                    if (m_Hook is IMapControl2)
                        return (m_Hook as IMapControl2).CurrentTool;
                    else if (m_Hook is IPageLayoutControl2)
                        return (m_Hook as IPageLayoutControl2).CurrentTool;
                }

                return null;
            }
            set
            {
                if (this.m_Hook is IMapControl2)
                    (m_Hook as IMapControl2).CurrentTool = value;
                else if (this.m_Hook is IPageLayoutControl2)
                    (m_Hook as IPageLayoutControl2).CurrentTool = value;
            }
        }

        /// <summary>
        /// 获取IOperationStack
        /// </summary>
        public IOperationStack OperationStack
        {
            get { return this.m_OperationStack; }
        }

        /// <summary>
        /// 获取或设置当前操作图层
        /// </summary>
        public ILayer CurrentLayer
        {
            get
            {
                return this.m_Layer;
            }
        }

        /// <summary>
        /// 获取或设置TOCControl
        /// </summary>
        public ITOCControl TOCControl
        {
            get
            {
                IDockDocumentService dockDocService = this.m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService;
                if (dockDocService == null)
                    return null;
                else
                {
                    ITocDocumentView tocDocView = dockDocService.GetDockDocument(Convert.ToString(EnumDocumentType.TocDocument)) as ITocDocumentView;
                    if (tocDocView == null)
                        return null;
                    else
                        return tocDocView.Hook as ITOCControl;

                }
            }
        }

        /// <summary>
        /// 获取鹰眼视图对象
        /// </summary>
        public IMapControl2 EagleMapControl
        {
            get
            {
                IDockDocumentService dockDocService = this.m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService;
                if (dockDocService == null)
                    return null;
                else
                {
                    IEagleDocumentView eagleDocView = dockDocService.GetDockDocument(Convert.ToString(EnumDocumentType.EagleDocument)) as IEagleDocumentView;
                    if (eagleDocView == null)
                        return null;
                    else
                        return eagleDocView.Hook as IMapControl2;
                }
            }
        }

        /// <summary>
        /// 地图信息提示
        /// </summary>
        public ToolTip InfoTip
        {
            get
            {
                return this.m_InfoTip;
            }
        }

        /// <summary>
        /// 获取或设置当前激活的地图操作对象
        /// </summary>
        public object Hook
        {
            get
            {
                return this.m_Hook;
            }
            set
            {
                this.m_Hook = value;
                if (value is IMapControl2)
                {
                    //注销相关事件
                    (m_Hook as IMapControlEvents2_Event).OnViewRefreshed -= new IMapControlEvents2_OnViewRefreshedEventHandler(MapService_OnViewRefreshed);
                    (m_Hook as IMapControlEvents2_Event).OnExtentUpdated -= new IMapControlEvents2_OnExtentUpdatedEventHandler(MapService_OnExtentUpdated);
                    (m_Hook as IMapControlEvents2_Event).OnMapReplaced -= new IMapControlEvents2_OnMapReplacedEventHandler(MapService_OnMapReplaced);

                    //注册相关事件
                    (m_Hook as IMapControlEvents2_Event).OnViewRefreshed += new IMapControlEvents2_OnViewRefreshedEventHandler(MapService_OnViewRefreshed);
                    (m_Hook as IMapControlEvents2_Event).OnExtentUpdated += new IMapControlEvents2_OnExtentUpdatedEventHandler(MapService_OnExtentUpdated);
                    (m_Hook as IMapControlEvents2_Event).OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(MapService_OnMapReplaced);
                }
            }
        }

        #endregion

        /// <summary>
        /// 通知所有登记地图服务中的OnMapReplaced事件的对象
        /// </summary>
        /// <param name="newMap">新地图对象</param>
        protected void MapService_OnMapReplaced(object newMap)
        {
            if (OnMapReplaced != null)
                OnMapReplaced(newMap);
        }

        /// <summary>
        /// 通知所有登记地图服务中的OnExtentUpdated事件的对象
        /// </summary>
        /// <param name="displayTransformation">转换对象</param>
        /// <param name="sizeChanged">size是否发生变化</param>
        /// <param name="newEnvelope">新的矩形区域</param>
        protected void MapService_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            if (this.OnExtentUpdated != null)
                this.OnExtentUpdated(displayTransformation, sizeChanged, newEnvelope);
        }

        /// <summary>
        /// 通知所有登记地图服务中的OnViewRefreshed事件的对象
        /// </summary>
        /// <param name="ActiveView">视图对象</param>
        /// <param name="viewDrawPhase"><see cref="viewDrawPhase"/>viewDrawPhase</param>
        /// <param name="layerOrElement">更新图层或元素对象</param>
        /// <param name="envelope">刷新范围</param>
        protected void MapService_OnViewRefreshed(object ActiveView, int viewDrawPhase, object layerOrElement, object envelope)
        {
            if (this.OnViewRefreshed != null)
                OnViewRefreshed(ActiveView, viewDrawPhase, layerOrElement, envelope);
        }

        /// <summary>
        /// 侦听数据视图中OnCurrentLayerChanged事件,并作出相应处理
        /// </summary>
        /// <param name="sender">ILayer对象</param>
        /// <param name="e">事件参数</param>
        private void MapService_OnCurrentLayerChanged(object sender, EventArgs e)
        {
            this.m_Layer = sender as ILayer;
        }

        /// <summary>
        /// 侦听鹰眼视图中的EagViewChanged事件,并作出相应处理
        /// </summary>
        /// <param name="newEnvelope"></param>
        /// <param name="e"></param>
        private void MapService_EagleViewChanged(object newEnvelope, EventArgs e)
        {
            if (newEnvelope != null)
            {
                IEnvelope envelope = newEnvelope as IEnvelope;
                IActiveView activeView = this.ActiveView;
                if ((envelope != null) && (activeView != null))
                {
                    IActiveView mapView = activeView.FocusMap as IActiveView;
                    if (mapView != null)
                    {
                        mapView.Extent = envelope;
                        mapView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                }
            }
        }

        private MapEditor m_Editor = new MapEditor();

        /// <summary>
        /// 获取地图编辑类
        /// </summary>
        public MapEditor Editing
        {
            get { return m_Editor; }
        }
    }

    /// <summary>
    /// 地图编辑类
    /// </summary>
    public class MapEditor
    {
        //可编辑的工作空间 
        private IWorkspaceEdit m_WorkspaceEditing = null;
        private IFeatureLayer m_LayerEditing = null;
        private INewGeometryFeedBack m_SketchFeedBack = null;

        /// <summary>
        /// 获取或设置可编辑工作空间
        /// </summary>
        public ESRI.ArcGIS.Geodatabase.IWorkspaceEdit WorkspaceEditing
        {
            get { return m_WorkspaceEditing; }
            set { m_WorkspaceEditing = value; }
        }

        /// <summary>
        /// 获取或设置当前编辑图层
        /// </summary>
        public IFeatureLayer LayerEditing
        {
            get { return m_LayerEditing; }
            set { m_LayerEditing = value; }
        }

        /// <summary>
        /// 获取或设置图形咬合反馈对象
        /// </summary>
        public INewGeometryFeedBack SketchFeedBack
        {
            get { return m_SketchFeedBack; }
            set { m_SketchFeedBack = value; }
        }

        /// <summary>
        /// 创建指定几何对象的新要素
        /// </summary>
        /// <param name="pGeometry">指定的几何对象</param>
        /// <returns>返回新要素</returns>
        public IFeature CreateFeature(ESRI.ArcGIS.Geometry.IGeometry pGeometry)
        {
            if (m_LayerEditing == null) return null;

            m_WorkspaceEditing.StartEditOperation();

            //创建新图形，并保存
            IFeature pFeature = m_LayerEditing.FeatureClass.CreateFeature();
            pFeature.Shape = pGeometry;
            pFeature.Store();

            IRowSubtypes prowSubTypes = (IRowSubtypes)pFeature;
            try
            {
                prowSubTypes.InitDefaultValues();
            }
            catch
            {
            }

            m_WorkspaceEditing.StopEditOperation();

            return pFeature;
        }
    }
}
