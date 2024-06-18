using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    partial class TocDocumentCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TocDocument));
            this.ActiveControl = new Control();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveControl)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxControl
            // 
            this.listBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControl.Location = new System.Drawing.Point(0, 0);
            this.listBoxControl.Name = "listBoxControl";
            //this.listBoxControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("listBoxControl.OcxState")));
            this.listBoxControl.Size = new System.Drawing.Size(268, 396);
            this.listBoxControl.TabIndex = 0;
            this.listBoxControl.MouseDown += new MouseEventHandler(this.listBoxControl_OnMouseDown);
            this.listBoxControl.MouseUp += new MouseEventHandler(this.listBoxControl_OnMouseUp);
            // 
            // TocDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxControl);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "TocDocument";
            this.Size = new System.Drawing.Size(268, 396);
            this.TabText = "图层视图";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ListBoxControl listBoxControl;
    }
}