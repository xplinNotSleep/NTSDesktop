using AG.COM.SDM.SystemUI.Utility;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 调色板工具箱基类
    /// </summary>
    public class BaseToolPalette : IToolPalette
    {
        protected ToolStripDropDown m_ToolStripDropDown;
        protected ToolStripItem m_ActiveItem;
        protected string m_Name;      
        protected string m_Caption;
        protected bool m_Enabled = true;
        protected object m_hook;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseToolPalette()
        {
            //实例化下拉列表框对象
            this.m_ToolStripDropDown = new ToolStripDropDown();
            this.m_ToolStripDropDown.Tag = this;
            //设置布局样式
            this.m_ToolStripDropDown.LayoutStyle = ToolStripLayoutStyle.Table;
            //设置允许的最大列数
            (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).ColumnCount = 3;           
        }

        /// <summary>
        /// 获取该项Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        /// <summary>
        /// 获取或设置调色板名称
        /// </summary>
        public virtual string Caption
        {
            get
            {
                return this.m_Caption;
            }
            set
            {
                this.m_Caption = value;
            }
        }

        /// <summary>
        /// 返回对象的可用状态
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                return this.m_Enabled;
            }
        }

        /// <summary>
        /// 获取或设置调色板布局允许最大的列数
        /// </summary>
        public virtual int ColumnCount
        {
            get
            {
                return (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).ColumnCount;
            }
            set
            {
                (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).ColumnCount = value;
            }
        }

        /// <summary>
        /// 获取或设置调色板布局允许最大的行数
        /// </summary>
        public virtual int RowCount
        {
            get
            {
                return (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).RowCount;
            }
            set
            {
                (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).RowCount = value;
            }
        }

        /// <summary>
        /// 返回下拉调色板包含的工具项总数
        /// </summary>
        public virtual int Count
        {
            get
            {
                return this.m_ToolStripDropDown.Items.Count;
            }
        }

        /// <summary>
        /// 确定集合中是否包含指定关键字的项
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        /// <returns>如果包含则返回 true，否则返回 false</returns>
        public virtual bool ContainsItem(string keyName)
        {
            return this.m_ToolStripDropDown.Items.ContainsKey(keyName);
        }

        /// <summary>
        /// 获取指定的关键字获取ToolStripItem对象
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        /// <returns>如果存在指定关键字对象则返回该对象，否则返回 null</returns>
        public virtual ToolStripItem GetItemByKeyName(string keyName)
        {
            if (ContainsItem(keyName))
            {
                ToolStripItem[] toolItems = this.m_ToolStripDropDown.Items.Find(keyName, false);
                return toolItems[0];
            }

            return null;
        }

        /// <summary>
        /// 获取ToolStripDropDown
        /// </summary>
        public virtual ToolStripDropDown ToolStripDropDown
        {
            get
            {
                return this.m_ToolStripDropDown;
            }
        }

        /// <summary>
        /// 在指定索引位置处插入项
        /// </summary>
        /// <param name="item">插入实现ICommand接口的对象或者ToolStripButton</param>
        /// <param name="index">插入索引位置，如果未指定则为 -1</param> 
        public virtual void AddItem(object item, int index)
        {
            if (index >= this.m_ToolStripDropDown.Items.Count)
                throw (new Exception("index 索引值超出范围"));

            ToolStripItem toolItem = null;

            //查询引用接口
            ICommand tCommand = item as ICommand;
            if (tCommand != null)
            {
                //实例化ToolStripButton对象
                toolItem = new ToolStripButton();
                toolItem.Name = tCommand.Name;
                toolItem.Text = tCommand.Caption;
                //指定的GDI位图句柄中创建对象
                toolItem.Image = LibImage.GetImageFromHbitmap(tCommand.Bitmap);
                toolItem.ToolTipText = tCommand.Tooltip;
                toolItem.Tag = item;
                toolItem.Click += new EventHandler(ToolStripItemClick);
            }
            else if (item is ToolStripItem)
            {
                toolItem = item as ToolStripItem;
            }

            if (toolItem != null)
            {
                if (index > 0)
                    this.m_ToolStripDropDown.Items.Insert(index, toolItem);
                else
                    //在指定索引位置处插入对象
                    this.m_ToolStripDropDown.Items.Add(toolItem);
            }
        }

        /// <summary>
        /// 移除指定关键字的对象
        /// </summary>
        /// <param name="keyName">指定的关键字对象</param>
        public virtual void RemoveByKey(string keyName)
        {
            if (this.m_ToolStripDropDown.Items.ContainsKey(keyName))
            {
                this.m_ToolStripDropDown.Items.RemoveByKey(keyName);
            }
        }

        /// <summary>
        /// 清除所有项
        /// </summary>
        public virtual void RemoveAll()
        {
            this.m_ToolStripDropDown.Items.Clear();
        }

        /// <summary>
        /// 在指定位置处显示
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <param name="hWndParent">控件句柄</param>
        public virtual void PopupPalette(int X, int Y, int hWndParent)
        {
            IntPtr tIntPtr = new IntPtr(hWndParent);
            //返回当前与指定句柄关联的控件
            Control tControl = Control.FromHandle(tIntPtr);
            //在指定位置处显示
            this.m_ToolStripDropDown.Show(tControl, X, Y);
        }

        /// <summary>
        /// 设置钩子对象
        /// </summary>
        /// <param name="hook">钩子对象</param>
        public virtual void OnCreate(object hook)
        {
            this.m_hook = hook;

            //循环设置对象的可用状态
            foreach (ToolStripItem toolItem in this.m_ToolStripDropDown.Items)
            {
                if (toolItem.Tag != null)
                {
                    ICommand tCommand = toolItem.Tag as ICommand;
                    if (tCommand != null)
                    {
                        //创建时设置钩子对象
                        tCommand.OnCreate(hook);
                        //设置对象的可用状态
                        toolItem.Enabled = tCommand.Enabled;
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置当前激活项
        /// </summary>
        public virtual ToolStripItem ActiveItem
        {
            get
            {
                if (this.m_ActiveItem == null && this.m_ToolStripDropDown.Items.Count>0)
                    this.m_ActiveItem = this.m_ToolStripDropDown.Items[0];

                return this.m_ActiveItem;
            }
            set
            {
                m_ActiveItem = value;
            }
        }

        /// <summary>
        /// ToolStripItem单击事件处理方法
        /// </summary>
        /// <param name="sender">事件的发起者</param>
        /// <param name="e">事件参数</param>
        private void ToolStripItemClick(object sender, EventArgs e)
        {
            this.m_ActiveItem = sender as ToolStripItem;
        }
    } 
}
