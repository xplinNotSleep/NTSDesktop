namespace AG.COM.SDM.DataTools.DataProcess
{
    partial class FormResetBSM
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFields = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudStartValue = new System.Windows.Forms.NumericUpDown();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.featureLayerSelector1 = new AG.COM.SDM.GeoDataBase.FeatureLayerSelector();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "标识码字段";
            // 
            // cboFields
            // 
            this.cboFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFields.FormattingEnabled = true;
            this.cboFields.Location = new System.Drawing.Point(19, 86);
            this.cboFields.Name = "cboFields";
            this.cboFields.Size = new System.Drawing.Size(248, 20);
            this.cboFields.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "起始值";
            // 
            // nudStartValue
            // 
            this.nudStartValue.Location = new System.Drawing.Point(19, 138);
            this.nudStartValue.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.nudStartValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStartValue.Name = "nudStartValue";
            this.nudStartValue.Size = new System.Drawing.Size(248, 21);
            this.nudStartValue.TabIndex = 3;
            this.nudStartValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(175, 182);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(70, 24);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(251, 182);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(70, 24);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudStartValue);
            this.groupBox1.Controls.Add(this.featureLayerSelector1);
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 176);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // featureLayerSelector1
            // 
            this.featureLayerSelector1.FeatureClass = null;
            this.featureLayerSelector1.Filter = AG.COM.SDM.GeoDataBase.FeatureLayerFilterType.lyFilterNone;
            this.featureLayerSelector1.Location = new System.Drawing.Point(19, 32);
            this.featureLayerSelector1.Name = "featureLayerSelector1";
            this.featureLayerSelector1.Size = new System.Drawing.Size(285, 25);
            this.featureLayerSelector1.TabIndex = 1;
            this.featureLayerSelector1.LayerChanged += new AG.COM.SDM.GeoDataBase.LayerChangedDelegate(this.featureLayerSelector1_LayerChanged);
            // 
            // FormResetBSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 211);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormResetBSM";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重新设置标识码";
            ((System.ComponentModel.ISupportInitialize)(this.nudStartValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private AG.COM.SDM.GeoDataBase.FeatureLayerSelector featureLayerSelector1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboFields;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudStartValue;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}