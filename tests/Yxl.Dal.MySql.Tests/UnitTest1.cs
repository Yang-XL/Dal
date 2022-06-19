using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using System.Reflection;
using Yxl.Dal.MySql.Tests.Mock.Entity;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var d = new UserEntity()
            {
                Name = "уе"
            };
            DapperExtensions.DapperExtensions.Configure(new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new MySqlDialect()));
            using (var con = new MySqlConnection("Server=localhost;Database=dapperTest;Uid=root;Pwd=test;"))
            {
                con.Open();

                con.Insert(d);

                con.Close();

            }
            Console.WriteLine(d.Id);
        }
    }
}