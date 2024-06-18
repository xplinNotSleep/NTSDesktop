using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;
using System;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ӥ����ͼ�ӿ���
    /// </summary>
    public interface IEagleDocumentView : IDocumentView
    {
        /// <summary>
        /// ӥ����ͼ��ͼ����
        /// </summary>
        IMap EagleMap { get; }
        /// <summary>
        /// ��ȡ�������Ƿ�ָ����ӥ����ͼͼ�����
        /// </summary>
        Boolean IsAffirm { get; }
        /// <summary>
        /// ����ӥ����ͼ������ͼ�ĵ�·��
        /// </summary>
        string MapDocument { set; }
    }
}
