using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using Yxl.Dapper.Extensions.Wrapper.Impl;

namespace Yxl.Dapper.Extensions
{
    public class SqlWhereBuilder<T> : Compare<Expression<Func<T, object>>, SqlWhereBuilder<T>>
    {
        private readonly ITable Table;

        public SqlWhereBuilder()
        {
            Table = typeof(T).CreateTable();
        }

        protected override IFiled GetColumn(Expression<Func<T, object>> column)
        {
            var columnName = ExpressionHelper.GetProperty(column).ToString();
            return new Filed(columnName, Table);
        }

        public override SqlInfo GetSqlWhere(ISqlDialect sqlDialect)
        {
            var allFileds = typeof(T).CreateFiles();
            foreach (var item in allFileds.Where(a => a.LogicalDelete))
            {
                AppendEq(item, false);
            }
            return base.GetSqlWhere(sqlDialect);
        }



    }
}
