using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.LookAndFeel;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Dev皮肤帮助类
    /// </summary>
    public class DevThemeHelper
    {
        /// <summary>
        /// 改变使用的皮肤
        /// </summary>
        /// <param name="themeName"></param>
        public static void ChangeTheme(string tempPath,string configPath,string themeName, string oldSkinName)
        {
            if (string.IsNullOrEmpty(themeName)) return;

            UserLookAndFeel defaultLF = UserLookAndFeel.Default;
            defaultLF.SkinName = themeName;

            ResourceHelper tResourceHelper = new ResourceHelper(configPath, tempPath);
            tResourceHelper.SetObject(oldSkinName, themeName);
            tResourceHelper.Save();

            //CommonVariables.CurrentSkinName = themeName;
            ////把当前颜色设置保存到系统配置文件           
            //ResourceHelper tResourceHelper = new ResourceHelper(CommonVariables.ConfigFile);
            //tResourceHelper.SetObject(CommonConstString.SkinKeyInConfig, CommonVariables.CurrentSkinName);
            //tResourceHelper.Save();
        }
    
    }
}
