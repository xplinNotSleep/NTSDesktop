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

namespace AG.COM.SDM.Config
{
    public partial class FormDBSystemParaManager : Form
    {
        #region 初始化

        public FormDBSystemParaManager()
        {
            InitializeComponent();
        }

        private void FormPrintGeneralParaSet_Load(object sender, EventArgs e)
        {
            try
            {
                EntityHandler entityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                IList<AGSDM_SYSTEMCONFIG> entities = entityHandler.GetEntities<AGSDM_SYSTEMCONFIG>("from AGSDM_SYSTEMCONFIG t ");

                foreach (AGSDM_SYSTEMCONFIG entity in entities)
                {
                    RefreshListViewItem(entity, null);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 刷新或添加一个实体对应的ListViewItem
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lvItem"></param>
        private void RefreshListViewItem(AGSDM_SYSTEMCONFIG entity, ListViewItem lvItem)
        {
            //true=新增，false=修改
            bool isAdd = lvItem == null ? true : false;

            if (lvItem == null)
            {
                lvItem = new ListViewItem();
                //注意：循环的最大数是listview的列数
                for (int i = 0; i < 3; i++)
                {
                    lvItem.SubItems.Add("");
                }

                lvItem.Tag = entity;
            }

            lvItem.SubItems[0].Text = entity.NAME;
            lvItem.SubItems[1].Text = entity.VALUE;
            lvItem.SubItems[2].Text = entity.DESCRIPTION;

            if (isAdd)
            {
                lvwMain.Items.Add(lvItem);
            }
        }

        #endregion

        #region 增删改

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormDBSystemParaInfo formDBSystemParaInfo = new FormDBSystemParaInfo(InfoFormUseState.Add);
                if (formDBSystemParaInfo.ShowDialog() == DialogResult.OK)
                {
                    AGSDM_SYSTEMCONFIG entity = formDBSystemParaInfo.CurrentEntity;

                    RefreshListViewItem(entity, null);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwMain.SelectedItems.Count > 0 ? lvwMain.SelectedItems[0] : null;
                if (lvItem == null) return;

                FormDBSystemParaInfo formDBSystemParaInfo = new FormDBSystemParaInfo(InfoFormUseState.Edit);
                formDBSystemParaInfo.CurrentEntity = lvItem.Tag as AGSDM_SYSTEMCONFIG;
                if (formDBSystemParaInfo.ShowDialog() == DialogResult.OK)
                {
                    AGSDM_SYSTEMCONFIG entity = formDBSystemParaInfo.CurrentEntity;

                    RefreshListViewItem(entity, lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void lvwGeneralPara_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //双击编辑
            btnEdit_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwMain.SelectedItems.Count > 0 ? lvwMain.SelectedItems[0] : null;
                if (lvItem == null) return;

                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EntityHandler entityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);                   
                    entityHandler.DeleteEntity(lvItem.Tag);

                    lvwMain.Items.Remove(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        #endregion
    }
}
