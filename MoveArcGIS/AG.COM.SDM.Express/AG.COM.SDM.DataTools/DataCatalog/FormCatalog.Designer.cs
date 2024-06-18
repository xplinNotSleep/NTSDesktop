namespace AG.COM.SDM.DataTools.DataCatalog
{
    partial class FormDataCatalog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btLocate = new System.Windows.Forms.Button();
            this.lblWorkspaceDes = new System.Windows.Forms.Label();
            this.txtWorkspace = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCreateFeatureDataset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateFeatureClass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModifyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRegisterVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUnRegisterVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCreateHisArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteHisArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvwItems = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLocate
            // 
            this.btLocate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btLocate.Location = new System.Drawing.Point(258, 4);
            this.btLocate.Name = "btLocate";
            this.btLocate.Size = new System.Drawing.Size(63, 20);
            this.btLocate.TabIndex = 1;
            this.btLocate.Text = "重定位...";
            this.btLocate.UseVisualStyleBackColor = true;
            this.btLocate.Click += new System.EventHandler(this.btLocate_Click);
            // 
            // lblWorkspaceDes
            // 
            this.lblWorkspaceDes.AutoSize = true;
            this.lblWorkspaceDes.Location = new System.Drawing.Point(2, 8);
            this.lblWorkspaceDes.Name = "lblWorkspaceDes";
            this.lblWorkspaceDes.Size = new System.Drawing.Size(59, 12);
            this.lblWorkspaceDes.TabIndex = 2;
            this.lblWorkspaceDes.Text = "工作空间:";
            // 
            // txtWorkspace
            // 
            this.txtWorkspace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWorkspace.Location = new System.Drawing.Point(60, 7);
            this.txtWorkspace.Name = "txtWorkspace";
            this.txtWorkspace.ReadOnly = true;
            this.txtWorkspace.Size = new System.Drawing.Size(192, 14);
            this.txtWorkspace.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateFeatureDataset,
            this.mnuCreateFeatureClass,
            this.toolStripMenuItem1,
            this.mnuRename,
            this.mnuModifyTable,
            this.toolStripMenuItem2,
            this.mnuDelete,
            this.toolStripMenuItem3,
            this.mnuRegisterVersion,
            this.mnuUnRegisterVersion,
            this.toolStripMenuItem4,
            this.mnuCreateHisArchive,
            this.mnuDeleteHisArchive,
            this.toolStripMenuItem5,
            this.mnuRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 254);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // mnuCreateFeatureDataset
            // 
            this.mnuCreateFeatureDataset.Name = "mnuCreateFeatureDataset";
            this.mnuCreateFeatureDataset.Size = new System.Drawing.Size(142, 22);
            this.mnuCreateFeatureDataset.Text = "新建要素集";
            // 
            // mnuCreateFeatureClass
            // 
            this.mnuCreateFeatureClass.Name = "mnuCreateFeatureClass";
            this.mnuCreateFeatureClass.Size = new System.Drawing.Size(142, 22);
            this.mnuCreateFeatureClass.Text = "新建要素类";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuRename
            // 
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.Size = new System.Drawing.Size(142, 22);
            this.mnuRename.Text = "重命名";
            // 
            // mnuModifyTable
            // 
            this.mnuModifyTable.Name = "mnuModifyTable";
            this.mnuModifyTable.Size = new System.Drawing.Size(142, 22);
            this.mnuModifyTable.Text = "修改表结构";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(142, 22);
            this.mnuDelete.Text = "删除";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuRegisterVersion
            // 
            this.mnuRegisterVersion.Name = "mnuRegisterVersion";
            this.mnuRegisterVersion.Size = new System.Drawing.Size(142, 22);
            this.mnuRegisterVersion.Text = "注册版本";
            // 
            // mnuUnRegisterVersion
            // 
            this.mnuUnRegisterVersion.Name = "mnuUnRegisterVersion";
            this.mnuUnRegisterVersion.Size = new System.Drawing.Size(142, 22);
            this.mnuUnRegisterVersion.Text = "反注册版本";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuCreateHisArchive
            // 
            this.mnuCreateHisArchive.Name = "mnuCreateHisArchive";
            this.mnuCreateHisArchive.Size = new System.Drawing.Size(142, 22);
            this.mnuCreateHisArchive.Text = "创建历史存档";
            // 
            // mnuDeleteHisArchive
            // 
            this.mnuDeleteHisArchive.Name = "mnuDeleteHisArchive";
            this.mnuDeleteHisArchive.Size = new System.Drawing.Size(142, 22);
            this.mnuDeleteHisArchive.Text = "删除历史存档";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.Size = new System.Drawing.Size(142, 22);
            this.mnuRefresh.Text = "刷新";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtWorkspace);
            this.panel1.Controls.Add(this.btLocate);
            this.panel1.Controls.Add(this.lblWorkspaceDes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 28);
            this.panel1.TabIndex = 5;
            // 
            // tvwItems
            // 
            this.tvwItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwItems.Location = new System.Drawing.Point(0, 28);
            this.tvwItems.Name = "tvwItems";
            this.tvwItems.Size = new System.Drawing.Size(324, 403);
            this.tvwItems.TabIndex = 6;
            this.tvwItems.DoubleClick += new System.EventHandler(this.tvwItems_DoubleClick);
            this.tvwItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwItems_MouseDown);
            // 
            // FormDataCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 431);
            this.Controls.Add(this.tvwItems);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataCatalog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据管理面板";
            this.Load += new System.EventHandler(this.FormDataCatalog_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLocate;
        private System.Windows.Forms.Label lblWorkspaceDes;
        private System.Windows.Forms.TextBox txtWorkspace;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateFeatureDataset;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateFeatureClass;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuModifyTable;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuRegisterVersion;
        private System.Windows.Forms.ToolStripMenuItem mnuUnRegisterVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateHisArchive;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteHisArchive;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvwItems;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
    }
}

