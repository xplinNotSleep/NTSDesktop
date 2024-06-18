namespace AG.COM.SDM.DataTools.Conversion
{
    partial class FormExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExport));
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvwInputFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnAddInputFile = new System.Windows.Forms.Button();
            this.btnDeleteInputFile = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFormat = new System.Windows.Forms.Button();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.txtExport = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ofdShpFile = new System.Windows.Forms.OpenFileDialog();
            this.fbdBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "导出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwInputFiles);
            this.groupBox1.Controls.Add(this.btnAddInputFile);
            this.groupBox1.Controls.Add(this.btnDeleteInputFile);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnFormat);
            this.groupBox1.Controls.Add(this.txtFormat);
            this.groupBox1.Controls.Add(this.txtExport);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 315);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导出设置";
            // 
            // lvwInputFiles
            // 
            this.lvwInputFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwInputFiles.FullRowSelect = true;
            this.lvwInputFiles.Location = new System.Drawing.Point(103, 63);
            this.lvwInputFiles.Name = "lvwInputFiles";
            this.lvwInputFiles.Size = new System.Drawing.Size(235, 193);
            this.lvwInputFiles.TabIndex = 11;
            this.lvwInputFiles.UseCompatibleStateImageBehavior = false;
            this.lvwInputFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件";
            this.columnHeader1.Width = 230;
            // 
            // btnAddInputFile
            // 
            this.btnAddInputFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddInputFile.Image")));
            this.btnAddInputFile.Location = new System.Drawing.Point(344, 71);
            this.btnAddInputFile.Name = "btnAddInputFile";
            this.btnAddInputFile.Size = new System.Drawing.Size(30, 29);
            this.btnAddInputFile.TabIndex = 13;
            this.btnAddInputFile.UseVisualStyleBackColor = true;
            this.btnAddInputFile.Click += new System.EventHandler(this.btnAddCADFile_Click);
            // 
            // btnDeleteInputFile
            // 
            this.btnDeleteInputFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteInputFile.Image")));
            this.btnDeleteInputFile.Location = new System.Drawing.Point(344, 114);
            this.btnDeleteInputFile.Name = "btnDeleteInputFile";
            this.btnDeleteInputFile.Size = new System.Drawing.Size(30, 29);
            this.btnDeleteInputFile.TabIndex = 12;
            this.btnDeleteInputFile.UseVisualStyleBackColor = true;
            this.btnDeleteInputFile.Click += new System.EventHandler(this.btnDeleteInputFile_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Location = new System.Drawing.Point(345, 271);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(29, 30);
            this.btnExport.TabIndex = 4;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(345, 22);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(32, 23);
            this.btnFormat.TabIndex = 4;
            this.btnFormat.Text = "...";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // txtFormat
            // 
            this.txtFormat.Location = new System.Drawing.Point(103, 21);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.ReadOnly = true;
            this.txtFormat.Size = new System.Drawing.Size(235, 21);
            this.txtFormat.TabIndex = 2;
            // 
            // txtExport
            // 
            this.txtExport.Location = new System.Drawing.Point(103, 277);
            this.txtExport.Name = "txtExport";
            this.txtExport.ReadOnly = true;
            this.txtExport.Size = new System.Drawing.Size(235, 21);
            this.txtExport.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "导出要素：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "导出数据格式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "导入要素：";
            // 
            // ofdShpFile
            // 
            this.ofdShpFile.Multiselect = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(316, 333);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 6;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(93, 343);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 7;
            // 
            // FormExport
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(414, 363);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "shape转换为其它数据";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFormat;
        private System.Windows.Forms.TextBox txtFormat;
        private System.Windows.Forms.TextBox txtExport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.OpenFileDialog ofdShpFile;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowser;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ListView lvwInputFiles;
        private System.Windows.Forms.Button btnAddInputFile;
        private System.Windows.Forms.Button btnDeleteInputFile;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}