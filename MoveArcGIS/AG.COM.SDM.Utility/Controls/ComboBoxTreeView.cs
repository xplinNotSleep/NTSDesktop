using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// 树形下拉框
    /// </summary>
    public class ComboBoxTreeView : ComboBox, IMessageFilter
    {
        private const int WM_LBUTTONDOWN = 0x201, WM_LBUTTONDBLCLK = 0x203;
        ToolStripControlHost treeViewHost;
        ToolStripDropDown dropDown;

        /// <summary>
        /// 快速搜索框
        /// </summary>
        private ListBox lstSearch = new ListBox();

        /// <summary>
        /// 快速搜索所有值
        /// </summary>
        private List<string> m_AllSearchValues = null;

        /// <summary>
        /// 保存所有查找值的拼音
        /// </summary>
        private Dictionary<string, List<List<string>>> m_PinYins = new Dictionary<string, List<List<string>>>();

        private bool m_QuickSearch = false;
        /// <summary>
        /// 是否使用快捷查询
        /// </summary>
        public bool QuickSearch
        {
            get { return m_QuickSearch; }
            set { m_QuickSearch = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tQuickSearch">是否使用快速查询</param>
        public ComboBoxTreeView(bool tQuickSearch)
        {
            //下拉框高度
            this.DropDownHeight = 300;
            //初始化搜索Listbox
            InitListboxSearch();

            this.TextChanged += new System.EventHandler(ComboBoxTreeView_TextChanged);
            this.MouseDown += new MouseEventHandler(ComboBoxTreeView_MouseDown);

            TreeView treeView = new TreeView();
            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            treeView.BorderStyle = BorderStyle.None;
            treeView.ItemHeight = 20;

            treeViewHost = new ToolStripControlHost(treeView);
            dropDown = new ToolStripDropDown();
            dropDown.Width = this.Width;
            dropDown.Items.Add(treeViewHost);
            if (tQuickSearch == true)
            {
                Application.AddMessageFilter(this);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboBoxTreeView()
            : this(false)
        {

        }

        /// <summary>
        /// 初始化搜索Listbox
        /// </summary>
        private void InitListboxSearch()
        {
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.ItemHeight = 12;
            this.lstSearch.Location = new System.Drawing.Point(63, 112);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(157, 88);
            this.lstSearch.TabIndex = 16;
            this.lstSearch.Visible = false;
            this.lstSearch.MouseClick += new MouseEventHandler(lstSearch_MouseClick);
        }

        #region 快速搜索

        void lstSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //单击搜索Listbox的项，选中对应的TreeView节点，并关闭Listbox
                if (lstSearch.SelectedItem == null) return;

                SelectNodeByText(this.TreeView.Nodes, Convert.ToString(lstSearch.SelectedItem));

                lstSearch.Visible = false;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 选择TreeNode
        /// </summary>
        /// <param name="tNodes"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool SelectNodeByText(TreeNodeCollection tNodes, string text)
        {
            foreach (TreeNode tNode in tNodes)
            {
                if (tNode.Text == text)
                {
                    this.TreeView.SelectedNode = tNode;
                    //触发选择事件
                    System.EventArgs ee = new System.EventArgs();
                    OnSelectedValueChanged(ee);

                    return true;
                }

                if (SelectNodeByText(tNode.Nodes, text) == true)
                    return true;
            }

            return false;
        }

        void ComboBoxTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Form tFormParent = this.FindForm();
                //鼠标单击到本控件，打开搜索Listbox

                //把Listbox添加到父控件
                if (lstSearch.Parent == null)
                {
                    Control tThisParent = tFormParent;
                    if (tThisParent != null)
                    {
                        tThisParent.Controls.Add(lstSearch);
                        lstSearch.BringToFront();
                    }
                }

                if (lstSearch.Visible == false)
                {
                    //加载TreeView所有的节点
                    if (m_AllSearchValues == null)
                    {
                        m_AllSearchValues = new List<string>();

                        GetNodeValue(this.TreeView.Nodes, m_AllSearchValues);
                    }
                    //触发文本改变事件，进行搜索
                    ComboBoxTreeView_TextChanged(null, null);

                    Point tPointScreen = this.Parent.PointToScreen(this.Location);
                    Point tPointInForm = tFormParent.PointToClient(tPointScreen);

                    tPointInForm.Y += this.Size.Height;

                    //移动Listbox到正确位置，并显示             
                    lstSearch.Location = tPointInForm;
                    lstSearch.Size = new Size(this.Size.Width, lstSearch.Size.Height);

                    if (m_QuickSearch == true)
                    {
                        lstSearch.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取TreeNode的值
        /// </summary>
        /// <param name="tNodes"></param>
        /// <param name="tAllSearchValues"></param>
        private void GetNodeValue(TreeNodeCollection tNodes, List<string> tAllSearchValues)
        {
            foreach (TreeNode tNode in tNodes)
            {
                if (!string.IsNullOrEmpty(tNode.Text) && tAllSearchValues.Contains(tNode.Text.Trim()) == false)
                {
                    tAllSearchValues.Add(tNode.Text.Trim());
                }

                GetNodeValue(tNode.Nodes, tAllSearchValues);
            }
        }

        void ComboBoxTreeView_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                //文本改变事件，进行搜索
                if (m_AllSearchValues == null) return;

                if (!string.IsNullOrEmpty(this.Text))
                {
                    List<string> result = ChineseSpellHelper.PinYinQuery(this.Text, m_AllSearchValues, ref m_PinYins);

                    lstSearch.DataSource = result;
                }
                else
                {
                    lstSearch.DataSource = m_AllSearchValues;

                    lstSearch.Visible = false;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Text = TreeView.SelectedNode.Text;
            System.EventArgs ee = new System.EventArgs();

            OnSelectedValueChanged(ee);
            dropDown.Close();
        }

        /// <summary>
        /// 获取树视图对象
        /// </summary>
        public TreeView TreeView
        {
            get { return treeViewHost.Control as TreeView; }
        }

        private void ShowDropDown()
        {
            if (dropDown != null)
            {
                treeViewHost.Size = new Size(DropDownWidth - 2, DropDownHeight);
                dropDown.Show(this, 0, this.Height);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_LBUTTONDOWN)
            {
                this.OnDropDown(new System.EventArgs());

                ShowDropDown();

                return;
            }

            base.WndProc(ref m);
        }

        public bool PreFilterMessage(ref Message m)
        {
            try
            {
                //实现点击快速查询框以外的地方关闭快速查询框
                if (this.Parent == null)
                {
                    Application.RemoveMessageFilter(this);

                    return false;
                }

                if (m.Msg == WM_LBUTTONDOWN && (m.HWnd != this.Handle && m.HWnd != this.lstSearch.Handle))
                {
                    lstSearch.Visible = false;
                }

                return false;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dropDown != null)
                {
                    dropDown.Dispose();
                    dropDown = null;
                }
            }
            base.Dispose(disposing);
        }
    }

}
