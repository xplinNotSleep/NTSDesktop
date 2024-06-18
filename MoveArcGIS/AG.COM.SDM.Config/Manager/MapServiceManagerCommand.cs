using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using System.Drawing;

namespace AG.COM.SDM.Config.Manager
{
    public class MapServiceManagerCommand : BaseCommand, IUseIcon
    {
        IHookHelperEx m_HookHelperEx = null;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MapServiceManagerCommand()
        {
            base.m_caption = "地图服务管理";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G9++.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G9++_32.ico"));
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

        public override void OnClick()
        {
            FormMapServiceManager tFormMapServiceManager = new FormMapServiceManager();
            tFormMapServiceManager.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            m_HookHelperEx = new HookHelperEx();
            m_HookHelperEx.Hook = hook;
        }
    }
}
