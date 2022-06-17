using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Tests.Mock.Entity;

namespace Yxl.Dapper.Extensions.Tests
{
    [TestClass]
    public class SqlUpdateBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();
        [TestMethod]
        public void UpdateByWhereTest()
        {
            SqlUpdateBuilder<UserEntity> builder = new();
            builder.Set(a => a.LastName, "王五");
            builder.Where(w => w.Eq(a => a.Id, "").And(a => a.Eq(b => b.FirstName, "张三").Or().Eq(b => b.LastName, "李四")));
            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

        }

        [TestMethod]
        public void UpdateNoWhereTest()
        {
            SqlUpdateBuilder<UserEntity> builder = new();
            builder.Set(a => a.LastName, "王五").Set(a => a.Id, 0);

            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

        }

        [TestMethod]
        public void UpdateByIdTest()
        {
            UserEntity user = new UserEntity()
            {
                Id = 1,
                CreateAt =DateTime.Now,
                UpdateAt = DateTime.Now,
                FirstName ="王",
                LastName = "五",              
                
            };
            user.FirstName = "李";
            SqlUpdateBuilder<UserEntity> builder = new();
            var sqlWhere = builder.UpdateById(user).GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

            user.LastName = "四";
            SqlUpdateBuilder<UserEntity> builder2 = new();
            var sqlWhere2 = builder2.UpdateById(user).GetSql(sqlDialect);
            Console.WriteLine(sqlWhere2.Sql);
            sqlWhere2.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

        }

    }
}
