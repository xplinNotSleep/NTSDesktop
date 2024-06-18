namespace AG.COM.SDM.Manager
{
    partial class FormMenuConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenuConfig));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvwMenu = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpControlAttr = new System.Windows.Forms.GroupBox();
            this.chbNewGroup = new System.Windows.Forms.CheckBox();
            this.rdbIconSizeSmall = new System.Windows.Forms.RadioButton();
            this.lblIconSize = new System.Windows.Forms.Label();
            this.rdbIconSizeBig = new System.Windows.Forms.RadioButton();
            this.btnApply = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAssembly = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.btnAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddSeparator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddComboBox = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddText = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddComboBoxTreeView = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CMnuMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.CMnuMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CMnuDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSavetoDB = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadbyDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSavetoLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadbyLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMenuConfigFrom = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpControlAttr.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvwMenu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(654, 377);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 2;
            // 
            // tvwMenu
            // 
            this.tvwMenu.AllowDrop = true;
            this.tvwMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwMenu.HideSelection = false;
            this.tvwMenu.ImageIndex = 0;
            this.tvwMenu.ImageList = this.imageList1;
            this.tvwMenu.Location = new System.Drawing.Point(0, 0);
            this.tvwMenu.Name = "tvwMenu";
            this.tvwMenu.SelectedImageIndex = 0;
            this.tvwMenu.ShowNodeToolTips = true;
            this.tvwMenu.Size = new System.Drawing.Size(217, 377);
            this.tvwMenu.TabIndex = 0;
            this.tvwMenu.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvwMenu_ItemDrag);
            this.tvwMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMenu_AfterSelect);
            this.tvwMenu.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvwMenu_DragDrop);
            this.tvwMenu.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvwMenu_DragEnter);
            this.tvwMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwMenu_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Control_MenuStrip.png");
            this.imageList1.Images.SetKeyName(1, "Control_ToolBar.png");
            this.imageList1.Images.SetKeyName(2, "Control_Button.png");
            this.imageList1.Images.SetKeyName(3, "ComboBox16.png");
            this.imageList1.Images.SetKeyName(4, "Text16.png");
            this.imageList1.Images.SetKeyName(5, "CustomControl.png");
            this.imageList1.Images.SetKeyName(6, "HtmlBalanceBracesHS.png");
            this.imageList1.Images.SetKeyName(7, "RibbonPage.png");
            this.imageList1.Images.SetKeyName(8, "RibbonPageGroup.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpControlAttr);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbAssembly);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCaption);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 377);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "菜单信息";
            // 
            // grpControlAttr
            // 
            this.grpControlAttr.Controls.Add(this.chbNewGroup);
            this.grpControlAttr.Controls.Add(this.rdbIconSizeSmall);
            this.grpControlAttr.Controls.Add(this.lblIconSize);
            this.grpControlAttr.Controls.Add(this.rdbIconSizeBig);
            this.grpControlAttr.Location = new System.Drawing.Point(100, 167);
            this.grpControlAttr.Name = "grpControlAttr";
            this.grpControlAttr.Size = new System.Drawing.Size(302, 77);
            this.grpControlAttr.TabIndex = 15;
            this.grpControlAttr.TabStop = false;
            this.grpControlAttr.Text = "控件属性";
            // 
            // chbNewGroup
            // 
            this.chbNewGroup.AutoSize = true;
            this.chbNewGroup.Location = new System.Drawing.Point(23, 35);
            this.chbNewGroup.Name = "chbNewGroup";
            this.chbNewGroup.Size = new System.Drawing.Size(60, 16);
            this.chbNewGroup.TabIndex = 11;
            this.chbNewGroup.Text = "新分组";
            this.chbNewGroup.UseVisualStyleBackColor = true;
            // 
            // rdbIconSizeSmall
            // 
            this.rdbIconSizeSmall.AutoSize = true;
            this.rdbIconSizeSmall.Location = new System.Drawing.Point(243, 34);
            this.rdbIconSizeSmall.Name = "rdbIconSizeSmall";
            this.rdbIconSizeSmall.Size = new System.Drawing.Size(35, 16);
            this.rdbIconSizeSmall.TabIndex = 14;
            this.rdbIconSizeSmall.Text = "小";
            this.rdbIconSizeSmall.UseVisualStyleBackColor = true;
            // 
            // lblIconSize
            // 
            this.lblIconSize.AutoSize = true;
            this.lblIconSize.Location = new System.Drawing.Point(143, 36);
            this.lblIconSize.Name = "lblIconSize";
            this.lblIconSize.Size = new System.Drawing.Size(53, 12);
            this.lblIconSize.TabIndex = 12;
            this.lblIconSize.Text = "图标大小";
            // 
            // rdbIconSizeBig
            // 
            this.rdbIconSizeBig.AutoSize = true;
            this.rdbIconSizeBig.Checked = true;
            this.rdbIconSizeBig.Location = new System.Drawing.Point(202, 34);
            this.rdbIconSizeBig.Name = "rdbIconSizeBig";
            this.rdbIconSizeBig.Size = new System.Drawing.Size(35, 16);
            this.rdbIconSizeBig.TabIndex = 13;
            this.rdbIconSizeBig.TabStop = true;
            this.rdbIconSizeBig.Text = "大";
            this.rdbIconSizeBig.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(175, 323);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(78, 24);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(100, 132);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(302, 20);
            this.cmbType.Sorted = true;
            this.cmbType.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "插件类";
            // 
            // cmbAssembly
            // 
            this.cmbAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAssembly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssembly.FormattingEnabled = true;
            this.cmbAssembly.Location = new System.Drawing.Point(100, 95);
            this.cmbAssembly.Name = "cmbAssembly";
            this.cmbAssembly.Size = new System.Drawing.Size(302, 20);
            this.cmbAssembly.Sorted = true;
            this.cmbAssembly.TabIndex = 5;
            this.cmbAssembly.SelectedIndexChanged += new System.EventHandler(this.cmbAssembly_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "dll文件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "菜单名称";
            // 
            // txtCaption
            // 
            this.txtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaption.Location = new System.Drawing.Point(100, 55);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(302, 21);
            this.txtCaption.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddItem,
            this.btnAddSeparator,
            this.btnAddComboBox,
            this.btnAddText,
            this.btnAddComboBoxTreeView,
            this.btnAddGroup,
            this.btnAddPage,
            this.toolStripMenuItem1,
            this.btnAddToolbar,
            this.toolStripSeparator6,
            this.CMnuMoveUp,
            this.CMnuMoveDown,
            this.toolStripSeparator4,
            this.CMnuDeleteItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 264);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(136, 22);
            this.btnAddItem.Text = "新建菜单项";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnAddSeparator
            // 
            this.btnAddSeparator.Name = "btnAddSeparator";
            this.btnAddSeparator.Size = new System.Drawing.Size(136, 22);
            this.btnAddSeparator.Text = "新建分割行";
            this.btnAddSeparator.Click += new System.EventHandler(this.btnAddSeparator_Click);
            // 
            // btnAddComboBox
            // 
            this.btnAddComboBox.Name = "btnAddComboBox";
            this.btnAddComboBox.Size = new System.Drawing.Size(136, 22);
            this.btnAddComboBox.Text = "新建下拉框";
            this.btnAddComboBox.Click += new System.EventHandler(this.btnAddComboBox_Click);
            // 
            // btnAddText
            // 
            this.btnAddText.Name = "btnAddText";
            this.btnAddText.Size = new System.Drawing.Size(136, 22);
            this.btnAddText.Text = "新建文字";
            this.btnAddText.Click += new System.EventHandler(this.btnAddText_Click);
            // 
            // btnAddComboBoxTreeView
            // 
            this.btnAddComboBoxTreeView.Name = "btnAddComboBoxTreeView";
            this.btnAddComboBoxTreeView.Size = new System.Drawing.Size(136, 22);
            this.btnAddComboBoxTreeView.Text = "新建下拉树";
            this.btnAddComboBoxTreeView.Click += new System.EventHandler(this.btnAddCustom_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(136, 22);
            this.btnAddGroup.Text = "新建组";
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnAddPage
            // 
            this.btnAddPage.Name = "btnAddPage";
            this.btnAddPage.Size = new System.Drawing.Size(136, 22);
            this.btnAddPage.Text = "新建页";
            this.btnAddPage.Click += new System.EventHandler(this.btnAddPage_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // btnAddToolbar
            // 
            this.btnAddToolbar.Name = "btnAddToolbar";
            this.btnAddToolbar.Size = new System.Drawing.Size(136, 22);
            this.btnAddToolbar.Text = "新建工具条";
            this.btnAddToolbar.Click += new System.EventHandler(this.btnAddToolbar_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(133, 6);
            // 
            // CMnuMoveUp
            // 
            this.CMnuMoveUp.Name = "CMnuMoveUp";
            this.CMnuMoveUp.Size = new System.Drawing.Size(136, 22);
            this.CMnuMoveUp.Text = "上移";
            this.CMnuMoveUp.Click += new System.EventHandler(this.CMnuMoveUp_Click);
            // 
            // CMnuMoveDown
            // 
            this.CMnuMoveDown.Name = "CMnuMoveDown";
            this.CMnuMoveDown.Size = new System.Drawing.Size(136, 22);
            this.CMnuMoveDown.Text = "下移";
            this.CMnuMoveDown.Click += new System.EventHandler(this.CMnuMoveDown_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(133, 6);
            // 
            // CMnuDeleteItem
            // 
            this.CMnuDeleteItem.Name = "CMnuDeleteItem";
            this.CMnuDeleteItem.Size = new System.Drawing.Size(136, 22);
            this.CMnuDeleteItem.Text = "删除";
            this.CMnuDeleteItem.Click += new System.EventHandler(this.CMnuDeleteItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(654, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSavetoDB,
            this.btnLoadbyDB,
            this.toolStripMenuItem2,
            this.btnSavetoLocal,
            this.btnLoadbyLocal});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "菜单";
            // 
            // btnSavetoDB
            // 
            this.btnSavetoDB.Name = "btnSavetoDB";
            this.btnSavetoDB.Size = new System.Drawing.Size(160, 22);
            this.btnSavetoDB.Text = "保存到数据库";
            this.btnSavetoDB.Click += new System.EventHandler(this.btnSavetoLocalDB_Click);
            // 
            // btnLoadbyDB
            // 
            this.btnLoadbyDB.Name = "btnLoadbyDB";
            this.btnLoadbyDB.Size = new System.Drawing.Size(160, 22);
            this.btnLoadbyDB.Text = "从数据库读取";
            this.btnLoadbyDB.Click += new System.EventHandler(this.btnLoadbyLocalDB_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // btnSavetoLocal
            // 
            this.btnSavetoLocal.Name = "btnSavetoLocal";
            this.btnSavetoLocal.Size = new System.Drawing.Size(160, 22);
            this.btnSavetoLocal.Text = "保存到配置文件";
            this.btnSavetoLocal.Click += new System.EventHandler(this.btnSavetoLocal_Click);
            // 
            // btnLoadbyLocal
            // 
            this.btnLoadbyLocal.Name = "btnLoadbyLocal";
            this.btnLoadbyLocal.Size = new System.Drawing.Size(160, 22);
            this.btnLoadbyLocal.Text = "从配置文件读取";
            this.btnLoadbyLocal.Click += new System.EventHandler(this.btnLoadbyLocal_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblMenuConfigFrom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(654, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(128, 17);
            this.toolStripStatusLabel1.Text = "当前菜单配置保存在：";
            // 
            // lblMenuConfigFrom
            // 
            this.lblMenuConfigFrom.Name = "lblMenuConfigFrom";
            this.lblMenuConfigFrom.Size = new System.Drawing.Size(0, 17);
            // 
            // FormMenuConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 424);
            this.CloseButton = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMenuConfig";
            this.Text = "菜单设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenuConfig_FormClosing);
            this.Load += new System.EventHandler(this.FormMenuConfig_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpControlAttr.ResumeLayout(false);
            this.grpControlAttr.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvwMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAssembly;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnAddItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem CMnuMoveUp;
        private System.Windows.Forms.ToolStripMenuItem CMnuMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem CMnuDeleteItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnSavetoLocal;
        private System.Windows.Forms.ToolStripMenuItem btnLoadbyLocal;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblMenuConfigFrom;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem btnAddComboBox;
        private System.Windows.Forms.ToolStripMenuItem btnAddText;
        private System.Windows.Forms.ToolStripMenuItem btnAddSeparator;
        private System.Windows.Forms.ToolStripMenuItem btnAddComboBoxTreeView;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnAddToolbar;
        private System.Windows.Forms.ToolStripMenuItem btnAddGroup;
        private System.Windows.Forms.ToolStripMenuItem btnAddPage;
        private System.Windows.Forms.RadioButton rdbIconSizeSmall;
        private System.Windows.Forms.RadioButton rdbIconSizeBig;
        private System.Windows.Forms.Label lblIconSize;
        private System.Windows.Forms.CheckBox chbNewGroup;
        private System.Windows.Forms.GroupBox grpControlAttr;
        private System.Windows.Forms.ToolStripMenuItem btnSavetoDB;
        private System.Windows.Forms.ToolStripMenuItem btnLoadbyDB;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}