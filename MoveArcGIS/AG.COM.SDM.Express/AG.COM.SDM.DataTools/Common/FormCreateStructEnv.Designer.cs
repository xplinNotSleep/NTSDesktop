namespace AG.COM.SDM.DataTools.Common
{
    partial class FormCreateStructEnv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateStructEnv));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxTreeView1 = new AG.COM.SDM.Utility.Controls.ComboBoxTreeView();
            this.combSpatialRel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUnits = new System.Windows.Forms.Label();
            this.numTolerance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtShpPath = new System.Windows.Forms.TextBox();
            this.btnOutPut = new System.Windows.Forms.Button();
            this.rdioNotOutput = new System.Windows.Forms.RadioButton();
            this.rdioOutput = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdioOutput);
            this.groupBox1.Controls.Add(this.rdioNotOutput);
            this.groupBox1.Controls.Add(this.btnOutPut);
            this.groupBox1.Controls.Add(this.txtShpPath);
            this.groupBox1.Controls.Add(this.comboBoxTreeView1);
            this.groupBox1.Controls.Add(this.combSpatialRel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblUnits);
            this.groupBox1.Controls.Add(this.numTolerance);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 221);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // comboBoxTreeView1
            // 
            this.comboBoxTreeView1.FormattingEnabled = true;
            this.comboBoxTreeView1.Location = new System.Drawing.Point(15, 41);
            this.comboBoxTreeView1.Name = "comboBoxTreeView1";
            this.comboBoxTreeView1.Size = new System.Drawing.Size(256, 20);
            this.comboBoxTreeView1.TabIndex = 12;
            this.comboBoxTreeView1.SelectedValueChanged += new System.EventHandler(this.comboBoxTreeView1_SelectedValueChanged);
            // 
            // combSpatialRel
            // 
            this.combSpatialRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSpatialRel.FormattingEnabled = true;
            this.combSpatialRel.Items.AddRange(new object[] {
            "相交",
            "包含",
            "被包含"});
            this.combSpatialRel.Location = new System.Drawing.Point(245, 93);
            this.combSpatialRel.Name = "combSpatialRel";
            this.combSpatialRel.Size = new System.Drawing.Size(256, 20);
            this.combSpatialRel.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "从指定图层选择集生成构造范围图层:";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Location = new System.Drawing.Point(171, 93);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(29, 12);
            this.lblUnits.TabIndex = 3;
            this.lblUnits.Text = "(米)";
            // 
            // numTolerance
            // 
            this.numTolerance.Location = new System.Drawing.Point(15, 91);
            this.numTolerance.Name = "numTolerance";
            this.numTolerance.Size = new System.Drawing.Size(150, 21);
            this.numTolerance.TabIndex = 2;
            this.numTolerance.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置咬合容差值:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "空间关系:";
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(190, 239);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 24);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(266, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "GroupLayer.bmp");
            this.imageList1.Images.SetKeyName(1, "Layer.bmp");
            // 
            // txtShpPath
            // 
            this.txtShpPath.Location = new System.Drawing.Point(34, 175);
            this.txtShpPath.Name = "txtShpPath";
            this.txtShpPath.Size = new System.Drawing.Size(238, 21);
            this.txtShpPath.TabIndex = 14;
            // 
            // btnOutPut
            // 
            this.btnOutPut.Location = new System.Drawing.Point(278, 175);
            this.btnOutPut.Name = "btnOutPut";
            this.btnOutPut.Size = new System.Drawing.Size(24, 24);
            this.btnOutPut.TabIndex = 4;
            this.btnOutPut.UseVisualStyleBackColor = true;
            this.btnOutPut.Click += new System.EventHandler(this.btnOutPut_Click);
            // 
            // rdioNotOutput
            // 
            this.rdioNotOutput.AutoSize = true;
            this.rdioNotOutput.Checked = true;
            this.rdioNotOutput.Location = new System.Drawing.Point(15, 131);
            this.rdioNotOutput.Name = "rdioNotOutput";
            this.rdioNotOutput.Size = new System.Drawing.Size(197, 16);
            this.rdioNotOutput.TabIndex = 15;
            this.rdioNotOutput.TabStop = true;
            this.rdioNotOutput.Text = "作为图层加载,但不作为文件输出";
            this.rdioNotOutput.UseVisualStyleBackColor = true;
            // 
            // rdioOutput
            // 
            this.rdioOutput.AutoSize = true;
            this.rdioOutput.Location = new System.Drawing.Point(15, 153);
            this.rdioOutput.Name = "rdioOutput";
            this.rdioOutput.Size = new System.Drawing.Size(275, 16);
            this.rdioOutput.TabIndex = 15;
            this.rdioOutput.Text = "作为图层加载,且输出构造范围图层到指定位置:";
            this.rdioOutput.UseVisualStyleBackColor = true;
            this.rdioOutput.CheckedChanged += new System.EventHandler(this.rdioOutput_CheckedChanged);
            // 
            // FormCreateStructEnv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 269);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateStructEnv";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成构造范围";
            this.Load += new System.EventHandler(this.FormCreateStructEnv_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combSpatialRel;
        private System.Windows.Forms.Label label1;
        private AG.COM.SDM.Utility.Controls.ComboBoxTreeView comboBoxTreeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnOutPut;
        private System.Windows.Forms.TextBox txtShpPath;
        private System.Windows.Forms.RadioButton rdioOutput;
        private System.Windows.Forms.RadioButton rdioNotOutput;

    }
}