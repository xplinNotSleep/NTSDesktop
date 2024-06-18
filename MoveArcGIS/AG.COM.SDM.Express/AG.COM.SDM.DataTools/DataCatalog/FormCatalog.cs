using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataCatalog
{
    /// <summary>
    /// 数据管理面板窗体类
    /// </summary>
    public partial class FormDataCatalog : Form
    { 
        private ImageListWrap m_Images = new ImageListWrap();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormDataCatalog()
        {
            InitializeComponent(); 
           
            tvwItems.ImageList = m_Images.ImageList;
            tvwItems.StateImageList = m_Images.ImageList;

            DataManagerHook hook = new DataManagerHook(tvwItems, m_Images);
            ESRI.ArcGIS.SystemUI.ICommand command;
            command = new Commands.AddFeatureDataset();
            command.OnCreate(hook);
            mnuCreateFeatureDataset.Tag = command; 
 
            command = new Commands.AddFeatureClass();            
            command.OnCreate(hook);
            mnuCreateFeatureClass.Tag = command; 

            command = new Commands.RenameObject();
            ((Commands.RenameObject)command).Rename += new Commands.RenameObject.RenameHandler(ReNameWorkspace);
            command.OnCreate(hook);
            mnuRename.Tag = command;

            command = new Commands.ModifyTableStructure();
            command.OnCreate(hook);
            mnuModifyTable.Tag = command;

            command = new Commands.DeleteObject();
            command.OnCreate(hook);
            mnuDelete.Tag = command;

            command = new Commands.RegisterVersion();
            command.OnCreate(hook);
            mnuRegisterVersion.Tag = command;

            command = new Commands.UnRegisterVersion();
            command.OnCreate(hook);
            mnuUnRegisterVersion.Tag = command;

            command = new Commands.CreateHisArchive();
            command.OnCreate(hook);
            mnuCreateHisArchive.Tag = command;

            command = new Commands.DeleteHisArchive();
            command.OnCreate(hook);
            mnuDeleteHisArchive.Tag = command;

            command = new Commands.RefreshObject();
            command.OnCreate(hook);
            mnuRefresh.Tag = command;

            mnuRefresh.Visible = false;
        }         

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 定位到指定的工作空间
        /// </summary>
        /// <param name="pWorkspace">工作空间</param>
        public void LocateTo(IWorkspace pWorkspace)
        {
            if (pWorkspace == null) return;
            ILockInfo tLockInfo = pWorkspace as ILockInfo;
            //定位到指定的工作空间
            LocateTo(new DatabaseWorkspaceItem(pWorkspace));
        }

        public void LocateTo(DataItem dataItem)
        {
            tvwItems.Nodes.Clear();

            if (dataItem.HasChildren == false)   return;

            TreeNode topNode = new TreeNode();
            topNode.Text = System.IO.Path.GetFileName(dataItem.Name);
            topNode.Tag = new DataItemWrap(dataItem, false);
            tvwItems.Nodes.Add(topNode);

            this.Cursor = Cursors.WaitCursor;
            try
            {
                txtWorkspace.Text = dataItem.Name;

                TreeNode node;
                DataItem item;

                IList<DataItem> items = dataItem.GetChildren();

                for (int i = 0; i <= items.Count - 1; i++)
                {
                    item = items[i];                     
                    node = new TreeNode();
                    node.Text = item.Name;
                    node.ImageIndex = m_Images.GetImageIndex(item.Type);
                    node.SelectedImageIndex = node.ImageIndex;

                    node.Tag = new DataItemWrap(item, false);
                    topNode.Nodes.Add(node);
                }

                topNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据管理", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FormDataCatalog_Load(object sender, EventArgs e)
        {

        }

        //重定位
        private void btLocate_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser dlg = new FormDataBrowser();
            dlg.AddFilter(new PersonalGeoDatabaseFilter());
            dlg.AddFilter(new FileGeoDatabaseFilter());
            dlg.AddFilter(new SDEWorkspaceFilter());
            dlg.MultiSelect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = dlg.SelectedItems;
                if (items.Count == 0)   return;

                LocateTo(items[0]);
            }

        }

        private void tvwItems_DoubleClick(object sender, EventArgs e)
        {
            if (tvwItems.SelectedNode == null) return;
            DataItemWrap wrap = tvwItems.SelectedNode.Tag as DataItemWrap;
            if (wrap == null) return;
            if (wrap.Opened) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                TreeNode parentNode = tvwItems.SelectedNode;
                TreeNode node;
                DataItem item;
                parentNode.Nodes.Clear();
                if (wrap.DataItem.HasChildren)
                {
                    IList<DataItem> items = wrap.DataItem.GetChildren();
                    for (int i = 0; i <= items.Count - 1; i++)
                    {
                        item = items[i];
                        node = new TreeNode();
                        node.Text = item.Name;
                        node.ImageIndex = m_Images.GetImageIndex(item.Type);
                        node.SelectedImageIndex = node.ImageIndex;

                        node.Tag = new DataItemWrap(item, false);
                        parentNode.Nodes.Add(node);
                    }
                }
                parentNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tvwItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (tvwItems.SelectedNode == null) return;

            //弹出右键菜单
            System.Drawing.Point pt = tvwItems.PointToScreen(new System.Drawing.Point(e.X, e.Y));
            contextMenuStrip1.Show(pt);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (tvwItems.SelectedNode == null) return;
            DataItemWrap wrap = tvwItems.SelectedNode.Tag as DataItemWrap;
            if (wrap == null) return;

            SetMenuEnabled(mnuCreateFeatureClass);
            SetMenuEnabled(mnuCreateFeatureDataset);
            SetMenuEnabled(mnuCreateHisArchive);
            SetMenuEnabled(mnuDelete);
            SetMenuEnabled(mnuDeleteHisArchive);
            SetMenuEnabled(mnuModifyTable);
            SetMenuEnabled(mnuRefresh);
            SetMenuEnabled(mnuRegisterVersion);
            SetMenuEnabled(mnuRename);
            SetMenuEnabled(mnuUnRegisterVersion);
        }

        private void ReNameWorkspace(string tName)
        {
            this.txtWorkspace.Text = tName;
        }

        private void SetMenuEnabled(ToolStripMenuItem menu)
        {
            ESRI.ArcGIS.SystemUI.ICommand command = menu.Tag as ESRI.ArcGIS.SystemUI.ICommand;
            if (command == null)
                menu.Enabled = false;
            else
                menu.Enabled = command.Enabled;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            contextMenuStrip1.Close(ToolStripDropDownCloseReason.ItemClicked);
            ESRI.ArcGIS.SystemUI.ICommand command = e.ClickedItem.Tag as ESRI.ArcGIS.SystemUI.ICommand;
            if (command != null)
                command.OnClick();
        }
    }

    public class DataItemWrap
    {
        public DataItemWrap(DataItem item,bool opened)
        {
            DataItem = item;
            Opened = opened;
        }

        public DataItem DataItem = null;
        public bool Opened = false;
        private bool openFlag = false;
        private object m_GeoObject = null;
        public object GeoObject
        {
            get
            {
                try
                {
                    if (openFlag == false)
                    {
                        m_GeoObject = DataItem.GetGeoObject();
                        openFlag = true;
                    }
                    return m_GeoObject;
                }
                catch
                {
                    return m_GeoObject;
                }
            }
        }
    }

    public class DataManagerHook
    {
        public DataManagerHook(TreeView treeView, ImageListWrap imgs)
        {
            m_Tree = treeView;
            m_Images = imgs;
        }

        private TreeView m_Tree = null;
        public TreeView TreeView
        {
            get { return m_Tree; }
        }

        private ImageListWrap m_Images=null;
        public ImageListWrap Images
        {
            get { return m_Images; }
        } 
    }
}