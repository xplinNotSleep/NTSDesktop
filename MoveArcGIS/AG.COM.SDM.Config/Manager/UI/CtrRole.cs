using AG.COM.SDM.DAL;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 角色用户控件
    /// </summary>
    /// Rewrite:徐斌 2010-9-10
    public partial class CtrRole : UserControl,IPrivilegeOperate 
    {              
        private TreeView m_TreeMenu;

        private IHookHelper m_HookHelper = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CtrRole(IHookHelper tHookHelper)
        {
            InitializeComponent();

            m_HookHelper = tHookHelper;
        }

        #region IPrivilegeOperate 成员
      
        /// <summary>
        /// 初始化处理
        /// </summary>
        /// Rewrite:2010-9-10
        public void Init()
        {
            string strHQL = "from AGSDM_ROLE t";
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            IList tListRole = tEntityHandler.GetEntities(strHQL);
            for (int i = 0; i < tListRole.Count; i++)
            {
                //读取数据库中的数据信息
                AGSDM_ROLE tRole = tListRole[i] as AGSDM_ROLE;
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = tRole.ROLE_NAME;
                tListViewItem.SubItems.Add(tRole.DESCRIPTION);
                tListViewItem.Tag = tRole;
                this.listRole.Items.Add(tListViewItem);
            }
        }

        #endregion 
        /// <summary>
        /// Depiction:添加角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormRoleInfo tFormRoleInfo = new FormRoleInfo();
                tFormRoleInfo.ShowInTaskbar = false;
                tFormRoleInfo.OperateState = EnumOperateState.Add;

                if (tFormRoleInfo.ShowDialog() == DialogResult.OK)
                {
                    AGSDM_ROLE tRole = tFormRoleInfo.pRole;
                    ListViewItem tListViewItem = new ListViewItem();
                    tListViewItem.Text = tRole.ROLE_NAME;
                    tListViewItem.SubItems.Add(tRole.DESCRIPTION);
                    tListViewItem.Tag = tRole;
                    this.listRole.Items.Add(tListViewItem);
                    tListViewItem.Selected = true;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        /// <summary>
        /// Depiction:修改角色信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listRole.SelectedItems.Count > 0)
                {
                    ListViewItem tListViewItem = this.listRole.SelectedItems[0];
                    AGSDM_ROLE tRole = tListViewItem.Tag as AGSDM_ROLE;
                    FormRoleInfo tFormRoleInfo = new FormRoleInfo();
                    tFormRoleInfo.ShowInTaskbar = false;
                    //设置窗体类型
                    tFormRoleInfo.OperateState = EnumOperateState.Modify;
                    tFormRoleInfo.pRole = tRole;
                    if (tFormRoleInfo.ShowDialog() == DialogResult.OK)
                    {
                        tListViewItem.Text = tRole.ROLE_NAME;
                        tListViewItem.SubItems[1].Text = tRole.DESCRIPTION;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void btnRightConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listRole.SelectedItems.Count > 0)
                {
                    ListViewItem tListViewItem = this.listRole.SelectedItems[0];
                    AGSDM_ROLE tRole = tListViewItem.Tag as AGSDM_ROLE;

                    FormUIMenuRoleConfig formUIMenuRoleConfig = new FormUIMenuRoleConfig();
                    formUIMenuRoleConfig.Role = tRole;

                    formUIMenuRoleConfig.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void btnDataRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listRole.SelectedItems.Count > 0)
                {
                    ListViewItem tListViewItem = this.listRole.SelectedItems[0];
                    AGSDM_ROLE tRole = tListViewItem.Tag as AGSDM_ROLE;

                    //FormRoleDataRightConfig tFormRoleDataRightConfig = new FormRoleDataRightConfig(m_HookHelper);
                    //tFormRoleDataRightConfig.Role = tRole;

                    //tFormRoleDataRightConfig.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Depiction:删除角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.listRole.SelectedItems.Count > 0)
                    {
                        string indexRole = null;
                        //从数据库中移除
                        ListViewItem tListViewItem = this.listRole.SelectedItems[0];
                        AGSDM_ROLE tRole = tListViewItem.Tag as AGSDM_ROLE;
                        indexRole = tRole.ROLE_ID.ToString();
                        EntityHandler tEntityHandle = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                        tEntityHandle.DeleteEntity(tRole);                        
                        //删除当前角色的所有角色图层关联记录
                        string tableName = tEntityHandle.GetEntityTableName(typeof(AGSDM_ROLELAYERRLT));
                        tEntityHandle.ExecuteNonQuery("DELETE FROM " + tableName +
                            " WHERE ROLEID = " + tRole.ROLE_ID);
                        //删除角色菜单表中的记录
                        string strHQL = "from AGSDM_ROLE_MENU t where t.ROLE_ID='" + indexRole + "'";
                        IList tListRoleMenu = tEntityHandle.GetEntities(strHQL);
                        for (int i = 0; i < tListRoleMenu.Count; i++)
                        {
                            AGSDM_ROLE_MENU tRoleMenu = tListRoleMenu[i] as AGSDM_ROLE_MENU;
                            tEntityHandle.DeleteEntity(tRoleMenu);
                        }
                        //移除选择项
                        int index = tListViewItem.Index < this.listRole.Items.Count - 1 ? tListViewItem.Index + 1 : 0;
                        if (index == -1) return;
                        ListViewItem pListVitem = this.listRole.Items[index];
                        this.listRole.SelectedItems.Clear();
                        pListVitem.Selected = true;
                        this.listRole.Items.Remove(tListViewItem);
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listRole.SelectedItems.Count > 0)
                {
                    ListViewItem tListViewItem = this.listRole.SelectedItems[0];
                    AGSDM_ROLE tRole = tListViewItem.Tag as AGSDM_ROLE;
                    FormRoleInfo tFormRoleInfo = new FormRoleInfo();
                    tFormRoleInfo.ShowInTaskbar = false;
                    tFormRoleInfo.OperateState = EnumOperateState.Query;
                    tFormRoleInfo.pRole = tRole;

                    tFormRoleInfo.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void listRole_SelectedIndexChanged(object sender, EventArgs e)
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

        /// <summary>
        /// 设置对象的可用状态
        /// </summary>
        private void SetButtonEnabled()
        {
            if (this.listRole.SelectedItems.Count > 0)
            {
                AGSDM_ROLE tRole = this.listRole.SelectedItems[0].Tag as AGSDM_ROLE;

                if (string.Equals(tRole.ROLE_CODE, "00") == true)
                {
                    this.btnDelete.Enabled = false;
                    this.btnModify.Enabled = false;
                }
                else
                {
                    this.btnDelete.Enabled = true;
                    this.btnModify.Enabled = true;
                }
                
                this.btnQuery.Enabled = true;
                this.btnRightConfig.Enabled = true;
                this.btnDataRight.Enabled = true;
            }
            else
            {
                this.btnModify.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnRightConfig.Enabled = false;
                this.btnDataRight.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }


    }
}
