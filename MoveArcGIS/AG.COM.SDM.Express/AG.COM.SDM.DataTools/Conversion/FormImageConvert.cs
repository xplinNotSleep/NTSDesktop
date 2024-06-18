using System;
using System.IO;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 栅格文件格式转换窗体类
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
            tOpenFileDialog.Title = "请选择您需要转换的栅格文件…";
            tOpenFileDialog.Multiselect = true;
            tOpenFileDialog.CheckFileExists = true;
            tOpenFileDialog.Filter = "JPG文件(*.jpg)|*.jpg|GIF文件(*.gif)|*.gif|位图文件(*.bmp)|*.bmp|W3C图形(*.png)|*.png|图元文件(*.wmf)|*.wmf|TIFF文件(*.tiff)|*.tif|可交换图(*.emf)|*.emf | 全部文件(*.*)|*.*";
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
            tFolderBrowserDialog.Description = "请您选择转换文件的保存路径…";

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
            #region 判断检查
            if (this.listFiles.Items.Count == 0)
            {
                Utility.Common.MessageHandler.ShowInfoMsg("请选择要转换的文件！","提示");  
                return;
            }

            if (this.txtFolder.Text.Length == 0)
            {
                MessageBox.Show("请选择您需要保存的文件路径", "提示信息",MessageBoxButtons .OK ,MessageBoxIcon.Information );
                return;
            }

            if (Directory.Exists(this.txtFolder.Text) == false)
            {
                MessageBox.Show("保存文件路径不正确，请选择您需要保存的文件路径", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }
            #endregion

            #region 获取转换的文件格式
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
                Utility.Common.MessageHandler.ShowInfoMsg("添加的图片格式不对！", "提示");
                return;
            }
            #endregion

            try
            {
                this.progressBar1.Maximum = this.listFiles.Items.Count;

                for (int i = 0; i < this.listFiles.Items.Count; i++)
                {
                    this.progressBar1.Value = i+1;

                    //源文件路径
                    string sourcePath = this.listFiles.Items[i].SubItems[1].Text;

                    //实例文件实例对象
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

                MessageBox.Show("您需要转换的栅格文件已完成！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 栅格文件转换
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="distationPath">目标文件路径</param>
        /// <param name="format">转换的目标格式</param>
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
                    throw new Exception("无法转换此格式！");
            }
        }
    }
}