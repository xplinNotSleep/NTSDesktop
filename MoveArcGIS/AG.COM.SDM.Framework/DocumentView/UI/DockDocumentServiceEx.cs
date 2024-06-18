using AG.COM.SDM.SystemUI;
using DevExpress.XtraBars.Docking;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Framework.DocumentView.UI
{
    public static class DockDocumentServiceEx
    {
        /// <summary>
        /// 隐藏指定文档
        /// </summary>
        /// <param name="m_HookHelper"></param>
        /// <param name="dockDocumentName"></param>
        public static void AutoHide(this IHookHelper m_HookHelper, string dockDocumentName)
        {
            var c = m_HookHelper.DockDocumentService.GetDockDocument(dockDocumentName);
            if (c == null) return;
            if (c.DockPanel == null) return;
            c.DockPanel.Visibility = DockVisibility.AutoHide;
            c.DockPanel.HideImmediately();
        }
        /// <summary>
        /// 恢复布局
        /// </summary>
        /// <param name="m_HookHelper"></param>
        public static void Recovery(this IHookHelper m_HookHelper)
        {
            //显示图层目录树
            TocDocument pTocDocument = m_HookHelper.DockDocumentService.GetDockDocument(Convert.ToString(EnumDocumentType.TocDocument)) as TocDocument;
            if (pTocDocument != null)
            {
                //pTocDocument.DockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
                pTocDocument.DockPanel.Visibility = DockVisibility.Visible;
                pTocDocument.DockPanel.Width = 300;
                pTocDocument.DockPanel.Show();
            }
           
        }
        /// <summary>
        /// 隐藏鹰眼 隐藏TOC隐藏任务导航条
        /// </summary>
        /// <param name="m_HookHelper"></param>
        public static void HideAll(this IHookHelper m_HookHelper)
        {
            //隐藏鹰眼
            //m_HookHelper.AutoHide(Convert.ToString(EnumDocumentType.EagleDocument));
            //隐藏TOC
            m_HookHelper.AutoHide(Convert.ToString(EnumDocumentType.TocDocument));
            //隐藏任务导航条
            m_HookHelper.AutoHide("FormTaskNavigation");

        }
        
        public static void AddDockDocumentSplit(this IHookHelper m_HookHelper, string dockDocumentName, DockDocument dockDocument)
        {
            IDockDocumentService pDockDocumentService = m_HookHelper.DockDocumentService;
            if (!pDockDocumentService.ContainsDocument(dockDocumentName))
            {
                pDockDocumentService.AddDockDocumentSplit(dockDocumentName, dockDocument);
                dockDocument.DockPanel.Options.AllowFloating = false;
                dockDocument.DockPanel.Options.ShowCloseButton = false;
                dockDocument.DockPanel.Options.ShowAutoHideButton = false;
                dockDocument.DockPanel.Options.ShowMaximizeButton = false;
                dockDocument.DockPanel.Options.FloatOnDblClick = false;

            }
            else
            {
                pDockDocumentService.GetDockDocument(dockDocumentName).Show();
            }
        }
        /// <summary>
        /// 激活主窗口
        /// </summary>
        /// <param name="m_HookHelper"></param>
        public static void ActivateMap(this IHookHelper m_HookHelper)
        {
            //显示地图文档窗体
            //AG.COM.SDM.Framework.DocumentView.MapDocument pMapDocument = m_HookHelper.DockDocumentService.GetDockDocument(Convert.ToString(EnumDocumentType.MapDocument)) as AG.COM.SDM.Framework.DocumentView.MapDocument;
            //if (pMapDocument != null)
            //{
            //    pMapDocument.Show();
            //}
        }


    }
}
