using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Model;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    public partial class FormDeptUser : Form
    {
        AGSDM_ORG m_Org = null;

        public  List<AGSDM_SYSTEM_USER> DeptUsers { get; set; }

        public FormDeptUser(AGSDM_ORG org)
        {
            InitializeComponent();

            m_Org = org;
        }

        private void FormDeptUser_Load(object sender, EventArgs e)
        {
            EntityHandler entityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            List<AGSDM_SYSTEM_USER> systemUserLst = entityHandler.GetEntitiesAll<AGSDM_SYSTEM_USER>().ToList();
            List<AGSDM_USER_ORG> userOrgLst = entityHandler.GetEntitiesAll<AGSDM_USER_ORG>().ToList();

            foreach (AGSDM_SYSTEM_USER user in systemUserLst)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = user.NAME_EN;
                lvItem.Tag = user;

                var find = userOrgLst.Find(f => f.USER_ID == user.USER_ID.ToString() && f.ORG_ID == m_Org.ORG_ID.ToString());             
                lvItem.Checked = (find != null);

                lvDeptUser.Items.Add(lvItem);
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }

        private void Save()
        {
            EntityHandler entityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            //删除旧的
            entityHandler.DeleteEntity<AGSDM_USER_ORG>("from AGSDM_USER_ORG where ORG_ID ='" + m_Org.ORG_ID.ToString() + "'");
            List<AGSDM_SYSTEM_USER> deptUserLst = new List<AGSDM_SYSTEM_USER>();//部门用户，勾选的用户
            foreach (ListViewItem lvItem in lvDeptUser.Items)
            {
                if (lvItem.Checked)
                {
                    AGSDM_SYSTEM_USER systemUser = lvItem.Tag as AGSDM_SYSTEM_USER;
                    AGSDM_USER_ORG tUserOrg = new AGSDM_USER_ORG();
                    tUserOrg.USER_ID = systemUser.USER_ID.ToString();
                    tUserOrg.ORG_ID = m_Org.ORG_ID.ToString();
                    entityHandler.AddEntity(tUserOrg);

                    deptUserLst.Add(systemUser);
                }
            }

            DeptUsers = deptUserLst;
        }


    }
}
