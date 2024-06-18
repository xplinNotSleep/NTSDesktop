using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AG.COM.SDM.Framework.DocumentView;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.SystemUI.Utility;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 工具栏服务对象
    /// </summary>
    public class ToolBarService : IToolBarService
    {
        #region 变量

        /// <summary>
        /// 定时刷新Enabled和Check的Timer
        /// </summary>
        private Timer m_Timer;                                          //计时触发器
        private IFramework m_Framework;                                 //当前应用集成框架对象
        private IPluginsService m_PluginsService;                       //插件服务对象 
        /// <summary>
        /// 带有结构的所有工具项集合，key为Toolbar，value为key下的工具项
        /// </summary>
        private Dictionary<UIDesignControl, List<UIDesignControl>> m_RootTools = null;
        /// <summary>
        /// 所有工具项集合
        /// </summary>
        private List<UIDesignControl> m_ToolItems = null;

        public string HelperFile;

        #endregion

        #region 初始化

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="mFramework">IFramework对象</param>
        public ToolBarService(IFramework mFramework)
        {
            this.m_Framework = mFramework;
            this.m_PluginsService = PluginsService.GetInstance(mFramework);
            this.m_RootTools = new Dictionary<UIDesignControl, List<UIDesignControl>>();
            this.m_ToolItems = new List<UIDesignControl>();
            this.m_Timer = new Timer();
            this.m_Timer.Interval = 1000;

            //注册文档窗体激活事件
            //(this.m_Framework as IFrameworkEvents).MapDocumentChanged += new MapDocumentChangedEventHandler(ToolBarService_MapDocumentChanged);
            this.m_Timer.Tick += new EventHandler(OnTimedEvent);
        }

        /// <summary>
        /// 初始化工具服务，绑定功能到控件
        /// </summary>
        /// <param name="tRootContainer"></param>
        public void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer)
        {
            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in tRootContainer)
            {
                if (tRootControl.Key.Control is RibbonQuickAccessToolbar)
                {
                    m_RootTools.Add(tRootControl.Key, tRootControl.Value);

                    foreach (UIDesignControl tUIDesignToolItem in tRootControl.Value)
                    {
                        BindMenuItem(tUIDesignToolItem);
                    }
                }
            }
        }

        /// <summary>
        /// 绑定MenuItem
        /// </summary>
        /// <param name="tUIDesignToolItem"></param>
        private void BindMenuItem(UIDesignControl tUIDesignToolItem)
        {

            if (tUIDesignToolItem.Control is BarItem)
            {
                BarItem tQToolItem = tUIDesignToolItem.Control as BarItem;

                //实例化插件
                PlugInfoConfig plugInfo = PlugInProvider.GetPlugInfoConfig(tUIDesignToolItem.BindFun);
                object pObjInstance = PlugInProvider.GetInstance(plugInfo);

                if (pObjInstance is ICommand)
                {
                    //ICommand的Name不能为空
                    string commandName = (pObjInstance as ICommand).Name;
                    if (string.IsNullOrEmpty(commandName))
                    {
                        throw new Exception("插件（" + pObjInstance.GetType().Name + "）的Name为空，不能加载");
                    }

                    ICommand tCommand = pObjInstance as ICommand;

                    if (tQToolItem is BarButtonItem)
                    {
                        BarButtonItem barButtonItem = tQToolItem as BarButtonItem;

                        //加载图片
                        Image image = BindFunHelper.GetRibbonItemIcon(pObjInstance, 16);
                        if (image != null)
                        {
                            //为了提高系统启动速度，在绑定菜单时把图标先放到内存中，在需要时在设置到控件
                            barButtonItem.Glyph = image;
                        }

                        //if (pObjInstance is ITool)
                        //{
                        //    barButtonItem.ButtonStyle = BarButtonStyle.Check;
                        //}

                        //菜单条单击事件                            
                        barButtonItem.ItemClick += new ItemClickEventHandler(barButtonItem_ItemClick);
                    }

                    if (pObjInstance is AG.COM.SDM.SystemUI.IComboBox)
                    {
                        BindComboboxToToolItem(pObjInstance, tQToolItem as BarEditItem);
                    }

                    tUIDesignToolItem.Plugin = pObjInstance;
                    this.m_PluginsService.AddPlugin(commandName, pObjInstance);
                    this.m_ToolItems.Add(tUIDesignToolItem);
                }
            }
        }

        /// <summary>
        /// 绑定ToolItem的Combobox
        /// </summary>
        /// <param name="command"></param>
        /// <param name="tQToolItem"></param>
        private void BindComboboxToToolItem(object command, BarEditItem barEditItem)
        {
            BaseComboBox tBaseComboBox = command as BaseComboBox;
            if (tBaseComboBox != null && barEditItem != null)
            {
                //实现BaseComboBox的LabelText
                if (tBaseComboBox.LabelText.Length > 0)
                {
                    barEditItem.Caption = tBaseComboBox.LabelText;
                }
                else
                {
                    barEditItem.Caption = "";
                }

                barEditItem.Width = tBaseComboBox.Width;

                RepositoryItemComboBox combobox = barEditItem.Edit as RepositoryItemComboBox;

                //Dev只能接受数组，因此要把object转为数组
                object[] objArr = (tBaseComboBox.DataSource as System.Collections.IList).OfType<object>().ToArray();
                combobox.Items.AddRange(objArr);

                //ComboBox事件设置
                combobox.KeyDown += new KeyEventHandler(tBaseComboBox.OnKeyDown);
                combobox.KeyUp += new KeyEventHandler(tBaseComboBox.OnKeyUp);
                combobox.KeyPress += new KeyPressEventHandler(tBaseComboBox.OnKeyPress);
                combobox.SelectedIndexChanged += new EventHandler(tBaseComboBox.OnSelectedIndexChanged);
            }
        }

        #endregion

        #region ToolItem刷新状态

        /// <summary>
        /// 刷新所有ToolItem状态
        /// </summary>
        private void RefreshToolItems()
        {
            if (m_Framework.EnabledMenuRefresh == false) return;

            foreach (UIDesignControl tUIDesignControl in m_ToolItems)
            {
                BarItem barItem = tUIDesignControl.Control as BarItem;
                BarButtonItem barButtonItem = tUIDesignControl.Control as BarButtonItem;
                ICommand pCommand = tUIDesignControl.Plugin as ICommand;
                if (pCommand != null)
                {
                    pCommand.OnCreate(m_Framework);

                    if (barItem != null)
                    {
                        barItem.Enabled = GetPluginEnabled(pCommand);

                        //if (barButtonItem != null)
                        //{
                        //    //由于设置Check较耗资源，因此有此判断
                        //    bool newCheck = (tool == pCommand);
                        //    if (barButtonItem.Down != newCheck)
                        //    {
                        //        barButtonItem.Down = newCheck;
                        //    }
                        //}
                    }
                }
            }
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

        /// <summary>
        /// 定时刷新ToolItem事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void OnTimedEvent(object sender, EventArgs e)
        {
            //实现功能点与界面控件进行通信
            //RefreshToolItems();
        }

        #endregion

        #region 工具项单击事件

        private void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sender == null) return;

            BarButtonItem tQRibbonItem = e.Item as BarButtonItem;

            object objPlugin = null;

            //找到工具项对应的插件
            foreach (UIDesignControl tUIDesignMenuItem in m_ToolItems)
            {
                if (tUIDesignMenuItem.Control == tQRibbonItem)
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
                string strHelpFile = HelperFile;
                //string strHelpFile = string.Format("{0}\\{1}.mht", CommonConstString.STR_HelpPath, objPlugin.GetType().FullName);
                tHelpDocument.DisplayHelpFile(strHelpFile);
            }

            //激发插件对象单击事件
            this.m_Framework.OnPlugCommandClicked(objPlugin, null);

            if (objPlugin is ICommand)
            {
                ICommand pCommand = objPlugin as ICommand;
                if (pCommand != null) pCommand.OnClick();
            }

            RefreshToolItems();
        }

        #endregion

        #region 其他

        /// <summary>
        /// 根据ICommand的Name获取插件
        /// </summary>
        /// <param name="ICommandName"></param>
        /// <returns></returns>
        public UIDesignControl GetPlugin(string ICommandName)
        {
            if (string.IsNullOrEmpty(ICommandName)) return null;

            return m_ToolItems.FirstOrDefault(t => (t.Plugin as ICommand).Name == ICommandName);
        }

        /// <summary>
        /// MapDocument文档发生改变时的处理事件
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">事件参数</param>
        //private void ToolBarService_MapDocumentChanged(object sender, EventArgs e)
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


        /// <summary>
        /// 保存工具条布局
        /// </summary>
        /// <param name="tFilePath"></param>
        public void SaveLayout(string tFilePath)
        {
            //if (m_RootTools == null || m_RootTools.Count <= 0) return;

            //using (StreamWriter writer = new StreamWriter(tFilePath, false, Encoding.Default))
            //{
            //    foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> kvp in m_RootTools)
            //    {
            //        QToolBar tToolBar = kvp.Key.Control as QToolBar;
            //        if (tToolBar != null)
            //        {
            //            string str = string.Format("{0},{1},{2}", tToolBar.Name, tToolBar.RowIndex, tToolBar.ToolBarIndex);
            //            writer.WriteLine(str);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 恢复工具条布局
        /// </summary>
        /// <param name="tFilePath"></param>
        public void RecoverLayout(string tFilePath)
        {
            //if (m_RootTools == null || m_RootTools.Count <= 0) return;
            //if (File.Exists(tFilePath) == false) return;

            //using (StreamReader reader = new StreamReader(tFilePath, Encoding.Default))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        string str = reader.ReadLine();
            //        string[] values = str.Split(',');

            //        foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> kvp in m_RootTools)
            //        {
            //            QToolBar tToolBar = kvp.Key.Control as QToolBar;
            //            if (tToolBar != null && tToolBar.Name == values[0])
            //            {
            //                QToolBarHost tToolBarHost = tToolBar.Parent as QToolBarHost;
            //                if (tToolBarHost != null)
            //                {
            //                    int rowIndex = int.Parse(values[1]);
            //                    int toolBarIndex = int.Parse(values[2]);

            //                    tToolBar.DockToolBar(tToolBarHost, rowIndex, toolBarIndex);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        #endregion
    }
}
