using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Tests.Mock.Entity;
using Yxl.Dapper.Extensions.Wrapper.Impl;

namespace Yxl.Dapper.Extensions.Tests
{
    [TestClass]
    public class SqlWhereBuilderTest
    {
        private readonly ISqlDialect sqlDialect = new MySqlDialect();

        [TestMethod]
        public void One()
        {
            var sqlWhereBuilder = new SqlWhereBuilder<UserEntity>();
            sqlWhereBuilder.Eq(a => a.Id, "").And(a => a.Eq(b => b.LastName, "张三").Or().Eq(b => b.FirstName, "李四"));
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            Console.WriteLine(sqlWhere.Sql);
            sqlWhere.Parameters.ForEach(a => Console.WriteLine($"[{a.Name} : {a.Value}]"));
        }
    }
}
