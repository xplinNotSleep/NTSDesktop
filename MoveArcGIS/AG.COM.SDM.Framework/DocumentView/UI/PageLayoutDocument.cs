using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;


namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// 版面视图文档类
    /// </summary>
    public partial class PageLayoutDocument:DockDocument,IPageLayOutDocumentView
    {
        private IFramework m_Framework;
        //判断系统的文档改变事件是否在执行
        private bool m_IsMapDocumentChanged = false;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PageLayoutDocument(IFramework pFramework)
        {
            //初始界面组件对象
            InitializeComponent();
            
            this.m_Framework = pFramework;

            //注册文档窗体激活事件
            (this.m_Framework as IFrameworkEvents).MapDocumentChanged += new MapDocumentChangedEventHandler(PageLayoutDocument_MapDocumentChanged);
            //注册停靠窗体事件
            this.SetDockPanelEvent += new EventHandler(PageLayoutDocument_SetDockPanelEvent);
            //注册打印视图文档改变时
            this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(printPageLayout_OnPageLayoutReplaced);
       
            //更新页面布局视图内容
            //this.ReplaceMaps();                       
        }

        private void printPageLayout_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {
            if (m_IsMapDocumentChanged)
                return;
            //获取地图服务对象
            IMapService tMapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;
            //激活地图窗口，去除布局窗口激活对象

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
                //更新页面布局视图内容
                this.ReplaceMaps();
            }
            else if (pDockContent is DocumentView.IMapDocumentView)
            {
                //获取地图服务对象
                IMapService tMapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;
                //激活地图窗口，去除布局窗口激活对象
                tMapService.PageLayoutControl.ActiveView.Deactivate();
                tMapService.MapControl.ActiveView.Activate(tMapService.MapControl.hWnd);
                tMapService.MapControl.ActiveView.Refresh();
            }
            m_IsMapDocumentChanged = false;

        }

        #region IPageLayOutDocumentView 成员

        /// <summary>
        /// 获取激活的文档视图对象
        /// </summary>
        public IActiveView ActiveView
        {
            get { return this.axPageLayoutControl1.ActiveView; }
        }

        /// <summary>
        /// 获取页面布局
        /// </summary>
        public IPageLayout PageLayout
        {
            get { return this.axPageLayoutControl1.PageLayout; }
        }

        #endregion

        #region IDocumentView 成员

        /// <summary>
        /// 获取或设置文档标题
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
        /// 获取文档类型
        /// </summary>
        public AG.COM.SDM.SystemUI.EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.PageLayoutDocument; }
        }

        /// <summary>
        /// 获取axPageLayoutControl1对象
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
        /// 更新页面布局视图内容
        /// </summary>
        private void ReplaceMaps()
        {
            //获取地图服务对象
            IMapService tMapService = this.m_Framework.GetService(typeof(IMapService)) as IMapService;

            IMap tMap = tMapService.MapControl.Map;
            tMapService.MapControl.ActiveView.Deactivate();
            //设置页面布局文档内容   
            if (this.axPageLayoutControl1.ActiveView.FocusMap != tMap)
            {
                IMaps tMaps = new MapsClass();
                tMaps.Add(tMap);
                this.axPageLayoutControl1.PageLayout.ReplaceMaps(tMaps);
                
                axPageLayoutControl1.PageLayout.RulerSettings.SmallestDivision = 1;
                axPageLayoutControl1.ActiveView.ShowRulers = true;
                
            }
            //下面两句非常重要，打开布局窗口时去除地图窗口焦点，激活布局窗口
            tMapService.MapControl.ActiveView.Deactivate();
            axPageLayoutControl1.ActiveView.Activate(axPageLayoutControl1.hWnd);//Must  

            IActiveView tActiveView = this.axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
            IEnvelope pEnv = ((tMap as IActiveView).Extent as IClone).Clone() as IEnvelope;
            tActiveView.Extent = pEnv;

            tActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

      
        }          
    }





    /// <summary>
    /// IMap集合对象类
    /// </summary>
    public class MapsClass : IMaps, IDisposable
    {
        private IList<IMap> m_Maps = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MapsClass()
        {
            this.m_Maps = new List<IMap>();
        }

        #region IMaps 成员

        /// <summary>
        /// 添加IMap对象
        /// </summary>
        /// <param name="Map">IMap对象</param>
        public void Add(IMap Map)
        {
            if (Map != null)
                this.m_Maps.Add(Map);
            else
                throw new Exception("Map没有初始化，不能为空");
        }

        /// <summary>
        /// 获取IMap集合总个数
        /// </summary>
        public int Count
        {
            get { return m_Maps.Count; }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public IMap Create()
        {
            IMap newMap = new MapClass();
            m_Maps.Add(newMap);

            return newMap;
        }

        /// <summary>
        /// 移除指定的IMap对象
        /// </summary>
        /// <param name="Map">IMap对象</param>
        public void Remove(IMap Map)
        {
            this.m_Maps.Remove(Map);
        }

        /// <summary>
        /// 移除指定索引处的对象
        /// </summary>
        /// <param name="Index">指定索引值</param>
        public void RemoveAt(int Index)
        {
            if (Index > m_Maps.Count || Index < 0)
                throw new Exception("指定索引超出范围，不能移除地图");

            m_Maps.RemoveAt(Index);
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            m_Maps.Clear();
        }

        /// <summary>
        /// 获取指定索引处的地图对象
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public IMap get_Item(int Index)
        {
            if (Index > m_Maps.Count || Index < 0)
                throw new Exception("指定索引超出范围");

            return m_Maps[Index];
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 释放对象
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