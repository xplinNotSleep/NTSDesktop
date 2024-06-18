using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataProcess.CoClass
{
    public class ExportShpHelper
    {
        /// <summary>
        /// 进度条
        /// </summary>
        private ITrackProgress m_TrackProgress = null;

        public bool Export(List<IFeatureLayer> featureLayers, string targetFolder)
        {
            try
            {
                m_TrackProgress = new TrackProgressDialog();
                
                m_TrackProgress.AutoFinishClose = true;
                m_TrackProgress.DisplayTotal = true;
                m_TrackProgress.TotalValue = 0;
                m_TrackProgress.TotalMessage = "";
                m_TrackProgress.TotalMax = featureLayers.Count;

                m_TrackProgress.SubMessage = "";
                m_TrackProgress.SubValue = 0;

                m_TrackProgress.Show();
                Application.DoEvents();

                foreach (IFeatureLayer featureLayer in featureLayers)
                {
                    m_TrackProgress.TotalMessage = "正在导出图层" + featureLayer.Name + "，第" + (m_TrackProgress.TotalValue + 1).ToString() + "个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
                    Application.DoEvents();

                    //新旧字段的匹配
                    Dictionary<int, int> fieldMatch = null;
                    //创建导入目标的FeatureClass
                    IFeatureClass featureClassTarget = CreateTargetFeatureClass(featureLayer.FeatureClass, targetFolder, ref fieldMatch);

                    IWorkspaceEdit workspaceEditTarget = (featureClassTarget as IDataset).Workspace as IWorkspaceEdit;
                    //从源FeatureClass复制数据到目标FeatureClass
                    CopyFeatureClassData(featureLayer.FeatureClass, featureClassTarget, workspaceEditTarget, fieldMatch);

                    m_TrackProgress.TotalValue++;
                    HandleStop();
                }

                m_TrackProgress.SetFinish();

                AG.COM.SDM.Utility.Common.MessageHandler.ShowInfoMsg("备份完成", "提示");

                return true;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");

                if (m_TrackProgress != null)
                {
                    m_TrackProgress.SetFinish();
                }

                return false;
            }
        }

        /// <summary>
        /// 创建导入目标的FeatureClass
        /// </summary>
        /// <param name="tFeatureClassSource"></param>
        /// <param name="targetFolder"></param>
        /// <param name="tFieldMatch"></param>
        /// <returns></returns>
        private IFeatureClass CreateTargetFeatureClass(IFeatureClass tFeatureClassSource, string targetFolder, ref  Dictionary<int, int> tFieldMatch)
        {
            //新旧字段索引匹配，key为新字段索引，value为旧字段索引
            tFieldMatch = new Dictionary<int, int>();
            //新字段名称与旧字段Index匹配
            Dictionary<string, int> tNewNameWithOldIdx = new Dictionary<string, int>();

            //创建shp文件
            IWorkspaceFactory tWorkspaceFactoryTarget = new ShapefileWorkspaceFactoryClass();

            if (Directory.Exists(targetFolder) == false)
                Directory.CreateDirectory(targetFolder);

            IWorkspace tWorkspaceTarget = tWorkspaceFactoryTarget.OpenFromFile(targetFolder, 0);
            IFeatureWorkspace tFeatureWorkspaceTarget = tWorkspaceTarget as IFeatureWorkspace;

            //验证字段，如不符合的字段名称
            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.InputWorkspace = (tFeatureClassSource as IDataset).Workspace;
            tFieldChecker.ValidateWorkspace = tFeatureWorkspaceTarget as IWorkspace;
            IFields tFieldsValid = null;
            IEnumFieldError tEnumFieldError = null;
            tFieldChecker.Validate(tFeatureClassSource.Fields, out tEnumFieldError, out tFieldsValid);

            //过滤掉Shapefile不支持的字段类型
            IFieldsEdit tFieldsEditNew = new FieldsClass();
            for (int i = 0; i < tFieldsValid.FieldCount; i++)
            {
                IField tFieldValid = tFieldsValid.get_Field(i);               

                if (tFieldValid.Type != esriFieldType.esriFieldTypeBlob && tFieldValid.Type != esriFieldType.esriFieldTypeRaster &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeXML &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeGUID)
                {
                    tFieldsEditNew.AddField(tFieldValid);
                    //写入新字段名称与就字段Index匹配
                    tNewNameWithOldIdx.Add(tFieldValid.Name, i);
                }
            }

            string tFileNameWithoutExt = (tFeatureClassSource as IDataset).Name;

            //防止文件重名
            string tFileName = System.IO.Path.Combine(targetFolder, tFileNameWithoutExt + ".shp");
            int k = 1;
            while (File.Exists(tFileName) == true)
            {
                tFileName = System.IO.Path.Combine(targetFolder, tFileNameWithoutExt + "_" + k + ".shp");
                k++;
            }

            IFeatureClass tFeatureClassTarget = tFeatureWorkspaceTarget.CreateFeatureClass(System.IO.Path.GetFileNameWithoutExtension(tFileName),
                          tFieldsEditNew, null, null, esriFeatureType.esriFTSimple, tFeatureClassSource.ShapeFieldName, null);
            //生成新旧字段Index匹配，因此创建FeatureClass时可能会新增字段，因此创建FeatureClass后根据新字段名称与
            foreach (KeyValuePair<string, int> kvp in tNewNameWithOldIdx)
            {
                int tNewIdx = tFeatureClassTarget.Fields.FindField(kvp.Key);
                if (tNewIdx >= 0)
                {
                    tFieldMatch.Add(tNewIdx, kvp.Value);
                }
            }

            return tFeatureClassTarget;
        }

        /// <summary>
        /// 复制要素类
        /// </summary>
        /// <param name="tFeatureClassSource"></param>
        /// <param name="tFeatureClassTarget"></param>
        /// <param name="tWorkspaceEditTarget"></param>
        private void CopyFeatureClassData(IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget, IWorkspaceEdit tWorkspaceEditTarget, Dictionary<int, int> fieldMatch)
        {
            IFeatureCursor tFeatureCursorSource = null, tFeatureCursorTarget = null;
            IFeatureBuffer tFeatureBufferTarget = null;
            IFeature tFeatureSource = null;

            tWorkspaceEditTarget.StartEditing(false);
            tWorkspaceEditTarget.StartEditOperation();

            try
            {
                //源要素类的数据复制到目标要素类
                tFeatureCursorSource = tFeatureClassSource.Search(null, false);
                tFeatureCursorTarget = tFeatureClassTarget.Insert(true);

                m_TrackProgress.SubValue = 0;
                m_TrackProgress.SubMax = tFeatureClassSource.FeatureCount(null);

                tFeatureSource = tFeatureCursorSource.NextFeature();
                while (tFeatureSource != null)
                {
                    m_TrackProgress.SubValue++;
                    m_TrackProgress.SubMessage = "正在导出第" + m_TrackProgress.SubValue.ToString()
                        + "条记录（共" + m_TrackProgress.SubMax + "条）";
                    Application.DoEvents();

                    tFeatureBufferTarget = tFeatureClassTarget.CreateFeatureBuffer();

                    tFeatureBufferTarget.Shape = tFeatureSource.ShapeCopy;
                    IFields tFieldsTarget = tFeatureClassTarget.Fields;

                    //赋值一般属性值
                    SetAttributes(fieldMatch, tFieldsTarget, tFeatureBufferTarget, tFeatureSource);

                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureBufferTarget);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureSource);

                    HandleStop();

                    tFeatureSource = tFeatureCursorSource.NextFeature();
                }

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);

                throw ex;
            }
            finally
            {
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorSource);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorTarget);
            }
        }

        /// <summary>
        /// 复制一般属性值
        /// </summary>
        /// <param name="tFieldMatch"></param>
        /// <param name="tFields"></param>
        /// <param name="tFeatureBuffer"></param>
        /// <param name="tFeatureSource"></param>
        private void SetAttributes(Dictionary<int, int> tFieldMatch, IFields tFields, IFeatureBuffer tFeatureBuffer, IFeature tFeatureSource)
        {
            //使用创建FeatureClass时的字段匹配，key为新字段，value为对应的旧字段
            foreach (KeyValuePair<int, int> kvp in tFieldMatch)
            {
                IField tFieldTarget = tFields.get_Field(kvp.Key);
                //几何字段另外赋值，不可编辑字段不复制
                if (tFieldTarget.Type != esriFieldType.esriFieldTypeGeometry && tFieldTarget.Editable == true)
                {
                    if (tFieldTarget.Type == esriFieldType.esriFieldTypeString)
                    {
                        tFeatureBuffer.set_Value(kvp.Key, Convert.ToString(tFeatureSource.get_Value(kvp.Value)));
                    }
                    else if (tFieldTarget.Type == esriFieldType.esriFieldTypeDouble || tFieldTarget.Type == esriFieldType.esriFieldTypeInteger
                         || tFieldTarget.Type == esriFieldType.esriFieldTypeSingle || tFieldTarget.Type == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        //shapefile的数字类型字段不能为空，原值为空要赋值0
                        double value = 0;
                        if (double.TryParse(Convert.ToString(tFeatureSource.get_Value(kvp.Value)), out value) == true)
                        {
                            tFeatureBuffer.set_Value(kvp.Key, value);
                        }
                        else
                        {
                            tFeatureBuffer.set_Value(kvp.Key, 0);
                        }
                    }
                    else
                    {
                        tFeatureBuffer.set_Value(kvp.Key, tFeatureSource.get_Value(kvp.Value));
                    }
                }
            }
        }

        /// <summary>
        /// 进度条停止操作判断并处理
        /// </summary>
        private void HandleStop()
        {
            if (m_TrackProgress.IsContinue == false)
            {
                throw new Exception("停止操作");
            }
        }
    }
}
