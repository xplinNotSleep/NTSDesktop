namespace AG.COM.SDM.DataTools.DataProcess
{
    partial class FormXZQFFExportCAD
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXZQFFExportCAD));
            this.grpSelectExtent = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lstAllExtent = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nudBufferSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ltvLayer = new AG.COM.SDM.Utility.Controls.LayersTreeView();
            this.grpSelectType = new System.Windows.Forms.GroupBox();
            this.rdoFromList = new System.Windows.Forms.RadioButton();
            this.rdoFromMap = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbCADVersion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkExportElement = new System.Windows.Forms.CheckBox();
            this.cmbCADTemplate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFenFu = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.grpSelectExtent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpSelectType.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSelectExtent
            // 
            this.grpSelectExtent.Controls.Add(this.txtSearch);
            this.grpSelectExtent.Controls.Add(this.lstAllExtent);
            this.grpSelectExtent.Location = new System.Drawing.Point(12, 61);
            this.grpSelectExtent.Name = "grpSelectExtent";
            this.grpSelectExtent.Size = new System.Drawing.Size(246, 290);
            this.grpSelectExtent.TabIndex = 1;
            this.grpSelectExtent.TabStop = false;
            this.grpSelectExtent.Text = "导出范围";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(14, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(218, 21);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lstAllExtent
            // 
            this.lstAllExtent.FormattingEnabled = true;
            this.lstAllExtent.ItemHeight = 12;
            this.lstAllExtent.Location = new System.Drawing.Point(14, 48);
            this.lstAllExtent.Name = "lstAllExtent";
            this.lstAllExtent.Size = new System.Drawing.Size(218, 232);
            this.lstAllExtent.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(401, 461);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(482, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nudBufferSize
            // 
            this.nudBufferSize.DecimalPlaces = 2;
            this.nudBufferSize.Location = new System.Drawing.Point(90, 20);
            this.nudBufferSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBufferSize.Name = "nudBufferSize";
            this.nudBufferSize.Size = new System.Drawing.Size(91, 21);
            this.nudBufferSize.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "缓冲大小：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ltvLayer);
            this.groupBox1.Location = new System.Drawing.Point(264, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 282);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导出图层";
            // 
            // ltvLayer
            // 
            this.ltvLayer.CheckBoxes = true;
            this.ltvLayer.ImageIndex = 0;
            this.ltvLayer.ItemHeight = 20;
            this.ltvLayer.Location = new System.Drawing.Point(15, 19);
            this.ltvLayer.Name = "ltvLayer";
            this.ltvLayer.SelectedImageIndex = 0;
            this.ltvLayer.Size = new System.Drawing.Size(287, 257);
            this.ltvLayer.TabIndex = 7;
            // 
            // grpSelectType
            // 
            this.grpSelectType.Controls.Add(this.rdoFromList);
            this.grpSelectType.Controls.Add(this.rdoFromMap);
            this.grpSelectType.Location = new System.Drawing.Point(12, 12);
            this.grpSelectType.Name = "grpSelectType";
            this.grpSelectType.Size = new System.Drawing.Size(246, 43);
            this.grpSelectType.TabIndex = 21;
            this.grpSelectType.TabStop = false;
            this.grpSelectType.Text = "范围选择方式";
            // 
            // rdoFromList
            // 
            this.rdoFromList.AutoSize = true;
            this.rdoFromList.Checked = true;
            this.rdoFromList.Location = new System.Drawing.Point(36, 19);
            this.rdoFromList.Name = "rdoFromList";
            this.rdoFromList.Size = new System.Drawing.Size(83, 16);
            this.rdoFromList.TabIndex = 21;
            this.rdoFromList.TabStop = true;
            this.rdoFromList.Text = "从列表选择";
            this.rdoFromList.UseVisualStyleBackColor = true;
            // 
            // rdoFromMap
            // 
            this.rdoFromMap.AutoSize = true;
            this.rdoFromMap.Location = new System.Drawing.Point(127, 19);
            this.rdoFromMap.Name = "rdoFromMap";
            this.rdoFromMap.Size = new System.Drawing.Size(83, 16);
            this.rdoFromMap.TabIndex = 22;
            this.rdoFromMap.Text = "在地图选择";
            this.rdoFromMap.UseVisualStyleBackColor = true;
            this.rdoFromMap.Click += new System.EventHandler(this.rdoFromMap_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "（米）";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.nudBufferSize);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(12, 406);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(246, 49);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "缓冲范围设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbCADVersion);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkExportElement);
            this.groupBox2.Controls.Add(this.cmbCADTemplate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnOutput);
            this.groupBox2.Controls.Add(this.txtOutput);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(264, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 155);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导出CAD设置";
            // 
            // cmbCADVersion
            // 
            this.cmbCADVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCADVersion.FormattingEnabled = true;
            this.cmbCADVersion.Location = new System.Drawing.Point(7, 123);
            this.cmbCADVersion.Name = "cmbCADVersion";
            this.cmbCADVersion.Size = new System.Drawing.Size(164, 20);
            this.cmbCADVersion.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "导出CAD版本：";
            // 
            // chkExportElement
            // 
            this.chkExportElement.AutoSize = true;
            this.chkExportElement.Location = new System.Drawing.Point(177, 125);
            this.chkExportElement.Name = "chkExportElement";
            this.chkExportElement.Size = new System.Drawing.Size(96, 16);
            this.chkExportElement.TabIndex = 23;
            this.chkExportElement.Text = "导出临时元素";
            this.chkExportElement.UseVisualStyleBackColor = true;
            // 
            // cmbCADTemplate
            // 
            this.cmbCADTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCADTemplate.FormattingEnabled = true;
            this.cmbCADTemplate.Location = new System.Drawing.Point(8, 83);
            this.cmbCADTemplate.Name = "cmbCADTemplate";
            this.cmbCADTemplate.Size = new System.Drawing.Size(265, 20);
            this.cmbCADTemplate.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "CAD模板：";
            // 
            // btnOutput
            // 
            this.btnOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnOutput.Image")));
            this.btnOutput.Location = new System.Drawing.Point(279, 36);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(30, 29);
            this.btnOutput.TabIndex = 22;
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(7, 41);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(266, 21);
            this.txtOutput.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "导出CAD文件位置：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkFenFu);
            this.groupBox3.Location = new System.Drawing.Point(12, 357);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(246, 43);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            // 
            // chkFenFu
            // 
            this.chkFenFu.AutoSize = true;
            this.chkFenFu.Checked = true;
            this.chkFenFu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFenFu.Location = new System.Drawing.Point(17, 17);
            this.chkFenFu.Name = "chkFenFu";
            this.chkFenFu.Size = new System.Drawing.Size(108, 16);
            this.chkFenFu.TabIndex = 0;
            this.chkFenFu.Text = "按图幅分幅打印";
            this.chkFenFu.UseVisualStyleBackColor = true;
            // 
            // FormSelectExtentExportCAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 492);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpSelectExtent);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grpSelectType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectExtentExportCAD";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "范围导出CAD";
            this.Load += new System.EventHandler(this.FormSelectExtentPrint_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSelectExtentPrint_FormClosing);
            this.grpSelectExtent.ResumeLayout(false);
            this.grpSelectExtent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grpSelectType.ResumeLayout(false);
            this.grpSelectType.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSelectExtent;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lstAllExtent;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudBufferSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpSelectType;
        private System.Windows.Forms.RadioButton rdoFromList;
        private System.Windows.Forms.RadioButton rdoFromMap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private AG.COM.SDM.Utility.Controls.LayersTreeView ltvLayer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbCADVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkExportElement;
        private System.Windows.Forms.ComboBox cmbCADTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkFenFu;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}