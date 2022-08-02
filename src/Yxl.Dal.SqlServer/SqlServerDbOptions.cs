using System.Data.SqlClient;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.MySql
{
    public class SqlServerDbOptions : DbOptions
    {
        public SqlServerDbOptions(string dbName, string connection)
        {
            Name = dbName;
            ConnectionString = connection;
            SqlDialect = new MySqlDialect();
            SqlProvider = Dapper.Extensions.Enum.SqlProvider.MYSQL;
            CreateDbConnection = () => new SqlConnection(connection);
        }
    }
}
