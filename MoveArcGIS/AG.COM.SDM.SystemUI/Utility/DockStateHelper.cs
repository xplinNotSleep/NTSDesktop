using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 框架DockState帮助类
    /// </summary>
    public class DockStateHelper
    {
        /// <summary>
        /// DockState转Dev的DockingStyle
        /// </summary>
        /// <param name="dockState"></param>
        /// <returns></returns>
        public static DockingStyle DockStateToDockingStyle(DockState dockState)
        {
            switch (dockState)
            {
                case DockState.Bottom:
                    return DockingStyle.Bottom;
                case DockState.Document:
                    return DockingStyle.Fill;
                case DockState.Float:
                    return DockingStyle.Float;
                case DockState.Left:
                    return DockingStyle.Left;
                case DockState.Right:
                    return DockingStyle.Right;
                case DockState.Top:
                    return DockingStyle.Top;
                default:
                    throw new Exception("未知的DockState枚举值" + dockState.ToString());
            }
        }

        /// <summary>
        /// DockingStyle转DockState
        /// </summary>
        /// <param name="dockingStyle"></param>
        /// <returns></returns>
        public static DockState DockingStyleToDockState(DockingStyle dockingStyle)
        {
            switch (dockingStyle)
            {
                case DockingStyle.Bottom:
                    return DockState.Bottom;
                case DockingStyle.Fill:
                    return DockState.Document;
                case DockingStyle.Float:
                    return DockState.Float;
                case DockingStyle.Left:
                    return DockState.Left;
                case DockingStyle.Right:
                    return DockState.Right;
                case DockingStyle.Top:
                    return DockState.Top;
                default:
                    throw new Exception("未知的DockingStyle枚举值" + dockingStyle.ToString());
            }
        }
    }
}
