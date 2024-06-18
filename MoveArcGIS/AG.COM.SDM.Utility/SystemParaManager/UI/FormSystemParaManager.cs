using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    public partial class FormSystemParaManager : Form
    {
        #region 初始化

        public FormSystemParaManager()
        {
            InitializeComponent();
        }

        private void FormPrintGeneralParaSet_Load(object sender, EventArgs e)
        {
            try
            {           
                List<SystemPara> tSystemParas = SystemParaHelper.ReadFromXml();
                foreach (SystemPara tPrintGeneralPara in tSystemParas)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = tPrintGeneralPara.Name;
                    lvItem.SubItems.Add(tPrintGeneralPara.Value);
                    lvItem.SubItems.Add(tPrintGeneralPara.Description);
                    lvItem.Tag = tPrintGeneralPara;

                    lvwGeneralPara.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion      

        #region 增删改

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormSystemParaInfo tFormSystemParaInfo = new FormSystemParaInfo(InfoFormUseState.Add);
                if (tFormSystemParaInfo.ShowDialog() == DialogResult.OK)
                {
                    SystemPara tSystemPara = tFormSystemParaInfo.CurrentSystemPara;

                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = tSystemPara.Name;
                    lvItem.SubItems.Add(tSystemPara.Value);
                    lvItem.SubItems.Add(tSystemPara.Description);
                    lvItem.Tag = tSystemPara;

                    lvwGeneralPara.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lvItem = lvwGeneralPara.SelectedItems.Count > 0 ? lvwGeneralPara.SelectedItems[0] : null;
                if (lvItem == null) return;

                FormSystemParaInfo tFormSystemParaInfo = new FormSystemParaInfo(InfoFormUseState.Edit);
                tFormSystemParaInfo.CurrentSystemPara = lvItem.Tag as SystemPara;
                if (tFormSystemParaInfo.ShowDialog() == DialogResult.OK)
                {
                    SystemPara tSystemPara = tFormSystemParaInfo.CurrentSystemPara;

                    lvItem.Text = tSystemPara.Name;
                    lvItem.SubItems[1].Text = tSystemPara.Value;
                    lvItem.SubItems[2].Text = tSystemPara.Description;
                    lvItem.Tag = tSystemPara;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
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
                ListViewItem lvItem = lvwGeneralPara.SelectedItems.Count > 0 ? lvwGeneralPara.SelectedItems[0] : null;
                if (lvItem == null) return;

                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lvwGeneralPara.Items.Remove(lvItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        private void Save()
        {
            List<SystemPara> tPrintGeneralParas = new List<SystemPara>();

            foreach (ListViewItem lvItem in lvwGeneralPara.Items)
            {
                if (lvItem.Tag is SystemPara)
                {
                    tPrintGeneralParas.Add(lvItem.Tag as SystemPara);
                }
            }

            //保存到资源文件
            SystemParaHelper.SaveToFile(tPrintGeneralParas);

            AG.COM.SDM.Utility.MessageHandler.ShowInfoMsg("保存成功", "提示");
        }

        #endregion      
    }
}
