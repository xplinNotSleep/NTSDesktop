using AG.COM.SDM.Database;
using System;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// SDE�����û��ؼ���
    /// </summary>
    public partial class SpatialDbConfig : UserControl
    {
        /// <summary>
        /// ʵ�����¶���
        /// </summary>
        public SpatialDbConfig()
        {
            //��ʼ���������
            InitializeComponent();

            //this.txtInstance.Text = "sde:oracle11g:localhost/orcl";
            //this.txtDataBase.Text = "SDE";
            //this.txtUser.Text = "SDE";
            this.cboVersions.Text = "SDE.DEFAULT";
        }

        /// <summary>
        /// ��ȡ�����÷�����
        /// </summary>
        public DatabaseType DataBaseType
        {
            get { return (DatabaseType)Enum.Parse(typeof(DatabaseType), cmbDataType.Text, false); }
            set { cmbDataType.Text = value.ToString(); }
        }

        /// <summary>
        /// ��ȡ�����÷�����
        /// </summary>
        public string Server
        {
            get { return txtServerName.Text; }
            set { txtServerName.Text = value; }
        }

        /// <summary>
        /// ��ȡ�����ö˿ں�
        /// </summary>
        public string Port
        {
            get { return txtPort.Text; }
            set { txtPort.Text = value; }
        }

        /// <summary>
        /// ��ȡ������ʵ��(����ͨ���ݿ����ã���������ʵ��)
        /// </summary>
        //public string Instance
        //{
        //    get { return txtInstance.Text; }
        //    set { txtInstance.Text = value; }
        //}

        /// <summary>
        /// ��ȡ���������ݿ�
        /// </summary>
        public string DataBase
        {
            get { return txtDataBase.Text; }
            set { txtDataBase.Text = value; }
        }

        /// <summary>
        /// ��ȡ�������û���
        /// </summary>
        public string User
        {
            get { return txtUser.Text; }
            set { txtUser.Text = value; }
        }


        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public string Password
        {
            get { return txtPwd.Text; }
            set { txtPwd.Text = value; }
        }

        /// <summary>
        /// ��ȡ�����ð汾
        /// </summary>
        public string Version
        {
            get { return cboVersions.Text; }
            set { cboVersions.Text = value; }
        }
        

        /// <summary>
        /// �������������Ч��
        /// </summary>
        /// <returns>���ؼ����Ϣ</returns>
        public bool CheckInputValid()
        {
            StringBuilder tStrBuilder = new StringBuilder();
            if (txtServerName.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'Server' ");
            }

            if (txtPort.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'Service' ");
            }

            //if (txtDataBase.Text.Trim().Length == 0)
            //{
            //    tStrBuilder.Append("'DataBase' ");
            //}

            if (txtUser.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'User' ");
            }

            if (tStrBuilder.ToString().Length > 0)
            {
                tStrBuilder.Append("����Ϊ��!");
                MessageBox.Show(tStrBuilder.ToString());
                return false;
            }
            else
                return true;
        }

        private void btnChanged_Click(object sender, EventArgs e)
        {
            try
            {
                IWorkspace ws = OpenWorkspace(true);
                if (ws == null)
                {
                    MessageBox.Show("����ʧ�ܣ�", "���ݿ�����");
                    return;
                }

                cboVersions.Items.Clear();

                IEnumVersionInfo pEnum = (ws as IVersionedWorkspace).Versions;
                IVersionInfo ver = pEnum.Next();
                while (ver != null)
                {
                    cboVersions.Items.Add(ver.VersionName);
                    ver = pEnum.Next();
                }
                if (cboVersions.SelectedIndex == -1)
                {
                    if (cboVersions.Items.Count > 0)
                        cboVersions.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }
       

        private void cmbDataType_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbDataType.SelectedItem.ToString()=="Oracle")
            {
                if(string.IsNullOrWhiteSpace(txtServerName.Text))
                {
                    txtPort.Text = "sde:oracle11g:localhost/orcl";
                }else
                {
                    txtPort.Text =$"sde:oracle11g:{txtServerName.Text}/orcl";
                }
              
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtServerName.Text))
                {
                    txtPort.Text = "sde:postgresql:localhost";
                }
                else
                {
                    txtPort.Text = $"sde:postgresql:{txtServerName.Text}";
                }
                   
            }
           
        }

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region ESRI����

        ///// <summary>
        ///// ��ȡ�����ռ�
        ///// </summary>
        ///// <returns>����IWorkspace</returns>
        //public IWorkspace OpenWorkspace()
        //{
        //    //�������������Ч��
        //    bool IsValid = CheckInputValid();
        //    if (IsValid == false)
        //        return null;

        //    //��ȡ��������
        //    IPropertySet tPropertySet = GetPropertySet();

        //    //���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
        //    string server = tPropertySet.GetProperty("Server").ToString();
        //    //if (NetHelper.Ping(server) == false) return null;    

        //    //ʵ����SdeWorkspaceFactoryClass��
        //    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

        //    try
        //    {
        //        //�����������Դ򿪹����ռ�
        //        IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
        //        return tWorkspace;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// ��ȡ�����ռ�
        ///// </summary>
        ///// <param name="emptyVersion">�Ƿ������汾��Ϊ��</param>
        ///// <returns>����IWorkspace</returns>
        //public IWorkspace OpenWorkspace(bool emptyVersion)
        //{
        //    //�������������Ч��
        //    bool IsValid = CheckInputValid();
        //    if (IsValid == false)
        //        return null;

        //    //��ȡ��������
        //    IPropertySet tPropertySet = GetPropertySet();
        //    if (emptyVersion)
        //        //tPropertySet.SetProperty("VERSION", "");
        //        tPropertySet.SetProperty("VERSION", "SDE.DEFAULT");

        //    //���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
        //    string server = tPropertySet.GetProperty("Server").ToString();
        //    if (NetHelper.Ping(server) == false)
        //    {
        //        if (MessageBox.Show("������Ping��ͨ�������Ƿ����������ڣ������Ƿ�������ӣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //        {
        //            throw new Exception("������ ping��ͨ");
        //        }
        //    }

        //    //ʵ����SdeWorkspaceFactoryClass��
        //    IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

        //    //�����������Դ򿪹����ռ�
        //    IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
        //    return tWorkspace;
        //}

        #endregion


    }
}