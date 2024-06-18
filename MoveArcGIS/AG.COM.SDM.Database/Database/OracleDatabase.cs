using System;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Text;

namespace AG.COM.SDM.Database
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class OracleDatabase : IOracleDatabase
    {
        private string m_OracleDatasource;
        private string m_UserName;
        private string m_Password;
        private string m_Server;
        private OracleConnection m_pConnection;

        //Add By Ouzhp 2008-07-14 Reason:执行事务
        private OracleCommand cmdTrans;
        private OracleTransaction myTrans;

        public OracleDatabase()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Datasource">数据库名称</param>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="Server">服务器名称或IP</param>
        public OracleDatabase(string Datasource, string UserName, string Password, string Server)
        {
            m_OracleDatasource = Datasource;
            m_UserName = UserName;
            m_Password = Password;
            m_Server = Server;
            m_pConnection = null;
            //pLog = log4net.LogManager.GetLogger(typeof(OracleDatabase));//获得日志对象跟踪正在进行日志记录的类
        }

        #region IOracleDatabase 成员

        public OracleConnection DBConnection
        {
            get
            {
                // TODO:  添加 OracleConnect.DbConnection getter 实现
                if (m_pConnection == null)
                {
                    OpenConnection();
                }
                return m_pConnection;
            }
        }

        public bool OpenConnection()
        {
            // TODO:  添加 OracleConnect.OpenConnection 实现string datasource = cfg.DataSource;           
            string ConnStr = string.Format("Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST ={0})(PORT = {1})))" +
               " (CONNECT_DATA = (SERVICE_NAME = {2})));User Id={3};Password={4};",
                  m_Server, "1521", m_OracleDatasource, m_UserName, m_Password);
            //xyy 修改连接字符串
            //2020/03/24
            //string connStr = string.Format("SERVER={0};DATABASE={1};INSTANCE=5151;USER={2};PASSWORD={3};", m_Server, m_OracleDatasource, m_UserName, m_Password);

            m_pConnection = new OracleConnection();
            m_pConnection.ConnectionString = ConnStr;

            try
            {
                if (m_pConnection.State == System.Data.ConnectionState.Closed)
                {
                    m_pConnection.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }

        }

        public void CloseConnection()
        {
            if (m_pConnection != null)
            {
                if (m_pConnection.State == System.Data.ConnectionState.Open)
                {
                    m_pConnection.Close();
                }
            }
        }

        public System.Data.DataSet GetDataSet(string SqlStr)
        {
            if (m_pConnection == null)
            {
                return null;
            }
            DataSet pDataSet;

            pDataSet = new DataSet();
            OracleDataAdapter pAdapter = new OracleDataAdapter(SqlStr, m_pConnection);
            pAdapter.Fill(pDataSet);
            return pDataSet;
        }

        public bool OperateData(string SqlStr)
        {
            if (m_pConnection == null) return false;
            OracleCommand pCommand = null;
            try
            {
                pCommand = new OracleCommand();
                pCommand.Connection = m_pConnection;
                pCommand.CommandText = SqlStr;
                pCommand.ExecuteNonQuery();
                pCommand.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (pCommand != null)
                {
                    pCommand.Dispose();
                }
                return false;
            }
        }

        public bool DataTableToExcel(System.Data.DataTable dt, string excelPath)
        {
            try
            {
                StreamWriter sw;//=File.CreateText(excelPath);
                sw = new StreamWriter(excelPath, false, Encoding.GetEncoding("GB2312"));
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName.ToString().Trim());
                    sw.Write("\t");
                }
                sw.WriteLine();
                string rowTest;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        rowTest = dt.Rows[i][j].ToString().Trim();
                        sw.Write(rowTest);
                        if (j + 1 < dt.Columns.Count) sw.Write("\t");
                    }
                    sw.WriteLine();
                }
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetTableMaxID(string pTableName, string pFieldName)
        {
            string pSQL = "SELECT MAX(" + pFieldName + ") FROM " + pTableName;
            DataSet pDataSet = GetDataSet(pSQL);
            if (pDataSet == null)
            {
                return 0;
            }
            if (pDataSet.Tables[0].Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                foreach (DataRow pRow in pDataSet.Tables[0].Rows)
                {
                    return int.Parse(pRow[0].ToString());
                }
                return 0;
            }
        }

        //Add By Ouzhp 2008-07-14 Reason:执行存储过程
        public bool ExecProcedure(string strProcName, string strParm1)
        {
            if (m_pConnection == null) return false;
            OracleCommand pCommand = null;

            OracleParameter InParameter1 = new OracleParameter("strMapPrjName", OracleType.VarChar, 0);
            InParameter1.Value = strParm1;
            InParameter1.Direction = ParameterDirection.Input;
            OracleParameter OutParameter2 = new OracleParameter("Out_SelfMsg", OracleType.VarChar, 100);
            OutParameter2.Direction = ParameterDirection.Output;
            OracleParameter OutParameter3 = new OracleParameter("Out_ErrMsg", OracleType.VarChar, 100);
            OutParameter3.Direction = ParameterDirection.Output;
            OracleParameter OutParameter4 = new OracleParameter("Out_Result", OracleType.VarChar, 100);
            OutParameter4.Direction = ParameterDirection.Output;

            pCommand = new OracleCommand();
            pCommand.Connection = m_pConnection;
            pCommand.CommandType = CommandType.StoredProcedure;

            pCommand.CommandText = strProcName;

            pCommand.Parameters.Add(InParameter1);
            pCommand.Parameters.Add(OutParameter2);
            pCommand.Parameters.Add(OutParameter3);
            pCommand.Parameters.Add(OutParameter4);

            pCommand.ExecuteNonQuery();
            pCommand.Dispose();
            return true;
        }

        //Add By Ouzhp 2008-07-14 Reason:执行事务
        /// <summary>
        /// 开始事务操作 add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public bool ExecBeginTrans()
        {
            cmdTrans = m_pConnection.CreateCommand();

            // Start a local transaction
            myTrans = m_pConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmdTrans.Transaction = myTrans;
            return true;
        }

        /// <summary>
        /// 执行Trans Command add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public bool ExecTransCommand(string SqlStr)
        {
            cmdTrans.CommandText = SqlStr;
            cmdTrans.ExecuteNonQuery();
            return true;
        }

        /// <summary>
        /// 执行Commit Trans add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public bool ExecCommitTrans()
        {
            myTrans.Commit();
            return true;
        }

        /// <summary>
        /// 回滚事务操作 add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public bool ExecRollTrans()
        {
            myTrans.Rollback();
            return true;
        }

        /// <summary>
        /// 获得数据集 add By Ouzhp 2008-07-14
        /// </summary>
        /// <returns>DataSet</returns>
        public OracleDataReader GetDataReader(string SqlStr)
        {
            if (m_pConnection == null)
            {
                return null;
            }
            OracleDataReader pDataReader;
            OracleCommand pCommand = null;

            pCommand = new OracleCommand();
            pCommand.Connection = m_pConnection;
            pCommand.CommandText = SqlStr;
            pCommand.Transaction = myTrans;
            pDataReader = pCommand.ExecuteReader();

            return pDataReader;
        }

        #endregion
    }
}
