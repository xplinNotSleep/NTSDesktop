namespace AG.COM.SDM.DataTools.DataProcess
{
    partial class FormRasterSlope
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckb_AddRSToMap = new System.Windows.Forms.CheckBox();
            this.txb_cellSize = new System.Windows.Forms.TextBox();
            this.btn_RasterSlope = new System.Windows.Forms.Button();
            this.cmb_RasterLayers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_FieldName = new System.Windows.Forms.Label();
            this.txb_FieldName = new System.Windows.Forms.TextBox();
            this.btn_CalcSlopeLevel = new System.Windows.Forms.Button();
            this.cmb_PolygonLayers = new System.Windows.Forms.ComboBox();
            this.pgb_Status = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_brsRCTab = new System.Windows.Forms.Button();
            this.txt_RSTablePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckb_AddCSToMap = new System.Windows.Forms.CheckBox();
            this.btn_ReClassSlope = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_saveFSPath = new System.Windows.Forms.Button();
            this.txt_FSPath = new System.Windows.Forms.TextBox();
            this.ckb_AddFSToMap = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_RS2FS = new System.Windows.Forms.Button();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckb_AddRSToMap);
            this.groupBox1.Controls.Add(this.txb_cellSize);
            this.groupBox1.Controls.Add(this.btn_RasterSlope);
            this.groupBox1.Controls.Add(this.cmb_RasterLayers);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(1, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生成栅格坡度图";
            // 
            // ckb_AddRSToMap
            // 
            this.ckb_AddRSToMap.AutoSize = true;
            this.ckb_AddRSToMap.Checked = true;
            this.ckb_AddRSToMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_AddRSToMap.Location = new System.Drawing.Point(6, 93);
            this.ckb_AddRSToMap.Name = "ckb_AddRSToMap";
            this.ckb_AddRSToMap.Size = new System.Drawing.Size(108, 16);
            this.ckb_AddRSToMap.TabIndex = 2;
            this.ckb_AddRSToMap.Text = "添加到当前地图";
            this.ckb_AddRSToMap.UseVisualStyleBackColor = true;
            // 
            // txb_cellSize
            // 
            this.txb_cellSize.Location = new System.Drawing.Point(63, 49);
            this.txb_cellSize.Name = "txb_cellSize";
            this.txb_cellSize.Size = new System.Drawing.Size(73, 21);
            this.txb_cellSize.TabIndex = 1;
            this.txb_cellSize.Text = "30";
            this.txb_cellSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_cellSize_KeyPress);
            // 
            // btn_RasterSlope
            // 
            this.btn_RasterSlope.Location = new System.Drawing.Point(227, 23);
            this.btn_RasterSlope.Name = "btn_RasterSlope";
            this.btn_RasterSlope.Size = new System.Drawing.Size(92, 23);
            this.btn_RasterSlope.TabIndex = 1;
            this.btn_RasterSlope.Text = "生   成";
            this.btn_RasterSlope.UseVisualStyleBackColor = true;
            this.btn_RasterSlope.Click += new System.EventHandler(this.btn_RasterSlope_Click);
            // 
            // cmb_RasterLayers
            // 
            this.cmb_RasterLayers.FormattingEnabled = true;
            this.cmb_RasterLayers.Location = new System.Drawing.Point(6, 23);
            this.cmb_RasterLayers.Name = "cmb_RasterLayers";
            this.cmb_RasterLayers.Size = new System.Drawing.Size(215, 20);
            this.cmb_RasterLayers.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "像元尺度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "（大于零的整数）";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_FieldName);
            this.groupBox2.Controls.Add(this.txb_FieldName);
            this.groupBox2.Controls.Add(this.btn_CalcSlopeLevel);
            this.groupBox2.Controls.Add(this.cmb_PolygonLayers);
            this.groupBox2.Location = new System.Drawing.Point(1, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "计算坡度等级";
            // 
            // lbl_FieldName
            // 
            this.lbl_FieldName.AutoSize = true;
            this.lbl_FieldName.Location = new System.Drawing.Point(6, 52);
            this.lbl_FieldName.Name = "lbl_FieldName";
            this.lbl_FieldName.Size = new System.Drawing.Size(41, 12);
            this.lbl_FieldName.TabIndex = 3;
            this.lbl_FieldName.Text = "字段名";
            // 
            // txb_FieldName
            // 
            this.txb_FieldName.Location = new System.Drawing.Point(53, 49);
            this.txb_FieldName.Name = "txb_FieldName";
            this.txb_FieldName.Size = new System.Drawing.Size(168, 21);
            this.txb_FieldName.TabIndex = 2;
            this.txb_FieldName.Text = "SlopeLevel";
            // 
            // btn_CalcSlopeLevel
            // 
            this.btn_CalcSlopeLevel.Location = new System.Drawing.Point(227, 49);
            this.btn_CalcSlopeLevel.Name = "btn_CalcSlopeLevel";
            this.btn_CalcSlopeLevel.Size = new System.Drawing.Size(95, 23);
            this.btn_CalcSlopeLevel.TabIndex = 1;
            this.btn_CalcSlopeLevel.Text = "计算坡度等级";
            this.btn_CalcSlopeLevel.UseVisualStyleBackColor = true;
            this.btn_CalcSlopeLevel.Click += new System.EventHandler(this.btn_CalcSlopeLevel_Click);
            // 
            // cmb_PolygonLayers
            // 
            this.cmb_PolygonLayers.FormattingEnabled = true;
            this.cmb_PolygonLayers.Location = new System.Drawing.Point(6, 20);
            this.cmb_PolygonLayers.Name = "cmb_PolygonLayers";
            this.cmb_PolygonLayers.Size = new System.Drawing.Size(215, 20);
            this.cmb_PolygonLayers.TabIndex = 0;
            // 
            // pgb_Status
            // 
            this.pgb_Status.Location = new System.Drawing.Point(1, 401);
            this.pgb_Status.Name = "pgb_Status";
            this.pgb_Status.Size = new System.Drawing.Size(359, 23);
            this.pgb_Status.TabIndex = 2;
            this.pgb_Status.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_brsRCTab);
            this.groupBox3.Controls.Add(this.txt_RSTablePath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.ckb_AddCSToMap);
            this.groupBox3.Controls.Add(this.btn_ReClassSlope);
            this.groupBox3.Location = new System.Drawing.Point(1, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 82);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "栅格坡度图重分类";
            // 
            // btn_brsRCTab
            // 
            this.btn_brsRCTab.Location = new System.Drawing.Point(227, 24);
            this.btn_brsRCTab.Name = "btn_brsRCTab";
            this.btn_brsRCTab.Size = new System.Drawing.Size(94, 23);
            this.btn_brsRCTab.TabIndex = 5;
            this.btn_brsRCTab.Text = "浏  览";
            this.btn_brsRCTab.UseVisualStyleBackColor = true;
            this.btn_brsRCTab.Click += new System.EventHandler(this.btn_brsRCTab_Click);
            // 
            // txt_RSTablePath
            // 
            this.txt_RSTablePath.Location = new System.Drawing.Point(65, 24);
            this.txt_RSTablePath.Name = "txt_RSTablePath";
            this.txt_RSTablePath.Size = new System.Drawing.Size(156, 21);
            this.txt_RSTablePath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "分类规则";
            // 
            // ckb_AddCSToMap
            // 
            this.ckb_AddCSToMap.AutoSize = true;
            this.ckb_AddCSToMap.Checked = true;
            this.ckb_AddCSToMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_AddCSToMap.Location = new System.Drawing.Point(6, 59);
            this.ckb_AddCSToMap.Name = "ckb_AddCSToMap";
            this.ckb_AddCSToMap.Size = new System.Drawing.Size(108, 16);
            this.ckb_AddCSToMap.TabIndex = 2;
            this.ckb_AddCSToMap.Text = "添加到当前地图";
            this.ckb_AddCSToMap.UseVisualStyleBackColor = true;
            // 
            // btn_ReClassSlope
            // 
            this.btn_ReClassSlope.Location = new System.Drawing.Point(227, 52);
            this.btn_ReClassSlope.Name = "btn_ReClassSlope";
            this.btn_ReClassSlope.Size = new System.Drawing.Size(95, 23);
            this.btn_ReClassSlope.TabIndex = 1;
            this.btn_ReClassSlope.Text = "重分类";
            this.btn_ReClassSlope.UseVisualStyleBackColor = true;
            this.btn_ReClassSlope.Click += new System.EventHandler(this.btn_ReClassSlope_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_saveFSPath);
            this.groupBox4.Controls.Add(this.txt_FSPath);
            this.groupBox4.Controls.Add(this.ckb_AddFSToMap);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btn_RS2FS);
            this.groupBox4.Location = new System.Drawing.Point(1, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(359, 81);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "坡度图矢量化";
            // 
            // btn_saveFSPath
            // 
            this.btn_saveFSPath.Location = new System.Drawing.Point(227, 16);
            this.btn_saveFSPath.Name = "btn_saveFSPath";
            this.btn_saveFSPath.Size = new System.Drawing.Size(94, 23);
            this.btn_saveFSPath.TabIndex = 5;
            this.btn_saveFSPath.Text = "浏  览";
            this.btn_saveFSPath.UseVisualStyleBackColor = true;
            this.btn_saveFSPath.Click += new System.EventHandler(this.btn_saveFSPath_Click);
            // 
            // txt_FSPath
            // 
            this.txt_FSPath.Location = new System.Drawing.Point(65, 21);
            this.txt_FSPath.Name = "txt_FSPath";
            this.txt_FSPath.Size = new System.Drawing.Size(156, 21);
            this.txt_FSPath.TabIndex = 4;
            // 
            // ckb_AddFSToMap
            // 
            this.ckb_AddFSToMap.AutoSize = true;
            this.ckb_AddFSToMap.Checked = true;
            this.ckb_AddFSToMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_AddFSToMap.Location = new System.Drawing.Point(8, 58);
            this.ckb_AddFSToMap.Name = "ckb_AddFSToMap";
            this.ckb_AddFSToMap.Size = new System.Drawing.Size(108, 16);
            this.ckb_AddFSToMap.TabIndex = 2;
            this.ckb_AddFSToMap.Text = "添加到当前地图";
            this.ckb_AddFSToMap.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "保存到";
            // 
            // btn_RS2FS
            // 
            this.btn_RS2FS.Location = new System.Drawing.Point(227, 51);
            this.btn_RS2FS.Name = "btn_RS2FS";
            this.btn_RS2FS.Size = new System.Drawing.Size(95, 23);
            this.btn_RS2FS.TabIndex = 1;
            this.btn_RS2FS.Text = "矢量化";
            this.btn_RS2FS.UseVisualStyleBackColor = true;
            this.btn_RS2FS.Click += new System.EventHandler(this.btn_RS2FS_Click);
            // 
            // FormRasterSlope
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 426);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pgb_Status);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRasterSlope";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "坡度计算";
            this.Load += new System.EventHandler(this.frmRasterSlope_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_RasterLayers;
        private System.Windows.Forms.Button btn_RasterSlope;
        private System.Windows.Forms.CheckBox ckb_AddRSToMap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_PolygonLayers;
        private System.Windows.Forms.Button btn_CalcSlopeLevel;
        private System.Windows.Forms.TextBox txb_FieldName;
        private System.Windows.Forms.Label lbl_FieldName;
        private System.Windows.Forms.ProgressBar pgb_Status;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ckb_AddCSToMap;
        private System.Windows.Forms.Button btn_ReClassSlope;
        private System.Windows.Forms.TextBox txt_RSTablePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Button btn_brsRCTab;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txb_cellSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_RS2FS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_saveFSPath;
        private System.Windows.Forms.TextBox txt_FSPath;
        private System.Windows.Forms.CheckBox ckb_AddFSToMap;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
    }
}