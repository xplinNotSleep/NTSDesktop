using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Framework.DocumentView;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// ���ɿ�ܶ���ͨ�Žӿ�
    /// </summary>
    public interface IHookHelper
    {
        /// <summary>
        /// ��ȡ��ͼ�������ӿ�
        /// </summary>
        //IMapService MapService { get;}

        IFramework Framework { get; set; }

        /// <summary>
        /// ��ȡ�������������ӿ�
        /// </summary>
        IToolBarService ToolBarService { get;}
        /// <summary>
        /// ��ȡ����������ӿ�
        /// </summary>
        IPluginsService PluginsService { get;}
        /// <summary>
        /// ��ȡ�˵��������ӿ�
        /// </summary>
        IMenuService MenuService { get;}        
        /// <summary>
        /// ��ȡ�ĵ��������ӿ�
        /// </summary>
        IDockDocumentService DockDocumentService { get;}
        /// <summary>
        /// ��ȡIWin32Window�ӿ�
        /// </summary>
        IWin32Window Win32Window { get;}


        
    }

    
}
