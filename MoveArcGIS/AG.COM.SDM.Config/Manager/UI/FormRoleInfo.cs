using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 角色信息 窗体类
    /// </summary>
    /// Rewrite:徐斌 2010-9-10
    public partial class FormRoleInfo : Form
    {
        private EnumOperateState m_OperateState;       
        private AGSDM_ROLE m_Role;  //设置角色信息
        private bool m_bCheckChild = false;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormRoleInfo()
        {
            InitializeComponent();
            this.m_OperateState = EnumOperateState.Query;
            this.m_Role = new AGSDM_ROLE();
        }

        /// <summary>
        /// 获取或设置操作状态
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
        /// 获取或设置角色信息
        /// </summary>
        public AGSDM_ROLE pRole
        {
            get
            {
                return this.m_Role;

            }
            set
            {
                this.m_Role = value;
            }
        }

        private void FormRoleInfo_Load(object sender, EventArgs e)
        {
            try
            {               
                if (this.m_OperateState == EnumOperateState.Query)
                {
                    //初始化角色信息
                    InitializeRoleInfo();             

                    DisabledAllControl();
                }
                else if (this.m_OperateState == EnumOperateState.Modify)
                {
                    //初始化角色信息
                    InitializeRoleInfo();
                }
                else if (this.m_OperateState == EnumOperateState.Add)
                {             
                    this.listUser.Enabled = false;
                    this.txtName.SelectionStart = 0;
                  
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                //Rewrite:2010-9-10
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                if (this.txtName.Name.Length == 0)
                {
                    MessageBox.Show("角色名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtName.Focus();
                    return;
                }
                if (this.txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("角色名称不能为空格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtName.Focus();
                    return;
                }
                this.m_Role.ROLE_NAME = this.txtName.Text;
                this.m_Role.DESCRIPTION = this.txtDescription.Text;
                //从角色菜单表中找出数据
                string strHQL = "from AGSDM_ROLE_MENU t where t.ROLE_ID='" + m_Role.ROLE_ID.ToString() + "'";
                IList tListRoleMenu = tEntityHandler.GetEntities(strHQL);
                if (this.m_Role.ROLE_ID == 0)
                {
                    strHQL = "from AGSDM_ROLE as t where t.ROLE_NAME='" + txtName.Text + "' ";
                    IList tListUserName = tEntityHandler.GetEntities(strHQL);
                    if (tListUserName.Count > 0)
                    {
                        MessageBox.Show("该角色名已经注册，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtName.Text = "";
                        this.txtName.Focus();
                        return;
                    }
                    object obj = this.m_Role;
                    tEntityHandler.AddEntity(obj);
                }
                else
                {
                    tEntityHandler.UpdateEntity(this.m_Role, this.m_Role.ROLE_ID);
                }           
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtName.Text, "角色名称") &&
             ValidateData.StringLength(txtName.Text, 10, "角色名称") &&
  ValidateData.StringLength(txtDescription.Text, 100, "描述信息");
        }      

        /// <summary>
        /// 递归调用添加子节点
        /// </summary>
        /// <param name="parentID">父节点ID号</param>  
        /// <param name="parentNode">父节点</param>
        /// Rewrite:2010-9-13
        private void AddChildMenu(string parentID, TreeNode parentNode, IList pListRole)
        {
            //初始化查询条件
            bool boolRoleMenu = false;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_MENU as t where t.PARENT_MENU_ID='" + parentID + "'";
            IList tListChildMenu = tEntityHandler.GetEntities(strHQL);
            if (tListChildMenu.Count == 0) return;
            for (int i = 0; i < tListChildMenu.Count; i++)
            {
                TreeNode tMenuChildNode = new TreeNode();
                AGSDM_MENU tMenuChile = tListChildMenu[i] as AGSDM_MENU;
                tMenuChildNode.Text = tMenuChile.MENU_NAME;
                tMenuChildNode.Tag = tMenuChile;
                for (int j = 0; j < pListRole.Count; j++)
                {
                    AGSDM_ROLE_MENU tRoleMenu = pListRole[j] as AGSDM_ROLE_MENU;
                    if (tRoleMenu.MENU_ID == tMenuChile.ID.ToString())
                    {
                        tMenuChildNode.Checked = true;
                        break;
                    }
                }
                //递归调用添加子节点
                AddChildMenu(tMenuChile.MENU_CODE, tMenuChildNode, pListRole);
                parentNode.Nodes.Add(tMenuChildNode);
            }
        }

        /// <summary>
        /// 是否新分配的角色
        /// </summary>
        /// <param name="lstMenu"></param>
        /// <param name="menuID"></param>
        /// <returns></returns>
        private AGSDM_ROLE_MENU GetRoleMenu(IList lstMenu, decimal menuID)
        {
            for (int i = 0; i < lstMenu.Count; i++)
            {
                AGSDM_ROLE_MENU roleMenu = lstMenu[i] as AGSDM_ROLE_MENU;
                if (menuID.ToString() == roleMenu.MENU_ID)
                    return roleMenu;
            }
            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 私有函数

        /// <summary>
        /// 初始化角色信息
        /// </summary>
        /// Rewrite:2010-9-10
        private void InitializeRoleInfo()
        {
            if (this.m_Role == null) return;
            //初始化基本信息
            this.txtName.Text = this.m_Role.ROLE_NAME;
            this.txtDescription.Text = this.m_Role.DESCRIPTION;

            ShowUser(m_Role);
        }

        /// <summary>
        /// 加载当前角色的用户
        /// </summary>
        /// <param name="tRole"></param>
        private void ShowUser(AGSDM_ROLE tRole)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

            string sql = "";
            ///当是管理员角色时显示所有用户
            if (tRole.ROLE_NAME == "Administrators")
            {
                sql = "from AGSDM_USER_ROLE ";
            }
            else
            {
                sql = "from AGSDM_USER_ROLE where ROLE_ID='" + tRole.ROLE_ID + "'";
            }

            ///加载当前角色的用户
            int i = 1;
            IList<AGSDM_USER_ROLE> tUserRoles = tEntityHandler.GetEntities<AGSDM_USER_ROLE>("from AGSDM_USER_ROLE where ROLE_ID='" + tRole.ROLE_ID + "'");
            foreach (AGSDM_USER_ROLE tUserRole in tUserRoles)
            {
                if (!string.IsNullOrEmpty(tUserRole.USER_ID))
                {
                    decimal tUserID = DataConvert.StrToDec(tUserRole.USER_ID);
                    AGSDM_SYSTEM_USER tSystemUser = tEntityHandler.GetEntity<AGSDM_SYSTEM_USER>("from AGSDM_SYSTEM_USER where USER_ID=" + tUserID);
                    if (tSystemUser != null)
                    {
                        ListViewItem tListViewItem = new ListViewItem();
                        tListViewItem.Text = i++.ToString();
                        tListViewItem.SubItems.Add(tSystemUser.NAME_EN);
                        this.listUser.Items.Add(tListViewItem);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 禁用所有输入类型控件
        /// </summary>
        private void DisabledAllControl()
        {
            txtName.ReadOnly = true;
            txtDescription.ReadOnly = true;          
            btnOk.Enabled = false;
        }
    }
}