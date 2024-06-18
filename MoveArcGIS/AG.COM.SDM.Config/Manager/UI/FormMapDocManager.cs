using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 地图文档管理
    /// </summary>
    public partial class FormMapDocManager : Form
    { 
        private IFeatureWorkspace m_FeatureWorkspace;   //要素工作空间
        private ITable m_MapDocTable;                   //应用工程系统表
        private IList<RoleInfo> m_ListRoleInfo;         //角色信息列表
        private bool m_IsDirty = false;                 //判断是否修改过

        public FormMapDocManager()
        {
            InitializeComponent();
        }

        //添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strAppID = "0001";
            if (this.listAppProject.Items.Count > 0)
            {
                MapDocInfo tempMapDoc = this.listAppProject.Items[this.listAppProject.Items.Count - 1].Tag as MapDocInfo;
                strAppID = Convert.ToString(Convert.ToInt16(tempMapDoc.AppID) + 1).PadLeft(4, '0');
            }

            MapDocInfo tMapDocInfo = new MapDocInfo();
            tMapDocInfo.AppID = strAppID;

            FormMapDocInfo tFrm = new FormMapDocInfo();
            tFrm.ShowInTaskbar = false;
            tFrm.OperateState = EnumOperateState.Add;
            tFrm.MapDocInfo = tMapDocInfo;

            //设置角色信息
            tFrm.SetRolesInfo(this.m_ListRoleInfo);  

            if (tFrm.ShowDialog() == DialogResult.OK)
            {
                //获取地图文档信息
                tMapDocInfo = tFrm.MapDocInfo;

                //实例化ListViewItem项
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = tMapDocInfo.AppID;
                tListViewItem.SubItems.Add(tMapDocInfo.AppName);
                tListViewItem.SubItems.Add(tMapDocInfo.Description);
                tListViewItem.Tag = tMapDocInfo;

                this.listAppProject.Items.Add(tListViewItem);

                this.m_IsDirty = true;
            }
        }

        private void listAppProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置对象的可用状态
            SetButtonEnabled();
        }

        private void FormMapDocManager_Load(object sender, EventArgs e)
        {
            try
            {
                //得到当前SDE工作空间
                m_FeatureWorkspace = CommonVariables.DatabaseConfig.Workspace as IFeatureWorkspace;

                #region 初始化用户信息
                //获取用户表
                this.m_MapDocTable = m_FeatureWorkspace.OpenTable(Resource.MapDocTable);

                ICursor tCursor = m_MapDocTable.Search(null, false);
                for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                {
                    //转换IRow到应用工程信息
                    MapDocInfo tMapDocInfo = ConvertRowToMapDocInfo(tRow);
                    tMapDocInfo.DataBrowserName = "数据库(二进制数据)";

                    ListViewItem tListViewItem = new ListViewItem();
                    tListViewItem.Text = tMapDocInfo.AppID;
                    tListViewItem.SubItems.Add(tMapDocInfo.AppName);
                    tListViewItem.SubItems.Add(tMapDocInfo.Description);
                    tListViewItem.Tag = tMapDocInfo;

                    this.listAppProject.Items.Add(tListViewItem);
                }

                //释放游标资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
                #endregion

                //获取角色信息
                this.m_ListRoleInfo = GetListRoleInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //修改
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                //获取选择项
                ListViewItem tListViewItem = this.listAppProject.SelectedItems[0];
                MapDocInfo tMapDocInfo = tListViewItem.Tag as MapDocInfo;

                //实例化用户信息窗体类
                FormMapDocInfo tFrm = new FormMapDocInfo();
                tFrm.ShowInTaskbar = false;
                tFrm.OperateState = EnumOperateState.Modify;

                //设置角色信息
                tFrm.SetRolesInfo(this.m_ListRoleInfo);

                tFrm.MapDocInfo = tMapDocInfo;

                if (tFrm.ShowDialog() == DialogResult.OK)
                {
                    tListViewItem.Text = tMapDocInfo.AppID;
                    tListViewItem.SubItems[1].Text = tMapDocInfo.AppName;
                    tListViewItem.SubItems[2].Text = tMapDocInfo.Description;

                    this.m_IsDirty = true;
                }
            }
        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                //移除选择项
                this.listAppProject.SelectedItems[0].Remove();

                this.m_IsDirty = true;
            }
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                //获取选择项
                ListViewItem tListViewItem = this.listAppProject.SelectedItems[0];
                MapDocInfo tMapDocInfo = tListViewItem.Tag as MapDocInfo;

                //实例化用户信息窗体类
                FormMapDocInfo tFrm = new FormMapDocInfo();
                tFrm.ShowInTaskbar = false;
                tFrm.OperateState = EnumOperateState.Query;

                //设置角色信息
                tFrm.SetRolesInfo(this.m_ListRoleInfo);

                tFrm.MapDocInfo = tMapDocInfo;

                if (tFrm.ShowDialog() == DialogResult.OK)
                {
                    tMapDocInfo = tFrm.MapDocInfo;
                    tListViewItem.Text = tMapDocInfo.AppID;
                    tListViewItem.SubItems[1].Text = tMapDocInfo.AppName;
                    tListViewItem.SubItems[2].Text = tMapDocInfo.Description;
                }
            }
        }

        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.m_IsDirty == true && this.m_FeatureWorkspace != null && this.m_MapDocTable != null)
            {
                IWorkspaceEdit tWorkspaceEdit = this.m_FeatureWorkspace as IWorkspaceEdit;
                tWorkspaceEdit.StartEditing(true);
                tWorkspaceEdit.StartEditOperation();

                try
                {
                    if (this.m_MapDocTable.RowCount(null) > 0)
                    {
                        this.m_MapDocTable.DeleteSearchedRows(null);
                    }

                    ICursor tCursor = this.m_MapDocTable.Insert(true);
                    IRowBuffer tRowBuffer = this.m_MapDocTable.CreateRowBuffer();

                    for (int i = 0; i < this.listAppProject.Items.Count; i++)
                    {
                        MapDocInfo tMapDocInfo = this.listAppProject.Items[i].Tag as MapDocInfo;

                        //转换用户信息到IRow
                        ConvertMapDocInfoToRow(tMapDocInfo, tRowBuffer);

                        //插入行
                        tCursor.InsertRow(tRowBuffer);
                    }

                    //一次性写入
                    tCursor.Flush();

                    MessageBox.Show("数据已成功保存到数据库","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                catch
                {
                    tWorkspaceEdit.AbortEditOperation();
                }
                finally
                {
                    tWorkspaceEdit.StopEditOperation();
                    tWorkspaceEdit.StopEditing(true);
                }
            }
        }

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置对象的可用状态
        /// </summary>
        private void SetButtonEnabled()
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                this.btnModify.Enabled = true;
                this.btnQuery.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnModify.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        /// <summary>
        /// 转换IRow到应用工程信息
        /// </summary>
        /// <param name="pRow">IRow对象</param>
        /// <returns>返回角色信息</returns>
        private MapDocInfo ConvertRowToMapDocInfo(IRow pRow)
        {
            if (pRow == null) return null;

            //实例化角色信息类
            MapDocInfo tMapDocInfo = new MapDocInfo();

            //应用工程编号
            int fieldIndex = pRow.Fields.FindField("AppID");
            if (fieldIndex > -1)
                tMapDocInfo.AppID = Convert.ToString(pRow.get_Value(fieldIndex));

            //应用工程名称
            fieldIndex = pRow.Fields.FindField("AppName");
            if (fieldIndex > -1)
                tMapDocInfo.AppName = Convert.ToString(pRow.get_Value(fieldIndex));

            //描述信息
            fieldIndex = pRow.Fields.FindField("Description");
            if (fieldIndex > -1)
                tMapDocInfo.Description = Convert.ToString(pRow.get_Value(fieldIndex));

            //是否激活 
            fieldIndex = pRow.Fields.FindField("IsActive");
            if (fieldIndex > -1)
                tMapDocInfo.IsActive = Convert.ToBoolean(pRow.get_Value(fieldIndex));

            //所属角色 
            fieldIndex = pRow.Fields.FindField("Role");
            if (fieldIndex > -1)
            {
                string[] strRoles = Convert.ToString(pRow.get_Value(fieldIndex)).Split('$');

                IList<RoleInfo> tListRoleInfo = new List<RoleInfo>();

                for (int i = 0; i < strRoles.Length - 1; i++)
                {
                    RoleInfo tRoleInfo = new RoleInfo();
                    tRoleInfo.RoleID = strRoles[i];
                    tListRoleInfo.Add(tRoleInfo);
                }

                tMapDocInfo.ListRoleInfo = tListRoleInfo;
            }

            return tMapDocInfo;
        }

        /// <summary>
        /// 转换应用工程信息到IRow
        /// </summary>
        /// <param name="pMapDocInfo">应用工程信息</param>
        /// <param name="pRowBuffer">行对象</param>
        private void ConvertMapDocInfoToRow(MapDocInfo pMapDocInfo, IRowBuffer pRowBuffer)
        {
            //应用工程ID
            int fieldIndex = pRowBuffer.Fields.FindField("AppID");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.AppID);

            //应用工程名称
            fieldIndex = pRowBuffer.Fields.FindField("AppName");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.AppName);

            //应用工程数据
            fieldIndex = pRowBuffer.Fields.FindField("AppData");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.AppData);

            //描述信息
            fieldIndex = pRowBuffer.Fields.FindField("Description");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.Description);

            //是否激活 
            fieldIndex = pRowBuffer.Fields.FindField("IsActive");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, Convert.ToInt16(pMapDocInfo.IsActive));

            //所属角色 
            fieldIndex = pRowBuffer.Fields.FindField("Role");
            if (fieldIndex > -1)
            {
                IList<RoleInfo> tListRoleInfo = pMapDocInfo.ListRoleInfo;

                StringBuilder tStrBuilder = new StringBuilder();
                for (int i = 0; i < tListRoleInfo.Count; i++)
                {
                    RoleInfo tRoleInfo = tListRoleInfo[i];
                    tStrBuilder.Append(tRoleInfo.RoleID + "$");
                }

                pRowBuffer.set_Value(fieldIndex, tStrBuilder.ToString());
            }
        }

        /// <summary>
        /// 获取所有角色名称
        /// </summary>
        /// <returns>返回角色名称字符组</returns>
        private IList<RoleInfo> GetListRoleInfo()
        {
            ITable tRoleTable = this.m_FeatureWorkspace.OpenTable(Resource.RoleTable);
            if (tRoleTable.RowCount(null) == 0) return null;

            IList<RoleInfo> tListRoleInfo = new List<RoleInfo>();

            ICursor tCursor = tRoleTable.Search(null, false);

            for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
            {
                RoleInfo tRoleInfo = ConvertRowToRoleInfo(tRow);
                tListRoleInfo.Add(tRoleInfo);
            }

            //释放游标对象
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);

            return tListRoleInfo;
        }

        /// <summary>
        /// 转换IRow到角色信息
        /// </summary>
        /// <param name="pRow">IRow对象</param>
        /// <returns>返回角色信息</returns>
        private RoleInfo ConvertRowToRoleInfo(IRow pRow)
        {
            if (pRow == null) return null;

            //实例化角色信息类
            RoleInfo tRoleInfo = new RoleInfo();

            //角色编号
            int fieldIndex = pRow.Fields.FindField("RoleID");
            if (fieldIndex > -1)
                tRoleInfo.RoleID = Convert.ToString(pRow.get_Value(fieldIndex));

            //角色名称
            fieldIndex = pRow.Fields.FindField("RoleName");
            if (fieldIndex > -1)
                tRoleInfo.RoleName = Convert.ToString(pRow.get_Value(fieldIndex));

            //描述信息
            fieldIndex = pRow.Fields.FindField("Description");
            if (fieldIndex > -1)
                tRoleInfo.Description = Convert.ToString(pRow.get_Value(fieldIndex));

            return tRoleInfo;
        }
    }
}