using AG.COM.SDM.Config;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Model;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace AG.COM.SDM.Config
{
    public partial class FormUIDesignRoleConfig : Form
    {
        #region 变量

        /// <summary>
        /// 角色权限预览
        /// </summary>
        FormUIDesignPreview m_FormUIDesignPreviewRoleRight = null;
        /// <summary>
        /// 所有功能预览
        /// </summary>
        FormUIDesignPreview m_FormUIDesignPreviewAll = null;

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

        public FormUIDesignRoleConfig()
        {
            InitializeComponent();
        }

        private void FormUIDesignRoleConfig_Load(object sender, EventArgs e)
        {
            XmlDocument tUIDesignXml = null;
            List<ItemCommandInfo> tBindFuns = null;
            //从数据库加载功能菜单
            AG.COM.SDM.Config.UIDesignHelper.LoadFromDatabase(out tUIDesignXml, out tBindFuns, false);

            //初始化功能菜单树        
            tvwUIDesign.Nodes.Clear();
            AddControlNode(tUIDesignXml.DocumentElement.ChildNodes, tvwUIDesign.Nodes);
            tvwUIDesign.ExpandAll();
            //选中第一个节点
            if (tvwUIDesign.Nodes.Count > 0)
            {
                tvwUIDesign.SelectedNode = tvwUIDesign.Nodes[0];
            }

            //初始化当前用户角色的权限
            ShowRoleRight();

            //两个预览窗口的添加
            //m_FormUIDesignPreviewRoleRight = new FormUIDesignPreview();
            //m_FormUIDesignPreviewRoleRight.Text = "当前角色功能";
            //m_FormUIDesignPreviewRoleRight.Show(dpRoleRight, DockState.Document);

            //m_FormUIDesignPreviewAll = new FormUIDesignPreview();
            //m_FormUIDesignPreviewAll.Text = "所有功能";
            //m_FormUIDesignPreviewAll.Show(dpAll, DockState.Document);

            RefreshPreviewAll();
            RefreshPreviewRoleRight();
        }

        /// <summary>
        /// 把功能菜单添加到树
        /// </summary>
        /// <param name="tXmlNodes"></param>
        /// <param name="tNodes"></param>
        private void AddControlNode(XmlNodeList tXmlNodes, TreeNodeCollection tNodes)
        {
            foreach (XmlNode tNode in tXmlNodes)
            {
                if (tNode.Name == "Object")
                {
                    XmlAttribute tAttrType = tNode.Attributes["type"];
                    XmlAttribute tAttrName = tNode.Attributes["name"];
                    TreeNode tTreeNode = new TreeNode();
                    string TypeName = tAttrType.Value.Substring(0, tAttrType.Value.IndexOf(","));
                    TypeName = TypeName.Substring(TypeName.LastIndexOf(".") + 1, TypeName.Length - TypeName.LastIndexOf(".") - 1);
                    tTreeNode.Text = TypeName + " " + tAttrName.Value;
                    tTreeNode.Tag = tAttrName.Value;

                    XmlAttribute tAttrTextPropName = tNode.Attributes["textPropName"];
                    if (tAttrTextPropName != null && !string.IsNullOrEmpty(tAttrTextPropName.Value))
                    {
                        XmlNode tNodeText = tNode.SelectSingleNode("Property[@name= '" + tAttrTextPropName.Value + "'] ");
                        if (tNodeText != null && !string.IsNullOrEmpty(tNodeText.InnerText))
                        {
                            tTreeNode.Text = "（" + tNodeText.InnerText + "）" + tTreeNode.Text;
                        }
                    }

                    tNodes.Add(tTreeNode);

                    AddControlNode(tNode.ChildNodes, tTreeNode.Nodes);

                    if (tTreeNode.Nodes.Count > 0)
                    {
                        tTreeNode.ImageIndex = 0;
                        tTreeNode.SelectedImageIndex = 1;
                    }
                    else
                    {
                        tTreeNode.ImageIndex = 2;
                        tTreeNode.SelectedImageIndex = 2;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化当前角色权限树
        /// </summary>
        private void ShowRoleRight()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

            IList<AGSDM_UI_ROLEFUNRLT> tUI_ROLEFUNRLTs = tEntityHandler.GetEntities<AGSDM_UI_ROLEFUNRLT>
                ("from AGSDM_UI_ROLEFUNRLT t where t.ROLEID='" + m_Role.ROLE_ID + "'");

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
                string controlName = tNode.Tag as string;
                if (!string.IsNullOrEmpty(controlName))
                {
                    if (tUI_ROLEFUNRLTs.Any(t => t.CONTROLNAME == controlName))
                    {
                        tNode.Checked = true;
                        SetNodeHasRight(tNode.Nodes, tUI_ROLEFUNRLTs);
                    }
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
                tEntityHandler.ExecuteNonQuery("DELETE FROM " + tableName +
                    " WHERE ROLEID = '" + m_Role.ROLE_ID + "' ");

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
                    string controlName = tNode.Tag as string;
                    AGSDM_UI_ROLEFUNRLT tUI_ROLEFUNRLT = new AGSDM_UI_ROLEFUNRLT();
                    tUI_ROLEFUNRLT.CONTROLNAME = controlName;
                    tUI_ROLEFUNRLT.ROLEID = m_Role.ROLE_ID.ToString();
                    tEntityHandler.AddEntity(tUI_ROLEFUNRLT);

                    SaveRoleRight(tNode.Nodes, tEntityHandler);
                }
            }
        }

        #endregion

        #region 预览

        private void btnPreview_Click_1(object sender, EventArgs e)
        {
            try
            {
                RefreshPreviewRoleRight();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// 刷新角色权限预览
        /// </summary>
        private void RefreshPreviewRoleRight()
        {
            List<AGSDM_UI_ROLEFUNRLT> tUIRoleFunRlts = new List<AGSDM_UI_ROLEFUNRLT>();

            GetTreeRights(tvwUIDesign.Nodes, ref tUIRoleFunRlts);

            XmlDocument tUIDesignXml = null;
            List<ItemCommandInfo> tBindFuns = null;

            AG.COM.SDM.Config.UIDesignHelper.LoadFromDatabase(out tUIDesignXml, out tBindFuns, false);

            UIDesignHelper.FilterRoleFun(ref  tUIDesignXml, tUIRoleFunRlts);

            Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer = null;

            m_FormUIDesignPreviewRoleRight.Controls.Clear();

            //AG.COM.SDM.Framework.UIDesignLoader.UIDesignLoader tUIDesignLoader = new AG.COM.SDM.Framework.UIDesignLoader.UIDesignLoader();
            //tUIDesignLoader.AddControlToForm(tUIDesignXml, tBindFuns, m_FormUIDesignPreviewRoleRight, ref tRootContainer);
        }

        /// <summary>
        /// 刷新全部功能预览
        /// </summary>
        private void RefreshPreviewAll()
        {
            List<AGSDM_UI_ROLEFUNRLT> tUIRoleFunRlt = new List<AGSDM_UI_ROLEFUNRLT>();

            XmlDocument tUIDesignXml = null;
            List<ItemCommandInfo> tBindFuns = null;

            AG.COM.SDM.Config.UIDesignHelper.LoadFromDatabase(out tUIDesignXml, out tBindFuns, false);

            Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer = null;

            m_FormUIDesignPreviewRoleRight.Controls.Clear();

            //AG.COM.SDM.Framework.UIDesignLoader.UIDesignLoader tUIDesignLoader = new AG.COM.SDM.Framework.UIDesignLoader.UIDesignLoader();
            //tUIDesignLoader.AddControlToForm(tUIDesignXml, tBindFuns, m_FormUIDesignPreviewAll, ref tRootContainer);
        }

        /// <summary>
        /// 获取树节点的权限设置
        /// </summary>
        /// <param name="tNodes"></param>
        /// <param name="tUIRoleFunRlt"></param>
        private void GetTreeRights(TreeNodeCollection tNodes, ref List<AGSDM_UI_ROLEFUNRLT> tUIRoleFunRlt)
        {
            foreach (TreeNode tNode in tNodes)
            {
                if (tNode.Checked == true)
                {
                    string controlName = tNode.Tag as string;
                    AGSDM_UI_ROLEFUNRLT tUI_ROLEFUNRLT = new AGSDM_UI_ROLEFUNRLT();
                    tUI_ROLEFUNRLT.CONTROLNAME = controlName;
                    tUIRoleFunRlt.Add(tUI_ROLEFUNRLT);

                    GetTreeRights(tNode.Nodes, ref tUIRoleFunRlt);
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
