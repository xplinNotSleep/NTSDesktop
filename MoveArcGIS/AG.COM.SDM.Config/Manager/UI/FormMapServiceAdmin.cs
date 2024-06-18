using ESRI.ArcGIS.Server;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    public partial class FormMapServiceAdmin : Form
    {
        private IServerObjectAdmin6 m_ServerObjectAdmin = null;

        public FormMapServiceAdmin()
        {
            InitializeComponent();
        }

        private void FormMapServiceAdmin_Load(object sender, EventArgs e)
        {

        }       

        private void btnGetService_Click(object sender, EventArgs e)
        {
            try
            {
                m_ServerObjectAdmin = null;

                string tUrl = txtUrl.Text;
                string tHost = txtHostName.Text;

                IGISServerConnection tGISServerConnection = new GISServerConnection();
                tGISServerConnection.Connect(tHost);

                m_ServerObjectAdmin = tGISServerConnection.ServerObjectAdmin as IServerObjectAdmin6;
                Refresh(m_ServerObjectAdmin);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void Refresh(IServerObjectAdmin6 tServerObjectAdmin)
        {
            lvwService.Items.Clear();

            IEnumServerObjectConfiguration tEnumConfig = tServerObjectAdmin.GetConfigurations();
            IServerObjectConfigurationStatus tServerObjectConfigurationStatus = null;
            IServerObjectConfiguration tServerObjectConfiguration = tEnumConfig.Next();
            while (tServerObjectConfiguration != null)
            {
                tServerObjectConfigurationStatus = tServerObjectAdmin.GetConfigurationStatus(tServerObjectConfiguration.Name, tServerObjectConfiguration.TypeName);

                ListViewItem lvItem = new ListViewItem(tServerObjectConfiguration.Name);
                string tState = "";
                switch (tServerObjectConfigurationStatus.Status)
                {
                    case esriConfigurationStatus.esriCSDeleted:
                        tState = "删除";
                        break;
                    case esriConfigurationStatus.esriCSPaused:
                        tState = "暂停";
                        break;
                    case esriConfigurationStatus.esriCSStarted:
                        tState = "启动";
                        break;
                    case esriConfigurationStatus.esriCSStarting:
                        tState = "启动中";
                        break;
                    case esriConfigurationStatus.esriCSStopped:
                        tState = "停止";
                        break;
                    case esriConfigurationStatus.esriCSStopping:
                        tState = "停止中";
                        break;
                    default:
                        tState = "未知";
                        break;
                }
                lvItem.SubItems.Add(tState);
                lvItem.Tag = tServerObjectConfiguration;

                lvwService.Items.Add(lvItem);

                tServerObjectConfiguration = tEnumConfig.Next();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwService.SelectedItems.Count > 0 ? lvwService.SelectedItems[0] : null;
                if (lvItem == null) return;

                if (m_ServerObjectAdmin == null) return;

                IServerObjectConfiguration tServerObjectConfiguration = lvItem.Tag as IServerObjectConfiguration;

                m_ServerObjectAdmin.StartConfiguration(tServerObjectConfiguration.Name, tServerObjectConfiguration.TypeName);

                Refresh(m_ServerObjectAdmin);
                lvwService_ItemSelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg("服务操作失败，原因可能是：" + ex.Message, "错误");
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwService.SelectedItems.Count > 0 ? lvwService.SelectedItems[0] : null;
                if (lvItem == null) return;

                if (m_ServerObjectAdmin == null) return;

                IServerObjectConfiguration tServerObjectConfiguration = lvItem.Tag as IServerObjectConfiguration;

                m_ServerObjectAdmin.PauseConfiguration(tServerObjectConfiguration.Name, tServerObjectConfiguration.TypeName);

                Refresh(m_ServerObjectAdmin);
                lvwService_ItemSelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg("服务操作失败，原因可能是：" + ex.Message, "错误");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwService.SelectedItems.Count > 0 ? lvwService.SelectedItems[0] : null;
                if (lvItem == null) return;

                if (m_ServerObjectAdmin == null) return;

                IServerObjectConfiguration tServerObjectConfiguration = lvItem.Tag as IServerObjectConfiguration;

                m_ServerObjectAdmin.StopConfiguration(tServerObjectConfiguration.Name, tServerObjectConfiguration.TypeName);

                Refresh(m_ServerObjectAdmin);
                lvwService_ItemSelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg("服务操作失败，原因可能是：" + ex.Message, "错误");
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            TextChangeEvent();
        }

        private void txtHostName_TextChanged(object sender, EventArgs e)
        {
            TextChangeEvent();
        }

        private void TextChangeEvent()
        {
            if (!string.IsNullOrEmpty(txtHostName.Text) && !string.IsNullOrEmpty(txtUrl.Text))
            {
                btnGetService.Enabled = true;
            }
            else
            {
                btnGetService.Enabled = false;
            }
        }

        private void lvwService_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwService.SelectedItems.Count > 0 ? lvwService.SelectedItems[0] : null;
                if (lvItem == null)
                {
                    btnStart.Enabled = false;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;

                    return;
                }

                string tState = lvItem.SubItems[1].Text;
                btnStart.Enabled = (tState == "停止" || tState == "暂停");
                btnPause.Enabled = (tState == "启动");
                btnStop.Enabled = (tState == "启动" || tState == "暂停");
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }    
    }
}
