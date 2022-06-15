using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Yxl.Dapper.Extensions.Wrapper.Impl
{


    public class UpdateWrapper<T> : Compare<Expression<Func<T, object>>, UpdateWrapper<T>>, IUpdateWrapper<T>

    {
        public IList<IUpdateFiled> Fileds { get; protected set; } = new List<IUpdateFiled>();

        public ITable Table { get; protected set; }

        public UpdateWrapper()
        {
            Table = typeof(T).CreateTable(null);
        }


        public IUpdateWrapper<T> Set(Expression<Func<T, object>> colum, object value)
        {
            TrySetFiled(new UpdateFiled(GetColumn(colum), value));
            return this;
        }

        public IUpdateWrapper<T> Set(IFiled filed, object value)
        {
            TrySetFiled(new UpdateFiled(filed, value));
            return this;
        }

        public override SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            return CreateSqlInfo(sqlDialect, base.GetSql(sqlDialect));
        }

        public SqlInfo CreateSqlInfo(ISqlDialect sqlDialect, SqlInfo sqlWhere)
        {
            var sqlFiled = Fileds.GetSql(sqlDialect);
            var where = string.IsNullOrWhiteSpace(sqlWhere.Sql) ? "" : $"WHERE {sqlWhere.Sql}";
            sqlFiled.Sql = $"UPDATE {Table.GetTableName(sqlDialect)} SET {sqlFiled.Sql} {where}";
            sqlFiled.AddParameter(sqlWhere.Parameters);
            return sqlFiled;
        }

        public IUpdateWrapper<T> Set(params IUpdateFiled[] fileds)
        {
            foreach (var item in fileds)
            {
                TrySetFiled(item);
            }
            return this;
        }

        protected bool TrySetFiled(IUpdateFiled updateFile)
        {
            if (Fileds.Any(a => a.Filed.Name == updateFile.Filed.Name))
            {
                return false;
            }
            Fileds.Add(updateFile);
            return true;
        }

        public IUpdateWrapper<T> Set(T entity)
        {
            foreach (var item in entity.GetType().CreateFiles())
            {
                if (item.IgnoreUpdate) continue;
                if (item.Key)
                {
                    AppendQuery = (query) =>
                    {
                        query.AppendEq(item, entity.GetType().GetProperty(item.MetaData.Name).GetValue(entity));
                    };
                    continue;
                }
                Set(item, item.UpdatedAt ? DateTime.Now : entity.GetType().GetProperty(item.MetaData.Name).GetValue(entity));
            }
            return this;
        }

        protected override IFiled GetColumn(Expression<Func<T, object>> column)
        {
            var columnName = ExpressionHelper.GetProperty(column).ToString();
            return new Filed(columnName, Table);
        }

      
    }
}
