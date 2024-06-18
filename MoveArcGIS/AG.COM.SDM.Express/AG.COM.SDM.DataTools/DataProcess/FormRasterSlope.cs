using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public partial class FormRasterSlope : Form
    {
        private IMap map = null;
        private IFeatureClass BaseSlopFeatureClass = null;
        private IRasterDataset OutRasterDataset = null;
        private string ReclassTabPath = @"D:\\reclass.dbf";
        private string FSlopeFilePath = @"D:\\";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="map">地图对象</param>
        public FormRasterSlope(IMap map)
        {
            InitializeComponent();
            this.map = map;

            CheckForPolygonLayersAndRasterLayers();             
        }

        /// <summary>
        /// 生成栅格坡度图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RasterSlope_Click(object sender, EventArgs e)
        {
            if (this.cmb_RasterLayers.Text == "请选择DEM图层"||this.cmb_RasterLayers.Text=="")
            {
                MessageBox.Show("请选择DEM图层", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RasterSlopeClass rsc = new RasterSlopeClass();
            //this.pgb_Status.
            IRasterLayer RLayer = rsc.GetSelectedRasterLayer(map, this.cmb_RasterLayers.Text);
            /// 生成栅格坡度图
            OutRasterDataset = rsc.CalcRasterSlope(RLayer, Convert.ToInt32(this.txb_cellSize.Text));
            ///是否将结果添加到当前地图
            if(this.ckb_AddRSToMap.Checked)
            {
                RasterLayer slopeRLayer = new RasterLayer();
                slopeRLayer.CreateFromDataset(OutRasterDataset);
                slopeRLayer.Name = "slope";

                map.AddLayer(slopeRLayer);
            }
        }

        private void btn_brsRCTab_Click(object sender, EventArgs e)
        {
            this.OpenFileDialog1.Filter = "重分类规则 (*.dbf)|*.dbf" ;
            this.OpenFileDialog1.RestoreDirectory = true;

            if (this.OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ReclassTabPath = this.OpenFileDialog1.FileName;
                this.txt_RSTablePath.Text = this.ReclassTabPath;
            }            

        }

        /// <summary>
        /// 根据土地坡度等级分类方法分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReClassSlope_Click(object sender, EventArgs e)
        {
            if(OutRasterDataset != null)
            {
                RasterSlopeClass rsc = new RasterSlopeClass();
                //根据规则将栅格坡度图重分类
                OutRasterDataset = rsc.ReclassSlopeRaster(OutRasterDataset, ReclassTabPath);
                ///是否将结果添加到当前地图
                if (this.ckb_AddCSToMap.Checked)
                {
                    RasterLayer slopeRLayer = new RasterLayer();
                    slopeRLayer.CreateFromDataset(OutRasterDataset);
                    slopeRLayer.Name = "slope_Reclass";

                    map.AddLayer(slopeRLayer);
                }
            }
        }

        /// <summary>
        /// 生成矢量坡度图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RS2FS_Click(object sender, EventArgs e)
        {
            if (OutRasterDataset != null)
            {
                RasterSlopeClass rsc = new RasterSlopeClass();

                //生成矢量坡度图，并保存到指定位置
                BaseSlopFeatureClass = rsc.SlopeRasterToPolygonShp(OutRasterDataset, FSlopeFilePath);

                ///是否将结果添加到当前地图
                if (this.ckb_AddFSToMap.Checked)
                {
                    FeatureLayer pFeatureLayer = new FeatureLayer();
                    pFeatureLayer.FeatureClass = BaseSlopFeatureClass;
                    pFeatureLayer.Name = BaseSlopFeatureClass.AliasName;

                    map.AddLayer(pFeatureLayer);
                }
            }
        }

        /// <summary>
        /// 计算各要素（多边形）坡度等级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CalcSlopeLevel_Click(object sender, EventArgs e)
        {            
            RasterSlopeClass rsc = new RasterSlopeClass();
            //获取需要计算坡度等级的矢量多边形图层
            IFeatureLayer ClipFeatureLayer = rsc.GetSelectedFeatureLayer(map, this.cmb_PolygonLayers.Text);

            if (BaseSlopFeatureClass != null)
            {
                //计算坡度等级
                rsc.CalcFeatureClassSlopeLevel(BaseSlopFeatureClass, ClipFeatureLayer.FeatureClass,this.txb_FieldName.Text.ToString());

                MessageBox.Show("完成坡度等级计算，存储在名为"+this.txb_FieldName.Text.ToString()+"的字段中");
            }
        }

        private void frmRasterSlope_Load(object sender, EventArgs e)
        {

        }

        private void btn_saveFSPath_Click(object sender, EventArgs e)
        {
            this.SaveFileDialog1.Filter   =   "ShapeFile|*.*";   
            this.SaveFileDialog1.RestoreDirectory = true;
            
            if(this.SaveFileDialog1.ShowDialog()   ==   DialogResult.OK)  
            {
                FSlopeFilePath = this.SaveFileDialog1.FileName;

                if (FSlopeFilePath.Contains("."))
                {
                    FSlopeFilePath = FSlopeFilePath.Substring(0, FSlopeFilePath.LastIndexOf("."));

                }
                this.txt_FSPath.Text = FSlopeFilePath;

                if (File.Exists(FSlopeFilePath + ".dbf") || File.Exists(FSlopeFilePath + ".shp")||File.Exists(FSlopeFilePath + ".shx"))
                {
                    FSlopeFilePath = FSlopeFilePath + "a";
                }
            }
        }

        private void txb_cellSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }   
        }

        private void CheckForPolygonLayersAndRasterLayers()
        {
            int layerCount = this.map.LayerCount;
            this.cmb_RasterLayers.Items.Clear();
            this.cmb_RasterLayers.Text = "请选择DEM图层";

            this.cmb_PolygonLayers.Items.Clear();
            this.cmb_PolygonLayers.Text = "请选择图层";

            for (int i = 0; i != layerCount; i++)
            {
                ILayer layer = this.map.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = (IFeatureLayer)layer;
                    if (layer2.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        this.cmb_PolygonLayers.Items.Add(layer2.Name);
                    }
                }
                else if (layer is IRasterLayer)
                {
                    IRasterLayer layer3 = (IRasterLayer)layer;
                    this.cmb_RasterLayers.Items.Add(layer3.Name);
                }
            }
        }
    }
}