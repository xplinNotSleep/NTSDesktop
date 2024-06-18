using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// դ�����ݲü��������
    /// </summary>
    public sealed class RasterClipCommand : BaseTool, IUseIcon
    {
        protected IHookHelperEx m_hookHelper = new HookHelperEx();
        private FormRasterClip frm;
        private INewPolygonFeedback m_PolygonFeedBack;
        private INewLineFeedback m_LineFeedback;

        /// <summary>
        /// ��ʼ��ʵ������
        /// </summary>
        public RasterClipCommand()
        {
            try
            {
                this.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("AG.COM.SDM.DataTools.Resources." + "GridClip.bmp"));
                this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C18+.ico"));
                this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C18+_32.ico"));       
            }
            catch
            {
                this.m_bitmap = null;
            }
            base.m_caption = "�Զ��嵼��դ��";
            base.m_toolTip = "�Զ��嵼��դ��";
            base.m_name = GetType().FullName;
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// icoͼ�����16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// icoͼ�����32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// ����դ�����ݲü�������
        /// </summary>
        public FormRasterClip MainForm
        {
            set
            {
                frm = value;
            }
        }      

        /// <summary>
        /// ��ȡ����Ŀ���״̬
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
        /// ����ʱ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            m_hookHelper.Hook = hook;
        }

        /// <summary>
        /// ��������Ƽ���ͼ��
        /// </summary>
        /// <param name="Button">���µ���갴ť</param>
        /// <param name="Shift">���µ�shift����Alt����Ctrl��</param>
        /// <param name="X">��Ļ����X</param>
        /// <param name="Y">��Ļ����Y</param>
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button != 1) return;
            if (frm.SelArea.Exit) return;

            IGeometry pGeometry = null;
            // ѡ�����ͣ� �㣬�ߣ��棬���Σ� ��ǰ��Ļ
            switch (frm.SelArea.SelectType)
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
                frm.SelArea.Geometry = pGeometry;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            }

            if (!frm.SelArea.Exit && (frm.SelArea.MainForm as Form).WindowState == FormWindowState.Minimized
                && null != pGeometry)
            {
                (frm.SelArea.MainForm as Form).WindowState = FormWindowState.Normal;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            switch (frm.SelArea.SelectType)
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
                default:
                    return;
            }
        }

        public override void OnDblClick()
        {
            IGeometry pGeometry = null;

            switch (frm.SelArea.SelectType)
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
                frm.SelArea.Geometry = pGeometry;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            }

            if (!frm.SelArea.Exit && (frm.SelArea.MainForm as Form).WindowState == FormWindowState.Minimized)
            {
                (frm.SelArea.MainForm as Form).WindowState = FormWindowState.Normal;
            }
        }
        
        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            //������б������ʱͼ��
            IGraphicsContainer container = m_hookHelper.ActiveView.GraphicsContainer;
            container.DeleteAllElements();
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            if (null == frm)
            {
                frm = new FormRasterClip();
                frm.FormClosed += new FormClosedEventHandler(OnFormClosed);
                MainForm = frm;
                frm.SetHook(m_hookHelper);
                frm.Show(m_hookHelper.Win32Window);
            }
            else
            {
                if (!frm.Visible)
                {
                    frm.Show(m_hookHelper.Win32Window);
                }
                frm.WindowState = FormWindowState.Normal;
            }

        }

        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        #endregion
    }
}

