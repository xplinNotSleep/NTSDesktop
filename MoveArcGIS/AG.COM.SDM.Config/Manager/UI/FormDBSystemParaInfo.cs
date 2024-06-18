using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using NHibernate.Linq;
namespace AG.COM.SDM.Config
{
    public partial class FormDBSystemParaInfo : Form
    {
        #region 变量

        /// <summary>
        /// 当前Form使用情况
        /// </summary>
        private InfoFormUseState m_UseState = InfoFormUseState.View;

        /// <summary>
        /// 当前操作的实体
        /// </summary>
        public AGSDM_SYSTEMCONFIG CurrentEntity
        {
            get;
            set;
        }

        #endregion

        #region  初始化

        public FormDBSystemParaInfo(InfoFormUseState useState)
        {
            InitializeComponent();

            m_UseState = useState;
        }

        private void FormPrintGeneralParaSetInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_UseState == InfoFormUseState.Edit || m_UseState == InfoFormUseState.View)
                {
                    txtName.Text = CurrentEntity.NAME;
                    txtValue.Text = CurrentEntity.VALUE;
                    txtDescription.Text = CurrentEntity.DESCRIPTION;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
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
                //数据验证
                if (Valid() == false) return;

                EntityHandler entityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                if (m_UseState == InfoFormUseState.Add)
                {
                    CurrentEntity = new AGSDM_SYSTEMCONFIG();


                   // CurrentEntity.ID = GetMaxId();
                    CurrentEntity.NAME = txtName.Text;
                    CurrentEntity.VALUE = txtValue.Text;
                    CurrentEntity.DESCRIPTION = txtDescription.Text;

                    entityHandler.AddEntity(CurrentEntity);

                    this.DialogResult = DialogResult.OK;
                    Close();
                }else
                {
                    CurrentEntity.NAME = txtName.Text;
                    CurrentEntity.VALUE = txtValue.Text;
                    CurrentEntity.DESCRIPTION = txtDescription.Text;

                    entityHandler.UpdateEntity(CurrentEntity,CurrentEntity.ID);

                    this.DialogResult = DialogResult.OK;
                    Close();
                }

              
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }
        private decimal GetMaxId()
        {
            var session = SessionFactory.OpenSession(CommonConstString.STR_ModelName);
            decimal taskid = session.Query<AGSDM_SYSTEMCONFIG>().Max(m => m.ID);
            return taskid + 1;
        }
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtName.Text, "参数名称")
               || ValidateData.StringLength(txtName.Text, 100, "参数名称")
                 || ValidateData.StringLength(txtDescription.Text, 200, "描述");
        }

        #endregion
    }
}
