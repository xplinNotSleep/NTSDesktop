﻿
namespace AG.COM.SDM.Framework.DocumentView
{
    partial class TocDocument
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
            this.TocTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // TocTreeView
            // 
            this.TocTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TocTreeView.Location = new System.Drawing.Point(0, 0);
            this.TocTreeView.Name = "TocTreeView";
            this.TocTreeView.Size = new System.Drawing.Size(311, 333);
            this.TocTreeView.TabIndex = 0;
            this.TocTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TocTreeView_OnMouseDown);
            // 
            // TocDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TocTreeView);
            this.Name = "TocDocument";
            this.Size = new System.Drawing.Size(311, 333);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TocTreeView;
    }
}
