using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    //删除当前控制点
    internal class ToolDeletePoint : BaseCommand
    {
        private HookHelper m_HookHelper = new HookHelperClass();
        private HookHelperEx m_HookHelperExMap;
        private ListView m_LvwPointInfo;

        /// <summary>
        /// 设置通信类对象
        /// </summary>
        public HookHelperEx HookHelperExMap
        {
            set { m_HookHelperExMap = value; }
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
        public ToolDeletePoint()
        {
            try
            {
                m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstImages + "DeletePoint.bmp"));
            }
            catch
            {
                m_bitmap = null;
            }
            finally
            {
                m_caption = "删除当前控制点";
                m_message = "删除当前控制点";
                m_name = "ToolDeletePoint";
                m_toolTip = "删除当前控制点";
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (m_LvwPointInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的控制点！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //获取选择项并删除
            IEnumerator pEnumerator = m_LvwPointInfo.SelectedItems.GetEnumerator();
            pEnumerator.MoveNext();
            ListViewItem pListViewItem = pEnumerator.Current as ListViewItem;
            int pListViewItemIndex = m_LvwPointInfo.Items.IndexOf(pListViewItem);
            m_LvwPointInfo.Items.RemoveAt(pListViewItemIndex);
            //修改点标号
            for (int i = pListViewItemIndex; i < m_LvwPointInfo.Items.Count; i++)
            {
                m_LvwPointInfo.Items[i].Text =System.Convert.ToString(i + 1);
            }
            //删除影像十字标记
            IElement pElement = ConstVariant.ElementImage[pListViewItemIndex];
            IGraphicsContainer pGraphicsContainer = m_HookHelper.FocusMap as IGraphicsContainer;
            pGraphicsContainer.DeleteElement(pElement);
            m_HookHelper.ActiveView.Refresh();
            ConstVariant.ElementImage.Remove(pElement);
            //删除地图十字标记
            //只有在地图窗口有地图时才画地图十字标示
            pElement = ConstVariant.ElementMap[pListViewItemIndex];
            if (m_HookHelperExMap.FocusMap.LayerCount != 0)
            {
                pGraphicsContainer = m_HookHelperExMap.FocusMap as IGraphicsContainer;
                pGraphicsContainer.DeleteElement(pElement);
                m_HookHelperExMap.ActiveView.Refresh();
            }
            ConstVariant.ElementMap.Remove(pElement);
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
