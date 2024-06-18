using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// ��������GeoDatabase �����
    /// </summary>
    public class CreateLocalGeoDatabaseCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CreateLocalGeoDatabaseCommand()
        {
            base.m_caption = "��������GeoDatabase";
            base.m_toolTip = "��������GeoDatabase";
            base.m_category = "���ݹ�����";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C02.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C02_32.ico"));          
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
            //ʵ������������GDB������
            FormCreateLocalGeoDatabase frm = new FormCreateLocalGeoDatabase();
            frm.ShowDialog();
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;               
        } 
    }
}
