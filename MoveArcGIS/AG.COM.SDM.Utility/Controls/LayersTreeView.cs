using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// FeatureLayerͼ�����ؼ�
    /// </summary>
    public partial class LayersTreeView : TreeViewRepair
    {
        IList<string> m_ReturnIlist= new List<string>();
        IList<string> m_ReturnAliasNames = new List<string>();

        public LayersTreeView()
        {
            InitializeComponent();

            this.CheckBoxes = true;
            this.ImageList = imageList1;
        }	

        #region ��ʼ��

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="tMap"></param>
        public void InitUI(IBasicMap tMap)
        {
            InitUI(tMap, false);
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="tMap"></param>
        /// <param name="tCheckWithVisible">ѡ�п��ӵ�</param>
        public void InitUI(IBasicMap tMap, bool tCheckWithVisible)
        {
            this.Nodes.Clear();
            ///��ʼ��ͼ����
            ILayer tLayer;
            for (int i = 0; i <= tMap.LayerCount - 1; i++)
            {
                tLayer = tMap.get_Layer(i);
                AddLayerNode(this.Nodes, tLayer);
            }

            if (tCheckWithVisible == true)
            {
                SetCheckWithVisible(this.Nodes);
            }

            this.ExpandAll();

            //Ĭ��ѡ���׸��ڵ�
            if (this.Nodes.Count > 0)
                this.SelectedNode = this.Nodes[0];
        }

        /// <summary>
        /// ���ͼ�㵽treeview
        /// </summary>
        /// <param name="tParentNodeColl"></param>
        /// <param name="tLayer"></param>
        public void AddLayerNode(TreeNodeCollection tParentNodeColl, ILayer tLayer)
        {
            TreeNode tNode = null;
            ///��ͼ��
            if (tLayer is IGroupLayer)
            {
                tNode = new TreeNode();
                tNode.Tag = tLayer;
                tNode.Text = tLayer.Name;
                tNode.ImageIndex = 2;
                tNode.SelectedImageIndex = tNode.ImageIndex;

                ILayer tLayerChild = null;
                ICompositeLayer tCompositeLayer = tLayer as ICompositeLayer;
                ///�����ͼ���µ�ͼ��
                for (int i = 0; i <= tCompositeLayer.Count - 1; i++)
                {
                    tLayerChild = tCompositeLayer.get_Layer(i);
                    AddLayerNode(tNode.Nodes, tLayerChild);
                }

                tParentNodeColl.Add(tNode);
            }
            ///Ҫ��ͼ��
            else if (tLayer is IFeatureLayer)
            {
                tNode = new TreeNode();
                tNode.Text = tLayer.Name;
                IDataset pDataset = (tLayer as IFeatureLayer).FeatureClass as IDataset;
                if (pDataset == null) return;
                string name = pDataset.BrowseName;
                string[] temps = name.Split('.');
                name = temps[temps.Length - 1];

                IFeatureLayer tFeatureLayer = tLayer as IFeatureLayer;
                tNode.ImageIndex = GetImageIndex(tFeatureLayer);
                tNode.SelectedImageIndex = tNode.ImageIndex;

                tNode.Tag = tLayer;
                tNode.Checked = false;
                tParentNodeColl.Add(tNode);
                #region ��Ҫ�����ֶ�����dll,����Ǩ�Ƶ�������Ŀ��
                //if (Pipe.FieldManager.FieldConfigHelper.IsPipeLayer(name) == true)
                //{
                //    IFeatureLayer tFeatureLayer = tLayer as IFeatureLayer;
                //    tNode.ImageIndex = GetImageIndex(tFeatureLayer);
                //    tNode.SelectedImageIndex = tNode.ImageIndex;

                //    tNode.Tag = tLayer;
                //    tNode.Checked = false;
                //    tParentNodeColl.Add(tNode);
                //}
                //else if(Pipe.FieldManager.FieldConfigHelper.IsDLayer(name) == true)
                //{
                //    IFeatureLayer tFeatureLayer = tLayer as IFeatureLayer;
                //    tNode.ImageIndex = GetImageIndex(tFeatureLayer);
                //    tNode.SelectedImageIndex = tNode.ImageIndex;

                //    tNode.Tag = tLayer;
                //    tNode.Checked = false;
                //    tParentNodeColl.Add(tNode);
                //}
                #endregion
            }
        }

        /// <summary>
        /// ��ȡ�ڵ��ImageIndex������ͼ�����ͣ�
        /// </summary>
        /// <param name="tFeatureLayer"></param>
        /// <returns></returns>
        private int GetImageIndex(IFeatureLayer tFeatureLayer)
        {
            int result = 0;

            if (tFeatureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                result = 6;             
            }
            else
            {
                esriGeometryType tShapeType = tFeatureLayer.FeatureClass.ShapeType;
                if (tShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    result = 5;               
                }
                else if (tShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    result = 4;                 
                }
                else if (tShapeType == esriGeometryType.esriGeometryPoint)
                {
                    result = 3;                
                }
            }

            return result;
        }

        /// <summary>
        /// �ѿ��ӵ�ͼ���check��Ϊtrue
        /// </summary>
        /// <param name="tNodes"></param>
        private void SetCheckWithVisible(TreeNodeCollection tNodes)
        {
            foreach (TreeNode tNode in tNodes)
            {
                ILayer tLayer = tNode.Tag as ILayer;
                if (tLayer != null && tLayer.Visible == true)
                {
                    tNode.Checked = true;
                }

                SetCheckWithVisible(tNode.Nodes);
            }
        }

        #endregion

        #region ��ȡѡ����

        /// <summary>
        /// ��ȡѡ���FeatureLayer
        /// </summary>
        /// <returns></returns>
        public List<ILayer> GetSelectFeatureLayer()
        {
            List<ILayer> tLayers = new List<ILayer>();

            AddSelectLayerToList(this.Nodes, tLayers);

            return tLayers;
        }

        /// <summary>
        /// ��ѡ���ͼ����ӵ�һ��list
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <param name="tLayers"></param>
        private void AddSelectLayerToList(TreeNodeCollection tNodeColl, List<ILayer> tLayers)
        {
            foreach (TreeNode tNode in tNodeColl)
            {
                if (tNode.Tag is IFeatureLayer && tNode.Checked == true)
                {
                    tLayers.Add(tNode.Tag as IFeatureLayer);
                }

                AddSelectLayerToList(tNode.Nodes, tLayers);
            }
        }

        #endregion

        #region �ڵ�ѡ��

        /// <summary>
        /// �ݹ����ø��ڵ�ѡ��״̬
        /// </summary>
        /// <param name="ptreeNode">��ǰ�ڵ�</param>
        public void SetParentNodeChecked(TreeNode ptreeNode)
        {
            if (ptreeNode.Checked == true)
            {
                if (ptreeNode.Parent != null)
                {
                    ptreeNode.Parent.Checked = ptreeNode.Checked;
                    //�ݹ����ø��ڵ�ѡ��״̬
                    SetParentNodeChecked(ptreeNode.Parent);
                }
            }
            else
            {
                if (ptreeNode.Parent != null)
                {
                    if (!HasChildChecked(ptreeNode.Parent))
                    {
                        ptreeNode.Parent.Checked = false;
                        SetParentNodeChecked(ptreeNode.Parent);
                    }
                }
            }
        }

        /// <summary>
        /// �ӽڵ��Ƿ���ѡ��
        /// </summary>
        /// <param name="ptreeNode"></param>
        /// <returns></returns>
        private bool HasChildChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode subNode in ptreeNode.Nodes)
            {
                if (subNode.Checked) return true;
            }

            return false;
        }

        /// <summary>
        /// �ݹ������ӽڵ�ѡ��״̬
        /// </summary>
        /// <param name="ptreeNode">��ǰ�ڵ�</param>
        public void SetChildNodeChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode treeNode in ptreeNode.Nodes)
            {
                treeNode.Checked = ptreeNode.Checked;
                //�ݹ������ӽڵ�ѡ��״̬
                SetChildNodeChecked(treeNode);
            }
        }
        #endregion
    }
}
