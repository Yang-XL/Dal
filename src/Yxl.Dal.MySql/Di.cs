using DapperExtensions;
using DapperExtensions.Sql;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Yxl.Dal.Context;
using Yxl.Dal.Options;

namespace Yxl.Dal.MySql
{
    public static class DI
    {

        public static IServiceCollection AddMysqlDal(this IServiceCollection services, IEnumerable<ReadWriteConnectionOptions> options)
        {
            services.AddSingleton(options);
            services.AddSingleton<ISqlGenerator, SqlGeneratorImpl>();
            services.AddSingleton<IDapperImplementor, DapperImplementor>();

            services.AddTransient<IDbContext, DbContext>();
            return services;
        }
    }
}
