using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ҳ�沼���ĵ�����ӿ�
    /// </summary>
    public interface IPageLayOutDocumentView : IDocumentView
    {
        /// <summary>
        /// ��ȡ��ͼ����
        /// </summary>
        IActiveView ActiveView { get;}
        /// <summary>
        /// ��ȡҳ�沼�ֶ���
        /// </summary>
        IPageLayout PageLayout { get;}
    }
}
