
namespace AG.COM.SDM.Config.DbConnUI
{
    partial class FrmDbOptions
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOlePwd = new System.Windows.Forms.TextBox();
            this.txtOlePort = new System.Windows.Forms.TextBox();
            this.txtOleUser = new System.Windows.Forms.TextBox();
            this.txtOleDbName = new System.Windows.Forms.TextBox();
            this.txtOleServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOleDbType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTestOle = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTestSpatial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSpatialDbType = new System.Windows.Forms.ComboBox();
            this.txtSpatialPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSpatialUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpatialDbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSpatialPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSpatialServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBucketName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMinioSecret = new System.Windows.Forms.TextBox();
            this.txtMinioAccess = new System.Windows.Forms.TextBox();
            this.txtMinioURL = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(499, 428);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtOlePwd);
            this.tabPage1.Controls.Add(this.txtOlePort);
            this.tabPage1.Controls.Add(this.txtOleUser);
            this.tabPage1.Controls.Add(this.txtOleDbName);
            this.tabPage1.Controls.Add(this.txtOleServer);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.cmbOleDbType);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(491, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "属性库";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(83, 295);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 35;
            this.label11.Text = "密码:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(83, 254);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 15);
            this.label10.TabIndex = 34;
            this.label10.Text = "用户名:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(83, 172);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 37;
            this.label12.Text = "端口:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(83, 213);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "数据库:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(83, 130);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 15);
            this.label8.TabIndex = 36;
            this.label8.Text = "服务器:";
            // 
            // txtOlePwd
            // 
            this.txtOlePwd.Location = new System.Drawing.Point(183, 289);
            this.txtOlePwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtOlePwd.Name = "txtOlePwd";
            this.txtOlePwd.PasswordChar = '*';
            this.txtOlePwd.Size = new System.Drawing.Size(224, 25);
            this.txtOlePwd.TabIndex = 32;
            // 
            // txtOlePort
            // 
            this.txtOlePort.Location = new System.Drawing.Point(183, 165);
            this.txtOlePort.Margin = new System.Windows.Forms.Padding(4);
            this.txtOlePort.Name = "txtOlePort";
            this.txtOlePort.Size = new System.Drawing.Size(224, 25);
            this.txtOlePort.TabIndex = 29;
            // 
            // txtOleUser
            // 
            this.txtOleUser.Location = new System.Drawing.Point(183, 248);
            this.txtOleUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtOleUser.Name = "txtOleUser";
            this.txtOleUser.Size = new System.Drawing.Size(224, 25);
            this.txtOleUser.TabIndex = 31;
            // 
            // txtOleDbName
            // 
            this.txtOleDbName.Location = new System.Drawing.Point(183, 206);
            this.txtOleDbName.Margin = new System.Windows.Forms.Padding(4);
            this.txtOleDbName.Name = "txtOleDbName";
            this.txtOleDbName.Size = new System.Drawing.Size(224, 25);
            this.txtOleDbName.TabIndex = 30;
            // 
            // txtOleServer
            // 
            this.txtOleServer.Location = new System.Drawing.Point(183, 124);
            this.txtOleServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtOleServer.Name = "txtOleServer";
            this.txtOleServer.Size = new System.Drawing.Size(224, 25);
            this.txtOleServer.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(83, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "数据库类型:";
            // 
            // cmbOleDbType
            // 
            this.cmbOleDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOleDbType.FormattingEnabled = true;
            this.cmbOleDbType.Location = new System.Drawing.Point(183, 84);
            this.cmbOleDbType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOleDbType.Name = "cmbOleDbType";
            this.cmbOleDbType.Size = new System.Drawing.Size(224, 23);
            this.cmbOleDbType.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestOle);
            this.groupBox1.Location = new System.Drawing.Point(21, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 354);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性库设置";
            // 
            // btnTestOle
            // 
            this.btnTestOle.Location = new System.Drawing.Point(311, 314);
            this.btnTestOle.Name = "btnTestOle";
            this.btnTestOle.Size = new System.Drawing.Size(75, 23);
            this.btnTestOle.TabIndex = 0;
            this.btnTestOle.Text = "测试连接";
            this.btnTestOle.UseVisualStyleBackColor = true;
            this.btnTestOle.Click += new System.EventHandler(this.btnTestOle_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(491, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "空间库";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTestSpatial);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbSpatialDbType);
            this.groupBox2.Controls.Add(this.txtSpatialPwd);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSpatialUser);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSpatialDbName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSpatialPort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSpatialServer);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(19, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(453, 362);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "空间库设置";
            // 
            // btnTestSpatial
            // 
            this.btnTestSpatial.Location = new System.Drawing.Point(336, 318);
            this.btnTestSpatial.Name = "btnTestSpatial";
            this.btnTestSpatial.Size = new System.Drawing.Size(75, 23);
            this.btnTestSpatial.TabIndex = 41;
            this.btnTestSpatial.Text = "测试连接";
            this.btnTestSpatial.UseVisualStyleBackColor = true;
            this.btnTestSpatial.Click += new System.EventHandler(this.btnTestSpatial_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 40;
            this.label1.Text = "数据库类型:";
            // 
            // cmbSpatialDbType
            // 
            this.cmbSpatialDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpatialDbType.FormattingEnabled = true;
            this.cmbSpatialDbType.Location = new System.Drawing.Point(152, 69);
            this.cmbSpatialDbType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpatialDbType.Name = "cmbSpatialDbType";
            this.cmbSpatialDbType.Size = new System.Drawing.Size(259, 23);
            this.cmbSpatialDbType.TabIndex = 39;
            // 
            // txtSpatialPwd
            // 
            this.txtSpatialPwd.Location = new System.Drawing.Point(152, 268);
            this.txtSpatialPwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpatialPwd.Name = "txtSpatialPwd";
            this.txtSpatialPwd.PasswordChar = '*';
            this.txtSpatialPwd.Size = new System.Drawing.Size(259, 25);
            this.txtSpatialPwd.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 271);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "密码:";
            // 
            // txtSpatialUser
            // 
            this.txtSpatialUser.Location = new System.Drawing.Point(152, 228);
            this.txtSpatialUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpatialUser.Name = "txtSpatialUser";
            this.txtSpatialUser.Size = new System.Drawing.Size(259, 25);
            this.txtSpatialUser.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 231);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 35;
            this.label4.Text = "用户名:";
            // 
            // txtSpatialDbName
            // 
            this.txtSpatialDbName.Location = new System.Drawing.Point(152, 188);
            this.txtSpatialDbName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpatialDbName.Name = "txtSpatialDbName";
            this.txtSpatialDbName.Size = new System.Drawing.Size(259, 25);
            this.txtSpatialDbName.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 194);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 33;
            this.label3.Text = "数据库:";
            // 
            // txtSpatialPort
            // 
            this.txtSpatialPort.Location = new System.Drawing.Point(152, 148);
            this.txtSpatialPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpatialPort.Name = "txtSpatialPort";
            this.txtSpatialPort.Size = new System.Drawing.Size(259, 25);
            this.txtSpatialPort.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "端口号:";
            // 
            // txtSpatialServer
            // 
            this.txtSpatialServer.Location = new System.Drawing.Point(152, 108);
            this.txtSpatialServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpatialServer.Name = "txtSpatialServer";
            this.txtSpatialServer.Size = new System.Drawing.Size(259, 25);
            this.txtSpatialServer.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "服务器名:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(491, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Minio服务器";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBucketName);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtMinioSecret);
            this.groupBox3.Controls.Add(this.txtMinioAccess);
            this.groupBox3.Controls.Add(this.txtMinioURL);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(20, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(455, 361);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Minio服务器参数";
            // 
            // txtBucketName
            // 
            this.txtBucketName.Location = new System.Drawing.Point(117, 304);
            this.txtBucketName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBucketName.Name = "txtBucketName";
            this.txtBucketName.Size = new System.Drawing.Size(309, 25);
            this.txtBucketName.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(28, 308);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.TabIndex = 27;
            this.label13.Text = "桶名称：";
            // 
            // txtMinioSecret
            // 
            this.txtMinioSecret.Location = new System.Drawing.Point(91, 214);
            this.txtMinioSecret.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinioSecret.Name = "txtMinioSecret";
            this.txtMinioSecret.Size = new System.Drawing.Size(335, 25);
            this.txtMinioSecret.TabIndex = 26;
            // 
            // txtMinioAccess
            // 
            this.txtMinioAccess.Location = new System.Drawing.Point(131, 122);
            this.txtMinioAccess.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinioAccess.Name = "txtMinioAccess";
            this.txtMinioAccess.Size = new System.Drawing.Size(295, 25);
            this.txtMinioAccess.TabIndex = 25;
            // 
            // txtMinioURL
            // 
            this.txtMinioURL.Location = new System.Drawing.Point(131, 32);
            this.txtMinioURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinioURL.Name = "txtMinioURL";
            this.txtMinioURL.Size = new System.Drawing.Size(295, 25);
            this.txtMinioURL.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 218);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 15);
            this.label14.TabIndex = 24;
            this.label14.Text = "密码：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 126);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 15);
            this.label15.TabIndex = 23;
            this.label15.Text = "账户名称：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(28, 36);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 15);
            this.label16.TabIndex = 22;
            this.label16.Text = "服务器url：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(291, 460);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(430, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmDbOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 495);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmDbOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库设置";
            this.Load += new System.EventHandler(this.FrmDbOptions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOlePwd;
        private System.Windows.Forms.TextBox txtOlePort;
        private System.Windows.Forms.TextBox txtOleUser;
        private System.Windows.Forms.TextBox txtOleDbName;
        private System.Windows.Forms.TextBox txtOleServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOleDbType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSpatialDbType;
        private System.Windows.Forms.TextBox txtSpatialPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSpatialUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSpatialDbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSpatialPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSpatialServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBucketName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMinioSecret;
        private System.Windows.Forms.TextBox txtMinioAccess;
        private System.Windows.Forms.TextBox txtMinioURL;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnTestOle;
        private System.Windows.Forms.Button btnTestSpatial;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}