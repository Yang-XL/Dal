
using Microsoft.Extensions.DependencyInjection;
using System;
using Yxl.Dal.Context;
using Yxl.Dal.DI;
using Yxl.Dal.MySql;
using Yxl.Dal.Options;
using Yxl.Dal.Repository;
using Yxl.Dal.SqlServer;
using Yxl.Dal.UnitWork;

namespace Yxl.Dal.SqlServer
{
    public static class DI
    {

        public static void AddMsSqlserver(this DbOptions options, string dbName, string connectionString)
        {
            Dal.DI.YxlDal.Register(new SqlServerDbOptions(dbName, connectionString));
        }
    }
}
