
using Microsoft.Extensions.DependencyInjection;
using System;
using Yxl.Dal.Context;
using Yxl.Dal.DI;
using Yxl.Dal.Repository;
using Yxl.Dapper.Extensions.DI;

namespace Yxl.Dal.MySql
{
    public static class DI
    {
        public static IServiceCollection AddMysqlDal(this IServiceCollection services, Action<MySqlOptionsProvider> options)
        {
            MySqlOptionsProvider p = new MySqlOptionsProvider();
            options(p);           
            p.Build();
            services.AddDal();
            services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
            services.AddScoped(typeof(IRespository<>), typeof(Respository<>));
            return services;
        }
    }
}
