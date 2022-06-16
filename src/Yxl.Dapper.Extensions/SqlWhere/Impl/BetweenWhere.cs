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


        public override SqlInfo GetSql(ISqlDialect sqlDialect, ref IList<Parameter> parameters)
        {
            var parameNameFrom = GetParamName(sqlDialect, ref parameters);
            parameters.Add(new Parameter(parameNameFrom, From));
            var parameNameTo = GetParamName(sqlDialect, ref parameters);
            parameters.Add(new Parameter(parameNameTo, To));
            return new SqlInfo($"({Filed.GetSqlWhereColumnName(sqlDialect)} BETWEEN {parameNameFrom} AND {parameNameTo})", parameters);
        }
    }
}
