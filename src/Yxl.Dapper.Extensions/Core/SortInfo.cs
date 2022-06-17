using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Core
{
    internal class SortInfo
    {
        public SortInfo(List<IFiled> filed, SortDirection sort)
        {
            Sort = sort;
            SortFiled = filed.Select(a => new SortFiled(a, sort));
        }


        internal IEnumerable<ISortFiled> SortFiled { get; }

        internal SortDirection Sort { get; }


        internal string ToSql(ISqlDialect sqlDialect)
        {
            StringBuilder sql = new StringBuilder();
            foreach (var item in SortFiled)
            {
                sql.Append($" ORDER BY {item.GetSql(sqlDialect)} {item.Sort.ToSql()}");
            }
            return sql.ToString();
        }
    }
}
