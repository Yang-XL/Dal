
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.MySql
{
    public class MsSqlServerOptionsProvider : OptionsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">当您的项目有多个数据库的时候,请给每隔数据库配置一个别名</param>
        /// <param name="connectionString"></param>
        public void UserSqlServer(string name, string connectionString)
        {
            base.Config(() => new SqlConnection(connectionString));
            base.Config(name, connectionString, Dapper.Extensions.Enum.SqlProvider.MSSQLSERVER, new SqlServerDialect());
        }

        /// <summary>
        /// 当您的项目只有一个数据的时候使用
        /// </summary>
        /// <param name="connectionString"></param>
        public void UserSqlServer(string connectionString)
        {
            UserSqlServer(String.Empty, connectionString);
        }

        public void UserSqlServer(IEnumerable<DbOptions> options)
        {
            foreach (var item in options.Where(a => a.SqlProvider == Dapper.Extensions.Enum.SqlProvider.MSSQLSERVER))
            {
                UserSqlServer(item.Name, item.ConnectionString);
            }
        }

    }
}
