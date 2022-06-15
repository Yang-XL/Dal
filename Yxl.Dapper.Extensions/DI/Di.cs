using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yxl.Dapper.Extensions.DI
{
    public static class Di
    {

        public static IServiceCollection AddMysqlDal(this IServiceCollection services)
        {
            return services;
        }
    }
}
