using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.SqlWhere;
using System;
using System.Collections.Generic;

namespace Yxl.Dapper.Extensions.Wrapper.Impl
{
    public abstract class Nested<Children> : INested<Children>
        where Children : class ,INested<Children> ,new()
    {
        public ISqlWhereGroup Group { get; set; } = new SqlWhereGroup();

        public GroupOperator QueryOperator { get; set; } = GroupOperator.And;

        public Children And(Action<Children> action)
        {
            var newWrapper = new Children();
            action(newWrapper);
            Group.Add(newWrapper.Group);
            return this as Children;
        }

        public Children Or(Action<Children> action)
        {
            var query = new Children();
            action(query);
            Group.Add(new SqlWhereGroup(query.Group, GroupOperator.Or));
            return this as Children;
        }

        public Children Or()
        {
            QueryOperator = GroupOperator.Or;
            return this as Children;
        }

        public virtual SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            return GetSqlWhere(sqlDialect);
        }

        protected virtual Children AddSqlItem(ISqlWhere sqlItem)
        {
            Group.Add(new SqlWhereGroup(sqlItem, QueryOperator));
            QueryOperator = GroupOperator.And;
            return this as Children;
        }

        public virtual SqlInfo GetSqlWhere(ISqlDialect sqlDialect)
        {
            IList<Parameter> par = new List<Parameter>();
            AppendQuery?.Invoke(this as Children);
            return Group.GetSql(sqlDialect, ref par);
        }

        protected virtual Action<Children> AppendQuery { get; set; } = null;
    }

}
