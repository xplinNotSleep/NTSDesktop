using System;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Logger;

namespace AG.COM.SDM.Manager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                //初始化系统参数
                CommonVariables.Init();

                Application.Run(new FormMain());
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message, ex);
                ExceptionLog.LogError(ex.Message, ex);
            }
            
        }
    }
}