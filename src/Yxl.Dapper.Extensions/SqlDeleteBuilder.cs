﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yxl.Dapper.Extensions.Core;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;

namespace Yxl.Dapper.Extensions
{
    public class SqlDeleteBuilder<T> : ISqlBuilder
    {
        private readonly SqlWhereBuilder<T> sqlWhereBuilder;
        private readonly bool logicalDelete;
        private readonly SqlUpdateBuilder<T> sqlUpdateBuilder;
        private readonly ITable table;


        public SqlDeleteBuilder()
        {
            table = typeof(T).CreateTable();
            sqlWhereBuilder = new SqlWhereBuilder<T>();
            var allFiles = typeof(T).CreateFiles();
            logicalDelete = allFiles.Any(a => a.LogicalDelete);
            sqlUpdateBuilder = new SqlUpdateBuilder<T>();

        }

        public SqlDeleteBuilder<T> Where(Action<SqlWhereBuilder<T>> where)
        {
            if (logicalDelete)
            {
                sqlUpdateBuilder.LogicalDelete(where);
            }
            where(sqlWhereBuilder);
            return this;
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            if (logicalDelete)
            {
                return sqlUpdateBuilder.GetSql(sqlDialect);
            }

            var sqlInfo = new SqlInfo();
            sqlInfo.Append($"DELETE FROM {table.GetTableName(sqlDialect)}");
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
            sqlInfo.AppendSqlWhere(sqlWhere);
            return sqlInfo;
        }

        public SqlDeleteBuilder<T> DeleteById(T entity)
        {
            if (logicalDelete)
            {
                sqlUpdateBuilder.LogicalDeleteById(entity);
                return this;
            }
            var allFiles = typeof(T).CreateFiles();
            foreach (var item in allFiles.Where(a => a.Key))
            {
                sqlWhereBuilder.Eq(item, item.MetaData.GetValue(entity));
            }
            return this;
        }

        public SqlDeleteBuilder<T> DeleteById(object id)
        {
            if (logicalDelete)
            {
                sqlUpdateBuilder.LogicalDeleteById(id);
                return this;
            }
            var allFiles = typeof(T).CreateFiles();
            var item = allFiles.First(a => a.Key);
            sqlWhereBuilder.Eq(item, id);
            return this;
        }
    }
}
