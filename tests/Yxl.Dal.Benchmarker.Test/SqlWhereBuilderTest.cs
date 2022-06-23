using BenchmarkDotNet.Attributes;
using Mock.Entitys;
using Yxl.Dapper.Extensions;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Benchmarker.Test
{
    [MemoryDiagnoser]
    public class SqlWhereBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();


        [Benchmark]
        public void MultipleWhereCombinationTest()
        {
            var guid = Guid.NewGuid();
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Eq(a => a.Id, guid).And(a => a.Eq(b => b.Name, "张三").Or().Eq(b => b.Name, "李四"));
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        }
        [Benchmark]
        public void LambdMultipleWhereCombinationTest()
        {
            var guid = Guid.NewGuid();
            var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
            sqlWhereBuilder.Where(a => a.Id == guid && (a.Name == "张三" || a.Name == "李四"));
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        }

        [Benchmark]
        public void MultipleWhereTest()
        {
            var guid = Guid.NewGuid();
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Eq(a => a.Id, guid).Eq(b => b.Name, "张三").Eq(b => b.Name, "李四");
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        }
        [Benchmark]
        public void LambdMultipleWhereTest()
        {
            var guid = Guid.NewGuid();
            var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
            sqlWhereBuilder.Where(a => a.Id == guid && a.Name == "张三" && a.Name == "李四");
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        }

        [Benchmark]
        public void OneWhereTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Eq(a => a.Name, "张三");
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        }

        [Benchmark]
        public void LambdOneWhereTest()
        {
            var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
            sqlWhereBuilder.Where(a => a.Name == "张三");
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        }


    }
}
