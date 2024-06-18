using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 线转面工具类
    /// </summary>
    public partial class FormLine2Area : Form
    {
        private IMap m_Map = null;

        public FormLine2Area()
        {
            InitializeComponent();
        }

        private void FormLine2Area_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new System.Drawing.Bitmap(btOpen.Image);
            bmp.MakeTransparent();
            btOpen.Image = bmp;
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
                MessageHandler.ShowErrorMsg("请选择要转换的线层！", this.Text);
                return;
            }
            if (srcFcls.ShapeType != esriGeometryType.esriGeometryPolyline)
            {
                MessageHandler.ShowErrorMsg("要转换的层必须为线层！", this.Text);
                return;
            }
            
            if (txtFileName.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("请输入目标Shape文件的位置！", this.Text);
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
                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory f = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                IWorkspace ws = f.OpenFromFile(dir,0);
                IFields flds = Utility.Editor.LibEditor.GetValidFields(srcFcls.Fields);
                IGeometryDef def = flds.get_Field(flds.FindField("Shape")).GeometryDef;
                (def as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                IFeatureClass destFcls = (ws as IFeatureWorkspace).CreateFeatureClass(lyname2, flds, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                //转换
                IFeatureCursor pLineCursor = srcFcls.Search(null, false);
                IFeatureConstruction constuctor = new FeatureConstructionClass();
                constuctor.ConstructPolygonsFromFeaturesFromCursor(null, destFcls, (srcFcls as IGeoDataset).Extent, false, false, pLineCursor, null, (double)numericUpDown1.Value, null);

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
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}