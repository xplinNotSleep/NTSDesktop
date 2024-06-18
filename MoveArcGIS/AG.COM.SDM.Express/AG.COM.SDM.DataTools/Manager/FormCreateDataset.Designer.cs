namespace AG.COM.SDM.DataTools.Manager
{
    partial class FormCreateDataset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateDataset));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btOpenWorkspace = new System.Windows.Forms.Button();
            this.txtWorkspace = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btSelectSpatialRef = new System.Windows.Forms.Button();
            this.txtSpatialRef = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(296, 183);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(70, 24);
            this.btOK.TabIndex = 38;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(372, 183);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(70, 24);
            this.btCancel.TabIndex = 37;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btOpenWorkspace);
            this.groupBox1.Controls.Add(this.txtWorkspace);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btSelectSpatialRef);
            this.groupBox1.Controls.Add(this.txtSpatialRef);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 162);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "位置";
            // 
            // btOpenWorkspace
            // 
            this.btOpenWorkspace.Image = ((System.Drawing.Image)(resources.GetObject("btOpenWorkspace.Image")));
            this.btOpenWorkspace.Location = new System.Drawing.Point(398, 29);
            this.btOpenWorkspace.Name = "btOpenWorkspace";
            this.btOpenWorkspace.Size = new System.Drawing.Size(23, 23);
            this.btOpenWorkspace.TabIndex = 48;
            this.btOpenWorkspace.UseVisualStyleBackColor = true;
            this.btOpenWorkspace.Click += new System.EventHandler(this.btOpenWorkspace_Click);
            // 
            // txtWorkspace
            // 
            this.txtWorkspace.Location = new System.Drawing.Point(94, 31);
            this.txtWorkspace.Name = "txtWorkspace";
            this.txtWorkspace.ReadOnly = true;
            this.txtWorkspace.Size = new System.Drawing.Size(298, 21);
            this.txtWorkspace.TabIndex = 47;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(94, 77);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(298, 21);
            this.txtName.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "空间参考";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 45;
            this.label3.Text = "数据集名称";
            // 
            // btSelectSpatialRef
            // 
            this.btSelectSpatialRef.Image = ((System.Drawing.Image)(resources.GetObject("btSelectSpatialRef.Image")));
            this.btSelectSpatialRef.Location = new System.Drawing.Point(398, 123);
            this.btSelectSpatialRef.Name = "btSelectSpatialRef";
            this.btSelectSpatialRef.Size = new System.Drawing.Size(23, 23);
            this.btSelectSpatialRef.TabIndex = 43;
            this.btSelectSpatialRef.UseVisualStyleBackColor = true;
            this.btSelectSpatialRef.Click += new System.EventHandler(this.btSelectSpatialRef_Click);
            // 
            // txtSpatialRef
            // 
            this.txtSpatialRef.Location = new System.Drawing.Point(94, 123);
            this.txtSpatialRef.Name = "txtSpatialRef";
            this.txtSpatialRef.ReadOnly = true;
            this.txtSpatialRef.Size = new System.Drawing.Size(298, 21);
            this.txtSpatialRef.TabIndex = 42;
            // 
            // FormCreateDataset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 214);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateDataset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建要素集";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btOpenWorkspace;
        private System.Windows.Forms.TextBox txtWorkspace;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSelectSpatialRef;
        private System.Windows.Forms.TextBox txtSpatialRef;
    }
}