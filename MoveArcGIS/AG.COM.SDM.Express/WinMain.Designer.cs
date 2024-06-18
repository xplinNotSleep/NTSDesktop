namespace AG.COM.SDM.Express
{
    partial class WinMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinMain));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lblOperation = new DevExpress.XtraBars.BarStaticItem();
            this.lblProcess = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.lblDate = new DevExpress.XtraBars.BarStaticItem();
            this.lblMousePos = new DevExpress.XtraBars.BarStaticItem();
            this.lblUser = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.lblTimer = new DevExpress.XtraBars.BarStaticItem();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideSpeed = 10;
            this.dockManager1.DockingOptions.HideImmediatelyOnAutoHide = true;
            this.dockManager1.DockModeVS2005FadeSpeed = 10;
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.lblOperation,
            this.lblProcess,
            this.lblDate,
            this.lblMousePos,
            this.lblUser,
            this.lblTimer});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 16;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.Size = new System.Drawing.Size(845, 58);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // lblOperation
            // 
            this.lblOperation.Caption = "当前操作：";
            this.lblOperation.Id = 10;
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lblOperation.Width = 200;
            // 
            // lblProcess
            // 
            this.lblProcess.Edit = this.repositoryItemPictureEdit1;
            this.lblProcess.Id = 11;
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Width = 20;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.repositoryItemPictureEdit1.Appearance.Options.UseBackColor = true;
            this.repositoryItemPictureEdit1.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseBackColor = true;
            this.repositoryItemPictureEdit1.AppearanceFocused.BackColor = System.Drawing.Color.Transparent;
            this.repositoryItemPictureEdit1.AppearanceFocused.Options.UseBackColor = true;
            this.repositoryItemPictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // lblDate
            // 
            this.lblDate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.lblDate.Id = 12;
            this.lblDate.Name = "lblDate";
            this.lblDate.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblMousePos
            // 
            this.lblMousePos.Id = 13;
            this.lblMousePos.Name = "lblMousePos";
            this.lblMousePos.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lblMousePos.Width = 150;
            // 
            // lblUser
            // 
            this.lblUser.Id = 14;
            this.lblUser.Name = "lblUser";
            this.lblUser.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lblUser.Width = 100;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.lblOperation);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblDate);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblMousePos);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblUser);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblProcess);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblTimer);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 450);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(845, 34);
            // 
            // lblTimer
            // 
            this.lblTimer.Id = 15;
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lblTimer.Width = 150;
            // 
            // WinMain
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.ClientSize = new System.Drawing.Size(845, 484);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WinMain";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "DevCS端";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinMain_FormClosing);
            this.Load += new System.EventHandler(this.WinMain_Load);
            this.SizeChanged += new System.EventHandler(this.WinMain_SizeChanged);
            this.Resize += new System.EventHandler(this.WinMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarStaticItem lblOperation;
        private DevExpress.XtraBars.BarEditItem lblProcess;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraBars.BarStaticItem lblDate;
        private DevExpress.XtraBars.BarStaticItem lblMousePos;
        private DevExpress.XtraBars.BarStaticItem lblUser;
        private DevExpress.XtraBars.BarStaticItem lblTimer;
    }
}

