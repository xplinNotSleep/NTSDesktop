using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// ���ݿռ��ϵ�����ֶ�ֵ ������
    /// </summary>
    public partial class FormCreateFieldValueBySpatialRel : Form
    {
        private IMap m_Map = null;
        
        /// <summary>
        /// ���õ�ͼ����
        /// </summary>
        public IMap Map
        {
            set
            {
                this.dsDestSelector.Map = value;
                this.dsSrcSelector.Map = value;
                m_Map = value;
            }
        }  

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormCreateFieldValueBySpatialRel()
        {
            InitializeComponent();
        }

        private void cboDestFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSrcFields.Items.Clear();
            IFeatureClass destFcls = dsDestSelector.FeatureClass;
            IFeatureClass srcFcls = dsSrcSelector.FeatureClass;
            if (destFcls == null)
                return;
            if (srcFcls == null)
                return;
            IField destFld = destFcls.Fields.get_Field(destFcls.Fields.FindField(cboDestFields.Text));
            IField fld;
            for (int i = 0; i <= srcFcls.Fields.FieldCount - 1; i++)
            {
                fld = srcFcls.Fields.get_Field(i);
                if ((fld.Type != esriFieldType.esriFieldTypeBlob) &&
                    (fld.Type != esriFieldType.esriFieldTypeGeometry) &&
                    (fld.Type != esriFieldType.esriFieldTypeGlobalID) &&
                    (fld.Type != esriFieldType.esriFieldTypeGUID) &&
                    (fld.Type != esriFieldType.esriFieldTypeOID) &&
                    (fld.Type != esriFieldType.esriFieldTypeRaster) &&
                    (fld.Type != esriFieldType.esriFieldTypeXML))
                {
                    if ((destFld.Type == esriFieldType.esriFieldTypeString) || (destFld.Type == fld.Type))
                        cboSrcFields.Items.Add(fld.Name);
                    else
                    {
                        if ((destFld.Type == esriFieldType.esriFieldTypeDouble) &&
                            ((fld.Type == esriFieldType.esriFieldTypeInteger) || (fld.Type == esriFieldType.esriFieldTypeSingle) || (fld.Type == esriFieldType.esriFieldTypeSmallInteger) || (fld.Type == esriFieldType.esriFieldTypeDouble)))
                        {
                            cboSrcFields.Items.Add(fld.Name);
                        }
                        else if ((destFld.Type == esriFieldType.esriFieldTypeInteger) &&
                            ((fld.Type == esriFieldType.esriFieldTypeInteger) || (fld.Type == esriFieldType.esriFieldTypeSingle) || (fld.Type == esriFieldType.esriFieldTypeSmallInteger)))
                        {
                            cboSrcFields.Items.Add(fld.Name);
                        }
                    }
                }
            }
        }    

        private void btOK_Click(object sender, EventArgs e)
        {
            if (CheckInput() == false)
                return;
            IFeatureClass destFcls = dsDestSelector.FeatureClass;
            IFeatureClass srcFcls = dsSrcSelector.FeatureClass;

            ITrackProgress pdlg = new TrackProgressDialog();
            pdlg.DisplayTotal = false;
            pdlg.AutoFinishClose = true;
            pdlg.SubMax = destFcls.FeatureCount(null);
            pdlg.SubValue = 0;
            (pdlg as Form).Owner = this;
            pdlg.Show();
            Application.DoEvents();

            Utility.Editor.Gauss gauss = new AG.COM.SDM.Utility.Editor.Gauss(120);
            int destFldIndex = destFcls.Fields.FindField(cboDestFields.Text);
            int srcFldIndex = srcFcls.Fields.FindField(cboSrcFields.Text);
            
            object fldVal;
            IWorkspaceEdit ws = Utility.Editor.LibEditor.GetNewEditableWorkspace((destFcls as IDataset).Workspace);
            ws.StartEditing(false);
            ws.StartEditOperation();
            try
            {
                IFeatureCursor pCursor = destFcls.Update(null, false);
                IFeature pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    fldVal = GetFieldValue(pFeature, srcFcls, srcFldIndex);
                    if (fldVal != null)
                    {
                        pFeature.set_Value(destFldIndex, fldVal);
                        pCursor.UpdateFeature(pFeature);
                    }

                    pdlg.SubValue++;
                    Application.DoEvents();

                    pFeature = pCursor.NextFeature();
                }
                ws.StartEditOperation();
                ws.StartEditing(true);
                pdlg.SetFinish();
                MessageHandler.ShowInfoMsg("����ֵ������ɣ�", Text);
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

        private object GetFieldValue(IFeature destFeature, IFeatureClass srcFcls,int srcFldIndex)
        {
            ISpatialFilter pFilter = new SpatialFilterClass();
            if (srcFcls.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
            {
                IPoint centroid = (destFeature.Shape as IArea).Centroid;
                pFilter.Geometry = centroid;
                pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin;
            }            
            else
            {
                pFilter.Geometry = destFeature.Shape;
                pFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            }
            IFeatureCursor pCursor = srcFcls.Search(pFilter, false);
            IFeature pFeature = pCursor.NextFeature();
            if (pFeature == null)
                return null;
            else
                return pFeature.get_Value(srcFldIndex);
        }

        private void dsDestSelector_LayerChanged()
        {
            cboDestFields.Items.Clear();
            IFeatureClass fcls = dsDestSelector.FeatureClass;
            IField fld;
            for (int i = 0; i <= fcls.Fields.FieldCount - 1; i++)
            {
                fld = fcls.Fields.get_Field(i);
                if ((fld.Type != esriFieldType.esriFieldTypeBlob) &&
                    (fld.Type != esriFieldType.esriFieldTypeGeometry) &&
                    (fld.Type != esriFieldType.esriFieldTypeGlobalID) &&
                    (fld.Type != esriFieldType.esriFieldTypeGUID) &&
                    (fld.Type != esriFieldType.esriFieldTypeOID) &&
                    (fld.Type != esriFieldType.esriFieldTypeRaster) &&
                    (fld.Type != esriFieldType.esriFieldTypeXML))
                {
                    cboDestFields.Items.Add(fld.Name);
                }
            }

        }

        private bool CheckInput()
        {
            if (dsDestSelector.FeatureClass == null)
            {
                MessageHandler.ShowErrorMsg("��ѡ��Ҫ��ֵ��ͼ��!", Text);
                return false;
            }
            if (dsSrcSelector.FeatureClass == null)
            {
                MessageHandler.ShowErrorMsg("��ѡ���ֶ�ֵ��Դͼ��!", Text);
                return false;
            }
            if (cboDestFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("��ѡ��Ҫ��ֵ���ֶ�!", Text);
                return false;
            }
            if (cboSrcFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("��ѡ����Դ�ֶ�!", Text);
                return false;
            }
            return true;
        }
    }
}