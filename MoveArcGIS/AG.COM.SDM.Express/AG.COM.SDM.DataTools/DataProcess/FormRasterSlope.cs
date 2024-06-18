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
        /// ���캯��
        /// </summary>
        /// <param name="map">��ͼ����</param>
        public FormRasterSlope(IMap map)
        {
            InitializeComponent();
            this.map = map;

            CheckForPolygonLayersAndRasterLayers();             
        }

        /// <summary>
        /// ����դ���¶�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RasterSlope_Click(object sender, EventArgs e)
        {
            if (this.cmb_RasterLayers.Text == "��ѡ��DEMͼ��"||this.cmb_RasterLayers.Text=="")
            {
                MessageBox.Show("��ѡ��DEMͼ��", "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RasterSlopeClass rsc = new RasterSlopeClass();
            //this.pgb_Status.
            IRasterLayer RLayer = rsc.GetSelectedRasterLayer(map, this.cmb_RasterLayers.Text);
            /// ����դ���¶�ͼ
            OutRasterDataset = rsc.CalcRasterSlope(RLayer, Convert.ToInt32(this.txb_cellSize.Text));
            ///�Ƿ񽫽����ӵ���ǰ��ͼ
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
            this.OpenFileDialog1.Filter = "�ط������ (*.dbf)|*.dbf" ;
            this.OpenFileDialog1.RestoreDirectory = true;

            if (this.OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ReclassTabPath = this.OpenFileDialog1.FileName;
                this.txt_RSTablePath.Text = this.ReclassTabPath;
            }            

        }

        /// <summary>
        /// ���������¶ȵȼ����෽������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReClassSlope_Click(object sender, EventArgs e)
        {
            if(OutRasterDataset != null)
            {
                RasterSlopeClass rsc = new RasterSlopeClass();
                //���ݹ���դ���¶�ͼ�ط���
                OutRasterDataset = rsc.ReclassSlopeRaster(OutRasterDataset, ReclassTabPath);
                ///�Ƿ񽫽����ӵ���ǰ��ͼ
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
        /// ����ʸ���¶�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RS2FS_Click(object sender, EventArgs e)
        {
            if (OutRasterDataset != null)
            {
                RasterSlopeClass rsc = new RasterSlopeClass();

                //����ʸ���¶�ͼ�������浽ָ��λ��
                BaseSlopFeatureClass = rsc.SlopeRasterToPolygonShp(OutRasterDataset, FSlopeFilePath);

                ///�Ƿ񽫽����ӵ���ǰ��ͼ
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
        /// �����Ҫ�أ�����Σ��¶ȵȼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CalcSlopeLevel_Click(object sender, EventArgs e)
        {            
            RasterSlopeClass rsc = new RasterSlopeClass();
            //��ȡ��Ҫ�����¶ȵȼ���ʸ�������ͼ��
            IFeatureLayer ClipFeatureLayer = rsc.GetSelectedFeatureLayer(map, this.cmb_PolygonLayers.Text);

            if (BaseSlopFeatureClass != null)
            {
                //�����¶ȵȼ�
                rsc.CalcFeatureClassSlopeLevel(BaseSlopFeatureClass, ClipFeatureLayer.FeatureClass,this.txb_FieldName.Text.ToString());

                MessageBox.Show("����¶ȵȼ����㣬�洢����Ϊ"+this.txb_FieldName.Text.ToString()+"���ֶ���");
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
            this.cmb_RasterLayers.Text = "��ѡ��DEMͼ��";

            this.cmb_PolygonLayers.Items.Clear();
            this.cmb_PolygonLayers.Text = "��ѡ��ͼ��";

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