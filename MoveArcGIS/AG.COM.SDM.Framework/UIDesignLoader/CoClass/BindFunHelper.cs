using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 功能绑定帮助类
    /// </summary>
    public class BindFunHelper
    {
        /// <summary>
        /// 获取图像
        /// </summary>
        /// <param name="objInstance">实例对象</param>
        public static Bitmap GetBitmap(object objInstance)
        {
            if (objInstance == null) return null;

            ICommand pCommand = objInstance as ICommand;
            if (pCommand == null) return null;


            if (pCommand.Bitmap != 0)
            {
                IntPtr pIntPtr = new IntPtr(pCommand.Bitmap);
                Bitmap pBitmap = Bitmap.FromHbitmap(pIntPtr);   //从句柄创建Bitmap 
                pBitmap.MakeTransparent();                      //使背景色透明

                return pBitmap;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取ICommand的Icon（RibbonItem）
        /// </summary>
        /// <param name="objInstance"></param>
        /// <param name="iconSize"></param>
        /// <returns></returns>
        public static Image GetRibbonItemIcon(object objInstance, int iconSize)
        {
            Image result = null;
            //如果实现了IUseImage，则优先从IUseImage取图标，其次再到ICommand.Bitmap
            if (objInstance is IUseImage)
            {
                //图标有16和32两种大小，根据设置的图标大小确定用哪个图标
                try
                {
                    if (iconSize <= 18)
                    {
                        result = (objInstance as IUseImage).Image16;
                    }
                    else
                    {
                        result = (objInstance as IUseImage).Image32;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            //与旧框架（QIOS）兼容，还支持IUseIcon
            //如果实现了IUseIcon，则优先从IUseIcon取图标，其次再到ICommand.Bitmap
            else if (objInstance is IUseIcon)
            {
                //图标有16和32两种大小，根据设置的图标大小确定用哪个图标
                try
                {
                    if (iconSize <= 18)
                    {
                        result = (objInstance as IUseIcon).Icon16.ToBitmap();
                    }
                    else
                    {
                        result = (objInstance as IUseIcon).Icon32.ToBitmap();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Bitmap tBitmap = BindFunHelper.GetBitmap(objInstance);
                result = tBitmap;             
            }
            return result;
        }
    }
}
