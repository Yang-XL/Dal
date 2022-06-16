using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.SqlWhere.Impl
{
    public class CompareSqlWhere : BaseSqlWhere
    {
        public CompareSqlWhere() { }

        public CompareSqlWhere(IFiled file, Operator op, object val)
        {
            Filed = file;
            Value = val;
            Op = op;
        }


        public object Value { get; set; }

        public override SqlInfo GetSql(ISqlDialect sqlDialect, ref IList<Parameter> parameters)
        {
            var parameName = GetParamName(sqlDialect, ref parameters);
            var sql = string.Format($"{Filed.GetSqlWhereColumnName(sqlDialect)} {Op.GetStringFormat()}", $"{parameName}");
            parameters.Add(new Parameter(parameName, Value));
            return new SqlInfo(sql, parameters);
        }
    }
}
