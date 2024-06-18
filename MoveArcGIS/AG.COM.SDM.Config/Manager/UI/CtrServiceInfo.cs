using System;
using System.Windows.Forms;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 服务数据信息
    /// </summary>
    public partial class CtrServiceInfo : UserControl
    {
        private AGSDM_DATA_SERVICE m_DataService = new AGSDM_DATA_SERVICE();

        public CtrServiceInfo()
        {
            InitializeComponent();
        }

        private void CtrServiceInfo_Load(object sender, EventArgs e)
        {
          
        }

        //应用
        private void btnApply_Click(object sender, EventArgs e)
        {
            m_DataService.SERVICENAME = this.tbServiceName.Text.Trim();
            m_DataService.URL = this.tbUrl.Text;
            m_DataService.USERNAME = this.tbAccount.Text;
            m_DataService.PASSWORD = this.tbPassword.Text;
            m_DataService.DESCRIPTION = this.tbDescription.Text;
        }

        /// <summary>
        /// 设置数据服务信息
        /// </summary>
        /// <param name="pDataService"></param>
        public void SetDataService(AGSDM_DATA_SERVICE pDataService)
        {
            m_DataService = pDataService;
            this.tbServiceName.Text = m_DataService.SERVICENAME;
            this.tbAccount.Text = m_DataService.USERNAME;
            this.tbPassword.Text = m_DataService.PASSWORD;
            this.tbUrl.Text = m_DataService.URL;
            this.tbDescription.Text = m_DataService.DESCRIPTION;
        }
    }
}
