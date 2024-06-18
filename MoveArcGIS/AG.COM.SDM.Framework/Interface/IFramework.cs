using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Ribbon;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 集成框架接口
    /// </summary>
    public interface IFramework : IServiceContainer
    {      
        /// <summary>
        /// 设置或获取状态栏
        /// </summary>
        RibbonStatusBar StatusBar { get; set; }
        /// <summary>
        /// 设置或获取DockManager
        /// </summary>
        DockManager DockManager { get; set; }
        /// <summary>
        /// 设置或获取DocumentManager
        /// </summary>
        DocumentManager DocumentManager { get; set; }
        /// <summary>
        /// 获取或设置当前激活子窗体
        /// </summary>
        Form ActiveMdiChildForm { get;}
        /// <summary>
        /// 获取或设置当前MDI父窗体
        /// </summary>
        Form MdiParentForm { get;set;}

        /// <summary>
        /// 有权限的插件类名称
        /// </summary>
        List<string> HasLicPlugins { get; set; }

         /// <summary>
        /// 无限制许可
        /// </summary>
        bool LicenseUnlimited { get; set; }

        /// <summary>
        /// 是否启用菜单刷新
        /// </summary>
        bool EnabledMenuRefresh { get; set; }

        /// <summary>
        /// 负责通知地图文档对象事件的登记对象
        /// </summary>
        /// <param name="sender">文档对象</param>
        /// <param name="e">事件参数</param>
        void OnMapDocumentChanged(object sender, EventArgs e);
        /// <summary>
        /// 负责通知当前图层发生变化的登记对象
        /// </summary>
        /// <param name="sender">图层对象</param>
        /// <param name="e">事件参数</param>
        void OnCurrentLayerChanged(object sender, EventArgs e);
        /// <summary>
        /// 负责通知鹰眼视图发生变化的登记对象
        /// </summary>
        /// <param name="newEnvelope">新的可视范围</param>
        /// <param name="e">事件参数</param>
        void OnEagleViewChanged(object newEnvelope, EventArgs e); 
        /// <summary>
        /// 负责通知插件对象单击时的登记对象
        /// </summary>
        /// <param name="sender">插件对象</param>
        /// <param name="e">事件参数</param>
        void OnPlugCommandClicked(object sender, EventArgs e);
    }
}
