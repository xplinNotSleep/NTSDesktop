using System;
using AG.COM.SDM.Config;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Manager
{
    /// <summary>
    /// Manager的数据库帮助类
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// 刷新AGSDM的SessionFactory，在修改OLE数据库参数后调用
        /// </summary>
        public static void RefreshAGSDMSessionFactory()
        {
            //清空SessionFactory
            SessionFactory.ClearSessionFactory();

            OleDBConfig sysConfig = CommonVariables.OledbConn;
            ORMHelper.InitSessionConn(sysConfig,
                CommonConstString.STR_ModelName);

            #region 可按如下格式继续添加多个OLE数据库
            //SysDBConfig sysConfig = SysDBConfig.GetInstance();
            //OleConnProperty sysOleConn = sysConfig.OLE_ConnManager.GetOleConn(CommonConstString.STR_AGSDMOleName);
            //if (sysOleConn == null)
            //{
            //    throw new Exception("请添加标识为 " + CommonConstString.STR_AGSDMOleName + " 的OLE数据库");
            //}
            //else
            //{
            //    SessionParameter frameSessionParameter = new SessionParameter(
            //            sysOleConn.OLE_Server, sysOleConn.OLE_DataBase, sysOleConn.OLE_Port, sysOleConn.OLE_User, sysOleConn.OLE_Password);
            //    SessionFactory.SessionParaManager.Add(CommonConstString.STR_ModelName, frameSessionParameter);
            //}
            #endregion


        }
    }
}
