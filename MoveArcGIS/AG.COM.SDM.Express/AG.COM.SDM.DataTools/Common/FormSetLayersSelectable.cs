using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.DataTools.Common
{
    /// <summary>
    /// 设置可选图层窗体类
    /// </summary>
    public partial class FormSetLayersSelectable : Form
    {
        private IBasicMap m_Map;

        public IBasicMap Map
        {
            set { m_Map = value; }
        }

        public FormSetLayersSelectable()
        {
            InitializeComponent();
        }

        private void frmSetLayersSelectable_Load(object sender, EventArgs e)
        {
            //初始化图层控件
            this.MltSetLayersSelectable.Init(m_Map);
            MltSetLayersSelectable.ExpandAll();
        }

        private void butSelectAll_Click(object sender, EventArgs e)
        {
            MltSetLayersSelectable.SelectAll();
        }

        private void butClearAll_Click(object sender, EventArgs e)
        {
            MltSetLayersSelectable.ClearAll();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MltSetLayersSelectable.Nodes.Count; i++)
            {
                SetLayersSelectableByNode(MltSetLayersSelectable.Nodes[i]);
            }
            this.Close();
        }

        private void SetLayersSelectableByNode(TreeNode node)
        {
            if (node.Tag is IFeatureLayer)
            {
                (node.Tag as IFeatureLayer).Selectable = node.Checked;
                return;
            }
            else if (node.Tag is IGroupLayer)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    SetLayersSelectableByNode(node.Nodes[i]);
                }
            }
        }
    }
}