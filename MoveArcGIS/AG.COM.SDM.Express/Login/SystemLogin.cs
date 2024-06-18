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
    /// ����˵����������֤ϵͳ��¼���û���Ϣ��
    /// </summary>
	public class SystemLogin
	{

		public SystemLogin()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��ȡ����sde���ݿ����Ӳ�����
		/// </summary>
		/// <returns>����Ѿ������򷵻�true�����򷵻�false</returns>
		public bool SysSpatialConnectionState()
		{
            try
            {
                SpatialDBConfig SpatialConfig = CommonVariables.SpatialdbConn;

                //��ȡSpatial�����ò���
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
                AutoCloseMsgBox.Show(ex.Message, "��ʾ", 3000);
                return false;
            }
        }

        /// <summary>
        /// ϵͳ��¼��ʱ����֤�û�		
        /// </summary>
        /// <param name="pLoginName">�û�����</param>
        /// <param name="pLoginPassWord">�û�����</param>
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
         /// �ж�ole���Ƿ����ӳɹ�
         /// </summary>
         /// <returns></returns>
        public bool SysOleConnectionState()
        {
            try
            {
                OleDBConfig oleConfig = CommonVariables.OledbConn;

                //��ȡole�����ò���
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
                AutoCloseMsgBox.Show(ex.Message, "��ʾ", 3000);
                return false;
            }
        }

        public bool SysCheckSession()
        {
            if(!ORMHelper.CheckSession(CommonConstString.STR_ModelName, 
                out string msgEx))
            {
                AutoCloseMsgBox.Show(msgEx, "��ʾ", 3000);
                return false;
            }
            return true;
        }

    }     
}
