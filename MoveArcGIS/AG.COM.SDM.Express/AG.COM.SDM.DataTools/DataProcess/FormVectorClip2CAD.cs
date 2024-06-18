using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AG.COM.SDM.CADManager;
using AG.COM.SDM.CADManager.Convert;
using AG.COM.SDM.DataTools.Conversion;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// ʸ�����ݲü�������
    /// </summary>
    public partial class FormVectorClip2CAD : Form, ISelAreaForm
    {
        private IHookHelperEx m_hookHelperEx;

        public FormVectorClip2CAD()
        {
            InitializeComponent();
            this.controlSelArea1.MainForm = this;
        }

        private IEnumLayer GetFeatureLayers()
        {
            IMap tMap = m_hookHelperEx.ActiveView.FocusMap;
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";//FeatureLayer
            IEnumLayer layers = tMap.get_Layers(uid, true);
            return layers;
        }

        public ControlSelArea SelArea
        {
            get { return this.controlSelArea1; }
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

        private void FormVectorClip_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.controlSelArea1.ExitSelect();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.controlSelArea1.RegionGeometry == null)
            {
                MessageBox.Show("����ѡ��ü���Χ");
                return;
            }

            if (string.IsNullOrEmpty(txtOutput.Text))
            {
                MessageBox.Show("����ѡ�񵼳�CAD�ļ�");
                return;
            }            

            List<ILayer> tLayers = GetSelectLayer(ltvLayer.Nodes);
            if (tLayers.Count < 1)
            {
                MessageBox.Show("����ѡ��ͼ��");
                return;
            }

            string CADTemplateFile = cmbCADTemplate.Text;
            if (string.IsNullOrEmpty(CADTemplateFile))
            {
                MessageBox.Show("����ѡ��CADģ��");
                return;
            }

            string CADVersion = cmbCADVersion.Text;
            if (string.IsNullOrEmpty(CADVersion))
            {
                MessageBox.Show("����ѡ�񵼳�CAD�汾");
                return;
            }

            EnabledAllControl(false);

            VectorClip2CADHelper tVectorClip2CADHelper = new VectorClip2CADHelper(txtOutput.Text, tLayers,
                controlSelArea1.RegionGeometry, CADTemplateFile, m_hookHelperEx.ActiveView, chkExportElement.Checked, CADVersion);
            if (tVectorClip2CADHelper.MainProcess() == true)
            {
                Close();
            }

            EnabledAllControl(true);
        }

        /// <summary>
        /// ��ȡѡ���ͼ��
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <returns></returns>
        public List<ILayer> GetSelectLayer(TreeNodeCollection tNodeColl)
        {
            List<ILayer> tLayers = new List<ILayer>();

            AddSelectLayerToList(tNodeColl, tLayers);

            return tLayers;
        }

        /// <summary>
        /// ��ѡ���ͼ����ӵ�һ��list
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <param name="tLayers"></param>
        private void AddSelectLayerToList(TreeNodeCollection tNodeColl, List<ILayer> tLayers)
        {
            foreach (TreeNode tNode in tNodeColl)
            {
                if (tNode.Tag is IFeatureLayer && tNode.Checked == true)
                {
                    tLayers.Add(tNode.Tag as IFeatureLayer);
                }

                AddSelectLayerToList(tNode.Nodes, tLayers);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormVectorClip_Load(object sender, EventArgs e)
        {
            try
            {
                //��ʼ��CAD�汾��Ϣ
                CADVersionHelper.InitVersionInfo();
                //���CAD�Ƿ�װ
                if (CADVersionHelper.CheckCurrentVersionIsInstall(true) == false)
                {
                    Close();
                    return;
                }

                ///��ʼ��ͼ����
                this.ltvLayer.InitUI(this.m_hookHelperEx.FocusMap as IBasicMap);

                //���CADģ���ļ�
                cmbCADTemplate.DataSource = ConvertManagerHelper.GetAllCADTemplateFile();
                if (cmbCADTemplate.Items.Count > 0)
                    cmbCADTemplate.SelectedIndex = 0;

                BindCADVersion();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");   
            }
        }

        /// <summary>
        /// �󶨵���CAD�汾
        /// </summary>
        private void BindCADVersion()
        {
            List<string> tCADVersion = CADVersionHelper.GetCanSaveVersionText();
            cmbCADVersion.DataSource = tCADVersion;

            if (cmbCADVersion.Items.Count > 0)
            {
                cmbCADVersion.SelectedIndex = 0;
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutput.Text = saveFileDialog1.FileName;
            }
        }

        /// <summary>
        /// ���û�������пؼ�
        /// </summary>
        /// <param name="enabled"></param>
        private void EnabledAllControl(bool enabled)
        {
            controlSelArea1.Enabled = enabled;
            btnOutput.Enabled = enabled;
            cmbCADTemplate.Enabled = enabled;
            cmbCADVersion.Enabled = enabled;
            btnExport.Enabled = enabled;
            chkExportElement.Enabled = enabled;
            btnCancel.Enabled = enabled;
        }
    }
}