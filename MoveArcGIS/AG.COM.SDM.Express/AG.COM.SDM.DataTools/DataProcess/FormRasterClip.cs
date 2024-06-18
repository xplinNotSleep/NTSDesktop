using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SpatialAnalyst;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// դ�����ݲü�������
    /// </summary>
    public partial class FormRasterClip : Form, ISelAreaForm
    {
        private IHookHelperEx m_hookHelperEx;
        private Dictionary<string, ILayer> m_dictLayers;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormRasterClip()
        {
            InitializeComponent();
            this.controlSelArea1.MainForm = this;
        }

        private void FormRasterClip_Load(object sender, EventArgs e)
        {
            //��ʼ���ֵ���
            m_dictLayers = new Dictionary<string, ILayer>();
            //��ȡ�Ѽ��ص�դ��ͼ��
            IEnumLayer tEnumLayer = GetRasterLayers();
            ILayer tLayer = tEnumLayer.Next();
            while (tLayer != null)
            {
                m_dictLayers.Add(tLayer.Name, tLayer);
                //ͼ����
                cboxSource.Items.Add(tLayer.Name);
                tLayer = tEnumLayer.Next();
            }

            ToolTip btnPathTip = new ToolTip();
            btnPathTip.SetToolTip(btnOpen, "����դ���������Ŀ¼");
        }

        /// <summary>
        /// ��ȡ�û�����ѡ��ؼ�
        /// </summary>
        public ControlSelArea SelArea
        {
            get { return this.controlSelArea1; }
        }

        /// <summary>
        /// �������м��ص�դ��ͼ��
        /// </summary>
        /// <returns></returns>
        private IEnumLayer GetRasterLayers()
        {
            IMap tMap = m_hookHelperEx.ActiveView.FocusMap;
            UID uid = new UIDClass();
            uid.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";   //RasterLayer
            IEnumLayer layers = tMap.get_Layers(uid, true);
            return layers;
        }

        /// <summary>
        /// ����Hook����
        /// </summary>
        /// <param name="pHook"></param>
        public void SetHook(IHookHelperEx pHook)
        {
            m_hookHelperEx = pHook;
            this.controlSelArea1.SetHook2(pHook);
        }

        /// <summary>
        /// Raster�ü�
        /// </summary>
        /// <param name="pGeoDataset">����դ������Դ</param>
        /// <param name="pGeometry">�ü�ͼ��</param>
        /// <param name="pSelectType">ͼ������</param>
        /// <returns></returns>
        private IGeoDataset RasterClip(IGeoDataset pGeoDataset, IGeometry pGeometry, AreaSelectType pSelectType)
        {
            //��Ϊ tExtractionOp.Polygon��������Բʱ��bug�����Ҫ����Ϊ��ͨ�����
            if (pSelectType == AreaSelectType.TYPE_CIRCLE)
            {
                IPolycurve tPolycurve = pGeometry as IPolycurve;
                tPolycurve.Densify(0.1, 0.01);
            }

            IExtractionOp tExtractionOp = new RasterExtractionOpClass();
            return tExtractionOp.Polygon(pGeoDataset, pGeometry as IPolygon, true);
        }

        /// <summary>
        /// ��ɫ����
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pOutput"></param>
        /// <returns></returns>
        private IRaster AddColormap(IRasterLayer pInput, IGeoDataset pOutput)
        {
            IRaster tOutRaster = null;
            if (pOutput is IRaster2)
            {
                tOutRaster = pOutput as IRaster;
            }
            else if(pOutput is IRasterLayer)
            {
                IRasterLayer tRasterLayer = pOutput as IRasterLayer;
                tOutRaster = tRasterLayer.Raster;
            }
            else if(pOutput is IRasterDataset)
            {
                IRasterDataset rasterDataset = pOutput as IRasterDataset;
                tOutRaster = rasterDataset.CreateDefaultRaster();
            }
            //������ɫģ��
            IRasterLayer tOutRasterLayer = new RasterLayerClass();
            tOutRasterLayer.CreateFromRaster(tOutRaster);
            tOutRasterLayer.Renderer = pInput.Renderer;
            tOutRasterLayer.Renderer.Update();

            return tOutRaster;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            //�������·��
            SaveFileDialog tSfd = new SaveFileDialog();
            tSfd.Title = @"����ļ�";
            tSfd.Filter = @"TIFF�ļ�(*.tif)|*.tif";
            if (tSfd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = tSfd.FileName;
            }
        }

        private void btnClip_Click(object sender, EventArgs e)
        {
            if (this.cboxSource.Text == string.Empty)
            {
                MessageBox.Show("��ѡ��դ�����ݼ�", "դ�����ݲü�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.txtPath.Text == string.Empty)
            {
                MessageBox.Show("��ѡ�񱣴�·��", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TrackProgressDialog tProgress = new TrackProgressDialog();
            tProgress.DisplayTotal = false;
            tProgress.SubMax = 3;
            tProgress.TopMost = true;
            tProgress.Show();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //��ȡ��������Դ
                ILayer tLayer = m_dictLayers[cboxSource.SelectedItem.ToString()];
                IRasterLayer tRasterLayer = tLayer as IRasterLayer;
                IRaster tRaster = tRasterLayer.Raster;
                IGeoDataset tInputGeo = tRaster as IGeoDataset;

                tProgress.SubValue = 1;
                tProgress.SubMessage = "����դ�����ݡ���";
                Application.DoEvents();

                //�ü�����
                IGeoDataset tOutputGeo = RasterClip(tInputGeo, controlSelArea1.RegionGeometry, controlSelArea1.SelectType);
                if (tOutputGeo == null)
                {
                    MessageHandler.ShowInfoMsg("�ü�ʧ��", Text);
                }
                
                //����ü����
                if (!tProgress.IsContinue)
                {
                    this.Close();
                    return;
                }
                    
                tProgress.SubValue = 2;
                tProgress.SubMessage = "����դ�����ݲ�����ɫ����";
                Application.DoEvents();

                IRaster clipRaster = AddColormap(tRasterLayer, tOutputGeo);
                IWorkspaceFactory tWsf = new RasterWorkspaceFactoryClass();

                IWorkspace tWorkspace = tWsf.OpenFromFile(System.IO.Path.GetDirectoryName(txtPath.Text), 0);
                ISaveAs2 tSaveAs = clipRaster as ISaveAs2;
                tSaveAs.SaveAs(System.IO.Path.GetFileName(txtPath.Text), tWorkspace, "TIFF");

                tProgress.SubValue = 3;
                tProgress.SubMessage = "�ü��ɹ�����";
                Application.DoEvents();

                if (MessageBox.Show("�ü��ɹ����Ƿ�����ü�", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageHandler.ShowErrorMsg(ex);
            }
            finally
            {
                tProgress.AutoFinishClose = true;
                tProgress.SetFinish();
                this.Cursor = Cursors.Default;
            }
        }

        private void FormRasterClip_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.controlSelArea1.ExitSelect();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}