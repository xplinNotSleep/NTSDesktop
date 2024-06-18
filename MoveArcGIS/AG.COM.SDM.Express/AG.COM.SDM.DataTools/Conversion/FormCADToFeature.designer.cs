namespace AG.COM.SDM.DataTools.Conversion
{
    partial class FormCADToFeature
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCADToFeature));
            this.lvwCADFileList = new System.Windows.Forms.ListView();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDeleteCADFile = new System.Windows.Forms.Button();
            this.btnAddCADFile = new System.Windows.Forms.Button();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.txtOutputName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwCADFileList
            // 
            this.lvwCADFileList.FullRowSelect = true;
            this.lvwCADFileList.Location = new System.Drawing.Point(12, 23);
            this.lvwCADFileList.Name = "lvwCADFileList";
            this.lvwCADFileList.Size = new System.Drawing.Size(345, 193);
            this.lvwCADFileList.TabIndex = 6;
            this.tipMain.SetToolTip(this.lvwCADFileList, "转换CAD文件列表");
            this.lvwCADFileList.UseCompatibleStateImageBehavior = false;
            this.lvwCADFileList.View = System.Windows.Forms.View.List;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(345, 337);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 11;
            this.btnCancle.Text = "关闭";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(264, 337);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDeleteCADFile
            // 
            this.btnDeleteCADFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCADFile.Image")));
            this.btnDeleteCADFile.Location = new System.Drawing.Point(378, 70);
            this.btnDeleteCADFile.Name = "btnDeleteCADFile";
            this.btnDeleteCADFile.Size = new System.Drawing.Size(30, 29);
            this.btnDeleteCADFile.TabIndex = 8;
            this.tipMain.SetToolTip(this.btnDeleteCADFile, "移除CAD文件");
            this.btnDeleteCADFile.UseVisualStyleBackColor = true;
            this.btnDeleteCADFile.Click += new System.EventHandler(this.btnDeleteCADFile_Click);
            // 
            // btnAddCADFile
            // 
            this.btnAddCADFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCADFile.Image")));
            this.btnAddCADFile.Location = new System.Drawing.Point(378, 27);
            this.btnAddCADFile.Name = "btnAddCADFile";
            this.btnAddCADFile.Size = new System.Drawing.Size(30, 29);
            this.btnAddCADFile.TabIndex = 10;
            this.tipMain.SetToolTip(this.btnAddCADFile, "添加CAD文件");
            this.btnAddCADFile.UseVisualStyleBackColor = true;
            this.btnAddCADFile.Click += new System.EventHandler(this.btnAddCADFile_Click);
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Image = ((System.Drawing.Image)(resources.GetObject("btnOutputPath.Image")));
            this.btnOutputPath.Location = new System.Drawing.Point(378, 15);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(30, 29);
            this.btnOutputPath.TabIndex = 13;
            this.tipMain.SetToolTip(this.btnOutputPath, "转换输出GDB文件位置");
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputFile_Click);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(59, 20);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(298, 21);
            this.txtOutputPath.TabIndex = 12;
            // 
            // txtOutputName
            // 
            this.txtOutputName.Location = new System.Drawing.Point(59, 47);
            this.txtOutputName.Name = "txtOutputName";
            this.txtOutputName.Size = new System.Drawing.Size(298, 21);
            this.txtOutputName.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "位置：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwCADFileList);
            this.groupBox1.Controls.Add(this.btnAddCADFile);
            this.groupBox1.Controls.Add(this.btnDeleteCADFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 234);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "转换的CAD文件：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOutputPath);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtOutputName);
            this.groupBox2.Controls.Add(this.btnOutputPath);
            this.groupBox2.Location = new System.Drawing.Point(12, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 83);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出的GDB文件：";
            // 
            // FormCADToFeature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 366);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCADToFeature";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CAD转文件数据库";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwCADFileList;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDeleteCADFile;
        private System.Windows.Forms.Button btnAddCADFile;
        private System.Windows.Forms.ToolTip tipMain;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.TextBox txtOutputName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}