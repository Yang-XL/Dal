using Mock.Entitys.DapperTest;
using Yxl.Dapper.Extensions.SqlDialect;

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
                Id = Guid.NewGuid(),
                Name = "张"
            };
            var builder = new SqlInsertBuilder<UserEntity>(userEntity);
            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
    }
}
