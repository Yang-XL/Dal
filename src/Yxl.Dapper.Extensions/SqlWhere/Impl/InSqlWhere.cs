using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.SqlWhere.Impl
{
    public class InSqlWhere : BaseSqlWhere, IInSqlWhere
    {
        public InSqlWhere() { }

        public InSqlWhere(IFiled file, Operator op, IEnumerable val)
        {
            Filed = file;
            In = val;
            Op = op;
        }

        public IEnumerable In { get; set; }


        public override SqlInfo GetSql(ISqlDialect sqlDialect, ref IList<Parameter> parameters)
        {
            var parameName = GetParamName(sqlDialect, ref parameters);
            var sql = string.Format($"{Filed.GetSqlWhereColumnName(sqlDialect)} {Op.GetStringFormat()}", $"{parameName}");
            parameters.Add(new Parameter(parameName, In));
            return new SqlInfo(sql, parameters);
        }
    }
}
