using AG.COM.SDM.Config;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Manager
{
    /// <summary>
    /// ϵͳ���������� ������
    /// </summary>
    public partial class FormStart : DockContent
    {
        private IList<FileInfo> m_ListFileInfo; //�����б�
        private string m_xmlPath;
        //private ITable m_Table;                 //ϵͳ�������б�        

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormStart()
        {
            InitializeComponent();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            try
            {
                //��ȡ���в�����
                this.m_ListFileInfo = GetStartPlugIns();

                this.columnHeader1.Width = this.listView1.Width / 4;
                this.columnHeader2.Width = this.listView1.Width / 4;
                this.columnHeader3.Width = this.listView1.Width / 4;
                this.columnHeader4.Width = this.listView1.Width / 4;

                string xmlPath = CommonConstString.STR_ConfigPath + "\\StartupPlugin.xml";
                if (File.Exists(xmlPath) == true)
                {
                    //��ָ�����ļ��ж�ȡ���ڵ���Ϣ
                    StartupConfigManager.ReadConfigFile(xmlPath, this.listView1);

                    this.m_xmlPath = xmlPath;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "����");
            }
        }

        private void ToolBtnAdd_Click(object sender, EventArgs e)
        {
            FormAddPlugIn tFormAddPlugIn = new FormAddPlugIn(this.m_ListFileInfo);
            if (tFormAddPlugIn.ShowDialog() == DialogResult.OK)
            {
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = tFormAddPlugIn.PlugInName;
                tListViewItem.SubItems.Add(tFormAddPlugIn.PlugInAssembly);
                tListViewItem.SubItems.Add(tFormAddPlugIn.PlugInType);
                tListViewItem.SubItems.Add(tFormAddPlugIn.PlugInDescription);

                this.listView1.Items.Add(tListViewItem);
            }
        }

        private void ToolBtnClear_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
        }

        private void ToolBtnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem tListViewItem = this.listView1.SelectedItems[0];
                this.listView1.Items.Remove(tListViewItem);
            }
        }

        private void ToolBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_xmlPath == null || this.m_xmlPath.Length == 0)
                {
                    SaveFileDialog tSaveFileDlg = new SaveFileDialog();
                    tSaveFileDlg.Filter = "�����������ļ�(*.xml)|*.xml";
                    tSaveFileDlg.Title = "�����ļ�";

                    if (tSaveFileDlg.ShowDialog() == DialogResult.OK)
                    {
                        this.m_xmlPath = tSaveFileDlg.FileName;
                    }
                }

                if (listView1.Items.Count < 1)
                {
                    MessageBox.Show("����дһ�����������ã�","����",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                StartupConfigManager.WriteConfigFile(this.m_xmlPath, this.listView1);

                #region ���浽���ݱ�
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                AGSDM_STARTITEM startItemEntity = tEntityHandler.GetEntity<AGSDM_STARTITEM>("from AGSDM_STARTITEM");
                if (startItemEntity == null)
                {
                    startItemEntity = new AGSDM_STARTITEM();
                }

                ListViewItem tListViewItem = listView1.Items[0];
                startItemEntity.ASSEMBLY_NAME = tListViewItem.SubItems[1].Text;
                startItemEntity.FILE_NAME = string.Empty;
                startItemEntity.START_NAME = tListViewItem.Text;
                startItemEntity.TYPE_NAME = tListViewItem.SubItems[2].Text;
                startItemEntity.DESCRIPTION = tListViewItem.SubItems[3].Text;
                if (startItemEntity.ID <= 0)
                {
                    tEntityHandler.AddEntity(startItemEntity);
                }
                else
                {
                    tEntityHandler.UpdateEntity(startItemEntity, startItemEntity.ID);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }    

        /// <summary>
        /// ��ȡ���в�����򼯼���
        /// </summary>
        /// <returns>����IList����</returns>
        private IList<FileInfo> GetStartPlugIns()
        {
            IList<FileInfo> tListFileInfo = new List<FileInfo>();

            string pluginPath = Application.StartupPath;
            DirectoryInfo pDirInfo = new DirectoryInfo(pluginPath);

            //ֻ����AG.��ͷ��dll
            FileInfo[] plugins = pDirInfo.GetFiles("AG.*.dll");
            foreach (FileInfo plugin in plugins)
            {
                //dll��һ�����������أ����Ҫtry����
                try
                {
                    //ͨ��ָ��·�����س���
                    Assembly pAssembly = Assembly.LoadFile(plugin.FullName);

                    //��ȡ�˳����е���������
                    Type[] pTypes = pAssembly.GetTypes();

                    foreach (Type pType in pTypes)
                    {
                        //�����жϸ������Ƿ�Ϊ������������
                        if (pType.IsClass == false || pType.IsPublic == false) continue;
                        //����Ƿ����������ӿ�
                        if (pType.GetInterface("IStartupPlugin") != null)
                        {
                            tListFileInfo.Add(plugin);
                            break;
                        }
                    }
                }
                catch
                {               
                    continue;
                }
            }

            return tListFileInfo;
        }
    }
}