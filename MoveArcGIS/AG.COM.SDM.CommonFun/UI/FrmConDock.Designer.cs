
namespace AG.COM.SDM.CommonFun.UI
{
    partial class FrmConDock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dockDocument1 = new AG.COM.SDM.SystemUI.DockDocument();
            this.SuspendLayout();
            // 
            // dockDocument1
            // 
            this.dockDocument1.AutoHide = false;
            this.dockDocument1.DockPanel = null;
            this.dockDocument1.DockVisibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            this.dockDocument1.Location = new System.Drawing.Point(64, 24);
            this.dockDocument1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dockDocument1.Name = "dockDocument1";
            this.dockDocument1.Size = new System.Drawing.Size(553, 413);
            this.dockDocument1.TabIndex = 0;
            this.dockDocument1.TabText = "";
            // 
            // FrmConDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dockDocument1);
            this.Name = "FrmConDock";
            this.Text = "FrmConDock";
            this.ResumeLayout(false);

        }

        #endregion

        private SystemUI.DockDocument dockDocument1;
    }
}