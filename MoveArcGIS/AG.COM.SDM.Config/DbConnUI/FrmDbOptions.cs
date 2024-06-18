using AG.COM.SDM.Database;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Config.DbConnUI
{
    public partial class FrmDbOptions : DockContent
    {
        private bool m_IsRefresh = false;

        public FrmDbOptions(bool isRefresh)
        {
            InitializeComponent();
            m_IsRefresh = isRefresh;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //Minio服务器配置
                MinioServer minioServer = new MinioServer();
                minioServer.ServerURL = txtMinioURL.Text;
                minioServer.AccessName = txtMinioAccess.Text;
                minioServer.PassWord = txtMinioSecret.Text;
                minioServer.BucketName = txtBucketName.Text;

                //保存配置信息到文件
                this.SaveConfigInfoToFile(minioServer);

                //重新初始化（从配置文件读取信息到CommonVariables）
                CommonVariables.Init(true);

                if(m_IsRefresh)
                {
                    CommonVariables.SpatialdbConn.RefreshDatabase();
                }

                AutoCloseMsgBox.Show("保存成功!","提示", 3000);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                AutoCloseMsgBox.Show($"{ex.Message},保存失败!"
                    ,"提示",4000);
            }
            
        }

        private void FrmDbOptions_Load(object sender, EventArgs e)
        {
            InitDbType();

            //系统变量初始化
            CommonVariables.Init();

            InitSpatialInfo();
            InitOleInfo();
            InitMinioInfo();

            
        }

        /// <summary>
        /// 从全局变量读取空间库数据连接参数
        /// </summary>
        private void InitSpatialInfo()
        {
            string strType = CommonVariables.SpatialdbConn.Spatial_DBType.ToString();

            //SDE连接设置
            cmbSpatialDbType.Text = string.IsNullOrEmpty(strType)?
                DatabaseType.Oracle.ToString():strType;
            txtSpatialServer.Text = CommonVariables.SpatialdbConn.Spatial_Server;
            txtSpatialPort.Text = CommonVariables.SpatialdbConn.Spatial_Port.ToString();
            txtSpatialDbName.Text = CommonVariables.SpatialdbConn.Spatial_DataBase;
            txtSpatialUser.Text = CommonVariables.SpatialdbConn.Spatial_User;
            txtSpatialPwd.Text = CommonVariables.SpatialdbConn.Spatial_Password;
        }

        private void InitOleInfo()
        {
            string strType = CommonVariables.OledbConn.DatabaseType.ToString();
            //SDE连接设置
            cmbOleDbType.Text = string.IsNullOrEmpty(strType) ?
                DatabaseType.Oracle.ToString() : strType;
            txtOleServer.Text = CommonVariables.OledbConn.OLE_Server;
            txtOlePort.Text = CommonVariables.OledbConn.OLE_Port.ToString();
            txtOleDbName.Text = CommonVariables.OledbConn.OLE_DataBase;
            txtOleUser.Text = CommonVariables.OledbConn.OLE_User;
            txtOlePwd.Text = CommonVariables.OledbConn.OLE_Password;
        }

        private void InitMinioInfo()
        {
            txtMinioURL.Text = CommonVariables.MinioConn.MinioServerURL;
            txtMinioAccess.Text = CommonVariables.MinioConn.MinioAccessName;
            txtMinioSecret.Text = CommonVariables.MinioConn.MinioPassWord;
            txtBucketName.Text = CommonVariables.MinioConn.MinioServerBucket;
        }

        private void InitDbType()
        {
            foreach(DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                cmbOleDbType.Items.Add(dbType.ToString());
                cmbSpatialDbType.Items.Add(dbType.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存配置信息到文件
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="stylefile">样式文件</param>
        private void SaveConfigInfoToFile(MinioServer minioServer)
        {
            ResourceHelper helper = new ResourceHelper
                (CommonVariables.ConfigFile, CommonConstString.STR_TempPath);

            //空间库连接设置
            helper.SetString(CommonVariablesKeys.SpatialDbType, cmbSpatialDbType.Text);
            helper.SetString(CommonVariablesKeys.SpatialServer, txtSpatialServer.Text);
            helper.SetString(CommonVariablesKeys.SpatialPort, txtSpatialPort.Text);
            helper.SetString(CommonVariablesKeys.SpatialDatabase, txtSpatialDbName.Text);
            helper.SetString(CommonVariablesKeys.SpatialUser, txtSpatialUser.Text);
            helper.SetString(CommonVariablesKeys.SpatialPassword, txtSpatialPwd.Text);

            //属性库连接设置
            helper.SetString(CommonVariablesKeys.OLEDbType, cmbOleDbType.Text);
            helper.SetString(CommonVariablesKeys.OLEServer, txtOleServer.Text);
            helper.SetString(CommonVariablesKeys.OLEPort, txtOlePort.Text);
            helper.SetString(CommonVariablesKeys.OLEDataBase, txtOleDbName.Text);
            helper.SetString(CommonVariablesKeys.OLEUser, txtOleUser.Text);
            helper.SetString(CommonVariablesKeys.OLEPassword, txtOlePwd.Text);

            //minio服务器设置
            helper.SetString(CommonVariablesKeys.MinioServerURL, minioServer.ServerURL);
            helper.SetString(CommonVariablesKeys.MinioServerAccess, minioServer.AccessName);
            helper.SetString(CommonVariablesKeys.MinioServerPassWord, minioServer.PassWord);
            helper.SetString(CommonVariablesKeys.MinioServerBucket, minioServer.BucketName);

            //保存对资源文件的修改
            helper.Save();
        }

        private void btnTestOle_Click(object sender, EventArgs e)
        {
            try
            {
                IAdoDatabase adoDatabase = new AdoDatabase();
                DatabaseType dbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cmbOleDbType.Text);
                if (!int.TryParse(txtOlePort.Text, out int port))
                {
                    AutoCloseMsgBox.Show("端口号格式错误!","提示",4000);
                    return;
                }
                adoDatabase.InitConnParam(dbType, txtOleServer.Text, port,
                    txtOleDbName.Text, txtOleUser.Text, txtOlePwd.Text);
                if(!adoDatabase.OpenConnect(false))
                {
                    AutoCloseMsgBox.Show("连接失败", "提示", 4000);
                    return;
                }
                AutoCloseMsgBox.Show("连接成功", "提示", 3000);
                //adoDatabase.Close();
            }
            catch(Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "提示", 4000);
            }

            
        }

        private void btnTestSpatial_Click(object sender, EventArgs e)
        {
            IAdoDatabase adoDatabase = new AdoDatabase();
            DatabaseType dbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cmbSpatialDbType.Text);
            if (!int.TryParse(txtSpatialPort.Text, out int port))
            {
                AutoCloseMsgBox.Show("端口号格式错误!", "提示", 4000);
                return;
            }
            adoDatabase.InitConnParam(dbType, txtSpatialServer.Text, port,
                txtSpatialDbName.Text, txtSpatialUser.Text, txtSpatialPwd.Text);
            if (!adoDatabase.OpenConnect(false))
            {
                AutoCloseMsgBox.Show("连接失败", "提示", 4000);
                return;
            }
            AutoCloseMsgBox.Show("连接成功", "提示", 3000);
            //adoDatabase.Close();
        }
    }
}
