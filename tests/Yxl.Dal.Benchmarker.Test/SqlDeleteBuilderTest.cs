using BenchmarkDotNet.Attributes;
using Mock.Entitys.DapperTest;
using Yxl.Dapper.Extensions;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Benchmarker.Test
{
    [MemoryDiagnoser]
    public class SqlDeleteBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        [Benchmark]
        public void DeleteWhereTest()
        {
            var builder = new SqlDeleteBuilder<UserEntity>();
            builder.Where(w => w.Eq(b => b.Name, "张"));
            var sql = builder.GetSql(sqlDialect);
        }
        [Benchmark]
        public void DeleteByIdTest()
        {
            var entity = new UserEntity()
            {
                Id = Guid.NewGuid()
            };
            var builder = new SqlDeleteBuilder<UserEntity>();
            builder.DeleteById(entity);
            var sql = builder.GetSql(sqlDialect);
        }
    }
}
