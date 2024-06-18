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
    /// ��ͼ��תΪ��ͼ�� ������
    /// </summary>
    public partial class FormArea2Line : Form
    {
        private IMap m_Map = null;

        public FormArea2Line()
        {
            InitializeComponent();
        }

        private void FormArea2Line_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new System.Drawing.Bitmap(btOpen.Image);
            bmp.MakeTransparent();
            btOpen.Image = bmp;
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
                MessageHandler.ShowErrorMsg("��ѡ��Ҫת������㣡", this.Text);
                return;
            }
            if (srcFcls.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                MessageHandler.ShowErrorMsg("Ҫת���Ĳ����Ϊ��㣡", this.Text);
                return;
            }

            if (txtFileName.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("������Ŀ��Shape�ļ���λ�ã�", this.Text);
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
                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory f = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                IWorkspace ws = f.OpenFromFile(dir, 0);
                IFields flds = Utility.Editor.LibEditor.GetValidFields(srcFcls.Fields);
                IGeometryDef def = flds.get_Field(flds.FindField("Shape")).GeometryDef;
                (def as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                IFeatureClass destFcls = (ws as IFeatureWorkspace).CreateFeatureClass(lyname2, flds, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                //ת��
                IFeatureCursor pCursor = srcFcls.Search(null, false);
                IFeatureConstruction constructor = new FeatureConstructionClass();
                
                constructor.ConstructLinesFromCursor(null, destFcls, pCursor, false, null,(double)numericUpDown1.Value);

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
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

    }
}