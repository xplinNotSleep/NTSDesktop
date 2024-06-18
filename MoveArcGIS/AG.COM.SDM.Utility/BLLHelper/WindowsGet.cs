using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// ������ز�����
    /// </summary>
    public class WindowsGet
    {
        /// <summary>
        /// ȡ�õ�ǰ����ڿؼ��ϵ�λ��
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
        /// ȡ��ϵͳ��������
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
        /// ���ݳ��򼯺�������������
        /// </summary>
        /// <param name="fileName">����·�����ļ���</param>
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
