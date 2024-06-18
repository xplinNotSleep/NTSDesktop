using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System.Runtime.InteropServices;
using AG.COM.SDM.Config;
using AG.COM.SDM.Config.DbConnUI;

namespace AG.COM.SDM.Express
{
    /// <summary>
    /// 系统登录窗体
    /// </summary>
    public partial class FormLogin : Form, IMessageForm
    {
        private int m_LoginNum = 0;
        private string loginname = string.Empty;
        /// <summary>
        /// 初始化系统窗体实例
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
            //初始化系统参数
            CommonVariables.Init();
            this.txtLogName.Text = GetUserName();
            this.txtPassword.Focus();

            //自定义窗体界面元素 2017-05-15
            if (LoginDesignHelper.Instance.Init())
            {
                var instance = LoginDesignHelper.Instance;
                this.BackgroundImage = instance.MainForm.BackgroundImage;
                this.Size = instance.MainForm.Size;
                this.Text = instance.MainForm.Text;

                var dict = instance.Controls.ToDictionary(key => key.Name);
                foreach (Control ctrl in Controls)
                {
                    if (dict.ContainsKey(ctrl.Name))
                    {
                        var item = dict[ctrl.Name];
                        ctrl.Visible = item.Visible;
                        ctrl.Location = item.Location;
                        ctrl.Size = item.Size;
                    }
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //系统启动时，开一个子线程连接数据库，这样连接过一次之后，在登录时访问数据库就快很多
            Console.WriteLine("开始连接线程");

            Thread thread = new Thread(delegate ()
            {
                //加try是防止连接出错，出错了也不用处理
                try
                {
                    Console.WriteLine("进入连接线程");

                    RefreshSessionFactory();
                    AbstractFactory factory = AbstractFactory.GetInstance();
                    IUser userManage = factory.CreateUser();
                    AGSDM_SYSTEM_USER user = userManage.GetUser("aa");

                    Console.WriteLine("结束连接线程");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                };
            });
            thread.Start();
        }

        private void btnLoginDataBase_Click(object sender, EventArgs e)
        {
            try
            {
                SaveUserName(this.txtLogName.Text);
                if (this.txtLogName.Text == "")
                {
                    AutoCloseMsgBox.Show("请输入用户名！", "登录", 3000);
                    return;
                }
                if (this.txtPassword.Text == "")
                {
                    AutoCloseMsgBox.Show("请输入用户密码！", "登录", 3000);
                    return;
                }
                RefreshSessionFactory();
                this.lblMessage.Text = "正在进行用户认证，请稍候......";
                this.lblMessage.Visible = true;

                //处理所有后台事件
                Application.DoEvents();

                this.Cursor = Cursors.WaitCursor;

                //初始化系统登录
                SystemLogin pSystemLogin = new SystemLogin();

                //判定本地sde库是否连接成功
                if (pSystemLogin.SysSpatialConnectionState() == false)
                {
                    this.lblMessage.Visible = false;
                    Cursor = Cursors.Default;
                    AutoCloseMsgBox.Show("系统的本地空间数据库配置出错!", "提示", 3000);
                    return;
                    #region 取消必须弹出设置或关闭程序
                    //if (AutoCloseMsgBox.Show("系统的本地sde数据库配置出错，是否重新进行配置？", "配置信息", MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    FrmDbOptions frmSDESet = new FrmDbOptions();
                    //    frmSDESet.ShowDialog(this);
                    //}
                    //else
                    //    Application.Exit();
                    #endregion
                }
                //判定本地设置的ole库是否连接成功
                else if (pSystemLogin.SysOleConnectionState() == false)
                {
                    this.lblMessage.Visible = false;
                    Cursor = Cursors.Default;
                    AutoCloseMsgBox.Show("系统的本地ole数据库配置出错!", "提示", 3000);
                    return;
                }
                else if (pSystemLogin.SysCheckSession()==false)
                {
                    this.lblMessage.Visible = false;
                    Cursor = Cursors.Default;
                    AutoCloseMsgBox.Show("数据库会话连接出错!", "提示", 3000);
                    return;
                }
                else
                {
                    // 调用系统登录事件
                    string pLoginName = this.txtLogName.Text;
                    string pPassWord = this.txtPassword.Text;
                    string nameCN;
                    decimal userID = -1;
                    // 判断系统登录是否成功。
                    if (pSystemLogin.SysLogin(pLoginName, pPassWord, out userID, out nameCN) == false)
                    {
                        AutoCloseMsgBox.Show("用户名和密码出错！", "配置信息", 3000);
                            

                        // 登录失败过三次之后，关闭系统。
                        if (m_LoginNum == 2) Application.Exit();
                        m_LoginNum = m_LoginNum + 1;

                        this.txtPassword.Text = "";
                        this.lblMessage.Text = "用户认证失败！请重新输入密码";

                        Cursor = Cursors.Default;

                        //处理所有后台事件 
                        Application.DoEvents();

                        return;
                    }
                    else
                    {
                        SystemInfo.UserName = this.txtLogName.Text;
                        SystemInfo.UserID = userID;
                        //读取岗位信息
                        AbstractFactory factory = AbstractFactory.GetInstance(); //new HKPopedom.OpusFactory();
                        List<AGSDM_POSITION> lstPosition = factory.CreatePost().GetPosts(SystemInfo.UserID);

                        // 用户登录成功后，进行系统初始化地图操作
                        this.lblMessage.Text = "用户认证成功，系统正在初始化。请稍候......";

                        //保存登录用户
                        SystemInfo.UserName = this.txtLogName.Text;

                        //TODO:人员角色流程复杂
                        if (lstPosition.Count > 0)
                        {
                            AGSDM_POSITION pos = lstPosition[0];
                            SystemInfo.PositionID = pos.ID;
                            SystemInfo.PositionName = pos.POS_NAME;
                            AGSDM_ORG org = GetUserORG(pos.ORG_ID);
                        }

                        if (SystemInfo.UserName.ToUpper() == "ADMIN")
                            SystemInfo.RoleName = "管理员";
                        else
                            SystemInfo.RoleName = "其它用户";

                        //记录用户所属的部门ID 
                        EntityHandler entityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                        string strHQL = "from AGSDM_USER_ORG t where t.USER_ID='" + userID.ToString() + "'";
                        IList<AGSDM_USER_ORG> orgLst = entityHandler.GetEntities<AGSDM_USER_ORG>(strHQL);
                        foreach (AGSDM_USER_ORG org in orgLst)
                        {
                            SystemInfo.OrgIDLst.Add(org.ORG_ID);
                        }

                        //成功登陆后禁用输入控件
                        txtLogName.ReadOnly = true;
                        txtPassword.ReadOnly = true;
                        //chkLoadLastProject.Enabled = false;

                        this.DialogResult = DialogResult.OK;

                        //清理系统下面的临时文件
                        this.lblMessage.Text = "系统正在删除临时文件夹的过期文件。请稍候......";
                        DeleteOutOfDateFile();
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AutoCloseMsgBox.Show(ex.Message, "错误", 5);

                txtLogName.Enabled = true;
                txtPassword.Enabled = true;
                lblMessage.Visible = false;
            }
        }

        /// <summary>
        /// 刷新SessionFactory，在修改OLE数据库参数后调用
        /// </summary>
        private void RefreshSessionFactory()
        {
            //清空SessionFactory
            SessionFactory.ClearSessionFactory();

            OleDBConfig oleConfig = CommonVariables.OledbConn;

            ORMHelper.InitSessionConn(oleConfig, CommonConstString.STR_ModelName);
            //添加编译模块至设定的ole库参数配置中
            //SessionParameter frameSessionParameter = new SessionParameter(
            //        oleServer, oleDbName, olePort, oleUser, olePwd);
            //SessionFactory.SessionParaManager.Add(CommonConstString.STR_ModelName, frameSessionParameter);

        }

        /// <summary>
        /// 获取用户部门信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        private AGSDM_ORG GetUserORG(decimal orgID)
        {
            string strHQL = "from AGSDM_ORG where ORG_ID =" + orgID;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            AGSDM_ORG org = tEntityHandler.GetEntity<AGSDM_ORG>(strHQL);
            return org;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDBSet_Click(object sender, EventArgs e)
        {
            FrmDbOptions frmSDESet = new FrmDbOptions(false);
            frmSDESet.ShowDialog(this);
        }

        public void SetControlEnabled(bool enabled)
        {
            txtLogName.Enabled = enabled;
            this.txtPassword.Enabled = enabled;
            this.btnDBSet.Enabled = enabled;
            this.btnLogin.Enabled = enabled;
            this.btnCancel.Enabled = enabled;
        }

        public void SetMessage(string msg)
        {
            lblMessage.Text = msg;
            Application.DoEvents();
        }

        private void txtLogName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLoginDataBase_Click(sender, null);
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveUserName(string strUserName)
        {
            //保存加载的文档路径，以便下次启动时自动加载
            string strUserFile = CommonConstString.STR_ConfigPath + "\\UserName.txt";

            using (StreamWriter sw = File.CreateText(strUserFile))
            {
                sw.WriteLine(strUserName);
            }
        }

        /// <summary>
        /// 获取上一次用户输入的用户名
        /// </summary>
        /// <returns></returns>
        private string GetUserName()
        {
            string strUserFile = CommonConstString.STR_ConfigPath + "\\UserName.txt";
            if (!File.Exists(strUserFile)) return string.Empty;
            using (StreamReader streamReader = new StreamReader(strUserFile))
            {
                return streamReader.ReadLine();
            }
        }

        #region 文件、文件夹清理
        /// <summary>
        /// 删除临时文件夹下的文件
        /// </summary>
        private void DeleteOutOfDateFile()
        {
            List<string> ltemp = GetTempPath();

            for (int i = 0; i < ltemp.Count; i++)
            {
                string tempPath = ltemp[i];
                if (Directory.Exists(tempPath) == false) return;

                //清理文件夹
                List<string> lstPtah = Directory.GetDirectories(tempPath).ToList();

                foreach (string filePath in lstPtah)
                {
                    DirectoryInfo dir = new DirectoryInfo(filePath);

                    TimeSpan ts = DateTime.Now - dir.CreationTime;

                    if (ts.TotalHours > 24)
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                //清理文件
                lstPtah = Directory.GetFiles(tempPath).ToList();
                foreach (string filePath in lstPtah)
                {
                    FileInfo fil = new FileInfo(filePath);

                    TimeSpan ts = DateTime.Now - fil.CreationTime;

                    if (ts.TotalHours > 24)
                    {
                        try
                        {
                            fil.Delete();
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }
            }
        }
        private void DeleteOutOfDateLogsFile()
        {
            List<string> ltemp = GetLogsPath();

            for (int i = 0; i < ltemp.Count; i++)
            {
                string tempPath = ltemp[i];
                if (Directory.Exists(tempPath) == false) return;

                //清理文件夹
                List<string> lstPtah = Directory.GetDirectories(tempPath).ToList();

                foreach (string filePath in lstPtah)
                {
                    DirectoryInfo dir = new DirectoryInfo(filePath);

                    TimeSpan ts = DateTime.Now - dir.CreationTime;

                    if (ts.TotalHours > 24)
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                //清理文件
                lstPtah = Directory.GetFiles(tempPath).ToList();
                foreach (string filePath in lstPtah)
                {
                    FileInfo fil = new FileInfo(filePath);

                    TimeSpan ts = DateTime.Now - fil.CreationTime;

                    if (ts.TotalDays > 24)
                    {
                        try
                        {
                            fil.Delete();
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 获取存放临时文件目录
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTempPath()
        {
            List<string> ltemp = new List<string>();
            //管线系统临时文件目录
            string tempPathCS = CommonConstString.STR_TempPath;

            //操作系统临时文件目录
            //string tempPathMS = System.IO.Path.GetTempPath() + "\\PipeSystemFileTemp";

            //操作3D分析临时缓存目录
            string mdbPathCS = string.Format("{0}\\Bin\\mdb", CommonConstString.STR_PreAppPath);

            if (Directory.Exists(tempPathCS) == false)
            {
                Directory.CreateDirectory(tempPathCS);

            }
            if (Directory.Exists(mdbPathCS) == false)
            {
                Directory.CreateDirectory(mdbPathCS);

            }
            string LogPath = string.Format("{0}\\Logs", CommonConstString.STR_PreAppPath);
            if (Directory.Exists(LogPath) == false)
            {
                Directory.CreateDirectory(LogPath);
            }
            ltemp.Add(tempPathCS);
            ltemp.Add(mdbPathCS);
            ltemp.Add(LogPath);

            return ltemp;
        }
        /// <summary>
        /// 获取存放临时文件目录
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLogsPath()
        {
            List<string> ltemp = new List<string>();
            //管线系统临时文件目录
            string tempPathCS = CommonConstString.STR_TempPath;

            //操作系统临时文件目录
            //string tempPathMS = System.IO.Path.GetTempPath() + "\\PipeSystemFileTemp";

            //操作3D分析临时缓存目录
            string mdbPathCS = string.Format("{0}\\Bin\\mdb", CommonConstString.STR_PreAppPath);

            if (Directory.Exists(tempPathCS) == false)
            {
                Directory.CreateDirectory(tempPathCS);

            }
            if (Directory.Exists(mdbPathCS) == false)
            {
                Directory.CreateDirectory(mdbPathCS);

            }
            string LogPath = string.Format("{0}\\Logs", CommonConstString.STR_PreAppPath);
            if (Directory.Exists(LogPath) == false)
            {
                Directory.CreateDirectory(LogPath);
            }
            //ltemp.Add(tempPathCS);
            //ltemp.Add(mdbPathCS);
            ltemp.Add(LogPath);

            return ltemp;
        }
        #endregion

        #region 无边框窗体拖动
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wparam, int lparam);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                SendMessage(Handle, 0x00A1, 2, 0);
            }
        }
        #endregion
    }
}