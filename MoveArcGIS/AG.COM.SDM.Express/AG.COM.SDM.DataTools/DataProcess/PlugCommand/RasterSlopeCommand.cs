using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// �¶ȼ�������
    /// </summary>
    public sealed class RasterSlopeCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_HookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public RasterSlopeCommand()
        {
            base.m_caption = "�¶�ͼ����";
            base.m_toolTip = "";
            base.m_message = "";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C17+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C17+_32.ico"));       
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
            FormRasterSlope tFormRasterSlope = new FormRasterSlope(this.m_HookHelper.FocusMap);
            tFormRasterSlope.ShowInTaskbar = false;
            tFormRasterSlope.ShowDialog();
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook"></param>
        public override void OnCreate(object hook)
        {
            this.m_HookHelper.Hook = hook;
        }
    }
}
