using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// ��ͼ�ĵ�����ӿ�
    /// </summary>
    public interface IMainDocumentView:IDocumentView 
    {

        /// <summary>
        /// ��ȡ�����õ�ͼ�ĵ�Ĭ���Ҽ��˵�
        /// </summary>
        IContextMenu DefaultContextMenu { get;set;}
    }
}
