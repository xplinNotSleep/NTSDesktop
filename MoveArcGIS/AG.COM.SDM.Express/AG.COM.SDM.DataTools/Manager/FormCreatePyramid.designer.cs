namespace AG.COM.SDM.DataTools.Manager
{
    partial class FormCreatePyramid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreatePyramid));
            this.label3 = new System.Windows.Forms.Label();
            this.txtRasterName = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nudPyramidLevel = new System.Windows.Forms.NumericUpDown();
            this.cbxPyramidResample = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuild = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPyramidLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "目标栅格数据集";
            // 
            // txtRasterName
            // 
            this.txtRasterName.Location = new System.Drawing.Point(107, 15);
            this.txtRasterName.Name = "txtRasterName";
            this.txtRasterName.ReadOnly = true;
            this.txtRasterName.Size = new System.Drawing.Size(182, 21);
            this.txtRasterName.TabIndex = 13;
            // 
            // btnOpen
            // 
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.Location = new System.Drawing.Point(295, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(29, 23);
            this.btnOpen.TabIndex = 12;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(255, 111);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(69, 21);
            this.txtY.TabIndex = 28;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(129, 111);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(69, 21);
            this.txtX.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(237, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "Y";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(112, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "X";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "金字塔参考坐标";
            // 
            // nudPyramidLevel
            // 
            this.nudPyramidLevel.Location = new System.Drawing.Point(118, 58);
            this.nudPyramidLevel.Name = "nudPyramidLevel";
            this.nudPyramidLevel.Size = new System.Drawing.Size(206, 21);
            this.nudPyramidLevel.TabIndex = 23;
            this.nudPyramidLevel.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // cbxPyramidResample
            // 
            this.cbxPyramidResample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPyramidResample.FormattingEnabled = true;
            this.cbxPyramidResample.Location = new System.Drawing.Point(118, 85);
            this.cbxPyramidResample.Name = "cbxPyramidResample";
            this.cbxPyramidResample.Size = new System.Drawing.Size(206, 20);
            this.cbxPyramidResample.TabIndex = 22;
            this.cbxPyramidResample.DropDown += new System.EventHandler(this.cbxPyramidResample_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "金字塔重采样技术";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "金字塔级别";
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(11, 136);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 29;
            this.btnBuild.Text = "创建";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(239, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "注意：金字塔级别为0，表示栅格数据集没有建立金字塔";
            // 
            // FormCreatePyramid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 169);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nudPyramidLevel);
            this.Controls.Add(this.cbxPyramidResample);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRasterName);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreatePyramid";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建&修改金字塔";
            this.Load += new System.EventHandler(this.FormCreatePyramid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPyramidLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRasterName;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudPyramidLevel;
        private System.Windows.Forms.ComboBox cbxPyramidResample;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}