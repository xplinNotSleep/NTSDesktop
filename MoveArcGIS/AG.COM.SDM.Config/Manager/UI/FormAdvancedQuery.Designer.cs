namespace AG.COM.SDM.Config
{
    partial class FormAdvancedQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdvancedQuery));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlUploadTime = new System.Windows.Forms.Panel();
            this.dtpLogerEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpLogerBeginTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTaskState = new System.Windows.Forms.Label();
            this.cobHostName = new System.Windows.Forms.ComboBox();
            this.lblEditUser = new System.Windows.Forms.Label();
            this.cobProductName = new System.Windows.Forms.ComboBox();
            this.lblCheckUser = new System.Windows.Forms.Label();
            this.cobLogLever = new System.Windows.Forms.ComboBox();
            this.lblTaskName = new System.Windows.Forms.Label();
            this.cobLoggerName = new System.Windows.Forms.ComboBox();
            this.lblUploadDept = new System.Windows.Forms.Label();
            this.cobLogType = new System.Windows.Forms.ComboBox();
            this.lblUploadUser = new System.Windows.Forms.Label();
            this.cobUsername = new System.Windows.Forms.ComboBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlUploadTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(525, 189);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlUploadTime);
            this.tabPage1.Controls.Add(this.lblTaskState);
            this.tabPage1.Controls.Add(this.cobHostName);
            this.tabPage1.Controls.Add(this.lblEditUser);
            this.tabPage1.Controls.Add(this.cobProductName);
            this.tabPage1.Controls.Add(this.lblCheckUser);
            this.tabPage1.Controls.Add(this.cobLogLever);
            this.tabPage1.Controls.Add(this.lblTaskName);
            this.tabPage1.Controls.Add(this.cobLoggerName);
            this.tabPage1.Controls.Add(this.lblUploadDept);
            this.tabPage1.Controls.Add(this.cobLogType);
            this.tabPage1.Controls.Add(this.lblUploadUser);
            this.tabPage1.Controls.Add(this.cobUsername);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(517, 164);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "高级查询";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlUploadTime
            // 
            this.pnlUploadTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUploadTime.Controls.Add(this.dtpLogerEndTime);
            this.pnlUploadTime.Controls.Add(this.dtpLogerBeginTime);
            this.pnlUploadTime.Controls.Add(this.label5);
            this.pnlUploadTime.Controls.Add(this.label1);
            this.pnlUploadTime.Location = new System.Drawing.Point(3, 123);
            this.pnlUploadTime.Name = "pnlUploadTime";
            this.pnlUploadTime.Size = new System.Drawing.Size(511, 33);
            this.pnlUploadTime.TabIndex = 49;
            // 
            // dtpLogerEndTime
            // 
            this.dtpLogerEndTime.Checked = false;
            this.dtpLogerEndTime.Location = new System.Drawing.Point(330, 7);
            this.dtpLogerEndTime.Name = "dtpLogerEndTime";
            this.dtpLogerEndTime.ShowCheckBox = true;
            this.dtpLogerEndTime.Size = new System.Drawing.Size(176, 21);
            this.dtpLogerEndTime.TabIndex = 24;
            // 
            // dtpLogerBeginTime
            // 
            this.dtpLogerBeginTime.Checked = false;
            this.dtpLogerBeginTime.Location = new System.Drawing.Point(72, 7);
            this.dtpLogerBeginTime.Name = "dtpLogerBeginTime";
            this.dtpLogerBeginTime.ShowCheckBox = true;
            this.dtpLogerBeginTime.Size = new System.Drawing.Size(176, 21);
            this.dtpLogerBeginTime.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "到";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "登录时间：";
            // 
            // lblTaskState
            // 
            this.lblTaskState.AutoSize = true;
            this.lblTaskState.Location = new System.Drawing.Point(265, 19);
            this.lblTaskState.Name = "lblTaskState";
            this.lblTaskState.Size = new System.Drawing.Size(65, 12);
            this.lblTaskState.TabIndex = 47;
            this.lblTaskState.Text = "主机名称：";
            // 
            // cobHostName
            // 
            this.cobHostName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobHostName.FormattingEnabled = true;
            this.cobHostName.Location = new System.Drawing.Point(334, 15);
            this.cobHostName.Name = "cobHostName";
            this.cobHostName.Size = new System.Drawing.Size(176, 20);
            this.cobHostName.TabIndex = 48;
            // 
            // lblEditUser
            // 
            this.lblEditUser.AutoSize = true;
            this.lblEditUser.Location = new System.Drawing.Point(6, 94);
            this.lblEditUser.Name = "lblEditUser";
            this.lblEditUser.Size = new System.Drawing.Size(41, 12);
            this.lblEditUser.TabIndex = 32;
            this.lblEditUser.Text = "级别：";
            // 
            // cobProductName
            // 
            this.cobProductName.FormattingEnabled = true;
            this.cobProductName.Location = new System.Drawing.Point(334, 90);
            this.cobProductName.Name = "cobProductName";
            this.cobProductName.Size = new System.Drawing.Size(176, 20);
            this.cobProductName.TabIndex = 33;
            // 
            // lblCheckUser
            // 
            this.lblCheckUser.AutoSize = true;
            this.lblCheckUser.Location = new System.Drawing.Point(265, 94);
            this.lblCheckUser.Name = "lblCheckUser";
            this.lblCheckUser.Size = new System.Drawing.Size(65, 12);
            this.lblCheckUser.TabIndex = 31;
            this.lblCheckUser.Text = "产品名称：";
            // 
            // cobLogLever
            // 
            this.cobLogLever.FormattingEnabled = true;
            this.cobLogLever.Location = new System.Drawing.Point(75, 90);
            this.cobLogLever.Name = "cobLogLever";
            this.cobLogLever.Size = new System.Drawing.Size(176, 20);
            this.cobLogLever.TabIndex = 34;
            // 
            // lblTaskName
            // 
            this.lblTaskName.AutoSize = true;
            this.lblTaskName.Location = new System.Drawing.Point(6, 18);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(53, 12);
            this.lblTaskName.TabIndex = 25;
            this.lblTaskName.Text = "登录名：";
            // 
            // cobLoggerName
            // 
            this.cobLoggerName.FormattingEnabled = true;
            this.cobLoggerName.Location = new System.Drawing.Point(75, 14);
            this.cobLoggerName.Name = "cobLoggerName";
            this.cobLoggerName.Size = new System.Drawing.Size(176, 20);
            this.cobLoggerName.TabIndex = 26;
            // 
            // lblUploadDept
            // 
            this.lblUploadDept.AutoSize = true;
            this.lblUploadDept.Location = new System.Drawing.Point(6, 56);
            this.lblUploadDept.Name = "lblUploadDept";
            this.lblUploadDept.Size = new System.Drawing.Size(53, 12);
            this.lblUploadDept.TabIndex = 10;
            this.lblUploadDept.Text = "用户名：";
            // 
            // cobLogType
            // 
            this.cobLogType.FormattingEnabled = true;
            this.cobLogType.Location = new System.Drawing.Point(334, 52);
            this.cobLogType.Name = "cobLogType";
            this.cobLogType.Size = new System.Drawing.Size(176, 20);
            this.cobLogType.TabIndex = 11;
            // 
            // lblUploadUser
            // 
            this.lblUploadUser.AutoSize = true;
            this.lblUploadUser.Location = new System.Drawing.Point(265, 55);
            this.lblUploadUser.Name = "lblUploadUser";
            this.lblUploadUser.Size = new System.Drawing.Size(65, 12);
            this.lblUploadUser.TabIndex = 6;
            this.lblUploadUser.Text = "登录类型：";
            // 
            // cobUsername
            // 
            this.cobUsername.FormattingEnabled = true;
            this.cobUsername.Location = new System.Drawing.Point(75, 52);
            this.cobUsername.Name = "cobUsername";
            this.cobUsername.Size = new System.Drawing.Size(176, 20);
            this.cobUsername.TabIndex = 15;
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(450, 198);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(63, 23);
            this.btnCancle.TabIndex = 22;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(367, 198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(63, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormAdvancedQuery
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(525, 227);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdvancedQuery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "高级查询";
            this.Load += new System.EventHandler(this.FormAdvancedQuery_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlUploadTime.ResumeLayout(false);
            this.pnlUploadTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.ComboBox cobLoggerName;
        private System.Windows.Forms.Label lblUploadDept;
        private System.Windows.Forms.ComboBox cobLogType;
        private System.Windows.Forms.Label lblUploadUser;
        private System.Windows.Forms.ComboBox cobUsername;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblEditUser;
        private System.Windows.Forms.ComboBox cobProductName;
        private System.Windows.Forms.Label lblCheckUser;
        private System.Windows.Forms.ComboBox cobLogLever;
        private System.Windows.Forms.Label lblTaskState;
        private System.Windows.Forms.ComboBox cobHostName;
        private System.Windows.Forms.Panel pnlUploadTime;
        private System.Windows.Forms.DateTimePicker dtpLogerEndTime;
        private System.Windows.Forms.DateTimePicker dtpLogerBeginTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
    }
}