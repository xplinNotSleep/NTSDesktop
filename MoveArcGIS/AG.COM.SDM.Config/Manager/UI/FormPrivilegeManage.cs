using System;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Ȩ�޹�����
    /// </summary>
    public partial class FormPrivilegeManage : Form
    {
        private IHookHelper m_HookHelper = null;

        private EnumPrivilegeType m_enumPrivilegeType = EnumPrivilegeType.User;
        private IPrivilegeOperate m_PrivilegeOperate = null;

        /// <summary>
        /// HookHelper�����ⲿ����
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
        /// ��ȡ������Ȩ�޹�������
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

                        //�û�����
                        CtrUser tCtrUser = new CtrUser();
                        tCtrUser.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrUser as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrUser);
                        this.Text = "�û�����";
                        break;

                    case EnumPrivilegeType.Dept:

                        //���Ź���
                        CtrDepartment tCtrDepartment = new CtrDepartment();
                        tCtrDepartment.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrDepartment as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrDepartment);
                        this.Text = "���Ź���";
                        break;
                    case EnumPrivilegeType.DeptPro:
                        //���Ź��̹��� Reweite:2010-9-26
                        CtrDeptProject tCtrDeptProject = new CtrDeptProject();
                        tCtrDeptProject.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrDeptProject as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrDeptProject);
                        this.Text = "���Ź��̹���";
                        break;
                    case EnumPrivilegeType.DeptLayer:
                        //����ͼ�����
                        CtrDeptLayer tCtrDepLayer = new CtrDeptLayer();
                        tCtrDepLayer.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrDepLayer as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrDepLayer);
                        this.Text = "����ͼ�����";
                        break;

                    default:
                        //Ĭ��Ϊ��ɫ����
                        CtrRole tCtrRole = new CtrRole(m_HookHelper);
                        tCtrRole.Dock = DockStyle.Fill;
                        m_PrivilegeOperate = tCtrRole as IPrivilegeOperate;
                        this.panel1.Controls.Add(tCtrRole);
                        this.Text = "��ɫ����";
                        break;
                }

                //��ʼ������
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