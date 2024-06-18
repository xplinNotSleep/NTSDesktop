using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// VCT��Shpת�� �����
    /// </summary>
    public sealed class VCT2SHPCommand : BaseCommand, IUseIcon
    {   
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public VCT2SHPCommand()
        {
            base.m_caption = "VCT��Shpת��";
            base.m_toolTip = "VCT��Shpת��";
            base.m_category = "����ת������";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E1+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E1+_32.ico"));        
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
            string fn = Application.StartupPath + "\\vct2shp.exe";
            if (System.IO.File.Exists(fn))
            {
                System.Diagnostics.Process.Start(fn);
            }
        }

        /// <summary>
        /// �������󷽷�
        /// </summary>
        /// <param name="hook"></param>
        public override void OnCreate(object hook)
        {
        } 
    }
}
