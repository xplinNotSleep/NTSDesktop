using AG.COM.SDM.Database;
using System;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// SDE连接用户控件类
    /// </summary>
    public partial class SpatialDbConfig : UserControl
    {
        /// <summary>
        /// 实例化新对象
        /// </summary>
        public SpatialDbConfig()
        {
            //初始化界面组件
            InitializeComponent();

            //this.txtInstance.Text = "sde:oracle11g:localhost/orcl";
            //this.txtDataBase.Text = "SDE";
            //this.txtUser.Text = "SDE";
            this.cboVersions.Text = "SDE.DEFAULT";
        }

        /// <summary>
        /// 获取或设置服务器
        /// </summary>
        public DatabaseType DataBaseType
        {
            get { return (DatabaseType)Enum.Parse(typeof(DatabaseType), cmbDataType.Text, false); }
            set { cmbDataType.Text = value.ToString(); }
        }

        /// <summary>
        /// 获取或设置服务器
        /// </summary>
        public string Server
        {
            get { return txtServerName.Text; }
            set { txtServerName.Text = value; }
        }

        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        public string Port
        {
            get { return txtPort.Text; }
            set { txtPort.Text = value; }
        }

        /// <summary>
        /// 获取或设置实例(按普通数据库设置，无需设置实例)
        /// </summary>
        //public string Instance
        //{
        //    get { return txtInstance.Text; }
        //    set { txtInstance.Text = value; }
        //}

        /// <summary>
        /// 获取或设置数据库
        /// </summary>
        public string DataBase
        {
            get { return txtDataBase.Text; }
            set { txtDataBase.Text = value; }
        }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string User
        {
            get { return txtUser.Text; }
            set { txtUser.Text = value; }
        }


        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password
        {
            get { return txtPwd.Text; }
            set { txtPwd.Text = value; }
        }

        /// <summary>
        /// 获取或设置版本
        /// </summary>
        public string Version
        {
            get { return cboVersions.Text; }
            set { cboVersions.Text = value; }
        }
        

        /// <summary>
        /// 检测属性设置有效性
        /// </summary>
        /// <returns>返回检测信息</returns>
        public bool CheckInputValid()
        {
            StringBuilder tStrBuilder = new StringBuilder();
            if (txtServerName.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'Server' ");
            }

            if (txtPort.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'Service' ");
            }

            //if (txtDataBase.Text.Trim().Length == 0)
            //{
            //    tStrBuilder.Append("'DataBase' ");
            //}

            if (txtUser.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'User' ");
            }

            if (tStrBuilder.ToString().Length > 0)
            {
                tStrBuilder.Append("不能为空!");
                MessageBox.Show(tStrBuilder.ToString());
                return false;
            }
            else
                return true;
        }

        private void btnChanged_Click(object sender, EventArgs e)
        {
            try
            {
                IWorkspace ws = OpenWorkspace(true);
                if (ws == null)
                {
                    MessageBox.Show("连接失败！", "数据库连接");
                    return;
                }

                cboVersions.Items.Clear();

                IEnumVersionInfo pEnum = (ws as IVersionedWorkspace).Versions;
                IVersionInfo ver = pEnum.Next();
                while (ver != null)
                {
                    cboVersions.Items.Add(ver.VersionName);
                    ver = pEnum.Next();
                }
                if (cboVersions.SelectedIndex == -1)
                {
                    if (cboVersions.Items.Count > 0)
                        cboVersions.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }
       

        private void cmbDataType_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbDataType.SelectedItem.ToString()=="Oracle")
            {
                if(string.IsNullOrWhiteSpace(txtServerName.Text))
                {
                    txtPort.Text = "sde:oracle11g:localhost/orcl";
                }else
                {
                    txtPort.Text =$"sde:oracle11g:{txtServerName.Text}/orcl";
                }
              
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtServerName.Text))
                {
                    txtPort.Text = "sde:postgresql:localhost";
                }
                else
                {
                    txtPort.Text = $"sde:postgresql:{txtServerName.Text}";
                }
                   
            }
           
        }

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region ESRI部分

        ///// <summary>
        ///// 获取工作空间
        ///// </summary>
        ///// <returns>返回IWorkspace</returns>
        //public IWorkspace OpenWorkspace()
        //{
        //    //检测属性设置有效性
        //    bool IsValid = CheckInputValid();
        //    if (IsValid == false)
        //        return null;

        //    //获取连接属性
        //    IPropertySet tPropertySet = GetPropertySet();

        //    //由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
        //    string server = tPropertySet.GetProperty("Server").ToString();
        //    //if (NetHelper.Ping(server) == false) return null;    

        //    //实例化SdeWorkspaceFactoryClass类
        //    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

        //    try
        //    {
        //        //根据连接属性打开工作空间
        //        IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
        //        return tWorkspace;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 获取工作空间
        ///// </summary>
        ///// <param name="emptyVersion">是否允许将版本设为空</param>
        ///// <returns>返回IWorkspace</returns>
        //public IWorkspace OpenWorkspace(bool emptyVersion)
        //{
        //    //检测属性设置有效性
        //    bool IsValid = CheckInputValid();
        //    if (IsValid == false)
        //        return null;

        //    //获取连接属性
        //    IPropertySet tPropertySet = GetPropertySet();
        //    if (emptyVersion)
        //        //tPropertySet.SetProperty("VERSION", "");
        //        tPropertySet.SetProperty("VERSION", "SDE.DEFAULT");

        //    //由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
        //    string server = tPropertySet.GetProperty("Server").ToString();
        //    if (NetHelper.Ping(server) == false)
        //    {
        //        if (MessageBox.Show("服务器Ping不通，可能是服务器不存在，请问是否继续连接？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //        {
        //            throw new Exception("服务器 ping不通");
        //        }
        //    }

        //    //实例化SdeWorkspaceFactoryClass类
        //    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

        //    //根据连接属性打开工作空间
        //    IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
        //    return tWorkspace;
        //}

        #endregion


    }
}