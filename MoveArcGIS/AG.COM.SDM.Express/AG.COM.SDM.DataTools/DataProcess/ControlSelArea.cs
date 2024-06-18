using System;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 用户区域选择用户控件
    /// </summary>
    public partial class ControlSelArea : UserControl
    {
        #region 字段/属性/构造函数

        private IMapControlEvents2_OnAfterDrawEventHandler AfterDrawEvent2;
        private bool bExit; // 主窗体是否退出
        private IActiveView m_ActiveView;
        private ISymbol m_BuffSymbol;

        private ITool m_CurrentTool;                                        //当前交互工具
        private IHookHelperEx m_hookHelperEx;
        private IGeometry m_pBufferShape;                                   // 缓冲区图形       
        private IGeometry m_pUserShape;                                     // 用户绘制的图形
        private IScreenDisplay m_ScreenDisplay;
        private ISymbol m_Symbol;                                           //图形样式
        private object objForm;                                             // 父窗体
        private AreaSelectType selType = AreaSelectType.TYPE_NONE;              // 查询类型

        /// <summary>
        /// 构造函数
        /// </summary>
        public ControlSelArea()
        {
            InitializeComponent();
            this.Text = "用户区域选择";
        }

        /// <summary>
        /// 获取父窗体是否已退出
        /// </summary>
        public bool Exit
        {
            get { return this.bExit; }
        }

        /// <summary>
        /// 获取或设置父窗体
        /// </summary>
        public object MainForm
        {
            get { return objForm; }
            set { objForm = value; }
        }

        /// <summary>
        /// 获取图形样式
        /// </summary>
        public ISymbol Symbol
        {
            get
            {
                if (this.trackBar1.Value > 0)
                    return m_BuffSymbol;
                else return m_Symbol;
            }
        }

        /// <summary>
        /// 设置用户绘制的区域图形
        /// </summary>
        public IGeometry Geometry
        {
            set
            {
                this.m_pUserShape = value;
                if (this.trackBar1.Value > 0)
                    m_pBufferShape = GetBufferArea(m_pUserShape, this.trackBar1.Value);
                else
                    m_pBufferShape = null;
            }
        }

        /// <summary>
        /// 获取用户选择的区域图形
        /// </summary>
        public IGeometry RegionGeometry
        {
            get
            {
                if (this.trackBar1.Value > 0) return m_pBufferShape;
                else return m_pUserShape;
            }
        }

        /// <summary>
        /// 获取或设置查询类型
        /// </summary>
        public AreaSelectType SelectType
        {
            get { return selType; }
            set { selType = value; }
        }

        /// <summary>
        /// 获取当前的交互工具
        /// </summary>
        public ITool CurrentTool
        {
            set { m_CurrentTool = value; }
        }

        #endregion

        /// <summary>
        /// 点击选择图形按钮
        /// </summary>
        private void button_Click(object sender, EventArgs e)
        {
            Button btClick = sender as Button;
         
            if (btClick == this.btnPolygon) /// 面
            {
                SelectRegion(AreaSelectType.TYPE_POLYGON);
            }
            else if (btClick == this.btnCircle)
            {
                SelectRegion(AreaSelectType.TYPE_CIRCLE);
            }
            else if (btClick == this.btnRect) /// 矩形
            {
                SelectRegion(AreaSelectType.TYPE_RECT);
            }
            else if (btClick == this.btnLine) /// 线
            {
                SelectRegion(AreaSelectType.TYPE_POLYLINE);
            }
            else if (btClick == this.btnAll) /// 当前显示区域
            {
                selType = AreaSelectType.TYPE_SCREEN;
                IEnvelope env = this.m_hookHelperEx.ActiveView.Extent;
                object missing = Type.Missing;
                IPointCollection4 pointCollection = new PolygonClass();
                pointCollection.AddPoint(env.LowerLeft, ref missing, ref missing);
                pointCollection.AddPoint(env.LowerRight, ref missing, ref missing);
                pointCollection.AddPoint(env.UpperRight, ref missing, ref missing);
                pointCollection.AddPoint(env.UpperLeft, ref missing, ref missing);
                pointCollection.AddPoint(env.LowerLeft, ref missing, ref missing);
                this.Geometry = pointCollection as IPolygon;
                m_ActiveView.Refresh();
            }
        }

        /// <summary>
        /// 检查输入是否合法
        /// </summary>
        public bool CheckInput()
        {
            if (selType == AreaSelectType.TYPE_NONE)
            {
                MessageBox.Show("请选择一个自定义区域，并绘制相应的图形！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (null == m_pUserShape)
            {
                MessageBox.Show("请绘制你选择的自定义区域图形！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if ((m_pUserShape.GeometryType == esriGeometryType.esriGeometryPoint
                 || m_pUserShape.GeometryType == esriGeometryType.esriGeometryPolyline)
                && this.trackBar1.Value <= 0)
            {
                MessageBox.Show("请调整缓冲区大小，至少应该大于零！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (this.trackBar1.Value > 0)
            {
                m_pBufferShape = GetBufferArea(m_pUserShape, this.trackBar1.Value);
                if (null == m_pBufferShape)
                {
                    MessageBox.Show("意外错误，获取的缓冲区为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 绘制缓冲区
        /// </summary>
        private void DrawBufferShape()
        {
            if (null == m_pBufferShape) return;
            if (m_pBufferShape.GeometryType != esriGeometryType.esriGeometryPolygon &&
                m_pBufferShape.GeometryType != esriGeometryType.esriGeometryEnvelope) return;
            m_BuffSymbol = MakeSymbol(m_pBufferShape.GeometryType, 255, 0, 0, 120);
            m_ScreenDisplay.SetSymbol(m_BuffSymbol);
            m_ScreenDisplay.DrawPolygon(m_pBufferShape);
        }

        /// <summary>
        /// 绘制用户选择区域图形
        /// </summary>
        private void DrawUserShape()
        {
            if (null == m_pUserShape) return;            
            m_Symbol = MakeSymbol(m_pUserShape.GeometryType, 255, 0, 0, 120);
            if (m_pUserShape.GeometryType == esriGeometryType.esriGeometryPolygon
                || m_pUserShape.GeometryType == esriGeometryType.esriGeometryEnvelope)
            {
                m_ScreenDisplay.SetSymbol(m_Symbol);
                m_ScreenDisplay.DrawPolygon(m_pUserShape);
            }
            else if (m_pUserShape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                m_ScreenDisplay.SetSymbol(m_Symbol);

                m_ScreenDisplay.DrawPoint(m_pUserShape);
            }
            else if (m_pUserShape.GeometryType == esriGeometryType.esriGeometryPolyline
                     || m_pUserShape.GeometryType == esriGeometryType.esriGeometryLine)
            {
                m_ScreenDisplay.SetSymbol(m_Symbol);
                m_ScreenDisplay.DrawPolyline(m_pUserShape);
            }
        }

        /// <summary>
        /// 退出选择
        /// </summary>
        public void ExitSelect()
        {
            bExit = true;
            m_pUserShape = null;
            m_pBufferShape = null;
            try
            {
                if (null != AfterDrawEvent2)
                {
                    (m_hookHelperEx.MapService.MapControl as IMapControlEvents2_Event).OnAfterDraw -= AfterDrawEvent2;
                }
            }
            catch
            {
            }

            if (null != m_ActiveView) m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
        }

        /// <summary>
        /// 获取图形缓冲区
        /// </summary>
        /// <param name="geoRegion">要产生缓冲区的图形</param>
        /// <param name="iBuffer">缓冲区大小</param>
        private IGeometry GetBufferArea(IGeometry geoRegion, int iBuffer)
        {
            if (iBuffer <= 0) return null;
            if (null == geoRegion) return null;
            ITopologicalOperator oper = geoRegion as ITopologicalOperator;

            if (null != oper)
            {
                oper.Simplify();
                IPolygon polygon = oper.Buffer(iBuffer) as IPolygon;
                return polygon;
            }
            return null;
        }

        /// <summary>
        /// 设置符号风格
        /// </summary>
        private ISymbol MakeSymbol(esriGeometryType type, int iRed, int iGreen, int iBlue, int iTransparency)
        {
            return MakeSymbol(type, iRed, iGreen, iBlue, iTransparency, 1);
        }

        /// <summary>
        /// 设置符号风格
        /// </summary>
        private ISymbol MakeSymbol(esriGeometryType type, int iRed, int iGreen, int iBlue, int iTransparency, int iWidth)
        {
            ISimpleFillSymbol pSimpleFillSymbol = null;
            ISimpleLineSymbol pSimpleLineSymbol = null;
            ISimpleMarkerSymbol pMarkerSymbol = null;
            IRgbColor colorRGBColor;
            colorRGBColor = new RgbColorClass();
            colorRGBColor.Red = iRed;
            colorRGBColor.Green = iGreen;
            colorRGBColor.Blue = iBlue;
            colorRGBColor.Transparency = (byte) iTransparency;
            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    pMarkerSymbol = new SimpleMarkerSymbolClass();
                    pMarkerSymbol.Color = colorRGBColor;
                    pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    pMarkerSymbol.Size = iWidth;
                    return pMarkerSymbol as ISymbol;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    pSimpleLineSymbol = new SimpleLineSymbolClass();
                    pSimpleLineSymbol.Color = colorRGBColor;
                    pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                    pSimpleLineSymbol.Width = iWidth;
                    return pSimpleLineSymbol as ISymbol;
                case esriGeometryType.esriGeometryEnvelope:
                case esriGeometryType.esriGeometryPolygon:
                    pSimpleFillSymbol = new SimpleFillSymbolClass();
                    pSimpleFillSymbol.Color = colorRGBColor;
                    pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
                    pSimpleFillSymbol.Outline.Width = 1;
                    //(pSimpleFillSymbol as ISymbol).ROP2 = esriRasterOpCode.esriROPMaskNotPen;
                    return pSimpleFillSymbol as ISymbol;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// IMapControlEvents2_OnAfterDrawEvent事件响应函数
        /// </summary>
        private void OnAfterDraw2(object Display, int viewDrawPhase)
        {
            if (!this.bExit)
            {
                if (viewDrawPhase == (int)esriViewDrawPhase.esriViewForeground)
                {
                    DrawUserShape();
                    DrawBufferShape();
                }
            }
        }

        /// <summary>
        /// 选择区域
        /// </summary>
        private void SelectRegion(AreaSelectType type)
        {
            if (null != m_CurrentTool && null != m_hookHelperEx)
                m_hookHelperEx.MapService.MapControl.CurrentTool = m_CurrentTool;
            selType = type;
            if (m_pUserShape != null)
            {
                m_pUserShape = null;
            }
            if (objForm != null)
            {
                // 最小化窗体
                (objForm as Form).WindowState = FormWindowState.Minimized;
            }
            else
            {
                MessageBox.Show("没有设置父窗体！请在你的对话框初始化时，调用该控件的MainForm属性进行设置！", this.Text, MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// 设置HookHelperEx
        /// </summary>
        /// <param name="pHook"></param>
        public void SetHook2(IHookHelperEx pHook)
        {
            bExit = false;
            m_hookHelperEx = pHook;
            m_ActiveView = m_hookHelperEx.MapService.ActiveView;
            m_ScreenDisplay = m_ActiveView.ScreenDisplay;

            try
            {
                AfterDrawEvent2 = new IMapControlEvents2_OnAfterDrawEventHandler(OnAfterDraw2);
                (m_hookHelperEx.MapService.MapControl as IMapControlEvents2_Event).OnAfterDraw += AfterDrawEvent2;
            }
            catch (Exception ex)
            {
                AfterDrawEvent2 = null;
                MessageHandler.ShowErrorMsg(ex);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.trackBar1.Value = (int) this.numericUpDown1.Value;
            m_pBufferShape = GetBufferArea(m_pUserShape, this.trackBar1.Value);           
            if (null != m_ActiveView) m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.numericUpDown1.Text = this.trackBar1.Value.ToString(); 
        }
    }
}