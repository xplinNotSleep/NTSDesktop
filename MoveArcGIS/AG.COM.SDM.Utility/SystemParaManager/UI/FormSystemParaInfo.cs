using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Utility
{
    public partial class FormSystemParaInfo : Form
    {
        #region 变量

        /// <summary>
        /// 当前Form使用情况
        /// </summary>
        private InfoFormUseState m_UseState = InfoFormUseState.View;

        /// <summary>
        /// 当前操作的SystemPara
        /// </summary>
        public SystemPara CurrentSystemPara
        {
            get;
            set;
        }

        #endregion

        #region  初始化

        public FormSystemParaInfo(InfoFormUseState tUseState)
        {
            InitializeComponent();

            m_UseState = tUseState;
        }

        private void FormPrintGeneralParaSetInfo_Load(object sender, EventArgs e)
        {
            try
            {              
                if (m_UseState == InfoFormUseState.Edit || m_UseState == InfoFormUseState.View)
                {
                    txtName.Text = CurrentSystemPara.Name;
                    txtValue.Text = CurrentSystemPara.Value;
                    txtDescription.Text = CurrentSystemPara.Description;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        #endregion

        #region 其他

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                SystemPara tSystemPara = CurrentSystemPara;
                if (tSystemPara == null)
                    tSystemPara = new SystemPara();

                tSystemPara.Name = txtName.Text;
                tSystemPara.Value = txtValue.Text;
                tSystemPara.Description = txtDescription.Text;

                CurrentSystemPara = tSystemPara;

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtName.Text, "参数名称");
        }

        #endregion
    }
}
