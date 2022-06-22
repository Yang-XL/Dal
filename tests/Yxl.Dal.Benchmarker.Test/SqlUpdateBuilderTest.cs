using Mock.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dapper.Extensions;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Benchmarker.Test
{
    public class SqlUpdateBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        public void UpdateByWhereTest()
        {
            SqlUpdateBuilder<UserEntity> builder = new();
            builder.Set(a => a.Name, "Administraotr");
            builder.Where(w => w.Eq(a => a.Id, "").And(a => a.Eq(b => b.Password, "123").Or().Eq(b => b.LoginName, "admin")));
            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

        }


        public void UpdateNoWhereTest()
        {
            SqlUpdateBuilder<UserEntity> builder = new();
            builder.Set(a => a.Name, "admin").Set(a => a.Id, Guid.NewGuid());

            var sqlWhere = builder.GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

        }


        public void UpdateByIdTest()
        {
            var user = new UserEntity()
            {
                Name = "admin",
                LoginName = "admin",
                Password = "123",
                Id = Guid.NewGuid()
            };
            user.Name = "李";
            SqlUpdateBuilder<UserEntity> builder = new();
            var sqlWhere = builder.UpdateById(user).GetSql(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

            user.Name = "四";
            SqlUpdateBuilder<UserEntity> builder2 = new();
            var sqlWhere2 = builder2.UpdateById(user).GetSql(sqlDialect);
            Console.WriteLine(sqlWhere2.Sql);
            sqlWhere2.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));

        }

    }
}
