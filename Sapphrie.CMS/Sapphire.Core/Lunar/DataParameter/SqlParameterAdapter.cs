using System.Data;
using System.Data.SqlClient;

namespace Sapphire.Core.Power
{
    /// <summary>
    /// Sql参数适配器。
    /// </summary>
    public class SqlParameterAdapter
    {
        /// <summary>
        /// 转换参数。
        /// </summary>
        /// <param name="command">SqlCommand对象。</param>
        /// <param name="parameters">参数集合。</param>
        public void ConvertParameter(SqlCommand command, Parameters parameters)
        {
            if (command != null && parameters != null)
            {
                foreach (var parameter in parameters.Entries)
                {
                    var sqlParameter = new SqlParameter(parameter.Name, this.ConvertDbType(parameter.DbType), parameter.Size) { Value = parameter.Value };
                    command.Parameters.Add(sqlParameter);
                }
            }
        }

        /// <summary>
        /// 转换数据类型。
        /// </summary>
        /// <param name="dbType">DbType。</param>
        /// <returns>返回SqlDbType。</returns>
        private SqlDbType ConvertDbType(DbType dbType)
        {
            var paraConvert = new SqlParameter { DbType = dbType };
            return paraConvert.SqlDbType;
        }
    }
}