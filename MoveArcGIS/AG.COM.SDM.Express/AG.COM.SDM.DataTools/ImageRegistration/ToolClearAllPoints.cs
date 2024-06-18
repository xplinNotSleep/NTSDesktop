using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    //清除所有控制点插件类
    internal class ToolClearAllPoints : BaseCommand
    {
        private HookHelper m_HookHelper = new HookHelperClass();
        private  HookHelperEx m_HookHelperExMap;
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
        public ToolClearAllPoints()
        {
            try
            {
                m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstImages + "ClearAllPoints.bmp"));
            }
            catch
            {
                m_bitmap = null;
            }
            finally
            {
                m_caption = "清除所有控制点";
                m_message = "清除所有控制点";
                m_name = "ToolClearAllPoints";
                m_toolTip = "清除所有控制点";
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (m_LvwPointInfo.Items.Count == 0)
                return;
            if (MessageBox.Show("确实删除所有控制点吗？", "提示！", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;
            //清除影像十字标志
            IGraphicsContainer pGraphicsContainer = m_HookHelper.FocusMap as IGraphicsContainer;
            IElement pElement;
            int i;
            for (i = 0; i < ConstVariant.ElementImage.Count; i++)
            {
                pElement = ConstVariant.ElementImage[i];
                pGraphicsContainer.DeleteElement(pElement);
            }
            m_HookHelper.ActiveView.Refresh();
            //清除地图十字标志
            //只在地图窗口加载地图时清除十字标示
            if (m_HookHelperExMap.FocusMap.LayerCount != 0)
            {
                pGraphicsContainer = m_HookHelperExMap.FocusMap as IGraphicsContainer;
                //用此方法避免删除绘画的其它地图要素
                for (i = 0; i < ConstVariant.ElementMap.Count; i++)
                {
                    pElement = ConstVariant.ElementMap[i];
                    pGraphicsContainer.DeleteElement(pElement);
                }
                m_HookHelperExMap.ActiveView.Refresh();
            }
            //清除列表点信息
            m_LvwPointInfo.Items.Clear();
            //清空全体变量
            ConstVariant.ElementImage.Clear();
            ConstVariant.ElementMap.Clear();
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
