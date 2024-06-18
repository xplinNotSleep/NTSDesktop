using System;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 权限管理窗体
    /// </summary>
    public partial class FormPrivilegeManage : Form
    {
        private IHookHelper m_HookHelper = null;

        private EnumPrivilegeType m_enumPrivilegeType = EnumPrivilegeType.User;
        private IPrivilegeOperate m_PrivilegeOperate = null;

        /// <summary>
        /// HookHelper，供外部传入
        /// </summary>
        public IHookHelper HookHelper
        {
            set
            {
                m_HookHelper = value;
            }
        }

        public FormPrivilegeManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置权限管理类型
        /// </summary>
        public EnumPrivilegeType PrivilegeType
        {
            get
            {
                return this.m_enumPrivilegeType;
            }
            set
            {
                this.m_enumPrivilegeType = value;
            }
        }       

        private void FormPrivilegeManage_Load(object sender, EventArgs e)
        {
            try
            {
                switch (this.m_enumPrivilegeType)
                {
                    case EnumPrivilegeType.User:

                        //用户管理
                        CtrUser tCtrUser = new CtrUser();
                        tCtrUser.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrUser as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrUser);
                        this.Text = "用户管理";
                        break;

                    case EnumPrivilegeType.Dept:

                        //部门管理
                        CtrDepartment tCtrDepartment = new CtrDepartment();
                        tCtrDepartment.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrDepartment as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrDepartment);
                        this.Text = "部门管理";
                        break;
                    case EnumPrivilegeType.DeptPro:
                        //部门工程管理 Reweite:2010-9-26
                        CtrDeptProject tCtrDeptProject = new CtrDeptProject();
                        tCtrDeptProject.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrDeptProject as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrDeptProject);
                        this.Text = "部门工程管理";
                        break;
                    case EnumPrivilegeType.DeptLayer:
                        //部门图层管理
                        CtrDeptLayer tCtrDepLayer = new CtrDeptLayer();
                        tCtrDepLayer.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrDepLayer as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrDepLayer);
                        this.Text = "部门图层管理";
                        break;

                    default:
                        //默认为角色管理
                        CtrRole tCtrRole = new CtrRole(m_HookHelper);
                        tCtrRole.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrRole as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrRole);
                        this.Text = "角色管理";
                        break;
                }

                //初始化处理
                m_PrivilegeOperate.Init();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}