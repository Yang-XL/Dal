using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using System.Text;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions.SqlWhere
{

    public class SqlWhereGroup : ISqlWhereGroup
    {

        public GroupOperator Operator { get; set; } = GroupOperator.And;

        public IList<ISqlWhere> Wheres { get; set; } = new List<ISqlWhere>();

        public ISqlWhereGroup Add(ISqlWhere sqlwhere)
        {
            if (sqlwhere != null) { Wheres.Add(sqlwhere); };
            return this;
        }

        public SqlWhereGroup(ISqlWhere sqlWhere, GroupOperator groupOperator = GroupOperator.And)
        {
            Add(sqlWhere);
            Operator = groupOperator;
        }
        public SqlWhereGroup(GroupOperator groupOperator = GroupOperator.And) : this(null, groupOperator)
        {
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect, ref IList<Parameter> parameters)
        {
            SqlInfo sqlInfo = new SqlInfo();
            foreach (var item in Wheres)
            {
                SqlInfo sqlWhereItemOrGroup = item.GetSql(sqlDialect, ref parameters);
                if (sqlInfo.Sql.Length > 0 )
                {
                    sqlInfo.Append((item as ISqlWhereGroup).Operator.ToSql());
                }
                if (item is ISqlWhereGroup op && op.Wheres.Count > 1)
                {
                    sqlInfo.Append("(");
                    sqlInfo.Append(sqlWhereItemOrGroup);
                    sqlInfo.Append(")");
                    continue;
                }
                sqlInfo.Append(sqlWhereItemOrGroup);
            }

            return sqlInfo;
        }
    }
}
