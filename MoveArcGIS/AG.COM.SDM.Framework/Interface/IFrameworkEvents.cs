using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ���ɿ���¼��ӿ�
    /// </summary>
    public interface IFrameworkEvents
    {
        /// <summary>
        /// �����ĵ����ڷ����仯�¼�
        /// </summary>
        event MapDocumentChangedEventHandler MapDocumentChanged;

        /// <summary>
        /// ��ǰͼ�㷢���仯�¼�
        /// </summary>
        event CurrentLayerChangedEventHandler CurrentLayerChanged;

        /// <summary>
        /// ӥ����ͼ���ŷ�Χ�����仯�¼�
        /// </summary>
        event EagleViewChangedEventHandler EagleViewChanged;

        /// <summary>
        /// ������󵥻��¼�
        /// </summary>
        event PlugCommandClikedEventHandler PlugCommandCliked;
    }
}
