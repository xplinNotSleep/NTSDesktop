using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using AG.COM.SDM.Framework;


namespace AG.COM.SDM.DataTools.ImageRegistration
{
    public partial class frmPointInfo : Form,ITool,ICommand
    {
        private Cursor m_CursorDrawPoint;
        public frmPointInfo()
        {
            InitializeComponent();
            //鼠标样式
            m_CursorDrawPoint = new Cursor(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstCursors + "AddPoint.cur"));
        }

        private string m_ImageX;
        public string ImageX
        {
            set { m_ImageX = value; }
        }

        private string m_ImageY;
        public string ImageY
        {
            set { m_ImageY = value; }
        }

        private string m_MapX;
        public string MapX
        {
            set { m_MapX = value; }
        }

        private string m_MapY;
        public string MapY
        {
            set { m_MapY = value; }
        }

        private frmGeoReference m_frmGeoReference;
        public frmGeoReference frmGeoReference
        {
            set { m_frmGeoReference = value; }
        }

        private ListViewItem m_lvwItemEdit;
        private int m_lvwItemEditIndex;
        private ListView m_lvwPoint;
        public ListView lvwPoint
        {
            set { m_lvwPoint = value; }
        }

        private ListViewItem m_lvwPointInfo;
        public ListViewItem lvwPointInfo
        {
            set { m_lvwPointInfo = value; }
        }

        //影像
        private IHookHelper m_HookHelper;
        public IHookHelper HookHelper
        {
            set { m_HookHelper = value; }
        }

        //地图
        private HookHelperEx m_HookHelperEx=new HookHelperEx();
        private void frmPointInfo_Load(object sender, EventArgs e)
        {
            this.txtImageX.Text = m_ImageX;
            this.txtImageY.Text = m_ImageY;
            this.txtMapX.Text = m_MapX;
            this.txtMapY.Text = m_MapY;
            this.txtImageX.Enabled = false;
            this.txtImageY.Enabled = false;
            //是否可从地图获取坐标信息取决于地图窗口是否加载地图
            this.btnFromMap.Enabled = (this.m_HookHelperEx.FocusMap.LayerCount != 0) ? true : false;
        }

        private void btnFromMap_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_frmGeoReference.Hide();
        }

        #region ITool 成员

        public new int Cursor
        {
            get 
            {
                if (m_CursorDrawPoint != null && ConstVariant.GeoReferState ==true)
                    return m_CursorDrawPoint.Handle.ToInt32();
                else
                    return 0;
            }
        }

        public new bool Deactivate()
        {
            if (m_HookHelperEx != null)
                this.m_HookHelperEx.MapService.CurrentTool = this;
            return true;
        }

        public bool OnContextMenu(int x, int y)
        {
            return false;
        }

        public void OnDblClick()
        {
        }

        public void OnKeyDown(int keyCode, int shift)
        {
        }

        public void OnKeyUp(int keyCode, int shift)
        {
        }

        private bool m_MouseDown = false;
        public void OnMouseDown(int button, int shift, int x, int y)
        {
            //不是影像配准状态：返回
            if (ConstVariant.GeoReferState == false)
                return;
            //防止在地图上多点控制点
            m_MouseDown = false;
            if (ConstVariant.ElementMap.Count == ConstVariant.ElementImage.Count)
            {
                m_MouseDown = true;
                return;
            }
            //获取选择项
            if (ConstVariant.GeoEditPoint == true && m_lvwPoint.SelectedItems.Count !=0)
            {
                IEnumerator pEnumerator = m_lvwPoint.SelectedItems.GetEnumerator();
                pEnumerator.MoveNext();
                m_lvwItemEdit = pEnumerator.Current as ListViewItem;
                m_lvwItemEditIndex = m_lvwPoint.Items.IndexOf(m_lvwItemEdit);
            }
            //获取点值
            IPoint pPoint = this.m_HookHelperEx.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
            decimal dX = System.Math.Round((decimal)pPoint.X, 6);
            decimal dY = System.Math.Round((decimal)pPoint.Y, 6);
            this.m_MapX = dX.ToString();
            this.m_MapY = dY.ToString();
            //画出十字标记
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
            pSymbol.Color = pRgbColor as IColor;
            pSymbol.Size =20;
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSCross;
            IMarkerElement pMarkerElement = new MarkerElementClass();
            pMarkerElement.Symbol = pSymbol;
            IElement pElement = pMarkerElement as IElement;
            pElement.Geometry = pPoint;
            //只有在地图窗口有地图时才画地图十字标示
            if (m_HookHelperEx.FocusMap.LayerCount != 0)
            {
                IGraphicsContainer pGraphicsContainer = m_HookHelperEx.FocusMap as IGraphicsContainer;
                pGraphicsContainer.AddElement(pElement, 0);
                m_HookHelperEx.ActiveView.Refresh();
            }
            //存储十字标记元素
            if (ConstVariant.GeoEditPoint ==false)          //默认添加到末尾(添加)
                ConstVariant.ElementMap.Add(pElement);
            else                                            //插入到指定位置(编辑中从地图获取)
                ConstVariant.ElementMap.Insert(m_lvwItemEditIndex, pElement);
        }

        public void OnMouseMove(int button, int shift, int x, int y)
        {
        }

        public void OnMouseUp(int button, int shift, int x, int y)
        {
            //不是影像配准状态：返回
            if (ConstVariant.GeoReferState == false)
                return;
            //防止在地图上多点控制点
            if (m_MouseDown == true)
                return;
            this.m_frmGeoReference.Show();
            this.ShowDialog();
            //变量恢复
            this.m_MapX = "";
            this.m_MapY = "";
        }

        public void Refresh(int hdc)
        {
        }

        #endregion

        #region ICommand 成员

        public int Bitmap
        {
            get { return 0; }
        }

        public string Caption
        {
            get { return ""; }
        }

        public string Category
        {
            get { return ""; }
        }

        public bool Checked
        {
            get { return false; }
        }

        public int HelpContextID
        {
            get { return 0; }
        }

        public string HelpFile
        {
            get { return ""; }
        }

        public string Message
        {
            get { return ""; }
        }

        public void OnClick()
        {            
        }

        public void OnCreate(object hook)
        {
            this.m_HookHelperEx.Hook = hook;
            this.m_HookHelperEx.MapService.CurrentTool = this;
        }

        public string Tooltip
        {
            get { return ""; }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool bMapPointDrawed = (ConstVariant.ElementImage.Count == ConstVariant.ElementMap.Count) ? true : false;
            IGraphicsContainer pGraphicsContainer;
            //添加状态
            if (ConstVariant.GeoEditPoint == false)
            {
                //删除刚才绘画的影像控制点标示及列表信息
                pGraphicsContainer = m_HookHelper.FocusMap as IGraphicsContainer;
                IElement pElement = ConstVariant.ElementImage[ConstVariant.ElementImage.Count - 1];
                pGraphicsContainer.DeleteElement(pElement);
                m_HookHelper.ActiveView.Refresh();
                this.m_lvwPoint.Items.Remove(this.m_lvwPointInfo);
                //移除影像全体变量
                ConstVariant.ElementImage.Remove(pElement);
                //如果画了地图控制点标示，移除地图控制点标示
                if (bMapPointDrawed == true)
                {
                    pElement = ConstVariant.ElementMap[ConstVariant.ElementMap.Count - 1];
                    //只有在地图窗口有地图时才删除地图十字标示
                    if (m_HookHelperEx.FocusMap.LayerCount != 0)
                    {
                        pGraphicsContainer = m_HookHelperEx.FocusMap as IGraphicsContainer;
                        pGraphicsContainer.DeleteElement(pElement);
                        m_HookHelperEx.ActiveView.Refresh();
                    }
                    //移除地图全体变量
                    ConstVariant.ElementMap.Remove(pElement);
                }
            }
            //编辑状态
            else if (ConstVariant.GeoEditPoint == true)
            {
                //获取选择项
                if ( m_lvwPoint.SelectedItems.Count != 0)
                {
                    IEnumerator pEnumerator = m_lvwPoint.SelectedItems.GetEnumerator();
                    pEnumerator.MoveNext();
                    m_lvwItemEdit = pEnumerator.Current as ListViewItem;
                    m_lvwItemEditIndex = m_lvwPoint.Items.IndexOf(m_lvwItemEdit);
                }
                IElement pElement;
                //如果画了地图控制点标示，移除地图控制点标示
                if (bMapPointDrawed == true)
                {
                    pElement = ConstVariant.ElementMap[ConstVariant.ElementMap.Count - 1];
                    //只有在地图窗口有地图时才删除地图十字标示
                    if (m_HookHelperEx.FocusMap.LayerCount != 0)
                    {
                        pGraphicsContainer = m_HookHelperEx.FocusMap as IGraphicsContainer;
                        pGraphicsContainer.DeleteElement(pElement);
                        m_HookHelperEx.ActiveView.Refresh();
                    }
                    //移除地图全体变量
                    ConstVariant.ElementMap.Remove(pElement);
                }
                //绘制原来的地图控制点标示并添加全体变量
                //设置符号样式
                IRgbColor pRgbColor = new RgbColorClass();
                pRgbColor.Red = 255;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbol();
                pSymbol.Color = pRgbColor as IColor;
                pSymbol.Style = esriSimpleMarkerStyle.esriSMSCross;
                pSymbol.Size = 20;
                IPoint pPoint;
                IMarkerElement pMarkerElement;
                //点位置确定
                pPoint = new PointClass();
                pPoint.PutCoords(System.Convert.ToDouble(this.m_lvwItemEdit.SubItems[3].Text),
                    System.Convert.ToDouble(this.m_lvwItemEdit.SubItems[4].Text));
                //设置要添加的元素
                pMarkerElement = new MarkerElementClass();
                pMarkerElement.Symbol = pSymbol;
                pElement = pMarkerElement as IElement;
                pElement.Geometry = pPoint;
                //只有在地图窗口有地图时才画地图十字标示
                if (m_HookHelperEx.FocusMap.LayerCount != 0)
                {
                    pGraphicsContainer = this.m_HookHelperEx.FocusMap as IGraphicsContainer;
                    pGraphicsContainer.AddElement(pElement, 0);
                    this.m_HookHelperEx.ActiveView.Refresh();
                }
                //添加元素到全体变量
                ConstVariant.ElementMap.Insert(m_lvwItemEditIndex, pElement);
                ConstVariant.GeoEditPoint = false;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private string CheckMapPointInfo(string X, string Y)
        {
            string sMessage = "";
            if (X == "" || Y == "")
                return sMessage = "地图点信息不完整！";
            if (ConstVariant.IsNumeric(X) == false || ConstVariant.IsNumeric(Y) == false)
                return sMessage = "点坐标信息格式不正确！";
            return sMessage;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //检测地图点信息的正确性
            string sMessage = CheckMapPointInfo(this.txtMapX.Text.Trim(), this.txtMapY.Text.Trim());
            if (sMessage != "")
            {
                MessageBox.Show(sMessage, "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Hide();
            //设置符号样式
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbol();
            pSymbol.Color = pRgbColor as IColor;
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSCross;
            pSymbol.Size = 20;
            IPoint pPoint;
            IMarkerElement pMarkerElement;
            IElement pElement;
            IGraphicsContainer pGraphicsContainer;
            //添加点
            if (ConstVariant.GeoEditPoint ==false)
            {
                this.m_lvwPointInfo.SubItems[3].Text = this.txtMapX.Text.Trim();
                this.m_lvwPointInfo.SubItems[4].Text = this.txtMapY.Text.Trim();
                //手工输入地图点坐标信息则增加地图点标示
                if (ConstVariant.ElementImage.Count > ConstVariant.ElementMap.Count)
                {
                    //点位置确定
                    pPoint = new PointClass();
                    pPoint.PutCoords(System.Convert.ToDouble(this.txtMapX.Text), System.Convert.ToDouble(this.txtMapY.Text));
                    //设置要添加的元素
                    pMarkerElement = new MarkerElementClass();
                    pMarkerElement.Symbol = pSymbol;
                    pElement = pMarkerElement as IElement;
                    pElement.Geometry = pPoint;
                    //只有在地图窗口有地图时才画地图十字标示
                    if (m_HookHelperEx.FocusMap.LayerCount != 0)
                    {
                        pGraphicsContainer = this.m_HookHelperEx.FocusMap as IGraphicsContainer;
                        pGraphicsContainer.AddElement(pElement, 0);
                        this.m_HookHelperEx.ActiveView.Refresh();
                    }
                    //添加元素到全体变量
                    ConstVariant.ElementMap.Insert(m_lvwItemEditIndex, pElement);
                }
            }
            //编辑点
            else
            {
                //获取选择项
                if (ConstVariant.GeoEditPoint == true && m_lvwPoint.SelectedItems.Count != 0)
                {
                    IEnumerator pEnumerator = m_lvwPoint.SelectedItems.GetEnumerator();
                    pEnumerator.MoveNext();
                    m_lvwItemEdit = pEnumerator.Current as ListViewItem;
                    m_lvwItemEditIndex = m_lvwPoint.Items.IndexOf(m_lvwItemEdit);
                }
                //修改点信息
                this.m_lvwItemEdit.SubItems[3].Text = this.txtMapX.Text;
                this.m_lvwItemEdit.SubItems[4].Text = this.txtMapY.Text;
                //设置编辑状态标示结束
                ConstVariant.GeoEditPoint = false;
                //添加地图点十字标示(如果从地图获取则已经添加,这里只在键盘输入方式添加)
                if (ConstVariant.ElementImage.Count == ConstVariant.ElementMap.Count)
                    return;
                else if (ConstVariant.ElementImage.Count > ConstVariant.ElementMap.Count)
                {
                    //点位置确定
                    pPoint = new PointClass();
                    pPoint.PutCoords(System.Convert.ToDouble(this.txtMapX.Text), System.Convert.ToDouble(this.txtMapY.Text));
                    //设置要添加的元素
                    pMarkerElement = new MarkerElementClass();
                    pMarkerElement.Symbol = pSymbol;
                    pElement = pMarkerElement as IElement;
                    pElement.Geometry = pPoint;
                    //只有在地图窗口有地图时才画地图十字标示
                    if (m_HookHelperEx.FocusMap.LayerCount != 0)
                    {
                        pGraphicsContainer = this.m_HookHelperEx.FocusMap as IGraphicsContainer;
                        pGraphicsContainer.AddElement(pElement, 0);
                        this.m_HookHelperEx.ActiveView.Refresh();
                    }
                    //添加元素到全体变量
                    ConstVariant.ElementMap.Insert(m_lvwItemEditIndex, pElement);
                }
            }
        }
    }
}