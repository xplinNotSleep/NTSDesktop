using System;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 部门信息窗体类
    /// </summary>
    /// Rewrite:2010-9-13
    public partial class FormDepartmentInfo : Form
    {
        private EnumOperateState m_OperateState;
        private DepartmentInfo m_DeptInfo;
        private AGSDM_ORG m_ORGCurrent;
        private string m_ORGCodeDefault;
        private decimal m_ORGIDParent;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormDepartmentInfo()
        {
            InitializeComponent();
            this.m_ORGCurrent = new AGSDM_ORG();
            this.m_DeptInfo = new DepartmentInfo();
            this.m_OperateState = EnumOperateState.Query;
        }

        /// <summary>
        /// 获取或设置部门信息
        /// </summary>
        public AGSDM_ORG ORGCurrent
        {
            get
            {
                return this.m_ORGCurrent;
            }
            set
            {
                this.m_ORGCurrent = value;
            }
        }

        /// <summary>
        /// 获取或设置默认部门编码
        /// </summary>
        public string ORGCodeDefault
        {
            get
            {
                return this.m_ORGCodeDefault;
            }
            set
            {
                this.m_ORGCodeDefault = value;
            }

        }

        /// <summary>
        /// 获取或设置父部门ID
        /// </summary>
        public decimal ORGIDParent
        {
            get
            {
                return this.m_ORGIDParent;
            }
            set
            {
                this.m_ORGIDParent = value;
            }
        }

        /// <summary>
        /// 获取或设置部门信息
        /// </summary>
        public DepartmentInfo DeptmentInfo
        {
            get
            {
                return this.m_DeptInfo;
            }
            set
            {
                this.m_DeptInfo = value;
            }
        }

        /// <summary>
        /// 获取或设置当前操作状态
        /// </summary>
        public EnumOperateState OperateState
        {
            get
            {
                return this.m_OperateState;
            }
            set
            {
                this.m_OperateState = value;
            }
        }

        private void FormDepartmentInfo_Load(object sender, EventArgs e)
        {
            try
            {
                //查询时初始化
                if (this.m_OperateState == EnumOperateState.Query)
                {
                    DisabledAllControl();

                    ShowData();
                }
                //添加时初始化
                else if (this.m_OperateState == EnumOperateState.Add)
                {
                    this.cmbCode.Text = this.m_ORGCodeDefault;
                    this.txtName.Focus();
                }
                else
                {
                    ShowData();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        //取消 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
                this.m_ORGCurrent.ORG_NAME = this.txtName.Text;
                this.m_ORGCurrent.DESCRIPTION = this.txtDescription.Text;
                this.m_ORGCurrent.ORG_ADDR = this.txtAddress.Text;
                this.m_ORGCurrent.ORG_CODE = this.cmbCode.Text;
                this.m_ORGCurrent.LINK_MAN = this.txtPeople.Text;
                this.m_ORGCurrent.LINK_TEL = this.txtTel.Text;

                this.DialogResult = DialogResult.OK;
                if (this.m_ORGCurrent.ORG_ID == 0)
                {
                    this.m_ORGCurrent.PARENT_ORG_ID = this.m_ORGIDParent;
                    tEntityHandler.AddEntity(this.m_ORGCurrent);
                }
                else
                {
                    tEntityHandler.UpdateEntity(this.m_ORGCurrent, this.m_ORGCurrent.ORG_ID);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        /// <summary>
        /// 赋值信息到界面
        /// </summary>
        private void ShowData()
        {
            this.txtName.Text = this.m_ORGCurrent.ORG_NAME;
            this.txtPeople.Text = this.m_ORGCurrent.LINK_MAN;
            this.txtTel.Text = this.m_ORGCurrent.LINK_TEL;
            this.txtDescription.Text = this.m_ORGCurrent.DESCRIPTION;
            this.txtAddress.Text = this.m_ORGCurrent.ORG_ADDR;
            this.cmbCode.Text = this.m_ORGCurrent.ORG_CODE;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
             return ValidateData.NotNull(txtName.Text, "单位名称") &&
                ValidateData.StringLength(txtName.Text, 50, "单位名称") &&
                ValidateData.StringLength(cmbCode.Text, 25, "单位代码") &&
                ValidateData.StringLength(txtPeople.Text, 25, "联系人") &&
                ValidateData.StringLength(txtAddress.Text, 125, "联系地址") &&
                ValidateData.StringLength(txtDescription.Text, 100, "描述信息");
        }

        /// <summary>
        /// 禁用所有输入类型控件
        /// </summary>
        private void DisabledAllControl()
        {
            txtName.ReadOnly = true;
            cmbCode.Enabled = false;
            txtPeople.ReadOnly = true;
            txtTel.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtDescription.ReadOnly = true;            
            btnOk.Enabled = false;
        }
    }
}