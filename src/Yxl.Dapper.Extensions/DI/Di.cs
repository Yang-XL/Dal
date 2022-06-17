using Microsoft.Extensions.DependencyInjection;
using Yxl.Dapper.Extensions.Dapper;

namespace Yxl.Dapper.Extensions.DI
{
    public static class Di
    {

        public static IServiceCollection AddYxlDapperExtensions(this IServiceCollection services)
        {
            DapperExtenstion.UseDapper();
            return services;
        }
    }
}
