using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework.DocumentView;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ��ͼ����ӿ�
    /// </summary>
    public interface IMapService
    {
        /// <summary>
        /// ��ȡ�����õ�ͼ��׽�ݲ�
        /// </summary>
        double Tolerance { get;set;}
        /// <summary>
        /// ��ȡ��ǰ������ͼ����
        /// </summary>
        IActiveView ActiveView { get;}
        /// <summary>
        /// ��ȡ��ǰ�����ͼ����
        /// </summary>
        IMap FocusMap { get;}
        /// <summary>
        /// ��ȡ�����õ�ͼ�ĵ�����
        /// </summary>
        IMapDocument MapDocument { get;set;}
        /// <summary>
        /// ��ȡ��ǰ������ͼ�ؼ�
        /// </summary>
        IMapControl2 MapControl { get;}
        /// <summary>
        /// ��ȡ��ǰ��ͼ��������
        /// </summary>
        ITool CurrentTool { get;set;}
        /// <summary>
        /// ��ȡ��ǰ������ͼͼ��
        /// </summary>
        ILayer CurrentLayer { get;}
        /// <summary>
        /// ��ȡ��ǰ��������ջ
        /// </summary>
        IOperationStack OperationStack { get;}
        /// <summary>
        /// ��ȡ������ͼ����
        /// </summary>
        IPageLayout PageLayout { get;}
        /// <summary>
        /// ��ȡ������ͼ�ؼ�
        /// </summary>
        IPageLayoutControl2 PageLayoutControl { get;}
        /// <summary>
        /// ��ȡ������ͼ�ؼ�
        /// </summary>
        ITOCControl TOCControl { get;}
        /// <summary>
        /// ��ȡӥ�۵�ͼ�ؼ�
        /// </summary>
        IMapControl2 EagleMapControl { get;}
        /// <summary>
        /// ��ȡ�����ù��Ӷ���
        /// </summary>
        object Hook { get;set;}
        /// <summary>
        /// ��ȡ��ͼ�༭����
        /// </summary>
        MapEditor Editing { get;}
        /// <summary>
        /// ��ͼ��Ϣ��ʾ
        /// </summary>
        ToolTip InfoTip { get;}
        /// <summary>
        /// ��ͼˢ���¼�
        /// </summary>
        event IMapControlEvents2_OnViewRefreshedEventHandler OnViewRefreshed;
        /// <summary>
        /// ��ͼ��ǰ��Χ�����¼�
        /// </summary>
        event IMapControlEvents2_OnExtentUpdatedEventHandler OnExtentUpdated;
        /// <summary>
        /// ��ͼ��������¼�
        /// </summary>
        event IMapControlEvents2_OnMapReplacedEventHandler OnMapReplaced;
    }
}
