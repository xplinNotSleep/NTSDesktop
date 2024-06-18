using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
 
namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 测试WMS服务
    /// </summary>
    public class TestWMS
    {
        public void Test(IMapControl2 pMapControl)
        {
            IPropertySet pPropertyset = new PropertySetClass();
            pPropertyset.SetProperty("url", "http://sampleserver1.arcgisonline.com/arcgis/services/Specialty/ESRI_StatesCitiesRivers_USA/MapServer/WMSServer?");
            pPropertyset.SetProperty("User", "Administrator");
            pPropertyset.SetProperty("Password","");
            IWMSConnectionName pWmsConnectionName = new WMSConnectionNameClass();
            IWMSGroupLayer pWmsMapLayer = new WMSMapLayerClass();
            IDataLayer pDataLayer = pWmsMapLayer as IDataLayer;
            pDataLayer.Connect(pWmsConnectionName as IName);
            IWMSServiceDescription pWmsServiceDesc = pWmsMapLayer.WMSServiceDescription;
            for (int i = 0; i < pWmsServiceDesc.LayerDescriptionCount; i++)
            {
                IWMSLayerDescription pWmsLayerDesc = pWmsServiceDesc.get_LayerDescription(i);
                ILayer pNewLayer = null;
                if (pWmsLayerDesc.LayerDescriptionCount == 0)
                {
                    IWMSLayer pWmsLayer = pWmsMapLayer.CreateWMSLayer(pWmsLayerDesc);
                    pNewLayer = pWmsLayer as ILayer;
                }
                else
                {
                    IWMSGroupLayer pWmsGroupLayer = pWmsMapLayer.CreateWMSGroupLayers(pWmsLayerDesc);
                    pNewLayer = pWmsGroupLayer as ILayer;
                }
                pWmsMapLayer.InsertLayer(pNewLayer, 0);
            }
            pMapControl.AddLayer(pWmsMapLayer as ILayer,0); 
        }
    }
}
