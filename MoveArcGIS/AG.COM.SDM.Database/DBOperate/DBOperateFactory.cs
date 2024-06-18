using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Database
{
    public class DBOperateFactory
    {
        public enum DatabaseType
        {
            Access,
            Sqlserver,
            Oracle
        }

        public static DBOperateFactory.DatabaseType DBType
        {
            get;
            set;
        }

        public static string ConnectionString
        {
            get;
            set;
        }

        public static IDBOperate GetDBOperate()
        {
            IDBOperate result = null;
            switch (DBOperateFactory.DBType)
            {
                case DBOperateFactory.DatabaseType.Access:
                    result = DBOperateFactory.GetAccessDBOperate(DBOperateFactory.ConnectionString);
                    break;
                case DBOperateFactory.DatabaseType.Sqlserver:
                    result = DBOperateFactory.GetSqlserverDBOperate(DBOperateFactory.ConnectionString);
                    break;
                case DBOperateFactory.DatabaseType.Oracle:
                    result = DBOperateFactory.GetOracleDBOperate(DBOperateFactory.ConnectionString);
                    break;
            }
            return result;
        }

        public static IDBOperate GetDBOperate(DBOperateFactory.DatabaseType dbType, string connectionString)
        {
            IDBOperate result = null;
            switch (dbType)
            {
                case DBOperateFactory.DatabaseType.Access:
                    result = DBOperateFactory.GetAccessDBOperate(connectionString);
                    break;
                case DBOperateFactory.DatabaseType.Sqlserver:
                    result = DBOperateFactory.GetSqlserverDBOperate(connectionString);
                    break;
                case DBOperateFactory.DatabaseType.Oracle:
                    result = DBOperateFactory.GetOracleDBOperate(connectionString);
                    break;
            }
            return result;
        }

        public static IDBOperate GetAccessDBOperate(string connectionString)
        {
            return new AccessDBOperate(connectionString);
        }

        public static IDBOperate GetSqlserverDBOperate(string connectionString)
        {
            return new SqlDBOperate();
        }

        public static IDBOperate GetOracleDBOperate(string connectionString)
        {
            return new OracleDBOperate();
        }
    }
}
