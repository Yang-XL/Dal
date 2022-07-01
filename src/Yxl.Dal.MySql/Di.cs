
using Microsoft.Extensions.DependencyInjection;
using System;
using Yxl.Dal.Context;
using Yxl.Dal.DI;
using Yxl.Dal.Repository;
using Yxl.Dal.UnitWork;

namespace Yxl.Dal.MySql
{
    public static class DI
    {
        private static IServiceCollection AddMysqlDal(this IServiceCollection services, Action<MySqlOptionsProvider> options)
        {
            MySqlOptionsProvider p = new MySqlOptionsProvider();
            options(p);
            p.Build();
            YxlDal.UseDal();
            services.AddScoped(typeof(IUnitWork<>), typeof(UnitWork<>));
            services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
            services.AddScoped(typeof(IRespository<>), typeof(Respository<>));
            return services;
        }

        public static IServiceCollection AddMysqlDal(this IServiceCollection services, string connectionString)
        {
            services.AddMysqlDal(options => options.UserMysql(connectionString));
            return services;
        }

        public static IServiceCollection AddMysqlDal(this IServiceCollection services, string dbName, string connectionString)
        {
            services.AddMysqlDal(options => options.UserMysql(dbName, connectionString));
            return services;
        }
    }
}
