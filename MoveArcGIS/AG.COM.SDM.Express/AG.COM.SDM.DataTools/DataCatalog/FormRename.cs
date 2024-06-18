using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;

namespace AG.COM.SDM.DataTools.DataCatalog
{
    /// <summary>
    /// 重命名窗体类
    /// </summary>
    public partial class FormRename : Form
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormRename()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置新名称
        /// </summary>
        public string NewName
        {
            get
            {
                return txtNewName.Text;
            }
            set
            {
                txtNewName.Text = value;
            }
        }

        /// <summary>
        /// 获取旧名称
        /// </summary>
        public string OldName
        {
            set
            {
                txtOldName.Text = value;
            }
        }
        
        private void btOK_Click(object sender, EventArgs e)
        {
            if (txtNewName.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("新名称不能为空！", this.Text);
                return;
            }

            if (txtOldName.Text == txtNewName.Text)
            {
                MessageHandler.ShowErrorMsg("新旧名称不能相同！", this.Text);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}