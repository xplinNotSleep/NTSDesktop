using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 岗位管理
    /// </summary>
    public partial class FormPositionManage : Form
    {
        public FormPositionManage()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPositionManage_Load(object sender, EventArgs e)
        {

        }
    }
}
