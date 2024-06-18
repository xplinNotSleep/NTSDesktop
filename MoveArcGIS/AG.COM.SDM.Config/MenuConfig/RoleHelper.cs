using AG.COM.SDM.Framework;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AG.COM.SDM.Config.MenuConfig
{
    public class RoleHelper
    {
        /// <summary>
        /// 初始化菜单树
        /// </summary>
        /// <param name="loadFrom"></param>
        public static void InitMenuConfigTree(TreeView tvw, UIDesignFrom loadFrom)
        {
            //加载所有菜单
            IList<AGSDM_UIMENU> menus = MenuLoadHelper.LoadMenuConfigEntity(loadFrom);

            tvw.Nodes.Clear();
            //根节点固定，菜单和工具条
            TreeNode nodeMenu = new TreeNode();
            nodeMenu.Text = "菜单";
            nodeMenu.ImageIndex = 0;
            nodeMenu.SelectedImageIndex = 0;
            nodeMenu.Tag = CreateRootMenuConfigNode("menu", "菜单", MenuType.Menu, 0);
            tvw.Nodes.Add(nodeMenu);

            //顶层节点
            List<AGSDM_UIMENU> menuTops = menus.Where(t => t.PARENTCODE == "menu").ToList();
            //添加节点
            foreach (AGSDM_UIMENU menuTop in menuTops)
            {
                BuildTreeNode(menuTop, nodeMenu.Nodes, menus);
            }

            //添加工具条根节点
            TreeNode nodeToolbar = new TreeNode();
            nodeToolbar.Text = "工具条";
            nodeToolbar.ImageIndex = 1;
            nodeToolbar.SelectedImageIndex = 1;
            nodeToolbar.Tag = CreateRootMenuConfigNode("toolbar", "工具条", MenuType.Toolbar, 1);
            tvw.Nodes.Add(nodeToolbar);

            menuTops = menus.Where(t => t.PARENTCODE == "toolbar").ToList();
            //添加节点
            foreach (AGSDM_UIMENU menuTop in menuTops)
            {
                BuildTreeNode(menuTop, nodeToolbar.Nodes, menus);
            }
            //展开第一层节点的子节点（节点太多，因此只展开第一层）
            foreach (TreeNode node in tvw.Nodes)
            {
                node.Expand();
            }

            if (tvw.Nodes.Count > 0) tvw.SelectedNode = tvw.Nodes[0];
        }

        /// <summary>
        /// 创建根节点的MenuConfigNode
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="caption"></param>
        /// <param name="menutype"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        private static MenuConfigNode CreateRootMenuConfigNode(string GUID, string caption, MenuType menutype, int sort)
        {
            MenuConfigNode menuConfigNode = new MenuConfigNode();
            menuConfigNode.GUID = GUID;
            menuConfigNode.Caption = caption;
            menuConfigNode.MenuType = menutype;
            menuConfigNode.Sort = sort;
            menuConfigNode.ParentCode = "0";

            return menuConfigNode;
        }

        /// <summary>
        /// 建树节点
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="nodesParent"></param>
        /// <param name="menuAlls"></param>
        private static void BuildTreeNode(AGSDM_UIMENU menu, TreeNodeCollection nodesParent, IList<AGSDM_UIMENU> menuAlls)
        {
            //实体转ItemCommandInfo对象
            MenuConfigNode menuConfigNode = new MenuConfigNode();
            menuConfigNode.GUID = menu.GUID;
            menuConfigNode.Caption = menu.CAPTION;
            menuConfigNode.MenuType = !string.IsNullOrEmpty(menu.MENUTYPE) ? (MenuType)Enum.Parse(typeof(MenuType), menu.MENUTYPE, true) : MenuType.None;
            menuConfigNode.Code = menu.CODE;
            menuConfigNode.ParentCode = menu.PARENTCODE;
            menuConfigNode.PlugAssembly = menu.DLLFILE;
            menuConfigNode.PlugType = menu.CLASSNAME;
            menuConfigNode.ExtValue1 = menu.EXT1;
            menuConfigNode.ExtValue2 = menu.EXT2;
            menuConfigNode.ExtValue3 = menu.EXT3;
            menuConfigNode.ExtValue4 = menu.EXT4;
            menuConfigNode.ExtValue5 = menu.EXT5;
            menuConfigNode.ExtValue6 = menu.EXT6;
            menuConfigNode.ExtValue7 = menu.EXT7;
            menuConfigNode.ExtValue8 = menu.EXT8;
            menuConfigNode.ExtValue9 = menu.EXT9;
            menuConfigNode.ExtValue10 = menu.EXT10;
            menuConfigNode.Sort = DataConvert.DecNotNull(menu.SORT);

            //新建树节点
            TreeNode node = new TreeNode();
            node.Text = menuConfigNode.Caption;
            node.Tag = menuConfigNode;
            node.ImageIndex = GetImageIdxByType(menuConfigNode.MenuType);
            node.SelectedImageIndex = node.ImageIndex;

            nodesParent.Add(node);
            //继续添加子节点
            List<AGSDM_UIMENU> menuChilds = menuAlls.Where(t => t.PARENTCODE == menuConfigNode.GUID).ToList();
            foreach (AGSDM_UIMENU menuChild in menuChilds)
            {
                BuildTreeNode(menuChild, node.Nodes, menuAlls);
            }
        }

        /// <summary>
        /// 根据菜单类型获取节点图片索引
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public static int GetImageIdxByType(MenuType menuType)
        {
            int imageIdx = 0;

            switch (menuType)
            {
                case MenuType.Menu:
                    imageIdx = 0;
                    break;
                case MenuType.Toolbar:
                    imageIdx = 1;
                    break;
                case MenuType.Item:
                    imageIdx = 2;
                    break;
                case MenuType.ComboBox:
                    imageIdx = 3;
                    break;
                case MenuType.Text:
                    imageIdx = 4;
                    break;
                case MenuType.CustomControl:
                    imageIdx = 5;
                    break;
                case MenuType.Separator:
                    imageIdx = 6;
                    break;
                case MenuType.RibbonPage:
                    imageIdx = 7;
                    break;
                case MenuType.RibbonPageGroup:
                    imageIdx = 8;
                    break;
                case MenuType.ComboBoxTreeView:
                    imageIdx = 5;
                    break;
                default:
                    imageIdx = 2;
                    break;
            }

            return imageIdx;
        }
    }
}
