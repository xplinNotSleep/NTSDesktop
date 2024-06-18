using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.COM.SDM.Utility;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;
using System.Drawing;
using AG.COM.SDM.Framework;

namespace AG.COM.SDM.Plugins.Demo
{
    public class CmdCommand : BaseCommand, IUseImage
    {
        private IHookHelper m_HookHelper = new HookHelper();

        public CmdCommand()
        {
            this.m_caption = "Command Demo";
            this.m_name = GetType().Name;
            this.m_toolTip = "Command Demo";
            this.m_message = "Command Demo";

            m_Image16 = AG.COM.SDM.Plugins.Properties.Resources.yin16;
            m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.yin32;
        }

        private Image m_Image16;
        private Image m_Image32;
        public Image Image16
        {
            get
            {
                return m_Image16;
            }
            set { m_Image16 = value; }
        }
        public Image Image32
        {
            get { return m_Image32; }
            set { m_Image16 = value; }
        }

        public override void OnClick()
        {
            try
            {
                AutoCloseMsgBox.Show("Hello World", "消息", 5000);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
