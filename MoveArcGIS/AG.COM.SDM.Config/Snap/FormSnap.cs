using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Editor;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Config
{
    public partial class FormSnap : SkinForm
    {
        private IMap m_Map = null;
        private Dictionary<IFeatureClass, SnapInfo> m_SnapDictCopy = null;

        public FormSnap()
        {
            InitializeComponent();

            this.mapLayersTreeView1.HideSelection = false;
            m_SnapDictCopy = CopySnapInfoList();
            this.mapLayersTreeView1.CheckBoxes = true;
        }

        //获取或设置地图
        public IMap Map
        {
            get { return m_Map; }
            set
            {
                m_Map = value;
                mapLayersTreeView1.Init(value as IBasicMap);
            }
        }

        private void FormSnap_Load(object sender, EventArgs e)
        {
            nudTolerance.Value = (decimal)Utility.CommonVariables.FeatureSnap.Tolerance;
            chkSnapEnabled.Checked = CommonVariables.FeatureSnap.SnapEnabled;
            this.btOK.Enabled = false;

            foreach (TreeNode tn in this.mapLayersTreeView1.Nodes)
                tn.Checked = false;
        }

        private void mapLayersTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IFeatureLayer layer = e.Node.Tag as IFeatureLayer;
            if (layer == null)
            {
                groupBox1.Enabled = false;
                chkCentroid.Checked = false;
                chkEdge.Checked = false;
                chkEndPoint.Checked = false;
                chkPendicular.Checked = false;
                chkVertex.Checked = false;
                return;
            }

            groupBox1.Enabled = true;
            SnapInfo snap = GetSnapInfo(layer.FeatureClass);
            chkCentroid.Checked = ((snap.SnapType & FeatureSnapType.stCentroid) == FeatureSnapType.stCentroid);
            this.chkEdge.Checked = ((snap.SnapType & FeatureSnapType.stEdge) == FeatureSnapType.stEdge);
            this.chkEndPoint.Checked = ((snap.SnapType & FeatureSnapType.stEndPoint) == FeatureSnapType.stEndPoint);
            this.chkPendicular.Checked = ((snap.SnapType & FeatureSnapType.stPendicularFoot) == FeatureSnapType.stPendicularFoot);
            this.chkVertex.Checked = ((snap.SnapType & FeatureSnapType.stVertex) == FeatureSnapType.stVertex);

        }

        //确定
        private void chkOK_Click(object sender, EventArgs e)
        {
            CommonVariables.FeatureSnap.Tolerance = (double)nudTolerance.Value;
            CommonVariables.FeatureSnap.SnapEnabled = chkSnapEnabled.Checked;

            CommonVariables.FeatureSnap.SnapList.Clear();
            foreach (var snap in m_SnapDictCopy.Values)
                if (snap.SnapType != FeatureSnapType.stNone)
                    CommonVariables.FeatureSnap.SnapList.Add(snap);

            this.Close();
        }

        //全部取消
        private void btCancelAll_Click(object sender, EventArgs e)
        {
            chkCentroid.Checked = false;
            chkEdge.Checked = false;
            chkEndPoint.Checked = false;
            chkPendicular.Checked = false;
            chkVertex.Checked = false;

            m_SnapDictCopy = new Dictionary<IFeatureClass, SnapInfo>();

            this.btOK.Enabled = true;
        }

        //捕捉类型
        private void chkVertex_Click(object sender, EventArgs e)
        {
            this.btOK.Enabled = true;

            if (mapLayersTreeView1.SelectedNode == null) return;

            IFeatureLayer layer = this.mapLayersTreeView1.SelectedNode.Tag as IFeatureLayer;
            if (layer == null) return;

            SnapInfo snap = GetSnapInfo(layer.FeatureClass);
            FeatureSnapType snapType = FeatureSnapType.stNone;
            if (chkVertex.Checked)
                snapType = snapType | FeatureSnapType.stVertex;
            if (chkCentroid.Checked)
                snapType = snapType | FeatureSnapType.stCentroid;
            if (chkEndPoint.Checked)
                snapType = snapType | FeatureSnapType.stEndPoint;
            if (chkPendicular.Checked)
                snapType = snapType | FeatureSnapType.stPendicularFoot;
            if (chkEdge.Checked)
                snapType = snapType | FeatureSnapType.stEdge;

            snap.SnapType = snapType;

        }

        //启用捕捉
        private void chkSnapEnabled_Click(object sender, EventArgs e)
        {
            btOK.Enabled = true;
        }

        //容差值
        private void nudTolerance_ValueChanged(object sender, EventArgs e)
        {
            btOK.Enabled = true;
        }

        private SnapInfo GetSnapInfo(ESRI.ArcGIS.Geodatabase.IFeatureClass fcls)
        {
            if (m_SnapDictCopy.ContainsKey(fcls))
                return m_SnapDictCopy[fcls];
            else
            {
                var snap = new AG.COM.SDM.Utility.Editor.SnapInfo();
                snap.FeatureClass = fcls;
                this.m_SnapDictCopy.Add(fcls, snap);
                return snap;
            }
        }

        private Dictionary<IFeatureClass, SnapInfo> CopySnapInfoList()
        {
            Dictionary<IFeatureClass, SnapInfo> snaps = new Dictionary<IFeatureClass, SnapInfo>();
            foreach (SnapInfo snap in CommonVariables.FeatureSnap.SnapList)
                snaps.Add(snap.FeatureClass, new SnapInfo() { FeatureClass = snap.FeatureClass, SnapType = snap.SnapType });
            return snaps;
        }

        private void btnSetSelected_Click(object sender, EventArgs e)
        {
            if (!chkCentroid.Checked && !chkEdge.Checked && !chkEndPoint.Checked && !chkVertex.Checked)
            {
                groupBox1.Enabled = true;
                MessageBox.Show("请设置捕捉类型");
                return;
            }

            if (!mapLayersTreeView1.Nodes[0].Checked)
            {
                MessageBox.Show("请选择图层");
                return;
            }
            TraverseTree(mapLayersTreeView1.Nodes);
            //if(mapLayersTreeView1.)
        }

        void TraverseTree(TreeNodeCollection nodes)
        {
            foreach (TreeNode tn in nodes)
            {
                if (tn.Checked)
                {
                    var layer = tn.Tag as IFeatureLayer;
                    if (null != layer)
                    {
                        SnapInfo snap = GetSnapInfo(layer.FeatureClass);
                        FeatureSnapType snapType = FeatureSnapType.stNone;
                        if (chkVertex.Checked)
                            snapType = snapType | FeatureSnapType.stVertex;
                        if (chkCentroid.Checked)
                            snapType = snapType | FeatureSnapType.stCentroid;
                        if (chkEndPoint.Checked)
                            snapType = snapType | FeatureSnapType.stEndPoint;
                        if (chkPendicular.Checked)
                            snapType = snapType | FeatureSnapType.stPendicularFoot;
                        if (chkEdge.Checked)
                            snapType = snapType | FeatureSnapType.stEdge;

                        snap.SnapType = snapType;
                    }
                }
                TraverseTree(tn.Nodes);
            }
        }

    }
}