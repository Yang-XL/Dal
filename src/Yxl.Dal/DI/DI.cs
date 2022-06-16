using System;
using Microsoft.Extensions.DependencyInjection;
using Yxl.Dal.Context;
using Yxl.Dal.Options;

namespace Yxl.Dal.DI
{
    public static class DI
    {
      

        public static IServiceCollection AddDal<TContext>(this IServiceCollection services, DbOptions dbOptions)
        {
            
            return services;
        }
    }

}
