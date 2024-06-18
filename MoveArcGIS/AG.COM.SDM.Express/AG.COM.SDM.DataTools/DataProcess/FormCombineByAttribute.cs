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
    /// 根据属性合并窗体类
    /// </summary>
    public partial class FormCombineByAttribute : Form
    {
        private IMap m_Map = null;

        /// <summary>
        /// 默认构造函数
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

        private void btOK_Click(object sender, EventArgs e)
        {
            //确认输入有效
            IFeatureClass srcFcls = featureLayerSelector1.FeatureClass;
            if (srcFcls == null)
            {
                MessageHandler.ShowErrorMsg("请选择要处理的图层！", this.Text);
                return;
            }
            if (cboFields.SelectedIndex < 0)
            {
                MessageHandler.ShowErrorMsg("请选择字段段！", this.Text);
                return;
            }
            string dir = System.IO.Path.GetDirectoryName(txtFileName.Text);
            string lyname = System.IO.Path.GetFileName(txtFileName.Text).Trim();
            string lyname2 = System.IO.Path.GetFileNameWithoutExtension(txtFileName.Text).Trim();

            if (System.IO.Directory.Exists(dir) == false)
            {
                MessageHandler.ShowErrorMsg("Shape文件路径不存在！", this.Text);
                return;
            }

            if (lyname2.Length == 0)
            {
                MessageHandler.ShowErrorMsg("请指定Shape文件名！", this.Text);
                return;
            }

            //删除已经存在的文件
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

            //创建Shape文件
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
                //转换
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
                        //获取合并后图形
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
                        //要目标图层中创建新的要素，保存合并后的图形
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
                        MessageHandler.ShowInfoMsg("转换完成！", this.Text);
                    else
                    {
                        if (MessageBox.Show("转换完成，是否立即添加到地图中？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            dlg.Filter = "Shape文件(*.shp)|*.shp";
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