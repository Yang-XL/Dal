using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.MySql
{
    public class MySqlOptionsProvider : OptionsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">当您的项目有多个数据库的时候,请给每隔数据库配置一个别名</param>
        /// <param name="connectionString"></param>
        public void UserMysql(string name, string connectionString)
        {
            base.Config(() => new MySqlConnection(connectionString));
            base.Config(name, connectionString, Dapper.Extensions.Enum.SqlProvider.MYSQL, new MySqlDialect());
        }

        /// <summary>
        /// 当您的项目只有一个数据的时候使用
        /// </summary>
        /// <param name="connectionString"></param>
        public void UserMysql(string connectionString)
        {
            UserMysql(String.Empty, connectionString);
        }

        public void UseMysql(IEnumerable<DbOptions> options)
        {
            foreach (var item in options.Where(a => a.SqlProvider == Dapper.Extensions.Enum.SqlProvider.MYSQL))
            {
                UserMysql(item.Name, item.ConnectionString);
            }
        }

    }
}
