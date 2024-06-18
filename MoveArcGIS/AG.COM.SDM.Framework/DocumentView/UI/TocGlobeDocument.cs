using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
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
    public partial class TocGlobeDocument :  DockDocument, ITocDocumentView
    {
        private ILayer m_Layer;
        private IBasicMap m_BiscMap;
        private IFramework m_Framework;
        private IContextMenu m_MapContextMenu = null;
        private IContextMenu m_LayerContextMenu = null;
        public TocGlobeDocument(IFramework pFramework)
        {
            InitializeComponent();
            this.axTOCControl1.EnableLayerDragDrop = true;
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
        /// 设置该文档的数据源对象
        /// </summary>
        /// <param name="pTocBuddy">绑定对象</param>
        public void SetBuddyControl(object pTocBuddy)
        {
            this.axTOCControl1.SetBuddyControl(pTocBuddy);
            this.axTOCControl1.Refresh();
        }

        /// <summary>
        /// 获取或设置地图结点上下文菜单
        /// </summary>
        public IContextMenu MapContextMenu
        {
            get
            {
                return this.m_MapContextMenu;
            }
            set
            {
                this.m_MapContextMenu = value;
            }
        }

        /// <summary>
        /// 获取或设置图层结点上下文菜单
        /// </summary>
        public IContextMenu LayerContextMenu
        {
            get
            {
                return this.m_LayerContextMenu;
            }
            set
            {
                this.m_LayerContextMenu = value;
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
            get { return EnumDocumentType.TocGlobeDocument; }
        }

        /// <summary>
        /// axTocControl.Object对象
        /// </summary>
        public object Hook
        {
            get { return this.axTOCControl1.Object; }
        }

        #endregion

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem tocControlItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer tLayer = null;
            object tLengedGroup = null;
            object tIndex = null;

            this.axTOCControl1.HitTest(e.x, e.y, ref tocControlItem, ref m_BiscMap, ref tLayer, ref tLengedGroup, ref tIndex);

            if (this.m_Layer != tLayer)
            {
                this.m_Layer = tLayer;
                //激发事件,通知所有登记该事件的对象
                this.m_Framework.OnCurrentLayerChanged(tLayer, null);
            }

            try
            {
                if (e.button == 2)        //鼠标右键单击事件     
                {
                    if (esriTOCControlItem.esriTOCControlItemMap == tocControlItem)
                    {
                        //选择项为地图对象时
                        IPlugin tPlugin = this.m_MapContextMenu as IPlugin;
                        tPlugin.OnCreate(this.m_Framework);
                        this.m_MapContextMenu.PopupMenu(e.x, e.y, this.axTOCControl1.hWnd);
                    }
                    else if (esriTOCControlItem.esriTOCControlItemLayer == tocControlItem)
                    {
                        //选择项为图层时的情况  
                        IPlugin tPlugin = this.m_LayerContextMenu as IPlugin;
                        tPlugin.OnCreate(this.m_Framework);
                        this.m_LayerContextMenu.PopupMenu(e.x, e.y, this.axTOCControl1.hWnd);
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.SystemUI.Utility.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
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
