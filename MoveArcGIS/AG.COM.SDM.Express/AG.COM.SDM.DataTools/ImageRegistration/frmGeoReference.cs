using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility.AEChineseCommand;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    /// <summary>
    /// 影像图配准窗体类
    /// </summary>
    public partial class frmGeoReference : Form
    {
        private HookHelperEx m_HookHelperEx;
        private string m_GeoImageFullName = "";
        private bool m_InitialFlag = false;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public frmGeoReference()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置HookHelperEx对象
        /// </summary>
        public HookHelperEx HookHelperEx
        { 
            set { m_HookHelperEx = value ;}
        }

        /// <summary>
        /// 获取或设置影像文件全路径
        /// </summary>
        public string GeoImageFullName
        {
            get { return m_GeoImageFullName; }
            set { m_GeoImageFullName = value; }
        }

        //初始化控件参数
        private void frmGeoReference_Load(object sender, EventArgs e)
        {
            m_InitialFlag = true;
            //清除十字标志
            this.ClearAllElement();

            //加载影像文件
            this.AddRasterLayer();

            //初始化控件
            this.InitialControls();

            //初始化工具栏
            this.InitialToolbarControl();

            m_InitialFlag = false;
        }

        /// <summary>
        /// 加载影像文件
        /// </summary>
        private void AddRasterLayer()
        {
            //加载影像文件
            if (m_GeoImageFullName == "") return;

            if (File.Exists(m_GeoImageFullName) == false)
                MessageBox.Show("影像文件不存在！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            IRasterLayer pRasterLayer = new RasterLayerClass();
            pRasterLayer.CreateFromFilePath(m_GeoImageFullName);

            this.axMapControl.AddLayer(pRasterLayer);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialControls()
        {
            //初始化Listview
            this.lvwPointReference.View = View.Details;
            this.lvwPointReference.FullRowSelect = true;
            this.lvwPointReference.HideSelection = false;
            this.lvwPointReference.Columns.Add("ID", "标号", 40, HorizontalAlignment.Center, -1);
            this.lvwPointReference.Columns.Add("ImageX", "图像 X", 90, HorizontalAlignment.Center, -1);
            this.lvwPointReference.Columns.Add("ImageY", "图像 Y", 90, HorizontalAlignment.Center, -1);
            this.lvwPointReference.Columns.Add("MapX", "地图 X", 90, HorizontalAlignment.Center, -1);
            this.lvwPointReference.Columns.Add("MapY", "地图 Y", 90, HorizontalAlignment.Center, -1);
            this.lvwPointReference.Columns.Add("Tolerance", "残差", 80, HorizontalAlignment.Center, -1);

            //配准样式初始化
            this.cboTransferMethod.Items.Add("线性配准(仿射变换)");
            this.cboTransferMethod.Items.Add("二项式配准");
            this.cboTransferMethod.Items.Add("三项式配准");
            this.cboTransferMethod.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void InitialToolbarControl()
        {
            //打开影像文件
            ToolOpenFile ToolOpenFileNew = new ToolOpenFile();
            ToolOpenFileNew.HookHelperExMap = m_HookHelperEx;
            ToolOpenFileNew.MapControlImage = this.axMapControl;
            ToolOpenFileNew.lvwPointInfo = this.lvwPointReference;
            this.axToolbarControl.AddItem(ToolOpenFileNew, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //放大
            this.axToolbarControl.AddItem(new MapZoomInTool(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //缩小
            this.axToolbarControl.AddItem(new MapZoomOutTool(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //固定比例放大
            this.axToolbarControl.AddItem(new MapZoomInFixedCommand(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //固定比例缩小
            this.axToolbarControl.AddItem(new MapZoomOutFixedCommand(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //平移
            this.axToolbarControl.AddItem(new MapPanTool(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //全图
            this.axToolbarControl.AddItem(new MapFullExtentCommand(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //前一范围
            this.axToolbarControl.AddItem(new MapZoomToLastExtentBackCommand(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //后一范围
            this.axToolbarControl.AddItem(new MapZoomToLastExtentForwardCommand(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //新增控制点
            ToolAddPoint ToolAddPointNew = new ToolAddPoint();
            ToolAddPointNew.HookHelperExMap = m_HookHelperEx;
            ToolAddPointNew.LvwPointInfo = this.lvwPointReference;
            ToolAddPointNew.frmGeoReference = this;
            this.axToolbarControl.AddItem(ToolAddPointNew, -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //编辑当前控制点
            ToolEditPoint ToolEditPointNew = new ToolEditPoint();
            ToolEditPointNew.HookHelperExMap = m_HookHelperEx;
            ToolEditPointNew.LvwPointInfo = this.lvwPointReference;
            ToolEditPointNew.frmGeoReference = this;
            this.axToolbarControl.AddItem(ToolEditPointNew, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //删除当前控制点
            ToolDeletePoint ToolDeletePointNew = new ToolDeletePoint();
            ToolDeletePointNew.HookHelperExMap = m_HookHelperEx;
            ToolDeletePointNew.LvwPointInfo = this.lvwPointReference;
            this.axToolbarControl.AddItem(ToolDeletePointNew, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //清除所有控制点
            ToolClearAllPoints ToolClearAllPointsNew = new ToolClearAllPoints();
            ToolClearAllPointsNew.HookHelperExMap = m_HookHelperEx;
            ToolClearAllPointsNew.LvwPointInfo = this.lvwPointReference;
            this.axToolbarControl.AddItem(ToolClearAllPointsNew, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
        }

        private void btnTolerance_Click(object sender, EventArgs e)
        {
            if (this.axMapControl.LayerCount == 0)
            {
                MessageBox.Show("请加载影像文件！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //判断控制点个数是否符合转换方式要求
            int iPointsCount = this.lvwPointReference.Items.Count;
            int iTransferMethod = this.cboTransferMethod.SelectedIndex;
            string strMessage = "";
            strMessage = this.CheckTransfer(iTransferMethod, iPointsCount);
            if (strMessage != "")
            {
                MessageBox.Show(strMessage, "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //检测地图点信息的完整性
            if (this.CheckMapPointInfor() == false)
            {
                MessageBox.Show("地图点配准信息不完整，无法完成容差计算！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //得到原点和目标点
            IPointCollection pSourcePoints = new PolygonClass();
            IPointCollection pTargetPoints = new PolygonClass();
            IPoint pPoint;
            int i;
            object Missing = System.Type.Missing;
            for (i = 0; i < iPointsCount; i++)
            {
                pPoint = new PointClass();
                pPoint.PutCoords(System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[1].Text),
                    System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[2].Text));
                pSourcePoints.AddPoint(pPoint, ref Missing, ref Missing);
                pPoint = new PointClass();
                pPoint.PutCoords(System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[3].Text),
                    System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[4].Text));
                pTargetPoints.AddPoint(pPoint, ref Missing, ref Missing);
            }
            //获取转换方式
            esriGeoTransTypeEnum pesriGeoTransTypeEnum;
            switch (iTransferMethod)
            {
                case 0:
                    pesriGeoTransTypeEnum = esriGeoTransTypeEnum.esriGeoTransPolyOrder1;
                    break;
                case 1:
                    pesriGeoTransTypeEnum = esriGeoTransTypeEnum.esriGeoTransPolyOrder2;
                    break;
                default:
                    pesriGeoTransTypeEnum = esriGeoTransTypeEnum.esriGeoTransPolyOrder3;
                    break;
            }
            //计算容差
            double[,] dSquareFit;
            IRasterGeometryProc pRasterGeometryProc = new RasterGeometryProcClass();
            dSquareFit = pRasterGeometryProc.LeastSquareFit(pSourcePoints, pTargetPoints, pesriGeoTransTypeEnum, true, false) as double[,];
            //显示容差
            string strSquareFit = "";
            for (i = 0; i < iPointsCount; i++)
            {
                strSquareFit =System.Convert.ToString(System.Math.Round(dSquareFit[i,6], 6));
                this.lvwPointReference.Items[i].SubItems[5].Text = strSquareFit;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.axMapControl.LayerCount == 0)
            {
                MessageBox.Show("请加载影像文件！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //判断控制点个数是否符合转换方式要求
            int iPointsCount = this.lvwPointReference.Items.Count;
            int iTransferMethod = this.cboTransferMethod.SelectedIndex;
            string strMessage = "";
            strMessage = this.CheckTransfer(iTransferMethod, iPointsCount);
            if (strMessage != "")
            {
                MessageBox.Show(strMessage, "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //检测地图点信息的完整性
            if (this.CheckMapPointInfor() == false)
            {
                MessageBox.Show("地图点配准信息不完整，无法完成配准！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //得到原点和目标点
            IPointCollection pSourcePoints = new PolygonClass();
            IPointCollection pTargetPoints = new PolygonClass();
            IPoint pPoint; 
            int i;
            object Missing = System.Reflection.Missing.Value;
            for (i = 0; i < iPointsCount; i++)
            {
                pPoint = new PointClass();
                pPoint.PutCoords(System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[1].Text),
                    System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[2].Text));
                pSourcePoints.AddPoint(pPoint, ref Missing, ref Missing);
                pPoint = new PointClass();
                pPoint.PutCoords(System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[3].Text),
                    System.Convert.ToDouble(lvwPointReference.Items[i].SubItems[4].Text));
                pTargetPoints.AddPoint(pPoint, ref Missing, ref Missing);
            }
            //获取栅格对象
            IRasterLayer pRasterLayer = this.axMapControl.get_Layer(0) as IRasterLayer;
            IRaster pRaster = (pRasterLayer.Raster as IRaster2).RasterDataset.CreateDefaultRaster();
            //获取转换方式
            esriGeoTransTypeEnum pesriGeoTransTypeEnum;
            switch (iTransferMethod)
            {
                case 0:
                    pesriGeoTransTypeEnum = esriGeoTransTypeEnum.esriGeoTransPolyOrder1;
                    break;
                case 1:
                    pesriGeoTransTypeEnum = esriGeoTransTypeEnum.esriGeoTransPolyOrder2;
                    break;
                default:
                    pesriGeoTransTypeEnum = esriGeoTransTypeEnum.esriGeoTransPolyOrder3;
                    break;
            }
            //影像配准
            IRasterGeometryProc pRasterGeometryProc = new RasterGeometryProcClass();
            pRasterGeometryProc.Warp(pSourcePoints, pTargetPoints, pesriGeoTransTypeEnum, pRaster);
            pRasterGeometryProc.Register(pRaster);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_GeoImageFullName = "";
            this.Close();
        }

        //判断控制点个数是否符合转换方式要求
        private string CheckTransfer(int iTransferMethod, int iPointsCount)
        {
            string strMessage = "";
            if (iTransferMethod ==0)
            {
                if (iPointsCount < 3)
                    strMessage = "[线性配准(仿射变换)]至少需要3个配准点,请确认!";
            }
            else if (iTransferMethod == 1)
            {
                if (iPointsCount < 6)
                    strMessage = "[二次多项式]配准至少需要6个配准点,请确认!";
            }
            else if (iTransferMethod == 2)
            {
                if (iPointsCount < 10)
                    strMessage = "[三次多项式]配准至少需要10个配准点,请确认!";
            }
            return strMessage;
        }

        //清除十字标志
        private void ClearAllElement()
        {
            //清除影像十字标志
            IGraphicsContainer pGraphicsContainer;
            IElement pElement;
            int i;
            if (m_InitialFlag == false)
            {
                pGraphicsContainer = this.axMapControl.Map as IGraphicsContainer;
                //用此方法避免删除绘画的其它地图要素
                for (i = 0; i < ConstVariant.ElementImage.Count; i++)
                {
                    pElement = ConstVariant.ElementImage[i];
                    pGraphicsContainer.DeleteElement(pElement);
                }
                this.axMapControl.ActiveView.Refresh();
            }
            //清除地图十字标志
            pGraphicsContainer = this.m_HookHelperEx.FocusMap as IGraphicsContainer;
            //只在地图窗口加载地图时清除十字标示
            if (m_HookHelperEx.FocusMap.LayerCount != 0)
            {
                for (i = 0; i < ConstVariant.ElementMap.Count; i++)
                {
                    pElement = ConstVariant.ElementMap[i];
                    pGraphicsContainer.DeleteElement(pElement);
                }
                this.m_HookHelperEx.ActiveView.Refresh();
            }
            //清空全体变量
            ConstVariant.ElementImage.Clear();
            ConstVariant.ElementMap.Clear();
        }

        private void frmGeoReference_FormClosed(object sender, FormClosedEventArgs e)
        {
            //退出影像配准状态
            ConstVariant.GeoReferState = false;
            ConstVariant.GeoEditPoint = false;
            int iLvwItemCount = this.lvwPointReference.Items.Count;
            if (iLvwItemCount == 0)
                return;
            //清除十字标志
            this.ClearAllElement();
        }

        //判断地图点信息是否缺失
        private bool CheckMapPointInfor()
        {
            bool bChecdedRight =true;
            for (int i = 0; i < this.lvwPointReference.Items.Count; i++)
            {
                if (this.lvwPointReference.Items[i].SubItems[3].Text == "" ||
                    this.lvwPointReference.Items[i].SubItems[4].Text == "")
                {
                    bChecdedRight = false;
                    break;
                }
            }
            return bChecdedRight;
        }

        private void lvwPointReference_DoubleClick(object sender, EventArgs e)
        {
            if (lvwPointReference.SelectedItems.Count != 1)
                return;

            IEnumerator pEnumerator = lvwPointReference.SelectedItems.GetEnumerator();
            pEnumerator.MoveNext();
            ListViewItem pListViewItem = pEnumerator.Current as ListViewItem;
            int iListViewItemIndex = lvwPointReference.Items.IndexOf(pListViewItem);
            string strImageX = pListViewItem.SubItems[1].Text;
            string strImageY = pListViewItem.SubItems[2].Text;
            string strMapX = pListViewItem.SubItems[3].Text;
            string strMapY = pListViewItem.SubItems[4].Text;
            //删除要编辑的地图十字标示
            IElement pElement = ConstVariant.ElementMap[iListViewItemIndex];
            //只在地图窗口加载地图时删除十字标示
            if (m_HookHelperEx.FocusMap.LayerCount != 0)
            {
                IGraphicsContainer pGraphicsContainer = this.m_HookHelperEx.FocusMap as IGraphicsContainer;
                pGraphicsContainer.DeleteElement(pElement);
                this.m_HookHelperEx.ActiveView.Refresh();
            }
            //删除该点的全体变量
            ConstVariant.ElementMap.Remove(pElement);
            //显示编辑窗体
            frmPointInfo frmPointInfoNew = new frmPointInfo();
            frmPointInfoNew.ShowInTaskbar = false;
            frmPointInfoNew.ImageX = strImageX;
            frmPointInfoNew.ImageY = strImageY;
            frmPointInfoNew.MapX = strMapX;
            frmPointInfoNew.MapY = strMapY;
            frmPointInfoNew.lvwPointInfo = pListViewItem;
            frmPointInfoNew.lvwPoint = this.lvwPointReference;
            frmPointInfoNew.frmGeoReference = this;
            ConstVariant.GeoEditPoint = true;
            frmPointInfoNew.OnCreate(this.m_HookHelperEx.Hook);
            frmPointInfoNew.ShowDialog();
        }
    }
}