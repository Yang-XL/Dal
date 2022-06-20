using Yxl.Dapper.Extensions.SqlDialect;
using System;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.Enum;

namespace Yxl.Dapper.Extensions.SqlDialect
{
    public class SqlServerDialect : SqlDialectBase
    {
        public override char OpenQuote
        {
            get { return '['; }
        }

        public override char CloseQuote
        {
            get { return ']'; }
        }

        public override SqlProvider SqlProvider => SqlProvider.SQLCE;

        public override string GetIdentitySql(string tableName, string identityName)
        {
            return string.Format("SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [{0}]", identityName);
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

            if (string.IsNullOrEmpty(GetOrderByClause(sql)))
                sql = $"{sql} ORDER BY CURRENT_TIMESTAMP";

            var result = $"{sql} OFFSET (@skipRows) ROWS FETCH NEXT @maxResults ROWS ONLY";

            parameters.Add("@skipRows", firstResult);
            parameters.Add("@maxResults", maxResults);

            return result;
        }

        protected static string GetOrderByClause(string sql)
        {
            var orderByIndex = sql.LastIndexOf(" ORDER BY ", StringComparison.InvariantCultureIgnoreCase);
            if (orderByIndex == -1)
            {
                return null;
            }

            var result = sql.Substring(orderByIndex).Trim();

            var whereIndex = result.IndexOf(" WHERE ", StringComparison.InvariantCultureIgnoreCase);
            if (whereIndex == -1)
            {
                return result;
            }

            return result.Substring(0, whereIndex).Trim();
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
            param.Add("@SQLDIALECT_AUTO_OFFSET", GetStartValue(page, pageSize));
            return "OFFSET (@SQLDIALECT_AUTO_OFFSET) ROWS FETCH NEXT @SQLDIALECT_AUTO_LIMIT ROWS ONLY";
        }


        public override string TopSql(int top)
        {
            return $"OFFSET (0) ROWS FETCH NEXT {top} ROWS ONLY";
        }
    }
}