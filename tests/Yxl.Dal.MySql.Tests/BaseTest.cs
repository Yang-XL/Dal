using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using Yxl.Dal.Repository;

namespace Yxl.Dal.MySql.Tests
{
    public class BaseTest
    {
        protected readonly IServiceProvider serviceProvider;

        protected readonly IConfiguration Configuration;

        public BaseTest()
        {
            var _services = new ServiceCollection();
            Configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();

            _services.AddMysqlDal(options =>
            {
                options.UserMysql(Configuration.GetConnectionString("yxl_mysql"));
            });
            serviceProvider = _services.BuildServiceProvider();
        }
    }
}
