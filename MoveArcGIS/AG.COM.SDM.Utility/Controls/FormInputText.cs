using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{
	/// <summary>
	/// �����ı�����
	/// </summary>
	public partial class FormInputText : Form
	{
		/// <summary>
		/// �����ı����
		/// </summary>
		public string InputLabel
		{
			get { return this.labelInput.Text; }
			set { this.labelInput.Text = value; }
		}
		/// <summary>
		/// �����ı�
		/// </summary>
		public string InputText
		{
			get { return this.textBoxInput.Text; }
			set { this.textBoxInput.Text = value; }
		}

		public FormInputText()
		{
			InitializeComponent();
		}
	}
}