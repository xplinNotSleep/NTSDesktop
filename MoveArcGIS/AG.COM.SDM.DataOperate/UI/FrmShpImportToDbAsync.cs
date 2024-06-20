using AG.COM.SDM.Config;
using AG.COM.SDM.Config.DbConnUI;
using AG.COM.SDM.Database;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Framework.DocumentView.UI;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.DataOperate
{
    public partial class FrmShpImportToDbAsync : DockDocument
    {
        private IHookHelper m_hookHelper;
        private string m_ShpPath = "";
        private SubDocument SubDocument;

        public FrmShpImportToDbAsync(IHookHelper hookHelper)
        {
            m_hookHelper = hookHelper;
            SubDocument = m_hookHelper.DockDocumentService.GetDockDocument
                    (Convert.ToString(EnumDocumentType.SubDocument)) as SubDocument; ;
            
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

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (txtSource.Text == "" || m_ShpPath == "")
            {
                AutoCloseMsgBox.Show("未选择文件夹!", "提示", 1500);
                return;
            }

            if (!Directory.Exists(m_ShpPath))
            {
                AutoCloseMsgBox.Show("文件夹不存在!", "提示", 1500);
                return;
            }
            try
            {
                bool IsOverload = IsCheckOverload.Checked;
                
                //将其封装成一个可在后台运行的线程
                await CreateToDbByShpAsync(m_ShpPath, IsOverload, 4490);
                
            }
            catch (Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "提示", 4000);
                ExceptionLog.LogError(ex.Message, ex);
            }

        }

        private void btnDbSet_Click(object sender, EventArgs e)
        {
            FrmDbOptions frm = new FrmDbOptions(true);
            frm.ShowInTaskbar = false;
            frm.Show();
        }

        private async Task CreateToDbByShpAsync(string shpPath, bool IsOverload, int SRID)
        {
            #region
            //读取系统配置空间库(判断是否为sde)
            //if(SpatialDbHelper.IsSdeDb())
            //{
            //    SpatialDbHelper.GetSdeConfig();
            //}
            #endregion
            SubDocument.SubDocText = "";
            DirectoryInfo dirInfo = new DirectoryInfo(shpPath);
            FileInfo[] filesInfo = dirInfo.GetFiles("*.shp", SearchOption.TopDirectoryOnly);
            if (filesInfo.Length == 0)
            {
                AutoCloseMsgBox.Show("未查找到矢量数据文件!", "提示", 2000);
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            SubDocument.SubDocText = "开始执行shp数据入库\r\n";
            List<Task> tasks = new List<Task>();
            AdoDatabase adoDb = CommonVariables.SpatialdbConn.ConfigDatabase;
            foreach (FileInfo fileInfo in filesInfo)
            {
                //await Task.Delay(100);
                string fileName = fileInfo.FullName;
                string shpName = Path.GetFileNameWithoutExtension(fileName);
                string shpFile = shpPath + "\\" + shpName;
                tasks.Add(Task.Run(() =>
                {
                    CreateToDbByShpInVoke(stopwatch, shpFile, shpName, adoDb, IsOverload, SRID);
                }));
            }
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            SubDocument.SubDocText += $"执行完毕,耗时:" +
                Convert.ToSingle(stopwatch.ElapsedMilliseconds / 1000.0) + "秒\r\n";

        }

        private void CreateToDbByShpInVoke(Stopwatch sw, string shpFile, string shpName,
             AdoDatabase adoDb, bool IsOverload, int SRID)
        {
            Action action = () =>
            {
                //Task<bool> IsInput = ShpToDbHelper.CreateLayer(shpFile, shpName, adoDb, IsOverload, SRID);
                bool IsInput = ShpToDbHelper.CreateLayer(shpFile, shpName, adoDb, IsOverload, SRID);
                if (IsInput)
                {
                    SubDocument.SubDocText += $"{shpName}已导入!\r\n";
                    SubDocument.SubDocText += shpName + "导入时间:" +
                    Convert.ToSingle(sw.ElapsedMilliseconds / 1000.0) + "秒\r\n";
                }
                else
                {
                    SubDocument.SubDocText += $"{shpName}导入失败!\r\n";
                }
            };

            this.Invoke(action);
        }

    }
}
