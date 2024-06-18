using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Database;
using AG.COM.SDM.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ϵͳ���ô�����
    /// </summary>
    public partial class FormOptions : DockContent
    {
        /// <summary>
        /// ��ʼ��ʵ������
        /// </summary>
        public FormOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ȷ����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, EventArgs e)
        {
            //���ݿ�����
            DatabaseType dbtype = DatabaseType.SqlServer;

            //������ʽ�ļ�
            int itemCount = this.listStyleFiles.Items.Count;
            string[] strStyles = new string[itemCount];
            StringBuilder sbStyleFile = new StringBuilder();
            for (int i = 0; i < itemCount; i++)
            {
                strStyles[i] = this.listStyleFiles.Items[i].SubItems[1].Text;
                sbStyleFile.Append(strStyles[i] + "|");
            }
            string strStyleFile = sbStyleFile.ToString();

            //Minio����������
            MinioServer minioServer = new MinioServer();
            minioServer.ServerURL= txtMinioURL.Text;
            minioServer.AccessName = txtMinioAccess.Text;
            minioServer.PassWord = txtMinioSecret.Text;
            minioServer.BucketName = txtBucketName.Text;

            //����������Ϣ���ļ�
            this.SaveConfigInfoToFile(dbtype, strStyleFile, minioServer);

            //���³�ʼ�����������ļ���ȡ��Ϣ��CommonVariables��
            CommonVariables.Init(true);  

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            //ϵͳ������ʼ��
            CommonVariables.Init();

            //SDE��������
            ctlSDE.DataBaseType = CommonVariables.SpatialdbConn.Spatial_DBType;
            ctlSDE.Server = CommonVariables.SpatialdbConn.Spatial_Server;
            //ctlSDE.Instance = CommonVariables.SpatialdbConn.Spatial_Instance;
            ctlSDE.Port = CommonVariables.SpatialdbConn.Spatial_Instance;
            ctlSDE.DataBase = CommonVariables.SpatialdbConn.Spatial_DataBase;
            ctlSDE.User = CommonVariables.SpatialdbConn.Spatial_User;
            ctlSDE.Password = CommonVariables.SpatialdbConn.Spatial_Password;
            ctlSDE.Version = CommonVariables.SpatialdbConn.Spatial_Version;

            #region OLE���ݿ�����(����ʶ����)
            //List<OleConnProperty> lstOleConnProperty = CommonVariables.DatabaseConfig.OLE_ConnManager.OleConnColl;
            //if (lstOleConnProperty != null)
            //{
            //    OleConnProperty tOleConnProperty;
            //    for (int i = 0; i < lstOleConnProperty.Count; i++)
            //    {
            //        tOleConnProperty = lstOleConnProperty[i];
            //        ListViewItem tItem = OleConnToItem(tOleConnProperty);
            //        lvwOleDbSet.Items.Add(tItem);
            //    }
            //}
            #endregion

            //��ʼ��Minio����������Ϣ
            txtMinioURL.Text= CommonVariables.MinioConn.MinioServerURL;
            txtMinioAccess.Text = CommonVariables.MinioConn.MinioAccessName;
            txtMinioSecret.Text = CommonVariables.MinioConn.MinioPassWord;
            txtBucketName.Text = CommonVariables.MinioConn.MinioServerBucket;

            //��ʼ����ʽ�����ļ�
            string[] styleFiels = CommonVariables.StyleFiles.Split('|', ',');
            for (int i = 0; i < styleFiels.Length-1; i++)
            {
                if (File.Exists(styleFiels[i]))
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = i.ToString();
                    listItem.SubItems.Add(styleFiels[i]);
                    this.listStyleFiles.Items.Add(listItem);
                }
            }
        }

        //����ʽ�ļ�
        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                openDlg.Multiselect = true;
                openDlg.Filter = "������ʽ�ļ�(*.ServerStyle)|*.ServerStyle";
                openDlg.Title = "ѡ����ʽ�ļ�";
                openDlg.InitialDirectory = CommonConstString.STR_StylePath;

                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    string[] strStyleFiles = openDlg.FileNames;
                    int itemId = this.listStyleFiles.Items.Count - 1;
                    int i = 1;

                    foreach (string stylefile in strStyleFiles)
                    {
                        //�Ѿ����ڵ��ļ������
                        if (!listStyleFiles.Items.Cast<ListViewItem>().Any(r => r.SubItems[1].Text == stylefile))
                        {
                            ListViewItem listitem = new ListViewItem();
                            listitem.Text = Convert.ToString(itemId + i);
                            listitem.SubItems.Add(stylefile);

                            this.listStyleFiles.Items.Add(listitem);
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listStyleFiles.SelectedItems.Count > 0)
            {
                int selIndex = this.listStyleFiles.SelectedItems[0].Index;
                this.listStyleFiles.SelectedItems[0].Remove();

                for (int i = selIndex; i < this.listStyleFiles.Items.Count; i++)
                {
                    ListViewItem listItem = this.listStyleFiles.Items[i];
                    listItem.Text = i.ToString();
                }
            }
        }

        //ȡ��
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //����OLE����
        private void btnAddOle_Click(object sender, EventArgs e)
        {
            FormOleDbOptions frmOleDbOptions = new FormOleDbOptions();
            if (frmOleDbOptions.ShowDialog() == DialogResult.OK)
            {
                //OleConnProperty tOleConnProperty = frmOleDbOptions.OLE_ConnProperty;
                //ListViewItem tItem = OleConnToItem(tOleConnProperty);
                //lvwOleDbSet.Items.Add(tItem);
            }
        }

        //�༭OLE����
        private void btnEditOle_Click(object sender, EventArgs e)
        {
            ListViewItem currentItem = lvwOleDbSet.FocusedItem;
            if (currentItem ==null ) 
            {
                MessageBox .Show ("û��ѡ���","��ʾ",MessageBoxButtons .OK ,MessageBoxIcon .Information );
                return ;
            }

            #region ���ݱ�ʶ����ole����
            //OleConnProperty tOleConnProperty = new OleConnProperty();

            //tOleConnProperty.OLE_Name = currentItem.Text;
            //tOleConnProperty.DataBaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), currentItem.SubItems[1].Text,false);
            //tOleConnProperty.OLE_Server = currentItem.SubItems[2].Text;
            //tOleConnProperty.OLE_Port = currentItem.SubItems[3].Text;
            //tOleConnProperty.OLE_DataBase = currentItem.SubItems[4].Text;
            //tOleConnProperty.OLE_User = currentItem.SubItems[5].Text;
            //tOleConnProperty.OLE_Password = currentItem.SubItems[6].Tag.ToString();

            //FormOleDbOptions frmOleDbOptions = new FormOleDbOptions(tOleConnProperty);

            //if (frmOleDbOptions.ShowDialog() == DialogResult.OK)
            //{
            //    tOleConnProperty = frmOleDbOptions.OLE_ConnProperty;
            //    int index = currentItem.Index;
            //    lvwOleDbSet.Items.Insert(index, OleConnToItem(tOleConnProperty));
            //    currentItem.Remove();
            //}
            #endregion

        }

        //ɾ��OLE����
        private void btnDelOle_Click(object sender, EventArgs e)
        {
            ListViewItem currentItem = lvwOleDbSet.FocusedItem;
            if (currentItem == null)
            {
                MessageBox.Show("û��ѡ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("ȷ��Ҫɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                currentItem.Remove();
            }
        }

        #region �Զ���˽�к���
        /// <summary>
        /// ����������Ϣ���ļ�
        /// </summary>
        /// <param name="dbtype">���ݿ�����</param>
        /// <param name="stylefile">��ʽ�ļ�</param>
        private void SaveConfigInfoToFile(DatabaseType dbtype, string stylefile, MinioServer minioServer)
        {
            AG.COM.SDM.Utility.ResourceHelper helper = new ResourceHelper
                (CommonVariables.ConfigFile, CommonConstString.STR_TempPath);

            //������ʽ�ļ�
            helper.SetString(CommonVariablesKeys.StyleFiles, stylefile);

            //SDE��������
            helper.SetString(CommonVariablesKeys.SpatialDbType, ctlSDE.DataBaseType.ToString());
            helper.SetString(CommonVariablesKeys.SpatialServer, ctlSDE.Server);
            helper.SetString(CommonVariablesKeys.SpatialPort, ctlSDE.Port);
            helper.SetString(CommonVariablesKeys.SpatialDatabase, ctlSDE.DataBase);
            helper.SetString(CommonVariablesKeys.SpatialUser, ctlSDE.User);
            helper.SetString(CommonVariablesKeys.SpatialPassword, ctlSDE.Password);

            #region OLE���ݿ�����
            //CommonVariables.DatabaseConfig.OLE_ConnManager.OleConnColl.Clear();  //Ole�������

            //List<OleConnProperty> lstOleDbConfig = CommonVariables.DatabaseConfig.OLE_ConnManager.OleConnColl;
            //for (int i = 0; i < lvwOleDbSet.Items.Count; i++)
            //{
            //    OleConnProperty tOleConfig = new OleConnProperty();
            //    ListViewItem lstViewItem = lvwOleDbSet.Items[i];
            //    tOleConfig.OLE_Name = lstViewItem.Text;
            //    tOleConfig.DataBaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), lstViewItem.SubItems[1].Text, false);
            //    tOleConfig.OLE_Server = lstViewItem.SubItems[2].Text;
            //    tOleConfig.OLE_Port = lstViewItem.SubItems[3].Text;
            //    tOleConfig.OLE_DataBase = lstViewItem.SubItems[4].Text;
            //    tOleConfig.OLE_User = lstViewItem.SubItems[5].Text;
            //    tOleConfig.OLE_Password = lstViewItem.SubItems[6].Tag.ToString();

            //    lstOleDbConfig.Add(tOleConfig);
            //}

            //helper.SetObject(CommonVariablesKeys.OLEConnColl, lstOleDbConfig);
            #endregion

            //minio����������
            helper.SetString(CommonVariablesKeys.MinioServerURL, minioServer.ServerURL);
            helper.SetString(CommonVariablesKeys.MinioServerAccess, minioServer.AccessName);
            helper.SetString(CommonVariablesKeys.MinioServerPassWord, minioServer.PassWord);
            helper.SetString(CommonVariablesKeys.MinioServerBucket, minioServer.BucketName);

            //�������Դ�ļ����޸�
            helper.Save();
        }

        /// <summary>
        /// Ole��������תΪListViewItem��
        /// </summary>
        /// <param name="pOleConnProperty">Ole��������</param>
        /// <returns>ListViewItemʵ��</returns>
        //private ListViewItem OleConnToItem(OleConnProperty pOleConnProperty)
        //{
        //    ListViewItem tItem = new ListViewItem();
        //    OleConnProperty tOleConnProperty = pOleConnProperty;
        //    tItem.Text = tOleConnProperty.OLE_Name;
        //    tItem.SubItems.Add(tOleConnProperty.DataBaseType.ToString());
        //    tItem.SubItems.Add(tOleConnProperty.OLE_Server);
        //    tItem.SubItems.Add(tOleConnProperty.OLE_Port);
        //    tItem.SubItems.Add(tOleConnProperty.OLE_DataBase);
        //    tItem.SubItems.Add(tOleConnProperty.OLE_User);
        //    tItem.SubItems.Add("******");

        //    //���벻�ɼ�
        //    ListViewItem.ListViewSubItem tSubItem = tItem.SubItems[6];
        //    StringBuilder strPassword = new StringBuilder();
        //    for (int i = 0; i < tOleConnProperty.OLE_Password.Length; i++)
        //    {
        //        strPassword.Append("*");
        //    }
        //    tSubItem.Text = strPassword.ToString();
        //    tSubItem.Tag = tOleConnProperty.OLE_Password;
        //    return tItem;
        //}

        #endregion

        private void ctlSpatial_Load(object sender, EventArgs e)
        {

        }
    }
}