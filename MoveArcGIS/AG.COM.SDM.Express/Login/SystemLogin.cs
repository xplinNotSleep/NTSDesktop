using System;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Model;
using AG.COM.SDM.DAL;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using Npgsql;
using AG.COM.SDM.Config;
using AG.COM.SDM.Utility.Logger;
using AG.COM.SDM.Database;

namespace AG.COM.SDM.Express
{ 
    /// <summary>
    /// 功能说明：用于认证系统登录的用户信息。
    /// </summary>
	public class SystemLogin
	{

		public SystemLogin()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 获取本地sde数据库连接操作。
		/// </summary>
		/// <returns>如果已经连接则返回true，否则返回false</returns>
		public bool SysSpatialConnectionState()
		{
            try
            {
                SpatialDBConfig SpatialConfig = CommonVariables.SpatialdbConn;

                //读取Spatial库配置参数
                DatabaseType dbType = SpatialConfig.Spatial_DBType;
                string server = SpatialConfig.Spatial_Server;
                int port = int.Parse(SpatialConfig.Spatial_Port);
                string dbName = SpatialConfig.Spatial_DataBase;
                string user = SpatialConfig.Spatial_User;
                string password = SpatialConfig.Spatial_Password;
                IAdoDatabase database = new AdoDatabase();
                database.InitConnParam(dbType, server, port, dbName, user, password);
                return database.OpenConnect(true);
            }
            catch (Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "提示", 3000);
                return false;
            }
        }

        /// <summary>
        /// 系统登录的时候，认证用户		
        /// </summary>
        /// <param name="pLoginName">用户名称</param>
        /// <param name="pLoginPassWord">用户密码</param>
        public bool SysLogin(string pLoginName, string pLoginPassWord, out decimal userID,out string nameCN)
        {          
            nameCN = string.Empty;
            AbstractFactory factory = AbstractFactory.GetInstance(); //new OpusFactory();//
            IUser userManage = factory.CreateUser();
            AGSDM_SYSTEM_USER user = userManage.GetUser(pLoginName);
            if(user==null)
            {
                userID = -1;
                return false;
            }
            if(user.PASSWORDTYPE==1)
            {
                string pLoginPassWordMD5 = Security.GetMd5(pLoginPassWord);
                if (user != null && user.PASSWORD == pLoginPassWordMD5)
                {
                    nameCN = user.NAME_CN;
                    userID = user.USER_ID;
                    return true;
                }
                else
                {
                    userID = -1;
                    return false;
                }
            }
            else
            {
                if (user != null && user.PASSWORD == pLoginPassWord)
                {
                    nameCN = user.NAME_CN;
                    userID = user.USER_ID;
                    return true;
                }
                else
                {
                    userID = -1;
                    return false;
                }
            }
            
        }

         /// <summary>
         /// 判定ole库是否连接成功
         /// </summary>
         /// <returns></returns>
        public bool SysOleConnectionState()
        {
            try
            {
                OleDBConfig oleConfig = CommonVariables.OledbConn;

                //读取ole库配置参数
                DatabaseType dbType = oleConfig.DatabaseType;
                string server = oleConfig.OLE_Server;
                int port = int.Parse(oleConfig.OLE_Port);
                string dbName = oleConfig.OLE_DataBase;
                string user = oleConfig.OLE_User;
                string password = oleConfig.OLE_Password;
                IAdoDatabase database = new AdoDatabase();
                database.InitConnParam(dbType, server, port, dbName, user,password);
                return database.OpenConnect(true);
            }
            catch(Exception ex)
            {
                AutoCloseMsgBox.Show(ex.Message, "提示", 3000);
                return false;
            }
        }

        public bool SysCheckSession()
        {
            if(!ORMHelper.CheckSession(CommonConstString.STR_ModelName, 
                out string msgEx))
            {
                AutoCloseMsgBox.Show(msgEx, "提示", 3000);
                return false;
            }
            return true;
        }

    }     
}
