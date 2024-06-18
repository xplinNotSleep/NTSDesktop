using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 用户信息窗体类
    /// </summary>
    /// Rewrite:2010-9-9
    public partial class FormUserInfo : Form
    {
        private EnumOperateState m_OperateState;    //操作类型
        private AGSDM_SYSTEM_USER m_User;           //用户信息
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormUserInfo()
        {
            InitializeComponent();

            this.m_OperateState = EnumOperateState.Query;
            this.m_User = new AGSDM_SYSTEM_USER();
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
                this.m_OperateState=value;
            }
        }

        /// <summary>
        /// 获取或设置用户信息
        /// </summary>
        public AGSDM_SYSTEM_USER pUserLogger
        {
            get
            {
                return this.m_User;
            }
            set
            {
                this.m_User = value;
            }
        }

        private void FormUserInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.m_OperateState == EnumOperateState.Query)
                {                  
                    //初始化用户信息
                    InitializeUserInfo();

                    DisabledAllControl();
                }
                else if (this.m_OperateState == EnumOperateState.Modify)
                {
                    //设置输入焦点
                    this.txtName.Focus();
                    //初始化用户信息
                    InitializeUserInfo();
                }
                else
                {
                    //设置输入焦点
                    this.txtName.Focus();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                if (this.txtPw.Text.Trim().Length == 0)
                {
                    MessageBox.Show("不能只以空格作为密码", "提示！");
                    this.txtPw.Focus();
                    return;
                }
                else if (this.txtPw.Text.Equals(this.txtPw2.Text, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    MessageBox.Show("再次输入密码有误，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPw2.Focus();
                    return;
                }
                //给AGSDM_USER1对象赋值
                this.m_User.NAME_EN = this.txtName.Text;
                this.m_User.NAME_CN = this.txtFullName.Text;
                this.m_User.PASSWORD = this.txtPw.Text;
                this.m_User.DESCRIPTION = this.txtDescription.Text;
                this.m_User.NAME_CN = this.txtFullName.Text;
                this.m_User.CTEATE_TIME = DateTime.Now;
                ////获取AGSDM_ORG对象
                //AGSDM_ORG tORG = this.cmbTreeDepart.Tag as AGSDM_ORG;

                AGSDM_SYSTEM_USER tUserSameName = tEntityHandler.GetEntity<AGSDM_SYSTEM_USER>("from AGSDM_SYSTEM_USER as t where t.NAME_EN=?", txtName.Text);

                //直接添加
                if (this.m_User.USER_ID == 0)
                {
                    //增加的时候进行用户名检查                  
                    if (tUserSameName != null)
                    {
                        MessageBox.Show("该用户名已经注册，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtName.Text = "";
                        this.txtName.Focus();
                        return;
                    }

                    ////这边在插入任务时前先找到最大的USER_ID，然后再插入，不然会提示“?,?,?,?,....”之类的提示
                    ////  [徐东江 2019-8-21]
                    string strHQL = "from AGSDM_SYSTEM_USER ORDER BY USER_ID DESC";
                    IList tListUser = tEntityHandler.GetEntities(strHQL);
                    if(tListUser.Count > 0)
                    {
                        AGSDM_SYSTEM_USER userTemp = tListUser[0] as AGSDM_SYSTEM_USER;
                        if(userTemp != null)
                        {
                            m_User.USER_ID = userTemp.USER_ID;
                            m_User.USER_ID++;
                        }
                    }

                    //添加到用户表
                    object obj = m_User;
                    tEntityHandler.AddEntity(obj);
                    //添加到用户角色表
                    bool madeRoleID = GetRoleID();
                    if (!madeRoleID)//未勾选所属部门时，返回
                    {
                        tEntityHandler.DeleteEntity(obj);
                        this.m_User = new AGSDM_SYSTEM_USER();
                        return;
                    }
                }
                else
                {
                    //修改时用户名检查
                    if (tUserSameName != null && tUserSameName.USER_ID != m_User.USER_ID)
                    {
                        MessageBox.Show("该用户名已经注册，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtName.Text = "";
                        this.txtName.Focus();
                        return;
                    }

                    //实现更新
                    tEntityHandler.UpdateEntity(this.m_User, this.m_User.USER_ID);
                    //UpdataUserOrg(tORG);
                    UpdataUserRole();

                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Depiction:当属性值发生改变时改变他的tag属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combTreeDepart_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (this.cmbTreeDepart.TreeView.SelectedNode == null) return;
            //TreeNode tTreeNode = this.cmbTreeDepart.TreeView.SelectedNode;
            //this.cmbTreeDepart.Tag = tTreeNode.Tag;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtName.Text, "名称") &&
             ValidateData.StringLength(txtName.Text, 25, "名称") &&
             ValidateData.NotNull(txtFullName.Text, "全名") &&
 ValidateData.StringLength(txtFullName.Text, 25, "全名") &&
  ValidateData.StringLength(txtDescription.Text, 50, "描述信息") &&
   ValidateData.NotNull(txtPw.Text, "密码") &&
   ValidateData.StringLength(txtPw.Text, 25, "密码");
        }

        /// <summary>
        /// 设置角色信息
        /// </summary>
        /// <param name="pListRoleInfo">角色信息</param>
        public void SetRolesInfo(IList pListRoleInfo)
        {
            if (pListRoleInfo.Count == 0) return;

            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_USER_ROLE t where t.USER_ID='" + this.m_User.USER_ID + "'";
            IList tListUSER = tEntityHandler.GetEntities(strHQL);
            int index = -1;
            bool tBool = false;
            for (int i = 0; i < pListRoleInfo.Count; i++)
            {
                TreeNode tRoleNode = new TreeNode();
                AGSDM_ROLE tROLE = pListRoleInfo[i] as AGSDM_ROLE;

                tRoleNode.Text = tROLE.ROLE_NAME;
                tRoleNode.Tag = tROLE;
                this.chkTreeView.Nodes.Add(tRoleNode);
                this.chkTreeView.ShowLines = false;
                this.chkTreeView.CheckBoxes = true;
                index++;
                tBool = false;
                for (int j = 0; j < tListUSER.Count; j++)
                {
                    AGSDM_USER_ROLE tUserRole = tListUSER[j] as AGSDM_USER_ROLE;
                    if (tROLE.ROLE_ID.ToString() == tUserRole.ROLE_ID)
                    {
                        tBool = true;
                    }
                }
                if (tBool == true)
                {
                    tRoleNode.Checked = true;
                }
            }
        }

        /// <summary>
        /// 设置部门信息
        /// </summary>
        /// <param name="pDeptTree">部门信息树</param>
        public void SetDeptInfo(TreeView pDeptTree)
        {
            if (pDeptTree == null) return;
            //设置查询的时候不能选择
            if (this.m_OperateState != EnumOperateState.Query)
            {
                //foreach (TreeNode childNode in pDeptTree.Nodes)
                //{
                //    this.cmbTreeDepart.TreeView.Nodes.Add(childNode.Clone() as TreeNode);
                //    this.cmbTreeDepart.Tag = childNode.Tag;
                //}
            }
            else
            {
                this.cmbTreeDepart.Enabled = false;
            }
        }

        /// <summary>
        /// Depiction:获取角色信息添加到用户角色表中
        /// </summary>
        /// Writer:徐斌
        /// Create Date:2010-9-15
        private bool GetRoleID()
        {
            bool madeRoleID = true;
            AGSDM_USER_ROLE tUserRole = new AGSDM_USER_ROLE();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            tUserRole.USER_ID = this.m_User.USER_ID.ToString();

            for (int i = 0; i < this.chkTreeView.Nodes.Count; i++)
            {
                AGSDM_ROLE tRole = new AGSDM_ROLE();
                if (this.chkTreeView.Nodes[i].Checked==true)
                {
                    tRole = this.chkTreeView.Nodes[i].Tag as AGSDM_ROLE;
                    tUserRole.ROLE_ID = tRole.ROLE_ID.ToString();
                    tEntityHandler.AddEntity(tUserRole);
                }
            }
            if (tUserRole.ROLE_ID == null)
            {
                this.tabControl1.SelectedTab = this.tabPage2;
                madeRoleID = false;
            }
            return madeRoleID;
        }

        /// <summary>
        /// Depiction:更新用户部门表
        /// </summary>
        /// Writer:徐斌
        /// Create Date:2010-9-15
        private void UpdataUserOrg(AGSDM_ORG tORG)
        {
            //更新用户部门表
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");

            //删除旧的
            tEntityHandler.DeleteEntity<AGSDM_USER_ORG>("from AGSDM_USER_ORG where USER_ID ='" + this.m_User.USER_ID.ToString() + "'");
            //添加新的
            AGSDM_USER_ORG tUserORG = new AGSDM_USER_ORG();
            tUserORG.USER_ID = this.m_User.USER_ID.ToString();
            tUserORG.ORG_ID = tORG.ORG_ID.ToString();
            tEntityHandler.AddEntity(tUserORG);
        }

        /// <summary>
        /// Depiction:更新用户角色表
        /// </summary>
        /// Writer:徐斌
        /// Create Date:2010-9-15
        private void UpdataUserRole()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string roleSQL = "from AGSDM_USER_ROLE t where t.USER_ID='" + this.m_User.USER_ID.ToString() + "'";
            IList tListUserRole = tEntityHandler.GetEntities(roleSQL);
            string strUserID = null;
            string strRoleID = null;
            for (int i = 0; i < tListUserRole.Count; i++)
            {
                AGSDM_USER_ROLE tUserRole = tListUserRole[i] as AGSDM_USER_ROLE;
                strUserID = this.m_User.USER_ID.ToString();
                tEntityHandler.DeleteEntity(tUserRole);
            }
            for (int j = 0; j < this.chkTreeView.Nodes.Count; j++)
            {
                AGSDM_ROLE tRole = new AGSDM_ROLE();
                if (this.chkTreeView.Nodes[j].Checked == true)
                {
                    tRole = this.chkTreeView.Nodes[j].Tag as AGSDM_ROLE;
                    strRoleID = tRole.ROLE_ID.ToString();
                    AGSDM_USER_ROLE tUserRoleNew = new AGSDM_USER_ROLE();
                    tUserRoleNew.USER_ID = m_User.USER_ID.ToString();
                    tUserRoleNew.ROLE_ID = strRoleID;
                    tEntityHandler.AddEntity(tUserRoleNew);
                }
            }                
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitializeUserInfo()
        {
            this.txtName.Text = this.m_User.NAME_EN;
            this.txtFullName.Text = this.m_User.NAME_CN;
            this.txtDescription.Text = this.m_User.DESCRIPTION;
            this.txtPw.Text = this.m_User.PASSWORD;
            this.txtPw2.Text = this.m_User.PASSWORD;

            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_USER_ORG t where t.USER_ID='" + this.m_User.USER_ID.ToString() + "'";
            IList tListORG = tEntityHandler.GetEntities(strHQL);
            if (tListORG.Count == 0) return;
            for (int i = 0; i < tListORG.Count; i++)
            {
                AGSDM_USER_ORG tUserORG = tListORG[i] as AGSDM_USER_ORG;
                string strORG = tUserORG.ORG_ID.ToString();
                strHQL = "from AGSDM_ORG as t where t.ORG_ID=" + strORG;
                IList tList = tEntityHandler.GetEntities(strHQL);
                for (int j = 0; j < tList.Count; j++)
                {
                    AGSDM_ORG tORG = tList[j] as AGSDM_ORG;
                    cmbTreeDepart.Text = tORG.ORG_NAME;
                    cmbTreeDepart.Tag = tORG;
                }
            }
        }

        /// <summary>
        /// 禁用所有输入类型控件
        /// </summary>
        private void DisabledAllControl()
        {
            txtName.ReadOnly = true;
            txtFullName.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtPw.ReadOnly = true;
            txtPw2.ReadOnly = true;
            cmbTreeDepart.Enabled = false;
            chkTreeView.Enabled = false;
            btnOK.Enabled = false;
        }
    }
}