using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Utility.Controls
{
    public class ComboBoxLayerTreeView : ComboBoxTreeView
    {
        private System.ComponentModel.IContainer components;
        /// <summary>
        /// 是否只显示可选图层
        /// </summary>
        public bool IsSelectable = false;

        public ComboBoxLayerTreeView()
        {
           
        }

        /// <summary>
        /// 初始化图层树
        /// </summary>
        /// <param name="tMap"></param>
        public void InitLayerTreeView(IMap tMap)
        {
            if (tMap == null) return;

            ILayer layer;
            TreeNode node;
            this.TreeView.Nodes.Clear();
            for (int i = 0; i < tMap.LayerCount; i++)
            {
                layer = tMap.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    node = new TreeNode(layer.Name);

                    //递归添加图层节点
                    AddGroupLayers(layer as IGroupLayer, node);

                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        this.TreeView.Nodes.Add(node);
                    }
                }
                else
                {
                    if (IsSelectable == false)
                    {
                        //添加图层对象
                        AddLayer(layer, null);
                    }
                    else
                    {
                        IFeatureLayer tFeatLayer = layer as IFeatureLayer;
                        if (tFeatLayer != null && tFeatLayer.Selectable)
                            AddLayer(layer, null);
                    }
                }
            }

            this.TreeView.ExpandAll();
        }

        /// <summary>
        /// 递归添加组图层对象
        /// </summary>
        /// <param name="glayer">组图层对象</param>
        /// <param name="parentNode">父节点</param>
        private void AddGroupLayers(IGroupLayer glayer, TreeNode parentNode)
        {
            ICompositeLayer clayer = glayer as ICompositeLayer;

            ILayer layer;
            TreeNode node;

            for (int i = 0; i <= clayer.Count - 1; i++)
            {
                layer = clayer.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    node = new TreeNode(layer.Name);

                    //递归添加组图层对象
                    AddGroupLayers(layer as IGroupLayer, node);

                    node.ImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        parentNode.Nodes.Add(node);
                    }
                }
                else
                {
                    if (IsSelectable == false)
                    {
                        //添加图层对象
                        AddLayer(layer, parentNode);
                    }
                    else
                    {
                        if ((layer as IFeatureLayer).Selectable == true)
                            AddLayer(layer, parentNode);
                    }
                }
            }
        }

        /// <summary>
        /// 添加图层对象
        /// </summary>
        /// <param name="pLayer">图层对象</param>
        /// <param name="pTreeNode">树节点对象</param>
        private void AddLayer(ILayer pLayer, TreeNode pTreeNode)
        {
            IFeatureLayer2 tFeaturelayer = pLayer as IFeatureLayer2;
            if (tFeaturelayer == null) return;

            Utility.Wrapers.LayerWrapper wrap = new AG.COM.SDM.Utility.Wrapers.LayerWrapper(pLayer);

            TreeNode treeNode = new TreeNode(wrap.ToString());
            treeNode.Tag = wrap;
            treeNode.ImageIndex = 1;
            treeNode.SelectedImageIndex = 1;
            if (pTreeNode == null)
            {
                this.TreeView.Nodes.Add(treeNode);
            }
            else
            {
                pTreeNode.Nodes.Add(treeNode);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
