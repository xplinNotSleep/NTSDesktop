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
    /// �˵�������
    /// </summary>
    public class MenuService : IMenuService
    {
        #region ����

        private IFramework m_Framework = null;
        /// <summary>
        /// ���нṹ�����в˵���ϣ�keyΪRibbonPage��Mainmenu��valueΪkey�µĲ˵���
        /// </summary>
        private Dictionary<UIDesignControl, List<UIDesignControl>> m_RootMenus = null;
        /// <summary>
        /// ���в˵����
        /// </summary>
        private List<UIDesignControl> m_MenuItems = null;
        private IPluginsService m_PluginsService = null;
        /// <summary>
        /// ��ʱˢ��Enabled��Check��Timer
        /// </summary>
        private Timer m_Timer;

        public string HelpFile = "";

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="mFramework">IFramework����</param>
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
        /// ��ʼ���˵����񣬰󶨹��ܵ��ؼ�
        /// </summary>
        /// <param name="tRootContainer"></param>
        public void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer)
        {
            //�Ƿ�󶨵�һҳ
            bool hasBindFirstPage = false;

            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in tRootContainer)
            {
                if (tRootControl.Key.Control is RibbonPage)
                {
                    m_RootMenus.Add(tRootControl.Key, tRootControl.Value);

                    //֧��һ�ֲ˵���RibbonPage
                    if (tRootControl.Key.Control is RibbonPage)
                    {
                        #region ��ӵ�ǰ�˵�ҳ����ʱ�ļ����¼�
                        if (hasBindFirstPage == false)
                        {
                            //ֻ��Ҫ��һ��SelectedPageChanged�¼�
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
        /// ��RibbonItem
        /// </summary>
        /// <param name="tUIDesignMenuItem"></param>
        private void BindRibbonItem(UIDesignControl tUIDesignMenuItem)
        {

            if (tUIDesignMenuItem.Control is BarItem)
            {
                BarItem barItem = tUIDesignMenuItem.Control as BarItem;

                //ʵ�������
                PlugInfoConfig plugInfo = PlugInProvider.GetPlugInfoConfig(tUIDesignMenuItem.BindFun);
                object pObjInstance = PlugInProvider.GetInstance(plugInfo);

                if (pObjInstance is ICommand)
                {
                    //ICommand��Name����Ϊ��
                    string commandName = (pObjInstance as ICommand).Name;
                    if (string.IsNullOrEmpty(commandName))
                    {
                        throw new Exception("�����" + pObjInstance.GetType().Name + "����NameΪ�գ����ܼ���");
                    }

                    if (barItem is BarButtonItem)
                    {
                        BarButtonItem barButtonItem = barItem as BarButtonItem;

                        //����ͼƬ
                        Image image = BindFunHelper.GetRibbonItemIcon(pObjInstance, barItem.RibbonStyle == RibbonItemStyles.Large ? 32 : 16);
                        if (image != null)
                        {
                            //Ϊ�����ϵͳ�����ٶȣ��ڰ󶨲˵�ʱ��ͼ���ȷŵ��ڴ��У�����Ҫʱ�����õ��ؼ�
                            tUIDesignMenuItem.Image = image;

                            barItem.Glyph = image;

                            tUIDesignMenuItem.HasInit = true;
                        }

                        //Ϊ��ʵ��checked��ѡ�У�Ч����Ҫ��ButtonStyle��ΪBarButtonStyle.Check
                        barButtonItem.ButtonStyle = BarButtonStyle.Check;

                        //�˵��������¼�                      
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
        /// ��RibbonItem��ComboBoxTreeView
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

                //HeightҪд���������д��barEditItem
                popupControl.Height = comboBoxTreeView.Height;

                popupControl.Controls.Add(comboBoxTreeView.TreeView);

                popupContainer.PopupControl = popupControl;

                popupContainer.Popup += new EventHandler(comboBoxTreeView.OnDropDown);
            }
        }

        /// <summary>
        /// ��RibbonItem��Combobox
        /// </summary>
        /// <param name="command"></param>
        /// <param name="barEditItem"></param>
        private void BindComboboxToRibbonItem(object command, BarEditItem barEditItem)
        {
            BaseComboBox baseComboBox = command as BaseComboBox;
            if (baseComboBox != null && barEditItem != null)
            {
                //ʵ��BaseComboBox��LabelText
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

                //Devֻ�ܽ������飬���Ҫ��objectתΪ����
                object[] objArr = (baseComboBox.DataSource as System.Collections.IList).OfType<object>().ToArray();

                combobox.Items.AddRange(objArr);

                //ComboBox�¼�����
                combobox.KeyDown += new KeyEventHandler(baseComboBox.OnKeyDown);
                combobox.KeyUp += new KeyEventHandler(baseComboBox.OnKeyUp);
                combobox.KeyPress += new KeyPressEventHandler(baseComboBox.OnKeyPress);
                combobox.SelectedIndexChanged += new EventHandler(baseComboBox.OnSelectedIndexChanged);
            }
        }

        #endregion

        #region RibbonItemˢ��״̬

        /// <summary>
        /// ��ʱˢ��Ribbon�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void OnTimedEvent(object sender, EventArgs e)
        {

            //RefreshActiveRibbonPage();

        }

        /// <summary>
        /// ˢ�µ�ǰ��ʾ��RibbonItem״̬
        /// </summary>
        private void RefreshActiveRibbonPage()
        {
            if (m_Framework.EnabledMenuRefresh == false) return;

            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in m_RootMenus)
            {
                RibbonPage tQRibbonPage = tRootControl.Key.Control as RibbonPage;
                if (tQRibbonPage != null)
                {
                    //ֻˢ�µ�ǰѡ���Page
                    if (tQRibbonPage.Ribbon.SelectedPage == tQRibbonPage)
                    {
                        RefreshChildRibbonItem(tQRibbonPage);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// ˢ��ĳ�ؼ�����RibbonItem״̬
        /// </summary>
        /// <param name="tParentItem">���ؼ�</param>
        private void RefreshChildRibbonItem(object tParentItem)
        {
            foreach (KeyValuePair<UIDesignControl, List<UIDesignControl>> tRootControl in m_RootMenus)
            {
                //�����ҵ�Ҫˢ�µĸ��ؼ�
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
                                //����ͼ��
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
                                    //��������Check�Ϻ���Դ������д��ж�
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
        /// RibbonPage��ʾʱˢ��RibbonItem״̬(�л��˵���ʱ)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QRibbonPage_Activated(object sender, EventArgs e)
        {
            RefreshActiveRibbonPage();

        }

        /// <summary>
        /// ��ȡ�˵����Checked
        /// </summary>
        /// <param name="tCommand"></param>
        /// <param name="tCurrentTool"></param>
        /// <returns></returns>
        private bool GetMenuItemChecked(ICommand tCommand)
        {
            return tCommand.Checked;

        }

        /// <summary>
        /// ��ȡ�����Enabled
        /// </summary>
        /// <param name="tCommand"></param>
        /// <returns></returns>
        private bool GetPluginEnabled(ICommand tCommand)
        {
            //���������������ɣ���Ҫ��֤�ò���Ƿ������Ȩ��
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

        #region �˵�����¼�

        private void tQRibbonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem tQRibbonItem = e.Item as BarButtonItem;

            //RefreshActiveRibbonPage();//��ʵ�ֵ���¼�ǰ�Ƚ���ͨ�ż���

            MenuItemOnClick(e.Item, tQRibbonItem.Caption);

        }

        /// <summary>
        /// �˵����
        /// </summary>
        /// <param name="objMenuItem"></param>
        /// <param name="MenuItemText"></param>
        private void MenuItemOnClick(object objMenuItem, string MenuItemText)
        {

            if (objMenuItem == null) return;

            object objPlugin = null;

            //�ҵ��˵����Ӧ�Ĳ��
            foreach (UIDesignControl tUIDesignMenuItem in m_MenuItems)
            {
                if (tUIDesignMenuItem.Control == objMenuItem)
                {
                    objPlugin = tUIDesignMenuItem.Plugin;
                    break;
                }
            }

            //��ȡ�ĵ��������ӿ�
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

            //����������󵥻��¼�
            this.m_Framework.OnPlugCommandClicked(objPlugin, null);


        }

        #endregion

        #region ����

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="ICommandName">ICommand��Name</param>
        /// <returns></returns>
        public UIDesignControl GetPlugin(string ICommandName)
        {
            if (string.IsNullOrEmpty(ICommandName)) return null;

            return m_MenuItems.FirstOrDefault(t => (t.Plugin as ICommand).Name == ICommandName);
        }

        /// <summary>
        /// MapDocument�ĵ������ı�ʱ�Ĵ����¼�
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">�¼�����</param>
        //private void MenuService_MapDocumentChanged(object sender, EventArgs e)
        //{
        //    IMapService pMapService = m_Framework.GetService(typeof(IMapService)) as IMapService;

        //    //����ת��
        //    DockDocument pDockContent = sender as DockDocument;
        //    if (pDockContent == null) return;

        //    //���ǰ�ĵ�����
        //    //pDockContent.Activate();
        //    pDockContent.Width -= 1;

        //    if (pDockContent is DocumentView.IMapDocumentView)
        //    {
        //        DocumentView.IMapDocumentView pMapDocument = pDockContent as DocumentView.IMapDocumentView;
        //        //���õ�ͼ�������
        //        pMapService.Hook = pMapDocument.Hook;
        //        //����ʱ�����¼�
        //        m_Timer.Enabled = true;
        //    }
        //    else if (pDockContent is DocumentView.IPageLayOutDocumentView)
        //    {
        //        DocumentView.IPageLayOutDocumentView pPageLayoutDoc = pDockContent as DocumentView.IPageLayOutDocumentView;
        //        //���õ�ͼ�������
        //        pMapService.Hook = pPageLayoutDoc.Hook;
        //        //����ʱ�����¼�
        //        m_Timer.Enabled = true;
        //    }
        //    else
        //    {
        //        //����岻��MapDocument�������,ֹͣ����ʱ�����¼�
        //        m_Timer.Enabled = false;
        //    }
        //}

        #endregion
    }
}
