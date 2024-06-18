namespace AG.COM.SDM.Config
{
    partial class CtrArcgisServer
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.tbServiceName = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboMapName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboServiceType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.lblUrlSample = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "服务别称:";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(304, 295);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(65, 25);
            this.btnApply.TabIndex = 53;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tbServiceName
            // 
            this.tbServiceName.Location = new System.Drawing.Point(71, 21);
            this.tbServiceName.Name = "tbServiceName";
            this.tbServiceName.Size = new System.Drawing.Size(298, 21);
            this.tbServiceName.TabIndex = 44;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(71, 193);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(298, 96);
            this.tbDescription.TabIndex = 52;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "服务地址:";
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(71, 55);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(298, 21);
            this.tbUrl.TabIndex = 46;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUrlSample);
            this.groupBox1.Controls.Add(this.cboMapName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboServiceType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.tbServiceName);
            this.groupBox1.Controls.Add(this.tbDescription);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbUrl);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbAccount);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 326);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            // 
            // cboMapName
            // 
            this.cboMapName.FormattingEnabled = true;
            this.cboMapName.Location = new System.Drawing.Point(263, 112);
            this.cboMapName.Name = "cboMapName";
            this.cboMapName.Size = new System.Drawing.Size(106, 20);
            this.cboMapName.TabIndex = 57;
            this.cboMapName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cboMapName_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 56;
            this.label4.Text = "地图名称:";
            // 
            // cboServiceType
            // 
            this.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceType.FormattingEnabled = true;
            this.cboServiceType.Items.AddRange(new object[] {
            "ArcGisServer服务",
            "WMS服务"});
            this.cboServiceType.Location = new System.Drawing.Point(71, 112);
            this.cboServiceType.Name = "cboServiceType";
            this.cboServiceType.Size = new System.Drawing.Size(106, 20);
            this.cboServiceType.TabIndex = 55;
            this.cboServiceType.SelectedIndexChanged += new System.EventHandler(this.cboServiceType_SelectedIndexChanged);
            this.cboServiceType.SelectedValueChanged += new System.EventHandler(this.cboServiceType_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "服务类型:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 51;
            this.label1.Text = "描    述:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(263, 153);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(106, 21);
            this.tbPassword.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 47;
            this.label8.Text = "访问账号:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(198, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 49;
            this.label11.Text = "访问密码:";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(71, 153);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(106, 21);
            this.tbAccount.TabIndex = 48;
            // 
            // lblUrlSample
            // 
            this.lblUrlSample.Location = new System.Drawing.Point(70, 82);
            this.lblUrlSample.Name = "lblUrlSample";
            this.lblUrlSample.Size = new System.Drawing.Size(299, 27);
            this.lblUrlSample.TabIndex = 58;
            // 
            // CtrArcgisServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrArcgisServer";
            this.Size = new System.Drawing.Size(392, 335);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox tbServiceName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboServiceType;
        private System.Windows.Forms.ComboBox cboMapName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUrlSample;
    }
}
