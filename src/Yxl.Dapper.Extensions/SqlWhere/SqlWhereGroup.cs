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

        public IList<ISqlWhere> WheresItems { get; set; } = new List<ISqlWhere>();

        public ISqlWhereGroup Add(ISqlWhere sqlwhere)
        {
            if (sqlwhere != null) { WheresItems.Add(sqlwhere); };
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
            int i = 0;
            if (WheresItems.Count > 1)
            {
                sqlWhere.Append("(");
            }
            foreach (var item in WheresItems)
            {
                if (i > 0)
                {
                    sqlWhere.Append(Operator.ToSql());
                }
                item.GetSql(sqlDialect, ref sqlWhere);
                i++;
            }
            if (WheresItems.Count > 1)
            {
                sqlWhere.Append(")");
            }

        }
    }
}
