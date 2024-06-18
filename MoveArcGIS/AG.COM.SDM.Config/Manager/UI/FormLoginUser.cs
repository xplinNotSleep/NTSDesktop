using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    public partial class FormLoginUser : Form
    {
        #region 私有变量 

        /// <summary>
        /// 当前用户
        /// </summary>
        private AGSDM_SYSTEM_USER m_User = null;

        #endregion

        public FormLoginUser()
        {
            InitializeComponent();
        }

        private void FormLoginUser_Load(object sender, EventArgs e)
        {
            try
            {
                //设置输入焦点
                this.txtFullName.Focus();
                //初始化用户信息
                InitializeUserInfo();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                Close();
            }
        }

        //确定保存
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                if (!string.IsNullOrEmpty(txtOldPw.Text) && m_User.PASSWORD != txtOldPw.Text)
                {
                    MessageBox.Show("原密码输入错误", "提示");
                    return;
                }

                if (!string.IsNullOrEmpty(txtOldPw.Text) && string.IsNullOrEmpty(txtPw.Text))
                {
                    MessageBox.Show("请输入新密码", "提示");
                    return;
                }

                if (string.IsNullOrEmpty(txtOldPw.Text) && !string.IsNullOrEmpty(txtPw.Text))
                {
                    MessageBox.Show("请输入原密码", "提示");
                    return;
                }

                var regex = new Regex(@"
(?=.*[0-9])                     #必须包含数字
(?=.*[a-zA-Z])                  #必须包含小写或大写字母
(?=([\x21-\x7e]+)[^a-zA-Z0-9])  #必须包含特殊符号
.{8,15}                         #至少8个字符，最多30个字符
", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                if (!regex.IsMatch(txtPw.Text))
                {
                    MessageBox.Show("请保证密码为字母，数字和特殊字符的8到15位组合", "提示");
                    return;
                }
                if (this.txtPw.Text.Equals(this.txtPw2.Text, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    MessageBox.Show("两次输入的新密码不一致", "提示");
                    return;
                }
                DateTime dateNow=DateTime.Now;
                DateTime dateCreate = m_User.CTEATE_TIME;
                if ((dateCreate.Year == dateNow.Year&&dateNow.DayOfYear-dateCreate.DayOfYear<=90)||(dateCreate.Year<dateNow.Year&&dateNow.DayOfYear+365-dateCreate.DayOfYear<=90))
                {
                    MessageBox.Show("更改周期应大于90天", "提示");
                    return;
                }
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                m_User.NAME_CN = this.txtFullName.Text;
                m_User.DESCRIPTION = this.txtDescription.Text;
                //当原密码和新密码都输入时才修改密码
                if (!string.IsNullOrEmpty(txtOldPw.Text) && !string.IsNullOrEmpty(txtPw.Text))
                {
                    m_User.PASSWORD = this.txtPw.Text;
                    m_User.CTEATE_TIME=DateTime.Now;
                }

                tEntityHandler.UpdateEntity(m_User, m_User.USER_ID);

                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitializeUserInfo()
        {
            decimal userID = SystemInfo.UserID;
            if (userID < 0) return;

            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

            //linq查询示例：
            //HQL查询
            //AGSDM_SYSTEM_USER tUser = tEntityHandler.GetEntity<AGSDM_SYSTEM_USER>("from AGSDM_SYSTEM_USER t where t.USER_ID=" + userID);

            //Linq查询
            AGSDM_SYSTEM_USER tUser = tEntityHandler.GetEntityLinq<AGSDM_SYSTEM_USER>(t => t.USER_ID == userID);

            if (tUser != null)
            {
                m_User = tUser;

                this.txtName.Text = tUser.NAME_EN;
                this.txtFullName.Text = tUser.NAME_CN;
                this.txtDescription.Text = tUser.DESCRIPTION;
            }
            else
            {
                throw new Exception("用户ID= " + userID + "，找不到用户");
            }
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.StringLength(txtFullName.Text, 25, "全名") &&
            ValidateData.StringLength(txtDescription.Text, 50, "描述信息") &&
            ValidateData.StringLength(txtOldPw.Text, 25, "原密码") &&
            ValidateData.StringLength(txtPw.Text, 25, "新密码") &&
            ValidateData.StringLength(txtPw2.Text, 25, "确认新密码");
        }
    }
}
