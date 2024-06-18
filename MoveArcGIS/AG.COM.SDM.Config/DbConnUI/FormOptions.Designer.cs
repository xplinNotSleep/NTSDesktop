namespace AG.COM.SDM.Config
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSDE = new System.Windows.Forms.TabPage();
            this.ctlSDE = new AG.COM.SDM.Config.SpatialDbConfig();
            this.tabOA = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvwOleDbSet = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDataType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDataBase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDelOle = new System.Windows.Forms.Button();
            this.btnEditOle = new System.Windows.Forms.Button();
            this.btnAddOle = new System.Windows.Forms.Button();
            this.tabMinio = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBucketName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinioSecret = new System.Windows.Forms.TextBox();
            this.txtMinioAccess = new System.Windows.Forms.TextBox();
            this.txtMinioURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabStyle = new System.Windows.Forms.TabPage();
            this.btnStyle = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.listStyleFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StyleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSymbols = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.txtAccess = new System.Windows.Forms.TextBox();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabSDE.SuspendLayout();
            this.tabOA.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabMinio.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabStyle.SuspendLayout();
            this.StyleMenu.SuspendLayout();
            this.tabSymbols.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSDE);
            this.tabControl1.Controls.Add(this.tabOA);
            this.tabControl1.Controls.Add(this.tabMinio);
            this.tabControl1.Controls.Add(this.tabStyle);
            this.tabControl1.Controls.Add(this.tabSymbols);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(460, 412);
            this.tabControl1.TabIndex = 1;
            // 
            // tabSDE
            // 
            this.tabSDE.Controls.Add(this.ctlSDE);
            this.tabSDE.Location = new System.Drawing.Point(4, 25);
            this.tabSDE.Margin = new System.Windows.Forms.Padding(4);
            this.tabSDE.Name = "tabSDE";
            this.tabSDE.Padding = new System.Windows.Forms.Padding(4);
            this.tabSDE.Size = new System.Drawing.Size(452, 383);
            this.tabSDE.TabIndex = 1;
            this.tabSDE.Text = "SDE数据库";
            this.tabSDE.UseVisualStyleBackColor = true;
            // 
            // ctlSDE
            // 
            //this.ctlSDE.DataBase = "SDE";
            //this.ctlSDE.Instance = "5151";
            this.ctlSDE.Location = new System.Drawing.Point(8, 8);
            this.ctlSDE.Margin = new System.Windows.Forms.Padding(5);
            this.ctlSDE.Name = "ctlSDE";
            this.ctlSDE.Password = "";
            this.ctlSDE.Server = "";
            this.ctlSDE.Size = new System.Drawing.Size(427, 366);
            this.ctlSDE.TabIndex = 0;
            //this.ctlSDE.User = "SDE";
            this.ctlSDE.Version = "SDE.DEFAULT";
            this.ctlSDE.Load += new System.EventHandler(this.ctlSpatial_Load);
            // 
            // tabOA
            // 
            this.tabOA.Controls.Add(this.groupBox3);
            this.tabOA.Location = new System.Drawing.Point(4, 25);
            this.tabOA.Margin = new System.Windows.Forms.Padding(4);
            this.tabOA.Name = "tabOA";
            this.tabOA.Size = new System.Drawing.Size(452, 383);
            this.tabOA.TabIndex = 3;
            this.tabOA.Text = "属性数据库";
            this.tabOA.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvwOleDbSet);
            this.groupBox3.Controls.Add(this.btnDelOle);
            this.groupBox3.Controls.Add(this.btnEditOle);
            this.groupBox3.Controls.Add(this.btnAddOle);
            this.groupBox3.Location = new System.Drawing.Point(13, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(421, 348);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "属性数据库设置";
            // 
            // lvwOleDbSet
            // 
            this.lvwOleDbSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDataType,
            this.columnServer,
            this.columnPort,
            this.columnDataBase,
            this.columnUser,
            this.columnPassword});
            this.lvwOleDbSet.FullRowSelect = true;
            this.lvwOleDbSet.GridLines = true;
            this.lvwOleDbSet.HideSelection = false;
            this.lvwOleDbSet.Location = new System.Drawing.Point(16, 25);
            this.lvwOleDbSet.Margin = new System.Windows.Forms.Padding(4);
            this.lvwOleDbSet.MultiSelect = false;
            this.lvwOleDbSet.Name = "lvwOleDbSet";
            this.lvwOleDbSet.Size = new System.Drawing.Size(388, 276);
            this.lvwOleDbSet.TabIndex = 15;
            this.lvwOleDbSet.UseCompatibleStateImageBehavior = false;
            this.lvwOleDbSet.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "标识";
            this.columnName.Width = 84;
            // 
            // columnDataType
            // 
            this.columnDataType.Text = "数据库类型";
            this.columnDataType.Width = 76;
            // 
            // columnServer
            // 
            this.columnServer.Text = "服务器名";
            this.columnServer.Width = 104;
            // 
            // columnPort
            // 
            this.columnPort.Text = "端口";
            this.columnPort.Width = 42;
            // 
            // columnDataBase
            // 
            this.columnDataBase.Text = "数据库";
            this.columnDataBase.Width = 51;
            // 
            // columnUser
            // 
            this.columnUser.Text = "用户名";
            // 
            // columnPassword
            // 
            this.columnPassword.Text = "密码";
            // 
            // btnDelOle
            // 
            this.btnDelOle.Location = new System.Drawing.Point(195, 309);
            this.btnDelOle.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelOle.Name = "btnDelOle";
            this.btnDelOle.Size = new System.Drawing.Size(76, 30);
            this.btnDelOle.TabIndex = 16;
            this.btnDelOle.Text = "删除";
            this.btnDelOle.UseVisualStyleBackColor = true;
            this.btnDelOle.Click += new System.EventHandler(this.btnDelOle_Click);
            // 
            // btnEditOle
            // 
            this.btnEditOle.Location = new System.Drawing.Point(111, 309);
            this.btnEditOle.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditOle.Name = "btnEditOle";
            this.btnEditOle.Size = new System.Drawing.Size(76, 30);
            this.btnEditOle.TabIndex = 16;
            this.btnEditOle.Text = "编辑";
            this.btnEditOle.UseVisualStyleBackColor = true;
            this.btnEditOle.Click += new System.EventHandler(this.btnEditOle_Click);
            // 
            // btnAddOle
            // 
            this.btnAddOle.Location = new System.Drawing.Point(27, 309);
            this.btnAddOle.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddOle.Name = "btnAddOle";
            this.btnAddOle.Size = new System.Drawing.Size(76, 30);
            this.btnAddOle.TabIndex = 16;
            this.btnAddOle.Text = "增加";
            this.btnAddOle.UseVisualStyleBackColor = true;
            this.btnAddOle.Click += new System.EventHandler(this.btnAddOle_Click);
            // 
            // tabMinio
            // 
            this.tabMinio.Controls.Add(this.groupBox4);
            this.tabMinio.Location = new System.Drawing.Point(4, 25);
            this.tabMinio.Margin = new System.Windows.Forms.Padding(4);
            this.tabMinio.Name = "tabMinio";
            this.tabMinio.Padding = new System.Windows.Forms.Padding(4);
            this.tabMinio.Size = new System.Drawing.Size(452, 383);
            this.tabMinio.TabIndex = 4;
            this.tabMinio.Text = "Minio配置";
            this.tabMinio.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBucketName);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtMinioSecret);
            this.groupBox4.Controls.Add(this.txtMinioAccess);
            this.groupBox4.Controls.Add(this.txtMinioURL);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(8, 8);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(433, 365);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Minio服务器配置参数";
            // 
            // txtBucketName
            // 
            this.txtBucketName.Location = new System.Drawing.Point(110, 314);
            this.txtBucketName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBucketName.Name = "txtBucketName";
            this.txtBucketName.Size = new System.Drawing.Size(309, 25);
            this.txtBucketName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 318);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "桶名称：";
            // 
            // txtMinioSecret
            // 
            this.txtMinioSecret.Location = new System.Drawing.Point(84, 224);
            this.txtMinioSecret.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinioSecret.Name = "txtMinioSecret";
            this.txtMinioSecret.Size = new System.Drawing.Size(335, 25);
            this.txtMinioSecret.TabIndex = 18;
            // 
            // txtMinioAccess
            // 
            this.txtMinioAccess.Location = new System.Drawing.Point(124, 132);
            this.txtMinioAccess.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinioAccess.Name = "txtMinioAccess";
            this.txtMinioAccess.Size = new System.Drawing.Size(295, 25);
            this.txtMinioAccess.TabIndex = 17;
            // 
            // txtMinioURL
            // 
            this.txtMinioURL.Location = new System.Drawing.Point(124, 42);
            this.txtMinioURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinioURL.Name = "txtMinioURL";
            this.txtMinioURL.Size = new System.Drawing.Size(295, 25);
            this.txtMinioURL.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 228);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "账户名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "服务器url：";
            // 
            // tabStyle
            // 
            this.tabStyle.Controls.Add(this.btnStyle);
            this.tabStyle.Controls.Add(this.label6);
            this.tabStyle.Controls.Add(this.listStyleFiles);
            this.tabStyle.Location = new System.Drawing.Point(4, 25);
            this.tabStyle.Margin = new System.Windows.Forms.Padding(4);
            this.tabStyle.Name = "tabStyle";
            this.tabStyle.Size = new System.Drawing.Size(452, 383);
            this.tabStyle.TabIndex = 2;
            this.tabStyle.Text = "样式文件";
            this.tabStyle.ToolTipText = "样式文件";
            this.tabStyle.UseVisualStyleBackColor = true;
            // 
            // btnStyle
            // 
            this.btnStyle.Image = ((System.Drawing.Image)(resources.GetObject("btnStyle.Image")));
            this.btnStyle.Location = new System.Drawing.Point(395, 21);
            this.btnStyle.Margin = new System.Windows.Forms.Padding(4);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new System.Drawing.Size(32, 30);
            this.btnStyle.TabIndex = 3;
            this.btnStyle.UseVisualStyleBackColor = true;
            this.btnStyle.Click += new System.EventHandler(this.btnStyle_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 29);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "选择样式文件";
            // 
            // listStyleFiles
            // 
            this.listStyleFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listStyleFiles.ContextMenuStrip = this.StyleMenu;
            this.listStyleFiles.FullRowSelect = true;
            this.listStyleFiles.GridLines = true;
            this.listStyleFiles.HideSelection = false;
            this.listStyleFiles.Location = new System.Drawing.Point(23, 64);
            this.listStyleFiles.Margin = new System.Windows.Forms.Padding(4);
            this.listStyleFiles.Name = "listStyleFiles";
            this.listStyleFiles.Size = new System.Drawing.Size(403, 300);
            this.listStyleFiles.TabIndex = 0;
            this.listStyleFiles.UseCompatibleStateImageBehavior = false;
            this.listStyleFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "文件路径";
            this.columnHeader2.Width = 300;
            // 
            // StyleMenu
            // 
            this.StyleMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StyleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenuItem});
            this.StyleMenu.Name = "StyleMenu";
            this.StyleMenu.Size = new System.Drawing.Size(109, 28);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(108, 24);
            this.DeleteMenuItem.Text = "移除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // tabSymbols
            // 
            this.tabSymbols.Controls.Add(this.groupBox2);
            this.tabSymbols.Controls.Add(this.groupBox1);
            this.tabSymbols.Location = new System.Drawing.Point(4, 25);
            this.tabSymbols.Margin = new System.Windows.Forms.Padding(4);
            this.tabSymbols.Name = "tabSymbols";
            this.tabSymbols.Padding = new System.Windows.Forms.Padding(4);
            this.tabSymbols.Size = new System.Drawing.Size(452, 383);
            this.tabSymbols.TabIndex = 0;
            this.tabSymbols.Text = "符号";
            this.tabSymbols.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(8, 115);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(432, 98);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "捕捉符号";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(432, 95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "绘制符号";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(365, 452);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(93, 30);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(264, 452);
            this.btOK.Margin = new System.Windows.Forms.Padding(4);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(93, 30);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // txtAccess
            // 
            this.txtAccess.Location = new System.Drawing.Point(93, 109);
            this.txtAccess.Name = "txtAccess";
            this.txtAccess.Size = new System.Drawing.Size(222, 25);
            this.txtAccess.TabIndex = 17;
            this.txtAccess.Visible = false;
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(63, 184);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(252, 25);
            this.txtSecret.TabIndex = 18;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 496);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSDE.ResumeLayout(false);
            this.tabOA.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabMinio.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabStyle.ResumeLayout(false);
            this.tabStyle.PerformLayout();
            this.StyleMenu.ResumeLayout(false);
            this.tabSymbols.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSymbols;
        private System.Windows.Forms.TabPage tabSDE;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TabPage tabStyle;
        private AG.COM.SDM.Config.SpatialDbConfig ctlSDE;
        private System.Windows.Forms.Button btnStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listStyleFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabOA;
        private System.Windows.Forms.ContextMenuStrip StyleMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvwOleDbSet;
        private System.Windows.Forms.ColumnHeader columnDataType;
        private System.Windows.Forms.ColumnHeader columnServer;
        private System.Windows.Forms.ColumnHeader columnPort;
        private System.Windows.Forms.ColumnHeader columnDataBase;
        private System.Windows.Forms.ColumnHeader columnUser;
        private System.Windows.Forms.ColumnHeader columnPassword;
        private System.Windows.Forms.Button btnDelOle;
        private System.Windows.Forms.Button btnEditOle;
        private System.Windows.Forms.Button btnAddOle;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.TabPage tabMinio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtMinioSecret;
        private System.Windows.Forms.TextBox txtMinioAccess;
        private System.Windows.Forms.TextBox txtMinioURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccess;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.TextBox txtBucketName;
        private System.Windows.Forms.Label label1;
    }
}

