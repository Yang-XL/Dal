using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dapper.Extensions.Metadata
{
    public interface ITable
    {
        /// <summary>
        /// 表名
        /// </summary>
        string TableName { get; }
        /// <summary>
        /// Schema
        /// </summary>
        string TableSchema { get; }
        /// <summary>
        /// 别名
        /// </summary>
        string Alias { get; set; }
        /// <summary>
        /// 获取SQL语句
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <returns></returns>
        string GetTableName(ISqlDialect sqlDialect);

        /// <summary>
        /// 获取别名 用作拼接查询列
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <returns></returns>
        string GetTableAlias(ISqlDialect sqlDialect);

    }
    public class Table : ITable
    {
        public Table(string tableName, string alias, string tableSchema = "")
        {
            TableName = tableName;
            Alias = alias;
            TableSchema = tableSchema;
        }

        public Table(string tableName) : this(tableName, string.Empty, string.Empty)
        {
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; }
        /// <summary>
        /// Schema
        /// </summary>
        public string TableSchema { get; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }


        public string GetTableName(ISqlDialect sqlDialect)
        {
            return sqlDialect.GetTableName(TableSchema, TableName, Alias);
        }

        public string GetTableAlias(ISqlDialect sqlDialect)
        {
            return string.IsNullOrWhiteSpace(Alias) ? GetTableName(sqlDialect) : Alias;
        }
    }
}
