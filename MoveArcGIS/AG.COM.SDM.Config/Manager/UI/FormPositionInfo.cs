using System;
using System.Windows.Forms;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 岗位信息
    /// </summary>
    public partial class FormPositionInfo : Form
    {
        public EnumOperateState OperateState {get;set;}

        /// <summary>
        /// 岗位实体
        /// </summary>
        public AGSDM_POSITION PostionEntity { get; set; }

        public decimal DepartMentID
        {
            set { tbDepartID.Text = value.ToString(); }
        }

        public FormPositionInfo()
        {
            InitializeComponent();
            PostionEntity = new AGSDM_POSITION();
        }

        private void FormPositionInfo_Load(object sender, EventArgs e)
        {
            switch (OperateState)
            {
                case EnumOperateState.Add:
                    btnOK.Visible = true;
                    cboType.SelectedIndex = 0;
                    cboIsBuildCreate.SelectedIndex = 0;
                    break;
                case EnumOperateState.Modify:
                    btnOK.Visible = true;
                    break;
                case EnumOperateState.Query:
                    tbDepartID.ReadOnly = true;
                    tbPostName.ReadOnly = true;
                    tbResponse.ReadOnly = true;
                    cboIsBuildCreate.Enabled = false;
                    cboType.Enabled = false;
                    tbDescption.ReadOnly = false;
                    tbEmail.ReadOnly = false;
                    btnOK.Visible = false;
                    break;
                default:
                    break;
            }
            
            tbPostName.Text = PostionEntity.POS_NAME;
            tbResponse.Text = PostionEntity.POS_FUNCTION;
            tbEmail.Text = PostionEntity.EMAIL;
            tbDescption.Text = PostionEntity.DESCRIPTION;
            tbDepartID.Text = PostionEntity.ORG_ID.ToString();
            cboType.Text = PostionEntity.EDITE_TYPE;
            cboIsBuildCreate.Text = PostionEntity.IS_EDITOR_CREATE;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ///数据验证
            if (Valid() == false) return;

            PostionEntity.POS_NAME = tbPostName.Text ;
            PostionEntity.POS_FUNCTION = tbResponse.Text;
            PostionEntity.EMAIL = tbEmail.Text;
            PostionEntity.DESCRIPTION = tbDescption.Text;
            decimal id = 0;
            decimal.TryParse(tbDepartID.Text,out id);
            PostionEntity.ORG_ID = id;
            PostionEntity.EDITE_TYPE = cboType.Text;
            PostionEntity.IS_EDITOR_CREATE = cboIsBuildCreate.Text;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(tbPostName.Text, "岗位名称") &&
             ValidateData.StringLength(tbPostName.Text, 50, "岗位名称") &&
 ValidateData.StringLength(tbResponse.Text, 200, "岗位职责") &&
  ValidateData.StringLength(cboType.Text, 10, "编制类型") &&
   ValidateData.StringLength(tbEmail.Text, 30, "邮箱") &&
   ValidateData.StringLength(tbDescption.Text, 100, "岗位描述");
        }
    }
}
