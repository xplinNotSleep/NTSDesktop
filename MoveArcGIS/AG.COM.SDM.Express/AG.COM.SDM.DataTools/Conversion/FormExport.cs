using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// shapeת������ʽ����
    /// </summary>
    public partial class FormExport : Form
    {
        private QuickType m_QuickType = QuickType.Nothing;
        private string m_InputFeatures;
        private string m_OutputFile;

        public FormExport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //��֤¼������
                if (m_QuickType == QuickType.Nothing)
                {
                    MessageBox.Show("����ѡ�񵼳����ݸ�ʽ");
                    return;
                }

                if (lvwInputFiles.Items.Count <= 0)
                {
                    MessageBox.Show("����ѡ����Ҫ��");
                    return;
                }

                if (string.IsNullOrEmpty(txtExport.Text))
                {
                    MessageBox.Show("����ѡ�񵼳�Ҫ��");
                    return;
                }

                //��ȡ�����ļ�������ļ��÷ֺŸ���
                m_InputFeatures = "";
                foreach (ListViewItem lvItem in lvwInputFiles.Items)
                {
                    m_InputFeatures += lvItem.Text + ";";
                }
                if (m_InputFeatures.EndsWith(";"))
                {
                    m_InputFeatures = m_InputFeatures.Substring(0, m_InputFeatures.Length - 1);
                }

                MyQuickExport tMyQuickExport = new MyQuickExport();

                tMyQuickExport.QType = m_QuickType;
                tMyQuickExport.InputFeatures = m_InputFeatures;
                tMyQuickExport.OutputFile = txtExport.Text;

                this.lblInfo.Text = "ת��ʱ����ܽϳ��������ĵȺ򡭡�";
                Application.DoEvents();
                this.Cursor = Cursors.WaitCursor;

                tMyQuickExport.Execute();

                this.lblInfo.Text = "";
                this.Cursor = Cursors.Default;
                MessageBox.Show("�����ѳɹ����ת��", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }          

        private void btnFormat_Click(object sender, EventArgs e)
        {
            FormFormat frm = new FormFormat(true);
            if(frm.ShowDialog()==DialogResult.OK)
            {
                lvwInputFiles.Items.Clear();
                txtExport.Text = "";
                switch (frm.SelectItem.Key)
                {
                    case "E00":
                        m_QuickType = QuickType.E00;
                        break;
                    case "vct":
                        m_QuickType = QuickType.Vct;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            switch (m_QuickType)
            {
                case QuickType.Arcinfo:
                case QuickType.E00:
                case QuickType.Mif:
                case QuickType.Vct:
                    if(fbdBrowser.ShowDialog()==DialogResult.OK)
                    {
                        txtExport.Text = fbdBrowser.SelectedPath;
                    }
                    break;
                case QuickType.Gml:
                    sfdExport.Title = "����Ҫ��";                  
                    sfdExport.Filter = "GML�ļ�|*.gml";
                    if(sfdExport.ShowDialog()==DialogResult.OK)
                    {
                        txtExport.Text = sfdExport.FileName;
                    }
                    break;
                case QuickType.Cad:
                    sfdExport.Title = "����Ҫ��";                
                    sfdExport.Filter = "AutoCad�ļ�|*.dwg";
                    if (sfdExport.ShowDialog() == DialogResult.OK)
                    {
                        txtExport.Text = sfdExport.FileName;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCADFile_Click(object sender, EventArgs e)
        {
            try
            {
                ofdShpFile.Title = "ѡ��Shp�ļ�";
                ofdShpFile.Filter = "Shp�ļ�|*.shp";
                ofdShpFile.CheckFileExists = true;
                if (ofdShpFile.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in ofdShpFile.FileNames)
                    {
                        ///��ͬ������
                        if (!lvwInputFiles.Items.Cast<ListViewItem>().Any(r => r.Text == fileName))
                        {
                            this.lvwInputFiles.Items.Add(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
        }

        private void btnDeleteInputFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwInputFiles.SelectedItems.Count > 0)
                {
                    this.lvwInputFiles.Items.Remove(this.lvwInputFiles.SelectedItems[0]);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "����");
            }
        }
    }
}
