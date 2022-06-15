
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Yxl.Dal.Context;

namespace Yxl.Dal.Tests
{
    public class BaseTest
    {
        protected readonly IServiceProvider serviceProvider;
        private static IConfiguration Configuration { get; set; }
        public BaseTest()
        {
            var _services = new ServiceCollection();
            Configuration = new ConfigurationBuilder()
                               .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                               .Build();
            //var dbSetting = Configuration.GetSection("DBSetting").Get<List<ReadWriteConnectionOptions>>();


            //_services.AddLogging();
            //_services.AddModDataBase(dbSetting);


            serviceProvider = _services.BuildServiceProvider();
        }
    }
}
