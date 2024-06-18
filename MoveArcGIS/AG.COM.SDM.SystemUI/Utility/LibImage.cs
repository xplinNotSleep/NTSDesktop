using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.SystemUI.Utility
{
    public class LibImage
    {
        /// <summary>
        /// 从GDI位图的句柄处创建位图对象
        /// </summary>
        /// <param name="gdi32">GDI位图句柄值</param>
        /// <returns>返回位图对象</returns>
        public static Image GetImageFromHbitmap(int gdi32)
        {
            if (gdi32 == 0) return null;

            IntPtr pIntPtr = new IntPtr(gdi32);
            //从句柄创建Bitmap
            Bitmap pBitmap = System.Drawing.Bitmap.FromHbitmap(pIntPtr);
            //使背景色透明
            pBitmap.MakeTransparent();

            return pBitmap;
        }
    }
}
