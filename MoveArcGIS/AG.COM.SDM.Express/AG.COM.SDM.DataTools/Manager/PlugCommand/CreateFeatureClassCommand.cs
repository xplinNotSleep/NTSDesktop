using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.SystemUI;
using System.Drawing;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// ����Ҫ��������
    /// </summary>
    public sealed class CreateFeatureClassCommand : BaseCommand, IUseIcon
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CreateFeatureClassCommand()
        {
            base.m_caption = "����Ҫ����";
            base.m_toolTip = "����Ҫ����";
            base.m_category = "���ݹ�����";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C04.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C04_32.ico"));  
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
            FormCreateFeatureClass frm = new FormCreateFeatureClass();         
            frm.ShowDialog(); 
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {    
        }
    }
}
