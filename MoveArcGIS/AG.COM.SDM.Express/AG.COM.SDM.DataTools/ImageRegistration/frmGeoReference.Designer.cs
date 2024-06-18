namespace AG.COM.SDM.DataTools.ImageRegistration
{
    partial class frmGeoReference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeoReference));
            this.axToolbarControl = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.lvwPointReference = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTransferMethod = new System.Windows.Forms.ComboBox();
            this.btnTolerance = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.SuspendLayout();
            // 
            // axToolbarControl
            // 
            this.axToolbarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axToolbarControl.Location = new System.Drawing.Point(12, 0);
            this.axToolbarControl.Name = "axToolbarControl";
            this.axToolbarControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl.OcxState")));
            this.axToolbarControl.Size = new System.Drawing.Size(453, 28);
            this.axToolbarControl.TabIndex = 1;
            // 
            // lvwPointReference
            // 
            this.lvwPointReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwPointReference.Location = new System.Drawing.Point(13, 310);
            this.lvwPointReference.Name = "lvwPointReference";
            this.lvwPointReference.Size = new System.Drawing.Size(452, 108);
            this.lvwPointReference.TabIndex = 2;
            this.lvwPointReference.UseCompatibleStateImageBehavior = false;
            this.lvwPointReference.DoubleClick += new System.EventHandler(this.lvwPointReference_DoubleClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "转换方式：";
            // 
            // cboTransferMethod
            // 
            this.cboTransferMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboTransferMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransferMethod.FormattingEnabled = true;
            this.cboTransferMethod.Location = new System.Drawing.Point(12, 448);
            this.cboTransferMethod.Name = "cboTransferMethod";
            this.cboTransferMethod.Size = new System.Drawing.Size(171, 20);
            this.cboTransferMethod.TabIndex = 4;
            // 
            // btnTolerance
            // 
            this.btnTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTolerance.Location = new System.Drawing.Point(209, 448);
            this.btnTolerance.Name = "btnTolerance";
            this.btnTolerance.Size = new System.Drawing.Size(72, 20);
            this.btnTolerance.TabIndex = 5;
            this.btnTolerance.Text = "计算残差";
            this.btnTolerance.UseVisualStyleBackColor = true;
            this.btnTolerance.Click += new System.EventHandler(this.btnTolerance_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(322, 445);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 20);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确　定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(396, 445);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 20);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取　消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // axMapControl
            // 
            this.axMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl.Location = new System.Drawing.Point(12, 34);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(453, 270);
            this.axMapControl.TabIndex = 6;
            // 
            // frmGeoReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 474);
            this.Controls.Add(this.axMapControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnTolerance);
            this.Controls.Add(this.cboTransferMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvwPointReference);
            this.Controls.Add(this.axToolbarControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGeoReference";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "影像图配准";
            this.Load += new System.EventHandler(this.frmGeoReference_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGeoReference_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl;
        private System.Windows.Forms.ListView lvwPointReference;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTransferMethod;
        private System.Windows.Forms.Button btnTolerance;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
    }
}