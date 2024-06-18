using Dm;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
namespace AG.COM.SDM.Database
{
    public class AdoDatabase : IAdoDatabase
    {
        private DbConnection m_pConnection;
        /// <summary>
        /// 事务对象
        /// </summary>
        private DbTransaction isOpenTrans { get; set; }

        public DatabaseType DbType { get; set; }

        public DbConnection DBConnection { get { return m_pConnection; } }

        public bool inTransaction { get; set; }

        public ConnProperty ConnProperty { get; set; }

        /// <summary>
        /// 开始新事务
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTrans()
        {
            if (!this.inTransaction)
            {
                if (OpenConnect(true) == true)
                {
                    inTransaction = true;
                    isOpenTrans = DBConnection.BeginTransaction();
                }
            }
            return isOpenTrans;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (this.m_pConnection != null)
            {
                this.m_pConnection.Close();
                this.m_pConnection.Dispose();
            }
            if (this.isOpenTrans != null)
            {
                this.isOpenTrans.Dispose();
            }
            this.m_pConnection = null;
            this.isOpenTrans = null;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (this.inTransaction)
            {
                this.inTransaction = false;
                this.isOpenTrans.Commit();
                //this.Close();
            }
        }

        /// <summary>
        /// 释放数据库连接对象
        /// </summary>
        public void Dispose()
        {
            if (this.m_pConnection != null)
            {
                this.m_pConnection.Dispose();
            }
            if (this.isOpenTrans != null)
            {
                this.isOpenTrans.Dispose();
            }
        }

        /// <summary>
        /// 根据sql语句获取dataset
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SqlStr)
        {
            DataSet pDataSet = null;

            if (m_pConnection == null)
            {
                OpenConnect(true);
            }
            pDataSet = new DataSet();
            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = SqlStr;
            IDbDataAdapter adapter = CreateDataAdapter(cmd);
            adapter.Fill(pDataSet);
            //this.Close();
            return pDataSet;
        }

        /// <summary>
        /// 根据sql语句获取数据表的每一行第一列信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<string> QueryList(string sql)
        {
            List<string> list = null;

            DataTable dataTable = this.QueryTable(sql);
            list = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string item = Convert.ToString(dataTable.Rows[i][0]);
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据sql语句获取数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable QueryTable(string sql)
        {
            DataTable dataTable = null;

            if (m_pConnection == null)
            {
                bool isOpen = OpenConnect(true);
                if (!isOpen) return dataTable;
            }

            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = sql;
            IDbDataAdapter adapter = CreateDataAdapter(cmd);
            DataSet pDataSet = new DataSet();
            adapter.Fill(pDataSet);
            if (pDataSet.Tables.Count == 0) return dataTable;
            dataTable = pDataSet.Tables[0];
            //this.Close();
            return dataTable;
        }

        /// <summary>
        /// 执行无返回结果sql语句
        /// </summary>
        /// <param name="sqls"></param>
        public void BatchExecute(List<string> sqls)
        {
            this.OpenConnect(true);
            this.BeginTrans();
            DbCommand cmd = CreateDbCommand();
            cmd.Transaction = this.isOpenTrans;
            cmd.Connection = m_pConnection;
            cmd.CommandType = CommandType.Text;
            for (int i = 0; i < sqls.Count; i++)
            {
                string commandText = sqls[i];
                cmd.CommandText = commandText;
                int num = cmd.ExecuteNonQuery();
            }
            this.Commit();
            //this.Close();
        }

        public bool OpenConnect(bool IsKeepConn)
        {
            //DatabaseType DbType = ConnProperty.DbType;
            DbType = ConnProperty.DbType;
            string Server = ConnProperty.Server;
            string DataBase = ConnProperty.DataBase;
            string User = ConnProperty.User;
            string Password = ConnProperty.Password;
            int Port = ConnProperty.Port;
            //string Instance = ConnProperty.Instance;
            //DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), DBType, false);
            string connectionString = string.Empty;
            switch (DbType)
            {
                //Oracle:1521;Pgsql:5432
                case DatabaseType.Oracle:
                    connectionString = $"Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST ={Server})(PORT ={Port}))) (CONNECT_DATA = (SERVICE_NAME = {DataBase})));User Id={User};Password={Password};";
                    m_pConnection = new OracleConnection(connectionString);
                    break;
                case DatabaseType.PostgresSql:
                    connectionString = $"Server={Server};Port={Port};User Id={User};Password={Password}; Database={DataBase};";
                    m_pConnection = new NpgsqlConnection(connectionString);
                    break;
                case DatabaseType.SqlServer:
                    connectionString = $"Data Source={Server};User Id={User};Password={Password}; Database={DataBase};";
                    m_pConnection = new SqlConnection(connectionString);
                    break;
                case DatabaseType.Dm:
                    connectionString = $"Server={Server};Port={Port};Database={DataBase};User ID={User};Password={Password};";
                    m_pConnection = new DmConnection(connectionString);
                    break;
                case DatabaseType.MySql:
                    connectionString = $"server={Server};port={Port};database={DataBase};user={User};password={Password};";
                    m_pConnection = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.Access:
                    connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Server}";
                    m_pConnection = new OleDbConnection(connectionString);
                    break;
                default:
                    break;
            }
            if (m_pConnection == null) return false;

            if (m_pConnection.State == ConnectionState.Closed)
            {
                try
                {
                    m_pConnection.Open();
                }
                catch
                {
                    return false;
                }
            }
            //用于测试数据库连接时，成功后会将其关闭
            if (!IsKeepConn) m_pConnection.Close();
            return true;
        }

        public bool OpenConnect_old()
        {
            //DatabaseType DbType = ConnProperty.DbType;
            DbType = ConnProperty.DbType;
            string Server = ConnProperty.Server;
            string DataBase = ConnProperty.DataBase;
            string User = ConnProperty.User;
            string Password = ConnProperty.Password;
            int Port = ConnProperty.Port;
            //string Instance = ConnProperty.Instance;
            //DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), DBType, false);
            string connectionString = string.Empty;
            switch (DbType)
            {
                //Oracle:1521;Pgsql:5432
                case DatabaseType.Oracle:
                    connectionString = $"Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST ={Server})(PORT ={Port}))) (CONNECT_DATA = (SERVICE_NAME = {DataBase})));User Id={User};Password={Password};";
                    m_pConnection = new OracleConnection(connectionString);
                    break;
                case DatabaseType.PostgresSql:
                    connectionString = $"Server={Server};Port={Port};User Id={User};Password={Password}; Database={DataBase};";
                    m_pConnection = new NpgsqlConnection(connectionString);
                    break;
                case DatabaseType.SqlServer:
                    connectionString = $"Data Source={Server};User Id={User};Password={Password}; Database={DataBase};";
                    m_pConnection = new SqlConnection(connectionString);
                    break;
                case DatabaseType.Dm:
                    connectionString = $"Server={Server};Port={Port};Database={DataBase};User ID={User};Password={Password};";
                    m_pConnection = new DmConnection(connectionString);
                    break;
                case DatabaseType.MySql:
                    connectionString = $"server={Server};port={Port};database={DataBase};user={User};password={Password};";
                    m_pConnection = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.Access:
                    connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Server}";
                    m_pConnection = new OleDbConnection(connectionString);
                    break;
                default:
                    break;
            }
            if (m_pConnection == null) return false;

            if (m_pConnection.State == ConnectionState.Closed)
            {
                try
                {
                    m_pConnection.Open();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否成功执行语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Execute(string sql)
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }
            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = sql;
            int num = cmd.ExecuteNonQuery();
            //this.Close();
            return true;

        }

        /// <summary>
        /// 关闭事务
        /// </summary>
        public void Rollback()
        {
            if (this.inTransaction)
            {
                this.inTransaction = false;
                this.isOpenTrans.Rollback();
                this.Close();
            }
        }

        private IDbDataAdapter CreateDataAdapter(DbCommand cmd)
        {
            IDbDataAdapter adapter = null;
            switch (DbType)
            {
                case DatabaseType.Oracle:
                    adapter = new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case DatabaseType.PostgresSql:
                    adapter = new NpgsqlDataAdapter((NpgsqlCommand)cmd);
                    break;
                case DatabaseType.SqlServer:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    break;
                case DatabaseType.Dm:
                    adapter = new DmDataAdapter((DmCommand)cmd);
                    break;
                case DatabaseType.MySql:
                    adapter = new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case DatabaseType.Access:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;

                default: throw new Exception("数据库类型目前不支持！");
            }
            return adapter;
        }

        private DbCommand CreateDbCommand()
        {
            DbCommand cmd = null;
            switch (DbType)
            {
                case DatabaseType.Oracle:
                    cmd = new OracleCommand();
                    break;
                case DatabaseType.PostgresSql:
                    cmd = new NpgsqlCommand();
                    break;
                case DatabaseType.SqlServer:
                    cmd = new SqlCommand();
                    break;
                case DatabaseType.MySql:
                    cmd = new MySqlCommand();
                    break;
                case DatabaseType.Dm:
                    cmd = new DmCommand();
                    break;
                case DatabaseType.Access:
                    cmd = new OleDbCommand();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return cmd;
        }

        private DbCommand CreateDbCommandWithQuery(string query)
        {
            DbCommand cmd = null;
            switch (DbType)
            {
                case DatabaseType.Oracle:
                    cmd = new OracleCommand(query);
                    break;
                case DatabaseType.PostgresSql:
                    cmd = new NpgsqlCommand(query);
                    break;
                case DatabaseType.SqlServer:
                    cmd = new SqlCommand(query);
                    break;
                case DatabaseType.MySql:
                    cmd = new MySqlCommand(query);
                    break;
                case DatabaseType.Dm:
                    cmd = new DmCommand(query);
                    break;
                case DatabaseType.Access:
                    cmd = new OleDbCommand(query);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return cmd;
        }

        public void InitConnParam(DatabaseType dbType,
            string server, int port, string dbName, string userName, string pwd)
        {
            ConnProperty = new ConnProperty();
            ConnProperty.DbType = dbType;
            ConnProperty.Server = server;
            ConnProperty.Port = port;
            ConnProperty.DataBase = dbName;
            ConnProperty.User = userName;
            ConnProperty.Password = pwd;
        }

        /// <summary>
        /// 是否存在指定表名称的表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool ExistTable(string tableName)
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }

            string strQuery = string.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES " +
                "WHERE TABLE_NAME = '{0}'", tableName);
            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = strQuery;
            //var result = cmd.ExecuteScalar();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            //this.Close();
            bool IsSDE = result > 0 ? true : false;
            return IsSDE;



        }

        /// <summary>
        /// 删除指定名称的表
        /// </summary>
        /// <param name="tableName"></param>
        public bool DeleteTable(string tableName)
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }

            string strExcute = string.Format("DROP TABLE {0}", tableName);
            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = strExcute;
            cmd.ExecuteNonQuery();
            //this.Close();
            return true;

        }

        /// <summary>
        /// 创建指定名称的表并且初始化自增主键id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="idField"></param>
        /// <returns></returns>
        public bool CreateTable(string tableName, string idField)
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }

            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            string strCreateTable = string.Format("create table {0} " +
                "({1} serial primary key)", tableName, idField);
            cmd.CommandText = strCreateTable;
            cmd.ExecuteNonQuery();
            string seq = $"{tableName}_{idField}_seq";
            if (!ExistSeq(seq, cmd))
            {
                string strCreateSeq = string.Format("create {0} " +
                "start with 1 increment by 1 no minvalue no maxvalue cache 1",
                seq);
                cmd.CommandText = strCreateSeq;
                cmd.ExecuteNonQuery();
            }
            string setNextVal = string.Format("alter table {0} alter column {1} set default nextval('{2}')"
                , tableName, idField, seq);
            cmd.CommandText = setNextVal;
            cmd.ExecuteNonQuery();
            //this.Close();
            return true;



        }

        public bool ExistSeq(string seqName, DbCommand cmd)
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }

            string strQuery = string.Format("SELECT COUNT(*) FROM information_schema.sequences " +
                "WHERE sequence_name = '{0}'", seqName);
            cmd.CommandText = strQuery;
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            //this.Close();
            bool IsSDE = result > 0 ? true : false;
            return IsSDE;



        }

        /// <summary>
        /// 在指定表中添加新的列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ColumnName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool AddColumn(string tableName, string ColumnName, string type)
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }

            string strExcute = string.Format("alter table {0} add {1} {2}",
                tableName, ColumnName, type);
            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = strExcute;
            cmd.ExecuteNonQuery();
            //this.Close();
            return true;
        }

        /// <summary>
        /// 新增:判断数据库是否为sde库
        /// </summary>
        /// <returns></returns>
        public bool IsSdeDatabase()
        {

            if (m_pConnection == null)
            {
                bool IsOpen = OpenConnect(true);
                if (!IsOpen) return false;
            }

            const string strQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA " +
                "WHERE SCHEMA_NAME = 'sde'";
            DbCommand cmd = CreateDbCommand();
            cmd.Connection = m_pConnection;
            cmd.CommandText = strQuery;
            //var result = cmd.ExecuteScalar();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            //this.Close();
            bool IsSDE = result > 0 ? true : false;
            return IsSDE;


        }


    }
}
