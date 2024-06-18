namespace AG.COM.SDM.Manager
{
    partial class FormStart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.ToolBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolBtnExit = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolBtnAdd,
            this.ToolBtnDelete,
            this.ToolBtnClear,
            this.toolStripSeparator1,
            this.ToolBtnSave,
            this.toolStripSeparator2,
            this.ToolBtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(605, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolBtnAdd
            // 
            this.ToolBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("ToolBtnAdd.Image")));
            this.ToolBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBtnAdd.Name = "ToolBtnAdd";
            this.ToolBtnAdd.Size = new System.Drawing.Size(52, 22);
            this.ToolBtnAdd.Text = "添加";
            this.ToolBtnAdd.Click += new System.EventHandler(this.ToolBtnAdd_Click);
            // 
            // ToolBtnDelete
            // 
            this.ToolBtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("ToolBtnDelete.Image")));
            this.ToolBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBtnDelete.Name = "ToolBtnDelete";
            this.ToolBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ToolBtnDelete.Text = "删除当前项";
            this.ToolBtnDelete.Click += new System.EventHandler(this.ToolBtnDelete_Click);
            // 
            // ToolBtnClear
            // 
            this.ToolBtnClear.Image = ((System.Drawing.Image)(resources.GetObject("ToolBtnClear.Image")));
            this.ToolBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBtnClear.Name = "ToolBtnClear";
            this.ToolBtnClear.Size = new System.Drawing.Size(52, 22);
            this.ToolBtnClear.Text = "清空";
            this.ToolBtnClear.Click += new System.EventHandler(this.ToolBtnClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolBtnSave
            // 
            this.ToolBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("ToolBtnSave.Image")));
            this.ToolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBtnSave.Name = "ToolBtnSave";
            this.ToolBtnSave.Size = new System.Drawing.Size(52, 22);
            this.ToolBtnSave.Text = "保存";
            this.ToolBtnSave.Click += new System.EventHandler(this.ToolBtnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolBtnExit
            // 
            this.ToolBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("ToolBtnExit.Image")));
            this.ToolBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBtnExit.Name = "ToolBtnExit";
            this.ToolBtnExit.Size = new System.Drawing.Size(52, 22);
            this.ToolBtnExit.Text = "退出";
            this.ToolBtnExit.Click += new System.EventHandler(this.ToolBtnExit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 18);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(605, 422);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 17;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "程序集";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "描述信息";
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 447);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormStart";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "系统启动项设置";
            this.Text = "系统启动项设置";
            this.Load += new System.EventHandler(this.FormStart_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ToolBtnClear;
        private System.Windows.Forms.ToolStripButton ToolBtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolBtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolBtnExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton ToolBtnAdd;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;

    }
}