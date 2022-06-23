using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using System.Text;
using Yxl.Dapper.Extensions.Core;
using System.Linq;

namespace Yxl.Dapper.Extensions.Uitls
{
    public static class IEnumerableHelper
    {
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

        public static void GetSqlSelect(this IEnumerable<IFiled> files, ISqlDialect sqlDialect, ref SqlInfo sql)
        {
            if (files == null || !files.Any()) sql.Append("*");
            int i = 0;
            foreach (var item in files)
            {
                if (i > 0) sql.Append(",");
                sql.Append(item.GetSqlWhereColumnName(sqlDialect));
                i++;
            }
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
                sqlInfo.AddParameters(fileSql.Parameters);
            }
            sqlInfo.Sql = sb;
            return sqlInfo;
        }
    }
}
