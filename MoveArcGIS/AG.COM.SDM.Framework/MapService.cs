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
    /// ��ͼ������
    /// </summary>
    public class MapService : IMapService
    {
        #region ����
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
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pFramework">IFramework����</param>
        public MapService(IFramework pFramework)
        {
            this.m_Framework = pFramework;
            (this.m_Framework as IFrameworkEvents).CurrentLayerChanged += new CurrentLayerChangedEventHandler(MapService_OnCurrentLayerChanged);
            (this.m_Framework as IFrameworkEvents).EagleViewChanged += new EagleViewChangedEventHandler(MapService_EagleViewChanged);

            //ʵ����������ʾ
            this.m_InfoTip = new ToolTip();
            this.m_InfoTip.ToolTipIcon = ToolTipIcon.Info;
            this.m_InfoTip.IsBalloon = true;
            this.m_InfoTip.AutoPopDelay = 3000;
        }

        #region IMapService�¼�
        /// <summary>
        /// ��ǰ������ͼ��ͼˢ���¼�
        /// </summary>
        public event IMapControlEvents2_OnViewRefreshedEventHandler OnViewRefreshed;

        /// <summary>
        /// ��ǰ������ͼ���ӷ�Χ�����仯�¼�
        /// </summary>
        public event IMapControlEvents2_OnExtentUpdatedEventHandler OnExtentUpdated;

        /// <summary>
        /// ��ͼ�������仯�¼�
        /// </summary>
        public event IMapControlEvents2_OnMapReplacedEventHandler OnMapReplaced;
        #endregion

        #region IMapService ��Ա

        /// <summary>
        /// �ݲ�
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
        /// ��ȡ�������ͼ����
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
        /// ��ȡ�����õ�ǰ����ĵ�ͼ����
        /// </summary>
        public IMap FocusMap
        {
            get
            {
                return ActiveView.FocusMap;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ͼ�ĵ�����
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
        /// ��ȡMapControl�ؼ�
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
        /// ҳ����ͼ��ʽ
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
        /// ��ȡIPageLayoutControl����
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
        /// ��ȡ�����õ�ǰ�������Ĳ�������
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
        /// ��ȡIOperationStack
        /// </summary>
        public IOperationStack OperationStack
        {
            get { return this.m_OperationStack; }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ����ͼ��
        /// </summary>
        public ILayer CurrentLayer
        {
            get
            {
                return this.m_Layer;
            }
        }

        /// <summary>
        /// ��ȡ������TOCControl
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
        /// ��ȡӥ����ͼ����
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
        /// ��ͼ��Ϣ��ʾ
        /// </summary>
        public ToolTip InfoTip
        {
            get
            {
                return this.m_InfoTip;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ����ĵ�ͼ��������
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
                    //ע������¼�
                    (m_Hook as IMapControlEvents2_Event).OnViewRefreshed -= new IMapControlEvents2_OnViewRefreshedEventHandler(MapService_OnViewRefreshed);
                    (m_Hook as IMapControlEvents2_Event).OnExtentUpdated -= new IMapControlEvents2_OnExtentUpdatedEventHandler(MapService_OnExtentUpdated);
                    (m_Hook as IMapControlEvents2_Event).OnMapReplaced -= new IMapControlEvents2_OnMapReplacedEventHandler(MapService_OnMapReplaced);

                    //ע������¼�
                    (m_Hook as IMapControlEvents2_Event).OnViewRefreshed += new IMapControlEvents2_OnViewRefreshedEventHandler(MapService_OnViewRefreshed);
                    (m_Hook as IMapControlEvents2_Event).OnExtentUpdated += new IMapControlEvents2_OnExtentUpdatedEventHandler(MapService_OnExtentUpdated);
                    (m_Hook as IMapControlEvents2_Event).OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(MapService_OnMapReplaced);
                }
            }
        }

        #endregion

        /// <summary>
        /// ֪ͨ���еǼǵ�ͼ�����е�OnMapReplaced�¼��Ķ���
        /// </summary>
        /// <param name="newMap">�µ�ͼ����</param>
        protected void MapService_OnMapReplaced(object newMap)
        {
            if (OnMapReplaced != null)
                OnMapReplaced(newMap);
        }

        /// <summary>
        /// ֪ͨ���еǼǵ�ͼ�����е�OnExtentUpdated�¼��Ķ���
        /// </summary>
        /// <param name="displayTransformation">ת������</param>
        /// <param name="sizeChanged">size�Ƿ����仯</param>
        /// <param name="newEnvelope">�µľ�������</param>
        protected void MapService_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            if (this.OnExtentUpdated != null)
                this.OnExtentUpdated(displayTransformation, sizeChanged, newEnvelope);
        }

        /// <summary>
        /// ֪ͨ���еǼǵ�ͼ�����е�OnViewRefreshed�¼��Ķ���
        /// </summary>
        /// <param name="ActiveView">��ͼ����</param>
        /// <param name="viewDrawPhase"><see cref="viewDrawPhase"/>viewDrawPhase</param>
        /// <param name="layerOrElement">����ͼ���Ԫ�ض���</param>
        /// <param name="envelope">ˢ�·�Χ</param>
        protected void MapService_OnViewRefreshed(object ActiveView, int viewDrawPhase, object layerOrElement, object envelope)
        {
            if (this.OnViewRefreshed != null)
                OnViewRefreshed(ActiveView, viewDrawPhase, layerOrElement, envelope);
        }

        /// <summary>
        /// ����������ͼ��OnCurrentLayerChanged�¼�,��������Ӧ����
        /// </summary>
        /// <param name="sender">ILayer����</param>
        /// <param name="e">�¼�����</param>
        private void MapService_OnCurrentLayerChanged(object sender, EventArgs e)
        {
            this.m_Layer = sender as ILayer;
        }

        /// <summary>
        /// ����ӥ����ͼ�е�EagViewChanged�¼�,��������Ӧ����
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
        /// ��ȡ��ͼ�༭��
        /// </summary>
        public MapEditor Editing
        {
            get { return m_Editor; }
        }
    }

    /// <summary>
    /// ��ͼ�༭��
    /// </summary>
    public class MapEditor
    {
        //�ɱ༭�Ĺ����ռ� 
        private IWorkspaceEdit m_WorkspaceEditing = null;
        private IFeatureLayer m_LayerEditing = null;
        private INewGeometryFeedBack m_SketchFeedBack = null;

        /// <summary>
        /// ��ȡ�����ÿɱ༭�����ռ�
        /// </summary>
        public ESRI.ArcGIS.Geodatabase.IWorkspaceEdit WorkspaceEditing
        {
            get { return m_WorkspaceEditing; }
            set { m_WorkspaceEditing = value; }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ�༭ͼ��
        /// </summary>
        public IFeatureLayer LayerEditing
        {
            get { return m_LayerEditing; }
            set { m_LayerEditing = value; }
        }

        /// <summary>
        /// ��ȡ������ͼ��ҧ�Ϸ�������
        /// </summary>
        public INewGeometryFeedBack SketchFeedBack
        {
            get { return m_SketchFeedBack; }
            set { m_SketchFeedBack = value; }
        }

        /// <summary>
        /// ����ָ�����ζ������Ҫ��
        /// </summary>
        /// <param name="pGeometry">ָ���ļ��ζ���</param>
        /// <returns>������Ҫ��</returns>
        public IFeature CreateFeature(ESRI.ArcGIS.Geometry.IGeometry pGeometry)
        {
            if (m_LayerEditing == null) return null;

            m_WorkspaceEditing.StartEditOperation();

            //������ͼ�Σ�������
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
