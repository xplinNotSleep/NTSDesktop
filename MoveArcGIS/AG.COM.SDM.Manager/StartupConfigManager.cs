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
    /// ������������Ϣ������
    /// </summary>
    public class StartupConfigManager
    {
        /// <summary>
        /// ��ָ�����ļ���ȡ������Ϣ��ListView����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="listView">ListView����</param>
        public static void ReadConfigFile(string fileName, ListView listView)
        {
            //ʵ����DataSet����
            DataSet ds = new DataSet();
            //��ָ�����ļ��еõ�������ͼ
            ds.ReadXml(fileName);

            DataView dv = ds.Tables[0].DefaultView;

            for (int i = 0; i < dv.Count; i++)
            {
                //����ListViewItem��
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = dv[i]["Name"].ToString();
                tListViewItem.SubItems.Add(dv[i]["PlugAssembly"].ToString());
                tListViewItem.SubItems.Add(dv[i]["PlugType"].ToString());
                tListViewItem.SubItems.Add(dv[i]["Description"].ToString());
                    
                //�������
                listView.Items.Add(tListViewItem);
            }
        }

        /// <summary>
        /// ��ָ�����ļ���д��ListView��Ϣ
        /// </summary>
        /// <param name="fileName">�ļ�·��</param>
        /// <param name="listView">ListView����</param>
        public static void WriteConfigFile(string fileName, ListView listView)
        {
            //��ʼ���ļ���
            System.IO.FileStream fs = new System.IO.FileStream(fileName, FileMode.Create);
            XmlTextWriter pXmlOut = new XmlTextWriter(fs, Encoding.Unicode);
            pXmlOut.Formatting = Formatting.Indented;

            //��дXML����
            pXmlOut.WriteStartDocument();

            //��д�����ļ�ע����Ϣ
            pXmlOut.WriteComment("���⣺������������Ϣ�ļ�!");
            pXmlOut.WriteComment("�汾��");
            pXmlOut.WriteComment("���ߣ�Echo-AG.COM.SDM");

            //��ʼ
            pXmlOut.WriteStartElement("StartupPluginConfig");

            for (int i = 0; i < listView.Items.Count; i++)
            {
                //д������ϢԪ����
                WriteElementString(pXmlOut, listView.Items[i]); 
            }

            //��ֹһ��Ԫ��
            pXmlOut.WriteEndElement();
            //�ر��κδ򿪵�Ԫ��
            pXmlOut.WriteEndDocument();
            //�ر�
            pXmlOut.Close();
        }

        /// <summary>
        /// д������ϢԪ����
        /// </summary>
        /// <param name="mxmlWriter">XmlTextWriter����</param>
        /// <param name="mlistViewItem">ListViewItem����</param>
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
