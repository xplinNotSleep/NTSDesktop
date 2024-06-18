using AG.COM.SDM.SystemUI;
using DevExpress.XtraBars.Docking;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ��ͼ�ĵ���
    /// </summary>
    public partial class TocDocumentCopy : DockDocument, ITocDocumentView
    {
        private IFramework m_Framework;
        private IContextMenu m_DefaultContextMenu = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pFramework">IFramework����</param>
        public TocDocumentCopy(IFramework pFramework)
        {
            //��ʼ���������
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

        #region ITocDocumentView ��Ա

        /// <summary>
        /// ���ø��ĵ�������Դ����
        /// </summary>
        /// <param name="pTocBuddy">�󶨶���</param>
        public void SetBuddyControl(object pTocBuddy)
        {
            this.listBoxControl.DataSource=pTocBuddy;
            this.listBoxControl.Refresh();
        }

        /// <summary>
        /// ��ȡ�����õ�ͼ��������Ĳ˵�
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

        #region IDocumentView ��Ա
        /// <summary>
        /// �ĵ�����
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
        /// �ĵ�����
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.TocDocument; }
        }

        /// <summary>
        /// axTocControl.Object����
        /// </summary>
        public object Hook
        {
            get { return this.listBoxControl.DataSource; }
        }

        #endregion

        private void listBoxControl_OnMouseDown(object sender, MouseEventArgs e)
        {
            object tLengedGroup = null;
            object tIndex = null;


            if (e.Button == MouseButtons.Right)        //����Ҽ������¼�     
            {
                IPlugin tPlugin = this.m_DefaultContextMenu as IPlugin;
                tPlugin.OnCreate(this.m_Framework);
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

        private void listBoxControl_OnMouseUp(object sender, MouseEventArgs e)
        {
            /*
             esriTOCControlItem tocControlItem = esriTOCControlItem.esriTOCControlItemNone;
             ILayer tLayer = null;
             object tLengedGroup = null;
             object tIndex = null;

             this.listBoxControl.HitTest(e.x, e.y, ref tocControlItem, ref m_BiscMap, ref tLayer, ref tLengedGroup, ref tIndex);
             if (e.button == 1)
             {
                 if (esriTOCControlItem.esriTOCControlItemLayer == tocControlItem)
                 {
                     if (tLayer is IGroupLayer)
                     {
                         IGroupLayer groupLayer = tLayer as IGroupLayer;
                         ICompositeLayer clayer = groupLayer as ICompositeLayer;
                         ILayer layer;
                         for (int i = 0; i <= clayer.Count - 1; i++)
                         {
                             layer = clayer.get_Layer(i);
                             if (layer is IFeatureLayer)
                             {
                                 IFeatureLayer featureLayer = layer as IFeatureLayer;
                                 featureLayer.Visible = !groupLayer.Visible;
                             }
                         }
                     }
                     else if (tLayer is IFeatureLayer)
                     {
                         //IFeatureLayer featureLayer = tLayer as IFeatureLayer;
                         //IGroupLayer groupLayer =GetGroupLayer(featureLayer);
                         //ICompositeLayer clayer = groupLayer as ICompositeLayer;
                         //ILayer layer;
                         //bool pVisible = false;
                         //for (int i = 0; i <= clayer.Count - 1; i++)
                         //{
                         //    layer = clayer.get_Layer(i);
                         //    if (layer is IFeatureLayer)
                         //    {
                         //        if (layer.Name == featureLayer.Name)
                         //        {
                         //            pVisible = !layer.Visible;
                         //        }
                         //        else
                         //        {
                         //            if (layer.Visible)
                         //            {
                         //                pVisible = true;
                         //                break;
                         //            }
                         //        }

                         //    }
                         //}
                         //groupLayer.Visible = pVisible;

                     }
                     //this.m_Layer.Visible = false;
                 }

                 this.listBoxControl.Update();
             }
            */
        }


    }
}