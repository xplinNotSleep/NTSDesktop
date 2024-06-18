using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Database
{
    public interface IAdoDatabase : IDisposable
    {
        DatabaseType DbType { get; set; }
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        DbConnection DBConnection { get; }
        bool inTransaction { get; set; }
        DbTransaction BeginTrans();
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        bool OpenConnect(bool IsKeepConn);
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        bool OpenConnect_old();
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();
        /// <summary>
        /// 提交Commit
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚Commit
        /// </summary>
        void Rollback();
        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <returns>DataSet</returns>
        DataSet GetDataSet(string SqlStr);
        List<string> QueryList(string sql);
        DataTable QueryTable(string sql);
        void BatchExecute(List<string> sqls);

        void InitConnParam(DatabaseType dbType,
            string server, int port, string dbName, string userName, string pwd);
    }
}
