using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 矢量数据裁剪插件命令
    /// </summary>
    public class VectorClipCommand : BaseTool, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();
        private FormVectorClip m_frmVectorClip;
        private INewPolygonFeedback m_PolygonFeedBack;
        private INewLineFeedback m_LineFeedback;

        /// <summary>
        /// 设置矢量数据裁剪窗体类
        /// </summary>
        public FormVectorClip MainForm
        {
            set
            {
                m_frmVectorClip = value;
            }
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public VectorClipCommand()
        {
            try
            {
                this.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("AG.COM.SDM.DataTools.Resources." + "RasterClip.bmp"));
                this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "F1.ico"));
                this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "F1_32.ico"));         
            }
            catch
            {
                this.m_bitmap = null;
            }
            
            base.m_caption = "矢量数据裁剪";
            base.m_toolTip = "矢量数据裁剪";
            base.m_name = GetType().FullName;
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_hookHelper.FocusMap == null) return false;
                if (this.m_hookHelper.FocusMap.LayerCount < 1) return false;

                return true;
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_hookHelper.Hook = hook;
        }

        /// <summary>
        /// 点击鼠标绘制几何图形
        /// </summary>
        /// <param name="Button">按下的鼠标按钮</param>
        /// <param name="Shift">按下的shift键、Alt键、Ctrl键</param>
        /// <param name="X">屏幕坐标X</param>
        /// <param name="Y">屏幕坐标Y</param>
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button != 1) return;
            if (m_frmVectorClip == null) return;
            if (m_frmVectorClip.SelArea.Exit) return;
          
            IGeometry pGeometry = null;
            // 选择类型： 点，线，面，矩形， 当前屏幕
            switch (m_frmVectorClip.SelArea.SelectType)
            {
                case AreaSelectType.TYPE_POINT:
                    {
                        IPoint pPt = new PointClass();
                        pPt = m_hookHelper.MapService.MapControl.ToMapPoint(X, Y);
                        pGeometry = pPt;
                    }
                    break;
                case AreaSelectType.TYPE_POLYLINE:
                    if (m_LineFeedback == null)
                    {
                        m_LineFeedback = new NewLineFeedbackClass();
                        IPoint pPtStart = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                        m_LineFeedback.Start(pPtStart);
                        m_LineFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
                    }
                    else
                    {
                        IPoint pPtAdd = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                        m_LineFeedback.AddPoint(pPtAdd);
                    }
                    break;
                case AreaSelectType.TYPE_POLYGON:
                    if (m_PolygonFeedBack == null)
                    {
                        m_PolygonFeedBack = new NewPolygonFeedbackClass();
                        IPoint pPtStart = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                        m_PolygonFeedBack.Start(pPtStart);
                        m_PolygonFeedBack.Display = m_hookHelper.ActiveView.ScreenDisplay;
                    }
                    else
                    {
                        IPoint pPtAdd = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                        m_PolygonFeedBack.AddPoint(pPtAdd);
                    }

                    break;
                case AreaSelectType.TYPE_CIRCLE:
                    pGeometry = m_hookHelper.MapService.MapControl.TrackCircle();
                    break;
                case AreaSelectType.TYPE_RECT:
                    {
                        IEnvelope env = m_hookHelper.MapService.MapControl.TrackRectangle();
                        if (env.IsEmpty) return;
                        if (env.Width == 0.0 || env.Height == 0.0) return;
                        object missing = Type.Missing;
                        IPointCollection4 pointCollection = new PolygonClass();
                        pointCollection.AddPoint(env.LowerLeft, ref missing, ref missing);
                        pointCollection.AddPoint(env.LowerRight, ref missing, ref missing);
                        pointCollection.AddPoint(env.UpperRight, ref missing, ref missing);
                        pointCollection.AddPoint(env.UpperLeft, ref missing, ref missing);
                        pointCollection.AddPoint(env.LowerLeft, ref missing, ref missing);
                        pGeometry = pointCollection as IPolygon;

                    }
                    break;
                case AreaSelectType.TYPE_SCREEN:
                    return;
                default:
                    return;
            }
            if (null != pGeometry)
            {
                m_frmVectorClip.SelArea.Geometry = pGeometry;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            }

            if (!m_frmVectorClip.SelArea.Exit && (m_frmVectorClip.SelArea.MainForm as Form).WindowState == FormWindowState.Minimized
                && null != pGeometry)
            {
                (m_frmVectorClip.SelArea.MainForm as Form).WindowState = FormWindowState.Normal;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (m_frmVectorClip == null) return;

            switch (m_frmVectorClip.SelArea.SelectType)
            {              
                case AreaSelectType.TYPE_POLYLINE:
                    if (m_LineFeedback != null)
                    {
                        IPoint pPtMove = new PointClass();
                        pPtMove = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                        m_LineFeedback.MoveTo(pPtMove);
                    }
                    break;
                case AreaSelectType.TYPE_POLYGON:
                    if (m_PolygonFeedBack != null)
                    {
                        IPoint pPtMove = new PointClass();
                        pPtMove = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                        m_PolygonFeedBack.MoveTo(pPtMove);
                    }

                    break;
                default :
                    return;
            }          
        }

        public override void OnDblClick()
        {
            IGeometry pGeometry = null;

            switch (m_frmVectorClip.SelArea.SelectType)
            {
                case AreaSelectType.TYPE_POLYLINE:
                    if (m_LineFeedback == null) return;
                    pGeometry = m_LineFeedback.Stop() as IGeometry;
                    m_LineFeedback = null;
                    break;
                case AreaSelectType.TYPE_POLYGON:
                    if (m_PolygonFeedBack == null) return;
                    pGeometry = m_PolygonFeedBack.Stop() as IGeometry;
                    m_PolygonFeedBack = null;

                    break;
                default:
                    return;
            }

            if (null != pGeometry)
            {
                m_frmVectorClip.SelArea.Geometry = pGeometry;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            }

            if (!m_frmVectorClip.SelArea.Exit && (m_frmVectorClip.SelArea.MainForm as Form).WindowState == FormWindowState.Minimized)
            {
                (m_frmVectorClip.SelArea.MainForm as Form).WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            //清空所有保存的临时图形
            IGraphicsContainer container = m_hookHelper.ActiveView.GraphicsContainer;
            container.DeleteAllElements();
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
          
            if (null == m_frmVectorClip)
            {
                m_frmVectorClip = new FormVectorClip();
                m_frmVectorClip.FormClosed += new FormClosedEventHandler(OnFormClosed);
                MainForm = m_frmVectorClip;
                m_frmVectorClip.SetHook(m_hookHelper);
                m_frmVectorClip.Show(m_hookHelper.Win32Window);
            }
            else
            {
                if (!m_frmVectorClip.Visible)
                {
                    m_frmVectorClip.Show(m_hookHelper.Win32Window);                  
                }
                m_frmVectorClip.WindowState = FormWindowState.Normal;
            }

        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            m_frmVectorClip = null;
        }
        #endregion
    }
}
