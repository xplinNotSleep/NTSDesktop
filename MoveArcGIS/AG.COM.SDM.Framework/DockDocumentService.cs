using AG.COM.SDM.SystemUI;
using AG.COM.SDM.SystemUI.Utility;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// �ĵ����������
    /// </summary>
    public class DockDocumentService:IDockDocumentService
    {
        /// <summary>
        /// ����ģʽ��ʵ��
        /// </summary>
        private static DockDocumentService m_Instance = null;

        //��ǰӦ�ü��ɿ�ܶ���
        private IFramework  m_Framework = null;

        /// <summary>
        /// �ĵ������ֵ���
        /// </summary>
        private Dictionary<string, DockDocument> m_DockDocuments = new Dictionary<string, DockDocument>();
        /// <summary>
        /// ����DockDocments����
        /// </summary>
        public Dictionary<string, DockDocument> DockDocuments
        {
            get { return m_DockDocuments; }
            set { m_DockDocuments = value; }
        }

        /// <summary>
        /// ��ȡΨһʵ��
        /// </summary>
        /// <returns></returns>
        public static DockDocumentService GetInstance()
        {
            //���÷�������ȫ�ĵ���ģʽ��ֻ��Ϊ�˷�����ûHookhelper������¿���ʹ��DockDocumentService���Ӷ�������ͣ������

            if (m_Instance == null)
                throw new Exception("δʵ����DockDocumentService");

            return m_Instance;
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="mFramework">IFramework����</param>
        public DockDocumentService(IFramework mFramework)
        {
            m_Instance = this;
            this.m_Framework = mFramework;
            //(this.m_Framework as IFrameworkEvents).MapDocumentChanged += new MapDocumentChangedEventHandler(DockDocumentService_MapDocumentChanged);
            //TabbedView tabbedView1= m_Framework.DocumentManager.View as TabbedView;
        }

        #region IDockDocumentService ��Ա
        /// <summary>
        /// ��ȡ��ǰ�������Ƿ�����˴���
        /// </summary>
        /// <param name="dockDocumentName">��������</param>
        /// <returns>��������򷵻� true,���򷵻� </returns>
        public bool ContainsDocument(string dockDocumentName)
        {
            return DockDocuments.ContainsKey(dockDocumentName);
        }

        /// <summary>
        /// �Ƴ�������
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException("�˷���δʵ��");

            //this.m_DockDocuments.Clear();

            //IEnumerable<IDockContent> tEnumDockContent = this.m_Framework.DockPanel.Documents;
            //foreach (IDockContent tDockContent in tEnumDockContent)
            //{
            //    (tDockContent as Form).Close();
            //}             
        }

        /// <summary>
        /// ����ĵ�����,Ĭ��ͣ��״̬Ϊ����ͣ��
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        /// <param name="dockDocument">DockDocument����</param>
        public void AddDockDocument(string dockDocumentName, DockDocument dockDocument)
        {
            AddDockDocument(dockDocumentName, dockDocument, DockState.Left);
        }

        /// <summary>
        /// ����ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        /// <param name="dockDocument">DockDocument����</param>
        /// <param name="dockState">ͣ��״̬</param>
        public void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockState dockState)
        {
            AddDockDocument(dockDocumentName, dockDocument, null, dockState);
        }

        /// <summary>
        /// ����ĵ�����Document��ʽ��ӣ����뵱ǰDocument���ҷ�����
        /// </summary>
        /// <param name="dockDocumentName"></param>
        /// <param name="dockDocument"></param>
        public void AddDockDocumentSplit(string dockDocumentName, DockDocument dockDocument)
        {
            if (DockDocuments.ContainsKey(dockDocumentName) == false)
            {
                //dockDocumentName��ΪDockDocument��Ψһ��ʶ��ҲҪ�Ѵ˱�ʶ���浽DockDocument��
                dockDocument.Name = dockDocumentName;

                DockDocuments.Add(dockDocumentName, dockDocument);

                DockPanel dockPanel = null;

                dockPanel = m_Framework.DockManager.AddPanel(DockingStyle.Float);

                TabbedView tabbedView = m_Framework.DocumentManager.View as TabbedView;
                ITabbedViewController iTabbedViewController = tabbedView.Controller;

                BaseDocument document = tabbedView.AddDocument(dockPanel);

                iTabbedViewController.CreateNewDocumentGroup(document as Document);

                dockDocument.Dock = DockStyle.Fill;
                dockDocument.DockPanel = dockPanel;

                dockPanel.Text = dockDocument.TabText;
                //���Զ���ؼ���DockDocument���ӵ�DockPanel
                dockPanel.ControlContainer.Controls.Add(dockDocument);

                //DockPanel��Closed�¼�
                dockPanel.ClosedPanel += (object sender, DockPanelEventArgs e) =>
                {
                    //����DockDocument��FormClosed�¼�
                    dockDocument.OnFormClosed(dockDocument, e);
                    if (e.Panel.ControlContainer != null)
                    {
                        Control control = e.Panel.ControlContainer.Controls[0];

                        //�Ƴ�ָ�����Ƶ��ĵ�����
                        this.RemoveKey(control.Name);

                    }
                };

                dockPanel.ClosingPanel += (object sender, DockPanelCancelEventArgs e) =>
                {
                    //����DockDocument��FormClosing�¼�
                    dockDocument.OnFormClosing(dockDocument, e);
                };
            }
            else
            {
                //�����Ѵ��ڵ��ĵ�
                DockDocuments[dockDocumentName].DockPanel.Show();
            }
        }

        /// <summary>
        /// ����ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ�����</param>
        /// <param name="dockDocument">DockDocument����</param>
        /// <param name="parentDocument">ͣ���ڵ�DockDocument����</param>
        /// <param name="dockState">ͣ��״̬</param>
        public void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockDocument parentDocument, DockState dockState)
        {
            if (DockDocuments.ContainsKey(dockDocumentName) == false)
            {
                //dockDocumentName��ΪDockDocument��Ψһ��ʶ��ҲҪ�Ѵ˱�ʶ���浽DockDocument��
                dockDocument.Name = dockDocumentName;

                DockDocuments.Add(dockDocumentName, dockDocument);

                DockPanel dockPanel = null;
                #region ��ע��
                //dock��ĳ��DockPanel�Ƚϸ��ӣ�˵����
                //DockPanel���и����ض���PanelContainer��ʵ����DockPanel������PanelContainer�¶�������DockPanel��
                //��Ҳ�����⣬���ǵ�DockPanel�ڴ��壨���㣩�£����Ҹ÷���ָtop��buttom��left��right����֮һ��ֻ��Ψһһ��DockPanel�����DockPanel��������From��������PanelContainer
                //DockPanel��ParentPanel���ԣ�����ŵ�ָ�ľ�������PanelContainer���������ΪFormֵ��Ϊnull
                //���AddPanel�Ļ������ParentPanel���Բ�Ϊnull�Ļ���Ӧ����ParentPanelȥAddPanel�����ParentPanel��null����Ӧ����DockPanel����
                //��˼�ǣ�����и����������ø�������ӣ���֮��DockPanel��ӣ����ʱ���Զ�����һ��������
                //�����������������Tab��ָtab���з�ʽ����֮��һ�������ţ�����Ҫ�л���Tab������������Tabbed����DockAsTab����
                #endregion
                if (parentDocument != null)
                {
                    //ͣ����ĳDockPanel
                    if (dockState == DockState.Document)
                    {
                        if (parentDocument.DockPanel.ParentPanel != null)
                        {
                            if (parentDocument.DockPanel.ParentPanel.Tabbed == true)
                            {
                                DockPanel dockTarget = parentDocument.DockPanel.ParentPanel;

                                dockPanel = dockTarget.AddPanel();
                                dockPanel.Show();
                            }
                            else
                            {
                                DockPanel dockTarget = parentDocument.DockPanel;

                                dockPanel = dockTarget.AddPanel();
                                dockPanel.DockAsTab(dockTarget);
                            }
                        }
                        else
                        {
                            DockPanel dockTarget = parentDocument.DockPanel;

                            dockPanel = dockTarget.AddPanel();
                            dockPanel.DockAsTab(dockTarget);
                        }

                        dockPanel.Show();
                    }
                    else
                    {
                        DockPanel dockTarget = parentDocument.DockPanel.ParentPanel != null ? parentDocument.DockPanel.ParentPanel : parentDocument.DockPanel;

                        dockPanel = dockTarget.AddPanel();

                        dockPanel.DockTo(dockTarget, DockStateHelper.DockStateToDockingStyle(dockState));
                    }                 
                }
                else
                {
                    if (dockState == DockState.Document)
                    {
                        //Devû��Document���ͣ���Top����
                        dockPanel = m_Framework.DockManager.AddPanel(DockingStyle.Top);
                    }
                    else
                    {
                        TabbedView tabbedView = m_Framework.DocumentManager.View as TabbedView;
                        ITabbedViewController iTabbedViewController = tabbedView.Controller;

                        //�ռ�����Document��ʽͣ����DockPanel
                        List<Control> documentDockPanels = new List<Control>();

                        foreach (BaseDocument baseDocument in tabbedView.Documents)
                        {
                            documentDockPanels.Add(baseDocument.Control);
                        }

                        //��ͨ��ͣ�������ͣ���ķ����Ѿ���DockPanel���Ǿ�Ҫ���µ�DockPanel��tab��ʽ�����еĺ���һ��
                        DockingStyle dockingStyle = DockStateHelper.DockStateToDockingStyle(dockState);
                        //ֻ����tab��rootpanel
                        foreach (DockPanel dockPanelRoot in m_Framework.DockManager.RootPanels)
                        {
                            //Document��ʽͣ����DockPanel�޷�ֻͨ��Dock�������֣����Ҫ���֮ǰ��ȡ������Document��ʽͣ����DockPanel��ɸѡ
                            if (dockPanelRoot.Dock == dockingStyle && documentDockPanels.Contains(dockPanelRoot) == false)
                            {
                                dockPanel = dockPanelRoot.AddPanel();
                                dockPanel.DockAsTab(dockPanelRoot);

                                break;
                            }
                        }

                        //���dockPanel==null��������û��dock��rootpanel��
                        if (dockPanel == null)
                        {
                            dockPanel = m_Framework.DockManager.AddPanel(dockingStyle);    
                        }

                        //����ͨ��ͣ��                                        
                        dockPanel.Show();
                    }
                }

                if (dockState == DockState.Float)
                {
                    //float�ģ�Ҫ���ô�С��λ��
                    //��ΪDockPanel��Size��������DockPanel��������DockPanel�ı������ȣ�����ʵ�ʴ�СҪ��Ƕ��Ŀؼ���Щ
                    dockPanel.FloatSize = new Size(dockDocument.Size.Width + 10, dockDocument.Size.Height + 20);
                    Point pt = new Point((int)(Screen.PrimaryScreen.WorkingArea.Width / 5), (int)(Screen.PrimaryScreen.WorkingArea.Height / 5));
                    dockPanel.MakeFloat(pt);
                }
                else
                {
                    //��float�ģ�Ҳ����ͣ���ģ�Ҫ���ÿ��
                    dockPanel.Width = dockDocument.Size.Width + 10;
                }

                dockDocument.Dock = DockStyle.Fill;
                dockDocument.DockPanel = dockPanel;
                dockPanel.Text = dockDocument.TabText;
                //���Զ���ؼ���DockDocument���ӵ�DockPanel
                dockPanel.ControlContainer.Controls.Add(dockDocument);

                if (parentDocument == null && dockState == DockState.Document)
                {
                    //ʵ��Document��ʽͣ��
                    dockPanel.DockedAsTabbedDocument = true;
                    dockPanel.Options.AllowDockTop = false;
                }

                if (parentDocument != null && dockState == DockState.Document)
                {
                    //ͣ����DockPanel����Tab���������ΪĬ����ʾ�ɵ�DockPanel������Ҫ�ֶ���ʾ
                    //dockPanel.Show();
                }
          
                //DockPanel��Closed�¼�
                dockPanel.ClosedPanel += (object sender, DockPanelEventArgs e) =>
                    {
                        if(e.Panel.ControlContainer!=null)
                        {
                            Control control = e.Panel.ControlContainer.Controls[0];

                            //�Ƴ�ָ�����Ƶ��ĵ�����
                            this.RemoveKey(control.Name);

                            //����DockDocument��FormClosed�¼�
                            dockDocument.OnFormClosed(dockDocument, e);
                        }
                        

                    };

                dockPanel.ClosingPanel += (object sender, DockPanelCancelEventArgs e) =>
                    {
                        //����DockDocument��FormClosing�¼�
                        dockDocument.OnFormClosing(dockDocument, e);
                        //DockDocument_FormClosing(dockDocument, e);
                    };
            }
            else
            {
                DockDocuments[dockDocumentName] = dockDocument;

                DockDocument ddNew = DockDocuments[dockDocumentName];
                DockPanel dockPanel = ddNew.DockPanel;
                dockPanel.SavedDock = DockingStyle.Top; 
                dockPanel.Show();

                //�����Ѵ��ڵ��ĵ�
                //DockDocuments[dockDocumentName].DockPanel.Show();
            }
        }

        private void DockDocument_FormClosing(object sender, EventArgs e)
        {

            RemoveKey((sender as DockDocument).Name);
        }

        /// <summary>
        /// ��Document��tab��ʽͣ������һ��Document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="documentTarget"></param>
        public void DockDocumentToAnother(DockDocument document, DockDocument documentTarget)
        {
            TabbedView tabbedView = m_Framework.DocumentManager.View as TabbedView;
            ITabbedViewController iTabbedViewController = tabbedView.Controller;

            BaseDocument documentDev = null;
            BaseDocument documentDevTarget = null;

            foreach (BaseDocument baseDocument in tabbedView.Documents)
            {
                //ͨ��DockPanel�ҵ���Ӧ��Document
                if (baseDocument.Control == document.DockPanel)
                {
                    documentDev = baseDocument;
                }

                if (baseDocument.Control == documentTarget.DockPanel)
                {
                    documentDevTarget = baseDocument;
                }
            }
            //ͣ��document
            iTabbedViewController.Dock(documentDev as Document, (documentDevTarget as Document).Parent);
            //����document��ʾ������Active��
            iTabbedViewController.Select(documentDevTarget as Document);
        }

        /// <summary>
        /// ��Document��Ϊ������ʾ
        /// </summary>
        /// <param name="document"></param>
        public void DockDocumentSplit(DockDocument document)
        {
            TabbedView tabbedView = m_Framework.DocumentManager.View as TabbedView;
            ITabbedViewController iTabbedViewController = tabbedView.Controller;

            BaseDocument baseDocument = tabbedView.AddDocument(document.DockPanel);
            //ʵ�ַ�����ʾ
            iTabbedViewController.CreateNewDocumentGroup(baseDocument as Document);
        }

        /// <summary>
        /// ��ȡָ�����Ƶ��ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        /// <returns>����ָ�����Ƶ��ĵ�����</returns>
        public DockDocument GetDockDocument(string dockDocumentName)
        {
            DockDocument dockDocument = null;
            if (DockDocuments.ContainsKey(dockDocumentName))
            {
                dockDocument = DockDocuments[dockDocumentName];
            }

            return dockDocument;
        }

        /// <summary>
        /// �Ƴ����ر�ָ�����Ƶ��ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">��������</param>
        public void RemoveDockDocument(string dockDocumentName)
        {
            if (DockDocuments.ContainsKey(dockDocumentName))
            {
                DockDocument dockDocument = DockDocuments[dockDocumentName];
                m_Framework.DockManager.RemovePanel(dockDocument.DockPanel);
               
                DockDocuments.Remove(dockDocumentName);
            }
            else
            {
                MessageBox.Show(string.Format("[{0}]�������󲻴���!",dockDocumentName), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// �Ƴ�ָ�����Ƶ��ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        public void RemoveKey(string dockDocumentName)
        {
            if (DockDocuments.ContainsKey(dockDocumentName))
            {
                DockDocument dockDocument = DockDocuments[dockDocumentName];
                m_Framework.DockManager.RemovePanel(dockDocument.DockPanel);
                DockDocuments.Remove(dockDocumentName);
            }
            else
            {
                MessageBox.Show(string.Format("[{0}]�ĵ����󲻴���!",dockDocumentName ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        /// <summary>
        /// m_Framework�����ô˷�����֪ͨDockDocumentService
        /// ������󼤻��ĵ������Ѿ������ı�
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">�¼�����</param>
        //private void DockDocumentService_MapDocumentChanged(object sender, EventArgs e)
        //{
        //    DocumentView.IMapDocumentView pMapDocument = sender as DocumentView.IMapDocumentView;
        //    if (pMapDocument != null)
        //    {
        //        foreach (DockDocument dock in DockDocuments.Values)
        //        {
        //            DocumentView.ITocDocumentView pTocDocView = dock as DocumentView.ITocDocumentView;
        //            if (pTocDocView != null)
        //            {
        //                pTocDocView.SetBuddyControl(pMapDocument.Hook);
        //                break;
        //            }
        //        }
        //    }
        //}
    
    
    }
}
