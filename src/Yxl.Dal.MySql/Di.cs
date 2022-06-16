
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Yxl.Dal.MySql
{
    public static class DI
    {
        public static IServiceCollection AddMysqlDal(this IServiceCollection services, Action<MySqlOptionsProvider> options)
        {
            MySqlOptionsProvider p = new MySqlOptionsProvider();
            options(p);
            p.Build();
            return services;
        }
    }
}
