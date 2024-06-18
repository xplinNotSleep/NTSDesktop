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
    /// 系统设置窗体类
    /// </summary>
    public partial class FormOptions : DockContent
    {
        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public FormOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, EventArgs e)
        {
            //数据库类型
            DatabaseType dbtype = DatabaseType.SqlServer;

            //符号样式文件
            int itemCount = this.listStyleFiles.Items.Count;
            string[] strStyles = new string[itemCount];
            StringBuilder sbStyleFile = new StringBuilder();
            for (int i = 0; i < itemCount; i++)
            {
                strStyles[i] = this.listStyleFiles.Items[i].SubItems[1].Text;
                sbStyleFile.Append(strStyles[i] + "|");
            }
            string strStyleFile = sbStyleFile.ToString();

            //Minio服务器配置
            MinioServer minioServer = new MinioServer();
            minioServer.ServerURL= txtMinioURL.Text;
            minioServer.AccessName = txtMinioAccess.Text;
            minioServer.PassWord = txtMinioSecret.Text;
            minioServer.BucketName = txtBucketName.Text;

            //保存配置信息到文件
            this.SaveConfigInfoToFile(dbtype, strStyleFile, minioServer);

            //重新初始化（从配置文件读取信息到CommonVariables）
            CommonVariables.Init(true);  

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            //系统变量初始化
            CommonVariables.Init();

            //SDE连接设置
            ctlSDE.DataBaseType = CommonVariables.SpatialdbConn.Spatial_DBType;
            ctlSDE.Server = CommonVariables.SpatialdbConn.Spatial_Server;
            //ctlSDE.Instance = CommonVariables.SpatialdbConn.Spatial_Instance;
            ctlSDE.Port = CommonVariables.SpatialdbConn.Spatial_Instance;
            ctlSDE.DataBase = CommonVariables.SpatialdbConn.Spatial_DataBase;
            ctlSDE.User = CommonVariables.SpatialdbConn.Spatial_User;
            ctlSDE.Password = CommonVariables.SpatialdbConn.Spatial_Password;
            ctlSDE.Version = CommonVariables.SpatialdbConn.Spatial_Version;

            #region OLE数据库设置(按标识设置)
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

            //初始化Minio服务配置信息
            txtMinioURL.Text= CommonVariables.MinioConn.MinioServerURL;
            txtMinioAccess.Text = CommonVariables.MinioConn.MinioAccessName;
            txtMinioSecret.Text = CommonVariables.MinioConn.MinioPassWord;
            txtBucketName.Text = CommonVariables.MinioConn.MinioServerBucket;

            //初始化样式符号文件
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

        //打开样式文件
        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                openDlg.Multiselect = true;
                openDlg.Filter = "符号样式文件(*.ServerStyle)|*.ServerStyle";
                openDlg.Title = "选择样式文件";
                openDlg.InitialDirectory = CommonConstString.STR_StylePath;

                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    string[] strStyleFiles = openDlg.FileNames;
                    int itemId = this.listStyleFiles.Items.Count - 1;
                    int i = 1;

                    foreach (string stylefile in strStyleFiles)
                    {
                        //已经存在的文件不添加
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

        //取消
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //增加OLE配置
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

        //编辑OLE配置
        private void btnEditOle_Click(object sender, EventArgs e)
        {
            ListViewItem currentItem = lvwOleDbSet.FocusedItem;
            if (currentItem ==null ) 
            {
                MessageBox .Show ("没有选择项！","提示",MessageBoxButtons .OK ,MessageBoxIcon .Information );
                return ;
            }

            #region 根据标识设置ole属性
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

        //删除OLE配置
        private void btnDelOle_Click(object sender, EventArgs e)
        {
            ListViewItem currentItem = lvwOleDbSet.FocusedItem;
            if (currentItem == null)
            {
                MessageBox.Show("没有选择项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                currentItem.Remove();
            }
        }

        #region 自定义私有函数
        /// <summary>
        /// 保存配置信息到文件
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="stylefile">样式文件</param>
        private void SaveConfigInfoToFile(DatabaseType dbtype, string stylefile, MinioServer minioServer)
        {
            AG.COM.SDM.Utility.ResourceHelper helper = new ResourceHelper
                (CommonVariables.ConfigFile, CommonConstString.STR_TempPath);

            //符号样式文件
            helper.SetString(CommonVariablesKeys.StyleFiles, stylefile);

            //SDE连接设置
            helper.SetString(CommonVariablesKeys.SpatialDbType, ctlSDE.DataBaseType.ToString());
            helper.SetString(CommonVariablesKeys.SpatialServer, ctlSDE.Server);
            helper.SetString(CommonVariablesKeys.SpatialPort, ctlSDE.Port);
            helper.SetString(CommonVariablesKeys.SpatialDatabase, ctlSDE.DataBase);
            helper.SetString(CommonVariablesKeys.SpatialUser, ctlSDE.User);
            helper.SetString(CommonVariablesKeys.SpatialPassword, ctlSDE.Password);

            #region OLE数据库设置
            //CommonVariables.DatabaseConfig.OLE_ConnManager.OleConnColl.Clear();  //Ole配置清除

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

            //minio服务器设置
            helper.SetString(CommonVariablesKeys.MinioServerURL, minioServer.ServerURL);
            helper.SetString(CommonVariablesKeys.MinioServerAccess, minioServer.AccessName);
            helper.SetString(CommonVariablesKeys.MinioServerPassWord, minioServer.PassWord);
            helper.SetString(CommonVariablesKeys.MinioServerBucket, minioServer.BucketName);

            //保存对资源文件的修改
            helper.Save();
        }

        /// <summary>
        /// Ole连接属性转为ListViewItem项
        /// </summary>
        /// <param name="pOleConnProperty">Ole连接属性</param>
        /// <returns>ListViewItem实例</returns>
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

        //    //密码不可见
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