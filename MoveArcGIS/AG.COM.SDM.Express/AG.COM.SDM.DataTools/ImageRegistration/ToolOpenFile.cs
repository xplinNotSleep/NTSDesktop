using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    /// <summary>
    /// 打开影像文件插件类
    /// </summary>
    internal class ToolOpenFile : BaseCommand
    {
        private HookHelperEx m_HookHelperExMap;
        private AxMapControl m_MapControlImage;
        private ListView m_lvwPointInfo;

        /// <summary>
        /// 设置通信类
        /// </summary>
        public HookHelperEx HookHelperExMap
        {
            set { m_HookHelperExMap = value; }
        }

        /// <summary>
        /// 设置影像容器
        /// </summary>
        public AxMapControl MapControlImage
        {
            set { m_MapControlImage = value; }
        }

        /// <summary>
        /// 设置点信息列表
        /// </summary>
        public ListView lvwPointInfo
        {
            set { m_lvwPointInfo = value; }
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get { return true; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ToolOpenFile()
        {
            try
            {
                m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstImages + "OpenFile.bmp"));
            }
            catch
            {
                m_bitmap = null;
            }
            finally
            {
                m_caption = "打开影像文件";
                m_message = "打开影像文件";
                m_name = "ToolOpenFile";
                m_toolTip = "打开影像文件";
            }
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            OpenFileDialog OpenFileDialogNew = new OpenFileDialog();
            OpenFileDialogNew.Multiselect = false;
            OpenFileDialogNew.RestoreDirectory = true;
            OpenFileDialogNew.Title = "请选择要配准的影像图";
            OpenFileDialogNew.Filter = "栅格图(*.gif;*.jpg;*.tif)|*.gif;*.jpg;*.tif";
            string strImageFileName = "";
            if (OpenFileDialogNew.ShowDialog() == DialogResult.OK)
            {
                ConstVariant.GeoReferState = true;
                //清除十字标示及初始化全体变量
                this.InitialGeoReference();

                strImageFileName = OpenFileDialogNew.FileName;
                if (File.Exists(strImageFileName) == false)
                    MessageBox.Show("影像文件不存在！", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_MapControlImage.ClearLayers();
                IRasterLayer pRasterLayer = new RasterLayerClass();
                pRasterLayer.CreateFromFilePath(strImageFileName);
                m_MapControlImage.AddLayer(pRasterLayer);
            }
        }

        /// <summary>
        /// 清除十字标示及初始化全体变量
        /// </summary>
        private void InitialGeoReference()
        {
            IGraphicsContainer pGraphicsContainer;
            IElement pElement;
            int i;
            //清除影像十字标志
            pGraphicsContainer = this.m_MapControlImage.Map as IGraphicsContainer;
            //用此方法避免删除绘画的其它地图要素
            for (i = 0; i < ConstVariant.ElementImage.Count; i++)
            {
                pElement = ConstVariant.ElementImage[i];
                pGraphicsContainer.DeleteElement(pElement);
            }
            this.m_MapControlImage.ActiveView.Refresh();
            //清除地图十字标志
            pGraphicsContainer = this.m_HookHelperExMap.FocusMap as IGraphicsContainer;
            //只在地图窗口加载地图时清除十字标示
            if (m_HookHelperExMap.FocusMap.LayerCount != 0)
            {
                for (i = 0; i < ConstVariant.ElementMap.Count; i++)
                {
                    pElement = ConstVariant.ElementMap[i];
                    pGraphicsContainer.DeleteElement(pElement);
                }
                this.m_HookHelperExMap.ActiveView.Refresh();
            }
            //清空点信息列表
            this.m_lvwPointInfo.Items.Clear();
            //清空全体变量
            ConstVariant.ElementImage.Clear();
            ConstVariant.ElementMap.Clear();
        }
    }
}
