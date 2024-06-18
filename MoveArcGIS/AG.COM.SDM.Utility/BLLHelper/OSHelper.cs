using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 操作系统相关帮助类
    /// </summary>
    public class OSHelper
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

        /// <summary>
        /// 是否64位
        /// </summary>
        /// <returns></returns>
        public static bool Is64Bit()
        {
            bool retVal;

            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

            return retVal;
        }

        /// <summary>
        /// 获取操作系统版本
        /// </summary>
        /// <returns></returns>
        public static OSVersion GetOSVersion()
        {
            OperatingSystem os = System.Environment.OSVersion;      
            switch (os.Platform)
            {
                //case PlatformID.Win32Windows:
                //    switch (os.Version.Minor)
                //    {
                //        case 0: osName = "Windows 95"; break;
                //        case 10: osName = "Windows 98"; break;
                //        case 90: osName = "Windows ME"; break;
                //    }
                //    break;
                case PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        //case 3: osName = "Windws NT 3.51"; break;
                        //case 4: osName = "Windows NT 4"; break;
                        case 5: if (os.Version.Minor == 1)
                            {
                                return OSVersion.XP;
                            }
                            else if (os.Version.Minor == 2)
                            {
                                return OSVersion.Server2003;
                            }
                            break;
                        case 6: if (os.Version.Minor == 0)
                            {
                                return OSVersion.Vista;
                            }
                            else if (os.Version.Minor == 1)
                            {
                                return OSVersion.Windows7;
                            }
                            else if (os.Version.Minor == 2)
                            {
                                return OSVersion.Windows8;
                            }
                            break;
                    }
                    break;
            }
            return OSVersion.Unknown;
        }
    }

    /// <summary>
    /// 操作系统类型
    /// </summary>
    public enum OSVersion
    {
        XP,
        Windows8,
        Windows7,
        Vista,
        Server2003,
        Windows10,
        Windows11,
        Unknown
    }
}
