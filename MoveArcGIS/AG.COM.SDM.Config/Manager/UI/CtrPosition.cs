using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
//using AG.COM.SDM.ProjectManage.Manager;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 岗位信息控件
    /// </summary>
    public partial class CtrPosition : UserControl, IPrivilegeOperate
    {
        public CtrPosition()
        {
            InitializeComponent();
        }

        #region IPrivilegeOperate 成员

        public bool IsDirty
        {
            get { throw new NotImplementedException(); }
        }

        public void Init()
        {
            AbstractFactory factory = AbstractFactory.GetInstance();
            IUser userManage = factory.CreateUser();
            List<AGSDM_SYSTEM_USER> tListUser = userManage.GetUserList();
            IPost postManage = factory.CreatePost();
            List<AGSDM_POSITION> tListPosition = postManage.GetPostList();
            IDepartment dptManage = factory.CreateDepartment();
            List<AGSDM_ORG> tListORG = dptManage.GetDepartmentList();

            List<AGSDM_ORG> tBaseORG = GetDepartments(tListORG, 0);

            List<AGSDM_USERPOSITIONRLT> tListUserPos = postManage.GetUserPostions(); 
            
            if (tListORG.Count == 0) return;
            for (int i = 0; i < tBaseORG.Count; i++)
            {
                //创建部门节点
                TreeNode tORGNode = new TreeNode();
                tORGNode.ImageIndex = 0;
                tORGNode.SelectedImageIndex = 0;
                AGSDM_ORG tORGpartment = tBaseORG[i] as AGSDM_ORG;
                tORGNode.Text = tORGpartment.ORG_NAME;
                tORGNode.Tag = BuiltDptProp(true, true, tORGpartment);
                //建立部门的岗位节点
                AddDepartmentPostNode(tORGNode, tORGpartment.ORG_ID,tListPosition,tListUserPos,tListUser); 

                //递归添加子节点
                this.AddChildNode(tORGpartment.ORG_ID, tORGNode,tListORG,tListPosition,tListUserPos,tListUser);
                this.tvwPosition.Nodes.Add(tORGNode);
            }
        }

        public void DoWork()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 窗体事件

        private void CtrPosition_Load(object sender, EventArgs e)
        {
            Init();
        }

        //添加
        private void tsbAddPosition_Click(object sender, EventArgs e)
        {
            if (tvwPosition.SelectedNode == null)
                return;
            DepartmentProperty prop = tvwPosition.SelectedNode.Tag as DepartmentProperty;

            FormPositionInfo info = new FormPositionInfo();
            info.ShowInTaskbar = false;
            info.StartPosition = FormStartPosition.CenterParent; 
            info.OperateState = EnumOperateState.Add;
            AGSDM_POSITION pos = new AGSDM_POSITION();
            pos.ORG_ID = prop.DepartmentID;
            info.PostionEntity = pos;
            if (info.ShowDialog() == DialogResult.OK)
            {
                AGSDM_POSITION t = info.PostionEntity;
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                tEntityHandler.AddEntity(t);
                TreeNode node = new TreeNode();
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                node.Text = t.POS_NAME + "(未指定)";
                PositionProperty posProp = new PositionProperty();
                posProp.PositionID = t.ID;
                posProp.PositionName = t.POS_NAME;
                posProp.Postion = t;
                node.Tag = posProp;
                if (tvwPosition.SelectedNode != null)
                {
                    tvwPosition.SelectedNode.Nodes.Add(node);
                    tvwPosition.SelectedNode.Expand();
                }
            }
        }

        //编辑
        private void tsbEditPos_Click(object sender, EventArgs e)
        {
            if (tvwPosition.SelectedNode == null)
                return;
            PositionProperty prop = tvwPosition.SelectedNode.Tag as PositionProperty;
            if (prop == null) return;

            FormPositionInfo info = new FormPositionInfo();
            info.ShowInTaskbar = false;
            info.StartPosition = FormStartPosition.CenterParent;
            info.OperateState = EnumOperateState.Modify;
            info.PostionEntity = prop.Postion;
            if (info.ShowDialog() == DialogResult.OK)
            {
                AGSDM_POSITION t = info.PostionEntity;
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                tEntityHandler.UpdateEntity(t, t.ID);
                if (tvwPosition.SelectedNode != null)
                {
                    string userName = "(未指定)";
                    AGSDM_SYSTEM_USER user = GetUser(prop.UserID);
                    if (user != null)
                    {
                        userName = "(" + user.NAME_CN + ")";
                    }
                    tvwPosition.SelectedNode.Text = t.POS_NAME + userName;
                }
            }
        }

        //删除
        private void tsbDeletePos_Click(object sender, EventArgs e)
        {
            if (tvwPosition.SelectedNode == null)
                return;
            PositionProperty t = tvwPosition.SelectedNode.Tag as PositionProperty;
            if (t != null && t.Postion != null)
            {
                if (MessageBox.Show("您确定要删除" + t.PositionName + "这个岗位？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                    tEntityHandler.DeleteEntity(t.Postion);
                    tvwPosition.SelectedNode.Remove();
                }
            }
        }

        //关联用户
        private void tsbSetUser_Click(object sender, EventArgs e)
        {
            if (tvwPosition.SelectedNode == null)
                return;

            PositionProperty posProp = tvwPosition.SelectedNode.Tag as PositionProperty;
            if (posProp == null)
                return;
            FormUserSelect frmUse = new FormUserSelect();
            if (posProp.IsSetUser)
            {
                frmUse.OriginalUserID = posProp.UserID;
            }
            if (frmUse.ShowDialog() == DialogResult.OK)
            {
                if (frmUse.SelectedUser == null)
                {
                    //删除用户绑定
                    EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                    IList lstPosProjects = tEntityHandler.GetEntities("from AGSDM_USERPOSITIONRLT where POS_ID =" + posProp.PositionID);
                    for (int i = 0; i < lstPosProjects.Count; i++)
                    {
                        AGSDM_USERPOSITIONRLT userPosition = lstPosProjects[i] as AGSDM_USERPOSITIONRLT;
                        tEntityHandler.DeleteEntity(userPosition); 
                    }
                    tvwPosition.SelectedNode.Text = posProp.PositionName + "(未指定)";
                    posProp.UserID = -1;
                }
                else 
                {
                    posProp.UserID = frmUse.SelectedUser.USER_ID;
                    AGSDM_SYSTEM_USER user = GetUser(frmUse.SelectedUser.USER_ID);
                    if (user != null)
                    {
                        tvwPosition.SelectedNode.Text = posProp.PositionName + "(" + user.NAME_CN + ")"; 
                    }
                    UpdateUsePosition(posProp.PositionID, posProp.UserID, SystemInfo.UserName, DateTime.Now);   
                }
            }
        }

        //选择节点
        private void tvwPosition_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            BaseProperty property = e.Node.Tag as BaseProperty;
            if (property != null)
            {
                switch (property.NodeType)
                {
                    case NodeType.Department:
                        tsbAddPosition.Enabled = true;
                        tsbDeletePos.Enabled = false;
                        tsbEditPos.Enabled = false;
                        tsbSetUser.Enabled = false;
                        break;
                    case NodeType.Position:
                        tsbAddPosition.Enabled = false;
                        tsbDeletePos.Enabled = true;
                        tsbEditPos.Enabled = true;
                        tsbSetUser.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //权限设置
        private void toolStripSetProject_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tNode = tvwPosition.SelectedNode;
                if (tNode == null) return;

                if (tNode.Tag is DepartmentProperty)
                {
                    DepartmentProperty tDepartmentProperty = tNode.Tag as DepartmentProperty;

                    //FormProjectManageORG tFormProjectManageORG = new FormProjectManageORG();
                    //tFormProjectManageORG.CurrentProjectManagerUseType = AG.COM.SDM.ProjectManage.Manager.ProjectManagerUseType.ORG;
                    //tFormProjectManageORG.CurrentOwnerID = tDepartmentProperty.DepartmentID;
                    //tFormProjectManageORG.ShowDialog();
                }
                else if (tNode.Tag is PositionProperty)
                {
                    PositionProperty tPositionProperty = tNode.Tag as PositionProperty;

                    //FormProjectManageORG tFormProjectManageORG = new FormProjectManageORG();
                    //tFormProjectManageORG.CurrentProjectManagerUseType = AG.COM.SDM.ProjectManage.Manager.ProjectManagerUseType.Position;
                    //tFormProjectManageORG.CurrentOwnerID = tPositionProperty.Postion.ID;
                    //tFormProjectManageORG.CurrentORGID = tPositionProperty.Postion.ORG_ID;
                    //tFormProjectManageORG.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }
       
        #endregion

        #region 自定义函数

        /// <summary>
        /// 获取根部门
        /// </summary>
        /// <param name="lstORG"></param>
        /// <param name="parentORGID"></param>
        /// <returns></returns>
        private List<AGSDM_ORG> GetDepartments(List<AGSDM_ORG> lstORG,decimal parentOrgID)
        {
            List<AGSDM_ORG> lstResult = new List<AGSDM_ORG>();
            for (int i = 0; i < lstORG.Count; i++)
            {
                AGSDM_ORG org = lstORG[i];
                if (org.PARENT_ORG_ID == parentOrgID)
                {
                    lstResult.Add(org);
                }
            }
            return lstResult;
        }

        /// <summary>
        /// 建立部门属性
        /// </summary>
        /// <param name="canBrowse"></param>
        /// <param name="canAuthorize"></param>
        /// <param name="orgEntity"></param>
        /// <returns></returns>
        private DepartmentProperty BuiltDptProp(bool canBrowse, bool canAuthorize,AGSDM_ORG orgEntity)
        {
            DepartmentProperty dptProp = new DepartmentProperty();
            dptProp.DepartmentName = orgEntity.ORG_NAME;
            dptProp.DepartmentID = orgEntity.ORG_ID;
            dptProp.CanAuthorize = canAuthorize;
            dptProp.CanBrowse = canBrowse;
            return dptProp;
        }

        /// <summary>
        /// 添加部门下属的岗位信息
        /// </summary>
        /// <param name="dptNode"></param>
        /// <param name="dptID"></param>
        private void AddDepartmentPostNode(TreeNode dptNode, decimal dptID, List<AGSDM_POSITION> pListPost, List<AGSDM_USERPOSITIONRLT> pListUserPos, List<AGSDM_SYSTEM_USER> pListUser)
        {
            List<AGSDM_POSITION> lstPositions = GetPosition(dptID, pListPost);
            for (int i = 0; i < lstPositions.Count; i++)
            {
                AGSDM_POSITION t = lstPositions[i] as AGSDM_POSITION;
                PositionProperty prop = new PositionProperty();
                prop.PositionName = t.POS_NAME;
                prop.PositionID = t.ID;
                prop.Postion = t;
                AGSDM_SYSTEM_USER user = GetUser(t.ID, pListUserPos, pListUser);
                TreeNode node = new TreeNode();
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                if (user == null)
                {
                    node.Text = t.POS_NAME + "(未设定" + ")";
                }
                else
                {
                    prop.UserID = user.USER_ID;
                    node.Text = t.POS_NAME + "(" + user.NAME_CN + ")";
                }
                node.Tag = prop;
                dptNode.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 递归添加子节点
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <param name="parentNode">父节点</param>
        private void AddChildNode(decimal parentId, TreeNode parentNode,List<AGSDM_ORG> lstORG, List<AGSDM_POSITION> pListPost, List<AGSDM_USERPOSITIONRLT> pListUserPos, List<AGSDM_SYSTEM_USER> pListUser)
        {
            //递归查找数据库中的节点
            IList tListChildORG = GetDepartments(lstORG,parentId);
            if (tListChildORG.Count == 0) return;
            for (int i = 0; i < tListChildORG.Count; i++)
            {
                TreeNode tORGChildNode = new TreeNode();
                AGSDM_ORG tORGChile = tListChildORG[i] as AGSDM_ORG;
                tORGChildNode.Text = tORGChile.ORG_NAME;
                tORGChildNode.ImageIndex = 0;
                tORGChildNode.SelectedImageIndex = 0;
             
                tORGChildNode.Tag = BuiltDptProp(true, true, tORGChile);
                //建立部门的岗位节点
                AddDepartmentPostNode(tORGChildNode, tORGChile.ORG_ID, pListPost, pListUserPos, pListUser);
                //递归添加子节点
                AddChildNode(tORGChile.ORG_ID, tORGChildNode, lstORG, pListPost, pListUserPos, pListUser);
                parentNode.Nodes.Add(tORGChildNode);
            }
        }

        /// <summary>
        /// 获取部门下的所有岗位
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="pListPosition"></param>
        /// <returns></returns>
        private List<AGSDM_POSITION> GetPosition(decimal orgID, IList pListPosition)
        {
            List<AGSDM_POSITION> lstResult = new List<AGSDM_POSITION>();
            for (int i = 0; i < pListPosition.Count; i++)
            {
                AGSDM_POSITION t = pListPosition[i] as AGSDM_POSITION;
                if (t.ORG_ID == orgID)
                {
                    lstResult.Add(t);
                }
            }
            return lstResult;
        }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="postID"></param>
        /// <param name="pListUserPos"></param>
        /// <param name="pListUser"></param>
        /// <returns></returns>
        private AGSDM_SYSTEM_USER GetUser(decimal postID, IList pListUserPos, List<AGSDM_SYSTEM_USER> pListUser)
        {
            decimal userID = -1;
            for (int i = 0; i < pListUserPos.Count; i++)
            {
                AGSDM_USERPOSITIONRLT t = pListUserPos[i] as AGSDM_USERPOSITIONRLT;
                if (t.POS_ID == postID)
                {
                    userID = Convert.ToDecimal(t.USER_ID);
                    break;
                }
            }
            if (userID < 0)
                return null;

            for (int i = 0; i < pListUser.Count; i++)
            {
                AGSDM_SYSTEM_USER userEntity = pListUser[i] as AGSDM_SYSTEM_USER;
                if (userEntity.USER_ID == userID)
                    return userEntity;
            }
            return null;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        private AGSDM_SYSTEM_USER GetUser(decimal userID)
        {
            AbstractFactory factory = AbstractFactory.GetInstance();
            IUser userManage = factory.CreateUser();
            return userManage.GetUser(userID);
        }

        /// <summary>
        /// 获取某个岗位工程的编号列表
        /// </summary>
        /// <param name="orgID">岗位编号</param>
        /// <returns></returns>
        private decimal GetPositionSelectedProjectID(decimal orgID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            AGSDM_ORG_PROJECT posProject = tEntityHandler.GetEntity<AGSDM_ORG_PROJECT>("from AGSDM_ORG_PROJECT where ORG_ID =" + orgID);
            if(posProject != null)
                return posProject.PROJECT_ID;
            else 
                return -1;
        }

        /// <summary>
        /// 更新用户岗位信息
        /// </summary>
        private void UpdateUsePosition(decimal posID,decimal userID,string assignUser,DateTime dtAssignTime)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstPosProjects = tEntityHandler.GetEntities("from AGSDM_USERPOSITIONRLT where POS_ID =" + posID);
            AGSDM_USERPOSITIONRLT pos = null;
            if (lstPosProjects.Count == 1)
            {
                pos = lstPosProjects[0] as AGSDM_USERPOSITIONRLT;
                pos.USER_ID = userID;
                pos.ASSIGN_USER = assignUser;
                pos.ASSIGN_TIME = dtAssignTime;
                tEntityHandler.UpdateEntity(pos, pos.ID); 
            }
            else
            {
                pos = new AGSDM_USERPOSITIONRLT();
                pos.POS_ID = posID;
                pos.USER_ID = userID;
                pos.ASSIGN_TIME = dtAssignTime;
                pos.ASSIGN_USER = assignUser;
                tEntityHandler.AddEntity(pos);
            }
        }

        /// <summary>
        /// 更新岗位工程信息
        /// </summary>
        /// <param name="posID"></param>
        /// <param name="projectID"></param>
        private void UpdateOrgProject(decimal posID, decimal projectID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            AGSDM_ORG_PROJECT posProject = tEntityHandler.GetEntity<AGSDM_ORG_PROJECT>("from AGSDM_ORG_PROJECT where ORG_ID =" + posID);
            if (posProject == null)
            {
                posProject = new AGSDM_ORG_PROJECT();
                posProject.ORG_ID = posID;
                posProject.PROJECT_ID = projectID;
                tEntityHandler.AddEntity(posProject);
            }
            else
            {
                posProject.PROJECT_ID = projectID;
                tEntityHandler.UpdateEntity(posProject,posProject.ID);
            }
        }

        #endregion
    }


    public enum NodeType
    {
        /// <summary>
        /// 部门
        /// </summary>
        Department,
        /// <summary>
        /// 岗位
        /// </summary>
        Position,
    }

    public class BaseProperty
    {
        public NodeType NodeType { get; protected set; }

        /// <summary>
        /// 能够查看
        /// </summary>
        public bool CanBrowse { get; set; }

        /// <summary>
        /// 可否授权权限
        /// </summary>
        public bool CanAuthorize { get; set; }

        public BaseProperty()
        {
            CanBrowse = false;
            CanAuthorize = false;
        }
    }

    /// <summary>
    /// 部门节点属性
    /// </summary>
    public class DepartmentProperty : BaseProperty
    {
        public DepartmentProperty()
        {
            base.NodeType = NodeType.Department; 
        }

        public decimal DepartmentID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
       

        /// <summary>
        /// 部门实体
        /// </summary>
        public AGSDM_ORG DepartmentEntity { get; set; }
    
    }

    /// <summary>
    /// 岗位节点信息
    /// </summary>
    public class PositionProperty : BaseProperty 
    {
        private decimal m_UserID;

        /// <summary>
        /// 是否已经设置关联用户
        /// </summary>
        public bool IsSetUser { get; private set; }

        /// <summary>
        /// 岗位编号
        /// </summary>
        public decimal PositionID { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 岗位信息
        /// </summary>
        public AGSDM_POSITION Postion { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public decimal UserID 
        {
            get
            {
                return m_UserID;
            }
            set 
            {
                if (value >= 0)
                {
                    IsSetUser = true;
                }
                else
                {
                    IsSetUser = false;
                }
                m_UserID = value;
            } 
        }

        public PositionProperty()
        {
            base.NodeType = NodeType.Position;
            IsSetUser = false;
        }
    }

}
