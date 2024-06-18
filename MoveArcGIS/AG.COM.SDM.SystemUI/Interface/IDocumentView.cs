using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �ĵ�����ӿ���
    /// </summary>
    public interface IDocumentView
    {    
        /// <summary>
        /// ��ȡ�������ĵ������������
        /// </summary>
        string DocumentTitle { get;set;}
        /// <summary>
        /// ��ȡ�ĵ���������
        /// </summary>
        EnumDocumentType DocumentType { get;}
        /// <summary>
        /// ��ȡ��ǰ�ĵ��ؼ�����
        /// </summary>
        Object Hook { get;}
    }
}
