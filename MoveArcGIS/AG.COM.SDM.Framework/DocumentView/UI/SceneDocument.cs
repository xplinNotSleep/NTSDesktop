using AG.COM.SDM.Framework.DocumentView.Interfaces;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
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
    public partial class SceneDocument : DockDocument, ISceneDocumentView
    {
       


        private IMap m_pMap;
        private IActiveView m_pMapActiveView = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_pMapActiveViewEvents = null;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;

        private IHookHelperEx m_HookHelperEx;             //系统集成框架对象
        private IContextMenu m_DefaultMenu;         //默认的右键菜单

        private IScene m_pScene;
        private IActiveView m_pSceneActiveView = null;
        private ESRI.ArcGIS.Carto.IActiveViewEvents_Event m_pSceneActiveViewEvents = null;
      
        private bool rd3Dto2D = false;
        private bool rd2Dto3D = false;
        public SceneDocument(IHookHelperEx pHookHelperEx)
        {
            InitializeComponent();
            this.m_HookHelperEx = pHookHelperEx;
            this.MouseWheel += SceneDocument_MouseWheel;
            //this.Load += SceneDocument_Load;
            this.FormClosing += SceneDocument_FormClosing;
            m_pScene = axSceneControl1.Scene;
            m_pSceneActiveView = m_pScene as IActiveView;
            m_pSceneActiveViewEvents = (ESRI.ArcGIS.Carto.IActiveViewEvents_Event)m_pScene;
            m_pSceneActiveViewEvents.AfterDraw += M_pSceneActiveViewEvents_AfterDraw;
            //ISceneViewer2 mView2 = axSceneControl1.SceneViewer as ISceneViewer2;
            //mView2.NorthArrowEnabled = true;
            Init();
            this.axSceneControl1.GotFocus += AxSceneControl1_GotFocus;
            //Init();
            this.Load += SceneDocument_Load;
        }
        public void ShowNorthArrowEnabled()
        {
            ISceneViewer2 mView2 = axSceneControl1.SceneViewer as ISceneViewer2;
            mView2.NorthArrowEnabled = true;
        }
        private void AxSceneControl1_GotFocus(object sender, EventArgs e)
        {
          
        }

        private void SceneDocument_FormClosing(object sender, EventArgs e)
        {
            m_pSceneActiveViewEvents.AfterDraw -= M_pSceneActiveViewEvents_AfterDraw;
            m_pMapActiveViewEvents.AfterDraw -= M_pActiveViewEvents_AfterDraw;
        }

    private void SceneDocument_Load(object sender, EventArgs e)
    {

    }
        public IScene Scene
        {
            get { return this.axSceneControl1.Scene; }
        } 

        public ICamera Camera
        {
            get { return this.axSceneControl1.Camera; }
        }

        public IActiveView ActiveView
        {
            get { return this.axSceneControl1.Scene as IActiveView; }
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
            get { return EnumDocumentType.SceneDocument; }
        }

        public object Hook
        {
            get { return this.axSceneControl1.Object; }
        }
        public ESRI.ArcGIS.Controls.AxSceneControl SceneControl
        {
            get { return this.axSceneControl1; }
        }
        public void LoadSxFile(string filename)
        {
            this.axSceneControl1.LoadSxFile(filename);
        }
        private void SceneDocument_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!rd3Dto2D) return;
            if (axSceneControl1.Camera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
            {
                System.Drawing.Point pSceLoc = axSceneControl1.PointToScreen(this.axSceneControl1.Location);
                System.Drawing.Point Pt = this.PointToScreen(e.Location);
                if (Pt.X < pSceLoc.X | Pt.X > pSceLoc.X + axSceneControl1.Width | Pt.Y < pSceLoc.Y | Pt.Y > pSceLoc.Y + axSceneControl1.Height) return;
                double scale = 0.6;
                if (e.Delta > 0) scale = 1.4;
                IEnvelope enve = axSceneControl1.Camera.OrthoViewingExtent;
                enve.Expand(scale, scale, true);
                ICamera3 pCamera = axSceneControl1.Camera as ICamera3;
                pCamera.OrthoViewingExtent_2 = enve;
                axSceneControl1.SceneGraph.RefreshViewers();
            }
            else
            {
                System.Drawing.Point pSceLoc = axSceneControl1.PointToScreen(this.axSceneControl1.Location);
                System.Drawing.Point Pt = this.PointToScreen(e.Location);
                if (Pt.X < pSceLoc.X | Pt.X > pSceLoc.X + axSceneControl1.Width | Pt.Y < pSceLoc.Y | Pt.Y > pSceLoc.Y + axSceneControl1.Height) return;
                double scale = 0.2;
                //if (e.Delta < 0) scale = -0.2;
                if (e.Delta > 0) scale = -0.2;
                ICamera pCamera = axSceneControl1.Camera;
                IPoint pPtObs = pCamera.Observer;
                IPoint pPtTar = pCamera.Target;
                pPtObs.X += (pPtObs.X - pPtTar.X) * scale;
                pPtObs.Y += (pPtObs.Y - pPtTar.Y) * scale;
                pPtObs.Z += (pPtObs.Z - pPtTar.Z) * scale;
                pCamera.Observer = pPtObs;

                axSceneControl1.SceneGraph.RefreshViewers();

                axMapControl1.MapScale = pCamera.ViewingDistance * 7.963403141361257;
            }
               
        }
        private void M_pSceneActiveViewEvents_AfterDraw(ESRI.ArcGIS.Display.IDisplay Display, esriViewDrawPhase phase)
        {
            if (rd3Dto2D)
            {
                tmr3Dto2D.Interval = 500;
                tmr3Dto2D.Enabled = true;
            }
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
        private void axSceneControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseDownEvent e)
        {
            //弹出右键菜单 
            if (e.button == 2)
            {
                IToolContextMenu toolMenu = axSceneControl1.CurrentTool as IToolContextMenu;
                if (toolMenu != null && toolMenu.ContextMenuStrip != null)
                {
                    //先设置每个菜单项的可用性
                    foreach (ToolStripItem item in toolMenu.ContextMenuStrip.Items)
                    {
                        if (item.Tag is ESRI.ArcGIS.SystemUI.ICommand)
                        {
                            item.Enabled = (item.Tag as ESRI.ArcGIS.SystemUI.ICommand).Enabled;
                        }
                    }

                    //再弹出菜单 
                    toolMenu.ContextMenuStrip.Show(axSceneControl1, e.x, e.y);
                }
                else
                {
                    //如果默认菜单不为空，则弹出
                    if (this.m_DefaultMenu != null)
                    {
                        this.m_DefaultMenu.OnCreate(this.m_HookHelperEx.Hook as IFramework);
                        this.m_DefaultMenu.PopupMenu(e.x, e.y, axSceneControl1.hWnd);
                    }
                }
            }

           
        }

        private void tmr2Dto3D_Tick(object sender, EventArgs e)
        {
            tmr2Dto3D.Enabled = false;
            ICamera pCamera = this.axSceneControl1.Camera;      //取得三维活动区域的Camara      ，就像你照相一样的视角，它有Taget（目标点）和Observer（观察点）两个属性需要设置    
            if (axSceneControl1.Camera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
            {
                IEnvelope pEnve = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds;
                ICamera3 pCamera3 = axSceneControl1.Camera as ICamera3;
                pCamera3.OrthoViewingExtent_2 = pEnve;
            }
            else
            {
                //2D—>3D联动
                IActiveView pActiveView1 = this.axMapControl1.Map as IActiveView;   //获取当前二维活动区域               
                IEnvelope enve = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds;// pActiveView1.Extent as IEnvelope;      //将此二位区域的Extent 保存在Envelope中

                IPoint point = new PointClass();        //将此区域的中心点保存起来
                point.X = (enve.XMax + enve.XMin) / 2;  //取得视角中心点X坐标
                point.Y = (enve.YMax + enve.YMin) / 2;  //取得视角中心点Y坐标

                //label1.Text = $"X:{point.X}  Y:{point.Y}";
                IPoint ptTaget = new PointClass();      //创建一个目标点
                ptTaget.X = point.X;        //视觉区域中心点作为目标点
                ptTaget.Y = point.Y;
                ptTaget.Z = 0;         //设置目标点高度，这里设为 0米

                IPoint ptObserver = new PointClass();   //创建观察点 的X，Y，Z
                ptObserver.X = point.X;     //设置观察点坐标的X坐标
                ptObserver.Y = point.Y;     //设置观察点坐标的Y坐标（这里加90米，是在南北方向上加了90米，当然这个数字可以自己定，意思就是将观察点和目标点有一定的偏差，从南向北观察
                double height = (enve.Width < enve.Height) ? enve.Width : enve.Height;      //计算观察点合适的高度，这里用三目运算符实现的，效果稍微好一些，当然可以自己拟定
                ptObserver.Z = height;              //设置观察点坐标的Y坐标
                IPoint ptObserver1 = new PointClass();
                ptObserver1.X = point.X;     //设置观察点坐标的X坐标
                ptObserver1.Y = point.Y;
                //label2.Text = $"X:{ptObserver.X}  Y:{ptObserver.Y}";

                pCamera.Target = ptTaget;       //赋予目标点
                pCamera.Observer = ptObserver;      //将上面设置的观察点赋予camera的观察点
                pCamera.Inclination = 90;       //设置三维场景视角，也就是高度角，视线与地面所成的角度
                pCamera.Azimuth = 180;          //设置三维场景方位角，视线与向北的方向所成的角度
            }


            axSceneControl1.SceneGraph.RefreshViewers();        //刷新地图，（很多时候，看不到效果，都是你没有刷新）


            double S = Math.Round(axMapControl1.MapScale, 0);   //保留0位小数
            //label3.Text = "比列尺: 1:" + S.ToString() + " ";
            double S1 = Math.Round(pCamera.ViewingDistance, 0);   //保留0位小数
            //label4.Text = "比列尺: 1:" + S1.ToString() + " ";
        }

        private void tmr3Dto2D_Tick(object sender, EventArgs e)
        {
            tmr3Dto2D.Enabled = false;
            ICamera pCamera = axSceneControl1.Camera;
            if (axSceneControl1.Camera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
            {
                IEnvelope pEnv = axSceneControl1.Camera.OrthoViewingExtent as IEnvelope;
                this.axMapControl1.Extent = pEnv;
                this.axMapControl1.Refresh();
            }
            else
            {
                IPoint observerPoint = new PointClass();
                IPoint targetPoint = new PointClass();
                IActiveView pActiveView2 = this.axMapControl1.Map as IActiveView;
                IEnvelope pEnv = pActiveView2.Extent as IEnvelope;
                observerPoint = pCamera.Observer;
                targetPoint = pCamera.Target;
                IPoint point = new PointClass();
                point.X = observerPoint.X;
                point.Y = observerPoint.Y;
                point.Z = 0;
                pEnv.CenterAt(point);
                this.axMapControl1.Extent = pEnv;
                this.axMapControl1.Refresh();
            }
              
            //label2.Text = $"X:{point.X}  Y:{point.Y}";
            double S = Math.Round(pCamera.ViewingDistance, 0);   //保留0位小数
           // label4.Text = "比列尺: 1:" + S.ToString() + " ";
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
            if (framework !=null)
            {
                //framework.DocumentManager.DocumentActivate += DocumentManager_DocumentActivate;
                framework.MapDocumentChanged += Framework_MapDocumentChanged;


            }
        }

        private void Framework_MapDocumentChanged(object sender, EventArgs e)
        {
            var doc = sender;
            if( sender is SceneDocument)
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

       
        private void DocumentManager_DocumentActivate(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
           var doc=  e.Document;
        }

        private void DockManager_ActivePanelChanged(object sender, DevExpress.XtraBars.Docking.ActivePanelChangedEventArgs e)
        {
          
        }

        private void M_pActiveViewEvents_AfterDraw(ESRI.ArcGIS.Display.IDisplay Display, esriViewDrawPhase phase)
        {
            if (rd2Dto3D)
            {
                tmr2Dto3D.Interval = 500;
                tmr2Dto3D.Enabled = true;
            }
           
        }
       
    }
}
