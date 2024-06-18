using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using AG.COM.SDM.Config;
using AG.COM.SDM.Config.MenuConfig;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Framework.DocumentView;
using AG.COM.SDM.Framework.DocumentView.UI;
using AG.COM.SDM.Model;
using AG.COM.SDM.Plugins;
using AG.COM.SDM.Plugins.Logger;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;

namespace AG.COM.SDM.Express
{
    /// <summary>
    /// 系统主窗体
    /// </summary>
    public partial class WinMain : RibbonForm
    {
        private IFramework m_Framework = new AG.COM.SDM.Framework.Framework();          //系统集成框架对象
        public static int timeCount = 0;
        static Timer timer = new Timer();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WinMain()
        {
            //初始化界面组件
            InitializeComponent();

            //自定义标题2017/05/15
            if (LoginDesignHelper.Instance.Init())
                this.Text = LoginDesignHelper.Instance.MainForm.Text;

            lblOperation.Caption = "当前操作:";
            lblUser.Caption = "(" + SystemInfo.UserName + ")";
            lblDate.Caption = string.Format("当前日期:{0:yyyy年MM月dd日}", DateTime.Now);

            //状态栏的运行进度
            lblProcess.EditValue = Properties.Resources.process;

            (this.m_Framework as IFrameworkEvents).PlugCommandCliked += new PlugCommandClikedEventHandler(WinMain_PlugCommandCliked);

            KeyPreview = true;


        }

        private void WinMain_Load(object sender, EventArgs e)
        {
            try
            {
                //实例化系统启动处理类
                StartupHandler tStartupHandler = new StartupHandler();
                tStartupHandler.OnCreate(this.m_Framework);//传入通信框架
                tStartupHandler.Startup();

                FormMessageFilter msg = new FormMessageFilter();
                Application.AddMessageFilter(msg);
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show("系统启动出错," + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeCount++;
            int minute = timeCount / 60;
            int second = timeCount - 60 * minute;
            lblTimer.Caption = $"系统静置时间：{minute}分{second}秒";
            if (timeCount > 600)
            {
                CommonVariables.IsClosed = true;
                //关闭应用程序
                Process.GetCurrentProcess().CloseMainWindow();
            }
            //Timer timer = sender as Timer;
            //timer.Stop();
        }


        private void WinMain_PlugCommandCliked(object sender, EventArgs e)
        {
            string strOperateMsg = string.Empty;
            if (sender is ICommand)
            {
                strOperateMsg = (sender as ICommand).Caption;
                lblOperation.Caption = "当前操作:" + strOperateMsg;

            }
            else if (sender is IPlugin)
            {
                strOperateMsg = (sender as IPlugin).Caption;
                lblOperation.Caption = "当前操作:" + strOperateMsg;
            }

            IOperateLevel operateLevel = sender as IOperateLevel;
            if (operateLevel != null && OperateLevel.Insignificance.CompareTo(operateLevel.OperateLevel) > 0)
            {
                MyLogger.GetInstance().WriteMessage(strOperateMsg, "正常操作", operateLevel.OperateLevel);
            }
            else if (operateLevel == null)
            {
                MyLogger.GetInstance().WriteMessage(strOperateMsg, "正常操作", OperateLevel.Normal);
            }
        }

        private void WinMain_Resize(object sender, EventArgs e)
        {

        }

        private void WinMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CommonVariables.IsClosed)
            {
                if (MessageBox.Show("是否关闭程序?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            CommonVariables.IsClosed = true;

        }

        /// <summary>
        /// 自定义初始化
        /// </summary>
        /// <param name="pMsgFrm">消息窗体</param>
        public void CustomInitialize(FormLogin pMsgFrm)
        {
            //设置系统集成框架相关属性         
            m_Framework.DockManager = dockManager1;
            m_Framework.DocumentManager = documentManager1;
            m_Framework.StatusBar = ribbonStatusBar1;
            m_Framework.MdiParentForm = this;
            m_Framework.LicenseUnlimited = true;

            try
            {
                //根据菜单配置内容进行菜单栏初始化
                Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer =
                   InitMenuContainer();

                pMsgFrm.SetMessage("正在初始化菜单服务……");
                //初始化菜单服务对象（实现功能入口点与界面控件进行通信）
                IMenuService pMenuService = new MenuService(m_Framework);
                pMenuService.Init(tRootContainer);
                m_Framework.AddService(typeof(IMenuService), pMenuService);

                #region 工具栏服务-跟菜单服务应该是重复的
                //pMsgFrm.SetMessage("正在初始化工具栏服务……");
                ////初始化工具栏服务对象
                //IToolBarService pToolBarService = new ToolBarService(m_Framework);
                //pToolBarService.Init(tRootContainer);
                //m_Framework.AddService(typeof(IToolBarService), pToolBarService);
                #endregion

                pMsgFrm.SetMessage("正在初始化文档对象窗体服务……");
                //初始化文档服务对象
                IDockDocumentService pDockDocService = new DockDocumentService(m_Framework);
                m_Framework.AddService(typeof(IDockDocumentService), pDockDocService);

                pMsgFrm.SetMessage("正在初始化数据文档视图……");
                //初始化数据文档对象
                TocDocument pTocDocument = new TocDocument(m_Framework);
                pDockDocService.AddDockDocument(Convert.ToString(EnumDocumentType.TocDocument), pTocDocument, DockState.Left);
                pTocDocument.DockPanel.Width = 300;

                pMsgFrm.SetMessage("正在初始化主体窗口……");
                MainDocument pMainDocument = new MainDocument(m_Framework);
                pMainDocument.DocumentTitle = "主窗口";
                pDockDocService.AddDockDocument(Convert.ToString(EnumDocumentType.MainDocument), pMainDocument, DockState.Document);

                pMsgFrm.SetMessage("正在初始化辅助模块窗口……");
                SubDocument pSubDocument = new SubDocument(m_Framework);
                pSubDocument.DocumentTitle = "辅助窗口";
                pDockDocService.AddDockDocument(Convert.ToString(EnumDocumentType.SubDocument), pSubDocument,
                    pTocDocument, DockState.Bottom);

                //帮助文档对象
                HelpDocument tHelpDocument = new HelpDocument();
                pDockDocService.AddDockDocument(Convert.ToString(EnumDocumentType.HelpDocument), tHelpDocument, DockState.Right);
                tHelpDocument.AutoHide = true;

                InitSkin();

                //使用上次的皮肤
                DevThemeHelper.ChangeTheme(CommonConstString.STR_TempPath,
                    CommonVariables.ConfigFile, CommonVariables.CurrentSkinName,
                    CommonVariables.CurrentSkinName);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 从数据库中初始化菜单内容
        /// </summary>
        /// <returns></returns>
        private Dictionary<UIDesignControl, List<UIDesignControl>> InitMenuContainer()
        {
            Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer =
                new Dictionary<UIDesignControl, List<UIDesignControl>>();
            List<AGSDM_UIMENU> menu = null;
            //从数据库加载菜单
            menu = MenuLoadHelper.LoadMenuConfigEntity(UIDesignFrom.Database).ToList();
            //根据当前登录角色过滤菜单
            menu = MenuLoadHelper.FilterCurrentRoleFun(menu);

            //根据菜单配置加载控件到菜单
            MenuLoadHelper menuLoadHelper = new MenuLoadHelper();
            menuLoadHelper.InitMenu(menu, ribbonControl1, tRootContainer);
            return tRootContainer;
        }

        /// <summary>
        /// 在“系统设置”菜单栏下添加devexpress的换肤按钮
        /// </summary>
        void InitSkin()
        {
            DevExpress.XtraBars.RibbonGalleryBarItem skinEx;
            DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEx;
            DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEx;
            skinEx = new DevExpress.XtraBars.RibbonGalleryBarItem();

            ribbonPageGroupEx = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            skinEx.Caption = "skin";
            skinEx.Name = "skin";
            //换肤按钮放在哪个page下面，如果没有则新建
            string strribbonPage = "系统设置";

            ribbonPageEx = ribbonControl1.Pages.GetPageByText(strribbonPage);
            bool IsibbonPageNull = false;
            if (ribbonPageEx == null)
            {
                ribbonPageEx = new DevExpress.XtraBars.Ribbon.RibbonPage();
                ribbonPageEx.Name = "ribbonPage2Ex";
                ribbonPageEx.Text = strribbonPage;
                IsibbonPageNull = true;

            }
            ribbonPageEx.Groups.AddRange(new RibbonPageGroup[] {
            ribbonPageGroupEx});
            ribbonPageGroupEx.ItemLinks.Add(skinEx);
            ribbonPageGroupEx.Name = "ribbonPageGroupEx";
            ribbonPageGroupEx.Text = "系统皮肤";
            ribbonPageGroupEx.ShowCaptionButton = false;
            if (IsibbonPageNull == true)
            {
                RibbonPage[] ribbonPages = new RibbonPage[] { ribbonPageEx };
                if (ribbonControl1.Pages.Count > 0)
                    ribbonControl1.Pages.AddRange(ribbonPages);
            }
            this.ribbonControl1.Items.Add(skinEx);
            skinEx.GalleryItemClick += SkinEx_GalleryItemClick;
            SkinHelper.InitSkinGallery(skinEx);
        }
        private void SkinEx_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            string ss = string.Empty;

            //使用上次的皮肤
            DevThemeHelper.ChangeTheme(CommonConstString.STR_TempPath,
                    CommonVariables.ConfigFile, e.Gallery.GetCheckedItem().Caption
                    , CommonVariables.CurrentSkinName);

        }

        /// <summary>
        /// 更新地图比例尺的状态显示
        /// </summary>
        /// <param name="displayTransformation"></param>
        /// <param name="sizeChanged"></param>
        /// <param name="newEnvelope"></param>

        private void WinMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.ribbonControl1.Width = this.Width;
            }
            else
            {
                this.ribbonControl1.Width = this.Width;
            }
        }

        internal class FormMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                //如果检测到有鼠标或则键盘的消息，则使计数为0.....
                if (m.Msg == 0x0200 || m.Msg == 0x0201 || m.Msg == 0x0204 || m.Msg == 0x0207)
                {
                    timer.Stop();
                    timeCount = 0;
                }
                else
                {
                    timer.Interval = 1000;
                    timer.Start();
                }
                return false;
            }

        }

    }




}