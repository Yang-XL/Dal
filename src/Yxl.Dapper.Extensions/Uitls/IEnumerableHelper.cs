using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using System.Text;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.Uitls
{
    public static class IEnumerableHelper
    {
        public static StringBuilder GetSqlWhere(this IEnumerable<IFiled> files, ISqlDialect sqlDialect)
        {
            var sb = new StringBuilder();
            foreach (var item in files)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(item.GetSqlWhereColumnName(sqlDialect));
            }
            return sb;
        }

        public static StringBuilder GetSqlSelect(this IEnumerable<IFiled> files, ISqlDialect sqlDialect)
        {
            var sb = new StringBuilder();
            foreach (var item in files)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(item.GetSqlWhereColumnName(sqlDialect));
            }
            return sb;
        }

        public static StringBuilder GetSql(this IEnumerable<ISortFiled> files, ISqlDialect sqlDialect)
        {
            var sb = new StringBuilder();
            foreach (var item in files)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(item.GetSql(sqlDialect));
            }
            return sb;
        }

        public static SqlInfo GetSql(this IEnumerable<IUpdateFiled> files, ISqlDialect sqlDialect)
        {
            var sqlInfo = new SqlInfo();
            var sb = new StringBuilder();
            foreach (var item in files)
            {
                if (sb.Length > 0) sb.Append(",");
                var fileSql = item.GetSql(sqlDialect);
                sb.Append(fileSql.Sql);
                sqlInfo.AddParameter(fileSql.Parameters);
            }
            sqlInfo.Sql = sb.ToString();
            return sqlInfo;
        }
    }
}
