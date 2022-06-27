using Mock.Entitys.DapperTest;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Tests
{
    [TestClass]
    public class SqlDeleteBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        [TestMethod]
        public void DeleteWhereTest()
        {

            var builder = new SqlDeleteBuilder<UserEntity>();
            builder.Where(w => w.Eq(b => b.Name, "张"));
            var sql = builder.GetSql(sqlDialect);
            Console.WriteLine(sql.Sql);
            sql.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void DeleteByIdTest()
        {
            var entity = new UserEntity()
            {
                Id = Guid.NewGuid()
            };
            var builder = new SqlDeleteBuilder<UserEntity>();
            builder.DeleteById(entity);           
            var sql = builder.GetSql(sqlDialect);
            Console.WriteLine(sql.Sql);
            sql.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
    }
}
