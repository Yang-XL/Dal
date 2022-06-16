using Yxl.Dapper.Extensions.Core;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Wrapper
{
    /// <summary>
    /// 查询解析器
    /// </summary>
    public interface IWrapper
    {
        /// <summary>
        /// 生成SQL
        /// </summary>
        /// <returns>
        /// Item1: WHERE SQL 不包含Where关键字
        /// Item2: WHERE SQL 对应的参数
        /// </returns>
        SqlInfo GetSql(ISqlDialect sqlDialect);
    }
}
