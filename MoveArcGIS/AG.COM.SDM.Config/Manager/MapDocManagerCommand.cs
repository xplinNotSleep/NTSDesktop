using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.Config.Manager
{
    public class MapDocManagerCommand:BaseCommand
    {
        /// <summary>
        /// Ĭ�Ϲ��캯�� 
        /// </summary>
        public MapDocManagerCommand()
        {
            base.m_caption = "��ͼ�ĵ�����";
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
