using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Model;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace AG.COM.SDM.Config
{
    public partial class FormMapServicePreview : Form
    {
        /// <summary>
        /// 当前操作的AGSDM_DATA_SERVICE
        /// </summary>
        public AGSDM_DATA_SERVICE CurrentDataService
        {
            get;
            set;
        }

        public FormMapServicePreview()
        {
            InitializeComponent();
        }

        private void FormMapServicePreview_Load(object sender, EventArgs e)
        {
            try
            {
                IMap tMap = mapMain.Map;
                IAGSServerConnectionFactory connFac = new AGSServerConnectionFactoryClass();
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("url", CurrentDataService.URL);
                propertySet.SetProperty("user", CurrentDataService.USERNAME == null ? string.Empty : CurrentDataService.USERNAME);
                propertySet.SetProperty("password", CurrentDataService.PASSWORD == null ? string.Empty : CurrentDataService.PASSWORD);
                IAGSServerConnection conn = connFac.Open(propertySet, 0);
                IAGSEnumServerObjectName names = conn.ServerObjectNames;
                IAGSServerObjectName serviceObjName = null;
                IAGSServerObjectName temObjName = null;
                names.Reset();
                while ((temObjName = names.Next()) != null)
                {
                    if (temObjName.Name == CurrentDataService.MAPNAME && temObjName.Type == "MapServer")
                    { serviceObjName = temObjName; break; }
                }
                if (serviceObjName == null)
                    return;               
                IMapServer mapServer = ((IName)serviceObjName).Open() as IMapServer;
                IMapServerLayer layer = new MapServerLayerClass();
                layer.ServerConnect(serviceObjName, mapServer.DefaultMapName);              
                tMap.AddLayer(layer as ILayer);

                mapMain.Refresh();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
    }
}
