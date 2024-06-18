using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// ϵͳע�������
    /// </summary>
    public sealed class ReLogin : BaseCommand, IUseIcon 
    {
        IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ReLogin()
        {
            base.m_caption = "ע��";
            base.m_toolTip = "ע��";
            base.m_name = GetType().FullName;
            base.m_message = "ע��";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "ReLogin.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "ReLogin_32.ico")); 
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// icoͼ�����16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// icoͼ�����32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            if(MessageBox.Show("�Ƿ���������?","��ʾ",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
            {
                return;
            }

            string strUserFile = CommonConstString.STR_ConfigPath + "\\Restart.txt";

            using (StreamWriter sw = File.CreateText(strUserFile))
            {
                sw.WriteLine("1");
            }

            CommonVariables.IsClosed = true;

            Application.Restart();         
        }

        /// <summary>
        /// �������󷽷�
        /// </summary>
        /// <param name="hook">����</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
