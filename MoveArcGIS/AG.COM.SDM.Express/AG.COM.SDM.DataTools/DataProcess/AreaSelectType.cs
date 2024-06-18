namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// ѡ������ö��
    /// </summary>
    public enum AreaSelectType
    {
        TYPE_NONE = 0,
        /// <summary>
        /// ��ѡ
        /// </summary>        
        TYPE_POINT = 1,
        /// <summary>
        /// ����ѡ��
        /// </summary>
        TYPE_POLYLINE = 2,
        /// <summary>
        /// �����ѡ��
        /// </summary>
        TYPE_POLYGON = 3,
        /// <summary>
        /// ����ѡ��
        /// </summary>
        TYPE_RECT = 4,
        /// <summary>
        /// ��ǰ��Ļѡ��
        /// </summary>
        TYPE_SCREEN = 5,
        /// <summary>
        /// Բѡ
        /// </summary>
        TYPE_CIRCLE = 6
    }

    /// <summary>
    /// �û�����ѡ��ӿ�
    /// </summary>
    public interface ISelAreaForm
    {
        /// <summary>
        /// ��ȡ�û�����ѡ��ؼ�
        /// </summary>
        ControlSelArea SelArea { get; }
    }
}
