using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// Workspace帮助类
    /// </summary>
    public class WorkspaceAGHelper
    {
        /// <summary>
        /// 创建FielGdb
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="gdbName"></param>
        /// <returns></returns>
        public static IFeatureWorkspace CreateFileGdb(string directory, string gdbName)
        {
            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
            IName name = (IName)workspaceFactory.Create(directory, gdbName, null, 0);
            return (IFeatureWorkspace)name.Open();
        }

        /// <summary>
        /// 创建Personal GDB（Mdb）
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="gdbName"></param>
        /// <returns></returns>
        public static IFeatureWorkspace CreatePersonalGdb(string directory, string gdbName)
        {
            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();
            IName name = (IName)workspaceFactory.Create(directory, gdbName, null, 0);
            return (IFeatureWorkspace)name.Open();
        }

        /// <summary>
        /// 获取Mdb的Workspace
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IFeatureWorkspace GetWorkspaceFromMdb(string filePath)
        {
            IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
            pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory";
            pWorspaceName.PathName = filePath;

            IFeatureWorkspace tFeatureWorkspace = (pWorspaceName as IName).Open() as IFeatureWorkspace;

            return tFeatureWorkspace;
        }

        /// <summary>
        /// 获取FileGDB的Workspace
        /// </summary>
        /// <returns></returns>
        public static IFeatureWorkspace GetWorkspaceFromFileGDB(string filePath)
        {
            IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
            pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
            pWorspaceName.PathName = filePath;

            IFeatureWorkspace tFeatureWorkspace = (pWorspaceName as IName).Open() as IFeatureWorkspace;

            return tFeatureWorkspace;
        }

        /// <summary>
        /// 检查SDEWorkspace是否连接，失去连接则重新连接
        /// </summary>
        /// <param name="tWorkspace"></param>
        public static bool CheckSDEWorkspaceConnect(ref IWorkspace tWorkspace)
        {
            IWorkspaceFactoryStatus workspaceFactoryStatus = tWorkspace.WorkspaceFactory as IWorkspaceFactoryStatus;
            //IWorkspaceFactoryStatus只支持SDE数据库
            if (workspaceFactoryStatus == null) return false;

            IEnumWorkspaceStatus enumWorkspaceStatus = workspaceFactoryStatus.WorkspaceStatus;

            IWorkspaceStatus workspaceStatus = null;
            while ((workspaceStatus = enumWorkspaceStatus.Next()) != null)
            {
                if (tWorkspace.Equals(workspaceStatus.Workspace))
                {
                    break;
                }
            }
            //找不到数据库的情况
            if (workspaceStatus == null)
            {
                return false;
            }

            if (workspaceStatus.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSDown)
            {
                //ping两次试试能不能连
                for (int i = 0; i < 2; i++)
                {
                    IWorkspaceStatus pingedStatus = workspaceFactoryStatus.PingWorkspaceStatus(tWorkspace);
                    if (pingedStatus.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSAvailable)
                    {
                        //重新连接数据库
                        tWorkspace = workspaceFactoryStatus.OpenAvailableWorkspace(pingedStatus);
                        return true;
                    }
                    Thread.Sleep(1000);
                }
            }

            return false;
        }

        /// <summary>
        /// 获取Workspace的所有FeatureClass（包括Dataset里面的）
        /// </summary>
        /// <param name="featureWorkspace"></param>
        /// <returns></returns>
        public static List<IFeatureClass> GetAllFeatureClassInWorkspace(IFeatureWorkspace featureWorkspace)
        {
            IWorkspace workspace = featureWorkspace as IWorkspace;
            List<IFeatureClass> featureClassResult = new List<IFeatureClass>();

            //首先遍历数据集
            IEnumDataset tEnumDataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            tEnumDataset.Reset();
            IFeatureDataset featureDataset = tEnumDataset.Next() as IFeatureDataset;
            while (featureDataset != null)
            {
                IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
                IEnumFeatureClass tEnumFeatureClassTarget = featureClassContainer.Classes;
                tEnumFeatureClassTarget.Reset();
                IFeatureClass featureClass = tEnumFeatureClassTarget.Next();
                //遍历数据集里的要素类
                while (featureClass != null)
                {
                    featureClassResult.Add(featureClass);

                    featureClass = tEnumFeatureClassTarget.Next();
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClassTarget);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureDataset);

                featureDataset = tEnumDataset.Next() as IFeatureDataset;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDataset);

            //然后遍历在数据库根目录的要素类
            tEnumDataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
            tEnumDataset.Reset();
            IFeatureClass featureClass2 = tEnumDataset.Next() as IFeatureClass;
            while (featureClass2 != null)
            {
                featureClassResult.Add(featureClass2);

                featureClass2 = tEnumDataset.Next() as IFeatureClass;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDataset);

            return featureClassResult;
        }

        /// <summary>
        /// 获取Workspace的所有FeatureClass和Table（包括Dataset里面的）
        /// </summary>
        /// <param name="featureWorkspace"></param>
        /// <returns></returns>
        public static List<IDataset> GetAllFeatureClassAnTableInWorkspace(IFeatureWorkspace featureWorkspace)
        {
            IWorkspace workspace = featureWorkspace as IWorkspace;
            List<IDataset> featureClassResult = new List<IDataset>();

            //首先遍历数据集
            IEnumDataset tEnumDataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            tEnumDataset.Reset();
            IFeatureDataset featureDataset = tEnumDataset.Next() as IFeatureDataset;
            while (featureDataset != null)
            {
                IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
                IEnumFeatureClass tEnumFeatureClassTarget = featureClassContainer.Classes;
                tEnumFeatureClassTarget.Reset();
                IFeatureClass featureClass = tEnumFeatureClassTarget.Next();
                //遍历数据集里的要素类
                while (featureClass != null)
                {
                    featureClassResult.Add(featureClass as IDataset);

                    featureClass = tEnumFeatureClassTarget.Next();
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClassTarget);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureDataset);

                featureDataset = tEnumDataset.Next() as IFeatureDataset;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDataset);

            //然后遍历在数据库根目录的要素类
            tEnumDataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
            tEnumDataset.Reset();
            IFeatureClass featureClass2 = tEnumDataset.Next() as IFeatureClass;
            while (featureClass2 != null)
            {
                featureClassResult.Add(featureClass2 as IDataset);

                featureClass2 = tEnumDataset.Next() as IFeatureClass;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDataset);

            tEnumDataset = workspace.get_Datasets(esriDatasetType.esriDTTable);
            tEnumDataset.Reset();
            ITable table = tEnumDataset.Next() as ITable;
            while (table != null)
            {
                featureClassResult.Add(table as IDataset);

                table = tEnumDataset.Next() as ITable;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDataset);

            return featureClassResult;
        }
    }
}
