using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework.DocumentView
{
    /// <summary>
    /// �����ĵ��ӿ���
    /// </summary>
    public interface ITocDocumentView : IDocumentView
    {
        /// <summary>
        /// Ĭ�������Ĳ˵���ͨ�ã�
        /// </summary>
        IContextMenu DefaultContextMenu { get; set; }

        #region ����Դ���ĵ�����
        /// <summary>
        /// �������ĵ���ָ������
        /// </summary>
        /// <param name="pTocBuddy">�󶨶���</param>
        //void SetBuddyControl(object pTocBuddy);
        #endregion

        #region ESRI
        /// <summary>
        /// ��ȡ�����õ�ͼ���������Ĳ˵�
        /// </summary>
        //IContextMenu MapContextMenu { get;set;}
        ///// <summary>
        ///// ��ȡ������ͼ���������Ĳ˵�
        ///// </summary>
        //IContextMenu LayerContextMenu { get;set;}
        #endregion

    }
}
