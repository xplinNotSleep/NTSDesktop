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
    /// �������������
    /// </summary>
    public class ToolBarService : IToolBarService
    {
        #region ����

        /// <summary>
        /// ��ʱˢ��Enabled��Check��Timer
        /// </summary>
        private Timer m_Timer;                                          //��ʱ������
        private IFramework m_Framework;                                 //��ǰӦ�ü��ɿ�ܶ���
        private IPluginsService m_PluginsService;                       //���������� 
        /// <summary>
        /// ���нṹ�����й�����ϣ�keyΪToolbar��valueΪkey�µĹ�����
        /// </summary>
        private Dictionary<UIDesignControl, List<UIDesignControl>> m_RootTools = null;
        /// <summary>
        /// ���й������
        /// </summary>
        private List<UIDesignControl> m_ToolItems = null;

        public string HelperFile;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="mFramework">IFramework����</param>
        public ToolBarService(IFramework mFramework)
        {
            this.m_Framework = mFramework;
            this.m_PluginsService = PluginsService.GetInstance(mFramework);
            this.m_RootTools = new Dictionary<UIDesignControl, List<UIDesignControl>>();
            this.m_ToolItems = new List<UIDesignControl>();
            this.m_Timer = new Timer();
            this.m_Timer.Interval = 1000;

            //ע���ĵ����弤���¼�
            //(this.m_Framework as IFrameworkEvents).MapDocumentChanged += new MapDocumentChangedEventHandler(ToolBarService_MapDocumentChanged);
            this.m_Timer.Tick += new EventHandler(OnTimedEvent);
        }

        /// <summary>
        /// ��ʼ�����߷��񣬰󶨹��ܵ��ؼ�
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
        /// ��MenuItem
        /// </summary>
        /// <param name="tUIDesignToolItem"></param>
        private void BindMenuItem(UIDesignControl tUIDesignToolItem)
        {

            if (tUIDesignToolItem.Control is BarItem)
            {
                BarItem tQToolItem = tUIDesignToolItem.Control as BarItem;

                //ʵ�������
                PlugInfoConfig plugInfo = PlugInProvider.GetPlugInfoConfig(tUIDesignToolItem.BindFun);
                object pObjInstance = PlugInProvider.GetInstance(plugInfo);

                if (pObjInstance is ICommand)
                {
                    //ICommand��Name����Ϊ��
                    string commandName = (pObjInstance as ICommand).Name;
                    if (string.IsNullOrEmpty(commandName))
                    {
                        throw new Exception("�����" + pObjInstance.GetType().Name + "����NameΪ�գ����ܼ���");
                    }

                    ICommand tCommand = pObjInstance as ICommand;

                    if (tQToolItem is BarButtonItem)
                    {
                        BarButtonItem barButtonItem = tQToolItem as BarButtonItem;

                        //����ͼƬ
                        Image image = BindFunHelper.GetRibbonItemIcon(pObjInstance, 16);
                        if (image != null)
                        {
                            //Ϊ�����ϵͳ�����ٶȣ��ڰ󶨲˵�ʱ��ͼ���ȷŵ��ڴ��У�����Ҫʱ�����õ��ؼ�
                            barButtonItem.Glyph = image;
                        }

                        //if (pObjInstance is ITool)
                        //{
                        //    barButtonItem.ButtonStyle = BarButtonStyle.Check;
                        //}

                        //�˵��������¼�                            
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
        /// ��ToolItem��Combobox
        /// </summary>
        /// <param name="command"></param>
        /// <param name="tQToolItem"></param>
        private void BindComboboxToToolItem(object command, BarEditItem barEditItem)
        {
            BaseComboBox tBaseComboBox = command as BaseComboBox;
            if (tBaseComboBox != null && barEditItem != null)
            {
                //ʵ��BaseComboBox��LabelText
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

                //Devֻ�ܽ������飬���Ҫ��objectתΪ����
                object[] objArr = (tBaseComboBox.DataSource as System.Collections.IList).OfType<object>().ToArray();
                combobox.Items.AddRange(objArr);

                //ComboBox�¼�����
                combobox.KeyDown += new KeyEventHandler(tBaseComboBox.OnKeyDown);
                combobox.KeyUp += new KeyEventHandler(tBaseComboBox.OnKeyUp);
                combobox.KeyPress += new KeyPressEventHandler(tBaseComboBox.OnKeyPress);
                combobox.SelectedIndexChanged += new EventHandler(tBaseComboBox.OnSelectedIndexChanged);
            }
        }

        #endregion

        #region ToolItemˢ��״̬

        /// <summary>
        /// ˢ������ToolItem״̬
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
                        //    //��������Check�Ϻ���Դ������д��ж�
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

        /// <summary>
        /// ��ʱˢ��ToolItem�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void OnTimedEvent(object sender, EventArgs e)
        {
            //ʵ�ֹ��ܵ������ؼ�����ͨ��
            //RefreshToolItems();
        }

        #endregion

        #region ��������¼�

        private void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sender == null) return;

            BarButtonItem tQRibbonItem = e.Item as BarButtonItem;

            object objPlugin = null;

            //�ҵ��������Ӧ�Ĳ��
            foreach (UIDesignControl tUIDesignMenuItem in m_ToolItems)
            {
                if (tUIDesignMenuItem.Control == tQRibbonItem)
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
                string strHelpFile = HelperFile;
                //string strHelpFile = string.Format("{0}\\{1}.mht", CommonConstString.STR_HelpPath, objPlugin.GetType().FullName);
                tHelpDocument.DisplayHelpFile(strHelpFile);
            }

            //����������󵥻��¼�
            this.m_Framework.OnPlugCommandClicked(objPlugin, null);

            if (objPlugin is ICommand)
            {
                ICommand pCommand = objPlugin as ICommand;
                if (pCommand != null) pCommand.OnClick();
            }

            RefreshToolItems();
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ICommand��Name��ȡ���
        /// </summary>
        /// <param name="ICommandName"></param>
        /// <returns></returns>
        public UIDesignControl GetPlugin(string ICommandName)
        {
            if (string.IsNullOrEmpty(ICommandName)) return null;

            return m_ToolItems.FirstOrDefault(t => (t.Plugin as ICommand).Name == ICommandName);
        }

        /// <summary>
        /// MapDocument�ĵ������ı�ʱ�Ĵ����¼�
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">�¼�����</param>
        //private void ToolBarService_MapDocumentChanged(object sender, EventArgs e)
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


        /// <summary>
        /// ���湤��������
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
        /// �ָ�����������
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
