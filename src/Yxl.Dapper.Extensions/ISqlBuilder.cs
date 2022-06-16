using System;
using System.Collections.Generic;
using System.Text;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions
{
    public interface ISqlBuilder
    {
        SqlInfo GetSql(ISqlDialect sqlDialect);
    }
}
