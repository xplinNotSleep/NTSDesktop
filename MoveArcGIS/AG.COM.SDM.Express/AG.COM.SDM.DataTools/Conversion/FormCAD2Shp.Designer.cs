namespace AG.COM.SDM.DataTools.Conversion
{
    partial class FormCAD2Shp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCAD2Shp));
            this.txtFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstTransformInfo = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbrStat = new System.Windows.Forms.ProgressBar();
            this.cmdTransform = new System.Windows.Forms.Button();
            this.cmdSpread = new System.Windows.Forms.Button();
            this.labState = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.butRemove = new System.Windows.Forms.Button();
            this.txtLayOut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butLayOut = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(48, 20);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(334, 21);
            this.txtFile.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butLayOut);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLayOut);
            this.groupBox1.Controls.Add(this.cmdOpenFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFile);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 86);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpenFile.Image")));
            this.cmdOpenFile.Location = new System.Drawing.Point(388, 18);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(24, 23);
            this.cmdOpenFile.TabIndex = 3;
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "打开：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstTransformInfo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(2, 292);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 182);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // lstTransformInfo
            // 
            this.lstTransformInfo.FormattingEnabled = true;
            this.lstTransformInfo.ItemHeight = 12;
            this.lstTransformInfo.Location = new System.Drawing.Point(6, 32);
            this.lstTransformInfo.Name = "lstTransformInfo";
            this.lstTransformInfo.Size = new System.Drawing.Size(407, 136);
            this.lstTransformInfo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "转化信息";
            // 
            // pbrStat
            // 
            this.pbrStat.Location = new System.Drawing.Point(8, 269);
            this.pbrStat.Name = "pbrStat";
            this.pbrStat.Size = new System.Drawing.Size(407, 17);
            this.pbrStat.TabIndex = 4;
            // 
            // cmdTransform
            // 
            this.cmdTransform.Location = new System.Drawing.Point(339, 100);
            this.cmdTransform.Name = "cmdTransform";
            this.cmdTransform.Size = new System.Drawing.Size(60, 24);
            this.cmdTransform.TabIndex = 5;
            this.cmdTransform.Text = "转换";
            this.cmdTransform.UseVisualStyleBackColor = true;
            this.cmdTransform.Click += new System.EventHandler(this.cmdTransform_Click);
            // 
            // cmdSpread
            // 
            this.cmdSpread.Location = new System.Drawing.Point(339, 236);
            this.cmdSpread.Name = "cmdSpread";
            this.cmdSpread.Size = new System.Drawing.Size(60, 24);
            this.cmdSpread.TabIndex = 6;
            this.cmdSpread.Text = "展开";
            this.cmdSpread.UseVisualStyleBackColor = true;
            this.cmdSpread.Click += new System.EventHandler(this.cmdSpread_Click);
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(0, 477);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(421, 15);
            this.labState.TabIndex = 7;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(8, 100);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(304, 160);
            this.listBox1.TabIndex = 8;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(339, 168);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(60, 24);
            this.butCancel.TabIndex = 9;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butExit
            // 
            this.butExit.Location = new System.Drawing.Point(339, 202);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(60, 24);
            this.butExit.TabIndex = 10;
            this.butExit.Text = "退出";
            this.butExit.UseVisualStyleBackColor = true;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(482, 147);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(304, 88);
            this.listBox2.TabIndex = 11;
            this.listBox2.Visible = false;
            // 
            // butRemove
            // 
            this.butRemove.Location = new System.Drawing.Point(339, 134);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(60, 24);
            this.butRemove.TabIndex = 12;
            this.butRemove.Text = " 移除";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // txtLayOut
            // 
            this.txtLayOut.Location = new System.Drawing.Point(48, 50);
            this.txtLayOut.Name = "txtLayOut";
            this.txtLayOut.Size = new System.Drawing.Size(334, 21);
            this.txtLayOut.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "输出：";
            // 
            // butLayOut
            // 
            this.butLayOut.Image = ((System.Drawing.Image)(resources.GetObject("butLayOut.Image")));
            this.butLayOut.Location = new System.Drawing.Point(388, 50);
            this.butLayOut.Name = "butLayOut";
            this.butLayOut.Size = new System.Drawing.Size(24, 23);
            this.butLayOut.TabIndex = 6;
            this.butLayOut.UseVisualStyleBackColor = true;
            this.butLayOut.Click += new System.EventHandler(this.butLayOut_Click);
            // 
            // frmCADTransform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 491);
            this.Controls.Add(this.butRemove);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labState);
            this.Controls.Add(this.cmdSpread);
            this.Controls.Add(this.cmdTransform);
            this.Controls.Add(this.pbrStat);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCADTransform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CAD转换器";
            this.Load += new System.EventHandler(this.frmCADTransform_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstTransformInfo;
        private System.Windows.Forms.ProgressBar pbrStat;
        private System.Windows.Forms.Button cmdTransform;
        private System.Windows.Forms.Button cmdSpread;
        private System.Windows.Forms.Label labState;
        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butExit;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.TextBox txtLayOut;
        private System.Windows.Forms.Button butLayOut;
        private System.Windows.Forms.Label label3;
    }
}