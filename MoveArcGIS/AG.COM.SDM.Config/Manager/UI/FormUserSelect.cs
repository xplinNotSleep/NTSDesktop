using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 用户选择界面
    /// </summary>
    public partial class FormUserSelect : Form
    {
        /// <summary>
        /// 选择的用户实体类
        /// </summary>
        public AGSDM_SYSTEM_USER SelectedUser { get; private set; }

        /// <summary>
        /// 原来设置的用户编号
        /// </summary>
        public decimal OriginalUserID { get; set; }

        /// <summary>
        /// 当前选中的节点
        /// </summary>
        private TreeNode m_CurSelectedNode;

        /// <summary>
        /// 用户列表
        /// </summary>
        private IList m_ListUserEntity;

        public FormUserSelect()
        {
            InitializeComponent();
        }

        private void FormUserSelect_Load(object sender, EventArgs e)
        {
            CustomInitial();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvwUsers.Nodes)
            {
                if (node.Checked)
                {
                    m_CurSelectedNode = node;
                    break;
                }
            }
            if (m_CurSelectedNode == null)
                return;
      
            if (m_CurSelectedNode.Checked)
            {
                SelectedUser = m_CurSelectedNode.Tag as AGSDM_SYSTEM_USER;
            }
            else
            {
                SelectedUser = null;   
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tvwUsers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                UnCheckCurNode(e.Node);
                m_CurSelectedNode = e.Node;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tvwUsers.Nodes.Clear();

            string strValue = tbUser.Text;
            if (string.IsNullOrEmpty(strValue))
            {
                for (int i = 0; i < m_ListUserEntity.Count; i++)
                {
                    AGSDM_SYSTEM_USER user = m_ListUserEntity[i] as AGSDM_SYSTEM_USER;
                    TreeNode node = new TreeNode();
                    node.Text = user.NAME_EN + "(" + user.NAME_CN + ")";
                    node.Tag = user;
                    tvwUsers.Nodes.Add(node);
                }
            }
            else
            {
                for (int i = 0; i < m_ListUserEntity.Count; i++)
                {
                    AGSDM_SYSTEM_USER user = m_ListUserEntity[i] as AGSDM_SYSTEM_USER;
                    if ((user.NAME_EN != null && user.NAME_EN.Contains(strValue)) ||
                        (user.NAME_CN != null && user.NAME_CN.Contains(strValue)))
                    {
                        TreeNode node = new TreeNode();
                        node.Text = user.NAME_EN + "(" + user.NAME_CN + ")";
                        node.Tag = user;
                        tvwUsers.Nodes.Add(node);
                    }
                }
            }
        }

        /// <summary>
        /// 自定义初始化
        /// </summary>
        private void CustomInitial()
        {
            AbstractFactory factory = AbstractFactory.GetInstance();
            IUser userManage = factory.CreateUser();
            List<AGSDM_SYSTEM_USER> lstUser = userManage.GetUserList();

            m_ListUserEntity = lstUser;
            for (int i = 0; i < lstUser.Count; i++)
            {
                AGSDM_SYSTEM_USER user = lstUser[i] as AGSDM_SYSTEM_USER;
                TreeNode node = new TreeNode();
                node.Text = user.NAME_EN + "(" + user.NAME_CN + ")";
                node.Tag = user;
                tvwUsers.Nodes.Add(node);
                if (user.USER_ID.Equals(OriginalUserID))
                {
                    node.Checked = true;
                    m_CurSelectedNode = node;
                }
                else
                {
                    node.Checked = false;
                }
            }
        }

        /// <summary>
        /// 取消当前节点的选中状态
        /// </summary>
        private void UnCheckCurNode(TreeNode node)
        {
            if (node.Equals(m_CurSelectedNode))
                return;

            if (m_CurSelectedNode == null)
                return;

            m_CurSelectedNode.Checked = false;
        }

    }
}
