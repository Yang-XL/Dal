using Yxl.Dapper.Extensions.SqlDialect;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yxl.Dapper.Extensions.SqlDialect
{
    public class SqlCeDialect : SqlDialectBase
    {
        public override char OpenQuote
        {
            get { return '['; }
        }

        public override char CloseQuote
        {
            get { return ']'; }
        }

        public override bool SupportsMultipleStatements
        {
            get { return false; }
        }

        public override bool SupportsCountOfSubquery => false;

        public override string GetTableName(string schemaName, string tableName, string alias)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName), $"{nameof(tableName)} cannot be null or empty.");
            }

            var result = new StringBuilder();
            result.Append(OpenQuote);
            if (!string.IsNullOrWhiteSpace(schemaName))
            {
                result.AppendFormat("{0}_", schemaName);
            }

            result.AppendFormat("{0}{1}", tableName, CloseQuote);


            if (!string.IsNullOrWhiteSpace(alias))
            {
                result.AppendFormat(" AS {0}{1}{2}", OpenQuote, alias, CloseQuote);
            }

            return result.ToString();
        }

        public override string GetIdentitySql(string tableName)
        {
            return "SELECT CAST(@@IDENTITY AS BIGINT) AS [Id]";
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

            var result = string.Format("{0} OFFSET @firstResult ROWS FETCH NEXT @maxResults ROWS ONLY", sql);
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

        public override string GetPageSql(int page, int pageSize, ref IDictionary<string, object> param)
        {

            param.Add("@SQLDIALECT_AUTO_LIMIT", pageSize);
            param.Add("@SQLDIALECT_AUTO_OFFSET", GetStartValue(page, pageSize));
            return "OFFSET @PAGE_P_0 SQLDIALECT_AUTO_OFFSET FETCH NEXT @SQLDIALECT_AUTO_LIMIT ROWS ONLY";

            throw new NotImplementedException();
        }
        public override string TopSql(int top)
        {
            return $"OFFSET 0 SQLDIALECT_AUTO_OFFSET FETCH NEXT {top} ROWS ONLY";
        }

  
    }
}