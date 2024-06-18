using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// ��ͼ�ĵ���Ϣ������
    /// </summary>
    public partial class FormMapDocInfo : Form
    {
        private EnumOperateState m_OperateState=EnumOperateState.Query;    //��������
        private MapDocInfo m_MapDocInfo = new MapDocInfo();
        private IList<string> m_RoleIDs;           //Ȩ�ޱ������

        public FormMapDocInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ȡ�����ò���״̬��Ϣ
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
        /// ��ȡ�����õ�ͼ�ĵ���Ϣ
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
                //��ʼ���û���Ϣ
                InitializeMapDocInfo();
            }
            else if (this.m_OperateState == EnumOperateState.Modify)
            {
                //�������뽹��
                this.txtName.Focus();
                //��ʼ���û���Ϣ
                InitializeMapDocInfo();
            }
            else
            {
                //�������뽹��
                this.txtName.Focus();
                this.txtAppID.Text = this.m_MapDocInfo.AppID;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtName.Name.Length == 0)
            {
                MessageBox.Show("Ӧ�ù������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            tOpenFileDialog.Filter = "��ͼ�ĵ����� (*.mxd)|*.mxd";

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtAppData.Text = tOpenFileDialog.FileName;

                //��ʼ��MemeoryBlobStreamClass����
                IMemoryBlobStream tMemoryBlobStrem = new MemoryBlobStreamClass();
                tMemoryBlobStrem.LoadFromFile(tOpenFileDialog.FileName);

                this.m_MapDocInfo.AppData = tMemoryBlobStrem;
                this.m_MapDocInfo.DataBrowserName = tOpenFileDialog.FileName;
            }
        } 

        /// <summary>
        /// ���ý�ɫ��Ϣ
        /// </summary>
        /// <param name="pListRoleInfo">��ɫ��Ϣ</param>
        public void SetRolesInfo(IList<RoleInfo> pListRoleInfo)
        {
            if (pListRoleInfo == null) return;

            //��ʼ��Ȩ�ޱ������
            this.m_RoleIDs = new List<string>();

            for (int i = 0; i < pListRoleInfo.Count; i++)
            {
                //��ӽ�ɫ��Ϣ
                this.chkListRole.Items.Add(pListRoleInfo[i]);

                this.m_RoleIDs.Add(pListRoleInfo[i].RoleID);
            }
        }

        /// <summary>
        /// ��ʼ���û���Ϣ
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