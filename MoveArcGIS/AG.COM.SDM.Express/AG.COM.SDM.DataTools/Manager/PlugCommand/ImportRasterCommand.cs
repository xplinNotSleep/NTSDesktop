using System.Drawing;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// ����դ�����ݲ��
    /// </summary>
    public class ImportRasterCommand : BaseCommand, IUseIcon
    {
        /// <summary>
        /// ��ʼ��ʵ������
        /// </summary>
        public ImportRasterCommand()
        {
            base.m_caption = "����դ������";
            base.m_toolTip = "����դ������";          
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C14.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C14_32.ico"));        
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
            FormImportRaster frm = new FormImportRaster();
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


