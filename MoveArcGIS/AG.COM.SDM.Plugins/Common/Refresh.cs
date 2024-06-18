using System;
using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// 刷新插件类
    /// </summary>
    public sealed class Refresh : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Refresh()
        {
            this.m_caption = "刷新";
            this.m_toolTip = "刷新";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "GenericRefresh16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "GenericRefresh32.ico"));            
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            DockDocument dock = this.m_hookHelper.DockDocumentService.GetDockDocument(Convert.ToString(EnumDocumentType.MapDocument));
            dock.Show();
            dock.Width = dock.Width - 1; 
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;    
        }
    }
}
