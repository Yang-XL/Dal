using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions
{


    public class SqlUpdateBuilder<T> : ISqlBuilder
    {
        private readonly List<IUpdateFiled> _updateFiled;
        private readonly SqlWhereBuilder<T> sqlWhereBuilder;

        private readonly ITable _updaeTable;

        public SqlUpdateBuilder()
        {
            _updaeTable = typeof(T).CreateTable();
            _updateFiled = new List<IUpdateFiled>();
            sqlWhereBuilder = new SqlWhereBuilder<T>();
        }

        public SqlUpdateBuilder<T> Where(Action<SqlWhereBuilder<T>> where)
        {
            where(sqlWhereBuilder);
            return this;
        }

        public SqlUpdateBuilder<T> Set(Expression<Func<T, object>> colum, object value)
        {
            _updateFiled.Add(new UpdateFiled(new Filed(ExpressionHelper.GetProperty(colum).ToString(), _updaeTable), value));
            return this;
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            var sqlInfo = new SqlInfo();
            var filedSql = _updateFiled.GetSql(sqlDialect);
            sqlInfo.Append($"UPDATE {_updaeTable.GetTableName(sqlDialect)} SET {filedSql.Sql}");
            sqlInfo.AddParameter(filedSql.Parameters);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            if (!string.IsNullOrWhiteSpace(sqlWhere.Sql))
            {
                sqlInfo.Append($" WHERE {sqlWhere.Sql}");
                sqlInfo.AddParameter(sqlWhere.Parameters);
            }
            return sqlInfo;
        }


        public SqlUpdateBuilder<T> UpdateById(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity Model Is Null");
            foreach (var item in typeof(T).CreateFiles())
            {
                if (item.IgnoreUpdate || item.CreateAt) continue;
                if (item.Key)
                {
                    sqlWhereBuilder.Eq(item, item.MetaData.GetValue(entity));
                    continue;
                }
                _updateFiled.Add(new UpdateFiled(item, item.UpdatedAt ? DateTime.Now : item.MetaData.GetValue(entity)));
            }
            return this;
        }

        internal SqlUpdateBuilder<T> LogicalDelete(Action<SqlWhereBuilder<T>> where)
        {
            foreach (var item in typeof(T).CreateFiles().Where(a => a.LogicalDelete))
            {
                _updateFiled.Add(new UpdateFiled(item, false));
            }
            sqlWhereBuilder.And(where);
            return this;
        }



        internal SqlUpdateBuilder<T> LogicalDeleteById(T entity)
        {

            if (entity == null) throw new ArgumentNullException("Entity Model Is Null");
            foreach (var item in typeof(T).CreateFiles())
            {
                if (item.IgnoreUpdate) continue;
                if (item.Key)
                {
                    sqlWhereBuilder.Eq(item, item.MetaData.GetValue(entity));
                    continue;
                }
                if (item.LogicalDelete)
                {
                    _updateFiled.Add(new UpdateFiled(item, false));
                }

            }
            return this;
        }
    }
}
