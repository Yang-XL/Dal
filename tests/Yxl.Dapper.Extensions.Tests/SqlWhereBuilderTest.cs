using Mock.Entitys;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Tests
{
    [TestClass]
    public class SqlWhereBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        [TestMethod]
        public void EqTest()
        {
            var guid = Guid.NewGuid();
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Eq(a => a.Id, guid).And(a => a.Eq(b => b.Name, "张三").Or().Eq(b => b.Name, "李四"));
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
        [TestMethod]
        public void LambdEqTest()
        {
            var guid = Guid.NewGuid();
            var sqlWhereBuilder = new SqlWhereLambdaBuilder<UserEntity>();
            sqlWhereBuilder.Where(a => a.Id == guid && (a.Name == "张三" || a.Name == "李四"));
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
        [TestMethod]
        public void NeTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Ne(a => a.Id, "").And(a => a.Eq(b => b.Name, "张三").Or().Eq(b => b.Name, "李四"));
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void InTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.In(a => a.Id, new[] { 1, 2 });
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void BetweenTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Between(a => a.Id, 10, 100);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void GeTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Ge(a => a.Id, 10);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void EndWithTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.EndWith(a => a.Name, "张三");
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void StartsWithTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.StartsWith(a => a.Name, "张三");
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void LikeTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Like(a => a.Name, "张三");
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void LtTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Lt(a => a.Id, 10);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }


        [TestMethod]
        public void LeTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Le(a => a.Id, 10);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }

        [TestMethod]
        public void GtTest()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Gt(a => a.Id, 10);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
    }
}
