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
    /// 文档对象服务类
    /// </summary>
    public class DockDocumentService:IDockDocumentService
    {
        /// <summary>
        /// 单例模式的实例
        /// </summary>
        private static DockDocumentService m_Instance = null;

        //当前应用集成框架对象
        private IFramework  m_Framework = null;

        /// <summary>
        /// 文档对象字典项
        /// </summary>
        private Dictionary<string, DockDocument> m_DockDocuments = new Dictionary<string, DockDocument>();
        /// <summary>
        /// 所有DockDocments集合
        /// </summary>
        public Dictionary<string, DockDocument> DockDocuments
        {
            get { return m_DockDocuments; }
            set { m_DockDocuments = value; }
        }

        /// <summary>
        /// 获取唯一实例
        /// </summary>
        /// <returns></returns>
        public static DockDocumentService GetInstance()
        {
            //此用法不算完全的单例模式，只是为了方便在没Hookhelper的情况下可以使用DockDocumentService，从而操作可停靠窗体

            if (m_Instance == null)
                throw new Exception("未实例化DockDocumentService");

            return m_Instance;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="mFramework">IFramework对象</param>
        public DockDocumentService(IFramework mFramework)
        {
            m_Instance = this;
            this.m_Framework = mFramework;
            //(this.m_Framework as IFrameworkEvents).MapDocumentChanged += new MapDocumentChangedEventHandler(DockDocumentService_MapDocumentChanged);
            //TabbedView tabbedView1= m_Framework.DocumentManager.View as TabbedView;
        }

        #region IDockDocumentService 成员
        /// <summary>
        /// 获取当前服务中是否包含此窗体
        /// </summary>
        /// <param name="dockDocumentName">窗体名称</param>
        /// <returns>如果包含则返回 true,否则返回 </returns>
        public bool ContainsDocument(string dockDocumentName)
        {
            return DockDocuments.ContainsKey(dockDocumentName);
        }

        /// <summary>
        /// 移除所有项
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException("此方法未实现");

            //this.m_DockDocuments.Clear();

            //IEnumerable<IDockContent> tEnumDockContent = this.m_Framework.DockPanel.Documents;
            //foreach (IDockContent tDockContent in tEnumDockContent)
            //{
            //    (tDockContent as Form).Close();
            //}             
        }

        /// <summary>
        /// 添加文档对象,默认停靠状态为靠左停靠
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        /// <param name="dockDocument">DockDocument对象</param>
        public void AddDockDocument(string dockDocumentName, DockDocument dockDocument)
        {
            AddDockDocument(dockDocumentName, dockDocument, DockState.Left);
        }

        /// <summary>
        /// 添加文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        /// <param name="dockDocument">DockDocument对象</param>
        /// <param name="dockState">停靠状态</param>
        public void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockState dockState)
        {
            AddDockDocument(dockDocumentName, dockDocument, null, dockState);
        }

        /// <summary>
        /// 添加文档（以Document方式添加，并与当前Document左右分屏）
        /// </summary>
        /// <param name="dockDocumentName"></param>
        /// <param name="dockDocument"></param>
        public void AddDockDocumentSplit(string dockDocumentName, DockDocument dockDocument)
        {
            if (DockDocuments.ContainsKey(dockDocumentName) == false)
            {
                //dockDocumentName作为DockDocument的唯一标识，也要把此标识保存到DockDocument中
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
                //把自定义控件（DockDocument）加到DockPanel
                dockPanel.ControlContainer.Controls.Add(dockDocument);

                //DockPanel的Closed事件
                dockPanel.ClosedPanel += (object sender, DockPanelEventArgs e) =>
                {
                    //触发DockDocument的FormClosed事件
                    dockDocument.OnFormClosed(dockDocument, e);
                    if (e.Panel.ControlContainer != null)
                    {
                        Control control = e.Panel.ControlContainer.Controls[0];

                        //移除指定名称的文档对象
                        this.RemoveKey(control.Name);

                    }
                };

                dockPanel.ClosingPanel += (object sender, DockPanelCancelEventArgs e) =>
                {
                    //触发DockDocument的FormClosing事件
                    dockDocument.OnFormClosing(dockDocument, e);
                };
            }
            else
            {
                //激活已存在的文档
                DockDocuments[dockDocumentName].DockPanel.Show();
            }
        }

        /// <summary>
        /// 添加文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档名称</param>
        /// <param name="dockDocument">DockDocument对象</param>
        /// <param name="parentDocument">停靠在的DockDocument对象</param>
        /// <param name="dockState">停靠状态</param>
        public void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockDocument parentDocument, DockState dockState)
        {
            if (DockDocuments.ContainsKey(dockDocumentName) == false)
            {
                //dockDocumentName作为DockDocument的唯一标识，也要把此标识保存到DockDocument中
                dockDocument.Name = dockDocumentName;

                DockDocuments.Add(dockDocumentName, dockDocument);

                DockPanel dockPanel = null;
                #region 长注释
                //dock到某个DockPanel比较复杂，说明：
                //DockPanel下有个隐藏对象PanelContainer，实际上DockPanel都是在PanelContainer下而不是在DockPanel下
                //但也有例外，就是当DockPanel在窗体（顶层）下，而且该方向（指top，buttom，left，right其中之一）只有唯一一个DockPanel，则此DockPanel父级就是From，而不是PanelContainer
                //DockPanel的ParentPanel属性，里面放的指的就是上述PanelContainer，如果父级为Form值就为null
                //因此AddPanel的话，如果ParentPanel属性不为null的话，应该用ParentPanel去AddPanel，如果ParentPanel是null，就应该用DockPanel本身
                //意思是，如果有父容器，就用父容器添加，反之用DockPanel添加，添加时会自动创建一个父容器
                //另外如果父容器不是Tab（指tab排列方式，反之是一个个并排），还要切换成Tab，可以用属性Tabbed，或DockAsTab方法
                #endregion
                if (parentDocument != null)
                {
                    //停靠到某DockPanel
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
                        //Dev没有Document类型，用Top代替
                        dockPanel = m_Framework.DockManager.AddPanel(DockingStyle.Top);
                    }
                    else
                    {
                        TabbedView tabbedView = m_Framework.DocumentManager.View as TabbedView;
                        ITabbedViewController iTabbedViewController = tabbedView.Controller;

                        //收集所有Document方式停靠的DockPanel
                        List<Control> documentDockPanels = new List<Control>();

                        foreach (BaseDocument baseDocument in tabbedView.Documents)
                        {
                            documentDockPanels.Add(baseDocument.Control);
                        }

                        //普通的停靠，如果停靠的方向已经有DockPanel，那就要把新的DockPanel用tab方式跟已有的合在一起
                        DockingStyle dockingStyle = DockStateHelper.DockStateToDockingStyle(dockState);
                        //只考虑tab到rootpanel
                        foreach (DockPanel dockPanelRoot in m_Framework.DockManager.RootPanels)
                        {
                            //Document方式停靠的DockPanel无法只通过Dock属性区分，因此要结合之前获取的所有Document方式停靠的DockPanel来筛选
                            if (dockPanelRoot.Dock == dockingStyle && documentDockPanels.Contains(dockPanelRoot) == false)
                            {
                                dockPanel = dockPanelRoot.AddPanel();
                                dockPanel.DockAsTab(dockPanelRoot);

                                break;
                            }
                        }

                        //如果dockPanel==null，就是在没有dock到rootpanel的
                        if (dockPanel == null)
                        {
                            dockPanel = m_Framework.DockManager.AddPanel(dockingStyle);    
                        }

                        //最普通的停靠                                        
                        dockPanel.Show();
                    }
                }

                if (dockState == DockState.Float)
                {
                    //float的，要设置大小和位置
                    //因为DockPanel的Size代表整个DockPanel，即包括DockPanel的标题栏等，所以实际大小要比嵌入的控件大些
                    dockPanel.FloatSize = new Size(dockDocument.Size.Width + 10, dockDocument.Size.Height + 20);
                    Point pt = new Point((int)(Screen.PrimaryScreen.WorkingArea.Width / 5), (int)(Screen.PrimaryScreen.WorkingArea.Height / 5));
                    dockPanel.MakeFloat(pt);
                }
                else
                {
                    //非float的，也就是停靠的，要设置宽度
                    dockPanel.Width = dockDocument.Size.Width + 10;
                }

                dockDocument.Dock = DockStyle.Fill;
                dockDocument.DockPanel = dockPanel;
                dockPanel.Text = dockDocument.TabText;
                //把自定义控件（DockDocument）加到DockPanel
                dockPanel.ControlContainer.Controls.Add(dockDocument);

                if (parentDocument == null && dockState == DockState.Document)
                {
                    //实现Document方式停靠
                    dockPanel.DockedAsTabbedDocument = true;
                    dockPanel.Options.AllowDockTop = false;
                }

                if (parentDocument != null && dockState == DockState.Document)
                {
                    //停靠到DockPanel并是Tab的情况，因为默认显示旧的DockPanel，所以要手动显示
                    //dockPanel.Show();
                }
          
                //DockPanel的Closed事件
                dockPanel.ClosedPanel += (object sender, DockPanelEventArgs e) =>
                    {
                        if(e.Panel.ControlContainer!=null)
                        {
                            Control control = e.Panel.ControlContainer.Controls[0];

                            //移除指定名称的文档对象
                            this.RemoveKey(control.Name);

                            //触发DockDocument的FormClosed事件
                            dockDocument.OnFormClosed(dockDocument, e);
                        }
                        

                    };

                dockPanel.ClosingPanel += (object sender, DockPanelCancelEventArgs e) =>
                    {
                        //触发DockDocument的FormClosing事件
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

                //激活已存在的文档
                //DockDocuments[dockDocumentName].DockPanel.Show();
            }
        }

        private void DockDocument_FormClosing(object sender, EventArgs e)
        {

            RemoveKey((sender as DockDocument).Name);
        }

        /// <summary>
        /// 把Document以tab形式停靠到另一个Document
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
                //通过DockPanel找到对应的Document
                if (baseDocument.Control == document.DockPanel)
                {
                    documentDev = baseDocument;
                }

                if (baseDocument.Control == documentTarget.DockPanel)
                {
                    documentDevTarget = baseDocument;
                }
            }
            //停靠document
            iTabbedViewController.Dock(documentDev as Document, (documentDevTarget as Document).Parent);
            //设置document显示（类似Active）
            iTabbedViewController.Select(documentDevTarget as Document);
        }

        /// <summary>
        /// 把Document设为分屏显示
        /// </summary>
        /// <param name="document"></param>
        public void DockDocumentSplit(DockDocument document)
        {
            TabbedView tabbedView = m_Framework.DocumentManager.View as TabbedView;
            ITabbedViewController iTabbedViewController = tabbedView.Controller;

            BaseDocument baseDocument = tabbedView.AddDocument(document.DockPanel);
            //实现分屏显示
            iTabbedViewController.CreateNewDocumentGroup(baseDocument as Document);
        }

        /// <summary>
        /// 获取指定名称的文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
        /// <returns>返回指定名称的文档对象</returns>
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
        /// 移除并关闭指定名称的文档对象
        /// </summary>
        /// <param name="dockDocumentName">容器名称</param>
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
                MessageBox.Show(string.Format("[{0}]容器对象不存在!",dockDocumentName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 移除指定名称的文档对象
        /// </summary>
        /// <param name="dockDocumentName">文档对象名称</param>
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
                MessageBox.Show(string.Format("[{0}]文档对象不存在!",dockDocumentName ), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        /// <summary>
        /// m_Framework将调用此方法来通知DockDocumentService
        /// 服务对象激活文档对象已经发生改变
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">事件参数</param>
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
