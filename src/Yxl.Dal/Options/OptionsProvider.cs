using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Yxl.Dapper.Extensions.Enum;

namespace Yxl.Dal.Options
{
    public abstract class OptionsProvider
    {
        private readonly DbOptions options;


        public OptionsProvider()
        {
            options = new DbOptions();
        }

        protected virtual void Config(string name, string connectionString, SqlProvider sqlProvider)
        {
            options.SqlProvider = sqlProvider;
            options.ConnectionString = connectionString;
            options.Name = name;

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
