using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// �ĵ��������ӿ�
    /// </summary>
    public interface IDockDocumentService
    {

        /// <summary>
        /// ����DockDocments����
        /// </summary>
        Dictionary<string, DockDocument> DockDocuments{get;}
        /// <summary>
        /// ��ȡ��ǰ�������Ƿ�����˴���
        /// </summary>
        /// <param name="dockDocumentName">��������</param>
        /// <returns>��������򷵻� true,���򷵻� </returns>
        Boolean ContainsDocument(string dockDocumentName);

        /// <summary>
        /// ��ȡָ�����Ƶ��ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        /// <returns>����ָ�����Ƶ��ĵ�����</returns>
        DockDocument GetDockDocument(string dockDocumentName);

        /// <summary>
        /// �Ƴ�������
        /// </summary>
        void Clear();
        
        /// <summary>
        /// ����ĵ�����,Ĭ��ͣ��״̬Ϊ����ͣ��
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        /// <param name="dockDocument">DockDocument����</param>
        void AddDockDocument(string dockDocumentName, DockDocument dockDocument);

        /// <summary>
        /// ����ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        /// <param name="dockDocument">DockDocument����</param>
        /// <param name="dockState">ͣ��״̬</param>
        void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockState dockState);

        /// <summary>
        /// ����ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ�����</param>
        /// <param name="dockDocument">DockDocument����</param>
        /// <param name="parentDocument">ͣ���ڵ�DockDocument����</param>
        /// <param name="dockState">ͣ��״̬</param>
        void AddDockDocument(string dockDocumentName, DockDocument dockDocument, DockDocument parentDocument, DockState dockState);

        /// <summary>
        /// ����ĵ�����Document��ʽ��ӣ����뵱ǰDocument���ҷ�����
        /// </summary>
        /// <param name="dockDocumentName"></param>
        /// <param name="dockDocument"></param>
        void AddDockDocumentSplit(string dockDocumentName, DockDocument dockDocument);

        /// <summary>
        /// ��Document��tab��ʽͣ������һ��Document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="documentTarget"></param>
        void DockDocumentToAnother(DockDocument document, DockDocument documentTarget);

        /// <summary>
        /// ��Document��Ϊ������ʾ
        /// </summary>
        /// <param name="document"></param>
        void DockDocumentSplit(DockDocument document);

        /// <summary>
        /// �Ƴ����ر�ָ�����Ƶ��ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">��������</param>
        void RemoveDockDocument(string dockDocumentName);

        /// <summary>
        /// �Ƴ�ָ�����Ƶ��ĵ�����
        /// </summary>
        /// <param name="dockDocumentName">�ĵ���������</param>
        void RemoveKey(string dockDocumentName);        
    }
}
