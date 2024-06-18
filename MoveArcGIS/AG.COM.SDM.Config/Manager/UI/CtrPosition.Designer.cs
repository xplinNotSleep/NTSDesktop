namespace AG.COM.SDM.Config
{
    partial class CtrPosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrPosition));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddPosition = new System.Windows.Forms.ToolStripButton();
            this.tsbEditPos = new System.Windows.Forms.ToolStripButton();
            this.tsbDeletePos = new System.Windows.Forms.ToolStripButton();
            this.tsbSetUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSetProject = new System.Windows.Forms.ToolStripButton();
            this.tvwPosition = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSetPrincipal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnSetPrincipal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnSetOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddPosition,
            this.tsbEditPos,
            this.tsbDeletePos,
            this.tsbSetUser,
            this.toolStripSetProject});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(441, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddPosition
            // 
            this.tsbAddPosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddPosition.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddPosition.Image")));
            this.tsbAddPosition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddPosition.Name = "tsbAddPosition";
            this.tsbAddPosition.Size = new System.Drawing.Size(57, 22);
            this.tsbAddPosition.Text = "添加岗位";
            this.tsbAddPosition.Click += new System.EventHandler(this.tsbAddPosition_Click);
            // 
            // tsbEditPos
            // 
            this.tsbEditPos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEditPos.Enabled = false;
            this.tsbEditPos.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditPos.Image")));
            this.tsbEditPos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditPos.Name = "tsbEditPos";
            this.tsbEditPos.Size = new System.Drawing.Size(57, 22);
            this.tsbEditPos.Text = "编辑岗位";
            this.tsbEditPos.Click += new System.EventHandler(this.tsbEditPos_Click);
            // 
            // tsbDeletePos
            // 
            this.tsbDeletePos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDeletePos.Enabled = false;
            this.tsbDeletePos.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeletePos.Image")));
            this.tsbDeletePos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeletePos.Name = "tsbDeletePos";
            this.tsbDeletePos.Size = new System.Drawing.Size(57, 22);
            this.tsbDeletePos.Text = "删除岗位";
            this.tsbDeletePos.Click += new System.EventHandler(this.tsbDeletePos_Click);
            // 
            // tsbSetUser
            // 
            this.tsbSetUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSetUser.Enabled = false;
            this.tsbSetUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetUser.Image")));
            this.tsbSetUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetUser.Name = "tsbSetUser";
            this.tsbSetUser.Size = new System.Drawing.Size(57, 22);
            this.tsbSetUser.Text = "设置用户";
            this.tsbSetUser.Click += new System.EventHandler(this.tsbSetUser_Click);
            // 
            // toolStripSetProject
            // 
            this.toolStripSetProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSetProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSetProject.Image")));
            this.toolStripSetProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSetProject.Name = "toolStripSetProject";
            this.toolStripSetProject.Size = new System.Drawing.Size(57, 22);
            this.toolStripSetProject.Text = "权限设置";
            this.toolStripSetProject.Click += new System.EventHandler(this.toolStripSetProject_Click);
            // 
            // tvwPosition
            // 
            this.tvwPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwPosition.ImageIndex = 0;
            this.tvwPosition.ImageList = this.imageList1;
            this.tvwPosition.Location = new System.Drawing.Point(0, 25);
            this.tvwPosition.Name = "tvwPosition";
            this.tvwPosition.SelectedImageIndex = 0;
            this.tvwPosition.Size = new System.Drawing.Size(441, 352);
            this.tvwPosition.TabIndex = 1;
            this.tvwPosition.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwPosition_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Office.ico");
            this.imageList1.Images.SetKeyName(1, "00.gif");
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetPrincipal,
            this.tsmiUnSetPrincipal,
            this.tsmiSetOperator,
            this.tsmiUnSetOperator});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(155, 92);
            // 
            // tsmiSetPrincipal
            // 
            this.tsmiSetPrincipal.Name = "tsmiSetPrincipal";
            this.tsmiSetPrincipal.Size = new System.Drawing.Size(154, 22);
            this.tsmiSetPrincipal.Text = "设为部门领导";
            // 
            // tsmiUnSetPrincipal
            // 
            this.tsmiUnSetPrincipal.Name = "tsmiUnSetPrincipal";
            this.tsmiUnSetPrincipal.Size = new System.Drawing.Size(154, 22);
            this.tsmiUnSetPrincipal.Text = "取消领导设置";
            // 
            // tsmiSetOperator
            // 
            this.tsmiSetOperator.Name = "tsmiSetOperator";
            this.tsmiSetOperator.Size = new System.Drawing.Size(154, 22);
            this.tsmiSetOperator.Text = "设为权限管理人";
            // 
            // tsmiUnSetOperator
            // 
            this.tsmiUnSetOperator.Name = "tsmiUnSetOperator";
            this.tsmiUnSetOperator.Size = new System.Drawing.Size(154, 22);
            this.tsmiUnSetOperator.Text = "取消权限管理人";
            // 
            // CtrPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvwPosition);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CtrPosition";
            this.Size = new System.Drawing.Size(441, 377);
            this.Load += new System.EventHandler(this.CtrPosition_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddPosition;
        private System.Windows.Forms.ToolStripButton tsbEditPos;
        private System.Windows.Forms.ToolStripButton tsbDeletePos;
        private System.Windows.Forms.TreeView tvwPosition;
        private System.Windows.Forms.ToolStripButton tsbSetUser;
        private System.Windows.Forms.ToolStripButton toolStripSetProject;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetPrincipal;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnSetPrincipal;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetOperator;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnSetOperator;
        private System.Windows.Forms.ImageList imageList1;
    }
}
