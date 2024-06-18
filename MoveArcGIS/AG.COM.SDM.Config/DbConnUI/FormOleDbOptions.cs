using System;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Ole数据库连接属性信息窗体类
    /// </summary>
    public partial class FormOleDbOptions : Form
    {
        private OleConnProperty m_OleConnProperty;          //Ole数据库连接属性

        #region 构造函数
        public FormOleDbOptions()
        {
            InitializeComponent();
        }

        public FormOleDbOptions(OleConnProperty pOleConnProperty):this()
        {
            m_OleConnProperty = pOleConnProperty;
        }
        #endregion

        /// <summary>
        /// 获取或设置Ole数据库连接属性
        /// </summary>
        public OleConnProperty OLE_ConnProperty
        {
            get
            {
                return this.m_OleConnProperty;
            }
            set
            {
                m_OleConnProperty = value;
            }
        }
        
        private void FormOleDbOptions_Load(object sender, EventArgs e)
        {
            if (m_OleConnProperty == null)
            {
                cmbDataType.SelectedIndex = 0;
                return;
            }

            txtName.Text = m_OleConnProperty.OLE_Name;
            cmbDataType.Text = m_OleConnProperty.DataBaseType.ToString();
            txtServer.Text = m_OleConnProperty.OLE_Server;
            txtPort.Text = m_OleConnProperty.OLE_Port;
            txtDataBase.Text = m_OleConnProperty.OLE_DataBase;
            txtUser.Text = m_OleConnProperty.OLE_User;
            txtPassword.Text = m_OleConnProperty.OLE_Password;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_OleConnProperty == null)
            {
                bool bOleContained = CommonVariables.DatabaseConfig.OLE_ConnManager.Contained(txtName.Text);
                if (bOleContained || txtName.Text == string.Empty)
                {
                    MessageBox.Show("标识必须唯一且不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }

                m_OleConnProperty = new OleConnProperty();
            }

            m_OleConnProperty.OLE_Name = txtName.Text;
            m_OleConnProperty.DataBaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cmbDataType.Text, false);
            m_OleConnProperty.OLE_Server = txtServer.Text;
            m_OleConnProperty.OLE_Port = txtPort.Text;
            m_OleConnProperty.OLE_DataBase = txtDataBase.Text;
            m_OleConnProperty.OLE_User = txtUser.Text;
            m_OleConnProperty.OLE_Password = txtPassword.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
