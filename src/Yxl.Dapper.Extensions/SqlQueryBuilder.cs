using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.Uitls;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Core;
using Yxl.Dapper.Extensions.Enum;

namespace Yxl.Dapper.Extensions
{

    public class SqlQueryBuilder<T> : ISqlBuilder
    {
        private readonly ITable _table;
        private readonly SqlWhereBuilder<T> _sqlWhereBuilder;
        private readonly List<IFiled> _selectFiled;
        private readonly List<IFiled> _groupBy;
        private readonly SortedSet<SortInfo> _orderByFiled;


        public SqlQueryBuilder()
        {
            _table = typeof(T).CreateTable();
            _sqlWhereBuilder = new SqlWhereBuilder<T>();
            _selectFiled = new List<IFiled> { };
            _groupBy = new List<IFiled> { };
            _orderByFiled = new SortedSet<SortInfo>();
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            var sqlInfo = new SqlInfo();
            sqlInfo.Append("SELECT ");
            sqlInfo.Append(!_selectFiled.Any() ? "*" : _selectFiled.GetSqlSelect(sqlDialect).ToString());
            sqlInfo.Append($" FROM {_table.GetTableName(sqlDialect)}");
            var sqlWhere = _sqlWhereBuilder.GetSqlWhere(sqlDialect);
            if (!string.IsNullOrWhiteSpace(sqlWhere.Sql))
            {
                sqlInfo.Append($" WHERE {sqlWhere.Sql}");
                sqlInfo.AddParameter(sqlWhere.Parameters);
            }
            sqlInfo.Append(!_groupBy.Any() ? "" : $" GROUP BY {_groupBy.GetSqlSelect(sqlDialect)}");
            foreach (var item in _orderByFiled)
            {
                sqlInfo.Append(item.ToSql(sqlDialect));
            }
            return sqlInfo;


        }
        /// <summary>
        /// 纯字符串列
        /// </summary>
        /// <param name="columnName">如： "create_at,id,name"</param>
        /// <returns></returns>
        public SqlQueryBuilder<T> Select(params string[] columnName)
        {
            _selectFiled.AddRange(columnName?.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => new Filed { Name = a, Table = _table }));
            return this;
        }
        /// <summary>
        /// Lamba 支持
        /// </summary>
        /// <param name="columnName"> 如 x=>new {x.id,x.name,}</param>
        /// <returns></returns>
        public SqlQueryBuilder<T> Select(Expression<Func<T, object>> columnName)
        {
            var members = ExpressionHelper.GetMemberInfo(columnName);
            foreach (var item in members.Where(a => a.MemberType == MemberTypes.Property))
            {
                if (item.MemberType == MemberTypes.Property && item is PropertyInfo p)
                {
                    var filed = p.CreateFiled(typeof(T));
                    if (filed.IgnoreSelect) { continue; }
                    _selectFiled.Add(filed);
                }
            }
            return this;
        }
        /// <summary>
        /// 原始封装 一般用于函数列
        /// </summary>
        /// <param name="filds">如 new Filed("NOW();")</param>
        /// <returns></returns>
        public SqlQueryBuilder<T> Select(params IFiled[] filds)
        {
            _selectFiled.AddRange(filds);
            return this;
        }

        public SqlQueryBuilder<T> OrderBy(Expression<Func<T, object>> columnName, SortDirection sort)
        {
            var members = ExpressionHelper.GetMemberInfo(columnName);
            var filedList = new List<IFiled>();
            foreach (var item in members.Where(a => a.MemberType == MemberTypes.Property))
            {
                if (item.MemberType == MemberTypes.Property && item is PropertyInfo p)
                {
                    var filed = p.CreateFiled(typeof(T));
                    if (filed.IgnoreSelect) { continue; }
                    filedList.Add(filed);
                }
            }
            _orderByFiled.Add(new SortInfo(filedList, sort));
            return this;
        }
        public SqlQueryBuilder<T> OrderByAsc(Expression<Func<T, object>> columnName)
        {
            return OrderBy(columnName, SortDirection.ASC);
        }
        public SqlQueryBuilder<T> OrderByDesc(Expression<Func<T, object>> columnName)
        {
            return OrderBy(columnName, SortDirection.DESC);
        }

        public SqlQueryBuilder<T> GroupBy(Expression<Func<T, object>> columnName)
        {
            var members = ExpressionHelper.GetMemberInfo(columnName);
            foreach (var item in members.Where(a => a.MemberType == MemberTypes.Property))
            {
                if (item.MemberType == MemberTypes.Property && item is PropertyInfo p)
                {
                    var filed = p.CreateFiled(typeof(T));
                    if (filed.IgnoreSelect) { continue; }
                    _groupBy.Add(filed);
                }
            }
            return this;
        }

        /// <summary>
        /// SqlWhere
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public SqlQueryBuilder<T> Where(Action<SqlWhereBuilder<T>> where)
        {
            where(_sqlWhereBuilder);
            return this;
        }

    }


}
