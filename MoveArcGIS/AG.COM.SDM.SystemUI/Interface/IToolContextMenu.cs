using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// ��ͼ���������Ҽ���ݲ˵��ӿ�
    /// </summary>
    public interface IToolContextMenu : IPlugin
    {
        /// <summary>
        /// ��ȡ�Ҽ���ݲ˵�
        /// </summary>
        ContextMenuStrip ContextMenuStrip { get;}
    }
}
