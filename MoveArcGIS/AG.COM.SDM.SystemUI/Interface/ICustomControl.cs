using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �Զ���ؼ��ӿ���
    /// </summary>
    public interface ICustomControl : IPlugin
    {
        /// <summary>
        /// ��ȡ�Զ���ؼ�
        /// </summary>
        Control CustomControl { get; }
    }
}
