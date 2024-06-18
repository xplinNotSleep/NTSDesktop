using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{
    public partial class FormExpressionConstructor : Form
    {
        public FormExpressionConstructor()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (expressionConstructor1.SqlExpression.Trim().Length == 0)
            {
                LibWindows.ShowErrorMsg("��������ʽ��", "���ʽ");
                return;
            }
            this.DialogResult = DialogResult.OK;

        }

        public ExpressionConstructor ExpressionControl
        {
            get { return expressionConstructor1; }
        }
    }
}