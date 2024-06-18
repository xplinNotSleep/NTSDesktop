namespace AG.COM.SDM.DataTools.Manager
{
    partial class FormCreateRasterDataSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateRasterDataSet));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkAdvanced = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCellSize = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btSelectSpatialRef = new System.Windows.Forms.Button();
            this.txtSpatialRef = new System.Windows.Forms.TextBox();
            this.nudBands = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxPixel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lstRaster = new System.Windows.Forms.ListBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gboxAdvance = new System.Windows.Forms.GroupBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nudCompression = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxCompression = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPyramidLevel = new System.Windows.Forms.NumericUpDown();
            this.cbxPyramidResample = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkPyramid = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBands)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gboxAdvance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompression)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPyramidLevel)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.delToolStripMenuItem.Text = "删除";
            this.delToolStripMenuItem.Click += new System.EventHandler(this.delToolStripMenuItem_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(274, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "退出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(189, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "创建";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(356, 415);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkAdvanced);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(348, 390);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本选项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkAdvanced
            // 
            this.chkAdvanced.AutoSize = true;
            this.chkAdvanced.Location = new System.Drawing.Point(10, 360);
            this.chkAdvanced.Name = "chkAdvanced";
            this.chkAdvanced.Size = new System.Drawing.Size(96, 16);
            this.chkAdvanced.TabIndex = 75;
            this.chkAdvanced.Text = "高级选项设置";
            this.chkAdvanced.UseVisualStyleBackColor = true;
            this.chkAdvanced.CheckedChanged += new System.EventHandler(this.chkAdvanced_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCellSize);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.btSelectSpatialRef);
            this.groupBox3.Controls.Add(this.txtSpatialRef);
            this.groupBox3.Controls.Add(this.nudBands);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cbxPixel);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Location = new System.Drawing.Point(7, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 184);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "属性设置";
            // 
            // txtCellSize
            // 
            this.txtCellSize.Location = new System.Drawing.Point(178, 106);
            this.txtCellSize.Name = "txtCellSize";
            this.txtCellSize.Size = new System.Drawing.Size(150, 21);
            this.txtCellSize.TabIndex = 78;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 12);
            this.label17.TabIndex = 77;
            this.label17.Text = "单元大小(可选)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 136);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 76;
            this.label16.Text = "空间参考";
            // 
            // btSelectSpatialRef
            // 
            this.btSelectSpatialRef.Image = ((System.Drawing.Image)(resources.GetObject("btSelectSpatialRef.Image")));
            this.btSelectSpatialRef.Location = new System.Drawing.Point(303, 150);
            this.btSelectSpatialRef.Name = "btSelectSpatialRef";
            this.btSelectSpatialRef.Size = new System.Drawing.Size(23, 23);
            this.btSelectSpatialRef.TabIndex = 75;
            this.btSelectSpatialRef.UseVisualStyleBackColor = true;
            this.btSelectSpatialRef.Click += new System.EventHandler(this.btSelectSpatialRef_Click);
            // 
            // txtSpatialRef
            // 
            this.txtSpatialRef.Location = new System.Drawing.Point(12, 151);
            this.txtSpatialRef.Name = "txtSpatialRef";
            this.txtSpatialRef.ReadOnly = true;
            this.txtSpatialRef.Size = new System.Drawing.Size(280, 21);
            this.txtSpatialRef.TabIndex = 74;
            // 
            // nudBands
            // 
            this.nudBands.Location = new System.Drawing.Point(178, 80);
            this.nudBands.Name = "nudBands";
            this.nudBands.Size = new System.Drawing.Size(150, 21);
            this.nudBands.TabIndex = 72;
            this.nudBands.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 71;
            this.label3.Text = "波段数量";
            // 
            // cbxPixel
            // 
            this.cbxPixel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPixel.FormattingEnabled = true;
            this.cbxPixel.Location = new System.Drawing.Point(178, 54);
            this.cbxPixel.Name = "cbxPixel";
            this.cbxPixel.Size = new System.Drawing.Size(150, 20);
            this.cbxPixel.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 69;
            this.label2.Text = "象素";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 68;
            this.label1.Text = "创建的栅格数据集";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(178, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 21);
            this.txtName.TabIndex = 67;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.lstRaster);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.txtPath);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(7, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 158);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "基本选项";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 61;
            this.label15.Text = "已有栅格数据集";
            // 
            // lstRaster
            // 
            this.lstRaster.FormattingEnabled = true;
            this.lstRaster.ItemHeight = 12;
            this.lstRaster.Location = new System.Drawing.Point(8, 68);
            this.lstRaster.Name = "lstRaster";
            this.lstRaster.ScrollAlwaysVisible = true;
            this.lstRaster.Size = new System.Drawing.Size(319, 64);
            this.lstRaster.TabIndex = 60;
            this.lstRaster.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseUp);
            // 
            // btnOpen
            // 
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.Location = new System.Drawing.Point(302, 17);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(27, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(45, 17);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(254, 21);
            this.txtPath.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 57;
            this.label14.Text = "路径";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gboxAdvance);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(348, 390);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "高级选项";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gboxAdvance
            // 
            this.gboxAdvance.Controls.Add(this.txtY);
            this.gboxAdvance.Controls.Add(this.txtX);
            this.gboxAdvance.Controls.Add(this.label13);
            this.gboxAdvance.Controls.Add(this.label12);
            this.gboxAdvance.Controls.Add(this.label11);
            this.gboxAdvance.Controls.Add(this.nudCompression);
            this.gboxAdvance.Controls.Add(this.label10);
            this.gboxAdvance.Controls.Add(this.cbxCompression);
            this.gboxAdvance.Controls.Add(this.label9);
            this.gboxAdvance.Controls.Add(this.txtHeight);
            this.gboxAdvance.Controls.Add(this.txtWidth);
            this.gboxAdvance.Controls.Add(this.label8);
            this.gboxAdvance.Controls.Add(this.label7);
            this.gboxAdvance.Controls.Add(this.label6);
            this.gboxAdvance.Controls.Add(this.nudPyramidLevel);
            this.gboxAdvance.Controls.Add(this.cbxPyramidResample);
            this.gboxAdvance.Controls.Add(this.label5);
            this.gboxAdvance.Controls.Add(this.label4);
            this.gboxAdvance.Controls.Add(this.chkPyramid);
            this.gboxAdvance.Enabled = false;
            this.gboxAdvance.Location = new System.Drawing.Point(6, 7);
            this.gboxAdvance.Name = "gboxAdvance";
            this.gboxAdvance.Size = new System.Drawing.Size(334, 235);
            this.gboxAdvance.TabIndex = 13;
            this.gboxAdvance.TabStop = false;
            this.gboxAdvance.Text = "高级选项";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(247, 91);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(69, 21);
            this.txtY.TabIndex = 19;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(131, 91);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(69, 21);
            this.txtX.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(229, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "Y";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(114, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "X";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "金字塔参考坐标";
            // 
            // nudCompression
            // 
            this.nudCompression.Enabled = false;
            this.nudCompression.Location = new System.Drawing.Point(120, 201);
            this.nudCompression.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCompression.Name = "nudCompression";
            this.nudCompression.Size = new System.Drawing.Size(196, 21);
            this.nudCompression.TabIndex = 14;
            this.nudCompression.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "压缩质量（1-100）";
            // 
            // cbxCompression
            // 
            this.cbxCompression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompression.FormattingEnabled = true;
            this.cbxCompression.Location = new System.Drawing.Point(120, 171);
            this.cbxCompression.Name = "cbxCompression";
            this.cbxCompression.Size = new System.Drawing.Size(196, 20);
            this.cbxCompression.TabIndex = 12;
            this.cbxCompression.SelectedIndexChanged += new System.EventHandler(this.cbxCompression_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "压缩方式";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(247, 137);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(67, 21);
            this.txtHeight.TabIndex = 10;
            this.txtHeight.Text = "128";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(32, 137);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(67, 21);
            this.txtWidth.TabIndex = 9;
            this.txtWidth.Text = "128";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(221, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "高";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "宽";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "马赛克瓷砖大小";
            // 
            // nudPyramidLevel
            // 
            this.nudPyramidLevel.Location = new System.Drawing.Point(120, 41);
            this.nudPyramidLevel.Name = "nudPyramidLevel";
            this.nudPyramidLevel.Size = new System.Drawing.Size(196, 21);
            this.nudPyramidLevel.TabIndex = 5;
            this.nudPyramidLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbxPyramidResample
            // 
            this.cbxPyramidResample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPyramidResample.FormattingEnabled = true;
            this.cbxPyramidResample.Location = new System.Drawing.Point(120, 65);
            this.cbxPyramidResample.Name = "cbxPyramidResample";
            this.cbxPyramidResample.Size = new System.Drawing.Size(196, 20);
            this.cbxPyramidResample.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "金字塔重采样技术";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "金字塔级别";
            // 
            // chkPyramid
            // 
            this.chkPyramid.AutoSize = true;
            this.chkPyramid.Checked = true;
            this.chkPyramid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPyramid.Location = new System.Drawing.Point(15, 20);
            this.chkPyramid.Name = "chkPyramid";
            this.chkPyramid.Size = new System.Drawing.Size(84, 16);
            this.chkPyramid.TabIndex = 0;
            this.chkPyramid.Text = "创建金字塔";
            this.chkPyramid.UseVisualStyleBackColor = true;
            this.chkPyramid.CheckedChanged += new System.EventHandler(this.chkPyramid_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 30);
            this.panel1.TabIndex = 51;
            // 
            // FormCreateRasterDataSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 445);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateRasterDataSet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建栅格数据";
            this.Load += new System.EventHandler(this.frmCRasterDataSet_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBands)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gboxAdvance.ResumeLayout(false);
            this.gboxAdvance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompression)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPyramidLevel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gboxAdvance;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudCompression;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxCompression;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPyramidLevel;
        private System.Windows.Forms.ComboBox cbxPyramidResample;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkPyramid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCellSize;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btSelectSpatialRef;
        private System.Windows.Forms.TextBox txtSpatialRef;
        private System.Windows.Forms.NumericUpDown nudBands;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxPixel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox lstRaster;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAdvanced;
    }
}