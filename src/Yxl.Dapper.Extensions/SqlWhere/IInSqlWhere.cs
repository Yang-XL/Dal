using System.Collections;

namespace Yxl.Dapper.Extensions.SqlWhere
{
    public interface IInSqlWhere : ISqlWhere
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable In { get; }
    }
}
