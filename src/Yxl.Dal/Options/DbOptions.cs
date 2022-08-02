using System;
using System.Data.Common;
using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Options
{
    public class DbOptions
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// 数据库类型
        /// </summary>
        public SqlProvider SqlProvider { get; protected set; }


        public Func<DbConnection> CreateDbConnection { get; protected set; }


        public ISqlDialect SqlDialect { get; protected set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }


}
