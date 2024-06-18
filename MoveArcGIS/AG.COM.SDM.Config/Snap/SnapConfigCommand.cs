using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ��׽����
    /// </summary>
    public sealed class SnapConfigCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelper;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public SnapConfigCommand()
        {             
            m_hookHelper = new HookHelperEx();
            this.m_caption = "��׽����";
            base.m_name = GetType().FullName;
            this.m_message = "��׽����";
            this.m_toolTip = "��׽����";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G7.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantConfig.STR_IMAGEPATH + "G7+_32.ico"));        
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
        /// ��ȡ��ǰ����״̬
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
        /// ����ʱ�ĳ�ʼ��ֵ
        /// </summary>
        /// <param name="hook">���ݶ���</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;    
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        public override void OnClick()
        {
            FormSnap frm = new FormSnap();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog();
        }
    }
}
