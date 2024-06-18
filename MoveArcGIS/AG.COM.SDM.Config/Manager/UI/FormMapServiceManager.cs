using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    public partial class FormMapServiceManager : Form
    {
        #region  初始化

        public FormMapServiceManager()
        {
            InitializeComponent();
        }

        private void FormMapServiceManager_Load(object sender, EventArgs e)
        {
            try
            {
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                IList<AGSDM_DATA_SERVICE> tServices = tEntityHandler.GetEntities<AGSDM_DATA_SERVICE>("from AGSDM_DATA_SERVICE");
                foreach (AGSDM_DATA_SERVICE tService in tServices)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = tService.SERVICENAME;
                    lvItem.SubItems.Add(tService.MAPNAME);
                    lvItem.SubItems.Add(tService.URL);
                    lvItem.Tag = tService;
                    lvwMapService.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        #endregion

        #region 增删改查

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormMapServiceInfo tFormMapServiceInfo = new FormMapServiceInfo(InfoFormUseState.Add);
                if (tFormMapServiceInfo.ShowDialog() == DialogResult.OK)
                {
                    AGSDM_DATA_SERVICE tDataService = tFormMapServiceInfo.CurrentDataService;

                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = tDataService.SERVICENAME;
                    lvItem.SubItems.Add(tDataService.MAPNAME);
                    lvItem.SubItems.Add(tDataService.URL);
                    lvItem.Tag = tDataService;
                    lvwMapService.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwMapService.SelectedItems.Count > 0 ? lvwMapService.SelectedItems[0] : null;
                if (lvItem == null) return;

                FormMapServiceInfo tFormMapServiceInfo = new FormMapServiceInfo(InfoFormUseState.Edit);
                tFormMapServiceInfo.CurrentDataService = lvItem.Tag as AGSDM_DATA_SERVICE;
                if (tFormMapServiceInfo.ShowDialog() == DialogResult.OK)
                {
                    AGSDM_DATA_SERVICE tDataService = tFormMapServiceInfo.CurrentDataService;

                    lvItem.Text = tDataService.SERVICENAME;
                    lvItem.SubItems[1].Text = tDataService.MAPNAME;
                    lvItem.SubItems[2].Text = tDataService.URL;
                    lvItem.Tag = tDataService;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwMapService.SelectedItems.Count > 0 ? lvwMapService.SelectedItems[0] : null;
                if (lvItem == null) return;

                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AGSDM_DATA_SERVICE tDataService = lvItem.Tag as AGSDM_DATA_SERVICE;
                    EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                    tEntityHandler.DeleteEntity(tDataService);

                    lvwMapService.Items.Remove(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwMapService.SelectedItems.Count > 0 ? lvwMapService.SelectedItems[0] : null;
                if (lvItem == null) return;

                FormMapServiceInfo tFormMapServiceInfo = new FormMapServiceInfo(InfoFormUseState.View);
                tFormMapServiceInfo.CurrentDataService = lvItem.Tag as AGSDM_DATA_SERVICE;
                if (tFormMapServiceInfo.ShowDialog() == DialogResult.OK)
                { }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwMapService.SelectedItems.Count > 0 ? lvwMapService.SelectedItems[0] : null;
                if (lvItem == null) return;

                AGSDM_DATA_SERVICE tService = lvItem.Tag as AGSDM_DATA_SERVICE;
                if (string.IsNullOrEmpty(tService.MAPNAME)) return;

                FormMapServicePreview tFormMapServicePreview = new FormMapServicePreview();
                tFormMapServicePreview.CurrentDataService = tService;
                tFormMapServicePreview.ShowDialog();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        #endregion

        #region 服务启动

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                FormMapServiceAdmin tFormMapServiceAdmin = new FormMapServiceAdmin();
                tFormMapServiceAdmin.ShowDialog();
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
