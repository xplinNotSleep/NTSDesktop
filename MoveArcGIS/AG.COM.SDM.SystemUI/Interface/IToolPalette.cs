using System;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{ 
    /// <summary>
    /// 调色板工具箱 接口
    /// </summary>
    public interface IToolPalette:IPlugin
    {
        /// <summary>
        /// 获取或设置调色板布局允许最大的列数
        /// </summary>
        int ColumnCount { get;set;}

        /// <summary>
        /// 获取或设置调色板布局允许最大的行数
        /// </summary>
        int RowCount { get;set;}

        /// <summary>
        /// 返回下拉调色板包含的工具项总数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 确定集合中是否包含指定关键字的项
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        /// <returns>如果包含则返回 true，否则返回 false</returns>
        bool ContainsItem(string keyName);

        /// <summary>
        /// 获取或设置当前激活项
        /// </summary>
        ToolStripItem ActiveItem { get; set;}

        /// <summary>
        /// 获取指定的关键字获取ToolStripItem对象
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        /// <returns>如果存在指定关键字对象则返回该对象，否则返回 null</returns>
        ToolStripItem GetItemByKeyName(string keyName);

        /// <summary>
        /// 获取ToolStripDropDown
        /// </summary>
        ToolStripDropDown ToolStripDropDown { get;}

        /// <summary>
        /// 在指定索引位置处插入项
        /// </summary>
        /// <param name="item">插入实现ICommand接口的对象或者ToolStripButton</param>
        /// <param name="index">插入索引位置，如果未指定则为 -1</param> 
        void AddItem(object item, int index);

        /// <summary>
        /// 在指定位置处显示
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <param name="hWndParent">控件句柄</param>
        void PopupPalette(int X, int Y, int hWndParent);

        /// <summary>
        /// 清除所有项
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// 移除指定关键字的对象
        /// </summary>
        /// <param name="keyName">指定的关键字对象</param>
        void RemoveByKey(string keyName);         
    }
}
