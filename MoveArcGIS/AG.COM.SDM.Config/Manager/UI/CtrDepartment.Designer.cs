namespace AG.COM.SDM.Config
{
    partial class CtrDepartment
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrDepartment));
            this.tvwDepartment = new System.Windows.Forms.TreeView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddRoot = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddRoot = new System.Windows.Forms.ToolStripButton();
            this.btnAddChild = new System.Windows.Forms.ToolStripButton();
            this.btnModify = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnQuery = new System.Windows.Forms.ToolStripButton();
            this.btnUsers = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddChild = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsModify = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvwDepartment
            // 
            this.tvwDepartment.ContextMenuStrip = this.contextMenuStrip2;
            this.tvwDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwDepartment.HideSelection = false;
            this.tvwDepartment.ImageIndex = 0;
            this.tvwDepartment.ImageList = this.imageList1;
            this.tvwDepartment.ItemHeight = 18;
            this.tvwDepartment.Location = new System.Drawing.Point(0, 0);
            this.tvwDepartment.Name = "tvwDepartment";
            this.tvwDepartment.SelectedImageIndex = 0;
            this.tvwDepartment.Size = new System.Drawing.Size(454, 343);
            this.tvwDepartment.TabIndex = 0;
            this.tvwDepartment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDepartment_AfterSelect);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddRoot});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 48);
            // 
            // tsmAddRoot
            // 
            this.tsmAddRoot.Image = ((System.Drawing.Image)(resources.GetObject("tsmAddRoot.Image")));
            this.tsmAddRoot.Name = "tsmAddRoot";
            this.tsmAddRoot.Size = new System.Drawing.Size(152, 22);
            this.tsmAddRoot.Text = "添加根级单位";
            this.tsmAddRoot.Click += new System.EventHandler(this.tsmAddRoot_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dept.png");
            this.imageList1.Images.SetKeyName(1, "user.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddRoot,
            this.btnAddChild,
            this.btnModify,
            this.btnDelete,
            this.btnQuery,
            this.btnUsers});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(454, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // btnAddRoot
            // 
            this.btnAddRoot.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRoot.Image")));
            this.btnAddRoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRoot.Name = "btnAddRoot";
            this.btnAddRoot.Size = new System.Drawing.Size(100, 22);
            this.btnAddRoot.Text = "添加根级单位";
            this.btnAddRoot.Click += new System.EventHandler(this.btnAddRoot_Click);
            // 
            // btnAddChild
            // 
            this.btnAddChild.Enabled = false;
            this.btnAddChild.Image = ((System.Drawing.Image)(resources.GetObject("btnAddChild.Image")));
            this.btnAddChild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(100, 22);
            this.btnAddChild.Text = "添加子级单位";
            this.btnAddChild.Click += new System.EventHandler(this.btnAddChild_Click);
            // 
            // btnModify
            // 
            this.btnModify.Enabled = false;
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(52, 22);
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(52, 22);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Enabled = false;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(52, 22);
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnUsers.Image")));
            this.btnUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(76, 22);
            this.btnUsers.Text = "部门用户";
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddChild,
            this.tsmUser,
            this.tmsModify,
            this.tsmDelete,
            this.tsmQuery});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 114);
            // 
            // tsmAddChild
            // 
            this.tsmAddChild.Image = ((System.Drawing.Image)(resources.GetObject("tsmAddChild.Image")));
            this.tsmAddChild.Name = "tsmAddChild";
            this.tsmAddChild.Size = new System.Drawing.Size(148, 22);
            this.tsmAddChild.Text = "添加子级单位";
            this.tsmAddChild.Click += new System.EventHandler(this.tsmAddChild_Click);
            // 
            // tsmUser
            // 
            this.tsmUser.Image = ((System.Drawing.Image)(resources.GetObject("tsmUser.Image")));
            this.tsmUser.Name = "tsmUser";
            this.tsmUser.Size = new System.Drawing.Size(148, 22);
            this.tsmUser.Text = "用户管理";
            this.tsmUser.Click += new System.EventHandler(this.tsmUser_Click);
            // 
            // tmsModify
            // 
            this.tmsModify.Image = ((System.Drawing.Image)(resources.GetObject("tmsModify.Image")));
            this.tmsModify.Name = "tmsModify";
            this.tmsModify.Size = new System.Drawing.Size(148, 22);
            this.tmsModify.Text = "修改";
            this.tmsModify.Click += new System.EventHandler(this.tmsModify_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmDelete.Image")));
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(148, 22);
            this.tsmDelete.Text = "删除";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // tsmQuery
            // 
            this.tsmQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsmQuery.Image")));
            this.tsmQuery.Name = "tsmQuery";
            this.tsmQuery.Size = new System.Drawing.Size(148, 22);
            this.tsmQuery.Text = "查询";
            this.tsmQuery.Click += new System.EventHandler(this.tsmQuery_Click);
            // 
            // CtrDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvwDepartment);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CtrDepartment";
            this.Size = new System.Drawing.Size(454, 343);
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvwDepartment;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddChild;
        private System.Windows.Forms.ToolStripButton btnModify;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnQuery;
        private System.Windows.Forms.ToolStripButton btnAddRoot;
        private System.Windows.Forms.ToolStripButton btnUsers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmAddChild;
        private System.Windows.Forms.ToolStripMenuItem tsmUser;
        private System.Windows.Forms.ToolStripMenuItem tmsModify;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmQuery;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmAddRoot;
        private System.Windows.Forms.ImageList imageList1;
    }
}
