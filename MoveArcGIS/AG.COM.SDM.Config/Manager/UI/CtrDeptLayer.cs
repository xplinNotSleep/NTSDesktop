using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    public partial class CtrDeptLayer : UserControl, IPrivilegeOperate
    {
        /// <summary>
        /// Depiction:部门图层管理类
        /// </summary>
        private bool m_IsDirty = false;
        private TreeView m_TreeLayerMenu;
        public CtrDeptLayer()
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
                    this.treeDepLayerment.Nodes.Add(tORGNode);
                    tORGNode.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DoWork()
        {

        }

        #endregion

        /// <summary>
        /// Depiction:添加部门图层权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeDepLayerment.SelectedNode;
            if (tSelNode != null)
            {
                this.InitTreeLayerMenu();
                //DepartmentInfo tDeptInfo = tSelNode.Tag as DepartmentInfo;
                //设置部门信息
                AGSDM_ORG tOrg = tSelNode.Tag as AGSDM_ORG;
                FormDeptLayer tFrmDeptLayer = new FormDeptLayer();
                tFrmDeptLayer.ShowInTaskbar = false;
                tFrmDeptLayer.OperateState = EnumOperateState.Add;
                tFrmDeptLayer.MainMenuTree = this.m_TreeLayerMenu;
                tFrmDeptLayer.pOrgInfo = tOrg;
                tFrmDeptLayer.Show();
            }
        }

        /// <summary>
        /// Depiction:删除部门图层权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //从部门图层表中删除数据
                TreeNode tTreeNede = this.treeDepLayerment.SelectedNode;
                AGSDM_ORG tOrg = tTreeNede.Tag as AGSDM_ORG;
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                string strHQL = "from AGSDM_ORG_LAYER as t where t.ORG_ID='" + tOrg.ORG_ID + "'";
                IList tListProject = tEntityHandler.GetEntities(strHQL);
                for (int i = 0; i < tListProject.Count; i++)
                {
                    AGSDM_ORG_LAYER tOrgProject = tListProject[i] as AGSDM_ORG_LAYER;
                    tEntityHandler.DeleteEntity(tOrgProject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("成功删除该部门的图层！");
            }
        }

        /// <summary>
        /// Depiction:查询部门图层权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeDepLayerment.SelectedNode;
            if (tSelNode != null)
            {
                this.InitTreeLayerMenu();
                //设置部门信息
                AGSDM_ORG tOrg = tSelNode.Tag as AGSDM_ORG;
                FormDeptLayer tFrmDeptLayer = new FormDeptLayer();
                tFrmDeptLayer.ShowInTaskbar = false;
                tFrmDeptLayer.OperateState = EnumOperateState.Query;
                tFrmDeptLayer.MainMenuTree = this.m_TreeLayerMenu;
                tFrmDeptLayer.pOrgInfo = tOrg;
                tFrmDeptLayer.Show();
            }
        }

        /// <summary>
        /// Depiction:改变按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDepLayerment_AfterSelect(object sender, TreeViewEventArgs e)
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
                this.btnAddLayer.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnQuery.Enabled = true;
            }
            else
            {
                this.btnAddLayer.Enabled = false;
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
        private void InitTreeLayerMenu()
        {
            this.m_TreeLayerMenu = new TreeView();
            this.m_TreeLayerMenu.BeginUpdate();

            //初始化查询条件
            bool boolOrgLayer = false;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            IList tListOrgLayer = null;
            string strHQL = "";
            //从部门工程表中找出数据
            TreeNode tTreeNede = this.treeDepLayerment.SelectedNode;
            AGSDM_ORG tOrg = tTreeNede.Tag as AGSDM_ORG;
            strHQL = "from AGSDM_ORG_LAYER t where t.ORG_ID='" + tOrg.ORG_ID + "'";
            tListOrgLayer = tEntityHandler.GetEntities(strHQL);
            //
            strHQL = "from AGSDM_LAYER t";
            IList tListLayer = tEntityHandler.GetEntities(strHQL);
            if (tListLayer.Count == 0) return;
            //判断该部门的工程表
            for (int i = 0; i < tListLayer.Count; i++)
            {
                boolOrgLayer = false;
                TreeNode tOrgNode = new TreeNode();
                AGSDM_LAYER tLayer = tListLayer[i] as AGSDM_LAYER;
                tOrgNode.Text = tLayer.LAYER_NAME;
                for (int j = 0; j < tListOrgLayer.Count; j++)
                {
                    AGSDM_ORG_LAYER tOrgLayer = tListOrgLayer[j] as AGSDM_ORG_LAYER;
                    if (tOrgLayer.LAYER_ID == tLayer.ID)
                    {
                        boolOrgLayer = true;
                    }
                    if (boolOrgLayer == true)
                        tOrgNode.Checked = true;
                }
                tOrgNode.Tag = tLayer;
                this.m_TreeLayerMenu.Nodes.Add(tOrgNode);
            }
        }
        #endregion
    }
}
