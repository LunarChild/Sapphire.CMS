using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.SS
{
    /// <summary>
    /// 
    /// </summary>
    public class SSCommonLogic
    {
        /// <summary>
        /// 
        /// </summary>
        public SSCommonLogic()
        {
            if (this.SSDB == null)
            {
                this.SSDB = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, //必填
                    DbType = DbType.SqlServer, //必填
                    IsAutoCloseConnection = true, //默认false 自动关闭连接
                    InitKeyType = InitKeyType.SystemTable//从数据库读取
                }); //默认SystemTable
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public SqlSugarClient SSDB { get; set; }


    }
}
