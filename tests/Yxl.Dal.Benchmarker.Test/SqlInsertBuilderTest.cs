using Mock.Entitys;
using Yxl.Dapper.Extensions;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Benchmarker.Test
{
    public class SqlInsertBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        public void InsertTest()
        {
            UserEntity userEntity = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Administrator"
            };
            var builder = new SqlInsertBuilder<UserEntity>(userEntity);
            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
    }
}
