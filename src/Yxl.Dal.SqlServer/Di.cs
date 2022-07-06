
using Microsoft.Extensions.DependencyInjection;
using System;
using Yxl.Dal.Context;
using Yxl.Dal.DI;
using Yxl.Dal.MySql;
using Yxl.Dal.Repository;
using Yxl.Dal.SqlServer;
using Yxl.Dal.UnitWork;

namespace Yxl.Dal.SqlServer
{
    public static class DI
    {
        private static IServiceCollection AddMsSqlserverDal(this IServiceCollection services, Action<MsSqlServerOptionsProvider> options)
        {
            MsSqlServerOptionsProvider p = new MsSqlServerOptionsProvider();
            options(p);
            p.Build();
            YxlDal.UseDal();
            services.AddScoped(typeof(IUnitWork<>), typeof(UnitWork<>));
            services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
            services.AddScoped(typeof(IRespository<>), typeof(Respository<>));
            return services;
        }

        public static IServiceCollection AddMsSqlserverDal(this IServiceCollection services, string connectionString)
        {
            services.AddMsSqlserverDal(options => options.UserSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddMsSqlserverDal(this IServiceCollection services, string dbName, string connectionString)
        {
            services.AddMsSqlserverDal(options => options.UserSqlServer(dbName, connectionString));
            return services;
        }
    }
}
