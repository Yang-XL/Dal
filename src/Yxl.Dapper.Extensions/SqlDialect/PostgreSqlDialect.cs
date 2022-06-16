
using Yxl.Dapper.Extensions.SqlDialect;
using System;
using System.Collections.Generic;

namespace Yxl.Dapper.Extensions.SqlDialect
{
    public class PostgreSqlDialect : SqlDialectBase
    {
        public override string GetIdentitySql(string tableName)
        {
            return "SELECT LASTVAL() AS Id";
        }

        public override string GetPagingSql(string sql, int page, int resultsPerPage, IDictionary<string, object> parameters)
        {
            return AppendLimitSql(sql, GetStartValue(page, resultsPerPage), resultsPerPage, parameters);
        }

        public override string AppendLimitSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException(nameof(sql), $"{nameof(sql)} cannot be null.");

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters), $"{nameof(parameters)} cannot be null.");

            if (!IsSelectSql(sql))
                throw new ArgumentException($"{nameof(sql)} must be a SELECT statement.", nameof(sql));

            var result = string.Format("{0} LIMIT @maxResults OFFSET @pageStartRowNbr", sql);
            parameters.Add("@maxResults", maxResults);
            parameters.Add("@pageStartRowNbr", firstResult);
            return result;
        }

        public override string GetColumnName(string prefix, string columnName, string alias, bool functionColumn = false)
        {
            return base.GetColumnName(prefix, columnName, alias, functionColumn).ToLower();
        }

        public override string GetTableName(string schemaName, string tableName, string alias)
        {
            return base.GetTableName(schemaName, tableName, alias).ToLower();
        }

        public override string GetDatabaseFunctionString(DatabaseFunction databaseFunction, string columnName, string functionParameters = "")
        {
            switch (databaseFunction)
            {
                case DatabaseFunction.Truncate:
                    return $"Truncate({columnName})";

                case DatabaseFunction.NullValue:
                    return $"coalesce({columnName}, {functionParameters})";
                default:
                    return columnName;
            }

        }

        public override string GetPageSql(int page, int pageSize, ref IDictionary<string, object> param)
        {
            param.Add("SQLDIALECT_AUTO_LIMIT", pageSize);
            param.Add("SQLDIALECT_AUTO_OFFSET", GetStartValue(page, pageSize));
            return "LIMIT @SQLDIALECT_AUTO_LIMIT OFFSET @SQLDIALECT_AUTO_OFFSET";

        }
        public override string TopSql(int top)
        {
            return $"LIMIT {top} OFFSET 0";
        }

    }
}
