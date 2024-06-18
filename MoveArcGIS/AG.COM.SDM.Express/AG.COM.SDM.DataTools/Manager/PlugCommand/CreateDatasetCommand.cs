using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.SystemUI;
using System.Drawing;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// �������ݼ������
    /// </summary>
    public sealed class CreateDatasetCommand : BaseCommand, IUseIcon
    { 
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CreateDatasetCommand()
        {
            base.m_caption = "�������ݼ�";
            base.m_toolTip = "�������ݼ�";
            base.m_category = "���ݹ�������";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C03.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C03_32.ico"));   
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
        /// �����¼���������
        /// </summary>
        public override void OnClick()
        {
            FormCreateDataset frm = new FormCreateDataset();         
            frm.ShowDialog(); 
        }

        /// <summary>
        /// ����ʱ��������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
           
        }
    }
}