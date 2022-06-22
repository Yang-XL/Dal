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

        //[Benchmark]
        //public void EqTest()
        //{
        //    var guid = Guid.NewGuid();
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Eq(a => a.Id, guid).And(a => a.Eq(b => b.Name, "张三").Or().Eq(b => b.Name, "李四"));
        //    var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}
        //[Benchmark]
        //public void LambdEqTest()
        //{
        //    var guid = Guid.NewGuid();
        //    var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
        //    sqlWhereBuilder.Where(a => a.Id == guid && (a.Name == "张三" || a.Name == "李四"));
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        //[Benchmark]
        //public void NeTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Ne(a => a.Id, "").And(a => a.Eq(b => b.Name, "张三"));
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        //[Benchmark]
        //public void LambdNeTest()
        //{
        //    var id = Guid.NewGuid();
        //    var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
        //    sqlWhereBuilder.Where(a => a.Id == id && a.Name == "张三");
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}
        //private List<Guid> ids = new List<Guid>()
        //    {
        //        Guid.NewGuid(),Guid.NewGuid()
        //    };
        //[Benchmark]
        //public void InTest()
        //{

        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.In(a => a.Id, ids).And(a => a.Eq(b => b.Name, "张三").Or().Eq(b => b.Name, "李四"));
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        //[Benchmark]
        //public void LambdaInTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
        //    sqlWhereBuilder.Where(a => ids.Contains(a.Id) && (a.Name == "" || a.Name == "李四"));
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}


        //[Benchmark]
        //public void BetweenTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Between(a => a.Age, 10, 100);
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        //[Benchmark]
        //public void LambdaBetweenTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
        //    sqlWhereBuilder.Where(a => a.Age > 10 && a.Age < 100);
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        [Benchmark]
        public void GeTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Ge(a => a.Age, 10);
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
            //Console.WriteLine(sqlWhere.Sql);
            //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [Benchmark]
        public void LambdaGeTest()
        {
            var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
            sqlWhereBuilder.Where(a => a.Age >= 10);
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
            //Console.WriteLine(sqlWhere.Sql);
            //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        //[Benchmark]

        //public void EndWithTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.EndWith(a => a.Name, "张三");
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}
        //[Benchmark]

        //public void LambdaEndWithTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
        //    sqlWhereBuilder.Where(a => a.Name.EndsWith("张三"));
        //    var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}


        //[Benchmark]
        //public void StartsWithTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.StartsWith(a => a.Name, "张三");
        //    var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        //[Benchmark]
        //public void LikeTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Like(a => a.Name, "张三");
        //    var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}

        //[Benchmark]
        //public void LtTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Lt(a => a.Id, 10);
        //    var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}


        //[Benchmark]
        //public void LeTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Le(a => a.Id, 10);
        //    var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}
        //[Benchmark]
        //public void GtTest()
        //{
        //    var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
        //    sqlWhereBuilder.Gt(a => a.Id, 10);
        //    var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
        //    //Console.WriteLine(sqlWhere.Sql);
        //    //sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        //}
    }
}
