using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace AG.COM.SDM.Config
{
    public partial class FormMapServiceInfo : Form
    {
        #region 变量

        /// <summary>
        /// 当前Form使用情况
        /// </summary>
        private InfoFormUseState m_UseState = InfoFormUseState.View;

        /// <summary>
        /// 当前操作的AGSDM_DATA_SERVICE
        /// </summary>
        public AGSDM_DATA_SERVICE CurrentDataService
        {
            get;
            set;
        }

        #endregion

        #region 初始化

        public FormMapServiceInfo(InfoFormUseState tUseState)
        {
            InitializeComponent();

            m_UseState = tUseState;
        }

        private void FormMapServiceInfo_Load(object sender, EventArgs e)
        {
            try
            {
                //为了触发CheckedChange事件
                rdoInternet.Checked = true;

                if (m_UseState == InfoFormUseState.Edit || m_UseState == InfoFormUseState.View)
                {
                    txtServiceName.Text = CurrentDataService.SERVICENAME;                  
                    txtDescription.Text = CurrentDataService.DESCRIPTION;
                    if (CurrentDataService.SERVERTYPE == 1)
                    {
                        rdoLocal.Checked = true;
                        txtHostName.Text = CurrentDataService.URL;
                    }
                    else
                    {
                        rdoInternet.Checked = true;
                        txtUrl.Text = CurrentDataService.URL;
                        txtUserName.Text = CurrentDataService.USERNAME;
                        txtPassword.Text = CurrentDataService.PASSWORD;
                    }

                    //在查看是不用获取所有服务
                    if (m_UseState != InfoFormUseState.View)
                    {
                        List<string> tMapServers = MapServiceHelper.GetAllMapServerNames(txtUrl.Text, txtUserName.Text, txtPassword.Text, rdoLocal.Checked);
                        cmbMapService.DataSource = tMapServers;
                    }
                    cmbMapService.Text = CurrentDataService.MAPNAME;

                    if (m_UseState == InfoFormUseState.View)
                    {                     
                        ControlHelper.EnabledAllControls(this.Controls, false);
                        btnCancel.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
                Close();
            }
        }

        #endregion

        #region 保存

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                AGSDM_DATA_SERVICE tDataService = CurrentDataService;
                if (tDataService == null)
                    tDataService = new AGSDM_DATA_SERVICE();

                tDataService.SERVICENAME = txtServiceName.Text;
                tDataService.MAPNAME = cmbMapService.Text;
                tDataService.DESCRIPTION = txtDescription.Text;
                if (rdoInternet.Checked == true)
                {
                    tDataService.SERVERTYPE = 0;
                    tDataService.URL = txtUrl.Text;
                    tDataService.USERNAME = txtUserName.Text;
                    tDataService.PASSWORD = txtPassword.Text;
                }
                else
                {
                    tDataService.SERVERTYPE = 1;
                    tDataService.URL = txtHostName.Text;
                }

                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                if (tDataService.ID > 0)
                {
                    tEntityHandler.UpdateEntity(tDataService, tDataService.ID);
                }
                else
                {
                    tEntityHandler.AddEntity(tDataService);
                }
                CurrentDataService = tDataService;

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtServiceName.Text, "服务别名") &&
             ValidateData.StringLength(txtServiceName.Text, 50, "服务别名") &&
 ValidateData.StringLength(txtUrl.Text, 200, "服务器地址") &&
  ValidateData.StringLength(txtUserName.Text, 50, "用户名") &&
   ValidateData.StringLength(txtPassword.Text, 50, "密码") &&
     ValidateData.StringLength(txtHostName.Text, 200, "主机名") &&
   ValidateData.StringLength(cmbMapService.Text, 100, "地图服务") &&
   ValidateData.StringLength(txtDescription.Text, 200, "描述");
        }

        #endregion

        #region 其他

        private void rdoInternet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoInternet.Checked == true)
            {
                txtUrl.Enabled = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;

                txtHostName.Enabled = false;
            }
            else
            {
                txtUrl.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;

                txtHostName.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefreshMapService_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoInternet.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtUrl.Text))
                    {
                        MessageBox.Show("请输入服务器地址");
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtHostName.Text))
                    {
                        MessageBox.Show("请输入主机名");
                        return;
                    }
                }

                List<string> tMapServers = MapServiceHelper.GetAllMapServerNames(txtUrl.Text, txtUserName.Text, txtPassword.Text, rdoLocal.Checked);
                cmbMapService.DataSource = tMapServers;               
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
       
        #endregion               
    }
}
