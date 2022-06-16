using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Yxl.Dapper.Extensions
{
    public class SqlInfo
    {
        public SqlInfo() { }

        public SqlInfo(string sql)
        {
            Sql = sql;
        }
        public SqlInfo(string sql, IEnumerable<Parameter> parameters)
        {
            Sql = sql;
            Parameters = parameters.ToList();
        }

        public string Sql { get; set; }
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public void AddParameter(params Parameter[] parameter)
        {
            Parameters.AddRange(parameter);
        }


        public void AddParameter(IEnumerable<Parameter> parameters)
        {
            if (parameters == null) return;
            Parameters.AddRange(parameters);
        }
        public void AddParameter(IDictionary<string, object> parameters)
        {
            if (parameters == null) return;
            Parameters.AddRange(parameters.Select(a => new Parameter(a.Key, a.Value)));
        }

        public SqlInfo Append(SqlInfo sqlInfo)
        {
            if (sqlInfo == null) return this;
            Sql = $"{Sql} {sqlInfo.Sql}";
            AddParameter(sqlInfo.Parameters);
            return this;
        }

        public SqlInfo Append(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return this;
            Sql = $"{Sql} {sql}";
            return this;
        }
        public SqlInfo Append(string sql, IDictionary<string, object> parameters)
        {
            if (parameters.Any())
            {
                AddParameter(parameters);
            }
            if (string.IsNullOrWhiteSpace(sql)) return this;
            Sql = $"{Sql} {sql}";
            return this;
        }

        public DynamicParameters GetDynamicParameters()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var p in Parameters)
            {
                dynamicParameters.Add(p.Name, p.Value, p.DbType,
                                      p.ParameterDirection, p.Size, p.Precision,
                                      p.Scale);

            }
            return dynamicParameters;
        }
    }

    public class PageSqlInfo
    {
        public SqlInfo PagedSql { get; set; }

        public SqlInfo TotalSql { get; set; }
    }
}
