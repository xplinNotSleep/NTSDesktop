namespace AG.COM.SDM.Framework.DocumentView.UI
{
    partial class SubDocument
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
            this.SubgroupBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // SubgroupBox
            // 
            this.SubgroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubgroupBox.Location = new System.Drawing.Point(0, 0);
            this.SubgroupBox.Name = "SubgroupBox";
            this.SubgroupBox.Size = new System.Drawing.Size(326, 283);
            this.SubgroupBox.TabIndex = 0;
            this.SubgroupBox.TabStop = false;
            this.SubgroupBox.Text = "辅助模块";
            // 
            // SubDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SubgroupBox);
            this.Name = "SubDocument";
            this.Size = new System.Drawing.Size(326, 283);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SubgroupBox;
    }
}
