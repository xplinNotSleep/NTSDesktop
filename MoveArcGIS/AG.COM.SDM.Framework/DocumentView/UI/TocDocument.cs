using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{

    public partial class TocDocument : DockDocument, ITocDocumentView
    {
        private IFramework m_Framework;
        private IContextMenu m_DefaultContextMenu = null;

        public TocDocument(IFramework pFramework)
        {
            InitializeComponent();
            this.m_Framework = pFramework;
            this.SetDockPanelEvent += new EventHandler(TocDocument_SetDockPanelEvent);
        }

        void TocDocument_SetDockPanelEvent(object sender, EventArgs e)
        {
            if (DockPanel != null)
            {
                DockPanel.Options.ShowCloseButton = false;
            }
        }

        #region ITocDocumentView 成员

        /// <summary>
        /// 获取或设置地图结点上下文菜单
        /// </summary>
        public IContextMenu DefaultContextMenu
        {
            get
            {
                return this.m_DefaultContextMenu;
            }
            set
            {
                this.m_DefaultContextMenu = value;
            }
        }


        #endregion

        #region IDocumentView 成员
        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        /// <summary>
        /// 文档类型
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.TocDocument; }
        }

        /// <summary>
        /// TocDocument的Object对象
        /// </summary>
        public object Hook
        {
            get { return this.TocTreeView.DataBindings; }
        }

        #endregion

        /// <summary>
        /// 右键单击可弹出上下文菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TocTreeView_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)        //鼠标右键单击事件     
            {
                this.m_DefaultContextMenu.OnCreate(this.m_Framework);
                this.m_DefaultContextMenu.PopupMenu(e.X, e.Y, TocTreeView.Handle);
            }
        }

        public void HideEx()
        {
            if (this.DockPanel != null)
            {
                this.DockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                this.DockPanel.HideImmediately();
            }
        }

    }


}
