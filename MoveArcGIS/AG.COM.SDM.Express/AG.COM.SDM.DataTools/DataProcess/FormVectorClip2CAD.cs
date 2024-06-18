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
    /// 矢量数据裁剪窗体类
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
        /// 设置Hook对象
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
                MessageBox.Show("请先选择裁剪范围");
                return;
            }

            if (string.IsNullOrEmpty(txtOutput.Text))
            {
                MessageBox.Show("请先选择导出CAD文件");
                return;
            }            

            List<ILayer> tLayers = GetSelectLayer(ltvLayer.Nodes);
            if (tLayers.Count < 1)
            {
                MessageBox.Show("请先选择图层");
                return;
            }

            string CADTemplateFile = cmbCADTemplate.Text;
            if (string.IsNullOrEmpty(CADTemplateFile))
            {
                MessageBox.Show("请先选择CAD模板");
                return;
            }

            string CADVersion = cmbCADVersion.Text;
            if (string.IsNullOrEmpty(CADVersion))
            {
                MessageBox.Show("请先选择导出CAD版本");
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
        /// 获取选择的图层
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
        /// 把选择的图层添加到一个list
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
                //初始化CAD版本信息
                CADVersionHelper.InitVersionInfo();
                //检查CAD是否安装
                if (CADVersionHelper.CheckCurrentVersionIsInstall(true) == false)
                {
                    Close();
                    return;
                }

                ///初始化图层树
                this.ltvLayer.InitUI(this.m_hookHelperEx.FocusMap as IBasicMap);

                //添加CAD模板文件
                cmbCADTemplate.DataSource = ConvertManagerHelper.GetAllCADTemplateFile();
                if (cmbCADTemplate.Items.Count > 0)
                    cmbCADTemplate.SelectedIndex = 0;

                BindCADVersion();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");   
            }
        }

        /// <summary>
        /// 绑定导出CAD版本
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
        /// 启用或禁用所有控件
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