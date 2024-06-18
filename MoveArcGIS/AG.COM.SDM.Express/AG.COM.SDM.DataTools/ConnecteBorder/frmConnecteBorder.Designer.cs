namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    partial class frmConnecteBorder
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLayer = new System.Windows.Forms.TextBox();
            this.numBuffer = new System.Windows.Forms.NumericUpDown();
            this.tbOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btCacel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJTB = new System.Windows.Forms.TextBox();
            this.tbOpenJTB = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btRomoveOne = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbNeedFields = new System.Windows.Forms.ListBox();
            this.lbNotNeedFields = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btRomoveAll = new System.Windows.Forms.Button();
            this.btAddAll = new System.Windows.Forms.Button();
            this.btAddOne = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numBuffer)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "要接边处理的图层";
            // 
            // txtLayer
            // 
            this.txtLayer.Location = new System.Drawing.Point(10, 31);
            this.txtLayer.Name = "txtLayer";
            this.txtLayer.Size = new System.Drawing.Size(251, 21);
            this.txtLayer.TabIndex = 1;
            // 
            // numBuffer
            // 
            this.numBuffer.DecimalPlaces = 2;
            this.numBuffer.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numBuffer.Location = new System.Drawing.Point(9, 121);
            this.numBuffer.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numBuffer.Name = "numBuffer";
            this.numBuffer.Size = new System.Drawing.Size(120, 21);
            this.numBuffer.TabIndex = 2;
            this.numBuffer.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // tbOpen
            // 
            this.tbOpen.Location = new System.Drawing.Point(263, 30);
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(23, 21);
            this.tbOpen.TabIndex = 3;
            this.tbOpen.UseVisualStyleBackColor = true;
            this.tbOpen.Click += new System.EventHandler(this.tbOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "允许的容差";
            // 
            // btCacel
            // 
            this.btCacel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCacel.Location = new System.Drawing.Point(226, 319);
            this.btCacel.Name = "btCacel";
            this.btCacel.Size = new System.Drawing.Size(70, 24);
            this.btCacel.TabIndex = 5;
            this.btCacel.Text = "取　消";
            this.btCacel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(149, 319);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(70, 24);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "确　定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtJTB);
            this.groupBox1.Controls.Add(this.tbOpenJTB);
            this.groupBox1.Controls.Add(this.numBuffer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLayer);
            this.groupBox1.Controls.Add(this.tbOpen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 148);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "接图表";
            // 
            // txtJTB
            // 
            this.txtJTB.Location = new System.Drawing.Point(10, 73);
            this.txtJTB.Name = "txtJTB";
            this.txtJTB.Size = new System.Drawing.Size(251, 21);
            this.txtJTB.TabIndex = 6;
            // 
            // tbOpenJTB
            // 
            this.tbOpenJTB.Location = new System.Drawing.Point(263, 72);
            this.tbOpenJTB.Name = "tbOpenJTB";
            this.tbOpenJTB.Size = new System.Drawing.Size(23, 21);
            this.tbOpenJTB.TabIndex = 7;
            this.tbOpenJTB.UseVisualStyleBackColor = true;
            this.tbOpenJTB.Click += new System.EventHandler(this.tbOpenJTB_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btRomoveOne);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbNeedFields);
            this.groupBox2.Controls.Add(this.lbNotNeedFields);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btRomoveAll);
            this.groupBox2.Controls.Add(this.btAddAll);
            this.groupBox2.Controls.Add(this.btAddOne);
            this.groupBox2.Location = new System.Drawing.Point(2, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 159);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字段设置";
            // 
            // btRomoveOne
            // 
            this.btRomoveOne.Location = new System.Drawing.Point(130, 92);
            this.btRomoveOne.Name = "btRomoveOne";
            this.btRomoveOne.Size = new System.Drawing.Size(35, 23);
            this.btRomoveOne.TabIndex = 18;
            this.btRomoveOne.Text = "<-";
            this.btRomoveOne.UseVisualStyleBackColor = true;
            this.btRomoveOne.Click += new System.EventHandler(this.btRomoveOne_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "不需要的字段";
            // 
            // lbNeedFields
            // 
            this.lbNeedFields.FormattingEnabled = true;
            this.lbNeedFields.ItemHeight = 12;
            this.lbNeedFields.Location = new System.Drawing.Point(173, 35);
            this.lbNeedFields.Name = "lbNeedFields";
            this.lbNeedFields.Size = new System.Drawing.Size(112, 112);
            this.lbNeedFields.TabIndex = 14;
            this.lbNeedFields.DoubleClick += new System.EventHandler(this.lbNeedFields_DoubleClick);
            // 
            // lbNotNeedFields
            // 
            this.lbNotNeedFields.FormattingEnabled = true;
            this.lbNotNeedFields.ItemHeight = 12;
            this.lbNotNeedFields.Location = new System.Drawing.Point(10, 35);
            this.lbNotNeedFields.Name = "lbNotNeedFields";
            this.lbNotNeedFields.Size = new System.Drawing.Size(112, 112);
            this.lbNotNeedFields.TabIndex = 20;
            this.lbNotNeedFields.DoubleClick += new System.EventHandler(this.lbNotNeedFields_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "需要的字段";
            // 
            // btRomoveAll
            // 
            this.btRomoveAll.Location = new System.Drawing.Point(130, 120);
            this.btRomoveAll.Name = "btRomoveAll";
            this.btRomoveAll.Size = new System.Drawing.Size(35, 23);
            this.btRomoveAll.TabIndex = 19;
            this.btRomoveAll.Text = "<<";
            this.btRomoveAll.UseVisualStyleBackColor = true;
            this.btRomoveAll.Click += new System.EventHandler(this.btRomoveAll_Click);
            // 
            // btAddAll
            // 
            this.btAddAll.Location = new System.Drawing.Point(130, 36);
            this.btAddAll.Name = "btAddAll";
            this.btAddAll.Size = new System.Drawing.Size(35, 23);
            this.btAddAll.TabIndex = 16;
            this.btAddAll.Text = ">>";
            this.btAddAll.UseVisualStyleBackColor = true;
            this.btAddAll.Click += new System.EventHandler(this.btAddAll_Click);
            // 
            // btAddOne
            // 
            this.btAddOne.Location = new System.Drawing.Point(130, 64);
            this.btAddOne.Name = "btAddOne";
            this.btAddOne.Size = new System.Drawing.Size(35, 23);
            this.btAddOne.TabIndex = 17;
            this.btAddOne.Text = "->";
            this.btAddOne.UseVisualStyleBackColor = true;
            this.btAddOne.Click += new System.EventHandler(this.btAddOne_Click);
            // 
            // frmConnecteBorder
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCacel;
            this.ClientSize = new System.Drawing.Size(300, 348);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCacel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConnecteBorder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图幅接边处理";
            ((System.ComponentModel.ISupportInitialize)(this.numBuffer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLayer;
        private System.Windows.Forms.NumericUpDown numBuffer;
        private System.Windows.Forms.Button tbOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCacel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btRomoveOne;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbNeedFields;
        private System.Windows.Forms.ListBox lbNotNeedFields;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btRomoveAll;
        private System.Windows.Forms.Button btAddAll;
        private System.Windows.Forms.Button btAddOne;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtJTB;
        private System.Windows.Forms.Button tbOpenJTB;
    }
}