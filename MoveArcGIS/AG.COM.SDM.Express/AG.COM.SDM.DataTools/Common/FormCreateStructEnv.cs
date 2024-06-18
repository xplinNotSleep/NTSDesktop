using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.Common
{
    /// <summary>
    /// ���췶Χѡ������
    /// </summary>
    public partial class FormCreateStructEnv : Form
    {
        private IMap m_Map = null;
        private IFeatureSelection m_FeatureSelection = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="map">��ͼ����</param>
        public FormCreateStructEnv(IMap map)
        {
            //��ʼ���������
            InitializeComponent();

            this.m_Map = map;
            this.lblUnits.Text = this.m_Map.MapUnits.ToString().Substring(4);           
        }

        private void FormCreateStructEnv_Load(object sender, EventArgs e)
        {
            this.comboBoxTreeView1.TreeView.ImageList = this.imageList1;
            this.combSpatialRel.SelectedIndex = 0;
            
            //����ͼ����
            ConstrucLayerTree();
        } 

        private void comboBoxTreeView1_SelectedValueChanged(object sender, EventArgs e)
        {
            TreeNode node = this.comboBoxTreeView1.TreeView.SelectedNode;

            if (node == null) return;

            if ((node.Tag is Utility.Wrapers.LayerWrapper) == false)
            {
                comboBoxTreeView1.Text = "";
                comboBoxTreeView1.TreeView.SelectedNode = null;
            }

            IFeatureLayer layer = (node.Tag as Utility.Wrapers.LayerWrapper).Layer as IFeatureLayer;

            if (layer == null)
            {
                comboBoxTreeView1.Text = "";
                comboBoxTreeView1.TreeView.SelectedNode = null;
                this.btnOk.Enabled = false;
            }
            else
            {
                this.m_FeatureSelection = layer as IFeatureSelection;
                comboBoxTreeView1.Text = layer.Name;
                this.btnOk.Enabled = true;
            }
        }

        private void btnOutPut_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDialog = new SaveFileDialog();
            tSaveFileDialog.InitialDirectory = Utility.CommonConstString.STR_TempPath;
            tSaveFileDialog.DefaultExt = ".shp";
            tSaveFileDialog.Filter = "Shape�ļ�(*.shp)|*.shp";

            if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtShpPath.Text = tSaveFileDialog.FileName;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        { 
            IWorkspaceFactory tWorkspaceFactory;
            IWorkspace tWorkspace=null;
            string LayerName = "tempPolygon";

            this.Cursor = Cursors.WaitCursor;
          
            //��ȡ��ȡ����
            ICursor pLineCursor;
            this.m_FeatureSelection.SelectionSet.Search(null, false, out pLineCursor);
            
            try
            {
                if (this.rdioNotOutput.Checked == true)
                {
                    tWorkspaceFactory = new InMemoryWorkspaceFactoryClass();
                    IWorkspaceName tWorkspaceName = tWorkspaceFactory.Create("", "MyWorkspace", null, 0);
                    IName tName = tWorkspaceName as IName;
                    tWorkspace = tName.Open() as IWorkspace;
                }
                else
                {
                    string strShpPath = this.txtShpPath.Text.Trim();
                    if (IsRightShpFile(strShpPath) == true)
                    {
                        tWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                        tWorkspace = tWorkspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(strShpPath), 0);

                        string layerName = System.IO.Path.GetFileNameWithoutExtension(strShpPath);
                    }
                }

                //�����ֶ�
                IFields tfields = CreateFields();
                IFeatureClass tDestFeatClass = (tWorkspace as IFeatureWorkspace).CreateFeatureClass(LayerName, tfields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                 
                //������
                IFeatureConstruction tFeatureConstuctor = new FeatureConstructionClass();
                tFeatureConstuctor.ConstructPolygonsFromFeaturesFromCursor(null, tDestFeatClass, null, false, false, pLineCursor as IFeatureCursor, null, (double)this.numTolerance.Value, null);

                if (tDestFeatClass.FeatureCount(null) == 0)
                {
                    this.Cursor = Cursors.Default;

                    MessageBox.Show("�Բ�����ѡ�����ܹ����棬������ѡ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                }
                else
                {
                    IFeatureLayer tFeatureLayer = new FeatureLayerClass();
                    tFeatureLayer.FeatureClass = tDestFeatClass;
                    tFeatureLayer.Name = "��������";
                    this.m_Map.AddLayer(tFeatureLayer);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("�Բ�����ѡ�����ܹ����棬������ѡ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tWorkspaceFactory = null;
                tWorkspace = null;
                //�ͷ���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pLineCursor);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// �����ֶμ�
        /// </summary>
        /// <returns>�����ֶμ�</returns>
        private IFields CreateFields()
        {
            //ʵ�����ֶμ��϶���
            IFields pFields = new FieldsClass();
            IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

            //�������ζ����ֶζ���
            IGeometryDef tGeometryDef = new GeometryDefClass();
            IGeometryDefEdit tGeometryDefEdit = tGeometryDef as IGeometryDefEdit;

            //ָ�����ζ����ֶ�����ֵ
            tGeometryDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon;
            tGeometryDefEdit.GridCount_2 = 1;
            tGeometryDefEdit.set_GridSize(0, 1000);
            tGeometryDefEdit.SpatialReference_2 = (this.m_FeatureSelection as IGeoDataset).SpatialReference;

            //����OID�ֶ�
            IField fieldOID = new FieldClass();
            IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
            fieldEditOID.Name_2 = "OBJECTID";
            fieldEditOID.AliasName_2 = "OBJECTID";
            fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
            tFieldsEdit.AddField(fieldOID);

            //����ID�ֶ�
            IField fieldID = new FieldClass();
            IFieldEdit fieldEditID = fieldOID as IFieldEdit;
            fieldEditID.Name_2 = "ID";
            fieldEditID.Type_2 = esriFieldType.esriFieldTypeString;
            tFieldsEdit.AddField(fieldOID);

            //���������ֶ�
            IField fieldShape = new FieldClass();
            IFieldEdit fieldEditShape = fieldShape as IFieldEdit;
            fieldEditShape.Name_2 = "SHAPE";
            fieldEditShape.AliasName_2 = "SHAPE";
            fieldEditShape.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fieldEditShape.GeometryDef_2 = tGeometryDef;
            tFieldsEdit.AddField(fieldShape);

            return pFields;
        }

        /// <summary>
        /// ����ͼ����
        /// </summary>
        /// <param name="combo">����������</param>
        /// <param name="ws"></param>
        private void ConstrucLayerTree()
        {
            if (m_Map == null) return;

            ILayer layer;
            TreeNode node;

            for (int i = 0; i < this.m_Map.LayerCount; i++)
            {
                layer = m_Map.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    node = new TreeNode(layer.Name);

                    //�ݹ����ͼ��ڵ�
                    AddGroupLayers(layer as IGroupLayer, node);

                    node.ImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        this.comboBoxTreeView1.TreeView.Nodes.Add(node);
                    }
                }
                else
                {
                    //���ͼ�����
                    AddLayer(layer, null);
                }
            }

            this.comboBoxTreeView1.TreeView.ExpandAll();
        }

        /// <summary>
        /// �ݹ������ͼ�����
        /// </summary>
        /// <param name="glayer">��ͼ�����</param>
        /// <param name="parentNode">���ڵ�</param>
        private void AddGroupLayers(IGroupLayer glayer, TreeNode parentNode)
        {
            ICompositeLayer clayer = glayer as ICompositeLayer;
            ILayer layer;
            TreeNode node;

            for (int i = 0; i <= clayer.Count - 1; i++)
            {
                layer = clayer.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    node = new TreeNode(layer.Name);

                    //�ݹ������ͼ�����
                    AddGroupLayers(layer as IGroupLayer, node);

                    node.ImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        parentNode.Nodes.Add(node);
                    }
                }
                else
                {
                    //���ͼ�����
                    AddLayer(layer, parentNode);
                }
            }
        }

        /// <summary>
        /// ���ͼ�����
        /// </summary>
        /// <param name="pLayer">ͼ�����</param>
        /// <param name="pTreeNode">���ڵ����</param>
        private void AddLayer(ILayer pLayer, TreeNode pTreeNode)
        {
            IFeatureLayer2 tFeaturelayer = pLayer as IFeatureLayer2;
            if (tFeaturelayer == null) return;

            if (tFeaturelayer.ShapeType == esriGeometryType.esriGeometryLine || tFeaturelayer.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                Utility.Wrapers.LayerWrapper wrap = new AG.COM.SDM.Utility.Wrapers.LayerWrapper(pLayer);

                TreeNode treeNode = new TreeNode(wrap.ToString());
                treeNode.Tag = wrap;
                treeNode.ImageIndex = 1;

                if (pTreeNode == null)
                {
                    this.comboBoxTreeView1.TreeView.Nodes.Add(treeNode);
                }
                else
                {
                    pTreeNode.Nodes.Add(treeNode);
                }
            }
        }

        private void rdioOutput_CheckedChanged(object sender, EventArgs e)
        {
            this.txtShpPath.Enabled = this.rdioOutput.Checked;
            this.btnOutPut.Enabled = this.rdioOutput.Checked; 
        }

        /// <summary>
        /// ���Shape�ļ��Ƿ���ȷ
        /// </summary>
        /// <param name="strShpFile">Shape�ļ�</param>
        /// <returns>�����ȷ�򷵻�true,���򷵻�false</returns>
        private bool IsRightShpFile(string strShpFile)
        {
            string dir = System.IO.Path.GetDirectoryName(this.txtShpPath.Text.Trim());
            string lyname = System.IO.Path.GetFileName(this.txtShpPath.Text.Trim());
            string lyname2 = System.IO.Path.GetFileNameWithoutExtension(this.txtShpPath.Text.Trim());

            if (System.IO.Directory.Exists(dir) == false)
            {
                MessageBox.Show("Shape�ļ�·�������ڣ�", this.Text);
                return false;
            }

            if (lyname2.Length == 0)
            {
                MessageBox.Show("��ָ��Shape�ļ�����", this.Text);
                return false;
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

            return true;
        }
    }
}