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
    public partial class FrmShpImportToDb : DockDocument
    {
        private IHookHelper m_hookHelper;
        private string m_ShpPath = "";
        private SubDocument SubDocument;

        public FrmShpImportToDb(IHookHelper hookHelper)
        {
            m_hookHelper = hookHelper;
            SubDocument = m_hookHelper.DockDocumentService.GetDockDocument
                    (Convert.ToString(EnumDocumentType.SubDocument)) as SubDocument; ;
            
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
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

        /// <summary>
        /// 单线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
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
                CreateToDbByShp(m_ShpPath, IsOverload, 4490);
                
            }
            catch (Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "提示", 4000);
                ExceptionLog.LogError(ex.Message, ex);
            }

        }

        /// <summary>
        /// 多线程(程序未关闭时操作后台运行，不过速度很慢)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnImportThread_Click(object sender, EventArgs e)
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
                CreateToDbByShpAsync(m_ShpPath, IsOverload, 4490);

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


        private async void CreateToDbByShpAsync(string shpPath, bool IsOverload, int SRID)
        {
            SubDocument.SubDocText = "";
            DirectoryInfo dirInfo = new DirectoryInfo(shpPath);
            FileInfo[] filesInfo = dirInfo.GetFiles("*.shp", SearchOption.TopDirectoryOnly);
            if (filesInfo.Length == 0)
            {
                AutoCloseMsgBox.Show("未查找到矢量数据文件!", "提示", 2000);
                return;
            }
            SubDocument.SubDocText = "开始执行shp数据入库\r\n";
            FrmTrackNoPause trackThread = new FrmTrackNoPause();
            trackThread.Text = "执行shp数据入库";
            trackThread.Show();
            Stopwatch sw = Stopwatch.StartNew();
            AdoDatabase adoDb = CommonVariables.SpatialdbConn.ConfigDatabase;
            foreach (FileInfo fileInfo in filesInfo)
            {
                if(trackThread.IsBreak)
                {
                    break;
                }
                string fileName = fileInfo.FullName;
                string shpName = Path.GetFileNameWithoutExtension(fileName);
                string shpFile = shpPath + "\\" + shpName;
                trackThread.TotalMsg = $"正在识别路径{shpFile}";
                await Task.Delay(100);
                ShpToDbHelper.CreateLayer(shpFile, shpName, adoDb,
                    IsOverload, SRID, trackThread);
                SubDocument.SubDocText += $"{shpName}导入完成!\r\n";
                SubDocument.SubDocText += shpName + "导入时间:" +
                Convert.ToSingle(sw.ElapsedMilliseconds / 1000.0) + "秒\r\n";
                
                #region 原来执行操作的方法返回的是bool类型
                //if (IsInput.Result)
                //{
                //    SubDocument.SubDocText += $"{shpName}已导入!\r\n";
                //    SubDocument.SubDocText += shpName + "导入时间:" +
                //    Convert.ToSingle(sw.ElapsedMilliseconds / 1000.0) + "秒\r\n";
                //}
                //else
                //{
                //    SubDocument.SubDocText += $"{shpName}导入失败!\r\n";
                //}
                #endregion
            }
            sw.Stop();
            trackThread.Close();

        }

        private void CreateToDbByShp(string shpPath, bool IsOverload, int SRID)
        {
            SubDocument.SubDocText = "";
            DirectoryInfo dirInfo = new DirectoryInfo(shpPath);
            FileInfo[] filesInfo = dirInfo.GetFiles("*.shp", SearchOption.TopDirectoryOnly);
            if (filesInfo.Length == 0)
            {
                AutoCloseMsgBox.Show("未查找到矢量数据文件!", "提示", 2000);
                return;
            }
            SubDocument.SubDocText = "开始执行shp数据入库\r\n";
            FrmDataOperateProgress trackThread = new FrmDataOperateProgress();
            trackThread.Text = "执行shp数据入库";
            trackThread.Show();
            trackThread.TotalMaxValue = filesInfo.Length;
            Application.DoEvents();
            Stopwatch sw = Stopwatch.StartNew();
            AdoDatabase adoDb = CommonVariables.SpatialdbConn.ConfigDatabase;
            int fileId = 0;
            
            foreach (FileInfo fileInfo in filesInfo)
            {
                if (trackThread.IsBreak)
                {
                    break;
                }
                fileId++;
                Stopwatch sw1 = Stopwatch.StartNew();
                string fileName = fileInfo.FullName;
                string shpName = Path.GetFileNameWithoutExtension(fileName);
                string shpFile = shpPath + "\\" + shpName;
                trackThread.TotalMsg = $"正在导入{shpName}({fileId}/{filesInfo.Length})";
                trackThread.TotalProValue++;
                bool IsInput = ShpToDbHelper.IsCreateLayer(shpFile, shpName, adoDb,
                    IsOverload, SRID, trackThread);
                if(IsInput)
                {
                    SubDocument.SubDocText += $"{shpName}导入完成!\r\n";
                    SubDocument.SubDocText += shpName + "导入时间:" +
                    Convert.ToSingle(sw1.ElapsedMilliseconds / 1000.0) + "秒\r\n";
                }
                else
                {
                    SubDocument.SubDocText += $"{shpName}导入失败!\r\n";
                }

            }
            SubDocument.SubDocText += $"矢量数据入库完成!\r\n";
            SubDocument.SubDocText += "总执行时间:" +
            Convert.ToSingle(sw.ElapsedMilliseconds / 1000.0) + "秒\r\n";
            sw.Stop();
            trackThread.Close();

        }

        #region  委托方法
        //private void CreateToDbByShpInVoke(Stopwatch sw, string shpFile, string shpName,
        //     AdoDatabase adoDb, bool IsOverload, int SRID)
        //{
        //    Action action = () =>
        //    {
        //        //Task<bool> IsInput = ShpToDbHelper.CreateLayer(shpFile, shpName, adoDb, IsOverload, SRID);
        //        bool IsInput = ShpToDbHelper.CreateLayer(shpFile, shpName, adoDb, IsOverload, SRID);
        //        if (IsInput)
        //        {
        //            SubDocument.SubDocText += $"{shpName}已导入!\r\n";
        //            SubDocument.SubDocText += shpName + "导入时间:" +
        //            Convert.ToSingle(sw.ElapsedMilliseconds / 1000.0) + "秒\r\n";
        //        }
        //        else
        //        {
        //            SubDocument.SubDocText += $"{shpName}导入失败!\r\n";
        //        }
        //    };

        //    this.Invoke(action);
        //}

        #endregion

    }
}
