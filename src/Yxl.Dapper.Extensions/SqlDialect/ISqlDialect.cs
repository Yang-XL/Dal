using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;

namespace Yxl.Dapper.Extensions.SqlDialect
{
    /// <summary>
    /// 参考 Dapper-Extensions
    /// </summary>
    public interface ISqlDialect
    {
        /// <summary>
        ///  如 SQLServer中的 [
        /// </summary>
        char OpenQuote { get; }

        /// <summary>
        ///  如 SQLServer中的 ]
        /// </summary>
        char CloseQuote { get; }
        /// <summary>
        /// 批量执行分割符号 mysql： “；”
        /// </summary>
        string BatchSeperator { get; }
        /// <summary>
        /// 多语句执行
        /// </summary>
        bool SupportsMultipleStatements { get; }
        /// <summary>
        /// 
        /// 支持子查询 统计
        /// </summary>
        bool SupportsCountOfSubquery { get; }
        /// <summary>
        /// 参数前缀 @
        /// </summary>
        char ParameterPrefix { get; }
        /// <summary>
        /// 空表达式
        /// </summary>
        string EmptyExpression { get; }
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        string GetTableName(string schemaName, string tableName, string alias);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        string GetParameterName(string columnName, string prefix = "");
        /// <summary>
        /// 列名
        /// </summary>
        /// <param name="prefix">f</param>
        /// <param name="columnName">id</param>
        /// <param name="alias">useId</param>
        /// <returns>`f`.`id` as `userId`</returns>
        string GetColumnName(string prefix, string columnName, string alias, bool functionColumn = false);
        /// <summary>
        /// IdentitySql
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string GetIdentitySql(string tableName);
        /// <summary>
        /// 生成分页完整Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetPagingSql(string sql, int page, int resultsPerPage, IDictionary<string, object> parameters);
        /// <summary>
        /// 拼接Limit Sql 条件
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string AppendLimitSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters);
        /// <summary>
        /// 是否已经 `` 包括
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsQuoted(string value);
        /// <summary>
        /// SQLServer[value]
        /// MySql `value`
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string QuoteString(string value);

        /// <summary>
        /// 数据函数区分
        /// </summary>
        /// <param name="databaseFunction"></param>
        /// <param name="columnName"></param>
        /// <param name="functionParameters"></param>
        /// <returns></returns>
        string GetDatabaseFunctionString(DatabaseFunction databaseFunction, string columnName, string functionParameters = "");

        /// <summary>
        /// Count SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        string GetCountSql(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="param">分页参数 key</param>
        /// <returns></returns>
        string GetPageSql(int page, int pageSize, ref IDictionary<string, object> param);


        string TopSql(int top);
    }
}
