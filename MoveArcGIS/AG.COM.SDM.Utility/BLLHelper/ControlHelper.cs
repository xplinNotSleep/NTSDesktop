using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 控件帮助类
    /// </summary>
    public class ControlHelper
    {
        #region TreeView

        /// <summary>
        /// 选择TreeView最下层的第一个子节点
        /// </summary>
        /// <param name="tTreeView"></param>
        public static void TreeViewSelectFirstChildNode(TreeView tTreeView)
        {
            if (tTreeView == null) return;
            ///获取TreeView节点集第一个节点的子节点
            TreeNode tNode = NodesGetFirstChildNode(tTreeView.Nodes);

            if (tNode != null)
            {
                tTreeView.SelectedNode = tNode;
            }
        }

        /// <summary>
        /// 获取TreeView节点集第一个节点的子节点
        /// </summary>
        /// <param name="tNodes"></param>
        /// <returns></returns>
        private static TreeNode NodesGetFirstChildNode(TreeNodeCollection tNodes)
        {
            if (tNodes != null && tNodes.Count > 0 && tNodes[0].Nodes.Count>0)
            {
                return NodesGetFirstChildNode(tNodes[0].Nodes);
            }
            else if (tNodes != null && tNodes.Count > 0)
            {
                return tNodes[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取一个节点的最上级父节点
        /// </summary>
        /// <param name="tNode"></param>
        /// <returns></returns>
        public static TreeNode TreeViewGetTopNode(TreeNode tNode)
        {
            TreeNode tNodeTop = null;

            if (tNode != null)
            {
                tNodeTop = tNode;
                while (tNodeTop.Parent != null)
                {
                    tNodeTop = tNodeTop.Parent;
                }
            }

            return tNodeTop;
        }

        #region TreeView树节点级联选择

        /// <summary>
        /// TreeView树节点级联选择
        /// </summary>
        /// <param name="e"></param>
        public static void TreeViewRelateSelect(TreeViewEventArgs e, TreeViewRelateSelectDirection tDirection)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (tDirection == TreeViewRelateSelectDirection.Parent)
                {
                    //递归设置父节点选中状态
                    SetParentNodeChecked(e.Node);
                }
                else if (tDirection == TreeViewRelateSelectDirection.Child)
                {
                    //递归设置子节点选中状态
                    SetChildNodeChecked(e.Node);
                }
                else
                {
                    //递归设置子节点选中状态
                    SetChildNodeChecked(e.Node);
                    //递归设置父节点选中状态
                    SetParentNodeChecked(e.Node);
                }
            }
        }

        /// <summary>
        /// 递归设置父节点选中状态
        /// </summary>
        /// <param name="ptreeNode">当前节点</param>
        private static void SetParentNodeChecked(TreeNode ptreeNode)
        {
            if (ptreeNode.Checked == true)
            {
                if (ptreeNode.Parent != null)
                {
                    ptreeNode.Parent.Checked = ptreeNode.Checked;
                    //递归设置父节点选中状态
                    SetParentNodeChecked(ptreeNode.Parent);
                }
            }
            else
            {
                if (ptreeNode.Parent != null)
                {
                    if (!HasChildChecked(ptreeNode.Parent))
                    {
                        ptreeNode.Parent.Checked = false;
                        SetParentNodeChecked(ptreeNode.Parent);
                    }
                }
            }
        }

        /// <summary>
        /// 子节点是否有选中
        /// </summary>
        /// <param name="ptreeNode"></param>
        /// <returns></returns>
        private static bool HasChildChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode subNode in ptreeNode.Nodes)
            {
                if (subNode.Checked) return true;
            }

            return false;
        }

        /// <summary>
        /// 递归设置子节点选中状态
        /// </summary>
        /// <param name="ptreeNode">当前节点</param>
        private static void SetChildNodeChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode treeNode in ptreeNode.Nodes)
            {
                treeNode.Checked = ptreeNode.Checked;
                //递归设置子节点选中状态
                SetChildNodeChecked(treeNode);
            }
        }

        #endregion

        #endregion

        #region  combobox

        /// <summary>
        /// 填充所有系统字体到Combobox
        /// </summary>
        /// <param name="tComboBox"></param>
        public static void ComboBoxFillAllFont(ComboBox tComboBox)
        {
            tComboBox.Items.Clear();

            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily ff in fonts.Families)
            {
                tComboBox.Items.Add(ff.Name);
            }
        }

        #endregion

        #region 其他

        /// <summary>
        /// 禁用的启用所有控件（一般用于禁用Form里的所有控件）
        /// </summary>
        /// <param name="tControls"></param>
        /// <param name="Enabled"></param>
        public static void EnabledAllControls(Control.ControlCollection tControls, bool Enabled)
        {
            foreach (Control tControl in tControls)
            {
                //TextBox是特例，使用ReadOnly
                if (tControl is TextBox)
                {
                    (tControl as TextBox).ReadOnly = (!Enabled);
                }
                else
                {
                    tControl.Enabled = Enabled;
                }

                EnabledAllControls(tControl.Controls, Enabled);
            }
        }

        #endregion
    }

    /// <summary>
    /// TreeView树节点级联选择方向
    /// </summary>
    public enum TreeViewRelateSelectDirection
    {
        /// <summary>
        /// 父节点
        /// </summary>
        Parent,

        /// <summary>
        /// 子节点
        /// </summary>
        Child,

        /// <summary>
        /// 父节点和子节点
        /// </summary>
        All
    }
}
