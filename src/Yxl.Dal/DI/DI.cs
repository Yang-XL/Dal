using Microsoft.Extensions.DependencyInjection;
using Yxl.Dal.UnitWork;
using Yxl.Dapper.Extensions.DI;

namespace Yxl.Dal.DI
{
    public static class DI
    {


        public static IServiceCollection AddDal(this IServiceCollection services)
        {
            services.AddYxlDapperExtensions();
            services.AddTransient(typeof(IUnitWork<>), typeof(UnitWork<>));
            return services;
        }
    }

}
