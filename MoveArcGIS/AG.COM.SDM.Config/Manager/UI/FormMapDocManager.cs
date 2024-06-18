using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ��ͼ�ĵ�����
    /// </summary>
    public partial class FormMapDocManager : Form
    { 
        private IFeatureWorkspace m_FeatureWorkspace;   //Ҫ�ع����ռ�
        private ITable m_MapDocTable;                   //Ӧ�ù���ϵͳ��
        private IList<RoleInfo> m_ListRoleInfo;         //��ɫ��Ϣ�б�
        private bool m_IsDirty = false;                 //�ж��Ƿ��޸Ĺ�

        public FormMapDocManager()
        {
            InitializeComponent();
        }

        //���
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

            //���ý�ɫ��Ϣ
            tFrm.SetRolesInfo(this.m_ListRoleInfo);  

            if (tFrm.ShowDialog() == DialogResult.OK)
            {
                //��ȡ��ͼ�ĵ���Ϣ
                tMapDocInfo = tFrm.MapDocInfo;

                //ʵ����ListViewItem��
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
            //���ö���Ŀ���״̬
            SetButtonEnabled();
        }

        private void FormMapDocManager_Load(object sender, EventArgs e)
        {
            try
            {
                //�õ���ǰSDE�����ռ�
                m_FeatureWorkspace = CommonVariables.DatabaseConfig.Workspace as IFeatureWorkspace;

                #region ��ʼ���û���Ϣ
                //��ȡ�û���
                this.m_MapDocTable = m_FeatureWorkspace.OpenTable(Resource.MapDocTable);

                ICursor tCursor = m_MapDocTable.Search(null, false);
                for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                {
                    //ת��IRow��Ӧ�ù�����Ϣ
                    MapDocInfo tMapDocInfo = ConvertRowToMapDocInfo(tRow);
                    tMapDocInfo.DataBrowserName = "���ݿ�(����������)";

                    ListViewItem tListViewItem = new ListViewItem();
                    tListViewItem.Text = tMapDocInfo.AppID;
                    tListViewItem.SubItems.Add(tMapDocInfo.AppName);
                    tListViewItem.SubItems.Add(tMapDocInfo.Description);
                    tListViewItem.Tag = tMapDocInfo;

                    this.listAppProject.Items.Add(tListViewItem);
                }

                //�ͷ��α���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
                #endregion

                //��ȡ��ɫ��Ϣ
                this.m_ListRoleInfo = GetListRoleInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //�޸�
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                //��ȡѡ����
                ListViewItem tListViewItem = this.listAppProject.SelectedItems[0];
                MapDocInfo tMapDocInfo = tListViewItem.Tag as MapDocInfo;

                //ʵ�����û���Ϣ������
                FormMapDocInfo tFrm = new FormMapDocInfo();
                tFrm.ShowInTaskbar = false;
                tFrm.OperateState = EnumOperateState.Modify;

                //���ý�ɫ��Ϣ
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

        //ɾ��
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                //�Ƴ�ѡ����
                this.listAppProject.SelectedItems[0].Remove();

                this.m_IsDirty = true;
            }
        }

        //��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.listAppProject.SelectedItems.Count > 0)
            {
                //��ȡѡ����
                ListViewItem tListViewItem = this.listAppProject.SelectedItems[0];
                MapDocInfo tMapDocInfo = tListViewItem.Tag as MapDocInfo;

                //ʵ�����û���Ϣ������
                FormMapDocInfo tFrm = new FormMapDocInfo();
                tFrm.ShowInTaskbar = false;
                tFrm.OperateState = EnumOperateState.Query;

                //���ý�ɫ��Ϣ
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

        //ȷ��
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

                        //ת���û���Ϣ��IRow
                        ConvertMapDocInfoToRow(tMapDocInfo, tRowBuffer);

                        //������
                        tCursor.InsertRow(tRowBuffer);
                    }

                    //һ����д��
                    tCursor.Flush();

                    MessageBox.Show("�����ѳɹ����浽���ݿ�","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        //ȡ��
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ���ö���Ŀ���״̬
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
        /// ת��IRow��Ӧ�ù�����Ϣ
        /// </summary>
        /// <param name="pRow">IRow����</param>
        /// <returns>���ؽ�ɫ��Ϣ</returns>
        private MapDocInfo ConvertRowToMapDocInfo(IRow pRow)
        {
            if (pRow == null) return null;

            //ʵ������ɫ��Ϣ��
            MapDocInfo tMapDocInfo = new MapDocInfo();

            //Ӧ�ù��̱��
            int fieldIndex = pRow.Fields.FindField("AppID");
            if (fieldIndex > -1)
                tMapDocInfo.AppID = Convert.ToString(pRow.get_Value(fieldIndex));

            //Ӧ�ù�������
            fieldIndex = pRow.Fields.FindField("AppName");
            if (fieldIndex > -1)
                tMapDocInfo.AppName = Convert.ToString(pRow.get_Value(fieldIndex));

            //������Ϣ
            fieldIndex = pRow.Fields.FindField("Description");
            if (fieldIndex > -1)
                tMapDocInfo.Description = Convert.ToString(pRow.get_Value(fieldIndex));

            //�Ƿ񼤻� 
            fieldIndex = pRow.Fields.FindField("IsActive");
            if (fieldIndex > -1)
                tMapDocInfo.IsActive = Convert.ToBoolean(pRow.get_Value(fieldIndex));

            //������ɫ 
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
        /// ת��Ӧ�ù�����Ϣ��IRow
        /// </summary>
        /// <param name="pMapDocInfo">Ӧ�ù�����Ϣ</param>
        /// <param name="pRowBuffer">�ж���</param>
        private void ConvertMapDocInfoToRow(MapDocInfo pMapDocInfo, IRowBuffer pRowBuffer)
        {
            //Ӧ�ù���ID
            int fieldIndex = pRowBuffer.Fields.FindField("AppID");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.AppID);

            //Ӧ�ù�������
            fieldIndex = pRowBuffer.Fields.FindField("AppName");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.AppName);

            //Ӧ�ù�������
            fieldIndex = pRowBuffer.Fields.FindField("AppData");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.AppData);

            //������Ϣ
            fieldIndex = pRowBuffer.Fields.FindField("Description");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, pMapDocInfo.Description);

            //�Ƿ񼤻� 
            fieldIndex = pRowBuffer.Fields.FindField("IsActive");
            if (fieldIndex > -1)
                pRowBuffer.set_Value(fieldIndex, Convert.ToInt16(pMapDocInfo.IsActive));

            //������ɫ 
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
        /// ��ȡ���н�ɫ����
        /// </summary>
        /// <returns>���ؽ�ɫ�����ַ���</returns>
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

            //�ͷ��α����
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);

            return tListRoleInfo;
        }

        /// <summary>
        /// ת��IRow����ɫ��Ϣ
        /// </summary>
        /// <param name="pRow">IRow����</param>
        /// <returns>���ؽ�ɫ��Ϣ</returns>
        private RoleInfo ConvertRowToRoleInfo(IRow pRow)
        {
            if (pRow == null) return null;

            //ʵ������ɫ��Ϣ��
            RoleInfo tRoleInfo = new RoleInfo();

            //��ɫ���
            int fieldIndex = pRow.Fields.FindField("RoleID");
            if (fieldIndex > -1)
                tRoleInfo.RoleID = Convert.ToString(pRow.get_Value(fieldIndex));

            //��ɫ����
            fieldIndex = pRow.Fields.FindField("RoleName");
            if (fieldIndex > -1)
                tRoleInfo.RoleName = Convert.ToString(pRow.get_Value(fieldIndex));

            //������Ϣ
            fieldIndex = pRow.Fields.FindField("Description");
            if (fieldIndex > -1)
                tRoleInfo.Description = Convert.ToString(pRow.get_Value(fieldIndex));

            return tRoleInfo;
        }
    }
}