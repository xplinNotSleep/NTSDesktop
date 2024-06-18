namespace AG.COM.SDM.DataTools.DataProcess
{
    partial class FormExecuteSQL
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
            this.txtSql = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btGenSQL = new System.Windows.Forms.Button();
            this.featureLayerSelector1 = new AG.COM.SDM.GeoDataBase.FeatureLayerSelector();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择要更新属性图层(不支持Shape文件）";
            // 
            // txtSql
            // 
            this.txtSql.Location = new System.Drawing.Point(14, 80);
            this.txtSql.Multiline = true;
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(337, 143);
            this.txtSql.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "SQL Update语句(set后面的部份,多个语句换行隔开）";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(281, 229);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(70, 24);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(205, 229);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(70, 24);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btGenSQL
            // 
            this.btGenSQL.Location = new System.Drawing.Point(14, 229);
            this.btGenSQL.Name = "btGenSQL";
            this.btGenSQL.Size = new System.Drawing.Size(80, 24);
            this.btGenSQL.TabIndex = 4;
            this.btGenSQL.Text = "生成语句...";
            this.btGenSQL.UseVisualStyleBackColor = true;
            this.btGenSQL.Click += new System.EventHandler(this.btGenSQL_Click);
            // 
            // featureLayerSelector1
            // 
            this.featureLayerSelector1.FeatureClass = null;
            this.featureLayerSelector1.Filter = AG.COM.SDM.GeoDataBase.FeatureLayerFilterType.lyFilterNone;
            this.featureLayerSelector1.Location = new System.Drawing.Point(12, 27);
            this.featureLayerSelector1.Name = "featureLayerSelector1";
            this.featureLayerSelector1.Size = new System.Drawing.Size(344, 25);
            this.featureLayerSelector1.TabIndex = 0;
            // 
            // FormExecuteSQL
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(363, 258);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btGenSQL);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.featureLayerSelector1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExecuteSQL";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "使用SQL更新数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AG.COM.SDM.GeoDataBase.FeatureLayerSelector featureLayerSelector1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btGenSQL;
    }
}