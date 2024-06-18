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
    /// 构造范围选择窗体类
    /// </summary>
    public partial class FormCreateStructEnv : Form
    {
        private IMap m_Map = null;
        private IFeatureSelection m_FeatureSelection = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="map">地图对象</param>
        public FormCreateStructEnv(IMap map)
        {
            //初始化界面组件
            InitializeComponent();

            this.m_Map = map;
            this.lblUnits.Text = this.m_Map.MapUnits.ToString().Substring(4);           
        }

        private void FormCreateStructEnv_Load(object sender, EventArgs e)
        {
            this.comboBoxTreeView1.TreeView.ImageList = this.imageList1;
            this.combSpatialRel.SelectedIndex = 0;
            
            //构造图层树
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
            tSaveFileDialog.Filter = "Shape文件(*.shp)|*.shp";

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
          
            //获取获取对象
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

                //创建字段
                IFields tfields = CreateFields();
                IFeatureClass tDestFeatClass = (tWorkspace as IFeatureWorkspace).CreateFeatureClass(LayerName, tfields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                 
                //构造类
                IFeatureConstruction tFeatureConstuctor = new FeatureConstructionClass();
                tFeatureConstuctor.ConstructPolygonsFromFeaturesFromCursor(null, tDestFeatClass, null, false, false, pLineCursor as IFeatureCursor, null, (double)this.numTolerance.Value, null);

                if (tDestFeatClass.FeatureCount(null) == 0)
                {
                    this.Cursor = Cursors.Default;

                    MessageBox.Show("对不起，所选对象不能构成面，请重新选择", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                }
                else
                {
                    IFeatureLayer tFeatureLayer = new FeatureLayerClass();
                    tFeatureLayer.FeatureClass = tDestFeatClass;
                    tFeatureLayer.Name = "构造面域";
                    this.m_Map.AddLayer(tFeatureLayer);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("对不起，所选对象不能构成面，请重新选择", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tWorkspaceFactory = null;
                tWorkspace = null;
                //释放资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pLineCursor);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 创建字段集
        /// </summary>
        /// <returns>返回字段集</returns>
        private IFields CreateFields()
        {
            //实倒化字段集合对象
            IFields pFields = new FieldsClass();
            IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

            //创建几何对象字段定义
            IGeometryDef tGeometryDef = new GeometryDefClass();
            IGeometryDefEdit tGeometryDefEdit = tGeometryDef as IGeometryDefEdit;

            //指定几何对象字段属性值
            tGeometryDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon;
            tGeometryDefEdit.GridCount_2 = 1;
            tGeometryDefEdit.set_GridSize(0, 1000);
            tGeometryDefEdit.SpatialReference_2 = (this.m_FeatureSelection as IGeoDataset).SpatialReference;

            //创建OID字段
            IField fieldOID = new FieldClass();
            IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
            fieldEditOID.Name_2 = "OBJECTID";
            fieldEditOID.AliasName_2 = "OBJECTID";
            fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
            tFieldsEdit.AddField(fieldOID);

            //创建ID字段
            IField fieldID = new FieldClass();
            IFieldEdit fieldEditID = fieldOID as IFieldEdit;
            fieldEditID.Name_2 = "ID";
            fieldEditID.Type_2 = esriFieldType.esriFieldTypeString;
            tFieldsEdit.AddField(fieldOID);

            //创建几何字段
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
        /// 构造图层树
        /// </summary>
        /// <param name="combo">树形下拉框</param>
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

                    //递归添加图层节点
                    AddGroupLayers(layer as IGroupLayer, node);

                    node.ImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        this.comboBoxTreeView1.TreeView.Nodes.Add(node);
                    }
                }
                else
                {
                    //添加图层对象
                    AddLayer(layer, null);
                }
            }

            this.comboBoxTreeView1.TreeView.ExpandAll();
        }

        /// <summary>
        /// 递归添加组图层对象
        /// </summary>
        /// <param name="glayer">组图层对象</param>
        /// <param name="parentNode">父节点</param>
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

                    //递归添加组图层对象
                    AddGroupLayers(layer as IGroupLayer, node);

                    node.ImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        parentNode.Nodes.Add(node);
                    }
                }
                else
                {
                    //添加图层对象
                    AddLayer(layer, parentNode);
                }
            }
        }

        /// <summary>
        /// 添加图层对象
        /// </summary>
        /// <param name="pLayer">图层对象</param>
        /// <param name="pTreeNode">树节点对象</param>
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
        /// 检查Shape文件是否正确
        /// </summary>
        /// <param name="strShpFile">Shape文件</param>
        /// <returns>如果正确则返回true,否则返回false</returns>
        private bool IsRightShpFile(string strShpFile)
        {
            string dir = System.IO.Path.GetDirectoryName(this.txtShpPath.Text.Trim());
            string lyname = System.IO.Path.GetFileName(this.txtShpPath.Text.Trim());
            string lyname2 = System.IO.Path.GetFileNameWithoutExtension(this.txtShpPath.Text.Trim());

            if (System.IO.Directory.Exists(dir) == false)
            {
                MessageBox.Show("Shape文件路径不存在！", this.Text);
                return false;
            }

            if (lyname2.Length == 0)
            {
                MessageBox.Show("请指定Shape文件名！", this.Text);
                return false;
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

            return true;
        }
    }
}