using System;
using System.Windows.Forms;
using AG.COM.SDM.Model;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Arcgis server服务参数设置控件类
    /// </summary>
    public partial class CtrArcgisServer : UserControl
    {
        private AGSDM_DATA_SERVICE m_DataService = new AGSDM_DATA_SERVICE();

        public CtrArcgisServer()
        {
            InitializeComponent();
        }

        public CtrArcgisServer(AGSDM_DATA_SERVICE dataServerEntity)
        {
            InitializeComponent();
            m_DataService = dataServerEntity;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (m_DataService == null)
                m_DataService = new AGSDM_DATA_SERVICE();
            m_DataService.SERVICENAME = this.tbServiceName.Text.Trim();
            m_DataService.URL = this.tbUrl.Text;
            m_DataService.USERNAME = this.tbAccount.Text;
            m_DataService.PASSWORD = this.tbPassword.Text;
            m_DataService.DESCRIPTION = this.tbDescription.Text;
            m_DataService.MAPNAME = this.cboMapName.Text;
            m_DataService.SERVERTYPE = this.cboServiceType.SelectedIndex; 
        }

        private void cboServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboServiceType.SelectedIndex == 0)
            {
                this.cboMapName.Enabled = true;
                this.label4.Enabled = true;
            }
            else
            {
                this.cboMapName.Enabled = false;
                this.label4.Enabled = false;
            }
        }

        /// <summary>
        /// 设置数据服务信息
        /// </summary>
        /// <param name="pDataService"></param>
        public void SetDataService(AGSDM_DATA_SERVICE pDataService)
        { 
            //应用上一次的结果
            //btnApply_Click(null,null);
            if (pDataService == null)
                return;
            m_DataService = pDataService;
            this.tbServiceName.Text = m_DataService.SERVICENAME;
            this.tbAccount.Text = m_DataService.USERNAME;
            this.tbPassword.Text = m_DataService.PASSWORD;
            if (m_DataService.SERVERTYPE == 0)
            {
                this.cboServiceType.SelectedIndex = 0;
            }
            else
            {
                this.cboServiceType.SelectedIndex = 1;
            }
            this.tbUrl.Text = m_DataService.URL;
            this.tbDescription.Text = m_DataService.DESCRIPTION;
            this.cboMapName.Text = m_DataService.MAPNAME;
        }

        /// <summary>
        /// 更新地图名称列表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        private void UpdateMapnameList(string url,string user,string password)
        {
            IAGSServerConnectionFactory connFac = new AGSServerConnectionFactoryClass();
            IPropertySet propertySet = new PropertySetClass();
            try
            {
                propertySet.SetProperty("url", url);
                propertySet.SetProperty("user", user == null ? string.Empty : user);
                propertySet.SetProperty("password", password == null ? string.Empty : password);
                IAGSServerConnection conn = connFac.Open(propertySet, 0);
                IAGSEnumServerObjectName names = conn.ServerObjectNames;
                IAGSServerObjectName serviceObjName = null;
                IAGSServerObjectName temObjName = null;
                names.Reset();
                while ((temObjName = names.Next()) != null)
                {
                    if (!cboMapName.Items.Contains(temObjName.Name))
                    {
                        cboMapName.Items.Add(temObjName.Name);
                    }
                }
            }
            catch
            {
                //do nothing
            }
        }

        private void cboMapName_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateMapnameList(this.tbUrl.Text.Trim(), this.tbAccount.Text.Trim(), this.tbPassword.Text.Trim());
        }

        private void cboServiceType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboServiceType.SelectedIndex == 0)
            {
                lblUrlSample.Text = @"http://www.myserver.com/arcgis/services";
            }
            else if(cboServiceType.SelectedIndex == 1)
            {
                lblUrlSample.Text = @"http://www.myserver.com/arcgis/services/mymap/MapServer/WMSServer?";
            }
        }
       
    }
}
