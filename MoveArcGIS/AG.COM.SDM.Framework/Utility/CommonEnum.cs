using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        Menu,
        Toolbar,
        RibbonPage,
        RibbonPageGroup,
        Item,
        None,
        Text,
        Separator,
        CustomControl,
        ComboBox,
        ComboBoxTreeView
    }

    /// <summary>
    /// 界面设计来源
    /// </summary>
    public enum UIDesignFrom
    {
        Database,
        XmlFile
    }
}
