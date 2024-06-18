using AG.COM.SDM.Config;
using AG.COM.SDM.Config.MenuConfig;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Manager
{
    public partial class FormMenuConfig : DockContent
    {
        private string strAssemblyPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// TreeView拖动节点辅助点
        /// </summary>
        private Point m_DrapPosition = new Point(0, 0);

        /// <summary>
        /// 当前菜单从哪加载
        /// </summary>
        private UIDesignFrom m_UIDesignFrom = UIDesignFrom.Database;

        public FormMenuConfig()
        {
            InitializeComponent();
        }

        private void FormMenuConfig_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化数据库参数配置
                DatabaseHelper.RefreshAGSDMSessionFactory();
                //获取默认的UIDesign加载来源
                m_UIDesignFrom = CommonVariables.UIDesignLoadFrom;

                //添加所有插件类到下拉框
                BindPlugin();
                //初始化菜单树
                InitTree(m_UIDesignFrom);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 初始化菜单树
        /// </summary>
        /// <param name="loadFrom"></param>
        private void InitTree(UIDesignFrom loadFrom)
        {
            //初始化菜单树
            RoleHelper.InitMenuConfigTree(tvwMenu, loadFrom);

            m_UIDesignFrom = loadFrom;
            RefreshConfigSource();
        }

        /// <summary>
        /// 加载插件类到下拉框
        /// </summary>
        private void BindPlugin()
        {
            DirectoryInfo tDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] Files = tDirectory.GetFiles("*.dll");
            List<string> errList = new List<string>();
            List<string> successList = new List<string>();
            foreach (FileInfo afile in Files)
            {
                try //屏蔽不能加载的Dll
                {
                    Assembly ass = null;
                    try
                    {
                        ass = Assembly.LoadFrom(afile.FullName);

                    }
                    catch (Exception e)
                    {
                        errList.Add(afile.FullName);
                        Console.WriteLine(e);
                    }
                    if (ass == null) continue;
                    Type[] types = ass.GetTypes();
                    successList.Add(afile.FullName);
                    foreach (Type type in types)
                    {
                        try
                        {
                            if ((type.IsClass == false) || (type.IsPublic == false)) continue;
                            //如果为上下文接口则不载入
                            if (type.GetInterface("IContextMenu") != null) continue;
                            //判断该类是否继承ICommand或IPlugin接口
                            if ((type.GetInterface("ICommand") != null) || (type.GetInterface("IPlugin")) != null)
                            {
                                this.cmbAssembly.Items.Add(afile.Name);
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            errList.Add(afile.FullName);
                            Console.WriteLine(e);
                        }
                    }
                }
                catch (Exception exception)
                {
                    errList.Add(afile.FullName);
                }
            }
        }

        private void cmbAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbType.Items.Clear();

                if (cmbAssembly.Text != "")
                {
                    //利用反射将Dll中的方法、类添加到cmbPlugAssembly列表中
                    string strfileName = strAssemblyPath + "\\" + cmbAssembly.Text;
                    Assembly ass = Assembly.LoadFrom(strfileName);
                    Type[] types = ass.GetTypes();
                    List<string> tTypeNames = new List<string>();
                    foreach (Type type in types)
                    {
                        if ((type.IsClass == false) || (type.IsPublic == false)) continue;
                        //如果为上下文接口则不载入
                        if (type.GetInterface("IContextMenu") != null) continue;
                        //判断该类是否继承ICommand或IPlugin接口
                        if ((type.GetInterface("ICommand") != null) || (type.GetInterface("IPlugin")) != null)
                        {
                            tTypeNames.Add(type.FullName);
                        }
                    }
                    tTypeNames.Sort();
                    cmbType.Items.AddRange(tTypeNames.ToArray());
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void tvwMenu_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    //右键选择节点
                    tvwMenu.SelectedNode = tvwMenu.GetNodeAt(e.X, e.Y);

                    MenuType menuType = GetSelectMenuType();

                    //上下文菜单设置
                    SetContextMenu(menuType);

                    contextMenuStrip1.Show(tvwMenu, new Point(e.X, e.Y));
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 获取当前选择节点的菜单类型
        /// </summary>
        /// <returns></returns>
        private MenuType GetSelectMenuType()
        {
            if (tvwMenu.SelectedNode == null) return MenuType.None;

            MenuType menuType;
            TreeNode node = tvwMenu.SelectedNode;

            if (node == null)
            {
                menuType = MenuType.None;
            }
            else
            {
                MenuConfigNode itemCommandInfo = node.Tag as MenuConfigNode;
                menuType = itemCommandInfo.MenuType;
            }

            return menuType;
        }

        /// <summary>
        /// 设置树右键菜单的菜单项的可用状态
        /// </summary>
        /// <param name="menuType"></param>
        private void SetContextMenu(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.None:
                    SetContextMenuItemEnabled(false, false, false, false, false, false, false, false, false, false, false);
                    break;
                case MenuType.Toolbar:
                    SetContextMenuItemEnabled(true, true, true, true, true, false, true, true, false, false, false);
                    break;
                case MenuType.Menu:
                    SetContextMenuItemEnabled(false, false, false, false, false, false, false, false, false, true, false);
                    break;
                case MenuType.RibbonPage:
                    SetContextMenuItemEnabled(true, true, false, true, false, false, false, false, false, false, true);
                    break;
                case MenuType.RibbonPageGroup:
                    SetContextMenuItemEnabled(true, true, true, true, true, false, false, true, false, false, false);
                    break;
                //case MenuType.Item:                         
                //    SetContextMenuItemEnabled(true, true, true, true, false, false, false, false, false, false, false);                 
                //    break;
                //case MenuType.Separator:
                //    CMnuMoveUp.Enabled = true;
                //    CMnuMoveDown.Enabled = true;
                //    btnAddItem.Enabled = false;
                //    CMnuDeleteItem.Enabled = true;
                //    btnAddComboBox.Enabled = false;
                //    btnAddText.Enabled = false;
                //    btnAddSeparator.Enabled = false;
                //    btnAddCustom.Enabled = false;
                //    btnAddToolbar.Enabled = false;
                //    break;
                case MenuType.Item:
                case MenuType.CustomControl:
                case MenuType.ComboBox:
                case MenuType.ComboBoxTreeView:
                    SetContextMenuItemEnabled(true, true, false, true, false, false, false, false, false, false, false);
                    break;
                default:
                    SetContextMenuItemEnabled(false, false, false, false, false, false, false, false, false, false, false);
                    break;
            }
        }

        /// <summary>
        /// 设置下拉菜单的enabled
        /// </summary>
        /// <param name="bolCMnuMoveUp"></param>
        /// <param name="bolCMnuMoveDown"></param>
        /// <param name="bolbtnAddItem"></param>
        /// <param name="bolCMnuDeleteItem"></param>
        /// <param name="bolbtnAddComboBox"></param>
        /// <param name="bolbtnAddText"></param>
        /// <param name="bolbtnAddSeparator"></param>
        /// <param name="bolbtnAddCustom"></param>
        /// <param name="bolbtnAddToolbar"></param>
        /// <param name="bolbtnAddPage"></param>
        /// <param name="bolbtnAddGroup"></param>
        private void SetContextMenuItemEnabled(bool bolCMnuMoveUp, bool bolCMnuMoveDown, bool bolbtnAddItem, bool bolCMnuDeleteItem, bool bolbtnAddComboBox, bool bolbtnAddText, bool bolbtnAddSeparator, bool bolbtnAddCustom, bool bolbtnAddToolbar, bool bolbtnAddPage, bool bolbtnAddGroup)
        {
            CMnuMoveUp.Enabled = bolCMnuMoveUp;
            CMnuMoveDown.Enabled = bolCMnuMoveDown;
            btnAddItem.Enabled = bolbtnAddItem;
            CMnuDeleteItem.Enabled = bolCMnuDeleteItem;
            btnAddComboBox.Enabled = bolbtnAddComboBox;
            btnAddText.Enabled = bolbtnAddText;
            btnAddSeparator.Enabled = bolbtnAddSeparator;
            btnAddComboBoxTreeView.Enabled = bolbtnAddCustom;
            btnAddToolbar.Enabled = bolbtnAddToolbar;
            btnAddPage.Enabled = bolbtnAddPage;
            btnAddGroup.Enabled = bolbtnAddGroup;
        }

        private void tvwMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (tvwMenu.SelectedNode == null) return;
                //获取节点菜单类型
                MenuType menuType = GetSelectMenuType();
                //设置内容面板的控件状态
                SetContentPanel(menuType);

                MenuConfigNode menuConfigNode = tvwMenu.SelectedNode.Tag as MenuConfigNode;
                //加载属性到界面
                txtCaption.Text = menuConfigNode.Caption;

                if (!string.IsNullOrEmpty(menuConfigNode.PlugAssembly))
                    cmbAssembly.Text = menuConfigNode.PlugAssembly;
                else
                    this.cmbAssembly.SelectedItem = null;

                cmbType.Text = menuConfigNode.PlugType;

                chbNewGroup.Checked = menuConfigNode.ExtValue1.EqualIgnoreCase("true") ? true : false;
                rdbIconSizeBig.Checked = menuConfigNode.ExtValue2.EqualIgnoreCase("true") ? true : false;
                rdbIconSizeSmall.Checked = !rdbIconSizeBig.Checked;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 设置内容面板的控件状态
        /// </summary>
        /// <param name="menuType"></param>
        private void SetContentPanel(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.None:
                case MenuType.Menu:
                    txtCaption.Enabled = false;
                    cmbAssembly.Enabled = false;
                    cmbType.Enabled = false;
                    btnApply.Enabled = false;
                    grpControlAttr.Enabled = false;
                    break;
                case MenuType.Item:
                case MenuType.ComboBox:
                case MenuType.ComboBoxTreeView:
                case MenuType.CustomControl:
                    txtCaption.Enabled = true;
                    cmbAssembly.Enabled = true;
                    cmbType.Enabled = true;
                    btnApply.Enabled = true;
                    grpControlAttr.Enabled = true;
                    break;
                case MenuType.Text:
                case MenuType.Separator:
                case MenuType.Toolbar:
                case MenuType.RibbonPage:
                case MenuType.RibbonPageGroup:
                    txtCaption.Enabled = true;
                    cmbAssembly.Enabled = false;
                    cmbType.Enabled = false;
                    btnApply.Enabled = true;
                    grpControlAttr.Enabled = false;
                    break;
                default:
                    txtCaption.Enabled = false;
                    cmbAssembly.Enabled = false;
                    cmbType.Enabled = false;
                    btnApply.Enabled = false;
                    grpControlAttr.Enabled = false;
                    break;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.Item);
        }

        private void btnAddComboBox_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.ComboBox);
        }

        private void btnAddText_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.Text);
        }

        private void btnAddSeparator_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.Separator);
        }

        private void btnAddCustom_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.ComboBoxTreeView);
        }

        private void btnAddToolbar_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.Toolbar);
        }

        private void btnAddPage_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.RibbonPage);
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            AddNodeEvent(MenuType.RibbonPageGroup);
        }

        /// <summary>
        /// 添加节点的事件
        /// </summary>
        /// <param name="menuType"></param>
        private void AddNodeEvent(MenuType menuType)
        {
            try
            {
                //添加page
                AddNode(menuType);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="menuType"></param>
        private void AddNode(MenuType menuType)
        {
            TreeNode parentNode = tvwMenu.SelectedNode;
            //新节点的名称
            string title = "菜单项";

            //创建节点
            TreeNode node = new TreeNode();
            node.Text = title;
            node.ImageIndex = RoleHelper.GetImageIdxByType(menuType);
            node.SelectedImageIndex = node.ImageIndex;

            //实例化节点插件信息
            MenuConfigNode itemCommandInfo = new MenuConfigNode();
            itemCommandInfo.GUID = Guid.NewGuid().ToString();
            itemCommandInfo.Caption = title;
            itemCommandInfo.MenuType = menuType;
            if (parentNode != null)
            {
                itemCommandInfo.ParentCode = ((MenuConfigNode)parentNode.Tag).GUID;
                itemCommandInfo.Sort = parentNode.Nodes.Count;
            }
            else
            {
                itemCommandInfo.ParentCode = "0";
                itemCommandInfo.Sort = tvwMenu.Nodes.Count;
            }

            node.Tag = itemCommandInfo;

            if (parentNode != null)
            {
                parentNode.Nodes.Add(node);
            }
            else
            {
                tvwMenu.Nodes.Add(node);
            }

            //设置当前节点为选择节点
            tvwMenu.SelectedNode = node;
        }

        private void CMnuDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvwMenu.SelectedNode == null) return;

                TreeNode parentNode = tvwMenu.SelectedNode.Parent;

                //删除当前选择节点
                tvwMenu.SelectedNode.Remove();

                //递归更新节点信息
                //UpdateNodePlugInfo(parentNode);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                //保存当前节点信息
                TreeNode node = tvwMenu.SelectedNode;

                //设置插件配置信息
                MenuConfigNode plugInfoConfig = node.Tag as MenuConfigNode;
                plugInfoConfig.Caption = txtCaption.Text;
                plugInfoConfig.PlugAssembly = cmbAssembly.Text;
                plugInfoConfig.PlugType = cmbType.Text;
                plugInfoConfig.ExtValue1 = chbNewGroup.Checked.ToString();
                plugInfoConfig.ExtValue2 = rdbIconSizeBig.Checked.ToString();

                node.Text = txtCaption.Text;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void btnSavetoLocal_Click(object sender, EventArgs e)
        {
            try
            {
                m_UIDesignFrom = UIDesignFrom.XmlFile;

                SaveMenuConfig(UIDesignFrom.XmlFile);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void btnSavetoLocalDB_Click(object sender, EventArgs e)
        {
            try
            {
                m_UIDesignFrom = UIDesignFrom.Database;

                SaveMenuConfig(UIDesignFrom.Database);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
                ExceptionLog.LogError(ex.Message,ex);
            }
        }

        /// <summary>
        /// 保存菜单配置
        /// </summary>
        /// <param name="loadFrom"></param>
        private void SaveMenuConfig(UIDesignFrom loadFrom)
        {
            List<AGSDM_UIMENU> menus = new List<AGSDM_UIMENU>();
            List<AGSDM_UIMENU> menuTems = new List<AGSDM_UIMENU>();
            #region 先把treeview的菜单配置转为实体
            //分别获取菜单和工具条的子节点
            //MenuConfigToEntity(tvwMenu.Nodes[0].Nodes, menuTems);
            //menus.AddRange(menuTems);
            //menuTems = new List<AGSDM_UIMENU>();

            //MenuConfigToEntity(tvwMenu.Nodes[1].Nodes, menuTems);
            //menus.AddRange(menuTems);
            #endregion

            MenuConfigToEntity(tvwMenu.Nodes, menus);
            menus.AddRange(menuTems);

            MenuLoadHelper.SaveMenuConfigEntity(loadFrom, menus);

            m_UIDesignFrom = loadFrom;
            RefreshConfigSource();

            //把UIDesign加载来源设置保存到系统配置文件
            ResourceHelper tResourceHelper = new ResourceHelper
                (CommonVariables.ConfigFile, CommonConstString.STR_TempPath);
            tResourceHelper.SetObject(CommonVariablesKeys.UIDesignLoadFrom, m_UIDesignFrom);
            tResourceHelper.Save();

            MessageBox.Show("保存成功");
        }

        /// <summary>
        /// tree的菜单配置转实体
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menus"></param>
        private void MenuConfigToEntity(TreeNodeCollection nodes, List<AGSDM_UIMENU> menus)
        {
            //顺序号
            int currentSort = 0;
            foreach (TreeNode nodeChild in nodes)
            {
                MenuConfigNode itemCommandInfo = nodeChild.Tag as MenuConfigNode;

                AGSDM_UIMENU menu = new AGSDM_UIMENU();
                menu.GUID = itemCommandInfo.GUID;
                menu.CODE = itemCommandInfo.Code;
                menu.PARENTCODE = itemCommandInfo.ParentCode;
                menu.CAPTION = itemCommandInfo.Caption;
                menu.DLLFILE = itemCommandInfo.PlugAssembly;
                menu.CLASSNAME = itemCommandInfo.PlugType;
                menu.MENUTYPE = itemCommandInfo.MenuType.ToString();
                menu.EXT1 = itemCommandInfo.ExtValue1;
                menu.EXT2 = itemCommandInfo.ExtValue2;
                menu.EXT3 = itemCommandInfo.ExtValue3;
                menu.EXT4 = itemCommandInfo.ExtValue4;
                menu.EXT5 = itemCommandInfo.ExtValue5;
                menu.EXT6 = itemCommandInfo.ExtValue6;
                menu.EXT7 = itemCommandInfo.ExtValue7;
                menu.EXT8 = itemCommandInfo.ExtValue8;
                menu.EXT9 = itemCommandInfo.ExtValue9;
                menu.EXT10 = itemCommandInfo.ExtValue10;
                //每次保存都重新计算设置排序号
                menu.SORT = currentSort;

                currentSort++;

                menus.Add(menu);

                MenuConfigToEntity(nodeChild.Nodes, menus);
            }
        }

        private void btnLoadbyLocalDB_Click(object sender, EventArgs e)
        {
            try
            {
                m_UIDesignFrom = UIDesignFrom.Database;

                //初始化菜单树
                InitTree(UIDesignFrom.Database);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void btnLoadbyLocal_Click(object sender, EventArgs e)
        {
            try
            {
                m_UIDesignFrom = UIDesignFrom.XmlFile;

                //初始化菜单树
                InitTree(UIDesignFrom.XmlFile);
                BindPlugin();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 刷新数据源Textbox
        /// </summary>
        private void RefreshConfigSource()
        {
            if (m_UIDesignFrom == UIDesignFrom.XmlFile)
            {
                lblMenuConfigFrom.Text = "本地文件";
            }
            else
            {
                lblMenuConfigFrom.Text = "数据库";
            }
        }

        private void CMnuMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNodeUpDown(true);           //节点上移
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void CMnuMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNodeUpDown(false);          //节点下移
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 节点上移与下移
        /// </summary>
        /// <param name="isup">如果上移则为true,否则为false</param>
        private void TreeNodeUpDown(bool isup)
        {
            //***************************************************
            //设计思想：整个思想是按上移方法来解决。
            //如果是下移的情况，则考虑将当前节点的下一节点上移；
            //***************************************************  

            TreeNode pTreeNode = tvwMenu.SelectedNode;         //当前选择节点 
            TreeNode pParentNode = pTreeNode.Parent;                    //父节点

            if (isup == false) pTreeNode = pTreeNode.NextNode;          //当前节点的下一节点  
            if (pTreeNode == null) return;                              //选择节点为空时返回

            int index = 0;
            //if (isup == false)
            //    index = pTreeNode.Index;                              //向下移动时,PTreeNode已为当前选择节点的下一节点
            //else
            index = pTreeNode.Index - 1;                            //向上移动时,选择索引应为当前选择节点索引减一

            if (index < 0) return;                                      //当前节点已为第一个子节点，不能上移。

            if (pParentNode == null)
            {
                //父节点为空的情况             
                tvwMenu.BeginUpdate();
                tvwMenu.Nodes.Insert(index, pTreeNode.Clone() as TreeNode);
                pTreeNode.Remove();
                tvwMenu.EndUpdate();

                tvwMenu.SelectedNode = tvwMenu.Nodes[index];
            }
            else
            {
                //父节点不为空的情况          
                tvwMenu.BeginUpdate();
                pParentNode.Nodes.Insert(index, pTreeNode.Clone() as TreeNode);
                pTreeNode.Remove();
                tvwMenu.EndUpdate();
                if (isup == false)
                    tvwMenu.SelectedNode = pParentNode.Nodes[index + 1];
                else
                    tvwMenu.SelectedNode = pParentNode.Nodes[index];
            }
        }

        private void FormMenuConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //进行保存
                if (m_UIDesignFrom == UIDesignFrom.XmlFile)
                {
                    btnSavetoLocal_Click(null, null);
                }
                else
                {
                    btnSavetoLocalDB_Click(null, null);
                }
            }
            else if (result == DialogResult.No)
            {

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void tvwMenu_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void tvwMenu_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeNode myNode = null;
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
                }
                else
                {
                    MessageBox.Show("操作错误");
                }

                m_DrapPosition.X = e.X;
                m_DrapPosition.Y = e.Y;
                m_DrapPosition = tvwMenu.PointToClient(m_DrapPosition);
                TreeNode DropNode = tvwMenu.GetNodeAt(m_DrapPosition);
                //移动目标节点不能为空
                if (DropNode == null) return;
                //目标节点不能和移动节点同一个
                if (myNode == DropNode) return;
                //父节点是节点
                if (myNode.Parent != null)
                {
                    //必须父节点是同一个，就是只能在同一父节点移动
                    if (myNode.Parent == DropNode.Parent)
                    {
                        int DropIndex = DropNode.Index;

                        myNode.Remove();

                        if (DropIndex > DropNode.Parent.Nodes.Count - 1)
                            DropIndex = DropNode.Parent.Nodes.Count - 1;

                        DropNode.Parent.Nodes.Insert(DropIndex, myNode);
                    }
                    else
                    {
                        MessageBox.Show("只能在同一父节点内移动");
                        return;
                    }
                }
                //父节点为null，也就是在根节点
                else
                {
                    //必须父节点是同一个，就是只能在同一父节点移动
                    if (myNode.Parent == null && DropNode.Parent == null)
                    {
                        int DropIndex = DropNode.Index;

                        myNode.Remove();

                        if (DropIndex > DropNode.Parent.Nodes.Count - 1)
                            DropIndex = DropNode.Parent.Nodes.Count - 1;

                        tvwMenu.Nodes.Insert(DropIndex, myNode);
                    }
                    else
                    {
                        MessageBox.Show("只能在同一父节点内移动");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void tvwMenu_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }
    }
}
