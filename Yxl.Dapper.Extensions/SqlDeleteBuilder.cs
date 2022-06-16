using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            sqlInfo.Append($"DELETE {table.GetTableName(sqlDialect)}");
            var sqlWhere = sqlWhereBuilder.GetSql(sqlDialect);
            if (!string.IsNullOrWhiteSpace(sqlWhere.Sql))
            {
                sqlInfo.Append($" WHERE {sqlWhere.Sql}");
                sqlInfo.AddParameter(sqlWhere.Parameters);
            }
            return sqlInfo;
        }

        public SqlDeleteBuilder<T> DeleteById(T entity)
        {
            var allFiles = typeof(T).CreateFiles();
            if (allFiles.Any(a => a.LogicalDelete))
            {
                var update = (new SqlUpdateBuilder<T>()).LogicalDeleteById(entity);
                return this;
            }
            foreach (var item in allFiles.Where(a => a.Key))
            {
                sqlWhereBuilder.Eq(item, item.MetaData.GetValue(entity));
            }
            return this;
        }
    }
}
