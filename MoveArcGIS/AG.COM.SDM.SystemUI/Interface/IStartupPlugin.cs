using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// ϵͳ��������ӿ�
    /// </summary>
    public interface IStartupPlugin:IPlugin
    {
        /// <summary>
        /// ������������Ϣ
        /// </summary>
        string Description { get;}

        /// <summary>
        /// ϵͳ��������
        /// </summary>
        void Startup();
    }
}
