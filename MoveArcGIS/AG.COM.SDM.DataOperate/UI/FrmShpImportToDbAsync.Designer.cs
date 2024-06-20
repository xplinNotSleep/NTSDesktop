
namespace AG.COM.SDM.DataOperate
{
    partial class FrmShpImportToDbAsync
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDbSet = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.IsCheckOverload = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(168, 69);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(411, 25);
            this.txtSource.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "导入源数据文件夹";
            // 
            // btnDbSet
            // 
            this.btnDbSet.Location = new System.Drawing.Point(22, 186);
            this.btnDbSet.Name = "btnDbSet";
            this.btnDbSet.Size = new System.Drawing.Size(141, 44);
            this.btnDbSet.TabIndex = 3;
            this.btnDbSet.Text = "数据库设置";
            this.btnDbSet.UseVisualStyleBackColor = true;
            this.btnDbSet.Click += new System.EventHandler(this.btnDbSet_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(648, 186);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(108, 45);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "确认导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(629, 60);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(127, 39);
            this.btnSource.TabIndex = 8;
            this.btnSource.Text = "文件夹路径";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // IsCheckOverload
            // 
            this.IsCheckOverload.AutoSize = true;
            this.IsCheckOverload.Location = new System.Drawing.Point(370, 134);
            this.IsCheckOverload.Name = "IsCheckOverload";
            this.IsCheckOverload.Size = new System.Drawing.Size(209, 19);
            this.IsCheckOverload.TabIndex = 9;
            this.IsCheckOverload.Text = "是否覆盖数据库中同名数据";
            this.IsCheckOverload.UseVisualStyleBackColor = true;
            // 
            // FrmShpImportToDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.IsCheckOverload);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnDbSet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Name = "FrmShpImportToDb";
            this.Size = new System.Drawing.Size(811, 410);
            this.TabText = "矢量数据入库";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDbSet;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.CheckBox IsCheckOverload;
    }
}
