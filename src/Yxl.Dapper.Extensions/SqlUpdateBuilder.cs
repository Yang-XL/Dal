using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using Yxl.Dapper.Extensions.Core;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace Yxl.Dapper.Extensions
{


    public class SqlUpdateBuilder<T> : ISqlBuilder
    {
        private readonly List<IUpdateFiled> _updateFiled;
        private SqlWhereBuilder<T> sqlWhereBuilder;
        private readonly ITable _updateTable;
        private readonly IEnumerable<IFiled> _allFiled;

        public SqlUpdateBuilder()
        {
            _updateTable = typeof(T).CreateTable();
            _allFiled = typeof(T).CreateFiles();
            _updateFiled = new List<IUpdateFiled>();
            sqlWhereBuilder = new SqlWhereBuilder<T>();
            InitUpdateFile();
        }

        private void InitUpdateFile()
        {
            foreach (var item in _allFiled)
            {
                if (item.UpdatedAt && !item.IgnoreUpdate)
                {
                    TryAddFile(item, DateTime.Now);
                }
            }
        }

        public SqlUpdateBuilder<T> Where(Action<SqlWhereBuilder<T>> where)
        {
            where(sqlWhereBuilder);
            return this;
        }

        public SqlUpdateBuilder<T> Set(Expression<Func<T, object>> colum, object value)
        {
            TryAddFile(new Filed(ExpressionHelper.GetProperty(colum).ToString()), value);
            return this;
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            var sqlInfo = new SqlInfo();

            var filedSql = _updateFiled.GetSql(sqlDialect);
            sqlInfo.Append($"UPDATE {_updateTable.GetTableName(sqlDialect)} SET {filedSql.Sql}");
            sqlInfo.AddParameters(filedSql.Parameters);
            var sqlWhere = sqlWhereBuilder.GetSqlWhere(sqlDialect);
            sqlInfo.AppendSqlWhere(sqlWhere);

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
                TryAddFile(item, item.UpdatedAt ? DateTime.Now : item.MetaData.GetValue(entity));
            }
            return this;
        }

        internal SqlUpdateBuilder<T> LogicalDelete(Action<SqlWhereBuilder<T>> where)
        {
            foreach (var item in typeof(T).CreateFiles())
            {
                if (item.LogicalDelete)
                {
                    TryAddFile(item, true);
                }
            }
            sqlWhereBuilder.And(where);
            return this;
        }



        internal SqlUpdateBuilder<T> LogicalDeleteById(T entity)
        {

            if (entity == null) throw new ArgumentNullException("Entity Model Is Null");
            foreach (var item in _allFiled)
            {
                if (item.IgnoreUpdate) continue;
                if (item.Key)
                {
                    sqlWhereBuilder.Eq(item, item.MetaData.GetValue(entity));
                    continue;
                }
                if (item.LogicalDelete)
                {
                    TryAddFile(item, false);
                }
            }
            return this;
        }
        internal SqlUpdateBuilder<T> LogicalDeleteById(object id)
        {
            foreach (var item in _allFiled)
            {
                if (item.IgnoreUpdate) continue;
                if (item.Key)
                {
                    sqlWhereBuilder.Eq(item, id);
                    continue;
                }
                if (item.LogicalDelete)
                {
                    TryAddFile(item, false);
                }
            }
            return this;
        }

        private bool TryAddFile(IFiled file, object val)
        {
            IUpdateFiled updateFile = _updateFiled.FirstOrDefault(a => a.Filed.Name.Equals(file.Name));
            if (updateFile != null)
            {
                _updateFiled.Remove(updateFile);
            }
            _updateFiled.Add(new UpdateFiled(file, val));
            return true;
        }


    }
}
