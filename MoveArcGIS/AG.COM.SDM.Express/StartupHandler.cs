using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using AG.COM.SDM.Utility;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Config;

namespace AG.COM.SDM.Express
{
    /// <summary>
    /// ϵͳ����������
    /// </summary>
    public class StartupHandler:BaseStartupPlugin
    {
        private object m_hook;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public StartupHandler()
        {
        }

        /// <summary>
        /// ϵͳ��������
        /// </summary>
        public override void Startup()
        {
            try
            {
                string xmlFile = CommonConstString.STR_ConfigPath  + "\\StartupPlugin.xml";

                if (File.Exists(xmlFile) == true)
                {
                    DataSet ds = new DataSet();

                    //��XML�ж�ȡ����.���ݽṹ������ϸ��һ��
                    ds.ReadXml(xmlFile);

                    DataView dv = ds.Tables[0].DefaultView;
                    for (int i = 0; i < dv.Count; i++)
                    {
                        string strAssembly = dv[i]["PlugAssembly"].ToString();
                        string strType = dv[i]["PlugType"].ToString();

                        //��ȡʵ��
                        object objInstance = PlugInProvider.GetInstance(strAssembly, strType);

                        IStartupPlugin tStartupPlugin = objInstance as IStartupPlugin;
                        if (tStartupPlugin != null)
                        {
                            //��ʼ����ֵ
                            tStartupPlugin.OnCreate(this.m_hook);
                            //ϵͳ��������
                            tStartupPlugin.Startup();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show("ϵͳ�����������"+ex.Message);     
            }
        }

        /// <summary>
        /// ����ʱ��ʼ����ֵ
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            this.m_hook = hook;
        } 
    }


}
