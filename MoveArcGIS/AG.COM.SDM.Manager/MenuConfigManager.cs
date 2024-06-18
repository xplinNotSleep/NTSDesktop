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
    /// �˵�������Ϣ������
    /// </summary>
    public class MenuConfigManager
    {
        /// <summary>
        /// ��ָ�����ļ��ж�ȡ���ڵ���Ϣ
        /// </summary>
        /// <param name="fileName">�ļ�·��</param>
        /// <param name="treeMenu">TreeView����</param>
        public static void ReadConfigFile(string fileName, TreeView treeMenu)
        {
            //ʵ����DataSet����
            DataSet ds = new DataSet();
            //��ָ�����ļ��еõ�������ͼ
            ds.ReadXml(fileName);

            DataView dv = ds.Tables[0].DefaultView;

            //ͨ��DataView�������������ȵõ��������ڵ�
            dv.RowFilter = "ParentID='0'";

            for (int i = 0; i < dv.Count; i++)
            {
                //�������ڵ�
                TreeNode pTreeNode = new TreeNode();
                //���ڵ㸳ֵ
                pTreeNode.Text = dv[i]["Caption"].ToString();
                //�õ��ڵ�����Ϣ
                pTreeNode.Tag = GetMenuEntity(dv[i]);//GetPlugNodeInfo(dv[i]);
                //���ýڵ�ѡ��ͼƬ

                if (dv[i]["EnumNodeType"].ToString() == EnumNodeType.Toolbar.ToString())
                {
                    pTreeNode.ImageIndex = 1;
                    pTreeNode.SelectedImageIndex = 1;
                }
                //�ݹ鴴���ӽڵ�
                CreatSubTreeNode(ref pTreeNode, dv[i]["ItemID"].ToString(), ds.Tables[0]);
                //��ӽڵ�
                treeMenu.Nodes.Add(pTreeNode);
            }
        }

        /// <summary>
        /// ����������Ϣ�����ݿ�
        /// </summary>
        /// <param name="pDataTable">���ݱ�</param>
        public static void SaveConfigFileToDB(DataTable pDataTable)
        {
            //Ӧ�ó��������ļ�·��
            string strFilePath = CommonConstString.STR_ConfigPath + "\\appconfig.resx";
            //ʵ������Դ�ļ����������
            ResourceHelper tResHelper = new ResourceHelper(strFilePath);

            //ʵ�����������ö���
            IPropertySet tPropertySet = new PropertySetClass();
            tPropertySet.SetProperty("Server", tResHelper.GetString(CommonVariablesKeys.SDEServer));
            tPropertySet.SetProperty("Instance", tResHelper.GetString(CommonVariablesKeys.SDEInstance));
            tPropertySet.SetProperty("DataBase", tResHelper.GetString(CommonVariablesKeys.SDEDatabase));
            tPropertySet.SetProperty("User", tResHelper.GetString(CommonVariablesKeys.SDEUser));
            tPropertySet.SetProperty("Password", tResHelper.GetString(CommonVariablesKeys.SDEPassword));
            tPropertySet.SetProperty("Version", tResHelper.GetString(CommonVariablesKeys.SDEVersion));

            //ʵ����SDEWorkspaceFactory����
            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();
            IFeatureWorkspace tFeatWorkspace = tWorkspaceFactory.Open(tPropertySet, 0) as IFeatureWorkspace;

            //string strMenuTableName = tResHelper.GetString("Sys_MainMenu");
            string strMenuTableName = CommonVariablesKeys.SYSMainMenu;

            //�ж�Ҫ�����Ƿ��Ѿ�����
            bool IsExist = (tFeatWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, strMenuTableName);

            if (IsExist == true)
            {
                //��ȡָ�����Ƶ����Ա�
                ITable tTable = tFeatWorkspace.OpenTable(strMenuTableName);

                IWorkspaceEdit tWorkspaceEdit = tFeatWorkspace as IWorkspaceEdit;
                tWorkspaceEdit.StartEditing(true);
                tWorkspaceEdit.StartEditOperation();

                try
                {
                    //�������ݵ�SDE���Ա�
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
                MessageBox.Show(string.Format("[{0}] �˵����ñ�����!", strMenuTableName));
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
        /// �ݹ鴴���ӽڵ�
        /// </summary>
        /// <param name="preNode">���ڵ�</param>
        /// <param name="itemID">ItemID���</param>
        /// <param name="dataTable">���ݱ�</param>
        private static void CreatSubTreeNode(ref TreeNode pNode, string itemID, DataTable dataTable)
        {
            //��ʼ�����ݱ���ͼ
            DataView dv = new DataView(dataTable);
            //���ù�������
            dv.RowFilter = "ParentID='" + itemID + "'";

            for (int i = 0; i < dv.Count; i++)
            {
                TreeNode pTreeNode = new TreeNode();
                pTreeNode.Text = dv[i]["Caption"].ToString();

                //�õ��ڵ�����Ϣ
                pTreeNode.Tag = GetMenuEntity(dv[i]);//GetPlugNodeInfo(dv[i]);

                //�õ��ڵ�����
                EnumNodeType pNodeType = (EnumNodeType)(Convert.ToInt16(dv[i]["EnumNodeType"]));

                //�������ΪMenuItem������ݹ���� 
                if (pNodeType == EnumNodeType.MenuItem || pNodeType == EnumNodeType.MenuStrip)
                {
                    pTreeNode.ImageIndex = 0;
                    pTreeNode.SelectedImageIndex = 0;
                    //�ݹ鴴���ӽڵ�
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
        /// �������ݵ����Ա�
        /// </summary>
        /// <param name="pDataTable">DataTable</param>
        /// <param name="pTable">SDE���Ա�</param>
        private static void ImportDataToTable(DataTable pDataTable, ITable pTable)
        {
            IDictionary<int, int> tDictFields = new Dictionary<int, int>();

            //ɾ��������
            pTable.DeleteSearchedRows(null);

            //����Ϊ�����α�
            ICursor tCursor = pTable.Insert(true);
            IRowBuffer tRowBuffer = pTable.CreateRowBuffer();       
             
            //���ö�Ӧ����
            for (int i = 0; i < pDataTable.Columns.Count; i++)
            {
                int fieldindex = tRowBuffer.Fields.FindField(pDataTable.Columns[i].ColumnName);
                tDictFields.Add(i, fieldindex);
            }

            int count=pDataTable.Columns.Count;
            int roleFieldIndex = tRowBuffer.Fields.FindField("RoleContains");           

            //����д��������
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

                //������
                tCursor.InsertRow(tRowBuffer);
            }

            //һ����д��
            tCursor.Flush();

            //�ͷ���Դ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor); 
        }
    }
}
