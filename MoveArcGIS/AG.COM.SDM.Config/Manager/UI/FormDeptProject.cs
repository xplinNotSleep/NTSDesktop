using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    public partial class FormDeptProject : Form
    {
        private EnumOperateState m_OperateState;//设置窗体状态
        private AGSDM_ORG m_OrgInfo;

        public FormDeptProject()
        {
            InitializeComponent();
            m_OrgInfo = new AGSDM_ORG();
        }

        /// <summary>
        /// 获取或设置当前操作状态
        /// </summary>
        public EnumOperateState OperateState
        {
            get
            {
                return this.m_OperateState;
            }
            set
            {
                this.m_OperateState = value;
            }
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        public AGSDM_ORG pOrgInfo
        {
            get
            {
                return this.m_OrgInfo;
            }
            set
            {
                this.m_OrgInfo = value;
            }
        }

        /// <summary>
        /// 获取或设置功能树对象
        /// </summary>
        public TreeView MainMenuTree
        {
            get
            {
                return this.treeProject;
            }
            set
            {
                if (value == null) return;

                TreeView ptreeMenu = value;

                //删除所有节点
                this.treeProject.Nodes.Clear();

                foreach (TreeNode childNode in ptreeMenu.Nodes)
                {
                    this.treeProject.Nodes.Add(childNode.Clone() as TreeNode);
                    childNode.Expand();
                }
            }
        }

        private void FormDeptProject_Load(object sender, EventArgs e)
        {
            if (this.m_OperateState == EnumOperateState.Query)
            {
                this.btnOk.Visible = false;
            }
            else
                this.btnOk.Visible = true;
            //初始化
            this.InitializeOrgProInfo();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdataOrgProject();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 初始化部门信息
        /// </summary>
        /// Write:2010-9-29
        private void InitializeOrgProInfo()
        {
            if (this.m_OrgInfo == null) return;
            int index = 0;
            //初始化基本信息
            this.txtName.Text = this.m_OrgInfo.ORG_NAME;
            this.txtDescription.Text = this.m_OrgInfo.DESCRIPTION;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            //初始化角色内容信息
            string strHQL = "from AGSDM_ORG_PROJECT t where t.ORG_ID ='" + this.m_OrgInfo.ORG_ID + "'";
            IList tListOrgProject = tEntityHandler.GetEntities(strHQL);
            for (int i = 0; i < tListOrgProject.Count; i++)
            {
                AGSDM_ORG_PROJECT tOrgProject = tListOrgProject[i] as AGSDM_ORG_PROJECT;
                strHQL = "from AGSDM_PROJECT t where t.ID ='" + tOrgProject.PROJECT_ID + "'";
                IList tListProject = tEntityHandler.GetEntities(strHQL);
                for (int j = 0; j < tListProject.Count; j++)
                {
                    AGSDM_PROJECT tProject = tListProject[j] as AGSDM_PROJECT;
                    ListViewItem tListViewItem = new ListViewItem();
                    tListViewItem.Text = index.ToString();
                    tListViewItem.SubItems.Add(tProject.PROJECT_NAME);
                    this.listDeProject.Items.Add(tListViewItem);
                    index++;
                }
            }
        }

        /// <summary>
        /// Depiction:更新部门工程表
        /// </summary>
        /// Write :徐斌
        /// Create Date：2010-9-29
        private void UpdataOrgProject()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string roleSQL = "from AGSDM_ORG_PROJECT t where t.ORG_ID='" + this.m_OrgInfo.ORG_ID + "'";
            IList tListOrgProject= tEntityHandler.GetEntities(roleSQL);
            AGSDM_ORG_PROJECT tOrgProject = new AGSDM_ORG_PROJECT();
            for (int i = 0; i < tListOrgProject.Count; i++)
            {
                tOrgProject = tListOrgProject[i] as AGSDM_ORG_PROJECT;
                tEntityHandler.DeleteEntity(tOrgProject);
            }
            foreach (TreeNode pTreeNode in this.treeProject.Nodes)
            {
                if (pTreeNode.Checked == true)
                {
                    AGSDM_PROJECT tProject = pTreeNode.Tag as AGSDM_PROJECT;
                    tOrgProject.ORG_ID = this.m_OrgInfo.ORG_ID;
                    tOrgProject.PROJECT_ID = tProject.ID;
                    tEntityHandler.AddEntity(tOrgProject);
                }
            }
        }
    }
}