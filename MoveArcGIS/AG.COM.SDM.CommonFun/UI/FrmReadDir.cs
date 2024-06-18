using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;

namespace AG.COM.SDM.CommonFun
{
    public partial class FrmReadDir : DockDocument
    {
        private IHookHelper m_hookHelper = new HookHelper();
        string m_selPath = "";

        public FrmReadDir()
        {
            InitializeComponent();
        }

        public FrmReadDir(IHookHelper hookHelper)
        {
            m_hookHelper = hookHelper;
            InitializeComponent();
        }

        /// <summary>
        /// 开始检索文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFold.Text == "" || m_selPath == "")
                {
                    AutoCloseMsgBox.Show("未选择文件夹!", "提示", 1500);
                    return;
                }
                DirectoryInfo dirInfo = new DirectoryInfo(m_selPath);
                string searchPattern = "";
                if(cbFileType.Text ==""||cbFileType.Text=="any")
                {
                    searchPattern = "*";
                }
                else
                {
                    searchPattern = "*." + cbFileType.Text;
                }
                FileInfo[] fileInfo = dirInfo.GetFiles(searchPattern, SearchOption.AllDirectories);
                if(fileInfo.Length == 0)
                {
                    AutoCloseMsgBox.Show("未查询到符合文件!", "提示", 2000);
                    return;
                }
                //ProgressDialog progressDialog = new ProgressDialog();
                FrmTrackProgress progressDialog = new FrmTrackProgress();
                progressDialog.Text = $"读取文件夹{DateTime.Now.ToString("yyMMddHHmmss")}";
                progressDialog.MaxValue = fileInfo.Length;
                //开始封装成线程
                ManualResetEvent resetEvent = new ManualResetEvent(true);
                CancellationTokenSource tokenSource = new CancellationTokenSource();//新增取消操作类
                Action action = () =>
                {
                    ReadFiles(progressDialog, resetEvent, tokenSource, fileInfo, fileInfo.Length);
                };
                #region 这里新建一个嵌入主窗体的进度条
                //m_hookHelper.DockDocumentService.AddDockDocument
                //    (progressDialog.TabText, progressDialog, 
                //    DockState.Document);

                #endregion
                progressDialog.Show();
                progressDialog.StartAction(action, resetEvent, tokenSource);

            }
            catch(Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "错误", 2000);
                ExceptionLog.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// 选择要遍历文件的文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            browserDialog.Description = "选择文件夹";
            if(browserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFold.Text = browserDialog.SelectedPath;
                m_selPath = browserDialog.SelectedPath;
            }
            else
            {
                return;
            }
        }

        #region 操作部分
        public void ReadFiles(FrmTrackProgress frmTrack, ManualResetEvent resetEvent,
        CancellationTokenSource tokenSource, FileInfo[] fileInfo, int countFiles)
        {
            try
            {
                int i = 0;
                // 遍历并输出每个文件的完整路径
                foreach (FileInfo file in fileInfo)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        throw new InvalidOperationException();
                    }
                    resetEvent.WaitOne();

                    Invoke(new Action(() =>
                    {
                        i++;
                        frmTrack.ProValue++;
                        frmTrack.SubMsg = file.FullName;
                        frmTrack.TotalMsg = "进度:" + i + "/" + countFiles;

                    }));
                    //await Task.Delay(200);
                    if(countFiles < 10000)
                    {
                        Thread.Sleep(100);
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }

                }
                frmTrack.Close();
                AutoCloseMsgBox.Show("可暂停进程遍历完成", "提示", 2000);
            }
            catch (InvalidOperationException)
            {
                Invoke(new Action(() =>
                {
                    AutoCloseMsgBox.Show("可暂停进程终止", "提示", 2000);
                }));
            }

        }

        /// <summary>
        /// 委托的方法
        /// </summary>
        /// <param name="frmTrack"></param>
        public void ReadFilesWithDock(ProgressDialog frmTrack, ManualResetEvent resetEvent,
        CancellationTokenSource tokenSource, FileInfo[] fileInfo, int countFiles)
        {
            try
            {
                int i = 0;
                // 遍历并输出每个文件的完整路径
                foreach (FileInfo file in fileInfo)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        throw new InvalidOperationException();
                    }
                    resetEvent.WaitOne();

                    Invoke(new Action(() =>
                    {
                        i++;
                        frmTrack.ProValue++;
                        frmTrack.SubMsg = file.FullName;
                        frmTrack.TotalMsg = "进度:" + i + "/" + countFiles;

                    }));
                    //await Task.Delay(200);
                    Thread.Sleep(10000/countFiles);

                }
                frmTrack.Close();
                AutoCloseMsgBox.Show("可暂停进程遍历完成", "提示", 2000);
            }
            catch (InvalidOperationException)
            {
                Invoke(new Action(() =>
                {
                    AutoCloseMsgBox.Show("可暂停进程终止", "提示", 2000);
                }));
            }

        }

        #region  添加进度条固定到DockDocument中
        //public void AddProgressBar(DockDocument progressDialog)
        //{
        //    if(!m_hookHelper.DockDocumentService.ContainsDocument("读取文件夹"))
        //    {
        //        m_hookHelper.DockDocumentService.AddDockDocument
        //            ("读取文件夹", progressDialog, DockState.Document);
        //    }
        //    else
        //    {
        //        DockDocument progressBar = m_hookHelper.DockDocumentService.
        //            GetDockDocument("读取文件夹");
        //        m_hookHelper.DockDocumentService.AddDockDocument
        //            ($"读取文件夹{DateTime.Now.ToShortTimeString()}", progressDialog, DockState.Document);
        //    }
            
        //}
        #endregion

        #endregion

    }
}
