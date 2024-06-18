using AG.COM.SDM.Config.Manager;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using System.Drawing;

namespace AG.COM.SDM.Plugins.Common
{
    public class DBSystemParaManagerCommand : BaseCommand, IUseIcon
    {
        protected IHookHelperEx m_hookHelper = new HookHelperEx();
        /// <summary>
        /// 初始化对象实例
        /// </summary>
        public DBSystemParaManagerCommand()
        {
            this.m_caption = "全局系统参数";        
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
        }

        public override void OnClick()
        {
            FormDBSystemParaManager tFormDBSystemParaManager = new FormDBSystemParaManager();
            tFormDBSystemParaManager.ShowDialog();
        }
    }
}
