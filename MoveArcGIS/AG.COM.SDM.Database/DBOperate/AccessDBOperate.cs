using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace AG.COM.SDM.Database
{
    /// <summary>
    /// Access数据库操作类
    /// </summary>
    public class AccessDBOperate : IDBOperate, IDBPage
    {
        #region 变量，属性，构造函数

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string m_connectionString = null;   //数据库连接字符串
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private OleDbConnection m_conn = null;      //数据库连接对象
        /// <summary>
        /// 数据库连接字符串属性
        /// </summary>
        public string ConnectionString
        {
            get { return m_connectionString; }
            set { m_connectionString = value; }
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AccessDBOperate()
        {

        }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public AccessDBOperate(string connectionString)
        {
            this.m_connectionString = connectionString;
        }

        #endregion

        #region IDBOperate 成员
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (m_conn == null || m_conn.State == ConnectionState.Closed)
                {
                    m_conn = new OleDbConnection(m_connectionString);
                    m_conn.Open();
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (m_conn != null && m_conn.State == ConnectionState.Open)
                {
                    m_conn.Close();
                    m_conn = null;
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        public void Execute(string sql)
        {
            try
            {
                OpenConnection();
                OleDbCommand cmd = new OleDbCommand(sql, m_conn);
                int count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 获取某列的最大值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回编号最大值</returns>
        public int GetMaxID(string sql)
        {
            int maxID = 0;
            try
            {
                OpenConnection();
                DataTable dt = QueryTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    maxID = 0;
                }
                else
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = dt.Columns[0].ColumnName + " DESC";
                    DataTable dtOrder = dv.ToTable();

                    string maxValue = Convert.ToString(dtOrder.Rows[0][0]);
                    if (maxValue != "")
                        maxID = Convert.ToInt32(maxValue);
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return maxID;
        }
        /// <summary>
        ///  获取某列的一个值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回一个值</returns>
        public string QueryOneValue(string sql)
        {
            string value = "";
            try
            {
                OpenConnection();
                DataTable dt = QueryTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    value = "";
                }
                else
                {
                    value = Convert.ToString(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return value;
        }
        /// <summary>
        /// 获取某列的一个值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回一个值</returns>
        public string QueryOneValue_2(string sql)
        {
            string value = "";
            try
            {
                OpenConnection();
                OleDbCommand cmd = new OleDbCommand(sql, m_conn);
                OleDbDataReader dr = cmd.ExecuteReader();
                bool isNull = dr.Read();
                if (isNull)
                    value = "";
                else
                    value = Convert.ToString(dr[0]);

            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return value;
        }
        /// <summary>
        /// 获取某列的不重复列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回字符串列表</returns>
        public List<string> QueryList(string sql)
        {
            List<string> list = null;
            try
            {
                OpenConnection();
                DataTable dt = QueryTable(sql);
                list = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string value = Convert.ToString(dt.Rows[i][0]);
                    if (!list.Contains(value))
                        list.Add(value);
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }
        /// <summary>
        /// 获取DataTable对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回DataTable对象</returns>
        public System.Data.DataTable QueryTable(string sql)
        {
            DataTable dt = null;
            try
            {
                OpenConnection();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, m_conn);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        #endregion

        #region IDBOperate 成员

        /// <summary>
        /// 批量更新DataTable对象（要求数据表有主键）
        /// </summary>
        /// <param name="sqlQuery">更新查询的sql语句，用于自动构造插入，修改，删除等sql语句</param>
        /// <param name="dtChanged">发生变化的DataTable信息</param>
        public void Update(string sqlQuery, DataTable dtChanged)
        {
            try
            {
                OpenConnection();
                OleDbDataAdapter da = new OleDbDataAdapter(sqlQuery, m_conn);
                OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder();
                cmdBuilder.QuotePrefix = "[";
                cmdBuilder.QuoteSuffix = "]";
                cmdBuilder.DataAdapter = da;

                //string xx = da.InsertCommand.CommandText;

                da.Update(dtChanged);
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 批量更新数据行对象（要求数据表有主键）
        /// </summary>
        /// <param name="sqlQuery">更新查询的sql语句，用于自动构造插入，修改，删除等sql语句</param>
        /// <param name="drsChanged">发生变化的DataRow信息</param>
        public void Update(string sqlQuery, DataRow[] drsChanged)
        {
            try
            {
                OpenConnection();
                OleDbDataAdapter da = new OleDbDataAdapter(sqlQuery, m_conn);
                OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder();
                cmdBuilder.DataAdapter = da;
                da.Update(drsChanged);
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region IDBOperate 成员

        /// <summary>
        /// 获取某列的不重复泛型列表
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <returns>返回泛型列表</returns>
        public List<T> QueryList<T>(string sql)
        {
            List<T> list = null;
            try
            {
                OpenConnection();
                DataTable dt = QueryTable(sql);
                list = new List<T>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    T value = (T)dt.Rows[i][0];
                    if (!list.Contains(value))
                        list.Add(value);
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        #endregion

        #region IDBOperate 成员

        /// <summary>
        /// 批量执行sql语句
        /// </summary>
        /// <param name="sqls">sql语句列表</param>
        public void BatchExecute(List<string> sqls)
        {
            try
            {
                OpenConnection();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = m_conn;
                cmd.CommandType = CommandType.Text;
                try
                {
                    for (int i = 0; i < sqls.Count; i++)
                    {
                        string sql = sqls[i];
                        cmd.CommandText = sql;
                        int count = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception err)
                {
                    if (DBErrorLog.IsStartLog)
                        DBErrorLog.WriteLog(err.Message);
                    else
                        throw err;
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region IDBOperate 成员

        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tbName">表名称</param>
        /// <returns>返回表结构的DT对象</returns>
        public DataTable GetTableSchema(string tbName)
        {
            DataTable schemaTable = null;
            try
            {
                OpenConnection();
                schemaTable = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tbName, null });
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return schemaTable;
        }
        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tbName">表名称</param>
        /// <returns>返回表结构的DT对象</returns>
        public DataTable GetTableSchema_2(string tbName)
        {
            DataTable dtNew = null;
            try
            {
                OpenConnection();
                string sqlStr = "Select * from [" + tbName + "]";
                OleDbCommand cmd = new OleDbCommand(sqlStr, m_conn);
                OleDbDataReader read = cmd.ExecuteReader();
                DataTable dt = read.GetSchemaTable();   //注意这句话 
                read.Close();

                dtNew = new DataTable();
                dtNew.Columns.Add("TableName", typeof(string));
                dtNew.Columns.Add("FieldName", typeof(string));
                dtNew.Columns.Add("FieldType", typeof(string));
                dtNew.Columns.Add("FieldLength", typeof(int));  //字符串用
                dtNew.Columns.Add("FieldDigit", typeof(int));   //浮点数用
                dtNew.TableName = tbName + "表的主要结构";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string fieldName = Convert.ToString(dt.Rows[i]["ColumnName"]);
                    string fieldType = Convert.ToString(dt.Rows[i]["DataType"]);
                    fieldType = GetType(fieldType);
                    int fieldLength = Convert.ToInt32(dt.Rows[i]["ColumnSize"]);
                    int fieldDigit = Convert.ToInt32(dt.Rows[i]["ColumnSize"]);
                    DataRow row = dtNew.NewRow();
                    row["TableName"] = tbName;
                    row["FieldName"] = fieldName;
                    row["FieldType"] = fieldType;
                    row["FieldLength"] = fieldLength;
                    row["FieldDigit"] = fieldDigit;
                    dtNew.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dtNew;
        }
        /// <summary>
        /// 获取数据库中所有的表
        /// </summary>
        /// <returns>返回表名称列表</returns>
        public List<string> GetAlTables()
        {
            List<string> tbList = null;
            try
            {
                tbList = new List<string>();
                OpenConnection();
                DataTable schemaTable = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string tableName = Convert.ToString(schemaTable.Rows[i]["TABLE_NAME"]);
                    tbList.Add(tableName);
                }
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return tbList;
        }

        private string GetType(string s)
        {
            int pos = s.LastIndexOf(".");
            if (pos > 0)
            {
                return s.Substring(pos + 1);
            }
            else
            {
                return s;
            }

        }

        #endregion

        #region IDBPage 成员

        public DataTable PageBeginBind(string sql, string tbName, int count, out int start, out int rowAll, out int page, out int pageAll)
        {
            start = 0;
            rowAll = 0;
            page = 0;
            pageAll = 0;

            DataTable dt = null;

            try
            {
                OpenConnection();
                List<object> list = QueryList<object>(sql);
                rowAll = list.Count;
                if (rowAll == 0)
                {
                    start = 0;
                    rowAll = 0;
                    page = 0;
                    pageAll = 0;
                }
                else if (rowAll > 0)
                {
                    page = 1;
                    start = 0;

                    int yushu = rowAll % count; //余数，用于计算页数
                    if (yushu == 0)
                    {
                        if (rowAll <= count)
                            pageAll = 1;
                        else
                            pageAll = rowAll / count;
                    }
                    else
                    {
                        pageAll = rowAll / count + 1;
                    }
                }

                dt = PageQueryTable(sql, tbName, start, count);
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        public DataTable PageQueryTable(string sql, string tbName, int start, int count)
        {
            DataSet ds = null;
            try
            {
                OpenConnection();
                ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, m_conn);
                da.Fill(ds, start, count, tbName);
            }
            catch (Exception ex)
            {
                if (DBErrorLog.IsStartLog)
                    DBErrorLog.WriteLog(ex.Message);
                else
                    throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return ds.Tables[tbName];
        }

        #endregion

        /// <summary>
        /// 新建mdb
        /// </summary>
        /// <param name="pCatalog">access数据库控制器</param>
        /// <param name="pLocation">数据库的位置</param>
        public static Boolean CreateNewMDB(ADOX.Catalog pCatalog, string pLocation)
        {
            try
            {
                string strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;";
                strConnect += "Data Source=";
                strConnect += pLocation;
                strConnect += ";Jet OLEDB:Engine Type=5";
                pCatalog.Create(strConnect);
                return true;
            }
            catch
            {
                MessageBox.Show("创建数据库失败！");
                return false;
            }
        }
    }
}
