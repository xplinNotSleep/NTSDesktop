using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 窗体相关操作类
    /// </summary>
    public class WindowsGet
    {
        /// <summary>
        /// 取得当前鼠标在控件上的位置
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static System.Drawing.Point GetMousePosInControl(int hwnd)
        {
            Win32APIs.POINTAPI pt = new Win32APIs.POINTAPI();
            Win32APIs.RECT r = new Win32APIs.RECT();
            Win32APIs.WinAPI.GetCursorPos(ref pt);
            Win32APIs.WinAPI.GetWindowRect(hwnd, ref r);
            System.Drawing.Point pt2 = new System.Drawing.Point();
            pt2.X = pt.x - r.Left;
            pt2.Y = pt.y - r.Top;

            return pt2;
        }

        /// <summary>
        /// 取得系统的主窗口
        /// </summary>
        /// <returns></returns>
        public static Form GetMainForm()
        {
            for (int i = 0; i <= Application.OpenForms.Count - 1; i++)
            {
                if (Application.OpenForms[i].TopLevel)
                    return Application.OpenForms[i];
            }
            return null;
        }

        /// <summary>
        /// 根据程序集和类名创建对象
        /// </summary>
        /// <param name="fileName">不带路径的文件名</param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateObject(string fileName, string className)
        {
            string path = Application.StartupPath;
            string fn = path + "\\" + fileName;
            if (System.IO.File.Exists(fn) == false)
            {
                fn = path + "\\bin\\" + fileName;
                if (System.IO.File.Exists(fn) == false)
                    return null;
            }
            
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(fn);
            if (asm == null)
                return null;
            return asm.CreateInstance(className);
        }
    }
}
