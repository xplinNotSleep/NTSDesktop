namespace AG.COM.SDM.Plugins.Common
{
    partial class MeasureResultForm
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
            this.SegLabel = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.SegLengthResultLabel = new System.Windows.Forms.Label();
            this.TotalLengthResultLabel = new System.Windows.Forms.Label();
            this.LengthResultgroupBox = new System.Windows.Forms.GroupBox();
            this.AreaResultgroupBox = new System.Windows.Forms.GroupBox();
            this.AreaLengthResultLabel = new System.Windows.Forms.Label();
            this.AreaLengthLabel = new System.Windows.Forms.Label();
            this.AreaResultLabel = new System.Windows.Forms.Label();
            this.AreaLabel = new System.Windows.Forms.Label();
            this.LengthResultgroupBox.SuspendLayout();
            this.AreaResultgroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SegLabel
            // 
            this.SegLabel.AutoSize = true;
            this.SegLabel.Location = new System.Drawing.Point(18, 17);
            this.SegLabel.Name = "SegLabel";
            this.SegLabel.Size = new System.Drawing.Size(65, 12);
            this.SegLabel.TabIndex = 0;
            this.SegLabel.Text = "线段长度：";
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(18, 40);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(53, 12);
            this.TotalLabel.TabIndex = 1;
            this.TotalLabel.Text = "总长度：";
            // 
            // SegLengthResultLabel
            // 
            this.SegLengthResultLabel.AutoSize = true;
            this.SegLengthResultLabel.Location = new System.Drawing.Point(83, 17);
            this.SegLengthResultLabel.Name = "SegLengthResultLabel";
            this.SegLengthResultLabel.Size = new System.Drawing.Size(0, 12);
            this.SegLengthResultLabel.TabIndex = 2;
            // 
            // TotalLengthResultLabel
            // 
            this.TotalLengthResultLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.TotalLengthResultLabel.AutoSize = true;
            this.TotalLengthResultLabel.Location = new System.Drawing.Point(83, 40);
            this.TotalLengthResultLabel.Name = "TotalLengthResultLabel";
            this.TotalLengthResultLabel.Size = new System.Drawing.Size(0, 12);
            this.TotalLengthResultLabel.TabIndex = 3;
            // 
            // LengthResultgroupBox
            // 
            this.LengthResultgroupBox.Controls.Add(this.SegLabel);
            this.LengthResultgroupBox.Controls.Add(this.TotalLengthResultLabel);
            this.LengthResultgroupBox.Controls.Add(this.TotalLabel);
            this.LengthResultgroupBox.Controls.Add(this.SegLengthResultLabel);
            this.LengthResultgroupBox.Location = new System.Drawing.Point(12, 12);
            this.LengthResultgroupBox.Name = "LengthResultgroupBox";
            this.LengthResultgroupBox.Size = new System.Drawing.Size(190, 63);
            this.LengthResultgroupBox.TabIndex = 4;
            this.LengthResultgroupBox.TabStop = false;
            this.LengthResultgroupBox.Text = "测距";
            // 
            // AreaResultgroupBox
            // 
            this.AreaResultgroupBox.Controls.Add(this.AreaLengthResultLabel);
            this.AreaResultgroupBox.Controls.Add(this.AreaLengthLabel);
            this.AreaResultgroupBox.Controls.Add(this.AreaResultLabel);
            this.AreaResultgroupBox.Controls.Add(this.AreaLabel);
            this.AreaResultgroupBox.Location = new System.Drawing.Point(12, 12);
            this.AreaResultgroupBox.Name = "AreaResultgroupBox";
            this.AreaResultgroupBox.Size = new System.Drawing.Size(190, 63);
            this.AreaResultgroupBox.TabIndex = 5;
            this.AreaResultgroupBox.TabStop = false;
            this.AreaResultgroupBox.Text = "测面积";
            this.AreaResultgroupBox.Enter += new System.EventHandler(this.AreaResultgroupBox_Enter);
            // 
            // AreaLengthResultLabel
            // 
            this.AreaLengthResultLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AreaLengthResultLabel.AutoSize = true;
            this.AreaLengthResultLabel.Location = new System.Drawing.Point(65, 18);
            this.AreaLengthResultLabel.Name = "AreaLengthResultLabel";
            this.AreaLengthResultLabel.Size = new System.Drawing.Size(0, 12);
            this.AreaLengthResultLabel.TabIndex = 3;
            // 
            // AreaLengthLabel
            // 
            this.AreaLengthLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AreaLengthLabel.AutoSize = true;
            this.AreaLengthLabel.Location = new System.Drawing.Point(18, 18);
            this.AreaLengthLabel.Name = "AreaLengthLabel";
            this.AreaLengthLabel.Size = new System.Drawing.Size(41, 12);
            this.AreaLengthLabel.TabIndex = 2;
            this.AreaLengthLabel.Text = "周长：";
            // 
            // AreaResultLabel
            // 
            this.AreaResultLabel.AutoSize = true;
            this.AreaResultLabel.Location = new System.Drawing.Point(65, 42);
            this.AreaResultLabel.Name = "AreaResultLabel";
            this.AreaResultLabel.Size = new System.Drawing.Size(0, 12);
            this.AreaResultLabel.TabIndex = 1;
            // 
            // AreaLabel
            // 
            this.AreaLabel.AutoSize = true;
            this.AreaLabel.Location = new System.Drawing.Point(18, 42);
            this.AreaLabel.Name = "AreaLabel";
            this.AreaLabel.Size = new System.Drawing.Size(41, 12);
            this.AreaLabel.TabIndex = 0;
            this.AreaLabel.Text = "面积：";
            // 
            // MeasureResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(214, 88);
            this.Controls.Add(this.AreaResultgroupBox);
            this.Controls.Add(this.LengthResultgroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(500, 500);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MeasureResultForm";
            this.Opacity = 0.8;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "测量结果";
            this.Load += new System.EventHandler(this.MeasureResultForm_Load);
            this.LengthResultgroupBox.ResumeLayout(false);
            this.LengthResultgroupBox.PerformLayout();
            this.AreaResultgroupBox.ResumeLayout(false);
            this.AreaResultgroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label SegLabel;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label SegLengthResultLabel;
        private System.Windows.Forms.Label TotalLengthResultLabel;
        private System.Windows.Forms.GroupBox LengthResultgroupBox;
        private System.Windows.Forms.GroupBox AreaResultgroupBox;
        private System.Windows.Forms.Label AreaResultLabel;
        private System.Windows.Forms.Label AreaLabel;
        private System.Windows.Forms.Label AreaLengthResultLabel;
        private System.Windows.Forms.Label AreaLengthLabel;
    }
}