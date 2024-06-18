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
    /// ������������
    /// </summary>
    public partial class FormRename : Form
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormRename()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ȡ������������
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
        /// ��ȡ������
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
                MessageHandler.ShowErrorMsg("�����Ʋ���Ϊ�գ�", this.Text);
                return;
            }

            if (txtOldName.Text == txtNewName.Text)
            {
                MessageHandler.ShowErrorMsg("�¾����Ʋ�����ͬ��", this.Text);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}