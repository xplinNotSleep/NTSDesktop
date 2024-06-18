using System;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 右键快捷菜单 接口类
    /// </summary>
    public interface IContextMenu:IToolContextMenu
    {
        /// <summary>
        /// 获取或设置上下文菜单图片
        /// </summary>
        int Bitmap { get; set; }

        /// <summary>
        /// 确定集合是否包含指定关键字的项
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        /// <returns>如果集合中包含此项则返回 true,否则返回 false</returns>
        bool ContainsItem(string keyName);

        /// <summary>
        /// 根据关键字查找匹配的项
        /// </summary>
        /// <param name="keyName">关键字</param>
        /// <returns>如果找到返回该菜单项，否则返回null</returns>
        ToolStripItem GetItemByKeyName(string keyName);

        /// <summary>
        /// 在指定索引位置处插入菜单项
        /// </summary>
        /// <param name="item">菜单项对象 默认为继承ICommand接口的对象</param>
        /// <param name="index">如果不指定则为-1,否则指定插入位置</param>
        /// <param name="beginGroup">是否开始分组</param>
        /// <param name="style">菜单样式设置</param>
        void AddItem(object item, int index, bool beginGroup, ToolStripItemDisplayStyle style);

        /// <summary>
        /// 在指定位置处加载菜单项
        /// </summary>
        /// <param name="keyName">关键字</param>
        /// <param name="index">指定索引位置,如果不指定则为 -1</param>
        /// <param name="beginGroup">如果开始分组则为true,否则为false</param>
        /// <param name="subMenuItem">要加载的菜单项</param>
        void AddSubMenu(string keyName, int index, bool beginGroup,object subMenuItem);

        void PopupMenu(int X, int Y, IntPtr intPtr);

        /// <summary>
        /// 相对指定的句柄控件处弹出上下文菜单
        /// </summary>
        /// <param name="X">X方向距离</param>
        /// <param name="Y">Y方向距离</param>
        /// <param name="hwndParent">指定控件的句柄</param>
        void PopupMenu(int X, int Y, int hwndParent);

        /// <summary>
        /// 移除指定索引处的菜单
        /// </summary>
        /// <param name="index">指定索引位置</param>
        void Remove(int index);

        /// <summary>
        /// 移除指定关键字的项
        /// </summary>
        /// <param name="keyName">指定关键字</param>
        void RemoveByKeyName(string keyName);

        /// <summary>
        /// 移除上下文菜单中的所有项
        /// </summary>
        void RemoveAll();    
    }
}
