namespace AG.COM.SDM.DataTools.DataProcess
{
    partial class FormCreateFieldValueBySpatialRel
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
            this.cboDestFields = new System.Windows.Forms.ComboBox();
            this.cboSrcFields = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dsDestSelector = new AG.COM.SDM.GeoDataBase.FeatureLayerSelector();
            this.dsSrcSelector = new AG.COM.SDM.GeoDataBase.FeatureLayerSelector();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboDestFields
            // 
            this.cboDestFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestFields.FormattingEnabled = true;
            this.cboDestFields.Location = new System.Drawing.Point(24, 131);
            this.cboDestFields.Name = "cboDestFields";
            this.cboDestFields.Size = new System.Drawing.Size(105, 20);
            this.cboDestFields.TabIndex = 1;
            this.cboDestFields.SelectedIndexChanged += new System.EventHandler(this.cboDestFields_SelectedIndexChanged);
            // 
            // cboSrcFields
            // 
            this.cboSrcFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSrcFields.FormattingEnabled = true;
            this.cboSrcFields.Location = new System.Drawing.Point(163, 131);
            this.cboSrcFields.Name = "cboSrcFields";
            this.cboSrcFields.Size = new System.Drawing.Size(105, 20);
            this.cboSrcFields.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "=";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "属性来源图层";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "要生成属性的图层(只支持面层）";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "字段规则";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(256, 197);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(70, 24);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(175, 197);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(70, 24);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dsDestSelector);
            this.groupBox1.Controls.Add(this.dsSrcSelector);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboDestFields);
            this.groupBox1.Controls.Add(this.cboSrcFields);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 179);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // dsDestSelector
            // 
            this.dsDestSelector.FeatureClass = null;
            this.dsDestSelector.Filter = AG.COM.SDM.GeoDataBase.FeatureLayerFilterType.lyFilterArea;
            this.dsDestSelector.Location = new System.Drawing.Point(24, 32);
            this.dsDestSelector.Name = "dsDestSelector";
            this.dsDestSelector.Size = new System.Drawing.Size(280, 25);
            this.dsDestSelector.TabIndex = 0;
            this.dsDestSelector.LayerChanged += new AG.COM.SDM.GeoDataBase.LayerChangedDelegate(this.dsDestSelector_LayerChanged);
            // 
            // dsSrcSelector
            // 
            this.dsSrcSelector.FeatureClass = null;
            this.dsSrcSelector.Filter = AG.COM.SDM.GeoDataBase.FeatureLayerFilterType.lyFilterNone;
            this.dsSrcSelector.Location = new System.Drawing.Point(24, 81);
            this.dsSrcSelector.Name = "dsSrcSelector";
            this.dsSrcSelector.Size = new System.Drawing.Size(280, 25);
            this.dsSrcSelector.TabIndex = 0;
            // 
            // FormCreateFieldValueBySpatialRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 224);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateFieldValueBySpatialRel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "根据空间关系获取字段值";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AG.COM.SDM.GeoDataBase.FeatureLayerSelector dsDestSelector;
        private AG.COM.SDM.GeoDataBase.FeatureLayerSelector dsSrcSelector;
        private System.Windows.Forms.ComboBox cboDestFields;
        private System.Windows.Forms.ComboBox cboSrcFields;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}