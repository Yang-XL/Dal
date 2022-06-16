using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.SqlWhere;
using System;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.Wrapper
{

    public interface INested<out Children> : IWrapper where Children : INested<Children>
    {
        ISqlWhereGroup Group { get; set; }        

        /// <summary>
        /// 拼接一个 AND 查询条件组
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Children And(Action<Children> action);
        /// <summary>
        /// 拼接一个Or查询条件组
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Children Or(Action<Children> action);
        /// <summary>
        /// 拼接一个Or查询条件
        /// </summary>
        /// <returns></returns>
        Children Or();
        /// <summary>
        /// SqlWhere
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <returns></returns>
        SqlInfo GetSqlWhere(ISqlDialect sqlDialect);
    }

}
