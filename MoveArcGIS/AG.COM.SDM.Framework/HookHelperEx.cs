using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ���ɿ�ܶ���ͨ����
    /// </summary>
    public class HookHelperEx : IHookHelperEx
    {
        //private IMapService m_MapService;
        private IFramework m_Framework;

        #region IHookHelper ��Ա

        #region ESRI
        ///// <summary>
        ///// ��ȡ��ǰ��ͼ��ActiveView(�����ǰ����Ϊ��ͼ����ʱ����ActiveViewΪIPageLayout��ActiveView��
        ///// ���Ҫ����IPageLayout�еĵ�ͼ������д�� ActiveView.FocusMap as IActiveView)
        ///// </summary>
        //public ESRI.ArcGIS.Carto.IActiveView ActiveView
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.ActiveView;
        //    }
        //}

        ///// <summary>
        ///// ��ȡ��ǰ��ͼ����ļ����ͼ
        ///// </summary>
        //public ESRI.ArcGIS.Carto.IMap FocusMap
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.FocusMap;
        //    }
        //}

        ///// <summary>
        ///// ��ȡ��ǰ����ջ
        ///// </summary>
        //public ESRI.ArcGIS.SystemUI.IOperationStack OperationStack
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.OperationStack;
        //    }
        //}

        ///// <summary>
        ///// ��ȡ������ͼ
        ///// </summary>
        //public ESRI.ArcGIS.Carto.IPageLayout PageLayout
        //{
        //    get
        //    {
        //        return (m_MapService == null) ? null : m_MapService.PageLayout;
        //    }
        //}

        #endregion

        /// <summary>
        /// ��ȡ��������Ϣ���Ӷ���
        /// </summary>
        public object Hook
        {
            get
            {
                return m_Framework;
            }
            set
            {
                m_Framework = value as IFramework;
                if (m_Framework == null)
                    throw new ArgumentNullException();
                //else
                //    m_MapService = m_Framework.GetService(typeof(IMapService)) as IMapService;
            }
        }
        

        #endregion

        #region IHookHelperEx ��Ա
        /// <summary>
        /// ��ȡIWin32window�ӿ�
        /// </summary>
        public IWin32Window Win32Window
        {
            get
            {
                return this.m_Framework.MdiParentForm as IWin32Window;
            }
        }

        /// <summary>
        /// ��ȡ��ͼ��ȡ����
        /// </summary>
        //public IMapService MapService
        //{
        //    get
        //    {
        //        return m_Framework.GetService(typeof(IMapService)) as IMapService;
        //    }
        //}

        /// <summary>
        /// ��ȡToolBar����
        /// </summary>
        public IToolBarService ToolBarService
        {
            get { return m_Framework.GetService(typeof(IToolBarService)) as IToolBarService; }
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public IPluginsService PluginsService
        {
            get { return m_Framework.GetService(typeof(IPluginsService)) as IPluginsService; }
        }

        /// <summary>
        /// ��ȡ�˵��������
        /// </summary>
        public IMenuService MenuService
        {
            get { return m_Framework.GetService(typeof(IMenuService)) as IMenuService; }
        }

        /// <summary>
        /// ��ȡ�ĵ���ͼ�������
        /// </summary>
        public IDockDocumentService DockDocumentService
        {
            get { return m_Framework.GetService(typeof(IDockDocumentService)) as IDockDocumentService; }
        }
        #endregion
    }
}
