using Yxl.Dapper.Extensions.SqlDialect;
using System.Reflection;

namespace Yxl.Dapper.Extensions.Metadata
{
    public interface IFiled
    {
        /// <summary>
        /// 函数列
        ///  例如Top(1),NOW()
        /// </summary>
        bool FunctionColumn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        PropertyInfo MetaData { get; }

        /// <summary>
        /// 列明
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 别名
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// 忽略更新
        /// </summary>
        bool IgnoreUpdate { get; }
        /// <summary>
        /// 忽略插入
        /// </summary>
        bool IgnoreInsert { get; }
        /// <summary>
        /// 忽略查询
        /// </summary>
        bool IgnoreSelect { get; }
        /// <summary>
        /// 主键列
        /// </summary>
        bool Key { get; }
        /// <summary>
        /// 更新时间列
        /// </summary>
        bool UpdatedAt { get; }
        /// <summary>
        /// 创建时间列
        /// </summary>
        bool CreateAt { get; }

        /// <summary>
        /// 逻辑删除列
        /// </summary>
        bool LogicalDelete { get; }
        /// <summary>
        /// 租户ID列
        /// </summary>
        bool TenantId { get; set; }


        /// <summary>
        /// 所属表
        /// </summary>
        ITable Table { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <returns></returns>
        string GetParameterName(ISqlDialect sqlDialect);
        /// <summary>
        /// SqlWhere中Column
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <returns></returns>
        string GetSqlWhereColumnName(ISqlDialect sqlDialect);

        /// <summary>
        /// 查询sql(可能包含 别名)
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <returns></returns>
        string GetSelectColumnSql(ISqlDialect sqlDialect);

    }
    public class Filed : IFiled
    {
        public Filed()
        {
        }

        public ITable Table { get; set; }

        public Filed(string name, ITable table = null)
        {
            Name = name;
            Table = table;
        }

        public bool IgnoreUpdate { get; set; }

        public bool IgnoreInsert { get; set; }

        public bool IgnoreSelect { get; set; }

        public bool FunctionColumn { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool UpdatedAt { get; set; }

        public bool CreateAt { get; set; }

        public bool LogicalDelete { get; set; }

        public bool TenantId { get; set; }


        public bool Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo MetaData { get; internal set; }


        public virtual string GetParameterName(ISqlDialect sqlDialect)
        {
            return sqlDialect.ParameterPrefix + Name;
        }

        public string GetSelectColumnSql(ISqlDialect sqlDialect)
        {
            return sqlDialect.GetColumnName(Table?.GetTableAlias(sqlDialect), Name, Alias, FunctionColumn);
        }

        public string GetSqlWhereColumnName(ISqlDialect sqlDialect)
        {
            return sqlDialect.GetColumnName(Table?.GetTableAlias(sqlDialect), Name, "", FunctionColumn);
        }
    }
}
