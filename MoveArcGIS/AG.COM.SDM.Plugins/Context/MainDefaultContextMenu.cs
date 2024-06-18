using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Plugins.Common;
using AG.COM.SDM.Plugins.Demo;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// 地图文档右键快捷菜单
    /// </summary>
    public sealed class MainDefaultContextMenu : BaseContextMenu
    {
        private IHookHelper m_hookHelper = new HookHelper();
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MainDefaultContextMenu()
            : base()
        {
            this.AddItem(new CmdCommand(), -1, false, ToolStripItemDisplayStyle.ImageAndText);
            this.AddItem(new CmdTheme2010Black(), -1, false, ToolStripItemDisplayStyle.ImageAndText);

        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            //this.m_hookHelper.Hook = hook;
        }

        #region 继承的BaseContextMenu中的方法

        ///// <summary>
        ///// 转换对象为ToolStripItem
        ///// </summary>
        ///// <param name="item">菜单对象</param>
        ///// <returns>返回ToolStripItem对象</returns>
        //protected override ToolStripItem GetToolStripItem(object item)
        //{
        //    ToolStripItem toolItem = null;

        //    ICommand tCommand = item as ICommand;
        //    if (tCommand != null)
        //    {
        //        //实例化菜单项对象
        //        toolItem = new ToolStripMenuItem();
        //        toolItem.Name = tCommand.Name;
        //        toolItem.Text = tCommand.Caption;
        //        toolItem.Image = this.GetImageFromHbitmap(tCommand.Bitmap);
        //        toolItem.ToolTipText = tCommand.Tooltip;
        //        toolItem.Tag = item;

        //        //ToolStripItem单击事件的委托处理
        //        toolItem.Click += new EventHandler(ToolStripItemClick);
        //    }
        //    else if (item is ToolStripItem)
        //    {
        //        toolItem = item as ToolStripItem;
        //        toolItem.Click += new EventHandler(ToolStripItemClick);
        //    }

        //    return toolItem;
        //}

        ///// <summary>
        ///// ToolStripItem单击事件处理方法
        ///// </summary>
        ///// <param name="sender">事件的发起者</param>
        ///// <param name="e">事件参数</param>
        //private void ToolStripItemClick(object sender, EventArgs e)
        //{
        //    ToolStripItem toolItem = sender as ToolStripItem;
        //    if (toolItem.Tag != null)
        //    {
        //        //查询引用接口

        //        ICommand tCommand = toolItem.Tag as ICommand;
        //        if (tCommand != null)
        //        {
        //            try
        //            {
        //                //单击处理
        //                tCommand.OnClick();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(string.Format("[{0}]单击出错,"+ex.Message, toolItem.Text));
        //            }

        //        }

        //    }
        //}

        ///// <summary>
        ///// 从GDI位图的句柄处创建位图对象
        ///// </summary>
        ///// <param name="gdi32">GDI位图句柄值</param>
        ///// <returns>返回位图对象</returns>
        //private Image GetImageFromHbitmap(int gdi32)
        //{
        //    if (gdi32 == 0) return null;

        //    IntPtr pIntPtr = new IntPtr(gdi32);
        //    //从句柄创建Bitmap
        //    Bitmap pBitmap = System.Drawing.Bitmap.FromHbitmap(pIntPtr);
        //    //使背景色透明
        //    pBitmap.MakeTransparent();

        //    return pBitmap;
        //}

        #endregion

    }
}
