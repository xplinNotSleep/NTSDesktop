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
    /// �û���Ϣ������
    /// </summary>
    /// Rewrite:2010-9-9
    public partial class FormUserInfo : Form
    {
        private EnumOperateState m_OperateState;    //��������
        private AGSDM_SYSTEM_USER m_User;           //�û���Ϣ
        
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormUserInfo()
        {
            InitializeComponent();

            this.m_OperateState = EnumOperateState.Query;
            this.m_User = new AGSDM_SYSTEM_USER();
        }

        /// <summary>
        /// ��ȡ�����ò���״̬
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
        /// ��ȡ�������û���Ϣ
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
                    //��ʼ���û���Ϣ
                    InitializeUserInfo();

                    DisabledAllControl();
                }
                else if (this.m_OperateState == EnumOperateState.Modify)
                {
                    //�������뽹��
                    this.txtName.Focus();
                    //��ʼ���û���Ϣ
                    InitializeUserInfo();
                }
                else
                {
                    //�������뽹��
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
                ///������֤
                if (Valid() == false) return;

                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                if (this.txtPw.Text.Trim().Length == 0)
                {
                    MessageBox.Show("����ֻ�Կո���Ϊ����", "��ʾ��");
                    this.txtPw.Focus();
                    return;
                }
                else if (this.txtPw.Text.Equals(this.txtPw2.Text, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    MessageBox.Show("�ٴ�����������������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPw2.Focus();
                    return;
                }
                //��AGSDM_USER1����ֵ
                this.m_User.NAME_EN = this.txtName.Text;
                this.m_User.NAME_CN = this.txtFullName.Text;
                this.m_User.PASSWORD = this.txtPw.Text;
                this.m_User.DESCRIPTION = this.txtDescription.Text;
                this.m_User.NAME_CN = this.txtFullName.Text;
                this.m_User.CTEATE_TIME = DateTime.Now;
                ////��ȡAGSDM_ORG����
                //AGSDM_ORG tORG = this.cmbTreeDepart.Tag as AGSDM_ORG;

                AGSDM_SYSTEM_USER tUserSameName = tEntityHandler.GetEntity<AGSDM_SYSTEM_USER>("from AGSDM_SYSTEM_USER as t where t.NAME_EN=?", txtName.Text);

                //ֱ�����
                if (this.m_User.USER_ID == 0)
                {
                    //���ӵ�ʱ������û������                  
                    if (tUserSameName != null)
                    {
                        MessageBox.Show("���û����Ѿ�ע�ᣬ���������룡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtName.Text = "";
                        this.txtName.Focus();
                        return;
                    }

                    ////����ڲ�������ʱǰ���ҵ�����USER_ID��Ȼ���ٲ��룬��Ȼ����ʾ��?,?,?,?,....��֮�����ʾ
                    ////  [�춫�� 2019-8-21]
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

                    //��ӵ��û���
                    object obj = m_User;
                    tEntityHandler.AddEntity(obj);
                    //��ӵ��û���ɫ��
                    bool madeRoleID = GetRoleID();
                    if (!madeRoleID)//δ��ѡ��������ʱ������
                    {
                        tEntityHandler.DeleteEntity(obj);
                        this.m_User = new AGSDM_SYSTEM_USER();
                        return;
                    }
                }
                else
                {
                    //�޸�ʱ�û������
                    if (tUserSameName != null && tUserSameName.USER_ID != m_User.USER_ID)
                    {
                        MessageBox.Show("���û����Ѿ�ע�ᣬ���������룡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtName.Text = "";
                        this.txtName.Focus();
                        return;
                    }

                    //ʵ�ָ���
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
        /// Depiction:������ֵ�����ı�ʱ�ı�����tag����
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
        /// ������֤
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtName.Text, "����") &&
             ValidateData.StringLength(txtName.Text, 25, "����") &&
             ValidateData.NotNull(txtFullName.Text, "ȫ��") &&
 ValidateData.StringLength(txtFullName.Text, 25, "ȫ��") &&
  ValidateData.StringLength(txtDescription.Text, 50, "������Ϣ") &&
   ValidateData.NotNull(txtPw.Text, "����") &&
   ValidateData.StringLength(txtPw.Text, 25, "����");
        }

        /// <summary>
        /// ���ý�ɫ��Ϣ
        /// </summary>
        /// <param name="pListRoleInfo">��ɫ��Ϣ</param>
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
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="pDeptTree">������Ϣ��</param>
        public void SetDeptInfo(TreeView pDeptTree)
        {
            if (pDeptTree == null) return;
            //���ò�ѯ��ʱ����ѡ��
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
        /// Depiction:��ȡ��ɫ��Ϣ��ӵ��û���ɫ����
        /// </summary>
        /// Writer:���
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
        /// Depiction:�����û����ű�
        /// </summary>
        /// Writer:���
        /// Create Date:2010-9-15
        private void UpdataUserOrg(AGSDM_ORG tORG)
        {
            //�����û����ű�
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");

            //ɾ���ɵ�
            tEntityHandler.DeleteEntity<AGSDM_USER_ORG>("from AGSDM_USER_ORG where USER_ID ='" + this.m_User.USER_ID.ToString() + "'");
            //����µ�
            AGSDM_USER_ORG tUserORG = new AGSDM_USER_ORG();
            tUserORG.USER_ID = this.m_User.USER_ID.ToString();
            tUserORG.ORG_ID = tORG.ORG_ID.ToString();
            tEntityHandler.AddEntity(tUserORG);
        }

        /// <summary>
        /// Depiction:�����û���ɫ��
        /// </summary>
        /// Writer:���
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
        /// ��ʼ���û���Ϣ
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
        /// ���������������Ϳؼ�
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