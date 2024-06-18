using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// �������Ժϲ�������
    /// </summary>
    public partial class FormCombineByAttribute : Form
    {
        private IMap m_Map = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormCombineByAttribute()
        {
            InitializeComponent();
        }

        private void FormDissolve_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new System.Drawing.Bitmap(btOpen.Image);
            bmp.MakeTransparent();
            btOpen.Image = bmp;
        }

        /// <summary>
        /// ���õ�ͼ����
        /// </summary>
        public IMap Map
        {
            set
            {
                featureLayerSelector1.Map = value;
                m_Map = value;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            //ȷ��������Ч
            IFeatureClass srcFcls = featureLayerSelector1.FeatureClass;
            if (srcFcls == null)
            {
                MessageHandler.ShowErrorMsg("��ѡ��Ҫ�����ͼ�㣡", this.Text);
                return;
            }
            if (cboFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("��ѡ���ֶζΣ�", this.Text);
                return;
            }
            string dir = System.IO.Path.GetDirectoryName(txtFileName.Text);
            string lyname = System.IO.Path.GetFileName(txtFileName.Text).Trim();
            string lyname2 = System.IO.Path.GetFileNameWithoutExtension(txtFileName.Text).Trim();

            if (System.IO.Directory.Exists(dir) == false)
            {
                MessageHandler.ShowErrorMsg("Shape�ļ�·�������ڣ�", this.Text);
                return;
            }

            if (lyname2.Length == 0)
            {
                MessageHandler.ShowErrorMsg("��ָ��Shape�ļ�����", this.Text);
                return;
            }

            //ɾ���Ѿ����ڵ��ļ�
            string tmpfn;
            tmpfn = dir + "\\" + lyname2 + ".shp";
            System.IO.File.Delete(tmpfn);
            tmpfn = dir + "\\" + lyname2 + ".shx";
            System.IO.File.Delete(tmpfn);
            tmpfn = dir + "\\" + lyname2 + ".dbf";
            System.IO.File.Delete(tmpfn);
            tmpfn = dir + "\\" + lyname2 + ".sbn";
            System.IO.File.Delete(tmpfn);
            tmpfn = dir + "\\" + lyname2 + ".prj";
            System.IO.File.Delete(tmpfn);

            //����Shape�ļ�
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                IWorkspaceFactory tWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                IWorkspace ws = tWorkspaceFactory.OpenFromFile(dir, 0);

                IFields flds = Utility.Editor.LibEditor.GetValidFields(srcFcls.Fields);
                IGeometryDef def = flds.get_Field(flds.FindField("Shape")).GeometryDef;
                (def as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                IFeatureClass destFcls = (ws as IFeatureWorkspace).CreateFeatureClass(lyname2, flds, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                ITrackProgress progressDlg = new TrackProgressDialog();
                progressDlg.AutoFinishClose = true;
                progressDlg.DisplayTotal = false;
                (progressDlg as Form).Owner = this;
                progressDlg.Show();
                //ת��
                IWorkspaceEdit wse = Utility.Editor.LibEditor.GetNewEditableWorkspace(ws);
                wse.StartEditing(false);
                wse.StartEditOperation();
                try
                {
                    IFeatureCursor pCursor = destFcls.Insert(true);
                    IList<string> vals = Utility.GeoTableHandler.GetUniqueValueByDataStat(srcFcls, cboFields.Text);
                    IFeatureBuffer pDestFeature;

                    IFeatureCursor pSrcCursor = null;
                    IQueryFilter pFilter;
                    esriFieldType srcFieldType = srcFcls.Fields.get_Field(srcFcls.Fields.FindField(cboFields.Text)).Type;
                    IGeometry pGeometry;
                    int destFldIndex = destFcls.Fields.FindField(cboFields.Text);

                    progressDlg.SubMax = vals.Count;
                    progressDlg.SubValue = 0;
                    for (int i = 0; i <= vals.Count - 1; i++)
                    {
                        //��ȡ�ϲ���ͼ��
                        pFilter = new QueryFilterClass();

                        if ((srcFieldType == esriFieldType.esriFieldTypeDate) || (srcFieldType == esriFieldType.esriFieldTypeString))
                        {
                            pFilter.WhereClause = cboFields.Text + "='" + vals[i] + "'";
                        }
                        else
                        {
                            pFilter.WhereClause = cboFields.Text + "=" + vals[i];
                        }
                        pSrcCursor = srcFcls.Search(pFilter, false);
                        pGeometry = GetUnionGeometry(pSrcCursor);
                        //ҪĿ��ͼ���д����µ�Ҫ�أ�����ϲ����ͼ��
                        if (pGeometry != null)
                        {
                            pDestFeature = destFcls.CreateFeatureBuffer();
                            pDestFeature.set_Value(destFldIndex, vals[i]);
                            pDestFeature.Shape = pGeometry;

                            pCursor.InsertFeature(pDestFeature);
                        }

                        progressDlg.SubValue++;
                        Application.DoEvents();
                    }
                    pCursor.Flush();

                    wse.StopEditOperation();
                    wse.StopEditing(true);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);

                    progressDlg.SetFinish();

                    if (m_Map == null)
                        MessageHandler.ShowInfoMsg("ת����ɣ�", this.Text);
                    else
                    {
                        if (MessageBox.Show("ת����ɣ��Ƿ�������ӵ���ͼ�У�", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            IFeatureLayer layer = new FeatureLayerClass();
                            layer.FeatureClass = destFcls;
                            layer.Name = destFcls.AliasName;
                            m_Map.AddLayer(layer);
                        }
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    wse.AbortEditOperation();
                    wse.StopEditing(false);
                    progressDlg.SetFinish();

                    MessageHandler.ShowErrorMsg(ex.Message, this.Text);
                }

            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private IGeometry GetUnionGeometry(IFeatureCursor pCursor)
        {
            IGeometry pGeometry = null;
            IFeature pFeature = pCursor.NextFeature();
            IGeometry tmpGeo;
            while (pFeature != null)
            {
                tmpGeo = pFeature.Shape;
                if (tmpGeo.IsEmpty == false)
                {
                    (tmpGeo as ITopologicalOperator2).IsKnownSimple_2 = false;
                    (tmpGeo as ITopologicalOperator2).Simplify();
                    tmpGeo.SnapToSpatialReference();
                    if (pGeometry == null)
                        pGeometry = tmpGeo;
                    else
                    {
                        pGeometry = (pGeometry as ITopologicalOperator).Union(tmpGeo);
                    }
                }

                pFeature = pCursor.NextFeature();
            }

            return pGeometry;
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Shape�ļ�(*.shp)|*.shp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = dlg.FileName;
            }
        }

        private void featureLayerSelector1_LayerChanged()
        {
            cboFields.Items.Clear();
            IFeatureClass srcFcls = featureLayerSelector1.FeatureClass;
            if (srcFcls == null)
            {
                return;
            }
            IField fld;
            for (int i = 0; i <= srcFcls.Fields.FieldCount - 1; i++)
            {
                fld = srcFcls.Fields.get_Field(i);
                if (Utility.StreamConvert.IsNormalField(fld))
                {
                    cboFields.Items.Add(fld.Name);
                }
            }
        }
    }
}