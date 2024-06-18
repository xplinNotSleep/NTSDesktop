using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.Plugins.Common
{
    public class CmdTheme2013Gray : BaseCommand, IUseImage
    {
        private IHookHelper m_HookHelper = new HookHelper();

        public CmdTheme2013Gray()
        {
            this.m_caption = "Office 2013 灰";
            this.m_name = GetType().Name;
            this.m_toolTip = "Office 2013 灰";
            this.m_message = "Office 2013 灰";

            m_Image16 = AG.COM.SDM.Plugins.Properties.Resources.lan16;
            m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.lan32;
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
                DevThemeHelper.ChangeTheme(CommonConstString.STR_TempPath,
                CommonVariables.ConfigFile, "Office 2010 Blue", CommonVariables.CurrentSkinName);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
