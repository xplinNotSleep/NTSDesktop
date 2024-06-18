using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    //新增控制点
    internal class ToolAddPoint : BaseTool
    {
        private IHookHelper m_HookHelper = new HookHelperClass();
        private frmGeoReference m_frmGeoReference;
        private ListView m_LvwPointInfo;
        private Cursor m_CursorAddPoint;
        public frmPointInfo m_frmPointInfo;
        private IPoint m_Point;
        private HookHelperEx m_HookHelperExMap;

        /// <summary>
        /// 设置通信类对象
        /// </summary>
        public HookHelperEx HookHelperExMap
        {
            set { m_HookHelperExMap = value; }
        }

        /// <summary>
        /// 设置影像配准窗体
        /// </summary>
        public frmGeoReference frmGeoReference
        {
            set { m_frmGeoReference = value; }
        }

        /// <summary>
        /// 设置点信息列表
        /// </summary>
        public ListView LvwPointInfo
        {
            set { m_LvwPointInfo = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ToolAddPoint()
        {
            try
            {
                base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstImages + "AddPoint.bmp"));
                base.m_cursor = new Cursor(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstCursors + "AddPoint.cur"));
            }
            catch
            {
                base.m_bitmap = null;
                base.m_cursor = null;
            }
            finally
            {
                m_caption = "新增控制点";
                m_message = "新增控制点";
                m_name = "ToolAddPoint";
                m_toolTip = "新增控制点";
            }
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="Button">按下的鼠标按钮</param>
        /// <param name="Shift">按下的shift键、Alt键、Ctrl键</param>
        /// <param name="X">屏幕坐标X</param>
        /// <param name="Y">屏幕坐标Y</param>
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            //设置符号样式
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbol();
            pSymbol.Color = pRgbColor as IColor;
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSCross;
            pSymbol.Size = 20;

            //点位置确定
            m_Point = new PointClass();
            m_Point = this.m_HookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

            //设置要添加的元素
            IMarkerElement pMarkerElement = new MarkerElementClass();
            pMarkerElement.Symbol = pSymbol;
            IElement pElement = pMarkerElement as IElement;
            pElement.Geometry = m_Point;

            //绘图容器
            IGraphicsContainer pGraphicsContainer = m_HookHelper.FocusMap as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElement, 0);
            m_HookHelper.ActiveView.Refresh();

            //存储图形元素
            ConstVariant.ElementImage.Add(pElement);
        }

        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="Button">弹起的鼠标按钮</param>
        /// <param name="Shift">弹起时按下的shift键、Alt键、Ctrl键</param>
        /// <param name="X">屏幕坐标X</param>
        /// <param name="Y">屏幕坐标Y</param>
        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            //添加影像点信息
            ListViewItem pListViewItem = new ListViewItem();
            pListViewItem.Text = ConstVariant.ElementImage.Count.ToString();
            decimal dX = System.Math.Round((decimal)m_Point.X, 6);
            decimal dY = System.Math.Round((decimal)m_Point.Y, 6);
            pListViewItem.SubItems.Add(dX.ToString());
            pListViewItem.SubItems.Add(dY.ToString());
            pListViewItem.SubItems.Add("");
            pListViewItem.SubItems.Add("");
            pListViewItem.SubItems.Add("");
            m_LvwPointInfo.Items.Add(pListViewItem);

            //获取地图点信息
            if (m_frmPointInfo == null)
            { m_frmPointInfo = new frmPointInfo(); }
            m_frmPointInfo.ShowInTaskbar = false;
            m_frmPointInfo.frmGeoReference = m_frmGeoReference;
            m_frmPointInfo.lvwPoint = this.m_LvwPointInfo;
            m_frmPointInfo.lvwPointInfo = pListViewItem;
            m_frmPointInfo.ImageX = dX.ToString();
            m_frmPointInfo.ImageY = dY.ToString();
            m_frmPointInfo.HookHelper = this.m_HookHelper;
            m_frmPointInfo.OnCreate(this.m_HookHelperExMap.Hook);
            m_frmPointInfo.ShowDialog();
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_HookHelper.Hook = hook;
        }
    }
}
