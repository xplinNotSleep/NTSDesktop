using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.ConnecteBorder
{
    /// <summary>
    /// ���ݽӱ� �����
    /// </summary>
    public sealed class ConnecteBorderCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_HookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ConnecteBorderCommand()
        {
            base.m_caption = "���ݽӱ�";
            base.m_name = GetType().FullName;
            base.m_toolTip = "���ݽӱ�";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E6+~.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E6+~_32.ico"));       
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
        /// �����¼�����
        /// </summary>
        public override void OnClick()
        {
            frmConnecteBorder frm = new frmConnecteBorder();
            frm.ShowDialog();
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Hook = hook;
        }
    }
}
