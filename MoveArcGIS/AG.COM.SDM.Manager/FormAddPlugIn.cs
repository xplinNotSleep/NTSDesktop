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
        /// ��ȡ�������
        /// </summary>
        public string PlugInAssembly
        {
            get
            {
                return this.combAssembly.Text;
            }
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        public string PlugInType
        {
            get
            {
                return this.combType.Text;
            }
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        public string PlugInName
        {
            get
            {
                return this.txtName.Text;
            }
        }

        /// <summary>
        /// ��ȡ���������Ϣ
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
            //�����������
            this.combType.Items.Clear();     

            //�õ������ļ���Ϣ
            FileInfo pFileInfo = this.combAssembly.SelectedItem as FileInfo;
            if (pFileInfo == null) return;

            try
            {
                //ͨ��·����ȡ����
                Assembly pAssembly = System.Reflection.Assembly.LoadFile(pFileInfo.FullName);
                Type[] pTypes = pAssembly.GetTypes();

                foreach (Type pType in pTypes)
                {                   
                    //��������͵ķ������Բ�Ϊ�����������ͣ�������
                    if (pType.IsClass == false || pType.IsPublic == false) continue;
                    //���Ϊ�����Ĳ˵��ӿ��򲻼��� 
                    if (pType.GetInterface("IContextMenu") != null) continue;
                    //����Ƿ����������ӿ�
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