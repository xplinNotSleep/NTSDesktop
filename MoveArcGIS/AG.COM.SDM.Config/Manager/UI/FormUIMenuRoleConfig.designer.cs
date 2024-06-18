using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    partial class FormUIMenuRoleConfig
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUIMenuRoleConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvwUIDesign = new System.Windows.Forms.TreeView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tvwUIDesign);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 551);
            this.panel1.TabIndex = 0;
            // 
            // tvwUIDesign
            // 
            this.tvwUIDesign.CheckBoxes = true;
            this.tvwUIDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwUIDesign.ImageIndex = 0;
            this.tvwUIDesign.ImageList = this.imageList1;
            this.tvwUIDesign.ItemHeight = 20;
            this.tvwUIDesign.Location = new System.Drawing.Point(0, 0);
            this.tvwUIDesign.Name = "tvwUIDesign";
            this.tvwUIDesign.SelectedImageIndex = 0;
            this.tvwUIDesign.Size = new System.Drawing.Size(433, 551);
            this.tvwUIDesign.TabIndex = 0;
            this.tvwUIDesign.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwUIDesign_AfterCheck);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(260, 559);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(341, 559);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Control_MenuStrip.png");
            this.imageList1.Images.SetKeyName(1, "Control_ToolBar.png");
            this.imageList1.Images.SetKeyName(2, "Control_Button.png");
            this.imageList1.Images.SetKeyName(3, "ComboBox16.png");
            this.imageList1.Images.SetKeyName(4, "Text16.png");
            this.imageList1.Images.SetKeyName(5, "CustomControl.png");
            this.imageList1.Images.SetKeyName(6, "HtmlBalanceBracesHS.png");
            this.imageList1.Images.SetKeyName(7, "RibbonPage.png");
            this.imageList1.Images.SetKeyName(8, "RibbonPageGroup.png");
            // 
            // FormUIMenuRoleConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 592);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "FormUIMenuRoleConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "菜单权限设置";
            this.Load += new System.EventHandler(this.FormUIDesignRoleConfig_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvwUIDesign;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}