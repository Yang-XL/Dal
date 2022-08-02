using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.MySql
{
    public class MySqlDbOptions : DbOptions
    {
        public MySqlDbOptions(string dbName, string connection)
        {
            Name = dbName;
            ConnectionString = connection;
            SqlDialect = new MySqlDialect();
            SqlProvider = Dapper.Extensions.Enum.SqlProvider.MYSQL;
            CreateDbConnection = () => new MySqlConnection(connection);
        }
    }
}
