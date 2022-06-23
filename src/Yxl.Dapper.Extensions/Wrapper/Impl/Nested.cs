using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.SqlWhere;
using System;
using System.Collections.Generic;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.Wrapper.Impl
{
    public abstract class Nested<Children> : INested<Children>
        where Children : class, INested<Children>, new()
    {
        public ISqlWhereGroup Group { get; set; } = new SqlWhereGroup();

        public GroupOperator QueryOperator { get; set; } = GroupOperator.And;

        public Children And(Action<Children> action)
        {
            var newWrapper = new Children();
            action(newWrapper);
            Group.Add(new SqlWhereGroup(newWrapper.Group, GroupOperator.And));
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
            Group.Operator = GroupOperator.Or;
            return this as Children;
        }

        public virtual SqlInfo GetSql(ISqlDialect sqlDialect)
        {
            return GetSqlWhere(sqlDialect);
        }

        protected virtual Children AddSqlItem(ISqlWhere sqlItem)
        {
            Group.Add(sqlItem);
            //QueryOperator = GroupOperator.And;
            return this as Children;
        }

        public virtual SqlInfo GetSqlWhere(ISqlDialect sqlDialect)
        {
            var sqlWhere = new SqlInfo();
            AppendQuery?.Invoke(this as Children);
            Group.GetSql(sqlDialect, ref sqlWhere);
            return sqlWhere;
        }

        protected virtual Action<Children> AppendQuery { get; set; } = null;
    }

}
