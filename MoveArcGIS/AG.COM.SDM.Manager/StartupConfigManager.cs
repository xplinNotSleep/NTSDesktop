using AG.COM.SDM.SystemUI;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AG.COM.SDM.Manager
{
    /// <summary>
    /// 启动项配置信息管理类
    /// </summary>
    public class StartupConfigManager
    {
        /// <summary>
        /// 从指定的文件读取配置信息到ListView对象
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="listView">ListView对象</param>
        public static void ReadConfigFile(string fileName, ListView listView)
        {
            //实例化DataSet对象
            DataSet ds = new DataSet();
            //从指定的文件中得到数据视图
            ds.ReadXml(fileName);

            DataView dv = ds.Tables[0].DefaultView;

            for (int i = 0; i < dv.Count; i++)
            {
                //创建ListViewItem项
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = dv[i]["Name"].ToString();
                tListViewItem.SubItems.Add(dv[i]["PlugAssembly"].ToString());
                tListViewItem.SubItems.Add(dv[i]["PlugType"].ToString());
                tListViewItem.SubItems.Add(dv[i]["Description"].ToString());
                    
                //添加子项
                listView.Items.Add(tListViewItem);
            }
        }

        /// <summary>
        /// 向指定的文件中写入ListView信息
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="listView">ListView对象</param>
        public static void WriteConfigFile(string fileName, ListView listView)
        {
            //初始化文件流
            System.IO.FileStream fs = new System.IO.FileStream(fileName, FileMode.Create);
            XmlTextWriter pXmlOut = new XmlTextWriter(fs, Encoding.Unicode);
            pXmlOut.Formatting = Formatting.Indented;

            //书写XML声明
            pXmlOut.WriteStartDocument();

            //书写配置文件注释信息
            pXmlOut.WriteComment("标题：启动项配置信息文件!");
            pXmlOut.WriteComment("版本：");
            pXmlOut.WriteComment("作者：Echo-AG.COM.SDM");

            //开始
            pXmlOut.WriteStartElement("StartupPluginConfig");

            for (int i = 0; i < listView.Items.Count; i++)
            {
                //写入插件信息元数据
                WriteElementString(pXmlOut, listView.Items[i]); 
            }

            //终止一个元素
            pXmlOut.WriteEndElement();
            //关闭任何打开的元素
            pXmlOut.WriteEndDocument();
            //关闭
            pXmlOut.Close();
        }

        /// <summary>
        /// 写入插件信息元数据
        /// </summary>
        /// <param name="mxmlWriter">XmlTextWriter对象</param>
        /// <param name="mlistViewItem">ListViewItem对象</param>
        private static void WriteElementString(XmlTextWriter mxmlWriter, ListViewItem mlistViewItem)
        {      
            mxmlWriter.WriteStartElement("StartupConfig");   
            mxmlWriter.WriteElementString("Name", mlistViewItem.Text);
            mxmlWriter.WriteElementString("PlugAssembly", mlistViewItem.SubItems[1].Text);
            mxmlWriter.WriteElementString("PlugType", mlistViewItem.SubItems[2].Text);
            mxmlWriter.WriteElementString("Description", mlistViewItem.SubItems[3].Text);  
            mxmlWriter.WriteElementString("EnumNodeType", Convert.ToString((int)EnumNodeType.StartupItem));
            mxmlWriter.WriteFullEndElement();
        }
    }
}
