using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Database
{
    public class SqlDBOperate : IDBOperate
    {
        private string m_connectionString = null;

        private SqlConnection m_conn = null;

        public string ConnectionString
        {
            get
            {
                return this.m_connectionString;
            }
            set
            {
                this.m_connectionString = value;
            }
        }

        public SqlDBOperate()
        {
        }

        public SqlDBOperate(string connectionString)
        {
            this.m_connectionString = connectionString;
        }

        public void OpenConnection()
        {
            try
            {
                if (this.m_conn == null || this.m_conn.State == ConnectionState.Closed)
                {
                    this.m_conn = new SqlConnection(this.m_connectionString);
                    this.m_conn.Open();
                }
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (this.m_conn != null && this.m_conn.State == ConnectionState.Open)
                {
                    this.m_conn.Close();
                    this.m_conn = null;
                }
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
        }

        public void Execute(string sql)
        {
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand(sql, this.m_conn);
                int num = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public int GetMaxID(string sql)
        {
            int result = 0;
            try
            {
                this.OpenConnection();
                DataTable dataTable = this.QueryTable(sql);
                if (dataTable.Rows.Count <= 0)
                {
                    result = 0;
                }
                else
                {
                    DataView defaultView = dataTable.DefaultView;
                    defaultView.Sort = dataTable.Columns[0].ColumnName + " DESC";
                    DataTable dataTable2 = defaultView.ToTable();
                    string value = Convert.ToString(dataTable2.Rows[0][0]);
                    result = Convert.ToInt32(value);
                }
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public string QueryOneValue(string sql)
        {
            string result = "";
            try
            {
                this.OpenConnection();
                DataTable dataTable = this.QueryTable(sql);
                if (dataTable.Rows.Count <= 0)
                {
                    result = "";
                }
                else
                {
                    result = Convert.ToString(dataTable.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public string QueryOneValue_2(string sql)
        {
            string result = "";
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand(sql, this.m_conn);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool flag = sqlDataReader.Read();
                if (flag)
                {
                    result = "";
                }
                else
                {
                    result = Convert.ToString(sqlDataReader[0]);
                }
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public List<string> QueryList(string sql)
        {
            List<string> list = null;
            try
            {
                this.OpenConnection();
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
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return list;
        }

        public DataTable QueryTable(string sql)
        {
            DataTable dataTable = null;
            try
            {
                this.OpenConnection();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, this.m_conn);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return dataTable;
        }

        public void Update(string sqlQuery, DataTable dtChanged)
        {
            try
            {
                this.OpenConnection();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, this.m_conn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder();
                sqlCommandBuilder.QuotePrefix = "[";
                sqlCommandBuilder.QuoteSuffix = "]";
                sqlCommandBuilder.DataAdapter = sqlDataAdapter;
                sqlDataAdapter.Update(dtChanged);
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(string sqlQuery, DataRow[] drsChanged)
        {
            try
            {
                this.OpenConnection();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, this.m_conn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder();
                sqlCommandBuilder.DataAdapter = sqlDataAdapter;
                sqlDataAdapter.Update(drsChanged);
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public List<T> QueryList<T>(string sql)
        {
            List<T> list = null;
            try
            {
                this.OpenConnection();
                DataTable dataTable = this.QueryTable(sql);
                list = new List<T>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    T item = (T)((object)dataTable.Rows[i][0]);
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex;
                }
                DBErrorLog.WriteLog(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return list;
        }

        public void BatchExecute(List<string> sqls)
        {
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.m_conn;
                sqlCommand.CommandType = CommandType.Text;
                try
                {
                    for (int i = 0; i < sqls.Count; i++)
                    {
                        string commandText = sqls[i];
                        sqlCommand.CommandText = commandText;
                        int num = sqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (!DBErrorLog.IsStartLog)
                    {
                        throw ex;
                    }
                    DBErrorLog.WriteLog(ex.Message);
                }
            }
            catch (Exception ex2)
            {
                if (!DBErrorLog.IsStartLog)
                {
                    throw ex2;
                }
                DBErrorLog.WriteLog(ex2.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public DataTable GetTableSchema(string tbName)
        {
            return null;
        }

        public DataTable GetTableSchema_2(string tbName)
        {
            return null;
        }

        public List<string> GetAlTables()
        {
            return null;
        }

        private string GetType(string s)
        {
            int num = s.LastIndexOf(".");
            string result;
            if (num > 0)
            {
                result = s.Substring(num + 1);
            }
            else
            {
                result = s;
            }
            return result;
        }
    }
}