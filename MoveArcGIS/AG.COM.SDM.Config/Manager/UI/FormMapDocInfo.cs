using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 地图文档信息窗体类
    /// </summary>
    public partial class FormMapDocInfo : Form
    {
        private EnumOperateState m_OperateState=EnumOperateState.Query;    //操作类型
        private MapDocInfo m_MapDocInfo = new MapDocInfo();
        private IList<string> m_RoleIDs;           //权限编号数组

        public FormMapDocInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置操作状态信息
        /// </summary>
        public EnumOperateState OperateState
        {
            get
            {
                return this.m_OperateState;
            }
            set
            {
                this.m_OperateState = value;
            }
        }

        /// <summary>
        /// 获取或设置地图文档信息
        /// </summary>
        public MapDocInfo MapDocInfo
        {
            get
            {
                return this.m_MapDocInfo;
            }
            set
            {
                this.m_MapDocInfo = value;
            }
        }

        private void FormMapDocInfo_Load(object sender, EventArgs e)
        {
            if (this.m_OperateState == EnumOperateState.Query)
            {              
                this.btnOK.Visible = false;
                this.btnLocate.Visible = false;
                //初始化用户信息
                InitializeMapDocInfo();
            }
            else if (this.m_OperateState == EnumOperateState.Modify)
            {
                //设置输入焦点
                this.txtName.Focus();
                //初始化用户信息
                InitializeMapDocInfo();
            }
            else
            {
                //设置输入焦点
                this.txtName.Focus();
                this.txtAppID.Text = this.m_MapDocInfo.AppID;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtName.Name.Length == 0)
            {
                MessageBox.Show("应用工程名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtName.Focus();
                return;
            }

            this.m_MapDocInfo.AppID = this.txtAppID.Text;
            this.m_MapDocInfo.AppName = this.txtName.Text;
            this.m_MapDocInfo.Description = this.txtDescription.Text;
            this.m_MapDocInfo.IsActive = this.chkActive.Checked;

            IList<RoleInfo> tListRoleInfo = new List<RoleInfo>();

            for (int i = 0; i < this.chkListRole.CheckedItems.Count; i++)
            {
                tListRoleInfo.Add(this.chkListRole.CheckedItems[i] as RoleInfo);
            }

            this.m_MapDocInfo.ListRoleInfo = tListRoleInfo;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLocate_Click(object sender, EventArgs e)
        {
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Filter = "地图文档类型 (*.mxd)|*.mxd";

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtAppData.Text = tOpenFileDialog.FileName;

                //初始化MemeoryBlobStreamClass对象
                IMemoryBlobStream tMemoryBlobStrem = new MemoryBlobStreamClass();
                tMemoryBlobStrem.LoadFromFile(tOpenFileDialog.FileName);

                this.m_MapDocInfo.AppData = tMemoryBlobStrem;
                this.m_MapDocInfo.DataBrowserName = tOpenFileDialog.FileName;
            }
        } 

        /// <summary>
        /// 设置角色信息
        /// </summary>
        /// <param name="pListRoleInfo">角色信息</param>
        public void SetRolesInfo(IList<RoleInfo> pListRoleInfo)
        {
            if (pListRoleInfo == null) return;

            //初始化权限编号数组
            this.m_RoleIDs = new List<string>();

            for (int i = 0; i < pListRoleInfo.Count; i++)
            {
                //添加角色信息
                this.chkListRole.Items.Add(pListRoleInfo[i]);

                this.m_RoleIDs.Add(pListRoleInfo[i].RoleID);
            }
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitializeMapDocInfo()
        {
            this.txtAppID.Text = this.m_MapDocInfo.AppID;
            this.txtName.Text = this.m_MapDocInfo.AppName;       
            this.txtDescription.Text = this.m_MapDocInfo.Description;
            this.txtAppData.Text = this.m_MapDocInfo.DataBrowserName;
            this.chkActive.Checked = this.m_MapDocInfo.IsActive;

            try
            {
                IList<RoleInfo> tListRoleInfo = this.m_MapDocInfo.ListRoleInfo;
                if (tListRoleInfo == null) return;

                int index = -1;
                for (int i = 0; i < tListRoleInfo.Count; i++)
                {
                    index = this.m_RoleIDs.IndexOf(tListRoleInfo[i].RoleID);
                    if (index > -1)
                    {
                        this.chkListRole.SetItemChecked(index, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}