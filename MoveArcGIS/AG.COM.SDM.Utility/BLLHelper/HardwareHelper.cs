using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 硬件帮助类
    /// </summary>
    public class HardwareHelper
    {
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
    }
}
