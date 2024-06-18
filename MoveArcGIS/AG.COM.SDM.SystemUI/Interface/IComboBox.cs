using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 下拉选项框基类 接口
    /// </summary>
    public interface IComboBox
    {   
        /// <summary>
        /// 标签名称
        /// </summary>
        string LabelText { get;}

        /// <summary>
        /// 获取下拉选项框的高度
        /// </summary>
        int Height { get;}

        /// <summary>
        /// 获取下拉选项框的宽度
        /// </summary>
        int Width { get;} 

        /// <summary>
        /// 获取绑定的数据源对象 
        /// </summary>
        object DataSource { get;}

        /// <summary>
        /// 获取下拉框样式设置
        /// </summary>
        ComboBoxStyle ComboBoxStyle { get;}

        /// <summary>
        /// 键按下处理方法
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        void OnKeyDown(object sender, KeyEventArgs e);

        /// <summary>
        /// 键弹起处理方法
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        void OnKeyUp(object sender, KeyEventArgs e);

        /// <summary>
        /// 键按下处理方法
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        void OnKeyPress(object sender, KeyPressEventArgs e);

        /// <summary>
        /// 下拉选项发生变化时的处理方法
        /// </summary>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        void OnSelectedIndexChanged(object sender, EventArgs e);      
    }
}
