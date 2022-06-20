
using Yxl.Dapper.Extensions.SqlDialect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Yxl.Dapper.Extensions.Enum;

namespace Yxl.Dapper.Extensions.SqlDialect
{
    public class MySqlDialect : SqlDialectBase
    {
        public override char OpenQuote
        {
            get { return '`'; }
        }

        public override char CloseQuote
        {
            get { return '`'; }
        }

        public override SqlProvider SqlProvider => SqlProvider.MYSQL;

        public override string GetIdentitySql(string tableName,string identityName)
        {
            return $"SELECT CONVERT(LAST_INSERT_ID(), SIGNED INTEGER) AS {identityName}";
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

            var result = string.Format("{0} LIMIT @maxResults OFFSET @firstResult", sql);
            parameters.Add("@firstResult", firstResult);
            parameters.Add("@maxResults", maxResults);
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

        public override string GetCountSql(string sql)
        {
            var countSQL = base.GetCountSql(sql);

            var count = Regex.Matches(sql.ToUpperInvariant(), "SELECT").Count;

            if (count > 1)
            {
                return $"{countSQL} AS {OpenQuote}Total{CloseQuote}";
            }

            return countSQL;
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