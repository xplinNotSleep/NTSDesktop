using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AG.COM.SDM.Utility
{
    public class LimitDateHelper
    {
        /// <summary>
        /// <summary>
        /// 系统当前许可信息保存文件路径
        /// </summary>
        private static readonly string SystemUseConfigFilePath = CommonConstString.STR_ConfigPath + "\\SystemUseConfig.resx";

        /// <summary>
        /// 许可申请协议书模板
        /// </summary>
        public static readonly string ApplyTemplatePath = CommonConstString.STR_TemplatePath + "\\软件授权使用协议书模板.doc";

        /// <summary>
        /// 许可申请界面协议内容
        /// </summary>
        public static readonly string ApplyFormContentPath = CommonConstString.STR_TemplatePath + "\\软件授权使用协议书注册界面内容.rtf";

        #region
        ///// <summary>
        ///// 判断软件是否可用
        ///// </summary>
        ///// <returns>如果软件没到过期时间则返回true,否则返回false</returns>
        //public static bool IsSoftwareValid()
        //{
        //    RegistryKey lm = Registry.LocalMachine;
        //    RegistryKey s;
        //    s = lm.OpenSubKey("SOFTWARE", false);
        //    if (s != null)
        //    {
        //        s = s.OpenSubKey(LimitDateHelperString.SoftwareName, false);
        //        if (s != null)
        //        {
        //            //检查并读取许可信息
        //            if (CheckLicenseLevelFromObj(s.GetValue(LimitDateHelperString.LicenseLevelRegItem)) == false)
        //            {
        //                MessageBox.Show("未能成功读取许可信息，请重新注册许可。", "许可错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                lm.Close();
        //                return false;
        //            }
        //            else
        //            {
        //                //非无限制许可，验证时间
        //                if (SystemInfo.LicenseUnlimited == false)
        //                {
        //                    TimeSpan span1 = (TimeSpan)(DateTime.Now - SystemInfo.LastDate);
        //                    if (span1.Milliseconds <= 0)
        //                    {
        //                        MessageBox.Show("当前时间早于上次使用时间，可能是操作系统时间有误。", "许可错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        lm.Close();
        //                        return false;
        //                    }

        //                    TimeSpan span2 = (TimeSpan)(SystemInfo.LimitDate - DateTime.Now);

        //                    if (span2.Milliseconds <= 0)
        //                    {
        //                        MessageBox.Show("许可已过期。许可有效期至：" + SystemInfo.LimitDate.ToString("yyyy年MM月dd日"), "许可错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        lm.Close();

        //                        return false;
        //                    }
        //                }

        //                ResourceHelper tResourceHelperSystem = new ResourceHelper(LimitDateHelper.SystemUseConfigFilePath);

        //                //保存上次使用时间，就是这次的时间
        //                tResourceHelperSystem.SetString(LimitDateHelperString.LastTimeKey, DateTime.Now.ToString("yyyy-MM-dd"));

        //                tResourceHelperSystem.Save();

        //                lm.Close();

        //                return true;
        //            }
        //        }
        //    }

        //    lm.Close();

        //    MessageBox.Show("未能成功读取许可信息，请重新注册许可。", "许可错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    return false;
        //}

        ///// <summary>
        ///// 注册许可
        ///// </summary>
        ///// <param name="licPath"></param>
        //public static void RegisterLic(string licPath)
        //{
        //    ResourceHelper tResourceHelper = new ResourceHelper(licPath);
        //    //获取当前机器的机器码
        //    string machineCodeThis = HardwareHelper.GetDiskVolumeSerialNumber();
        //    if (string.IsNullOrEmpty(machineCodeThis))
        //    {
        //        throw new Exception("无法获取机器码，请联系系统管理员");
        //    }
        //    //获取许可记录的机器码
        //    string machineCodeLic = tResourceHelper.GetString(LimitDateHelperString.MachineCodeKey);
        //    //判断机器码是否一样，一个许可只能给一台机用
        //    if (machineCodeThis != machineCodeLic)
        //    {
        //        throw new Exception("此许可不能用于当前电脑，请重新申请许可");
        //    }

        //    Security security = new Security();

        //    RegistryKey lm = Registry.LocalMachine;
        //    RegistryKey s;

        //    s = lm.OpenSubKey("SOFTWARE", true);

        //    s = s.CreateSubKey(LimitDateHelperString.SoftwareName);

        //    //用guid把保存在系统的许可信息和本机（注册表）绑定起来
        //    string guid = Guid.NewGuid().ToString();
        //    //guid写入注册表
        //    s.SetValue(LimitDateHelperString.LicenseLevelRegItem, guid, RegistryValueKind.String);

        //    ResourceHelper tResourceHelperSystem = new ResourceHelper(SystemUseConfigFilePath);
        //    //guid写入系统许可信息
        //    tResourceHelperSystem.SetString(LimitDateHelperString.SystemLicGuidKey, guid);

        //    //上次使用时间
        //    tResourceHelperSystem.SetString(LimitDateHelperString.LastTimeKey, DateTime.Now.ToString("yyyy-MM-dd"));
        //    //使用期限
        //    tResourceHelperSystem.SetString(LimitDateHelperString.LimitTimeKey, tResourceHelper.GetString(LimitDateHelperString.LimitTimeKey));
        //    //许可级别名称
        //    tResourceHelperSystem.SetString(LimitDateHelperString.LicenseLevelNameKey, tResourceHelper.GetString(LimitDateHelperString.LicenseLevelNameKey));
        //    //是否无限制许可
        //    tResourceHelperSystem.SetString(LimitDateHelperString.UnlimitedKey, tResourceHelper.GetString(LimitDateHelperString.UnlimitedKey));
        //    //有权限的插件类
        //    tResourceHelperSystem.SetObject(LimitDateHelperString.PluginClassesKey, tResourceHelper.GetObject(LimitDateHelperString.PluginClassesKey));
        //    //插件类中文名
        //    tResourceHelperSystem.SetObject(LimitDateHelperString.PluginCnNameKey, tResourceHelper.GetObject(LimitDateHelperString.PluginCnNameKey));

        //    tResourceHelperSystem.Save();

        //    lm.Close();
        //}

        ///// <summary>
        ///// 检查并读取许可信息
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns>false=未能成功读取许可</returns>
        //public static bool CheckLicenseLevelFromObj(object obj)
        //{
        //    if (File.Exists(SystemUseConfigFilePath) == false) return false;

        //    //清空有权限的插件类名称集合
        //    SystemInfo.HasLicPlugins = null;

        //    //注册表记录的GUID
        //    string regeditGuid = Convert.ToString(obj);
        //    //读取保存在系统的许可信息
        //    ResourceHelper tResourceHelper = new ResourceHelper(SystemUseConfigFilePath);
        //    //系统许可信息的Guid
        //    string systemLicGuid = tResourceHelper.GetString(LimitDateHelperString.SystemLicGuidKey);

        //    //如果两个Guid统一，可判断系统保存的许可信息是这台机用的，否则反之
        //    if (!string.IsNullOrEmpty(systemLicGuid) && regeditGuid == systemLicGuid)
        //    {
        //        //许可有效期
        //        string strLimitDate = tResourceHelper.GetString(LimitDateHelperString.LimitTimeKey);
        //        DateTime limitDate = Convert.ToDateTime(strLimitDate);
        //        SystemInfo.LimitDate = limitDate;

        //        //上次使用时间
        //        string strLastDate = tResourceHelper.GetString(LimitDateHelperString.LastTimeKey);
        //        DateTime lastDate = Convert.ToDateTime(strLastDate);
        //        SystemInfo.LastDate = lastDate;

        //        //许可级别名称
        //        string licenseLevelName = tResourceHelper.GetString(LimitDateHelperString.LicenseLevelNameKey);
        //        SystemInfo.LicenseLevelName = licenseLevelName;
        //        //读取是否无限制许可
        //        string strUnlimited = tResourceHelper.GetString(LimitDateHelperString.UnlimitedKey);
        //        if (!string.IsNullOrEmpty(strUnlimited))
        //        {
        //            SystemInfo.LicenseUnlimited = Convert.ToBoolean(strUnlimited);
        //        }
        //        //有权限的插件类
        //        List<string> tPluginClasses = tResourceHelper.GetObject(LimitDateHelperString.PluginClassesKey) as List<string>;
        //        if (tPluginClasses != null)
        //        {
        //            SystemInfo.HasLicPlugins = tPluginClasses;
        //        }
        //        //插件类中文名
        //        List<LicensePluginTag> tLicensePluginTags = tResourceHelper.GetObject(LimitDateHelperString.PluginCnNameKey) as List<LicensePluginTag>;
        //        if (tLicensePluginTags != null)
        //        {
        //            SystemInfo.HasLicPluginTags = tLicensePluginTags;
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }

}
