
namespace AG.COM.SDM.Framework.DocumentView
{
    partial class GlobeDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobeDocument));
            this.axGlobeControl1 = new ESRI.ArcGIS.Controls.AxGlobeControl();
            this.tmr2Dto3D = new System.Windows.Forms.Timer();
            this.tmr3Dto2D = new System.Windows.Forms.Timer();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            ((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axGlobeControl1
            // 
            this.axGlobeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGlobeControl1.Location = new System.Drawing.Point(0, 28);
            this.axGlobeControl1.Name = "axGlobeControl1";
            this.axGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGlobeControl1.OcxState")));
            this.axGlobeControl1.Size = new System.Drawing.Size(800, 422);
            this.axGlobeControl1.TabIndex = 0;
            this.axGlobeControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IGlobeControlEvents_Ax_OnMouseUpEventHandler(this.axGlobeControl1_OnMouseUp);
            // 
            // tmr2Dto3D
            // 
            this.tmr2Dto3D.Interval = 500;
            this.tmr2Dto3D.Tick += new System.EventHandler(this.tmr2Dto3D_Tick);
            // 
            // tmr3Dto2D
            // 
            this.tmr3Dto2D.Interval = 500;
            this.tmr3Dto2D.Tick += new System.EventHandler(this.tmr3Dto2D_Tick);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(800, 28);
            this.axToolbarControl1.TabIndex = 1;
            // 
            // GlobeDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axGlobeControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Name = "GlobeDocument";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl1;
        private System.Windows.Forms.Timer tmr2Dto3D;
        private System.Windows.Forms.Timer tmr3Dto2D;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
    }
}