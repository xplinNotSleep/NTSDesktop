using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Depiction:权限管理之用户控件
    /// </summary>
    /// Rewriter:徐斌
    /// Create Date:2010-9-9
    public partial class CtrUser : UserControl, IPrivilegeOperate
    {       
        private IList m_ListRoleInfo;
        private TreeView m_DeptTree;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CtrUser()
        {
            InitializeComponent();
        }

        //添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormUserInfo tFormUserInfo = new FormUserInfo();
                tFormUserInfo.ShowInTaskbar = false;
                tFormUserInfo.OperateState = EnumOperateState.Add;

                //设置角色信息
                tFormUserInfo.SetRolesInfo(this.m_ListRoleInfo);

                //设置部门信息
                tFormUserInfo.SetDeptInfo(this.m_DeptTree);

                if (tFormUserInfo.ShowDialog() == DialogResult.OK)
                {
                    AGSDM_SYSTEM_USER tUser = tFormUserInfo.pUserLogger;
                    ListViewItem tListViewItem = new ListViewItem();
                    tListViewItem.Text = tUser.NAME_EN;
                    tListViewItem.SubItems.Add(tUser.NAME_CN);
                    tListViewItem.SubItems.Add(tUser.DESCRIPTION);
                    tListViewItem.Tag = tUser;
                    this.listUser.Items.Add(tListViewItem);
                    tListViewItem.Selected = true;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        //修改
        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listUser.SelectedItems.Count > 0)
                {
                    //获取选择项
                    ListViewItem tListViewItem = this.listUser.SelectedItems[0];
                    AGSDM_SYSTEM_USER tUserInfo = tListViewItem.Tag as AGSDM_SYSTEM_USER;

                    //实例化用户信息窗体类
                    FormUserInfo tFormUserInfo = new FormUserInfo();
                    tFormUserInfo.ShowInTaskbar = false;
                    tFormUserInfo.OperateState = EnumOperateState.Modify;
                    tFormUserInfo.pUserLogger = tUserInfo;
                    //设置角色信息
                    tFormUserInfo.SetRolesInfo(this.m_ListRoleInfo);

                    //设置部门信息
                    tFormUserInfo.SetDeptInfo(this.m_DeptTree);
                    if (tFormUserInfo.ShowDialog() == DialogResult.OK)
                    {
                        tListViewItem.Text = tUserInfo.NAME_EN;
                        tListViewItem.SubItems[1].Text = tUserInfo.NAME_CN;
                        tListViewItem.SubItems[2].Text = tUserInfo.DESCRIPTION;
                        tListViewItem.Tag = tFormUserInfo.pUserLogger;
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.listUser.SelectedItems.Count > 0)
                    {
                        string indexUserID = null;
                        //从数据库中移除
                        ListViewItem tListViewItem = this.listUser.SelectedItems[0];
                        AGSDM_SYSTEM_USER tUser = tListViewItem.Tag as AGSDM_SYSTEM_USER;
                        indexUserID = tUser.USER_ID.ToString();
                        EntityHandler tEntityHandle = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                        tEntityHandle.DeleteEntity(tUser);
                        //删除用户部门表中的记录                
                        string strHQL = "from AGSDM_USER_ORG t where t.USER_ID='" + indexUserID + "'";
                        IList tListUserOrg = tEntityHandle.GetEntities(strHQL);
                        for (int i = 0; i < tListUserOrg.Count; i++)
                        {
                            AGSDM_USER_ORG tUserOrg = tListUserOrg[i] as AGSDM_USER_ORG;
                            tEntityHandle.DeleteEntity(tUserOrg);
                        }
                        //删除用户角色表中的记录
                        strHQL = "from AGSDM_USER_ROLE t where t.USER_ID='" + indexUserID + "'";
                        IList tListUserRole = tEntityHandle.GetEntities(strHQL);
                        for (int j = 0; j < tListUserRole.Count; j++)
                        {
                            AGSDM_USER_ROLE tUserRole = tListUserRole[j] as AGSDM_USER_ROLE;
                            tEntityHandle.DeleteEntity(tUserRole);
                        }
                        //移除选择项
                        int index = tListViewItem.Index < this.listUser.Items.Count - 1 ? tListViewItem.Index + 1 : 0;
                        if (index == -1) return;
                        ListViewItem pListVitem = this.listUser.Items[index];
                        this.listUser.SelectedItems.Clear();
                        pListVitem.Selected = true;
                        this.listUser.Items.Remove(tListViewItem);
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listUser.SelectedItems.Count > 0)
                {
                    //获取选择项
                    ListViewItem tListViewItem = this.listUser.SelectedItems[0];
                    AGSDM_SYSTEM_USER tUserInfo = tListViewItem.Tag as AGSDM_SYSTEM_USER;

                    //实例化用户信息窗体类
                    FormUserInfo tFormUserInfo = new FormUserInfo();
                    tFormUserInfo.ShowInTaskbar = false;
                    tFormUserInfo.OperateState = EnumOperateState.Query;
                    tFormUserInfo.pUserLogger = tUserInfo;
                    //设置角色信息
                    tFormUserInfo.SetRolesInfo(this.m_ListRoleInfo);
                    //设置部门信息
                    tFormUserInfo.SetDeptInfo(this.m_DeptTree);
                    if (tFormUserInfo.ShowDialog() == DialogResult.OK)
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void listUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //设置对象的可用状态
                SetButtonEnabled();           
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        #region IPrivilegeOperate 成员

        /// <summary>
        /// 初始化处理
        /// </summary>
        /// Rewrite:2010-9-9
        public void Init()
        {
            #region 初始化用户信息
            //Rewriter:2010-9-9
            string strHQL = "from AGSDM_SYSTEM_USER t";
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            IList tListUser = tEntityHandler.GetEntities(strHQL);
            for (int i = 0; i < tListUser.Count; i++)
            {
                AGSDM_SYSTEM_USER tUser = tListUser[i] as AGSDM_SYSTEM_USER;
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = tUser.NAME_EN;
                tListViewItem.SubItems.Add(tUser.NAME_CN);
                tListViewItem.SubItems.Add(tUser.DESCRIPTION);
                tListViewItem.Tag = tUser;
                this.listUser.Items.Add(tListViewItem);
            }
            #endregion

            //获取部门信息
            this.m_DeptTree = GetTreeDeptInfo();

            //获取角色信息
            this.m_ListRoleInfo = GetListRoleInfo();
        }

        #endregion     

        /// <summary>
        /// 设置对象的可用状态
        /// </summary>
        private void SetButtonEnabled()
        {
            if (this.listUser.SelectedItems.Count > 0)
            {
                this.btnModify.Enabled = true;
                this.btnQuery.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnModify.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        #region 获取部门信息
        /// <summary>
        /// 获取所有部门名称
        /// </summary>
        /// <returns></returns>
        /// Rewrite:2010-9-14
        private TreeView GetTreeDeptInfo()
        {
            //获取部门信息表
            TreeView tDeptTree = new TreeView();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ORG as t where t.PARENT_ORG_ID <= 0";
            IList tListChildORG = tEntityHandler.GetEntities(strHQL);
            if (tListChildORG.Count == 0) return null;
            for (int i = 0; i < tListChildORG.Count; i++)
            {
                TreeNode tORGChildNode = new TreeNode();
                AGSDM_ORG tORGChile = tListChildORG[i] as AGSDM_ORG;
                tORGChildNode.Text = tORGChile.ORG_NAME;
                tORGChildNode.Tag = tORGChile;
                AddChildNode(tORGChile.ORG_ID, tORGChildNode);
                tDeptTree.Nodes.Add(tORGChildNode);
                tORGChildNode.Expand();
            }
            return tDeptTree;
        }

        /// <summary>
        /// 递归添加子节点
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <param name="parentNode">父节点</param>
        private void AddChildNode(decimal parentId, TreeNode parentNode)
        {
            //从数据库中查找数据
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ORG as t where t.PARENT_ORG_ID=" + parentId;
            IList tListChildORG = tEntityHandler.GetEntities(strHQL);
            if (tListChildORG.Count == 0) return;
            for (int i = 0; i < tListChildORG.Count; i++)
            {
                TreeNode tORGChildNode = new TreeNode();
                AGSDM_ORG tORGChile = tListChildORG[i] as AGSDM_ORG;
                tORGChildNode.Text = tORGChile.ORG_NAME;
                tORGChildNode.Tag = tORGChile;              
                //递归调用函数添加节点
                AddChildNode(tORGChile.ORG_ID, tORGChildNode);
                parentNode.Nodes.Add(tORGChildNode);
            }
        }
        #endregion

        /// <summary>
        /// 获取所有角色名称
        /// </summary>
        /// <returns>返回角色名称字符组</returns>
        private IList GetListRoleInfo()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ROLE t";
            IList tListROLE = tEntityHandler.GetEntities(strHQL);
            return tListROLE;
        }
    }
}
