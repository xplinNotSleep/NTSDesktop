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
    /// 系统启动项设置 窗体类
    /// </summary>
    public partial class FormStart : DockContent
    {
        private IList<FileInfo> m_ListFileInfo; //程序集列表
        private string m_xmlPath;
        //private ITable m_Table;                 //系统启动项列表        

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormStart()
        {
            InitializeComponent();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            try
            {
                //获取所有插件项集合
                this.m_ListFileInfo = GetStartPlugIns();

                this.columnHeader1.Width = this.listView1.Width / 4;
                this.columnHeader2.Width = this.listView1.Width / 4;
                this.columnHeader3.Width = this.listView1.Width / 4;
                this.columnHeader4.Width = this.listView1.Width / 4;

                string xmlPath = CommonConstString.STR_ConfigPath + "\\StartupPlugin.xml";
                if (File.Exists(xmlPath) == true)
                {
                    //从指定的文件中读取树节点信息
                    StartupConfigManager.ReadConfigFile(xmlPath, this.listView1);

                    this.m_xmlPath = xmlPath;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
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
                    tSaveFileDlg.Filter = "启动项配置文件(*.xml)|*.xml";
                    tSaveFileDlg.Title = "保存文件";

                    if (tSaveFileDlg.ShowDialog() == DialogResult.OK)
                    {
                        this.m_xmlPath = tSaveFileDlg.FileName;
                    }
                }

                if (listView1.Items.Count < 1)
                {
                    MessageBox.Show("请填写一个启动项配置！","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                StartupConfigManager.WriteConfigFile(this.m_xmlPath, this.listView1);

                #region 保存到数据表
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
        /// 获取所有插件程序集集合
        /// </summary>
        /// <returns>返回IList类型</returns>
        private IList<FileInfo> GetStartPlugIns()
        {
            IList<FileInfo> tListFileInfo = new List<FileInfo>();

            string pluginPath = Application.StartupPath;
            DirectoryInfo pDirInfo = new DirectoryInfo(pluginPath);

            //只加载AG.开头的dll
            FileInfo[] plugins = pDirInfo.GetFiles("AG.*.dll");
            foreach (FileInfo plugin in plugins)
            {
                //dll不一定能正常加载，因此要try包着
                try
                {
                    //通过指定路径加载程序集
                    Assembly pAssembly = Assembly.LoadFile(plugin.FullName);

                    //获取此程序集中的所有类型
                    Type[] pTypes = pAssembly.GetTypes();

                    foreach (Type pType in pTypes)
                    {
                        //首先判断该类型是否为公开的类类型
                        if (pType.IsClass == false || pType.IsPublic == false) continue;
                        //检测是否包含启动项接口
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