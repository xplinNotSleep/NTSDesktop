using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// ��������GeoDatabase ������
    /// </summary>
    public partial class FormCreateLocalGeoDatabase : Form
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
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
        /// ��������GeoDatabase 
        /// </summary>
        private void btOK_Click(object sender, EventArgs e)
        {
            if (txtDirectory.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("��ѡ��Ҫ�������ĸ��ļ��У�", Text);
                return;
            }
            if (System.IO.Directory.Exists(txtDirectory.Text) == false)
            {
                MessageHandler.ShowErrorMsg("�ļ��в����ڣ�", Text);
                return;
            }
            if (txtName.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("���������ƣ�", Text);
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

                MessageHandler.ShowInfoMsg("�����ɹ���", Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMsg("����ʧ�ܣ�" + ex.Message, Text);
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