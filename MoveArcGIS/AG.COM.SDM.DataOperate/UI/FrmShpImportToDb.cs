using AG.COM.SDM.Config.DbConnUI;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.DataOperate
{
    public partial class FrmShpImportToDb : DockDocument
    {
        private string m_ShpPath = "";

        public FrmShpImportToDb()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 选择导入shp数据的文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            browserDialog.Description = "选择源数据文件夹";
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = browserDialog.SelectedPath;
                m_ShpPath = browserDialog.SelectedPath;
            }
            else
            {
                return;
            }


        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if(txtSource.Text =="" || m_ShpPath =="")
            {
                AutoCloseMsgBox.Show("未选择文件夹!", "提示", 1500);
                return;
            }

            if(!Directory.Exists(m_ShpPath))
            {
                AutoCloseMsgBox.Show("文件夹不存在!", "提示", 1500);
                return;
            }

            bool IsOverload = IsCheckOverload.Checked ;
            //根据所选文件夹和设置的SRID对其中的矢量数据进行入库
            ShpToDbHelper.CreateToDbByShp(m_ShpPath, IsOverload, 4490);

        }

        private void btnDbSet_Click(object sender, EventArgs e)
        {
            FrmDbOptions frm = new FrmDbOptions(true);
            frm.ShowInTaskbar = false;
            frm.Show();
        }
    }
}
