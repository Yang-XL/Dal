using Yxl.Dapper.Extensions.Enum;
using System.Collections.Generic;

namespace Yxl.Dapper.Extensions.SqlWhere
{
    public interface ISqlWhereGroup : ISqlWhere
    {
        /// <summary>
        /// 
        /// </summary>
        GroupOperator Operator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IList<ISqlWhere> WheresItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        ISqlWhereGroup Add(ISqlWhere sqlwhere);
    }
}
