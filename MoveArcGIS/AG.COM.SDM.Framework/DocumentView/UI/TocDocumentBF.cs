using AG.COM.SDM.SystemUI;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ��ͼ�ĵ���
    /// </summary>
    public partial class TocDocument : DockDocument, ITocDocumentView
    {
        private IFramework m_Framework;
        private ILayer m_Layer;
        private IBasicMap m_BiscMap;
        private ILayer m_ToLayer = null;
        private IContextMenu m_MapContextMenu = null;
        private IContextMenu m_LayerContextMenu = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pFramework">IFramework����</param>
        public TocDocument(IFramework pFramework)
        {
            //��ʼ���������
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

        #region ITocDocumentView ��Ա

        /// <summary>
        /// ���ø��ĵ�������Դ����
        /// </summary>
        /// <param name="pTocBuddy">�󶨶���</param>
        public void SetBuddyControl(object pTocBuddy)
        {
            this.axTOCControl1.SetBuddyControl(pTocBuddy);
            this.axTOCControl1.Refresh();
        }

        /// <summary>
        /// ��ȡ�����õ�ͼ��������Ĳ˵�
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
        /// ��ȡ������ͼ���������Ĳ˵�
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
                //�����¼�,֪ͨ���еǼǸ��¼��Ķ���
                this.m_Framework.OnCurrentLayerChanged(tLayer, null);
            }

            try
            {
                if (e.button == 2)        //����Ҽ������¼�     
                {
                    if (esriTOCControlItem.esriTOCControlItemMap == tocControlItem)
                    {
                        //ѡ����Ϊ��ͼ����ʱ
                        IPlugin tPlugin = this.m_MapContextMenu as IPlugin;
                        tPlugin.OnCreate(this.m_Framework);
                        this.m_MapContextMenu.PopupMenu(e.x, e.y, this.axTOCControl1.hWnd);
                    }
                    else if (esriTOCControlItem.esriTOCControlItemLayer == tocControlItem)
                    {
                        //ѡ����Ϊͼ��ʱ�����  
                        IPlugin tPlugin = this.m_LayerContextMenu as IPlugin;
                        tPlugin.OnCreate(this.m_Framework);
                        this.m_LayerContextMenu.PopupMenu(e.x, e.y, this.axTOCControl1.hWnd);
                    }
                }
                else if(e.button == 1)
                {
                    if (esriTOCControlItem.esriTOCControlItemLayer == tocControlItem)
                    {
                        if(this.m_Layer is IGroupLayer)
                        {
                            IGroupLayer groupLayer = this.m_Layer as IGroupLayer;
                        }
                        else if (this.m_Layer is IFeatureLayer)
                        {
                            IFeatureLayer featureLayer = this.m_Layer as IFeatureLayer;
                        }
                        //this.m_Layer.Visible = false;
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

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
           /*
            esriTOCControlItem tocControlItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer tLayer = null;
            object tLengedGroup = null;
            object tIndex = null;

            this.axTOCControl1.HitTest(e.x, e.y, ref tocControlItem, ref m_BiscMap, ref tLayer, ref tLengedGroup, ref tIndex);
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

                this.axTOCControl1.Update();
            }
           */
        }

       private IGroupLayer GetGroupLayer(IFeatureLayer pLayer)
        {
            IGroupLayer group = null;
            ILayer tLayer;
            for (int i = 0; i <= m_BiscMap.LayerCount - 1; i++)
            {
                tLayer = m_BiscMap.get_Layer(i);
                if (tLayer is IGroupLayer)
                {
                    IGroupLayer groupLayer = tLayer as IGroupLayer;
                    ICompositeLayer clayer = groupLayer as ICompositeLayer;
                    ILayer layer;
                    TreeNode node;
                    for (int j = 0;j <= clayer.Count - 1; j++)
                    {
                        layer = clayer.get_Layer(j);
                        if (layer is IFeatureLayer)
                        {
                            IFeatureLayer featureLayer = layer as IFeatureLayer;
                            if(featureLayer.FeatureClass.AliasName ==pLayer.FeatureClass.AliasName)
                            {
                                group = tLayer as IGroupLayer;
                            }
                        }
                    }
                }
            }

            return group;
        }
      
    }
}