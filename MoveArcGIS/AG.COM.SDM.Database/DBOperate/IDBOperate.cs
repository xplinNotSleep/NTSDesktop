using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AG.COM.SDM.Database
{
    public interface IDBOperate
    {
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        void OpenConnection();
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void CloseConnection();
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        void Execute(string sql);
        /// <summary>
        /// 获取某列的最大值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回最大值</returns>
        int GetMaxID(string sql);
        /// <summary>
        /// 获取某列的一个值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回一个值</returns>
        string QueryOneValue(string sql);
        /// <summary>
        /// 获取某列的不重复列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回字符串列表</returns>
        List<string> QueryList(string sql);
        /// <summary>
        /// 获取某列的不重复泛型列表
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <returns>返回泛型列表</returns>
        List<T> QueryList<T>(string sql);
        /// <summary>
        /// 获取DataTable对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回DataTable对象</returns>
        DataTable QueryTable(string sql);
       /// <summary>
        /// 批量更新DataTable对象（要求数据表有主键）
       /// </summary>
        /// <param name="sqlQuery">更新查询的sql语句，用于自动构造插入，修改，删除等sql语句</param>
        /// <param name="dtChanged">发生变化的DataTable信息</param>
        void Update(string sqlQuery, DataTable dtChanged);
        /// <summary>
        /// 批量更新数据行对象（要求数据表有主键）
        /// </summary>
        /// <param name="sqlQuery">更新查询的sql语句，用于自动构造插入，修改，删除等sql语句</param>
        /// <param name="drsChanged">发生变化的DataRow信息</param>
        void Update(string sqlQuery, DataRow[] drsChanged);
        /// <summary>
        /// 批量执行sql语句
        /// </summary>
        /// <param name="sqls">sql列表</param>
        void BatchExecute(List<string> sqls);
        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tbName">表名称</param>
        /// <returns>返回表结构DT</returns>
        DataTable GetTableSchema(string tbName);
        /// <summary>
        /// 获取表结构，经过处理
        /// </summary>
        /// <param name="tbName">表名称</param>
        /// <returns>返回表结构DT</returns>
        DataTable GetTableSchema_2(string tbName);
        /// <summary>
        /// 获取数据库中所有的表
        /// </summary>
        /// <returns>返回表名列表</returns>
        List<string> GetAlTables();
    }
}
