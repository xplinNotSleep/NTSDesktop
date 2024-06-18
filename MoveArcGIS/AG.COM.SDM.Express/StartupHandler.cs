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
    /// 系统启动处理类
    /// </summary>
    public class StartupHandler:BaseStartupPlugin
    {
        private object m_hook;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public StartupHandler()
        {
        }

        /// <summary>
        /// 系统启动处理
        /// </summary>
        public override void Startup()
        {
            try
            {
                string xmlFile = CommonConstString.STR_ConfigPath  + "\\StartupPlugin.xml";

                if (File.Exists(xmlFile) == true)
                {
                    DataSet ds = new DataSet();

                    //从XML中读取数据.数据结构后面详细讲一下
                    ds.ReadXml(xmlFile);

                    DataView dv = ds.Tables[0].DefaultView;
                    for (int i = 0; i < dv.Count; i++)
                    {
                        string strAssembly = dv[i]["PlugAssembly"].ToString();
                        string strType = dv[i]["PlugType"].ToString();

                        //获取实例
                        object objInstance = PlugInProvider.GetInstance(strAssembly, strType);

                        IStartupPlugin tStartupPlugin = objInstance as IStartupPlugin;
                        if (tStartupPlugin != null)
                        {
                            //初始化赋值
                            tStartupPlugin.OnCreate(this.m_hook);
                            //系统启动处理
                            tStartupPlugin.Startup();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show("系统启动处理出错"+ex.Message);     
            }
        }

        /// <summary>
        /// 创建时初始化赋值
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hook = hook;
        } 
    }


}
