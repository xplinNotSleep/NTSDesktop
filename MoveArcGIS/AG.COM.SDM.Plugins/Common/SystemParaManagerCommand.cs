using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// 模板参数设置插件类
    /// </summary>
    public class SystemParaManagerCommand : BaseCommand, IUseIcon
    {
        protected IHookHelperEx m_hookHelper = new HookHelperEx();
        /// <summary>
        /// 初始化对象实例
        /// </summary>
        public SystemParaManagerCommand()
        {
            this.m_caption = "系统参数设置";
            this.m_message = "系统参数设置";
            this.m_toolTip = "系统参数设置";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "F8.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "F8_32.ico"));            
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

        public override void OnCreate(object hook)
        {
            m_hookHelper.Hook = hook;
        }

        public override void OnClick()
        {
            FormSystemParaManager tFormSystemParaManager = new FormSystemParaManager();
            tFormSystemParaManager.ShowDialog();
        }
    }
}
