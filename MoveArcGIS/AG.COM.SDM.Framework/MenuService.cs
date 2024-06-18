using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework.DocumentView;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 菜单服务类
    /// </summary>
    public class MenuService : IMenuService
    {
        #region 变量

        private IFramework m_Framework = null;
        /// <summary>
        /// 带有结构的所有菜单项集合，key为RibbonPage或Mainmenu，value为key下的菜单项
        /// </summary>
        private Dictionary<UIDesignControl, List<UIDesignControl>> m_RootMenus = null;
        /// <summary>
        /// 所有菜单项集合
        /// </summary>
        private List<UIDesignControl> m_MenuItems = null;
        private IPluginsService m_PluginsService = null;
        /// <summary>
        /// 定时刷新Enabled和Check的Timer
        /// </summary>
        private Timer m_Timer;

        public string HelpFile = "";

        #endregion

        #region 初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mFramework">IFramework对象</param>
        public MenuService(IFramework mFramework)
        {
            this.m_Framework = mFramework;
            this.m_RootMenus = new Dictionary<UIDesignControl, List<UIDesignControl>>();
            this.m_MenuItems = new List<UIDesignControl>();
            this.m_PluginsService = PluginsService.GetInstance(mFramework);

            this.m_Timer = new Timer();
            this.m_Timer.Interval = 1000;

            //this.m_Timer.Tick += new EventHandler(OnTimedEvent);
        }

        /// <summary>
        /// 初始化菜单服务，绑定功能到控件
        /// </summary>
        /// <param name="tRootContainer"></param>
        public void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer)
        {
            //是否绑定第一页
            bool hasBindFirstPage = false;

            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in tRootContainer)
            {
                if (tRootControl.Key.Control is RibbonPage)
                {
                    m_RootMenus.Add(tRootControl.Key, tRootControl.Value);

                    //支持一种菜单，RibbonPage
                    if (tRootControl.Key.Control is RibbonPage)
                    {
                        #region 添加当前菜单页更换时的激活事件
                        if (hasBindFirstPage == false)
                        {
                            //只需要绑定一次SelectedPageChanged事件
                            RibbonPage ribbonPage = tRootControl.Key.Control as RibbonPage;
                            ribbonPage.Ribbon.SelectedPageChanged += new EventHandler(QRibbonPage_Activated);
                            hasBindFirstPage = true;
                        }
                        #endregion

                        foreach (UIDesignControl tUIDesignMenuItem in tRootControl.Value)
                        {
                            BindRibbonItem(tUIDesignMenuItem);
                        }
                    }
                }
            }

            RefreshActiveRibbonPage();
        }

        /// <summary>
        /// 绑定RibbonItem
        /// </summary>
        /// <param name="tUIDesignMenuItem"></param>
        private void BindRibbonItem(UIDesignControl tUIDesignMenuItem)
        {

            if (tUIDesignMenuItem.Control is BarItem)
            {
                BarItem barItem = tUIDesignMenuItem.Control as BarItem;

                //实例化插件
                PlugInfoConfig plugInfo = PlugInProvider.GetPlugInfoConfig(tUIDesignMenuItem.BindFun);
                object pObjInstance = PlugInProvider.GetInstance(plugInfo);

                if (pObjInstance is ICommand)
                {
                    //ICommand的Name不能为空
                    string commandName = (pObjInstance as ICommand).Name;
                    if (string.IsNullOrEmpty(commandName))
                    {
                        throw new Exception("插件（" + pObjInstance.GetType().Name + "）的Name为空，不能加载");
                    }

                    if (barItem is BarButtonItem)
                    {
                        BarButtonItem barButtonItem = barItem as BarButtonItem;

                        //加载图片
                        Image image = BindFunHelper.GetRibbonItemIcon(pObjInstance, barItem.RibbonStyle == RibbonItemStyles.Large ? 32 : 16);
                        if (image != null)
                        {
                            //为了提高系统启动速度，在绑定菜单时把图标先放到内存中，在需要时在设置到控件
                            tUIDesignMenuItem.Image = image;

                            barItem.Glyph = image;

                            tUIDesignMenuItem.HasInit = true;
                        }

                        //为了实现checked（选中）效果，要把ButtonStyle设为BarButtonStyle.Check
                        barButtonItem.ButtonStyle = BarButtonStyle.Check;

                        //菜单条单击事件                      
                        barButtonItem.ItemClick += new ItemClickEventHandler(tQRibbonItem_ItemClick);
                    }

                    tUIDesignMenuItem.Plugin = pObjInstance;

                    this.m_PluginsService.AddPlugin(commandName, pObjInstance);
                    this.m_MenuItems.Add(tUIDesignMenuItem);

                    if (pObjInstance is IComboBoxTreeView)
                    {
                        BindComboBoxTreeViewToRibbonItem(pObjInstance, barItem as BarEditItem);
                    }
                    else if (pObjInstance is IComboBox)
                    {
                        BindComboboxToRibbonItem(pObjInstance, barItem as BarEditItem);
                    }
                }

            }
        }

        /// <summary>
        /// 绑定RibbonItem的ComboBoxTreeView
        /// </summary>
        /// <param name="tCommand"></param>
        /// <param name="barEditItem"></param>
        private void BindComboBoxTreeViewToRibbonItem(object tCommand, BarEditItem barEditItem)
        {
            IComboBoxTreeView comboBoxTreeView = tCommand as IComboBoxTreeView;
            if (comboBoxTreeView != null && barEditItem != null)
            {
                comboBoxTreeView.BarEditItem = barEditItem;

                RepositoryItemPopupContainerEdit popupContainer = barEditItem.Edit as RepositoryItemPopupContainerEdit;

                barEditItem.Width = comboBoxTreeView.Width;

                comboBoxTreeView.TreeView.Dock = DockStyle.Fill;
                PopupContainerControl popupControl = new PopupContainerControl();

                //Height要写在这里，不能写到barEditItem
                popupControl.Height = comboBoxTreeView.Height;

                popupControl.Controls.Add(comboBoxTreeView.TreeView);

                popupContainer.PopupControl = popupControl;

                popupContainer.Popup += new EventHandler(comboBoxTreeView.OnDropDown);
            }
        }

        /// <summary>
        /// 绑定RibbonItem的Combobox
        /// </summary>
        /// <param name="command"></param>
        /// <param name="barEditItem"></param>
        private void BindComboboxToRibbonItem(object command, BarEditItem barEditItem)
        {
            BaseComboBox baseComboBox = command as BaseComboBox;
            if (baseComboBox != null && barEditItem != null)
            {
                //实现BaseComboBox的LabelText
                if (baseComboBox.LabelText.Length > 0)
                {
                    barEditItem.Caption = baseComboBox.LabelText;
                }
                else
                {
                    barEditItem.Caption = "";
                }

                barEditItem.Width = baseComboBox.Width;

                RepositoryItemComboBox combobox = barEditItem.Edit as RepositoryItemComboBox;

                //Dev只能接受数组，因此要把object转为数组
                object[] objArr = (baseComboBox.DataSource as System.Collections.IList).OfType<object>().ToArray();

                combobox.Items.AddRange(objArr);

                //ComboBox事件设置
                combobox.KeyDown += new KeyEventHandler(baseComboBox.OnKeyDown);
                combobox.KeyUp += new KeyEventHandler(baseComboBox.OnKeyUp);
                combobox.KeyPress += new KeyPressEventHandler(baseComboBox.OnKeyPress);
                combobox.SelectedIndexChanged += new EventHandler(baseComboBox.OnSelectedIndexChanged);
            }
        }

        #endregion

        #region RibbonItem刷新状态

        /// <summary>
        /// 定时刷新Ribbon事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void OnTimedEvent(object sender, EventArgs e)
        {

            //RefreshActiveRibbonPage();

        }

        /// <summary>
        /// 刷新当前显示的RibbonItem状态
        /// </summary>
        private void RefreshActiveRibbonPage()
        {
            if (m_Framework.EnabledMenuRefresh == false) return;

            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in m_RootMenus)
            {
                RibbonPage tQRibbonPage = tRootControl.Key.Control as RibbonPage;
                if (tQRibbonPage != null)
                {
                    //只刷新当前选择的Page
                    if (tQRibbonPage.Ribbon.SelectedPage == tQRibbonPage)
                    {
                        RefreshChildRibbonItem(tQRibbonPage);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 刷新某控件的子RibbonItem状态
        /// </summary>
        /// <param name="tParentItem">父控件</param>
        private void RefreshChildRibbonItem(object tParentItem)
        {
            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in m_RootMenus)
            {
                //遍历找到要刷新的父控件
                if (tRootControl.Key.Control == tParentItem)
                {
                    foreach (UIDesignControl tUIDesignControl in tRootControl.Value)
                    {
                        BarItem barItem = tUIDesignControl.Control as BarItem;
                        BarButtonItem barButtonItem = tUIDesignControl.Control as BarButtonItem;
                        ICommand tCommand = tUIDesignControl.Plugin as ICommand;
                        if (tCommand != null)
                        {
                            tCommand.OnCreate(m_Framework);

                            if (barItem != null)
                            {
                                //加载图标
                                if (tUIDesignControl.HasInit == false)
                                {
                                    if (tUIDesignControl.Image != null)
                                    {
                                        barItem.Glyph = tUIDesignControl.Image;
                                    }

                                    tUIDesignControl.HasInit = true;
                                }

                                barItem.Enabled = GetPluginEnabled(tCommand);

                                if (barButtonItem != null)
                                {
                                    //由于设置Check较耗资源，因此有此判断
                                    bool newCheck = GetMenuItemChecked(tCommand);
                                    if (barButtonItem.Down != newCheck)
                                    {
                                        barButtonItem.Down = newCheck;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// RibbonPage显示时刷新RibbonItem状态(切换菜单组时)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QRibbonPage_Activated(object sender, EventArgs e)
        {
            RefreshActiveRibbonPage();

        }

        /// <summary>
        /// 获取菜单项的Checked
        /// </summary>
        /// <param name="tCommand"></param>
        /// <param name="tCurrentTool"></param>
        /// <returns></returns>
        private bool GetMenuItemChecked(ICommand tCommand)
        {
            return tCommand.Checked;

        }

        /// <summary>
        /// 获取插件的Enabled
        /// </summary>
        /// <param name="tCommand"></param>
        /// <returns></returns>
        private bool GetPluginEnabled(ICommand tCommand)
        {
            //如果不是无限制许可，则要验证该插件是否有许可权限
            if (m_Framework.LicenseUnlimited == false)
            {
                string pluginClassName = tCommand.GetType().FullName;
                if (m_Framework.HasLicPlugins.Contains(pluginClassName) == false)
                {
                    return false;
                }
            }

            return tCommand.Enabled;
        }

        #endregion

        #region 菜单项单击事件

        private void tQRibbonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem tQRibbonItem = e.Item as BarButtonItem;

            //RefreshActiveRibbonPage();//在实现点击事件前先进行通信激活

            MenuItemOnClick(e.Item, tQRibbonItem.Caption);

        }

        /// <summary>
        /// 菜单项单击
        /// </summary>
        /// <param name="objMenuItem"></param>
        /// <param name="MenuItemText"></param>
        private void MenuItemOnClick(object objMenuItem, string MenuItemText)
        {

            if (objMenuItem == null) return;

            object objPlugin = null;

            //找到菜单项对应的插件
            foreach (UIDesignControl tUIDesignMenuItem in m_MenuItems)
            {
                if (tUIDesignMenuItem.Control == objMenuItem)
                {
                    objPlugin = tUIDesignMenuItem.Plugin;
                    break;
                }
            }

            //获取文档对象服务接口
            IDockDocumentService tDockDocumentService = this.m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService;
            if (tDockDocumentService.ContainsDocument(EnumDocumentType.HelpDocument.ToString()))
            {
                IHelpDocumentView tHelpDocument = tDockDocumentService.GetDockDocument(EnumDocumentType.HelpDocument.ToString()) as IHelpDocumentView;
                string strHelpFile = HelpFile;
                tHelpDocument.DisplayHelpFile(strHelpFile);
            }

            if (objPlugin is ICommand)
            {
                ICommand pCommand = objPlugin as ICommand;
                if (pCommand != null) pCommand.OnClick();
            }

            //激发插件对象单击事件
            this.m_Framework.OnPlugCommandClicked(objPlugin, null);


        }

        #endregion

        #region 其他

        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="ICommandName">ICommand的Name</param>
        /// <returns></returns>
        public UIDesignControl GetPlugin(string ICommandName)
        {
            if (string.IsNullOrEmpty(ICommandName)) return null;

            return m_MenuItems.FirstOrDefault(t => (t.Plugin as ICommand).Name == ICommandName);
        }

        /// <summary>
        /// MapDocument文档发生改变时的处理事件
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">事件参数</param>
        //private void MenuService_MapDocumentChanged(object sender, EventArgs e)
        //{
        //    IMapService pMapService = m_Framework.GetService(typeof(IMapService)) as IMapService;

        //    //对象转换
        //    DockDocument pDockContent = sender as DockDocument;
        //    if (pDockContent == null) return;

        //    //激活当前文档对象
        //    //pDockContent.Activate();
        //    pDockContent.Width -= 1;

        //    if (pDockContent is DocumentView.IMapDocumentView)
        //    {
        //        DocumentView.IMapDocumentView pMapDocument = pDockContent as DocumentView.IMapDocumentView;
        //        //设置地图服务对象
        //        pMapService.Hook = pMapDocument.Hook;
        //        //启动时间间隔事件
        //        m_Timer.Enabled = true;
        //    }
        //    else if (pDockContent is DocumentView.IPageLayOutDocumentView)
        //    {
        //        DocumentView.IPageLayOutDocumentView pPageLayoutDoc = pDockContent as DocumentView.IPageLayOutDocumentView;
        //        //设置地图服务对象
        //        pMapService.Hook = pPageLayoutDoc.Hook;
        //        //启动时间间隔事件
        //        m_Timer.Enabled = true;
        //    }
        //    else
        //    {
        //        //激活窗体不是MapDocument的情况下,停止处理时间间隔事件
        //        m_Timer.Enabled = false;
        //    }
        //}

        #endregion
    }
}
