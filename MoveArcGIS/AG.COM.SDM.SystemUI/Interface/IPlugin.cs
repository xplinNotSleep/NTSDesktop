using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �������ӿ�
    /// </summary>
    public interface  IPlugin
    {        
        /// <summary>
        /// �������
        /// </summary>
        string Name { get;}

        /// <summary>
        /// �����ʾ����
        /// </summary>
        string Caption { get;}

        /// <summary>
        /// ��ȡ����Ŀ���״̬
        /// </summary>
        bool Enabled { get;}

        /// <summary>
        /// ����ʱ��ʼ����ֵ
        /// </summary>
        /// <param name="hook">hook����</param>
        void OnCreate(object framework);
    }
}
