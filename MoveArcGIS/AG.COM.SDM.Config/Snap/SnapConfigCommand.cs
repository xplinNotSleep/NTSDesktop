using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 捕捉设置
    /// </summary>
    public sealed class SnapConfigCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelper;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SnapConfigCommand()
        {             
            m_hookHelper = new HookHelperEx();
            this.m_caption = "捕捉设置";
            base.m_name = GetType().FullName;
            this.m_message = "捕捉设置";
            this.m_toolTip = "捕捉设置";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G7.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G7+_32.ico"));        
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
        /// 获取当前可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_hookHelper.FocusMap.LayerCount == 0) 
                    return false;
                else 
                    return true;
            }
        }

        /// <summary>
        /// 创建时的初始赋值
        /// </summary>
        /// <param name="hook">传递对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;    
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        public override void OnClick()
        {
            FormSnap frm = new FormSnap();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog();
        }
    }
}
