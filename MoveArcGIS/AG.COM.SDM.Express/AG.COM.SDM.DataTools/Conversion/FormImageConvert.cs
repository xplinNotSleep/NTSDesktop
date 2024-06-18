using System;
using System.IO;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// դ���ļ���ʽת��������
    /// </summary>
    public partial class FormImageConvert : Form
    {
        public FormImageConvert()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Title = "��ѡ������Ҫת����դ���ļ���";
            tOpenFileDialog.Multiselect = true;
            tOpenFileDialog.CheckFileExists = true;
            tOpenFileDialog.Filter = "JPG�ļ�(*.jpg)|*.jpg|GIF�ļ�(*.gif)|*.gif|λͼ�ļ�(*.bmp)|*.bmp|W3Cͼ��(*.png)|*.png|ͼԪ�ļ�(*.wmf)|*.wmf|TIFF�ļ�(*.tiff)|*.tif|�ɽ���ͼ(*.emf)|*.emf | ȫ���ļ�(*.*)|*.*";
            tOpenFileDialog.FilterIndex = 1;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                int i=0;
                string[] files = tOpenFileDialog.FileNames;
                foreach (string file in files)
                {
                    i++;
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = i.ToString();
                    listItem.SubItems.Add(file);

                    this.listFiles.Items.Add(listItem);
                }
            }
        }

        private void btnSelFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog tFolderBrowserDialog = new FolderBrowserDialog();
            tFolderBrowserDialog.ShowNewFolderButton = true;
            tFolderBrowserDialog.Description = "����ѡ��ת���ļ��ı���·����";

            if (tFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = tFolderBrowserDialog.SelectedPath;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            listFiles.SelectedItems[0].Remove();           
            for (int i = 0; i < listFiles.Items.Count; i++)
            {
                listFiles.Items[i].Text =Convert.ToString ( i + 1);
            }
        }

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count > 0)
                this.btnDelete.Enabled = true;
            else
                this.btnDelete.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormImageConvert_Load(object sender, EventArgs e)
        {
            this.cmbImageType.SelectedIndex = 3;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            #region �жϼ��
            if (this.listFiles.Items.Count == 0)
            {
                Utility.Common.MessageHandler.ShowInfoMsg("��ѡ��Ҫת�����ļ���","��ʾ");  
                return;
            }

            if (this.txtFolder.Text.Length == 0)
            {
                MessageBox.Show("��ѡ������Ҫ������ļ�·��", "��ʾ��Ϣ",MessageBoxButtons .OK ,MessageBoxIcon.Information );
                return;
            }

            if (Directory.Exists(this.txtFolder.Text) == false)
            {
                MessageBox.Show("�����ļ�·������ȷ����ѡ������Ҫ������ļ�·��", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }
            #endregion

            #region ��ȡת�����ļ���ʽ
            string strDestFormat=string.Empty;

            switch (cmbImageType.SelectedIndex )
            {
                case 0:
                    strDestFormat = "jpg";
                    break;
                case 1:
                    strDestFormat = "png";
                    break;
                case 2:
                    strDestFormat = "ico";
                    break;
                case 3:
                    strDestFormat = "tif";
                    break;
                case 4:
                    strDestFormat = "emf";
                    break;
                case 5:
                    strDestFormat = "gif";
                    break;
                case 6:
                    strDestFormat = "wmf";
                    break;
                default :
                    strDestFormat = string.Empty;
                    break;
            }
            if (strDestFormat == string.Empty)
            {
                Utility.Common.MessageHandler.ShowInfoMsg("��ӵ�ͼƬ��ʽ���ԣ�", "��ʾ");
                return;
            }
            #endregion

            try
            {
                this.progressBar1.Maximum = this.listFiles.Items.Count;

                for (int i = 0; i < this.listFiles.Items.Count; i++)
                {
                    this.progressBar1.Value = i+1;

                    //Դ�ļ�·��
                    string sourcePath = this.listFiles.Items[i].SubItems[1].Text;

                    //ʵ���ļ�ʵ������
                    FileInfo tFileInfo = new FileInfo(sourcePath);
                    string strExtName = tFileInfo.Extension;
                    string distationPath = string.Format("{0}\\{1}.{2}", txtFolder.Text, tFileInfo.Name.Replace (strExtName ,"").Trim(),strDestFormat );

                    if (string.Compare(strDestFormat, strExtName, true) == 0)
                    {
                        File.Copy(sourcePath, distationPath);
                    }
                    else
                    {
                        this.ImageFormatter(sourcePath, distationPath, strDestFormat);
                    }
                }

                MessageBox.Show("����Ҫת����դ���ļ�����ɣ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// դ���ļ�ת��
        /// </summary>
        /// <param name="sourcePath">Դ�ļ�·��</param>
        /// <param name="distationPath">Ŀ���ļ�·��</param>
        /// <param name="format">ת����Ŀ���ʽ</param>
        public void ImageFormatter(string sourcePath, string distationPath, string format)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(sourcePath);
            switch (format.ToLower())
            {
                case "bmp":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case "emf":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Emf);
                    break;
                case "gif":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case "ico":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case "jpg":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "png":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "tif":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                case "wmf":
                    bitmap.Save(distationPath, System.Drawing.Imaging.ImageFormat.Wmf);
                    break;
                default: 
                    throw new Exception("�޷�ת���˸�ʽ��");
            }
        }
    }
}