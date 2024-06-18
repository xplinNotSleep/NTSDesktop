using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ������Ϣ���� �û��ؼ�
    /// </summary>
    /// Rewrite:2010-9-13
    public partial class CtrDepartment : UserControl, IPrivilegeOperate
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CtrDepartment()
        {
            InitializeComponent();
        }

        #region IPrivilegeOperate ��Ա

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            //��ѯ���ݿ⣬��ȡ���ڵ�
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ORG t where t.PARENT_ORG_ID <= 0";
            IList tListORG = tEntityHandler.GetEntities(strHQL);
            if (tListORG.Count == 0) return;
            for (int i = 0; i < tListORG.Count; i++)
            {
                TreeNode tORGNode = new TreeNode();
                tORGNode.ContextMenuStrip = contextMenuStrip1;
                tORGNode.ImageIndex = 0;
                tORGNode.SelectedImageIndex = 0;
                AGSDM_ORG tORGpartment = tListORG[i] as AGSDM_ORG;
                tORGNode.Text = tORGpartment.ORG_NAME;
                tORGNode.Tag = tORGpartment;
                //�ݹ�����ӽڵ�
                this.AddChildNode(tORGpartment.ORG_ID, tORGNode);
                this.tvwDepartment.Nodes.Add(tORGNode);
                tORGNode.Expand();
            }
        }

        #endregion

        //��Ӹ�����λ
        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            try
            {
                FormDepartmentInfo tFormDepartmentInfo = new FormDepartmentInfo();
                tFormDepartmentInfo.ShowInTaskbar = false;
                tFormDepartmentInfo.OperateState = EnumOperateState.Add;
                tFormDepartmentInfo.ORGIDParent = 0;
                string NubID = (this.tvwDepartment.GetNodeCount(false) + 1).ToString();
                tFormDepartmentInfo.ORGCodeDefault = NubID.PadLeft(3, '0');
                if (tFormDepartmentInfo.ShowDialog() == DialogResult.OK)
                {
                    //��Ӹ��ڵ�
                    TreeNode tNode = new TreeNode();
                    tNode.Text = tFormDepartmentInfo.ORGCurrent.ORG_NAME;
                    tNode.Tag = tFormDepartmentInfo.ORGCurrent;
                    this.tvwDepartment.Nodes.Add(tNode);
                    this.tvwDepartment.SelectedNode = tNode;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        //����Ӽ���λ
        private void btnAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tNodeParent = this.tvwDepartment.SelectedNode;
                if (tNodeParent == null) return;

                FormDepartmentInfo tFormDepartmentInfo = new FormDepartmentInfo();
                tFormDepartmentInfo.ShowInTaskbar = false;
                tFormDepartmentInfo.OperateState = EnumOperateState.Add;
                AGSDM_ORG tORGParent = tNodeParent.Tag as AGSDM_ORG;
                string NumbNode = (tNodeParent.GetNodeCount(true) + 1).ToString();
                NumbNode = NumbNode.PadLeft(2, '0');

                string strCodeDefault = tORGParent.ORG_CODE + NumbNode;
                tFormDepartmentInfo.ORGCodeDefault = strCodeDefault;
                tFormDepartmentInfo.ORGIDParent = tORGParent.ORG_ID;
                if (tFormDepartmentInfo.ShowDialog() == DialogResult.OK)
                {
                    TreeNode tNode = new TreeNode();
                    tNode.Text = tFormDepartmentInfo.ORGCurrent.ORG_NAME;
                    tNode.Tag = tFormDepartmentInfo.ORGCurrent;

                    tNodeParent.Nodes.Add(tNode);
                    tNodeParent.Expand();  //չ�����ڵ�

                    this.tvwDepartment.SelectedNode = tNode;
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
                TreeNode tNode = this.tvwDepartment.SelectedNode;
                AGSDM_ORG tORG = tNode.Tag as AGSDM_ORG;
                FormDepartmentInfo tFormDepartmentInfo = new FormDepartmentInfo();
                tFormDepartmentInfo.ShowInTaskbar = false;
                tFormDepartmentInfo.OperateState = EnumOperateState.Modify;
                tFormDepartmentInfo.ORGCurrent = tORG;
                if (tFormDepartmentInfo.ShowDialog() == DialogResult.OK)
                {
                    tNode.Tag = tFormDepartmentInfo.ORGCurrent;
                    tNode.Text = tFormDepartmentInfo.ORGCurrent.ORG_NAME;
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
                    TreeNode tNode = this.tvwDepartment.SelectedNode;
                    if (tNode == null) return;

                    EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                    ///����ɾ���ڵ�����ݿ�����
                    DeleteNodeTag(tNode, tEntityHandler);
                 
                    //��ϵͳ��ɾ������
                    if (tNode.NextNode != null)
                    {
                        this.tvwDepartment.SelectedNode = tNode.NextNode;
                    }
                    else if (tNode.PrevNode != null)
                    {
                        this.tvwDepartment.SelectedNode = tNode.PrevNode;
                    }
                    else if (tNode.Parent != null)
                    {
                        this.tvwDepartment.SelectedNode = tNode.Parent;
                    }
                    else
                    {
                        this.tvwDepartment.SelectedNode = null;
                        //���ö������״̬
                        SetButtonEnable(null);
                    }
                    tNode.Remove();
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
                TreeNode tSelNode = this.tvwDepartment.SelectedNode;
                if (tSelNode != null)
                {
                    //���ò�����Ϣ
                    AGSDM_ORG tORG = tSelNode.Tag as AGSDM_ORG;
                    FormDepartmentInfo tFrm = new FormDepartmentInfo();
                    tFrm.ShowInTaskbar = false;
                    tFrm.OperateState = EnumOperateState.Query;
                    tFrm.ORGCurrent = tORG;
                    tFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void treeDepartment_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //���ö���Ŀ���״̬
                SetButtonEnable(e.Node);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        #region �Զ���˽�к���

        /// <summary>
        /// ɾ���ڵ�����ݿ�����
        /// </summary>
        /// <param name="tNode"></param>
        /// <param name="tEntityHandler"></param>
        private void DeleteNodeTag(TreeNode tNode, EntityHandler tEntityHandler)
        {
            if (tNode == null) return;

            ///��ɾ���ӽڵ�
            foreach (TreeNode childNode in tNode.Nodes)
            {
                DeleteNodeTag(childNode, tEntityHandler);
            }
            
            ///ɾ�����ݿ��¼
            if (tNode.Tag is AGSDM_ORG)
            {
                AGSDM_ORG tAGSDM_ORG = tNode.Tag as AGSDM_ORG;

                tEntityHandler.DeleteEntity<AGSDM_ORG>("from AGSDM_ORG where ORG_ID =" + tAGSDM_ORG.ORG_ID);
                tEntityHandler.DeleteEntity<AGSDM_USER_ORG>("from AGSDM_USER_ORG where ORG_ID =" + tAGSDM_ORG.ORG_ID);

                //�ҵ�������صĸ�λ
                IList<AGSDM_POSITION> tPositions = tEntityHandler.GetEntities<AGSDM_POSITION>("from AGSDM_POSITION where ORG_ID =" + tAGSDM_ORG.ORG_ID);
                foreach (AGSDM_POSITION tPosition in tPositions)
                {
                    //ɾ���û���λ����
                    tEntityHandler.DeleteEntity<AGSDM_USERPOSITIONRLT>("from AGSDM_USERPOSITIONRLT where POS_ID =" + tPosition.ID);
                    //ɾ����λ
                    tEntityHandler.DeleteEntity(tPosition);
                }
            }          
        }
        
        /// <summary>
        /// ���ö���Ŀ���״̬
        /// </summary>
        /// <param name="treeNode"></param>
        private void SetButtonEnable(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                this.btnAddChild.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnQuery.Enabled = true;
            }
            else
            {
                this.btnAddChild.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        /// <summary>
        /// �ݹ�����ӽڵ�
        /// </summary>
        /// <param name="parentId">���ڵ�ID</param>
        /// <param name="parentNode">���ڵ�</param>
        private void AddChildNode(decimal tORGIDParent, TreeNode parentNode)
        {
            AddUserChildNode(parentNode);
            //�ݹ�������ݿ��еĽڵ�
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_ORG as t where t.PARENT_ORG_ID=" + tORGIDParent;
            IList tListChildORG = tEntityHandler.GetEntities(strHQL);
            if (tListChildORG.Count == 0) return;
            for (int i = 0; i < tListChildORG.Count; i++)
            {
                TreeNode tORGChildNode = new TreeNode();
                tORGChildNode.ContextMenuStrip = contextMenuStrip1;
                tORGChildNode.ImageIndex = 0;
                tORGChildNode.SelectedImageIndex = 0;
                AGSDM_ORG tORGChile = tListChildORG[i] as AGSDM_ORG;
                tORGChildNode.Text = tORGChile.ORG_NAME;
                tORGChildNode.Tag = tORGChile;
                //tORGChildNode.Checked = true;
                //�ݹ�����ӽڵ�
                AddChildNode(tORGChile.ORG_ID, tORGChildNode);
                parentNode.Nodes.Add(tORGChildNode);
            }
        }

        /// <summary>
        /// ����û��ӽڵ�
        /// </summary>
        /// <param name="parentNode"></param>
        private void AddUserChildNode(TreeNode parentNode)
        {
            AGSDM_ORG org = parentNode.Tag as AGSDM_ORG;
            EntityHandler entityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_USER_ORG as t where t.ORG_ID=" + org.ORG_ID;
            List<AGSDM_USER_ORG> userOrgLst = entityHandler.GetEntities<AGSDM_USER_ORG>(strHQL).ToList();
            List<AGSDM_SYSTEM_USER> systemUserLst = entityHandler.GetEntitiesAll<AGSDM_SYSTEM_USER>().ToList();
            foreach (AGSDM_USER_ORG userOrg in userOrgLst)
            {
                AGSDM_SYSTEM_USER findUser = systemUserLst.Find(f => f.USER_ID.ToString() == userOrg.USER_ID);
                if (findUser != null)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.ImageIndex = 1;
                    childNode.SelectedImageIndex = 1;
                    childNode.Text = findUser.NAME_EN;
                    childNode.Tag = findUser;
                    parentNode.Nodes.Add(childNode);
                    parentNode.Expand();
                }
            }
            
        }
        #endregion

        private void btnUsers_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode treeNode = this.tvwDepartment.SelectedNode;

                if (treeNode == null) return;

                AGSDM_ORG org = treeNode.Tag as AGSDM_ORG;
                FormDeptUser form = new FormDeptUser(org);
                form.ShowDialog();

            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void tsmAddRoot_Click(object sender, EventArgs e)
        {
            try
            {
                FormDepartmentInfo tFormDepartmentInfo = new FormDepartmentInfo();
                tFormDepartmentInfo.ShowInTaskbar = false;
                tFormDepartmentInfo.OperateState = EnumOperateState.Add;
                tFormDepartmentInfo.ORGIDParent = 0;
                string NubID = (this.tvwDepartment.GetNodeCount(false) + 1).ToString();
                tFormDepartmentInfo.ORGCodeDefault = NubID.PadLeft(3, '0');
                if (tFormDepartmentInfo.ShowDialog() == DialogResult.OK)
                {
                    //��Ӹ��ڵ�
                    TreeNode tNode = new TreeNode();
                    tNode.ContextMenuStrip = contextMenuStrip1;
                    tNode.ImageIndex = 0;
                    tNode.SelectedImageIndex = 0;
                    tNode.Text = tFormDepartmentInfo.ORGCurrent.ORG_NAME;
                    tNode.Tag = tFormDepartmentInfo.ORGCurrent;
                    this.tvwDepartment.Nodes.Add(tNode);
                    this.tvwDepartment.SelectedNode = tNode;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void tsmAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tNodeParent = this.tvwDepartment.SelectedNode;
                if (tNodeParent == null) return;

                FormDepartmentInfo tFormDepartmentInfo = new FormDepartmentInfo();
                tFormDepartmentInfo.ShowInTaskbar = false;
                tFormDepartmentInfo.OperateState = EnumOperateState.Add;
                AGSDM_ORG tORGParent = tNodeParent.Tag as AGSDM_ORG;
                string NumbNode = (tNodeParent.GetNodeCount(true) + 1).ToString();
                NumbNode = NumbNode.PadLeft(2, '0');

                string strCodeDefault = tORGParent.ORG_CODE + NumbNode;
                tFormDepartmentInfo.ORGCodeDefault = strCodeDefault;
                tFormDepartmentInfo.ORGIDParent = tORGParent.ORG_ID;
                if (tFormDepartmentInfo.ShowDialog() == DialogResult.OK)
                {
                    TreeNode tNode = new TreeNode();
                    tNode.ContextMenuStrip = contextMenuStrip1;
                    tNode.ImageIndex = 0;
                    tNode.SelectedImageIndex = 0;
                    tNode.Text = tFormDepartmentInfo.ORGCurrent.ORG_NAME;
                    tNode.Tag = tFormDepartmentInfo.ORGCurrent;

                    tNodeParent.Nodes.Add(tNode);
                    tNodeParent.Expand();  //չ�����ڵ�

                    this.tvwDepartment.SelectedNode = tNode;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void tsmUser_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode treeNode = this.tvwDepartment.SelectedNode;

                if (treeNode == null) return;

                AGSDM_ORG org = treeNode.Tag as AGSDM_ORG;
                FormDeptUser form = new FormDeptUser(org);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    treeNode.Nodes.Clear();
                    List<AGSDM_SYSTEM_USER> systemUserLst = form.DeptUsers;
                    foreach (AGSDM_SYSTEM_USER systemUser in systemUserLst)
                    {
                        TreeNode childNote = new TreeNode();
                        childNote.ImageIndex = 1;
                        childNote.SelectedImageIndex = 1;
                        childNote.Text = systemUser.NAME_EN;
                        childNote.Tag = systemUser;
                        treeNode.Nodes.Add(childNote);
                        treeNode.Expand();
                    }
                   
                }

            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void tmsModify_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tNode = this.tvwDepartment.SelectedNode;
                AGSDM_ORG tORG = tNode.Tag as AGSDM_ORG;
                FormDepartmentInfo tFormDepartmentInfo = new FormDepartmentInfo();
                tFormDepartmentInfo.ShowInTaskbar = false;
                tFormDepartmentInfo.OperateState = EnumOperateState.Modify;
                tFormDepartmentInfo.ORGCurrent = tORG;
                if (tFormDepartmentInfo.ShowDialog() == DialogResult.OK)
                {
                    tNode.Tag = tFormDepartmentInfo.ORGCurrent;
                    tNode.Text = tFormDepartmentInfo.ORGCurrent.ORG_NAME;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ȷ��Ҫɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TreeNode tNode = this.tvwDepartment.SelectedNode;
                    if (tNode == null) return;

                    EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                    ///����ɾ���ڵ�����ݿ�����
                    DeleteNodeTag(tNode, tEntityHandler);

                    //��ϵͳ��ɾ������
                    if (tNode.NextNode != null)
                    {
                        this.tvwDepartment.SelectedNode = tNode.NextNode;
                    }
                    else if (tNode.PrevNode != null)
                    {
                        this.tvwDepartment.SelectedNode = tNode.PrevNode;
                    }
                    else if (tNode.Parent != null)
                    {
                        this.tvwDepartment.SelectedNode = tNode.Parent;
                    }
                    else
                    {
                        this.tvwDepartment.SelectedNode = null;
                        //���ö������״̬
                        SetButtonEnable(null);
                    }
                    tNode.Remove();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void tsmQuery_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tSelNode = this.tvwDepartment.SelectedNode;
                if (tSelNode != null)
                {
                    //���ò�����Ϣ
                    AGSDM_ORG tORG = tSelNode.Tag as AGSDM_ORG;
                    FormDepartmentInfo tFrm = new FormDepartmentInfo();
                    tFrm.ShowInTaskbar = false;
                    tFrm.OperateState = EnumOperateState.Query;
                    tFrm.ORGCurrent = tORG;
                    tFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

     
    }
}
