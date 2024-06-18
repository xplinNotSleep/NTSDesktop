using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ���嵱ǰͼ�㷢���ı�ʱ��ί������
    /// </summary>
    /// <param name="sender">����</param>
    /// <param name="e">�¼�����</param>
    public delegate void CurrentLayerChangedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// ����ӥ����ͼ��Χ�����仯ʱ��ί������
    /// </summary>
    /// <param name="newEnvelope">�µ���ͼ��Χ</param>
    /// <param name="e">�¼�����</param>
    public delegate void EagleViewChangedEventHandler(object newEnvelope,EventArgs e);

    /// <summary>
    /// ��ǰ�����ĵ������仯ʱ��ί������
    /// </summary>
    /// <param name="sender">�ĵ�����</param>
    /// <param name="e">�¼�����</param>
    public delegate void MapDocumentChangedEventHandler(object sender, EventArgs e);

    //public delegate void MapDocumentOpenEventHandler(object sender, EventArgs e);

    //public delegate void MapDocumentSaveEventHandler(object sender, EventArgs e);

    //public delegate void MapDocumentNewEventHandler(object sender,

    /// <summary>
    /// ������󵥻������¼���ί������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PlugCommandClikedEventHandler(object sender,EventArgs e);

    /// <summary>
    /// ͼ����ͼ������굥���¼���ί������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LayerControlMouseDownEventHandler(object sender, EventArgs e);

}
