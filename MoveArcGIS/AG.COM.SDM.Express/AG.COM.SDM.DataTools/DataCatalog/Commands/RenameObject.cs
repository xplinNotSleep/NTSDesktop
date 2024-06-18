using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.DataSourcesFile;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.DataTools.DataCatalog.Commands
{
    /// <summary>
    /// 重命名 插件类
    /// </summary>
    internal class RenameObject : BaseCommand
    {
        private TreeView m_Tree = null;
        private ImageListWrap m_Images = null;

        public delegate void RenameHandler(string strName);
        public event RenameHandler Rename;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RenameObject()
        {
        }

        /// <summary>
        /// 获取对象可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_Tree.SelectedNode == null) return false;

                DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
                IDataset tDataset = wrap.GeoObject as IDataset;

                if (tDataset!=null)
                {
                    return tDataset.CanRename();
                }
                else
                    return false; 
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (m_Tree.SelectedNode == null)
                return;
            string oldName = "";
            DataItemWrap wrap = m_Tree.SelectedNode.Tag as DataItemWrap;
            object obj = wrap.GeoObject;
            if (obj is ESRI.ArcGIS.Geodatabase.IDataset)
            {
                oldName = (obj as ESRI.ArcGIS.Geodatabase.IDataset).Name;
            }
            else if (obj is ESRI.ArcGIS.Geodatabase.IFeatureDataset)
                oldName = (obj as ESRI.ArcGIS.Geodatabase.IFeatureDataset).Name;
            else
                MessageHandler.ShowInfoMsg("不支持该对象的重命名！", "重命名");
            string[] s = oldName.Split('.');
            string name;
            if (s.Length >= 2)
            {
                name = s[0];
                for (int i = 1; i < s.Length ; i++)
                {
                    name += "." + s[i];
                }
            }
               
            else
                name = s[0];

            try
            {
                FormRename frm = new FormRename();
                frm.OldName = name;
                frm.NewName = name;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    IDataset tDataset = obj as ESRI.ArcGIS.Geodatabase.IDataset;
                    if (tDataset.Type == esriDatasetType.esriDTContainer)
                    {
                        tDataset.Rename(frm.NewName);
                        m_Tree.SelectedNode.Text = frm.NewName;
                        Rename(frm.NewName);
                    }
                    else if (tDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        ESRI.ArcGIS.Geodatabase.IFeatureDataset tFeatureDataset = obj as ESRI.ArcGIS.Geodatabase.IFeatureDataset;
                        tFeatureDataset.Rename(frm.NewName);
                        m_Tree.SelectedNode.Text = tFeatureDataset.Name;
                        if ((tFeatureDataset.FullName as IFeatureDatasetName) != null)
                        {
                            FeatureDatasetItem tFeatureDatasetItem = new FeatureDatasetItem(tFeatureDataset.FullName as IFeatureDatasetName);
                            DataItemWrap tDataItemWrap = new DataItemWrap(tFeatureDatasetItem, false);
                            m_Tree.SelectedNode.Tag = tDataItemWrap;
                        }

                    }
                    else if (tDataset.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        ESRI.ArcGIS.Geodatabase.IFeatureClass tFeatureClass = obj as ESRI.ArcGIS.Geodatabase.IFeatureClass;
                        IDataset tFDataset = tFeatureClass as IDataset;
                        tFDataset.Rename(frm.NewName);
                        m_Tree.SelectedNode.Text = tFeatureClass.AliasName;
                        if ((tFDataset.FullName as IFeatureClassName) != null)
                        {
                            FeatureClassItem tFeatureClassItem = new FeatureClassItem(tDataset.FullName as IFeatureClassName);
                            DataItemWrap tDataItemWrap = new DataItemWrap(tFeatureClassItem, false);
                            m_Tree.SelectedNode.Tag = tDataItemWrap;
                        }

                    }
                    else if (tDataset.Type == esriDatasetType.esriDTRasterDataset)
                    {
                        ESRI.ArcGIS.Geodatabase.IRasterDataset tRasterDataset = obj as ESRI.ArcGIS.Geodatabase.IRasterDataset;
                        IDataset tRDataset = tRasterDataset as IDataset;
                        tRDataset.Rename(frm.NewName);
                        m_Tree.SelectedNode.Text = frm.NewName;
                        if ((tRDataset.FullName as IRasterDatasetName) != null)
                        {
                            RasterDatasetItem tRasterDatasetItem = new RasterDatasetItem(tRDataset.FullName as IRasterDatasetName);
                            DataItemWrap tDataItemWrap = new DataItemWrap(tRasterDatasetItem, false);
                            m_Tree.SelectedNode.Tag = tDataItemWrap;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMsg(ex.Message, "重命名");
            }

        }

        /// <summary>
        /// 创建时处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_Tree = (hook as DataManagerHook).TreeView;
            m_Images = (hook as DataManagerHook).Images;
        }


    }
}
