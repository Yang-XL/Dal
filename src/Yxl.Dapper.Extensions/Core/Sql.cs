﻿using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yxl.Dapper.Extensions.Core
{
    public class SqlInfo
    {

        public static SqlInfo SelectSqlInfo()
        {
            return new SqlInfo("SELECT ");
        }        

        public static SqlInfo UpdateSqlInfo()
        {
            return new SqlInfo("UPDATE ");
        }

        public static SqlInfo InsertSqlInfo()
        {
            return new SqlInfo("INSERT ");
        }

        public static SqlInfo FromSqlInfo()
        {
            return new SqlInfo(" FROM ");
        }

        public static SqlInfo WhereSqlInfo()
        {
            return new SqlInfo(" WHERE ");
        }


        #region 构造函数

        public SqlInfo()
        {
            Sql = new StringBuilder(150);
        }

        public SqlInfo(string sql) : this()
        {
            Sql.Append(sql);
        }
        public SqlInfo(StringBuilder sql) : this()
        {
            Sql.Append(sql);
        }
        public SqlInfo(string sql, IEnumerable<Parameter> parameters) : this()
        {
            Sql.Append(sql);
            Parameters.AddRange(parameters);
        }

        #endregion

        #region 属性
        public StringBuilder Sql { get; set; }

        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        #endregion


        public string AddParameter(string key, object val)
        {
            Parameters.Add(new Parameter(key, val));
            return key;
        }


        public void AddParameters(IEnumerable<Parameter> parameters)
        {
            if (parameters == null) return;
            Parameters.AddRange(parameters);
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

}
