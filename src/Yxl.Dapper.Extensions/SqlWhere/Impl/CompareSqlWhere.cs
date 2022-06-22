﻿using Yxl.Dapper.Extensions.Enum;
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

        public override void GetSql(ISqlDialect sqlDialect, ref SqlInfo sqlWhereItem)
        {
            var parameName = GetParamName(sqlDialect, Value, ref sqlWhereItem);

            var sql = string.Format($"{Filed.GetSqlWhereColumnName(sqlDialect)} {Op.GetStringFormat()}", $"{parameName}");

            sqlWhereItem.Append(sql);
        }
    }
}
