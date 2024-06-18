using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    /// <summary>
    /// 数据接边 插件类
    /// </summary>
    public sealed class ConnecteBorderCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_HookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ConnecteBorderCommand()
        {
            base.m_caption = "数据接边";
            base.m_name = GetType().FullName;
            base.m_toolTip = "数据接边";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E6+~.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E6+~_32.ico"));       
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
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            frmConnecteBorder frm = new frmConnecteBorder();
            frm.ShowDialog();
        }

        /// <summary>
        /// 创建对象处理
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Hook = hook;
        }
    }
}
