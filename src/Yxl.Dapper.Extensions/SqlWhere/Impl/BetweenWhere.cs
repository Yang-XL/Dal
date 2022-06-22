using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.SqlWhere.Impl
{
    public class BetweenWhere : BaseSqlWhere, IBetweenWhere
    {

        public BetweenWhere() { }

        public BetweenWhere(IFiled file, object from, object to)
        {
            Filed = file;
            From = from;
            To = to;
        }


        public object From { get; set; }

        public object To { get; set; }


        public override void GetSql(ISqlDialect sqlDialect, ref SqlInfo sqlWhereItem)
        {
            var fromKey = GetParamName(sqlDialect, From, ref sqlWhereItem);
            var toKey = GetParamName(sqlDialect, To, ref sqlWhereItem);
            sqlWhereItem.Append($"({Filed.GetSqlWhereColumnName(sqlDialect)} BETWEEN {fromKey} AND {toKey})");
        }
    }
}
