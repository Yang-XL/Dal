using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Tests.Mock.Entity;

namespace Yxl.Dapper.Extensions.Tests
{
    [TestClass]
    public class SqlInsertBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        [TestMethod]
        public void InsertTest()
        {
            UserEntity userEntity = new()
            {
                Id = 1,
                FirstName = "张",
                LastName = "三"
            };
            var builder = new SqlInsertBuilder<UserEntity>(userEntity);
            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
    }
}
