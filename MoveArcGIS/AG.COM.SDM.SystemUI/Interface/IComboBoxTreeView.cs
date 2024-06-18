using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 带TreeView的Combobox的接口
    /// </summary>
    public interface IComboBoxTreeView
    {
        /// <summary>
        /// 获取下拉选项框的宽度
        /// </summary>
        int Width { get; }

        /// <summary>
        /// 获取下拉选项框的宽度
        /// </summary>
        int Height { get; } 

        /// <summary>
        /// TreeView
        /// </summary>
        TreeView TreeView { get; set; }

        /// <summary>
        /// 绑定的BarEditItem
        /// </summary>
        BarEditItem BarEditItem { get; set; }

        /// <summary>
        /// ComboBox的下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDropDown(object sender, EventArgs e);
    }
}
