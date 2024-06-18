using System;
using System.IO;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// ������ʽ����תPersonal GDB
    /// </summary>
    public partial class FormImport : Form
    {
        private QuickType m_QuickType = QuickType.Nothing;
        private string m_InputFeatures;
        private string m_OutputFile;

        public FormImport()
        {
            InitializeComponent();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            FormFormat frm = new FormFormat(false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtImport.Text = "";
                txtExport.Text = "";
                switch (frm.SelectItem.Key)
                {
                    case "E00":
                        m_QuickType = QuickType.E00;
                        break;
                    case "mif":
                        m_QuickType = QuickType.Mif;
                        break;
                    case "dwg":
                        m_QuickType = QuickType.Cad;
                        break;
                    case "gml":
                        m_QuickType = QuickType.Gml;
                        break;
                    case "adf":
                        m_QuickType = QuickType.Arcinfo;
                        break;
                }
                txtFormat.Text = frm.SelectItem.Value;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ofdImport.Title = "�����Ҫ��";
            ofdImport.InitialDirectory = Application.StartupPath;
            switch (m_QuickType)
            {
                case QuickType.Arcinfo:
                    if (fbdImport.ShowDialog() == DialogResult.OK)
                    {
                        txtImport.Text = fbdImport.SelectedPath;
                    }
                    break;
                case QuickType.E00:
                    ofdImport.Filter = "E00�ļ�|*.e00";
                    if (ofdImport.ShowDialog() == DialogResult.OK)
                    {
                        txtImport.Text = ofdImport.FileName;
                    }
                    break;
                case QuickType.Mif:
                    ofdImport.Filter = "MIF�ļ�|*.mif";
                    if (ofdImport.ShowDialog() == DialogResult.OK)
                    {
                        txtImport.Text = ofdImport.FileName;
                    }
                    break;
                case QuickType.Gml:
                    ofdImport.Filter = "GML�ļ�|*.gml";
                    if (ofdImport.ShowDialog() == DialogResult.OK)
                    {
                        txtImport.Text = ofdImport.FileName;
                    }
                    break;
                case QuickType.Cad:
                    ofdImport.Filter = "AutoCad�ļ�|*.dwg";
                    if (ofdImport.ShowDialog() == DialogResult.OK)
                    {
                        txtImport.Text = ofdImport.FileName;
                    }
                    break;
            }
            if (File.Exists(txtImport.Text))
            {
                FileInfo tt = new FileInfo(txtImport.Text);
                txtExport.Text = tt.FullName.Substring(0, tt.FullName.Length - tt.Extension.Length) + ".gdb";
            }
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            if(fbdGeoDataBase.ShowDialog()==DialogResult.OK)
            {
                txtExport.Text = fbdGeoDataBase.SelectedPath+".gdb";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_InputFeatures = txtImport.Text;
            m_OutputFile = txtExport.Text;

            MyQuickImport tMyQuickImport = new MyQuickImport();
            tMyQuickImport.QType = m_QuickType;
            tMyQuickImport.InputFeatures = m_InputFeatures;
            tMyQuickImport.OutputFile = m_OutputFile;

            this.lblInfo.Text = "���ڽ���ת������";
            this.Cursor = Cursors.WaitCursor;          

            tMyQuickImport.Execute();

            this.Cursor = Cursors.Default;
            this.lblInfo.Text = "";

            MessageBox.Show("�ѳɹ�����������ݵ��������ݿ��ת��", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}