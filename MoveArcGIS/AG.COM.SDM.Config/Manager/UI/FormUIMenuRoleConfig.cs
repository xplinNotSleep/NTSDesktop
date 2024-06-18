using AG.COM.SDM.Config.MenuConfig;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    public partial class FormUIMenuRoleConfig : Form
    {
        #region 变量

        private AGSDM_ROLE m_Role = null;
        /// <summary>
        /// 当前角色，ShowDialog赋值
        /// </summary>
        public AGSDM_ROLE Role
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

        #endregion

        #region 初始化

        public FormUIMenuRoleConfig()
        {
            InitializeComponent();
        }

        private void FormUIDesignRoleConfig_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化菜单树
                RoleHelper.InitMenuConfigTree(tvwUIDesign, Framework.UIDesignFrom.Database);

                //初始化当前用户角色的权限
                ShowRoleRight();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// 初始化当前角色权限树
        /// </summary>
        private void ShowRoleRight()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            //查询当前角色的菜单权限
            IList<AGSDM_UI_ROLEFUNRLT> tUI_ROLEFUNRLTs = tEntityHandler.GetEntitiesLinq<AGSDM_UI_ROLEFUNRLT>(t => t.ROLEID == m_Role.ROLE_ID.ToString());
            //显示到菜单树
            SetNodeHasRight(tvwUIDesign.Nodes, tUI_ROLEFUNRLTs);
        }

        /// <summary>
        /// 把角色权限添加到树
        /// </summary>
        /// <param name="tNodes"></param>
        /// <param name="tUI_ROLEFUNRLTs"></param>
        private void SetNodeHasRight(TreeNodeCollection tNodes, IList<AGSDM_UI_ROLEFUNRLT> tUI_ROLEFUNRLTs)
        {
            foreach (TreeNode tNode in tNodes)
            {
                MenuConfigNode menuConfigNode = tNode.Tag as MenuConfigNode;

                if (tUI_ROLEFUNRLTs.Any(t => t.CONTROLNAME == menuConfigNode.GUID))
                {
                    tNode.Checked = true;
                    SetNodeHasRight(tNode.Nodes, tUI_ROLEFUNRLTs);
                }
            }
        }

        #endregion

        #region 保存

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            try
            {
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                //删除当前角色的所有记录
                string tableName = tEntityHandler.GetEntityTableName(typeof(AGSDM_UI_ROLEFUNRLT));
                tEntityHandler.ExecuteNonQuery("DELETE FROM " + tableName + " WHERE ROLEID = '" + m_Role.ROLE_ID + "' ");
                //从节点保存角色权限
                SaveRoleRight(tvwUIDesign.Nodes, tEntityHandler);

                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// 从节点保存角色权限
        /// </summary>
        /// <param name="tNodes"></param>
        /// <param name="tEntityHandler"></param>
        private void SaveRoleRight(TreeNodeCollection tNodes, EntityHandler tEntityHandler)
        {
            foreach (TreeNode tNode in tNodes)
            {
                if (tNode.Checked == true)
                {
                    MenuConfigNode menuConfigNode = tNode.Tag as MenuConfigNode;
                    AGSDM_UI_ROLEFUNRLT tUI_ROLEFUNRLT = new AGSDM_UI_ROLEFUNRLT();
                    //菜单的GUID作为菜单的唯一标识
                    tUI_ROLEFUNRLT.CONTROLNAME = menuConfigNode.GUID;
                    tUI_ROLEFUNRLT.ROLEID = m_Role.ROLE_ID.ToString();
                    tEntityHandler.AddEntity(tUI_ROLEFUNRLT);

                    SaveRoleRight(tNode.Nodes, tEntityHandler);
                }
            }
        }

        #endregion

        #region 其他

        private void tvwUIDesign_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //树级联选择
            ControlHelper.TreeViewRelateSelect(e, TreeViewRelateSelectDirection.All);
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
