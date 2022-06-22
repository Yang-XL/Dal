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

        public void GetSql(ISqlDialect sqlDialect, ref SqlInfo sqlWhere)
        {
            if (sqlWhere.Sql.Length > 0)
            {
                sqlWhere.Append(Operator.ToSql());
            }
            else
            {
                foreach (var item in Wheres)
                {
                    sqlWhere.Append("(");
                    item.GetSql(sqlDialect, ref sqlWhere);
                    sqlWhere.Append(")");
                }
            }

        }
    }
}
