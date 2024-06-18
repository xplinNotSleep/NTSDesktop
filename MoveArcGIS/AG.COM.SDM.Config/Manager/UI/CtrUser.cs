using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Depiction:Ȩ�޹���֮�û��ؼ�
    /// </summary>
    /// Rewriter:���
    /// Create Date:2010-9-9
    public partial class CtrUser : UserControl, IPrivilegeOperate
    {       
        private IList m_ListRoleInfo;
        private TreeView m_DeptTree;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CtrUser()
        {
            InitializeComponent();
        }

        //���
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormUserInfo tFormUserInfo = new FormUserInfo();
                tFormUserInfo.ShowInTaskbar = false;
                tFormUserInfo.OperateState = EnumOperateState.Add;

                //���ý�ɫ��Ϣ
                tFormUserInfo.SetRolesInfo(this.m_ListRoleInfo);

                //���ò�����Ϣ
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

        //�޸�
        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listUser.SelectedItems.Count > 0)
                {
                    //��ȡѡ����
                    ListViewItem tListViewItem = this.listUser.SelectedItems[0];
                    AGSDM_SYSTEM_USER tUserInfo = tListViewItem.Tag as AGSDM_SYSTEM_USER;

                    //ʵ�����û���Ϣ������
                    FormUserInfo tFormUserInfo = new FormUserInfo();
                    tFormUserInfo.ShowInTaskbar = false;
                    tFormUserInfo.OperateState = EnumOperateState.Modify;
                    tFormUserInfo.pUserLogger = tUserInfo;
                    //���ý�ɫ��Ϣ
                    tFormUserInfo.SetRolesInfo(this.m_ListRoleInfo);

                    //���ò�����Ϣ
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

        //ɾ��
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ȷ��Ҫɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.listUser.SelectedItems.Count > 0)
                    {
                        string indexUserID = null;
                        //�����ݿ����Ƴ�
                        ListViewItem tListViewItem = this.listUser.SelectedItems[0];
                        AGSDM_SYSTEM_USER tUser = tListViewItem.Tag as AGSDM_SYSTEM_USER;
                        indexUserID = tUser.USER_ID.ToString();
                        EntityHandler tEntityHandle = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                        tEntityHandle.DeleteEntity(tUser);
                        //ɾ���û����ű��еļ�¼                
                        string strHQL = "from AGSDM_USER_ORG t where t.USER_ID='" + indexUserID + "'";
                        IList tListUserOrg = tEntityHandle.GetEntities(strHQL);
                        for (int i = 0; i < tListUserOrg.Count; i++)
                        {
                            AGSDM_USER_ORG tUserOrg = tListUserOrg[i] as AGSDM_USER_ORG;
                            tEntityHandle.DeleteEntity(tUserOrg);
                        }
                        //ɾ���û���ɫ���еļ�¼
                        strHQL = "from AGSDM_USER_ROLE t where t.USER_ID='" + indexUserID + "'";
                        IList tListUserRole = tEntityHandle.GetEntities(strHQL);
                        for (int j = 0; j < tListUserRole.Count; j++)
                        {
                            AGSDM_USER_ROLE tUserRole = tListUserRole[j] as AGSDM_USER_ROLE;
                            tEntityHandle.DeleteEntity(tUserRole);
                        }
                        //�Ƴ�ѡ����
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

        //��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listUser.SelectedItems.Count > 0)
                {
                    //��ȡѡ����
                    ListViewItem tListViewItem = this.listUser.SelectedItems[0];
                    AGSDM_SYSTEM_USER tUserInfo = tListViewItem.Tag as AGSDM_SYSTEM_USER;

                    //ʵ�����û���Ϣ������
                    FormUserInfo tFormUserInfo = new FormUserInfo();
                    tFormUserInfo.ShowInTaskbar = false;
                    tFormUserInfo.OperateState = EnumOperateState.Query;
                    tFormUserInfo.pUserLogger = tUserInfo;
                    //���ý�ɫ��Ϣ
                    tFormUserInfo.SetRolesInfo(this.m_ListRoleInfo);
                    //���ò�����Ϣ
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
                //���ö���Ŀ���״̬
                SetButtonEnabled();           
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        #region IPrivilegeOperate ��Ա

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// Rewrite:2010-9-9
        public void Init()
        {
            #region ��ʼ���û���Ϣ
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

            //��ȡ������Ϣ
            this.m_DeptTree = GetTreeDeptInfo();

            //��ȡ��ɫ��Ϣ
            this.m_ListRoleInfo = GetListRoleInfo();
        }

        #endregion     

        /// <summary>
        /// ���ö���Ŀ���״̬
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

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ���в�������
        /// </summary>
        /// <returns></returns>
        /// Rewrite:2010-9-14
        private TreeView GetTreeDeptInfo()
        {
            //��ȡ������Ϣ��
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
        /// �ݹ�����ӽڵ�
        /// </summary>
        /// <param name="parentId">���ڵ�ID</param>
        /// <param name="parentNode">���ڵ�</param>
        private void AddChildNode(decimal parentId, TreeNode parentNode)
        {
            //�����ݿ��в�������
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
                //�ݹ���ú�����ӽڵ�
                AddChildNode(tORGChile.ORG_ID, tORGChildNode);
                parentNode.Nodes.Add(tORGChildNode);
            }
        }
        #endregion

        /// <summary>
        /// ��ȡ���н�ɫ����
        /// </summary>
        /// <returns>���ؽ�ɫ�����ַ���</returns>
        private IList GetListRoleInfo()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ROLE t";
            IList tListROLE = tEntityHandler.GetEntities(strHQL);
            return tListROLE;
        }
    }
}
