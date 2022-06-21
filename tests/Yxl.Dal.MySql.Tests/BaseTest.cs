using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Yxl.Dal.MySql.Tests.Mock.Entity;
using Yxl.Dal.Repository;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class BaseTest
    {

        protected IRespository<MemberEntity> UserRepository;
        protected static IServiceProvider serviceProvider;

        [AssemblyInitialize()]
        public static async Task AssemblyInit(TestContext context)
        {
            await Task.Run(() =>
            {
                var _services = new ServiceCollection();
                var config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
                _services.AddSingleton(config);
                _services.AddMysqlDal(options =>
                {
                    options.UserMysql(config.GetConnectionString("yxl_mysql"));
                });
                serviceProvider = _services.BuildServiceProvider();
            });
        }
    }
}
