using Microsoft.Extensions.DependencyInjection;
using System;
using Yxl.Dal.Context;
using Yxl.Dal.Options;
using Yxl.Dal.Repository;
using Yxl.Dal.UnitWork;
using Yxl.Dapper.Extensions.Dapper;

namespace Yxl.Dal
{
    public static class YxlDal
    {
        public static IServiceCollection AddYxlDal(this IServiceCollection services, Action<DbOptions> config)
        {
            DapperExtenstion.UseDapper();
            services.AddScoped(typeof(IUnitWork<>), typeof(UnitWork<>));
            services.AddSingleton(typeof(IRespository<>), typeof(Respository<>));
            return services;
        }

        public static void Register(DbOptions options)
        {
            DbContextProvider.Register(options);
        }
    }

}
