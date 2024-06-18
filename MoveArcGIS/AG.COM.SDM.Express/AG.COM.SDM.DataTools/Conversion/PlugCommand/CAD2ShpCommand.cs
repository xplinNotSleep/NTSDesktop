using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    public sealed class CAD2ShpCommand : BaseCommand
    {
        private FormCAD2Shp m_frmCADTranform;

        public CAD2ShpCommand()
        {
            this.m_name = "CADConvertToShp";
            this.m_caption = "CAD转SHP";
            this.m_toolTip = "将CAD文件转化为Shp图层";
            this.m_message = "将CAD文件转化为Shp图层";
        }

        public override void OnClick()
        {
            try
            {
                this.m_frmCADTranform = new FormCAD2Shp();
                this.m_frmCADTranform.ShowDialog();
            }
            catch
            {
                this.m_frmCADTranform = null;
            }
        }

        public override void OnCreate(object hook)
        {
        }
    }
}
