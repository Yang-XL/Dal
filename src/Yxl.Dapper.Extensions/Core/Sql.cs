using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yxl.Dapper.Extensions.Core
{
    public class SqlInfo
    {
        public SqlInfo()
        {
        }

        public SqlInfo(string sql) : base()
        {
            Sql.Append(sql);
        }
        public SqlInfo(StringBuilder sql)
        {
            Sql.Append(sql);
        }
        public SqlInfo(string sql, IEnumerable<Parameter> parameters)
        {
            Sql.Append(sql);
            Parameters.AddRange(parameters);
        }

        public StringBuilder Sql { get; set; } = new StringBuilder();


        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public void AddParameter(params Parameter[] parameter)
        {
            Parameters.AddRange(parameter);
        }

        public void AddParameter(string key, object val)
        {
            Parameters.Add(new Parameter(key, val));
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
            Sql.AppendFormat(" {0}", sqlInfo.Sql);
            Parameters.AddRange(sqlInfo.Parameters);
            return this;
        }

        public SqlInfo AppendSqlWhere(SqlInfo sqlWhere)
        {
            if (sqlWhere == null) return this;

            if (sqlWhere.Sql.Length <= 0)
            {
                sqlWhere.Append($" WHERE");
            }
            sqlWhere.Append(sqlWhere);
            return this;
        }

        public SqlInfo Append(params string[] sql)
        {
            foreach (string s in sql)
            {
                Sql.AppendFormat(" {0}", s);
            }
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
