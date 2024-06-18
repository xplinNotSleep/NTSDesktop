using System.Data;
using System.Data.OracleClient;
namespace AG.COM.SDM.Database
{
    /// <summary>
    /// Oracle连接接口
    /// </summary>
    public interface IOracleDatabase
    {
        /// <summary>
        /// 返回数据库连接对象
        /// </summary>
        OracleConnection DBConnection
        {
            get;
        }
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        bool OpenConnection();

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void CloseConnection();
        
        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <returns>DataSet</returns>
        DataSet GetDataSet(string SqlStr);
        
        /// <summary>
        /// 数据操作(增、删、改)
        /// </summary>
        /// <param name="m_sqlstr"></param>
        /// <returns></returns>
        bool OperateData(string SqlStr);
        
        /// <summary>
        /// 导出到EXCEL
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        bool DataTableToExcel(DataTable dt, string excelPath);
        
        /// <summary>
        /// 获取表中某字段最大的值
        /// </summary>
        /// <param name="pTableName"></param>
        /// <param name="pFieldName"></param>
        /// <returns></returns>
        int GetTableMaxID(string pTableName, string pFieldName);
        
        /// <summary>
        /// 执行存储过程操作 add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="strProcName">调用的存储过程名称</param>
        /// <param name="strParm1">传入参数</param>
        /// <returns></returns>
        bool ExecProcedure(string strProcName, string strParm1);
       
        /// <summary>
        /// 开始事务操作 add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        bool ExecBeginTrans();
       
        /// <summary>
        /// 执行Trans Command add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        bool ExecTransCommand(string SqlStr);
       
        /// <summary>
        /// 执行Commit Trans add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        bool ExecCommitTrans();
        
        /// <summary>
        /// 回滚事务操作 add By Ouzhp 2008-07-14
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        bool ExecRollTrans();
        
        /// <summary>
        /// 获得数据集 add By Ouzhp 2008-07-14
        /// </summary>
        /// <returns>DataSet</returns>
        OracleDataReader GetDataReader(string SqlStr);
    }

  
}
