using AG.COM.SDM.Model;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace AG.COM.SDM.Manager
{
    /// <summary>
    /// 菜单配置信息管理类
    /// </summary>
    public class MenuConfigManager
    {
        /// <summary>
        /// 从指定的文件中读取树节点信息
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="treeMenu">TreeView对象</param>
        public static void ReadConfigFile(string fileName, TreeView treeMenu)
        {
            //实例化DataSet对象
            DataSet ds = new DataSet();
            //从指定的文件中得到数据视图
            ds.ReadXml(fileName);

            DataView dv = ds.Tables[0].DefaultView;

            //通过DataView来过滤数据首先得到最顶层的树节点
            dv.RowFilter = "ParentID='0'";

            for (int i = 0; i < dv.Count; i++)
            {
                //创建树节点
                TreeNode pTreeNode = new TreeNode();
                //给节点赋值
                pTreeNode.Text = dv[i]["Caption"].ToString();
                //得到节点插件信息
                pTreeNode.Tag = GetMenuEntity(dv[i]);//GetPlugNodeInfo(dv[i]);
                //设置节点选择图片

                if (dv[i]["EnumNodeType"].ToString() == EnumNodeType.Toolbar.ToString())
                {
                    pTreeNode.ImageIndex = 1;
                    pTreeNode.SelectedImageIndex = 1;
                }
                //递归创建子节点
                CreatSubTreeNode(ref pTreeNode, dv[i]["ItemID"].ToString(), ds.Tables[0]);
                //添加节点
                treeMenu.Nodes.Add(pTreeNode);
            }
        }

        /// <summary>
        /// 保存配置信息到数据库
        /// </summary>
        /// <param name="pDataTable">数据表</param>
        public static void SaveConfigFileToDB(DataTable pDataTable)
        {
            //应用程序配置文件路径
            string strFilePath = CommonConstString.STR_ConfigPath + "\\appconfig.resx";
            //实例化资源文件帮助类对象
            ResourceHelper tResHelper = new ResourceHelper(strFilePath);

            //实例化属性设置对象
            IPropertySet tPropertySet = new PropertySetClass();
            tPropertySet.SetProperty("Server", tResHelper.GetString(CommonVariablesKeys.SDEServer));
            tPropertySet.SetProperty("Instance", tResHelper.GetString(CommonVariablesKeys.SDEInstance));
            tPropertySet.SetProperty("DataBase", tResHelper.GetString(CommonVariablesKeys.SDEDatabase));
            tPropertySet.SetProperty("User", tResHelper.GetString(CommonVariablesKeys.SDEUser));
            tPropertySet.SetProperty("Password", tResHelper.GetString(CommonVariablesKeys.SDEPassword));
            tPropertySet.SetProperty("Version", tResHelper.GetString(CommonVariablesKeys.SDEVersion));

            //实例化SDEWorkspaceFactory对象
            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
            IFeatureWorkspace tFeatWorkspace = tWorkspaceFactory.Open(tPropertySet, 0) as IFeatureWorkspace;

            //string strMenuTableName = tResHelper.GetString("Sys_MainMenu");
            string strMenuTableName = CommonVariablesKeys.SYSMainMenu;

            //判断要素类是否已经存在
            bool IsExist = (tFeatWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, strMenuTableName);

            if (IsExist == true)
            {
                //获取指定名称的属性表
                ITable tTable = tFeatWorkspace.OpenTable(strMenuTableName);

                IWorkspaceEdit tWorkspaceEdit = tFeatWorkspace as IWorkspaceEdit;
                tWorkspaceEdit.StartEditing(true);
                tWorkspaceEdit.StartEditOperation();

                try
                {
                    //导入数据到SDE属性表
                    ImportDataToTable(pDataTable, tTable);
                }
                catch (Exception ex)
                {
                    tWorkspaceEdit.AbortEditOperation();
                }
                finally
                {
                    tWorkspaceEdit.StopEditOperation();
                    tWorkspaceEdit.StopEditing(true);
                }
            }
            else
                MessageBox.Show(string.Format("[{0}] 菜单配置表不存在!", strMenuTableName));
        }

        private static AGSDM_MENU GetMenuEntity(DataRowView datarow)
        {
            AGSDM_MENU tMenu = new AGSDM_MENU();
            tMenu.ID = 0;
            tMenu.PARENT_MENU_ID = datarow["ParentID"].ToString();
            tMenu.MENU_CODE = datarow["ItemID"].ToString();
            tMenu.MENU_NAME = datarow["Caption"].ToString();
            tMenu.ASSEMBLY_NAME = datarow["PlugAssembly"].ToString();
            tMenu.TYPE_NAME = datarow["PlugType"].ToString();
            tMenu.SHORTCUT = datarow["ShortCut"].ToString();
            tMenu.ISBEGINGROUP =datarow["IsBeginGroup"].ToString();
            tMenu.MENU_TYPE = Convert.ToDecimal(datarow["EnumNodeType"].ToString());
            tMenu.MENU_LEVEL = Convert.ToDecimal(datarow["LocationLevel"]);
            return tMenu;
        }

        /// <summary>
        /// 递归创建子节点
        /// </summary>
        /// <param name="preNode">树节点</param>
        /// <param name="itemID">ItemID编号</param>
        /// <param name="dataTable">数据表</param>
        private static void CreatSubTreeNode(ref TreeNode pNode, string itemID, DataTable dataTable)
        {
            //初始化数据表视图
            DataView dv = new DataView(dataTable);
            //设置过滤条件
            dv.RowFilter = "ParentID='" + itemID + "'";

            for (int i = 0; i < dv.Count; i++)
            {
                TreeNode pTreeNode = new TreeNode();
                pTreeNode.Text = dv[i]["Caption"].ToString();

                //得到节点插件信息
                pTreeNode.Tag = GetMenuEntity(dv[i]);//GetPlugNodeInfo(dv[i]);

                //得到节点类型
                EnumNodeType pNodeType = (EnumNodeType)(Convert.ToInt16(dv[i]["EnumNodeType"]));

                //如果类型为MenuItem则继续递归加载 
                if (pNodeType == EnumNodeType.MenuItem || pNodeType == EnumNodeType.MenuStrip)
                {
                    pTreeNode.ImageIndex = 0;
                    pTreeNode.SelectedImageIndex = 0;
                    //递归创建子节点
                    CreatSubTreeNode(ref pTreeNode, dv[i]["ItemID"].ToString(), dataTable);
                }
                else if (pNodeType == EnumNodeType.Toolbar)
                {
                    pTreeNode.ImageIndex = 1;
                    pTreeNode.SelectedImageIndex = 1;
                }
                else if (pNodeType == EnumNodeType.ComboBox)
                {
                    pTreeNode.ImageIndex = 3;
                    pTreeNode.SelectedImageIndex = 3;
                }
                else if (pNodeType == EnumNodeType.CustomControl)
                {
                    pTreeNode.ImageIndex = 3;
                    pTreeNode.SelectedImageIndex = 3;
                }
                else
                {
                    pTreeNode.ImageIndex = 2;
                    pTreeNode.SelectedImageIndex = 2;
                }

                pNode.Nodes.Add(pTreeNode);
            }
        }

        /// <summary>
        /// 导入数据到属性表
        /// </summary>
        /// <param name="pDataTable">DataTable</param>
        /// <param name="pTable">SDE属性表</param>
        private static void ImportDataToTable(DataTable pDataTable, ITable pTable)
        {
            IDictionary<int, int> tDictFields = new Dictionary<int, int>();

            //删除所有行
            pTable.DeleteSearchedRows(null);

            //设置为插入游标
            ICursor tCursor = pTable.Insert(true);
            IRowBuffer tRowBuffer = pTable.CreateRowBuffer();       
             
            //设置对应规则
            for (int i = 0; i < pDataTable.Columns.Count; i++)
            {
                int fieldindex = tRowBuffer.Fields.FindField(pDataTable.Columns[i].ColumnName);
                tDictFields.Add(i, fieldindex);
            }

            int count=pDataTable.Columns.Count;
            int roleFieldIndex = tRowBuffer.Fields.FindField("RoleContains");           

            //遍历写入所有行
            for (int j = 0; j < pDataTable.Rows.Count; j++)
            {
                for (int k = 0; k < count; k++)
                {
                    if (tDictFields[k] > -1)
                    {
                        tRowBuffer.set_Value(tDictFields[k], pDataTable.Rows[j][k]);
                    }
                }

                if (roleFieldIndex > -1)
                {
                    tRowBuffer.set_Value(roleFieldIndex, "00$");
                }

                //插入行
                tCursor.InsertRow(tRowBuffer);
            }

            //一次性写入
            tCursor.Flush();

            //释放资源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor); 
        }
    }
}
