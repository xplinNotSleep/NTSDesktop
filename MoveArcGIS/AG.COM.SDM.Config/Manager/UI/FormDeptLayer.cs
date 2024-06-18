using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    public partial class FormDeptLayer : Form
    {
        /// <summary>
        /// Depiction:部门图层窗体类
        /// </summary>
        private EnumOperateState m_OperateState;//设置窗体状态
        private AGSDM_ORG m_OrgInfo;

        public FormDeptLayer()
        {
            InitializeComponent();
            this.m_OrgInfo = new AGSDM_ORG();
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
                return this.treeLayer;
            }
            set
            {
                if (value == null) return;

                TreeView ptreeMenu = value;

                //删除所有节点
                this.treeLayer.Nodes.Clear();

                foreach (TreeNode childNode in ptreeMenu.Nodes)
                {
                    this.treeLayer.Nodes.Add(childNode.Clone() as TreeNode);
                    childNode.Expand();
                }
            }
        }

        private void FormDeptLayer_Load(object sender, EventArgs e)
        {
            if (this.m_OperateState == EnumOperateState.Query)
            {
                this.btnOk.Visible = false;
            }
            else
                this.btnOk.Visible = true;
            //显示
            InitializeOrgLayerInfo();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdataOrgLayer();
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
        private void InitializeOrgLayerInfo()
        {
            if (this.m_OrgInfo == null) return;
            int index = 0;
            //初始化基本信息
            this.txtName.Text = this.m_OrgInfo.ORG_NAME;
            this.txtDescription.Text = this.m_OrgInfo.DESCRIPTION;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            //初始化角色内容信息
            string strHQL = "from AGSDM_ORG_LAYER t where t.ORG_ID ='" + this.m_OrgInfo.ORG_ID + "'";
            IList tListOrgProject = tEntityHandler.GetEntities(strHQL);
            for (int i = 0; i < tListOrgProject.Count; i++)
            {
                AGSDM_ORG_LAYER tOrgLayer = tListOrgProject[i] as AGSDM_ORG_LAYER;
                strHQL = "from AGSDM_LAYER t where t.ID ='" + tOrgLayer.LAYER_ID + "'";
                IList tListLayer = tEntityHandler.GetEntities(strHQL);
                for (int j = 0; j < tListLayer.Count; j++)
                {
                    AGSDM_LAYER tLayer = tListLayer[j] as AGSDM_LAYER;
                    ListViewItem tListViewItem = new ListViewItem();
                    tListViewItem.Text = index.ToString();
                    tListViewItem.SubItems.Add(tLayer.LAYER_NAME);
                    this.listDeptLayer.Items.Add(tListViewItem);
                    index++;
                }
            }
        }

        /// <summary>
        /// Depiction:更新部门图层表
        /// </summary>
        /// Write :徐斌
        /// Create Date：2010-9-30
        private void UpdataOrgLayer()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string roleSQL = "from AGSDM_ORG_LAYER t where t.ORG_ID='" + this.m_OrgInfo.ORG_ID + "'";
            IList tListOrgLayer = tEntityHandler.GetEntities(roleSQL);
            AGSDM_ORG_LAYER tOrgLayer = new AGSDM_ORG_LAYER();
            for (int i = 0; i < tListOrgLayer.Count; i++)
            {
                tOrgLayer = tListOrgLayer[i] as AGSDM_ORG_LAYER;
                tEntityHandler.DeleteEntity(tOrgLayer);
            }
            foreach (TreeNode pTreeNode in this.treeLayer.Nodes)
            {
                if (pTreeNode.Checked == true)
                {
                    AGSDM_LAYER tLayer = pTreeNode.Tag as AGSDM_LAYER;
                    tOrgLayer.ORG_ID = this.m_OrgInfo.ORG_ID;
                    tOrgLayer.LAYER_ID = tLayer.ID;
                    tEntityHandler.AddEntity(tOrgLayer);
                }
            }
        }
    }
}