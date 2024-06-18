using AG.COM.SDM.Config;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Database;
using AG.COM.SDM.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AG.COM.SDM.Config
{
    public static class ORMHelper
    {
        public static void InitSessionConn(OleDBConfig oleDbConfig, string AssemblyName)
        {
            SessionParameter frameSessionParameter = new SessionParameter(
                        oleDbConfig.OLE_Server, oleDbConfig.OLE_DataBase, oleDbConfig.OLE_Port.ToString(), oleDbConfig.OLE_User, oleDbConfig.OLE_Password);
            SessionFactory.SessionParaManager.Add(AssemblyName, frameSessionParameter);

            SessionParameter sessionParameter = frameSessionParameter;
            DatabaseType oleDataBaseType = oleDbConfig.DatabaseType;

            IDictionary<string, string> connProps = InitConnProps(oleDataBaseType, sessionParameter);
            SessionFactory.ConnProps = connProps;

        }

        public static bool CheckSession(string AssemblyName, out string msgEx)
        {
            msgEx = string.Empty;
            try
            {
                SessionFactory.CheckOpenSession(AssemblyName);
                return true;
            }
            catch(Exception ex)
            {
                msgEx = ex.Message;
                ExceptionLog.LogError(msgEx, ex);
                return false;
            }
        }

        private static IDictionary<string, string> InitConnProps(DatabaseType oleDataBaseType, 
            SessionParameter sessionParameter)
        {
            IDictionary<string, string> connProps = new Dictionary<string, string>();
            if (oleDataBaseType == DatabaseType.Oracle)
            {
                string connStr1 = string.Format("User ID={0};Password={1};", sessionParameter.OleUser, sessionParameter.OlePassword);
                string connStr2 = string.Format("Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST ={0})(PORT = {1}))) (CONNECT_DATA = (SERVICE_NAME = {2})))", sessionParameter.OleServer, sessionParameter.OlePort, sessionParameter.OleDatabase);
                connProps.Add("connection.driver_class", "NHibernate.Driver.OracleClientDriver");
                connProps.Add("connection.connection_string", connStr1 + connStr2);
                connProps.Add("query.substitutions", "true 1, false 0, yes 'Y', no 'N'");
                connProps.Add("dialect", "NHibernate.Dialect.Oracle10gDialect");
            }
            else if (oleDataBaseType == DatabaseType.PostgresSql)
            {
                string connStr = string.Format("Server={0};Port={1};Database={2};User ID={3};Password={4}", sessionParameter.OleServer, sessionParameter.OlePort, sessionParameter.OleDatabase, sessionParameter.OleUser, sessionParameter.OlePassword);

                connProps.Add("connection.driver_class", "NHibernate.Driver.NpgsqlDriver");
                connProps.Add("connection.connection_string", connStr);
                connProps.Add("query.substitutions", "true 1, false 0, yes 'Y', no 'N'");
                connProps.Add("dialect", "NHibernate.Dialect.PostgreSQLDialect");
            }
            else if (oleDataBaseType == DatabaseType.Dm)
            {
                string connStr = string.Format("Server={0};Port={1};Database={2};User ID={3};Password={4}", sessionParameter.OleServer, sessionParameter.OlePort, sessionParameter.OleDatabase, sessionParameter.OleUser, sessionParameter.OlePassword);
                connProps.Add("connection.driver_class", "NHibernate.Driver.DmDriver, DmDialect, Version=1.0.0.0, Culture=neutral,PublicKeyToken = 072d25982b139bf8");
                connProps.Add("connection.connection_string", connStr);
                connProps.Add("query.substitutions", "true 1, false 0, yes 'Y', no 'N'");
                connProps.Add("dialect", "NHibernate.Dialect.DmDialect, DmDialect, Version=1.0.0.0, Culture=neutral,PublicKeyToken = 072d25982b139bf8");
            }
            else if (oleDataBaseType == DatabaseType.SQLite)
            {
                string connStr = string.Format("Data Source={0}", sessionParameter.OleDatabase);
                connProps.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
                connProps.Add("connection.connection_string", connStr);
                connProps.Add("query.substitutions", "true 1, false 0, yes 'Y', no 'N'");
                connProps.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            }

            return connProps;
        }


    }
}
