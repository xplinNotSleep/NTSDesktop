using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace AG.COM.SDM.DataTools.ImageRegistration
{    
    /// <summary>
    /// 编辑当前控制点
    /// </summary>
    internal class ToolEditPoint : BaseCommand
    {
        private HookHelper m_HookHelper = new HookHelperClass();
        private HookHelperEx m_HookHelperExMap;
        private frmGeoReference m_frmGeoReference;
        private ListView m_LvwPointInfo;

        /// <summary>
        /// 设置通信类对象
        /// </summary>
        public HookHelperEx HookHelperExMap
        {
            set { m_HookHelperExMap = value; }
        }

        /// <summary>
        /// 设置影像图配准类对象
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
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ToolEditPoint()
        {
            try
            {
                m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstImages + "EditPoint.bmp"));
            }
            catch
            {
                m_bitmap = null;
            }
            finally
            {
                m_caption = "编辑当前控制点";
                m_message = "编辑当前控制点";
                m_name = "ToolEditPoint";
                m_toolTip = "编辑当前控制点";
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (m_LvwPointInfo.Items.Count == 0)
                return;
            if (m_LvwPointInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要编辑的控制点！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            IEnumerator pEnumerator = m_LvwPointInfo.SelectedItems.GetEnumerator();
            pEnumerator.MoveNext();
            ListViewItem pListViewItem = pEnumerator.Current as ListViewItem;
            int iListViewItemIndex = this.m_LvwPointInfo.Items.IndexOf(pListViewItem);
            string strImageX = pListViewItem.SubItems[1].Text;
            string strImageY = pListViewItem.SubItems[2].Text;
            string strMapX = pListViewItem.SubItems[3].Text;
            string strMapY = pListViewItem.SubItems[4].Text;
            //删除要编辑的地图十字标示
            IElement pElement = ConstVariant.ElementMap[iListViewItemIndex];
            //只在地图窗口加载地图时删除十字标示
            if (m_HookHelperExMap.FocusMap.LayerCount != 0)
            {
                IGraphicsContainer pGraphicsContainer = this.m_HookHelperExMap.FocusMap as IGraphicsContainer;
                pGraphicsContainer.DeleteElement(pElement);
                this.m_HookHelperExMap.ActiveView.Refresh();
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
            frmPointInfoNew.lvwPoint = this.m_LvwPointInfo;
            frmPointInfoNew.frmGeoReference = m_frmGeoReference;
            ConstVariant.GeoEditPoint = true;
            frmPointInfoNew.OnCreate(this.m_HookHelperExMap.Hook);
            frmPointInfoNew.ShowDialog();
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Hook = hook;
        }
    }
}
