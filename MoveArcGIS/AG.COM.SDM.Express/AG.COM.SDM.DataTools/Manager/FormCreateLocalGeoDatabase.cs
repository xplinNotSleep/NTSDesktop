using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// 创建本地GeoDatabase 窗体类
    /// </summary>
    public partial class FormCreateLocalGeoDatabase : Form
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormCreateLocalGeoDatabase()
        {
            InitializeComponent();
        }

        private void FormCreateLocalGeoDatabase_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new System.Drawing.Bitmap(btOpen.Image);
            bmp.MakeTransparent();
            btOpen.Image = bmp;

            this.cboType.SelectedIndex = 0;
        }

        /// <summary>
        /// 创建本地GeoDatabase 
        /// </summary>
        private void btOK_Click(object sender, EventArgs e)
        {
            if (txtDirectory.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("请选择要创建到哪个文件夹！", Text);
                return;
            }
            if (System.IO.Directory.Exists(txtDirectory.Text) == false)
            {
                MessageHandler.ShowErrorMsg("文件夹不存在！", Text);
                return;
            }
            if (txtName.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("请输入名称！", Text);
                return;
            }

            try
            {
                string dir = txtDirectory.Text;
                string name = txtName.Text;
                string ext = System.IO.Path.GetExtension(name).Trim().ToLower();
                IWorkspaceFactory pWF;
                if (cboType.SelectedIndex == 0)
                {
                    if (ext != ".mdb")
                        name = name + ".mdb";

                    pWF = new AccessWorkspaceFactoryClass();
                    pWF.Create(dir, name, null, 0);

                }
                else
                {
                    if (ext != ".gdb")
                        name = name + ".gdb";

                    pWF = new FileGDBWorkspaceFactoryClass();
                    pWF.Create(dir, name, null, 0);
                }

                MessageHandler.ShowInfoMsg("创建成功！", Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMsg("创建失败！" + ex.Message, Text);
            }            
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtDirectory.Text = dlg.SelectedPath;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}