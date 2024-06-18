using System.Drawing;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;


namespace AG.COM.SDM.Plugins
{
    public class PositionManageCommand : BaseCommand, IUseIcon
    {
        private IHookHelper m_hookHelper;

        /// <summary>
        /// 默认构造函数 
        /// </summary>
        public PositionManageCommand()
        {
            m_hookHelper = new HookHelper();
            base.m_caption = "岗位管理";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G4.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "G4_32.ico"));          
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
            FormPositionManage tFrm = new FormPositionManage();
            tFrm.ShowInTaskbar = false;
            tFrm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; 
            tFrm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;
        }
    }
}
