using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class BaseTest
    {

        protected static IServiceProvider serviceProvider;

        [AssemblyInitialize()]
        public static async Task AssemblyInit(TestContext context)
        {
            await Task.Run(() =>
            {
                context.WriteLine("AssemblyInit Begin");
                var _services = new ServiceCollection();
                var config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
                _services.AddSingleton(config);
                _services.AddMysqlDal("dapper_test", config.GetConnectionString("dapper_test"));
                _services.AddMysqlDal("dapper_test_order", config.GetConnectionString("dapper_test_order"));
                serviceProvider = _services.BuildServiceProvider();
                context.WriteLine("AssemblyInit End");
            });
        }
    }
}
