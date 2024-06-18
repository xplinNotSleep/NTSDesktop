namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    partial class frmProgeress
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
            this.labDisplay = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labDisplay
            // 
            this.labDisplay.AutoSize = true;
            this.labDisplay.Location = new System.Drawing.Point(10, 9);
            this.labDisplay.Name = "labDisplay";
            this.labDisplay.Size = new System.Drawing.Size(41, 12);
            this.labDisplay.TabIndex = 0;
            this.labDisplay.Text = "进度条";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(265, 18);
            this.progressBar1.TabIndex = 1;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(95, 55);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(70, 24);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "终 止";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // frmProgeress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(282, 89);
            this.ControlBox = false;
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labDisplay);
            this.Name = "frmProgeress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labDisplay;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btCancel;
    }
}