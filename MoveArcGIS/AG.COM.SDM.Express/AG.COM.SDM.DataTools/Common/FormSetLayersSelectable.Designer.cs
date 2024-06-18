namespace AG.COM.SDM.DataTools.Common
{
    partial class FormSetLayersSelectable
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetLayersSelectable));
            this.label1 = new System.Windows.Forms.Label();
            this.butSelectAll = new System.Windows.Forms.Button();
            this.butClearAll = new System.Windows.Forms.Button();
            this.butClose = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.MltSetLayersSelectable = new AG.COM.SDM.Utility.Controls.MapLayersTreeView2();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置图层要素是否可被选中";
            // 
            // butSelectAll
            // 
            this.butSelectAll.Location = new System.Drawing.Point(12, 292);
            this.butSelectAll.Name = "butSelectAll";
            this.butSelectAll.Size = new System.Drawing.Size(70, 24);
            this.butSelectAll.TabIndex = 2;
            this.butSelectAll.Text = "选择所有";
            this.butSelectAll.UseVisualStyleBackColor = true;
            this.butSelectAll.Click += new System.EventHandler(this.butSelectAll_Click);
            // 
            // butClearAll
            // 
            this.butClearAll.Location = new System.Drawing.Point(88, 292);
            this.butClearAll.Name = "butClearAll";
            this.butClearAll.Size = new System.Drawing.Size(70, 24);
            this.butClearAll.TabIndex = 3;
            this.butClearAll.Text = "清除选择";
            this.butClearAll.UseVisualStyleBackColor = true;
            this.butClearAll.Click += new System.EventHandler(this.butClearAll_Click);
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(191, 292);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(70, 24);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "确定";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(267, 292);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(70, 24);
            this.butCancel.TabIndex = 4;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // MltSetLayersSelectable
            // 
            this.MltSetLayersSelectable.CheckBoxes = true;
            this.MltSetLayersSelectable.ImageIndex = 0;
            this.MltSetLayersSelectable.ItemHeight = 20;
            this.MltSetLayersSelectable.Location = new System.Drawing.Point(14, 28);
            this.MltSetLayersSelectable.Name = "MltSetLayersSelectable";
            this.MltSetLayersSelectable.SelectedImageIndex = 0;
            this.MltSetLayersSelectable.Size = new System.Drawing.Size(324, 258);
            this.MltSetLayersSelectable.TabIndex = 5;
            // 
            // FormSetLayersSelectable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 323);
            this.Controls.Add(this.MltSetLayersSelectable);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butClearAll);
            this.Controls.Add(this.butSelectAll);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetLayersSelectable";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置可选图层";
            this.Load += new System.EventHandler(this.frmSetLayersSelectable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butSelectAll;
        private System.Windows.Forms.Button butClearAll;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butCancel;
        private AG.COM.SDM.Utility.Controls.MapLayersTreeView2 MltSetLayersSelectable;
    }
}