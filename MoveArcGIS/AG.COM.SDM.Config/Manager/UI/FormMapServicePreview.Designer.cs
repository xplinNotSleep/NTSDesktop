namespace AG.COM.SDM.Config
{
    partial class FormMapServicePreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMapServicePreview));
            this.mapMain = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.mapMain)).BeginInit();
            this.SuspendLayout();
            // 
            // mapMain
            // 
            this.mapMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapMain.Location = new System.Drawing.Point(0, 0);
            this.mapMain.Name = "mapMain";
            this.mapMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapMain.OcxState")));
            this.mapMain.Size = new System.Drawing.Size(923, 464);
            this.mapMain.TabIndex = 0;
            // 
            // FormMapServicePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 464);
            this.Controls.Add(this.mapMain);
            this.MinimizeBox = false;
            this.Name = "FormMapServicePreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预览服务";
            this.Load += new System.EventHandler(this.FormMapServicePreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl mapMain;
    }
}