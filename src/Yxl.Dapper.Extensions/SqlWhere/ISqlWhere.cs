using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.SqlWhere
{
    /// <summary>
    /// Sql Where 生成器
    /// </summary>
    public interface ISqlWhere
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">
        ///  参数
        /// </param>
        /// <returns>
        ///  SqlWhere
        ///  eg: 1 = 1 and 2 = 2 and ( 3 = 3 or 4 = 4) and 5 = 5 or 6=6
        /// </returns>
        void GetSql(ISqlDialect sqlDialect, ref SqlInfo sqlWhere);
    }



}
