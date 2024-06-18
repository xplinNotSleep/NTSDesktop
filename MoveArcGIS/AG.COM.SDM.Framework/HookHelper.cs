using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ���ɿ�ܶ���ͨ����
    /// </summary>
    public class HookHelper : IHookHelper
    {
        //private IMapService m_MapService;
        private IFramework m_Framework;

        #region IHookHelper ��Ա

        /// <summary>
        /// ��ȡ��������Ϣ���Ӷ���
        /// </summary>
        public IFramework Framework
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

        #region IHookHelper ��Ա
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
