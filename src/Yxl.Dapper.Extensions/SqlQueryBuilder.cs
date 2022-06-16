using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.Uitls;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions
{

    public class SqlQueryBuilder<T> : ISqlBuilder
    {
        private readonly ITable _table;
        private readonly SqlWhereBuilder<T> sqlWhereBuilder;
        private readonly List<IFiled> _selectFiled;
        private readonly List<IFiled> _orderByFiled;


        public SqlQueryBuilder()
        {
            _table = typeof(T).CreateTable();
            sqlWhereBuilder = new SqlWhereBuilder<T>();
            _selectFiled = new List<IFiled> { };
            _orderByFiled = new List<IFiled> { };
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            throw new NotImplementedException();
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
        /// <summary>
        /// SqlWhere
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public SqlQueryBuilder<T> Where(Action<SqlWhereBuilder<T>> where)
        {
            where(sqlWhereBuilder);
            return this;
        }

    }


}
