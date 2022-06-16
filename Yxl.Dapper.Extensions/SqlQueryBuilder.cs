using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.Uitls;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.SqlDialect;

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

        public SqlQueryBuilder<T> Select(params string[] filedName)
        {
            _selectFiled.AddRange(filedName?.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => new Filed { Name = a, Table = _table }));
            return this;
        }


        public SqlQueryBuilder<T> Select(Expression<Func<T, object>> filedName)
        {
            var members = ExpressionHelper.GetMemberInfo(filedName);
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


        public SqlQueryBuilder<T> Where(Action<SqlWhereBuilder<T>> where)
        {
            where(sqlWhereBuilder);
            return this;
        }



    }


}
