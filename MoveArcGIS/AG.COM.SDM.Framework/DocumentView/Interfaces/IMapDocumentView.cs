using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ��ͼ�ĵ�����ӿ�
    /// </summary>
    public interface IMapDocumentView:IDocumentView 
    {

        /// <summary>
        /// ��ȡ��ǰ��ͼ�ĵ���ͼ
        /// </summary>
        IMap Map { get;}
        /// <summary>
        /// ��ȡ��ǰ��ͼ�ĵ���ͼ
        /// </summary>
        IActiveView ActiveView { get;}

        /// <summary>
        /// ��ȡ�����õ�ͼ�ĵ�Ĭ���Ҽ��˵�
        /// </summary>
        IContextMenu DefaultContextMenu { get;set;}
    }
}
