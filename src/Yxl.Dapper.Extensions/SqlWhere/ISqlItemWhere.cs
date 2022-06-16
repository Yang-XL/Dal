using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;

namespace Yxl.Dapper.Extensions.SqlWhere
{
    public interface ISqlItemWhere : ISqlWhere
    {

        IFiled Filed { get; }

        /// <summary>
        /// 
        /// </summary>
        Operator Op { get; set; }

    }
}
