using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// �˳����������
    /// </summary>
    public class Exit : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Exit()
        {
            base.m_caption = "�˳�";
            base.m_toolTip = "�˳�";
            base.m_name = GetType().FullName;
            base.m_message = "�˳�";
            base.m_category = "�ļ�";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "Close16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "Close32.ico"));         
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
            if(MessageBox.Show("�Ƿ�رճ���?", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CommonVariables.IsClosed = true;

            //�ر�Ӧ�ó���
            Process.GetCurrentProcess().CloseMainWindow();
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">����</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }   
    }
}
