using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{
	/// <summary>
	/// 输入文本窗体
	/// </summary>
	public partial class FormInputText : Form
	{
		/// <summary>
		/// 输入文本标记
		/// </summary>
		public string InputLabel
		{
			get { return this.labelInput.Text; }
			set { this.labelInput.Text = value; }
		}
		/// <summary>
		/// 输入文本
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