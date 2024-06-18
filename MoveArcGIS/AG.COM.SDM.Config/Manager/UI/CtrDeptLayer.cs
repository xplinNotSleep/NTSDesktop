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
        /// Depiction:����ͼ�������
        /// </summary>
        private bool m_IsDirty = false;
        private TreeView m_TreeLayerMenu;
        public CtrDeptLayer()
        {
            InitializeComponent();
        }

        #region IPrivilegeOperate ��Ա

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
                //��ѯ���ݿ⣬��ȡ���ڵ�
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
                    //�ݹ�����ӽڵ�
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
        /// Depiction:��Ӳ���ͼ��Ȩ��
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
                //���ò�����Ϣ
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
        /// Depiction:ɾ������ͼ��Ȩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //�Ӳ���ͼ�����ɾ������
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
                MessageBox.Show("�ɹ�ɾ���ò��ŵ�ͼ�㣡");
            }
        }

        /// <summary>
        /// Depiction:��ѯ����ͼ��Ȩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeDepLayerment.SelectedNode;
            if (tSelNode != null)
            {
                this.InitTreeLayerMenu();
                //���ò�����Ϣ
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
        /// Depiction:�ı䰴ť״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDepLayerment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //���ö���Ŀ���״̬
            SetButtonEnable(e.Node);
        }

        /// <summary>
        ///Depiction: ���ö���Ŀ���״̬
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
        ///Depiction: �ݹ�����ӽڵ�
        /// </summary>
        /// <param name="parentId">���ڵ�ID</param>
        /// <param name="parentNode">���ڵ�</param>
        private void AddChildNode(string parentId, TreeNode parentNode)
        {
            //�ݹ�������ݿ��еĽڵ�
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
                //�ݹ�����ӽڵ�
                AddChildNode(tORGChile.ORG_CODE, tORGChildNode);
                parentNode.Nodes.Add(tORGChildNode);
            }
        }

        #region ��ʼ�����ܲ˵�������
        /// <summary>
        /// ��ʼ�����ܲ˵���
        /// </summary>
        /// Rewrite:2010-9-13
        private void InitTreeLayerMenu()
        {
            this.m_TreeLayerMenu = new TreeView();
            this.m_TreeLayerMenu.BeginUpdate();

            //��ʼ����ѯ����
            bool boolOrgLayer = false;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            IList tListOrgLayer = null;
            string strHQL = "";
            //�Ӳ��Ź��̱����ҳ�����
            TreeNode tTreeNede = this.treeDepLayerment.SelectedNode;
            AGSDM_ORG tOrg = tTreeNede.Tag as AGSDM_ORG;
            strHQL = "from AGSDM_ORG_LAYER t where t.ORG_ID='" + tOrg.ORG_ID + "'";
            tListOrgLayer = tEntityHandler.GetEntities(strHQL);
            //
            strHQL = "from AGSDM_LAYER t";
            IList tListLayer = tEntityHandler.GetEntities(strHQL);
            if (tListLayer.Count == 0) return;
            //�жϸò��ŵĹ��̱�
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
