using AG.COM.SDM.Config;
using AG.COM.SDM.Config.DbConnUI;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Manager
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //FormUIDesignManager tFormUIDesignManager = new FormUIDesignManager();
            //tFormUIDesignManager.Show(this.dockPanel1); 

            FormMenuConfig form = new FormMenuConfig();
            form.Show(this.dockPanel1); 
        }

        private void ToolStartItem_Click(object sender, EventArgs e)
        {
            FormStart tFormStart = new FormStart();
            tFormStart.ShowDialog();
        }

        private void ToolSysItem_Click(object sender, EventArgs e)
        {
            FrmDbOptions tFormOption = new FrmDbOptions(true);
            tFormOption.Text = "菜单配置数据库";
            if (tFormOption.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DatabaseHelper.RefreshAGSDMSessionFactory();
                }
                catch (Exception ex)
                {
                    AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                    MessageBox.Show(ex.Message, "错误");
                }
            }
        }

        private void ToolDictItem_Click(object sender, EventArgs e)
        {
            FormKeyDict tFormKeyDict = new FormKeyDict();
            tFormKeyDict.Show(this.dockPanel1, DockState.Document);
        }

        private void ToolExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 插件信息列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowAllCommand tFormShowAllCommand = new FormShowAllCommand();
            tFormShowAllCommand.ShowDialog(); 
        }

        private void ToolMainExpressItem_Click(object sender, EventArgs e)
        {
            FormLoginSetting frm = new FormLoginSetting();
            frm.ShowDialog();
        }
    }
}