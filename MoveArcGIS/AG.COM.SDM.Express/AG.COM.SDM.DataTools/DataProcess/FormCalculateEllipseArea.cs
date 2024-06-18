using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 椭球面积计算窗体类
    /// </summary>
    public partial class FormCalculateEllipseArea : Form
    {
        private IMap m_Map = null;
        
        /// <summary>
        /// 设置地图对象
        /// </summary>
        public IMap Map
        {
            set
            {
                featureLayerSelector1.Map = value;
                m_Map = value;
            }
        } 

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormCalculateEllipseArea()
        {
            InitializeComponent();
        }

        private void featureLayerSelector1_LayerChanged()
        {
            cboFields.Items.Clear();
            IFeatureClass fcls = featureLayerSelector1.FeatureClass;
            if (fcls == null)
                return;
            IField fld;
            for (int i = 0; i <= fcls.Fields.FieldCount - 1; i++)
            {
                fld = fcls.Fields.get_Field(i);
                if (fld.Type == esriFieldType.esriFieldTypeDouble)
                {
                    cboFields.Items.Add(fld.Name);
                }
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            IFeatureClass fcls = featureLayerSelector1.FeatureClass;
            if (fcls == null)
            {
                MessageHandler.ShowErrorMsg("请选择图层!", Text);
                return;
            }
            if (fcls.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
            {
                MessageHandler.ShowErrorMsg("请选择多边形图层！", Text);
                return;
            }
            if (cboFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("请选择字段！", this.Text);
                return;
            }

            Utility.Common.ITrackProgress pdlg = new AG.COM.SDM.Utility .Common.TrackProgressDialog();
            pdlg.DisplayTotal = false;
            pdlg.AutoFinishClose = true;
            pdlg.SubMax = fcls.FeatureCount(null);
            pdlg.SubValue = 0;
            (pdlg as Form).Owner = this;
            pdlg.Show();
            Application.DoEvents();

            double area;
            Utility.Editor.Gauss gauss = new AG.COM.SDM.Utility.Editor.Gauss(120);
            int fldIndex = fcls.Fields.FindField(cboFields.Text);

            IWorkspaceEdit ws = Utility.Editor.LibEditor.GetNewEditableWorkspace((fcls as IDataset).Workspace);
            ws.StartEditing(false);
            ws.StartEditOperation();
            try
            {
                IFeatureCursor pCursor = fcls.Search(null, false);
                IFeature pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    area = gauss.ComputeArea(pFeature.Shape as ESRI.ArcGIS.Geometry.IPolygon4);
                    pFeature.set_Value(fldIndex, area);
                    pFeature.Store();

                    pdlg.SubValue++;
                    Application.DoEvents();

                    pFeature = pCursor.NextFeature();
                }
                ws.StopEditOperation();
                ws.StopEditing(true);                
                pdlg.SetFinish();
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
                MessageHandler.ShowInfoMsg("椭球面积计算完成！", Text);
                this.Close();
            }
            catch (Exception ex)
            {
                pdlg.SetFinish();
                ws.AbortEditOperation();
                ws.StopEditing(false);

                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}