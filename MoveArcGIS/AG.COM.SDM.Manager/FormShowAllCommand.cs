using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace AG.COM.SDM.Manager
{
    public partial class FormShowAllCommand : Form
    {
        /// <summary>
        /// 列出所有插件类
        /// </summary>
        public FormShowAllCommand()
        {
            InitializeComponent();
        }

        private void FormShowAllCommand_Load(object sender, EventArgs e)
        {
            try
            {               
                string result = "";

                string strAssemblyPath = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo tDirectory = new DirectoryInfo(strAssemblyPath);
                FileInfo[] Files = tDirectory.GetFiles("*.dll");
                foreach (FileInfo afile in Files)
                {
                    try//屏蔽不能加载的Dll
                    {
                        string name = afile.Name;
                        if (!name.StartsWith("AG"))
                        {
                            continue;
                        }
                        Assembly ass = Assembly.LoadFrom(afile.FullName);
                        Type[] types = ass.GetTypes();
                        foreach (Type type in types)
                        {
                            try
                            {
                                if ((type.IsClass == false) || (type.IsPublic == false)) continue;
                                //如果为上下文接口则不载入
                                if (type.GetInterface("IContextMenu") != null) continue;
                                //判断该类是否继承ICommand或IPlugin接口
                                if ((type.GetInterface("ICommand") != null) || (type.GetInterface("IPlugin")) != null)
                                {
                                    object instance = Activator.CreateInstance(type);
                                    PropertyInfo pi = type.GetProperty("Caption");
                                    string caption = (string)pi.GetValue(instance, null);

                                    result += caption + "," + type.Namespace + "," + type.Name + "," + type.FullName + Environment.NewLine;
                                }
                            }
                            catch { }
                        }
                    }
                    catch
                    { }
                }

                txtInfo.Text = result;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default))
                    {
                        sw.Write(txtInfo.Text);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误");
            }
        }
    }
}
