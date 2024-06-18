using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AG.COM.SDM.Manager
{
    public partial class FormAddPlugIn : Form
    {
        public FormAddPlugIn(IList<FileInfo> pListAssemblyInfo)
        {
            InitializeComponent();

            this.combAssembly.DataSource = pListAssemblyInfo;
        }

        /// <summary>
        /// 获取插件程序集
        /// </summary>
        public string PlugInAssembly
        {
            get
            {
                return this.combAssembly.Text;
            }
        }

        /// <summary>
        /// 获取插件类型
        /// </summary>
        public string PlugInType
        {
            get
            {
                return this.combType.Text;
            }
        }

        /// <summary>
        /// 获取插件名称
        /// </summary>
        public string PlugInName
        {
            get
            {
                return this.txtName.Text;
            }
        }

        /// <summary>
        /// 获取插件描述信息
        /// </summary>
        public string PlugInDescription
        {
            get
            {
                return this.txtDescription.Text;
            }
        }

        private void combAssembly_SelectedValueChanged(object sender, EventArgs e)
        {
            //清空所有类型
            this.combType.Items.Clear();     

            //得到程序集文件信息
            FileInfo pFileInfo = this.combAssembly.SelectedItem as FileInfo;
            if (pFileInfo == null) return;

            try
            {
                //通过路径获取程序集
                Assembly pAssembly = System.Reflection.Assembly.LoadFile(pFileInfo.FullName);
                Type[] pTypes = pAssembly.GetTypes();

                foreach (Type pType in pTypes)
                {                   
                    //如果该类型的访问属性不为公开的类类型，则跳过
                    if (pType.IsClass == false || pType.IsPublic == false) continue;
                    //如果为上下文菜单接口则不加入 
                    if (pType.GetInterface("IContextMenu") != null) continue;
                    //检测是否包含启动项接口
                    if (pType.GetInterface("IStartupPlugin") != null)                   
                    {
                        this.combType.Items.Add(pType.FullName);
                    }                   
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}