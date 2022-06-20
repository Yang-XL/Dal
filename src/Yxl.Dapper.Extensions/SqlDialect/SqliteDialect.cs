
using Yxl.Dapper.Extensions.SqlDialect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Yxl.Dapper.Extensions.SqlDialect
{
    public class SqliteDialect : SqlDialectBase
    {
        public override string GetIdentitySql(string tableName, string identityName)
        {
            return $"SELECT LAST_INSERT_ROWID() AS [{identityName}]";
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

            var result = string.Format("{0} LIMIT @Offset, @Count", sql);
            parameters.Add("@Offset", firstResult);
            parameters.Add("@Count", maxResults);
            return result;
        }

        public override string GetDatabaseFunctionString(DatabaseFunction databaseFunction, string columnName, string functionParameters = "")
        {
            switch (databaseFunction)
            {
                case DatabaseFunction.Truncate:
                    return $"Truncate({columnName})";

                case DatabaseFunction.NullValue:
                    return $"IsNull({columnName}, {functionParameters})";
                default:
                    return columnName;
            }
        }

        public override string GetPageSql(int page, int pageSize, ref IDictionary<string, object> param)
        {
            param.Add("@SQLDIALECT_AUTO_LIMIT", pageSize);
            param.Add("SQLDIALECT_AUTO_OFFSET", GetStartValue(page, pageSize));
            return "LIMIT @SQLDIALECT_AUTO_LIMIT, @SQLDIALECT_AUTO_OFFSET";
            throw new NotImplementedException();
        }
        public override string TopSql(int top)
        {
            return $"LIMIT 0, {top}";
        }
    }
}
