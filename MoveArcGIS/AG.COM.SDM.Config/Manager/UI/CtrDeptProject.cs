using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Depiction:部门工程用户控件
    /// </summary>
    public partial class CtrDeptProject : UserControl, IPrivilegeOperate
    {
        private bool m_IsDirty = false;
        private TreeView m_TreeMenu;

        //默认构造函数
        public CtrDeptProject()
        {
            InitializeComponent();
        }

        #region IPrivilegeOperate 成员

        public bool IsDirty
        {
            get
            {
                return this.m_IsDirty;
            }
        }

        public void Init()
        {
            try
            {
                //查询数据库，获取根节点
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                string strHQL = "from AGSDM_ORG t where t.PARENT_ORG_ID='" + 0 + "'";
                IList tListORG = tEntityHandler.GetEntities(strHQL);
                if (tListORG.Count == 0) return;
                for (int i = 0; i < tListORG.Count; i++)
                {
                    TreeNode tORGNode = new TreeNode();
                    AGSDM_ORG tORGpartment = tListORG[i] as AGSDM_ORG;
                    tORGNode.Text = tORGpartment.ORG_NAME;
                    tORGNode.Tag = tORGpartment;
                    //递归添加子节点
                    this.AddChildNode(tORGpartment.ORG_CODE, tORGNode);
                    this.treeDeProject.Nodes.Add(tORGNode);
                    tORGNode.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //throw new Exception("The method or operation is not implemented.");
        }

        public void DoWork()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        /// Depiction:添加地图工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProject_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeDeProject.SelectedNode;
            if (tSelNode != null)
            {
                this.InitTreeMenu();

                //设置部门信息
                AGSDM_ORG tORG = tSelNode.Tag as AGSDM_ORG;
                FormDeptProject tFrmDeptPrj = new FormDeptProject();
                tFrmDeptPrj.ShowInTaskbar = false;

                tFrmDeptPrj.OperateState = EnumOperateState.Add;
                tFrmDeptPrj.MainMenuTree = this.m_TreeMenu;
                tFrmDeptPrj.pOrgInfo = tORG;
                tFrmDeptPrj.Show();
            }
        }

        /// <summary>
        /// Depiction:删除地图工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //从部门工程表中删除数据
                TreeNode tTreeNede = this.treeDeProject.SelectedNode;
                AGSDM_ORG tOrg = tTreeNede.Tag as AGSDM_ORG;
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                string strHQL = "from AGSDM_ORG_PROJECT as t where t.ORG_ID='" + tOrg.ORG_ID + "'";
                IList tListProject = tEntityHandler.GetEntities(strHQL);
                for (int i = 0; i < tListProject.Count; i++)
                {
                    AGSDM_ORG_PROJECT tOrgProject = tListProject[i] as AGSDM_ORG_PROJECT;
                    tEntityHandler.DeleteEntity(tOrgProject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("成功删除该部门的地图工程！");
            }
        }

        /// <summary>
        /// Depiction:查询地图工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeDeProject.SelectedNode;
            if (tSelNode != null)
            {
                this.InitTreeMenu();

                //设置部门信息
                AGSDM_ORG tORG = tSelNode.Tag as AGSDM_ORG;
                FormDeptProject tFrmDeptPrj = new FormDeptProject();
                tFrmDeptPrj.ShowInTaskbar = false;

                tFrmDeptPrj.OperateState = EnumOperateState.Query;
                tFrmDeptPrj.MainMenuTree = this.m_TreeMenu;
                tFrmDeptPrj.pOrgInfo = tORG;
                tFrmDeptPrj.Show();
            }
        }

        /// <summary>
        /// Depiction:设置可选状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDeProtment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //设置对象的可用状态
            SetButtonEnable(e.Node);
        }

        /// <summary>
        ///Depiction: 设置对象的可用状态
        /// </summary>
        /// <param name="treeNode"></param>
        private void SetButtonEnable(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                this.btnAddProject.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnQuery.Enabled = true;
            }
            else
            {
                this.btnAddProject.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnQuery.Enabled = false;
            }
        }

        /// <summary>
        ///Depiction: 递归添加子节点
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <param name="parentNode">父节点</param>
        private void AddChildNode(string parentId, TreeNode parentNode)
        {
            //递归查找数据库中的节点
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ORG as t where t.PARENT_ORG_ID='" + parentId + "'";
            IList tListChildORG = tEntityHandler.GetEntities(strHQL);
            if (tListChildORG.Count == 0) return;
            for (int i = 0; i < tListChildORG.Count; i++)
            {
                TreeNode tORGChildNode = new TreeNode();
                AGSDM_ORG tORGChile = tListChildORG[i] as AGSDM_ORG;
                tORGChildNode.Text = tORGChile.ORG_NAME;
                tORGChildNode.Tag = tORGChile;

                //递归添加子节点
                AddChildNode(tORGChile.ORG_CODE, tORGChildNode);
                parentNode.Nodes.Add(tORGChildNode);
            }
        }

        #region 初始化功能菜单树对象
        /// <summary>
        /// 初始化功能菜单树
        /// </summary>
        /// Rewrite:2010-9-13
        private void InitTreeMenu()
        {
            this.m_TreeMenu = new TreeView();
            this.m_TreeMenu.BeginUpdate();

            //初始化查询条件
            bool boolOrgProject = false;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            IList tListOrgProject = null;
            string strHQL = "";
            //从部门工程表中找出数据
            TreeNode tTreeNede = this.treeDeProject.SelectedNode;
            AGSDM_ORG tRole = tTreeNede.Tag as AGSDM_ORG;
            strHQL = "from AGSDM_ORG_PROJECT t where t.ORG_ID='" + tRole.ORG_ID + "'";
            tListOrgProject = tEntityHandler.GetEntities(strHQL);
            //
            strHQL = "from AGSDM_PROJECT t";
            IList tListProject = tEntityHandler.GetEntities(strHQL);
            if (tListProject.Count == 0) return;
            //判断该部门的工程表
            for (int i = 0; i < tListProject.Count; i++)
            {
                boolOrgProject = false;
                TreeNode tOrgNode = new TreeNode();
                AGSDM_PROJECT tProject = tListProject[i] as AGSDM_PROJECT;
                tOrgNode.Text =tProject.PROJECT_NAME ;
                for (int j = 0; j < tListOrgProject.Count; j++)
                {
                    AGSDM_ORG_PROJECT tOrgProject = tListOrgProject[j] as AGSDM_ORG_PROJECT;
                    if (tOrgProject.PROJECT_ID == tProject.ID)
                    {
                        boolOrgProject = true;
                    }
                    if (boolOrgProject == true)
                        tOrgNode.Checked = true;
                }
                tOrgNode.Tag = tProject;
                this.m_TreeMenu.Nodes.Add(tOrgNode);
            }
        }
        #endregion
    }
}
