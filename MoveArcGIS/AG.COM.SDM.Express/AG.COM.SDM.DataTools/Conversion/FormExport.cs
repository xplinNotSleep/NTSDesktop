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
    /// shape转其它格式数据
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
                //验证录入内容
                if (m_QuickType == QuickType.Nothing)
                {
                    MessageBox.Show("请先选择导出数据格式");
                    return;
                }

                if (lvwInputFiles.Items.Count <= 0)
                {
                    MessageBox.Show("请先选择导入要素");
                    return;
                }

                if (string.IsNullOrEmpty(txtExport.Text))
                {
                    MessageBox.Show("请先选择导出要素");
                    return;
                }

                //获取输入文件，多个文件用分号隔开
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

                this.lblInfo.Text = "转换时间可能较长，请耐心等候……";
                Application.DoEvents();
                this.Cursor = Cursors.WaitCursor;

                tMyQuickExport.Execute();

                this.lblInfo.Text = "";
                this.Cursor = Cursors.Default;
                MessageBox.Show("数据已成功完成转换", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
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
                    sfdExport.Title = "导出要素";                  
                    sfdExport.Filter = "GML文件|*.gml";
                    if(sfdExport.ShowDialog()==DialogResult.OK)
                    {
                        txtExport.Text = sfdExport.FileName;
                    }
                    break;
                case QuickType.Cad:
                    sfdExport.Title = "导出要素";                
                    sfdExport.Filter = "AutoCad文件|*.dwg";
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
                ofdShpFile.Title = "选择Shp文件";
                ofdShpFile.Filter = "Shp文件|*.shp";
                ofdShpFile.CheckFileExists = true;
                if (ofdShpFile.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in ofdShpFile.FileNames)
                    {
                        ///相同的项不添加
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
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
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
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
    }
}
