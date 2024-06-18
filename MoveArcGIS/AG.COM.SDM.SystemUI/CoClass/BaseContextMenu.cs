using AG.COM.SDM.SystemUI.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 上下文菜单 基础类
    /// </summary>
    public class BaseContextMenu: IContextMenu
    {
        /// <summary>
        /// 右键快捷菜单
        /// </summary>
        protected ContextMenuStrip m_contextMenuStrip;
        /// <summary>
        /// 右键菜单图片
        /// </summary>
        protected Bitmap m_bitmap;
        /// <summary>
        /// 右键菜单显示文本
        /// </summary>
        protected string m_caption;
        protected string m_name;
        protected bool m_enabled=true;

        #region IContextMenu 成员
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseContextMenu()
        {
            m_contextMenuStrip = new ContextMenuStrip();
        }

        /// <summary>
        /// 获取或设置该项Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.m_name;
            }
        }

        /// <summary>
        /// 获取或设置上下文菜单图片
        /// </summary>
        public virtual int Bitmap
        {
            get
            {
                return this.m_bitmap.GetHbitmap().ToInt32();
            }
            set
            {
                //从GDI位图的句柄处创建位图
                this.m_bitmap = LibImage.GetImageFromHbitmap(value) as Bitmap;
            }
        }

        /// <summary>
        /// 获取或设置上下文菜单显示的名称
        /// </summary>
        public virtual string Caption
        {
            get
            {
                return this.m_caption;
            }
            set
            {
                this.m_caption = value;
            }
        }

        /// <summary>
        /// 获取当前对象的可用状态
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                return this.m_enabled;
            }
        }

        /// <summary>
        /// 确定集合是否包含指定关键字的项
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        /// <returns>如果集合中包含此项则返回 true,否则返回 false</returns>
        public virtual bool ContainsItem(string keyName)
        {
            return this.m_contextMenuStrip.Items.ContainsKey(keyName);
        }

        /// <summary>
        /// 获取上下文菜单
        /// </summary>
        public ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return this.m_contextMenuStrip;
            }
        }

        /// <summary>
        /// 根据关键字查找匹配的项
        /// </summary>
        /// <param name="keyName">关键字</param>
        /// <returns>如果找到返回该菜单项，否则返回null</returns>
        public virtual ToolStripItem GetItemByKeyName(string keyName)
        {
            ToolStripItem[] toolItems = this.m_contextMenuStrip.Items.Find(keyName, true);
            if (toolItems.Length > 0)
                return toolItems[0];
            else
                return null;
        }

        /// <summary>
        /// 在指定索引位置处插入菜单项
        /// </summary>
        /// <param name="item">菜单项对象 默认为继承ICommand接口的对象</param>
        /// <param name="index">如果不指定则为-1,否则指定插入位置</param>
        /// <param name="beginGroup">是否开始分组</param>
        /// <param name="style">菜单样式设置</param>
        public virtual void AddItem(object item, int index, bool beginGroup, ToolStripItemDisplayStyle style)
        {
            ToolStripItem toolItem = GetToolStripItem(item);

            if (toolItem != null)
            {
                if (this.m_contextMenuStrip.Items.Find(toolItem.Name, true).Length == 0)
                {
                    //设置显示样式
                    toolItem.DisplayStyle = style;

                    if (index > -1)
                    {
                        //确定指定索引是否在范围之内
                        if (index >= this.m_contextMenuStrip.Items.Count)
                            throw (new Exception(string.Format("[{0}] 指定索引不在范围之内!", index)));

                        //在指定索引位置处插入                       
                        if (beginGroup == true)
                        {
                            //添加分隔条
                            this.m_contextMenuStrip.Items.Insert(index, new ToolStripSeparator());
                            this.m_contextMenuStrip.Items.Insert(index + 1, toolItem);
                        }
                        else
                        {
                            this.m_contextMenuStrip.Items.Insert(index, toolItem);
                        }
                    }
                    else
                    {
                        //未指定索引位置
                        if (beginGroup == true)
                        {
                            //添加分隔条
                            this.m_contextMenuStrip.Items.Add(new ToolStripSeparator());
                        }

                        //添加上下文菜单项
                        this.m_contextMenuStrip.Items.Add(toolItem);
                    }
                }
                else
                {
                    throw (new Exception(string.Format("不能加载[{0}]项,该上下文菜单中已存在名称相同的项.请确认!", toolItem.Name)));
                }
            }
            else
            {
                throw (new Exception("[item] 类型为null,不能加载.请确认!"));
            }
        }

        /// <summary>
        /// 在指定位置处加载菜单项
        /// </summary>
        /// <param name="keyName">关键字</param>
        /// <param name="index">指定索引位置,如果不指定则为 -1</param>
        /// <param name="beginGroup">如果开始分组则为true,否则为false</param>
        /// <param name="subMenuItem">要加载的菜单项</param>
        public virtual void AddSubMenu(string keyName, int index, bool beginGroup, ToolStripItem subMenuItem)
        {
            ToolStripItem[] toolItems = this.m_contextMenuStrip.Items.Find(keyName, true);
            if (toolItems.Length > 0)
            {
                ToolStripMenuItem toolMenuItem = toolItems[0] as ToolStripMenuItem;

                if (index < 0)
                {
                    if (beginGroup == true)
                    {
                        //添加分隔条
                        toolMenuItem.DropDownItems.Add(new ToolStripSeparator());
                    }
                    //添加子菜单项
                    toolMenuItem.DropDownItems.Add(subMenuItem);
                }
                else
                {
                    if (beginGroup == true)
                    {
                        //添加分隔条
                        toolMenuItem.DropDownItems.Insert(index, new ToolStripSeparator());
                        //在指定位置处插入子菜单项
                        toolMenuItem.DropDownItems.Insert(index + 1, subMenuItem);
                    }
                    else
                    {
                        //在指定位置处插入子菜单项
                        toolMenuItem.DropDownItems.Insert(index, subMenuItem);
                    }
                }
            }
            else
            {
                throw (new Exception(string.Format("该上下文菜单中找不到指定关键字项的菜单.请确认!", keyName)));
            }
        }

        /// <summary>
        /// 在指定位置处加载菜单项
        /// </summary>
        /// <param name="keyName">关键字</param>
        /// <param name="index">指定索引位置,如果不指定则为 -1</param>
        /// <param name="beginGroup">如果开始分组则为true,否则为false</param>
        /// <param name="subMenuItem">要加载的菜单项</param>
        public virtual void AddSubMenu(string keyName, int index, bool beginGroup, object subMenuItem)
        {
            //转换对象为ToolStripItem对象
            ToolStripItem toolItem = GetToolStripItem(subMenuItem);

            ToolStripItem[] toolItems = this.m_contextMenuStrip.Items.Find(keyName, true);
            if (toolItems.Length > 0)
            {
                ToolStripMenuItem toolMenuItem = toolItems[0] as ToolStripMenuItem;

                if (index < 0)
                {
                    if (beginGroup == true)
                    {
                        //添加分隔条
                        toolMenuItem.DropDownItems.Add(new ToolStripSeparator());
                    }
                    //添加子菜单项
                    toolMenuItem.DropDownItems.Add(toolItem);
                }
                else
                {
                    if (beginGroup == true)
                    {
                        //添加分隔条
                        toolMenuItem.DropDownItems.Insert(index, new ToolStripSeparator());
                        //在指定位置处插入子菜单项
                        toolMenuItem.DropDownItems.Insert(index + 1, toolItem);
                    }
                    else
                    {
                        //在指定位置处插入子菜单项
                        toolMenuItem.DropDownItems.Insert(index, toolItem);
                    }
                }
            }
            else
            {
                throw (new Exception(string.Format("该上下文菜单中找不到指定关键字项的菜单.请确认!", keyName)));
            }
        }

        /// <summary>
        /// 在光标处显示弹出的上下文菜单
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="intPtr"></param>
        public virtual void PopupMenu(int X, int Y, IntPtr intPtr)
        {
            //返回当前与指定句柄关联的控件
            Control tControl = Control.FromHandle(intPtr);
            this.m_contextMenuStrip.Show(tControl, X, Y);
        }

        /// <summary>
        /// 相对指定的句柄控件处弹出上下文菜单(ArcEngine)
        /// </summary>
        /// <param name="X">X方向距离</param>
        /// <param name="Y">Y方向距离</param>
        /// <param name="hwndParent">指定控件的句柄</param>
        public virtual void PopupMenu(int X, int Y, int hwndParent)
        {
            IntPtr tIntPtr = new IntPtr(hwndParent);
            //返回当前与指定句柄关联的控件
            Control tControl = Control.FromHandle(tIntPtr);
            this.m_contextMenuStrip.Show(tControl, X, Y);
        }

        /// <summary>
        /// 移除指定索引处的菜单
        /// </summary>
        /// <param name="index">指定索引位置</param>
        public virtual void Remove(int index)
        {
            this.m_contextMenuStrip.Items.RemoveAt(index);
        }

        /// <summary>
        /// 移除指定关键字的项
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        public virtual void RemoveByKeyName(string keyName)
        {
            if (this.m_contextMenuStrip.Items.ContainsKey(keyName))
            {
                this.m_contextMenuStrip.Items.RemoveByKey(keyName);
            }
        }

        /// <summary>
        /// 移除上下文菜单中的所有项
        /// </summary>
        public virtual void RemoveAll()
        {
            //移除所有项
            this.m_contextMenuStrip.Items.Clear();
        }

        /// <summary>
        /// 设置传递hook对象，以用于对象的OnCreate方法
        /// </summary>
        /// <param name="hook"></param>
        public virtual void OnCreate(object hook)
        {
            foreach (ToolStripItem toolItem in this.m_contextMenuStrip.Items)
            {
                //递归设置各菜单项的依赖对象
                SetChildHook(toolItem, hook);

                //各菜单项钩子对象的初始化
                OnCreate(toolItem, hook);
            }
        }

        /// <summary>
        /// 转换对象为ToolStripItem
        /// </summary>
        /// <param name="item">菜单对象</param>
        /// <returns>返回ToolStripItem对象</returns>
        protected virtual ToolStripItem GetToolStripItem(object item)
        {
            ToolStripItem toolItem = null;

            ICommand tCommand = item as ICommand;
            if (tCommand != null)
            {
                //实例化菜单项对象
                toolItem = new ToolStripMenuItem();
                toolItem.Name = tCommand.Name;
                toolItem.Text = tCommand.Caption;
                toolItem.Image = this.GetImageFromHbitmap(tCommand.Bitmap);
                toolItem.ToolTipText = tCommand.Tooltip;
                toolItem.Tag = item;

                //ToolStripItem单击事件的委托处理
                toolItem.Click += new EventHandler(ToolStripItemClick);
            }
            else if (item is ToolStripItem)
            {
                toolItem = item as ToolStripItem;
                toolItem.Click += new EventHandler(ToolStripItemClick);
            }

            return toolItem;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 递归设置各菜单项的钩子对象
        /// </summary>
        /// <param name="pToolStripItem"><see cref="ToolStripItem"/>pToolStripItem</param>
        /// <param name="hook">hook对象</param>
        private void SetChildHook(ToolStripItem pToolStripItem, object hook)
        {
            ToolStripMenuItem toolStripMenuItem = pToolStripItem as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                foreach (ToolStripItem toolItem in toolStripMenuItem.DropDownItems)
                {
                    //递归调用此方法
                    SetChildHook(toolItem, hook);

                    //各菜单项钩子对象的初始化
                    OnCreate(toolItem, hook);
                }
            }
        }

        /// <summary>
        /// 各菜单项钩子对象的初始化
        /// </summary>
        /// <param name="pToolStripItem"><see cref="ToolStripItem"/>pToolStripItem</param>
        /// <param name="hook">hook对象</param>
        private void OnCreate(ToolStripItem pToolStripItem, object hook)
        {
            //查询引用接口
            ICommand tCommand = pToolStripItem.Tag as ICommand;
            if (tCommand != null)
            {
                //设置依赖对象
                tCommand.OnCreate(hook);
                //设置对象的可用状态
                pToolStripItem.Enabled = tCommand.Enabled;
                //设置对象的的选中状态
                ToolStripMenuItem toolStripMenuitem = pToolStripItem as ToolStripMenuItem;
                if (toolStripMenuitem != null)
                    toolStripMenuitem.Checked = tCommand.Checked;               
            }
        } 

        /// <summary>
        /// ToolStripItem单击事件处理方法
        /// </summary>
        /// <param name="sender">事件的发起者</param>
        /// <param name="e">事件参数</param>
        private void ToolStripItemClick(object sender, EventArgs e)
        {
            ToolStripItem toolItem = sender as ToolStripItem;
            if (toolItem.Tag != null)
            {
                //查询引用接口
                ICommand tCommand = toolItem.Tag as ICommand;
                if (tCommand != null)
                {
                     //单击处理
                     tCommand.OnClick();
                }
            }
        }

        /// <summary>
        /// 从GDI位图的句柄处创建位图对象
        /// </summary>
        /// <param name="gdi32">GDI位图句柄值</param>
        /// <returns>返回位图对象</returns>
        private Image GetImageFromHbitmap(int gdi32)
        {
            if (gdi32 == 0) return null;

            IntPtr pIntPtr = new IntPtr(gdi32);
            //从句柄创建Bitmap
            Bitmap pBitmap = System.Drawing.Bitmap.FromHbitmap(pIntPtr);
            //使背景色透明
            pBitmap.MakeTransparent();

            return pBitmap;
        }
        #endregion
    }
}
