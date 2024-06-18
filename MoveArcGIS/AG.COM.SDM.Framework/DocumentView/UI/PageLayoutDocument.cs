using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;


namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ������ͼ�ĵ���
    /// </summary>
    public partial class PageLayoutDocument:DockDocument,IPageLayOutDocumentView
    {
        private IFramework m_Framework;
        //�ж�ϵͳ���ĵ��ı��¼��Ƿ���ִ��
        private bool m_IsMapDocumentChanged = false;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public PageLayoutDocument(IFramework pFramework)
        {
            //��ʼ�����������
            InitializeComponent();
            
            this.m_Framework = pFramework;

            //ע���ĵ����弤���¼�
            (this.m_Framework as IFrameworkEvents).MapDocumentChanged += new MapDocumentChangedEventHandler(PageLayoutDocument_MapDocumentChanged);
            //ע��ͣ�������¼�
            this.SetDockPanelEvent += new EventHandler(PageLayoutDocument_SetDockPanelEvent);
            //ע���ӡ��ͼ�ĵ��ı�ʱ
            this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(printPageLayout_OnPageLayoutReplaced);
       
            //����ҳ�沼����ͼ����
            //this.ReplaceMaps();                       
        }

        private void printPageLayout_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {
            if (m_IsMapDocumentChanged)
                return;
            //��ȡ��ͼ�������
            IMapService tMapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;
            //�����ͼ���ڣ�ȥ�����ִ��ڼ������

            tMapService.MapControl.Map = axPageLayoutControl1.ActiveView.FocusMap;
            tMapService.MapControl.ActiveView.Refresh();
        }

        private void PageLayoutDocument_SetDockPanelEvent(object sender, EventArgs e)
        {
            if (DockPanel != null)
            {
                DockPanel.Options.AllowDockBottom = false;
                DockPanel.Options.AllowDockFill = false;
                DockPanel.Options.AllowDockLeft = false;
                DockPanel.Options.AllowDockRight = false;
                DockPanel.Options.AllowDockTop = false;
                DockPanel.Options.FloatOnDblClick = false;
                DockPanel.Options.ShowAutoHideButton = false;
                DockPanel.Options.ShowCloseButton = false;
                DockPanel.Options.ShowMaximizeButton = false;
            }
        }

        private void PageLayoutDocument_MapDocumentChanged(object sender, EventArgs e)
        {
            m_IsMapDocumentChanged = true;
            DockDocument pDockContent = sender as DockDocument;
            if (pDockContent is DocumentView.IPageLayOutDocumentView)
            {
                //����ҳ�沼����ͼ����
                this.ReplaceMaps();
            }
            else if (pDockContent is DocumentView.IMapDocumentView)
            {
                //��ȡ��ͼ�������
                IMapService tMapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;
                //�����ͼ���ڣ�ȥ�����ִ��ڼ������
                tMapService.PageLayoutControl.ActiveView.Deactivate();
                tMapService.MapControl.ActiveView.Activate(tMapService.MapControl.hWnd);
                tMapService.MapControl.ActiveView.Refresh();
            }
            m_IsMapDocumentChanged = false;

        }

        #region IPageLayOutDocumentView ��Ա

        /// <summary>
        /// ��ȡ������ĵ���ͼ����
        /// </summary>
        public IActiveView ActiveView
        {
            get { return this.axPageLayoutControl1.ActiveView; }
        }

        /// <summary>
        /// ��ȡҳ�沼��
        /// </summary>
        public IPageLayout PageLayout
        {
            get { return this.axPageLayoutControl1.PageLayout; }
        }

        #endregion

        #region IDocumentView ��Ա

        /// <summary>
        /// ��ȡ�������ĵ�����
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
                    if (this.DockPanel != null)
                        this.DockPanel.Text = value;
            }
        }

        /// <summary>
        /// ��ȡ�ĵ�����
        /// </summary>
        public AG.COM.SDM.SystemUI.EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.PageLayoutDocument; }
        }

        /// <summary>
        /// ��ȡaxPageLayoutControl1����
        /// </summary>
        public object Hook
        {
            get { return this.axPageLayoutControl1.Object; }
        }

        #endregion  


        protected  void ResetMapSurroudFrame(IMap pMap)
        {
            try
            {
                IActiveView pActiveView = axPageLayoutControl1.ActiveView;
                IGraphicsContainer pGra = pActiveView.GraphicsContainer;
                pGra.Reset();

                IMapFrame pMapFrame = pGra.FindFrame(pActiveView.FocusMap) as IMapFrame;
                if (pMapFrame == null) return;

                IElement pElement = null;
                while ((pElement = pGra.Next()) != null)
                {
                    if (pElement is IMapFrame) continue;

                    IMapSurroundFrame pMapSurroundFrame = pElement as IMapSurroundFrame;
                    if (pMapSurroundFrame != null)
                    {

                        pMapSurroundFrame.MapFrame = pMapFrame;
                        pMapSurroundFrame.MapSurround.Map = pMap;

                        pGra.UpdateElement(pElement);
                    }
                }

                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics ^ esriViewDrawPhase.esriViewGraphicSelection, null, null);
            }
            catch { }
        }

        /// <summary>
        /// ����ҳ�沼����ͼ����
        /// </summary>
        private void ReplaceMaps()
        {
            //��ȡ��ͼ�������
            IMapService tMapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;

            IMap tMap = tMapService.MapControl.Map;
            tMapService.MapControl.ActiveView.Deactivate();
            //����ҳ�沼���ĵ�����   
            if (this.axPageLayoutControl1.ActiveView.FocusMap != tMap)
            {
                IMaps tMaps = new MapsClass();
                tMaps.Add(tMap);
                this.axPageLayoutControl1.PageLayout.ReplaceMaps(tMaps);
                
                axPageLayoutControl1.PageLayout.RulerSettings.SmallestDivision = 1;
                axPageLayoutControl1.ActiveView.ShowRulers = true;
                
            }
            //��������ǳ���Ҫ���򿪲��ִ���ʱȥ����ͼ���ڽ��㣬����ִ���
            tMapService.MapControl.ActiveView.Deactivate();
            axPageLayoutControl1.ActiveView.Activate(axPageLayoutControl1.hWnd);//Must  

            IActiveView tActiveView = this.axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
            IEnvelope pEnv = ((tMap as IActiveView).Extent as IClone).Clone() as IEnvelope;
            tActiveView.Extent = pEnv;

            tActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

      
        }          
    }





    /// <summary>
    /// IMap���϶�����
    /// </summary>
    public class MapsClass : IMaps, IDisposable
    {
        private IList<IMap> m_Maps = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MapsClass()
        {
            this.m_Maps = new List<IMap>();
        }

        #region IMaps ��Ա

        /// <summary>
        /// ���IMap����
        /// </summary>
        /// <param name="Map">IMap����</param>
        public void Add(IMap Map)
        {
            if (Map != null)
                this.m_Maps.Add(Map);
            else
                throw new Exception("Mapû�г�ʼ��������Ϊ��");
        }

        /// <summary>
        /// ��ȡIMap�����ܸ���
        /// </summary>
        public int Count
        {
            get { return m_Maps.Count; }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public IMap Create()
        {
            IMap newMap = new MapClass();
            m_Maps.Add(newMap);

            return newMap;
        }

        /// <summary>
        /// �Ƴ�ָ����IMap����
        /// </summary>
        /// <param name="Map">IMap����</param>
        public void Remove(IMap Map)
        {
            this.m_Maps.Remove(Map);
        }

        /// <summary>
        /// �Ƴ�ָ���������Ķ���
        /// </summary>
        /// <param name="Index">ָ������ֵ</param>
        public void RemoveAt(int Index)
        {
            if (Index > m_Maps.Count || Index < 0)
                throw new Exception("ָ������������Χ�������Ƴ���ͼ");

            m_Maps.RemoveAt(Index);
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Reset()
        {
            m_Maps.Clear();
        }

        /// <summary>
        /// ��ȡָ���������ĵ�ͼ����
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public IMap get_Item(int Index)
        {
            if (Index > m_Maps.Count || Index < 0)
                throw new Exception("ָ������������Χ");

            return m_Maps[Index];
        }

        #endregion

        #region IDisposable ��Ա

        /// <summary>
        /// �ͷŶ���
        /// </summary>
        public void Dispose()
        {
            if (m_Maps != null)
            {
                m_Maps.Clear();
                m_Maps = null;
            }
        }

        #endregion
    }
}