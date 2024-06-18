namespace AG.COM.SDM.SystemUI
{
    partial class TrackProgressDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackProgressDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubtip = new System.Windows.Forms.Label();
            this.progbarSub = new System.Windows.Forms.ProgressBar();
            this.panelTotal = new System.Windows.Forms.Panel();
            this.lblTotaltip = new System.Windows.Forms.Label();
            this.progBarTotal = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelTotal.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.lblSubtip);
            this.panel1.Controls.Add(this.progbarSub);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 56);
            this.panel1.TabIndex = 8;
            // 
            // lblSubtip
            // 
            this.lblSubtip.AutoSize = true;
            this.lblSubtip.Location = new System.Drawing.Point(7, 12);
            this.lblSubtip.Name = "lblSubtip";
            this.lblSubtip.Size = new System.Drawing.Size(77, 12);
            this.lblSubtip.TabIndex = 0;
            this.lblSubtip.Text = "正在处理……";
            // 
            // progbarSub
            // 
            this.progbarSub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progbarSub.BackColor = System.Drawing.SystemColors.Window;
            this.progbarSub.ForeColor = System.Drawing.Color.GreenYellow;
            this.progbarSub.Location = new System.Drawing.Point(7, 27);
            this.progbarSub.Name = "progbarSub";
            this.progbarSub.Size = new System.Drawing.Size(303, 23);
            this.progbarSub.TabIndex = 1;
            // 
            // panelTotal
            // 
            this.panelTotal.BackColor = System.Drawing.Color.AliceBlue;
            this.panelTotal.Controls.Add(this.lblTotaltip);
            this.panelTotal.Controls.Add(this.progBarTotal);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotal.Location = new System.Drawing.Point(0, 56);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(319, 52);
            this.panelTotal.TabIndex = 7;
            // 
            // lblTotaltip
            // 
            this.lblTotaltip.AutoSize = true;
            this.lblTotaltip.Location = new System.Drawing.Point(7, 7);
            this.lblTotaltip.Name = "lblTotaltip";
            this.lblTotaltip.Size = new System.Drawing.Size(77, 12);
            this.lblTotaltip.TabIndex = 0;
            this.lblTotaltip.Text = "正在处理……";
            // 
            // progBarTotal
            // 
            this.progBarTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progBarTotal.BackColor = System.Drawing.SystemColors.Window;
            this.progBarTotal.ForeColor = System.Drawing.Color.GreenYellow;
            this.progBarTotal.Location = new System.Drawing.Point(7, 22);
            this.progBarTotal.Name = "progBarTotal";
            this.progBarTotal.Size = new System.Drawing.Size(303, 23);
            this.progBarTotal.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(319, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 108);
            this.panel2.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(7, 34);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关    闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(7, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "停止操作";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // TrackProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(399, 108);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTotal);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrackProgressDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.TrackProgressDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTotal.ResumeLayout(false);
            this.panelTotal.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubtip;
        private System.Windows.Forms.ProgressBar progbarSub;
        private System.Windows.Forms.Panel panelTotal;
        private System.Windows.Forms.Label lblTotaltip;
        private System.Windows.Forms.ProgressBar progBarTotal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;

    }
}