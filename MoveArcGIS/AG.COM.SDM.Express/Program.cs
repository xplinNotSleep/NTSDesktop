using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Plugins.Logger;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;

namespace AG.COM.SDM.Express
{
    static class Program
    {
        public static bool Restart = false;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //SplashScreen.Show(typeof(SplashForm));
            LogHelper.Info("系统正在初始化");

            try
            {
                //处理未捕获的异常   
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常   
                Application.ThreadException += Application_ThreadException;
                //处理非UI线程异常   
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.Skins.SkinManager.EnableMdiFormSkins();


                //初始化系统参数
                CommonVariables.Init();
                Restart = pRestart();
                if (Restart)
                {
                    SaveRestart("0");
                }
                //SplashScreen.Close();
                FormLogin tLogfrm = new FormLogin();
                //tLogfrm.TopMost = true;
                if (tLogfrm.ShowDialog() == DialogResult.OK)
                {
                    tLogfrm.TopMost = false;
                    //重新打开登录界面显示加载信息
                    tLogfrm.Show();
                    Application.DoEvents();
                    tLogfrm.Activate();
                    //写入日志
                    MyLogger.GetInstance().WriteMessage("登录成功", "登录");

                    //初始化系统主界面
                    WinMain frmMain = new WinMain();
                    frmMain.CustomInitialize(tLogfrm);

                    //关闭消息窗体
                    (tLogfrm as Form).Close();

                    Application.Run(frmMain);

                }

            }
            catch (Exception ex)
            {
                var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";

                var str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                                           ex.GetType().Name, ex.Message, ex.StackTrace);

                WriteLog(str);
                SplashScreen.Close();
                MessageBox.Show(ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            finally
            {
                
            }
        }
        private static bool pRestart()
        {
            string strUserFile = CommonConstString.STR_ConfigPath + "\\Restart.txt";
            if (!File.Exists(strUserFile)) return false;
            using (StreamReader streamReader = new StreamReader(strUserFile))
            {
                return streamReader.ReadLine() == "1" ? true : false;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private static void SaveRestart(string p)
        {
            //保存加载的文档路径，以便下次启动时自动加载
            string strUserFile = CommonConstString.STR_ConfigPath + "\\Restart.txt";

            using (StreamWriter sw = File.CreateText(strUserFile))
            {
                sw.WriteLine(p);
            }
        }

        /// <summary>
        ///错误弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var error = e.Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            WriteLog(str);
            MessageBox.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Environment.Exit(0);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var error = e.ExceptionObject as Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var str = error != null ? string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace) : string.Format("Application UnhandledError:{0}", e);

            WriteLog(str);
            MessageBox.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Environment.Exit(0);
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="str"></param>
        static void WriteLog(string str)
        {
            LogHelper.Error(str);
        }

    }

    public static class StopwatchHelper
    {
        public static void ExeuteEx(this Action action, string actionname)
        {
            LogHelper.Info($"{actionname}方法开始");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            action.Invoke();
            sw.Stop();
            LogHelper.Info($"{actionname}方法耗时:{sw.ElapsedMilliseconds}");
        }
    }

}