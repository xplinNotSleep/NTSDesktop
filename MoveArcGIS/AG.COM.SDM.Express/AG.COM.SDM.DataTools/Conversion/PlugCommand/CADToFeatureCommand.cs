using System.Drawing;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    public class CADToFeatureCommand : BaseCommand, IUseIcon
    {
        public CADToFeatureCommand()
        {
            base.m_caption = "CAD转GDB";
            base.m_toolTip = "CAD转GDB";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E3.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E3_32.ico"));         
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        public override void OnCreate(object hook)
        {

        }

        public override void OnClick()
        {
            FormCADToFeature tFormCADToFeature = new FormCADToFeature();
            tFormCADToFeature.ShowDialog();
        }
    }
}
