using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Options
{
    public abstract class OptionsProvider
    {
        private readonly DbOptions options;


        public OptionsProvider()
        {
            options = new DbOptions();
        }
        protected virtual void Config(string name, string connectionString, SqlProvider sqlProvider, ISqlDialect sqlDialect)
        {
            options.SqlProvider = sqlProvider;
            options.ConnectionString = connectionString;
            options.Name = name;
            options.SqlDialect = sqlDialect;
        }

        protected virtual void Config(string name, string connectionString, SqlProvider sqlProvider)
        {
            options.SqlProvider = sqlProvider;
            options.ConnectionString = connectionString;
            options.Name = name;
            switch (sqlProvider)
            {
                case SqlProvider.MYSQL:
                    options.SqlDialect = new MySqlDialect();
                    break;
                case SqlProvider.MSSQLSERVER:
                    options.SqlDialect = new SqlServerDialect();
                    break;
                default:
                    options.SqlDialect = new MySqlDialect();
                    break;
            }
        }

        protected virtual void Config(Func<DbConnection> dbConnection)
        {
            options.CreateDbConnection = dbConnection;
        }

        public void Build()
        {
            DbOptionStore.AddOptions(options);
        }
    }
}
