using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.Sql
{
    /// <summary>
    /// sql语句帮助类
    /// </summary>
    public static class SqlHelper
    {
        #region 分页方法
        /// <summary>
        /// 通用分页方法
        /// </summary>
        /// <param name="sql">原始Sql</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageNum">第几页</param>
        /// <returns>分页sql</returns>
        public static string getPagingSql(string sql, string pageSize, string pageNum, string OrderBy, string OrderWay)
        {
            string fenye = @"SELECT TOP {0} * 
FROM 
        (
        SELECT ROW_NUMBER() OVER (ORDER BY {3} {4}) AS RowNumber,* FROM ({1}) B
        ) A
WHERE RowNumber > {0}*({2}-1) order by {3} {4}";
            sql = string.Format(fenye, pageSize, sql, pageNum, OrderBy, OrderWay);
            return sql;
        }
        #endregion

    }
}
