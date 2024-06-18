using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.Config.Manager
{
    public class MapDocManagerCommand:BaseCommand
    {
        /// <summary>
        /// 默认构造函数 
        /// </summary>
        public MapDocManagerCommand()
        {
            base.m_caption = "地图文档管理";
            base.m_name = "MapDocManagerCommand";
        }

        public override void OnClick()
        {
            FormMapDocManager tFrm = new FormMapDocManager();
            tFrm.ShowInTaskbar = false;
            tFrm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
        }
    }
}
