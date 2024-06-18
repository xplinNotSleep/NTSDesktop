using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 地图工程选择
    /// </summary>
    public partial class FormProjectSelect : Form
    {
        /// <summary>
        /// 选中的地图工程列表
        /// </summary>
        public AGSDM_PROJECT SelectedProject { get; private set; }
        
        /// <summary>
        /// 原有工程编号列表
        /// </summary>
        public decimal OriginProjectID { get; set; }

        /// <summary>
        /// 当前选中的节点
        /// </summary>
        private TreeNode m_CurSelectedNode;

        public FormProjectSelect()
        {
            InitializeComponent();
            OriginProjectID = -1;
        }

        private void FormProjectSelect_Load(object sender, EventArgs e)
        {
            CustomIntial();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_CurSelectedNode == null)
            {
                MessageBox.Show("请选择一个用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SelectedProject = m_CurSelectedNode.Tag as AGSDM_PROJECT;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tvwProjects_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (m_CurSelectedNode == null)
            {
                if (e.Node.Checked)
                {
                    m_CurSelectedNode = e.Node;
                }
            }
            else
            {
                if (e.Node.Checked)//选中
                {
                    if (m_CurSelectedNode != null && m_CurSelectedNode.Equals(e.Node))//前节点与本次节点相同
                        return;
                    else
                        UnCheckCurNode();
                    m_CurSelectedNode = e.Node;
                }
                else
                {
                    m_CurSelectedNode = null;
                }
            }

            
        }
        
        /// <summary>
        /// 自定义初始化
        /// </summary>
        public void CustomIntial()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstProjects = tEntityHandler.GetEntities("from AGSDM_PROJECT");
            for (int i = 0; i < lstProjects.Count; i++)
            {
                AGSDM_PROJECT project = lstProjects[i] as AGSDM_PROJECT;
                TreeNode node = new TreeNode();
                if (project.ID == OriginProjectID)
                {
                    node.Checked = true;
                    m_CurSelectedNode = node;
                }
                else
                {
                    node.Checked = false;
                }
                node.Text = project.PROJECT_NAME;
                node.Tag = project;
                tvwProjects.Nodes.Add(node);
            }
        }
       
        /// <summary>
        /// 取消当前节点的选中状态
        /// </summary>
        private void UnCheckCurNode()
        {
            if (m_CurSelectedNode == null)
                return;

            m_CurSelectedNode.Checked = false;
        }
    }
}
