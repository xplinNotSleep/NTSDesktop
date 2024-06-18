using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AG.COM.SDM.Database
{
    public interface IDBPage
    {
        /// <summary>
        /// 分页初次调用函数，返回相关分页信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tbName"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <param name="rowAll"></param>
        /// <param name="page"></param>
        /// <param name="pageAll"></param>
        /// <returns></returns>
        DataTable PageBeginBind(string sql, string tbName, int count, out int start, out int rowAll, out int page, out int pageAll);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tbName"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        DataTable PageQueryTable(string sql, string tbName, int start, int count);
    }
}
